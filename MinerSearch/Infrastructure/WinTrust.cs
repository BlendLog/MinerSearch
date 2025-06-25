//Thanx for https://www.pinvoke.net/default.aspx/wintrust.winverifytrust
// credit to Alex Dragokas 

using MSearch.Core;
using System;
using System.Collections.Generic; // Для List
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace MSearch
{

    #region WinTrustData struct field enums
    enum WinTrustDataUIChoice : uint
    {
        All = 1,
        None = 2,
        NoBad = 3,
        NoGood = 4
    }

    enum WinTrustDataRevocationChecks : uint
    {
        None = 0x00000000,
        WholeChain = 0x00000001
    }

    enum WinTrustDataChoice : uint
    {
        File = 1,
        Catalog = 2,
        Blob = 3,
        Signer = 4,
        Certificate = 5
    }

    enum WinTrustDataStateAction : uint
    {
        Ignore = 0x00000000,
        Verify = 0x00000001,
        Close = 0x00000002,
        AutoCache = 0x00000003,
        AutoCacheFlush = 0x00000004
    }

    [Flags]
    enum WinTrustDataProvFlags : uint
    {
        UseIe4TrustFlag = 0x00000001,
        NoIe4ChainFlag = 0x00000002,
        NoPolicyUsageFlag = 0x00000004,
        RevocationCheckNone = 0x00000010,
        RevocationCheckEndCert = 0x00000020,
        RevocationCheckChain = 0x00000040,
        RevocationCheckChainExcludeRoot = 0x00000080,
        SaferFlag = 0x00000100,        // Used by software restriction policies. Should not be used.
        HashOnlyFlag = 0x00000200,
        UseDefaultOsverCheck = 0x00000400,
        LifetimeSigningFlag = 0x00000800,
        CacheOnlyUrlRetrieval = 0x00001000,      // affects CRL retrieval and AIA retrieval
        DisableMD2andMD4 = 0x00002000      // Win7 SP1+: Disallows use of MD2 or MD4 in the chain except for the root
    }

    enum WinTrustDataUIContext : uint
    {
        Execute = 0,
        Install = 1
    }

    #endregion

    #region WinTrust structures
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    class WinTrustFileInfo : IDisposable
    {
        UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof(WinTrustFileInfo));
        IntPtr pszFilePath;                     // required, file name to be verified
        IntPtr hFile = IntPtr.Zero;             // optional, open handle to FilePath
        IntPtr pgKnownSubject = IntPtr.Zero;    // optional, subject type if it is known

        public WinTrustFileInfo() { }

        public WinTrustFileInfo(string _filePath)
        {
            pszFilePath = Marshal.StringToCoTaskMemAuto(_filePath);
        }
        public void Dispose()
        {
            if (pszFilePath != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pszFilePath);
                pszFilePath = IntPtr.Zero;
            }
            GC.SuppressFinalize(this);
        }
        ~WinTrustFileInfo()
        {
            Dispose();
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class WINTRUST_CATALOG_INFO : IDisposable
    {
        public UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof(WINTRUST_CATALOG_INFO));
        public UInt32 CatalogVersion = 0;
        public string CatalogFilePath;
        public string MemberTag;
        public string MemberFilePath;
        public IntPtr hMemberFile;
        public IntPtr pbCalculatedFileHash; // Pointer to the hash bytes
        public UInt32 cbCalculatedFileHash; // Size of the hash bytes
        public IntPtr CatalogContext;
        public IntPtr hCatAdmin;

        private GCHandle? _hashHandle = null; // Store the GCHandle if hash was pinned

        public WINTRUST_CATALOG_INFO() { }

        // Method to set hash and pin it
        public void SetHash(byte[] hash)
        {
            if (_hashHandle != null)
            {
                _hashHandle.Value.Free(); // Free previous handle if any
            }
            if (hash != null)
            {
                _hashHandle = GCHandle.Alloc(hash, GCHandleType.Pinned);
                pbCalculatedFileHash = _hashHandle.Value.AddrOfPinnedObject();
                cbCalculatedFileHash = (uint)hash.Length;
            }
            else
            {
                _hashHandle = null;
                pbCalculatedFileHash = IntPtr.Zero;
                cbCalculatedFileHash = 0;
            }
        }

        public void Dispose()
        {
            if (_hashHandle != null)
            {
                _hashHandle.Value.Free();
                _hashHandle = null;
                pbCalculatedFileHash = IntPtr.Zero;
                cbCalculatedFileHash = 0;
            }
            // Note: hMemberFile, hCatAdmin, CatalogContext are handles managed by WinTrust/CryptCAT,
            // and should typically be released via their respective functions (like CryptCATAdminReleaseContext/CryptCATAdminReleaseCatalogContext)
            // after the WinTrustData structure is used with StateAction.Close.
            // Disposing the struct itself doesn't release these OS handles.
            // The WINTRUST_DATA Dispose handles the UnionInfoPtr memory but relies on WVT_Data.StateAction=Close
            // to potentially clean up OS handles within the WinTrust system.
            GC.SuppressFinalize(this);
        }
        ~WINTRUST_CATALOG_INFO()
        {
            Dispose();
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    class WinTrustData : IDisposable
    {
        public UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof(WinTrustData));
        public IntPtr PolicyCallbackData = IntPtr.Zero;
        public IntPtr SIPClientData = IntPtr.Zero;
        public WinTrustDataUIChoice UIChoice = WinTrustDataUIChoice.None;
        public WinTrustDataRevocationChecks RevocationChecks = WinTrustDataRevocationChecks.None;
        public WinTrustDataChoice UnionChoice = WinTrustDataChoice.File; // required: which structure is being passed in?
        public IntPtr UnionInfoPtr; // Pointer to the specific info structure (WinTrustFileInfo or WINTRUST_CATALOG_INFO)
        public WinTrustDataStateAction StateAction = WinTrustDataStateAction.Ignore; // default to ignore
        public IntPtr StateData = IntPtr.Zero; // state data specific to the chosen action
        public String URLReference = null;
        public WinTrustDataProvFlags ProvFlags = WinTrustDataProvFlags.RevocationCheckChainExcludeRoot;
        public WinTrustDataUIContext UIContext = WinTrustDataUIContext.Execute;

        // Store the object that UnionInfoPtr points to, to call its Dispose
        private object _unionInfo = null;

        private void InitFlags()
        {
            // On Win7SP1+, don't allow MD2 or MD4 signatures
            if (WinTrust.IsWindows7SP1OrGreater())
            {
                ProvFlags |= WinTrustDataProvFlags.DisableMD2andMD4;
            }
            // Add more flags if needed, e.g., WTD_SAFER_FLAG, WTD_LIFETIME_SIGNING_FLAG etc.
            // Depending on policy requirements. Default WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT is common.
        }

        public WinTrustData(WinTrustFileInfo fileInfo)
        {
            InitFlags();
            if (fileInfo == null) throw new ArgumentNullException(nameof(fileInfo));
            UnionChoice = WinTrustDataChoice.File;
            _unionInfo = fileInfo;
            UnionInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WinTrustFileInfo)));
            Marshal.StructureToPtr(fileInfo, UnionInfoPtr, false);
        }

        public WinTrustData(WINTRUST_CATALOG_INFO catalogInfo)
        {
            InitFlags();
            if (catalogInfo == null) throw new ArgumentNullException(nameof(catalogInfo));
            UnionChoice = WinTrustDataChoice.Catalog;
            _unionInfo = catalogInfo;
            UnionInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINTRUST_CATALOG_INFO)));
            Marshal.StructureToPtr(catalogInfo, UnionInfoPtr, false);
        }

        public void Dispose()
        {
            // Dispose the specific union info object if it implements IDisposable
            IDisposable disposableUnionInfo = _unionInfo as IDisposable;
            if (disposableUnionInfo != null)
            {
                disposableUnionInfo.Dispose();
            }
            _unionInfo = null;

            // Free the unmanaged memory for the union info structure
            if (UnionInfoPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(UnionInfoPtr);
                UnionInfoPtr = IntPtr.Zero;
            }

            // StateData is allocated and managed by WinTrust itself when StateAction is Verify.
            // It should be freed by setting StateAction to Close before calling WinVerifyTrust,
            // or explicitly freed after the call if Close wasn't used (which is less common/recommended).
            // The WinTrustData structure itself doesn't own the StateData pointer memory
            // in the context it's usually used for verification followed by close.
            StateData = IntPtr.Zero; // Just null out the reference within the struct

            GC.SuppressFinalize(this);
        }
        ~WinTrustData()
        {
            Dispose();
        }
    }
    #endregion

    public enum WinVerifyTrustResult : uint
    {
        Error = 0xffffffff,
        Success = 0,
        ProviderUnknown = 0x800b0001,           // Trust provider is not recognized on this system
        ActionUnknown = 0x800b0002,         // Trust provider does not support the specified action
        SubjectFormUnknown = 0x800b0003,        // Trust provider does not support the form specified for the subject
        SubjectNotTrusted = 0x800b0004,         // Subject failed the specified verification action
        DIGSIG_ENCODE = 0x800b0005,
        DIGSIG_DECODE = 0x800b0006,
        FileNotSigned = 0x800B0100,         // TRUST_E_NOSIGNATURE - File was not signed
        SubjectExplicitlyDistrusted = 0x800B0111,   // Signer's certificate is in the Untrusted Publishers store
        SignatureOrFileCorrupt = 0x80096010,    // TRUST_E_BAD_DIGEST - file was probably corrupt
        SubjectCertExpired = 0x800B0101,        // CERT_E_EXPIRED - Signer's certificate was expired
        SubjectCertificateRevoked = 0x800B010C,     // CERT_E_REVOKED Subject's certificate was revoked
        UntrustedRoot = 0x800B0109,          // CERT_E_UNTRUSTEDROOT - A certification chain processed correctly but terminated in a root certificate that is not trusted by the trust provider.
        Unknown = 0x8000000D,
        CryptFileError = 0x80092003,
        CertIsNotValidForUsage = 0x800b0107 // CERT_E_PURPOSE
    }

    public class WinTrust
    {
        public const int MAX_PATH = 260;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private const string WINTRUST_ACTION_GENERIC_VERIFY_V2 = "{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}"; // GUID of the action to perform
        private const string DRIVER_ACTION_VERIFY = "{F750E6C3-38EE-11D1-85E5-00C04FC295EE}"; // GUID of the action to perform

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CATALOG_INFO
        {
            public UInt32 cbStruct;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string wszCatalogFile;
        }

        public const int GENERIC_READ = unchecked((int)0x80000000);
        public const int FILE_SHARE_READ = 1;
        public const int FILE_SHARE_WRITE = 2;
        public const int FILE_SHARE_DELETE = 4;
        public const int OPEN_EXISTING = 3;
        public const int FILE_ATTRIBUTE_NORMAL = 0x80;
        public const int FILE_READ_ATTRIBUTES = 0x80;
        public const int FILE_READ_DATA = 1;
        public const int STANDARD_RIGHTS_READ = 0x20000;
        public const string BCRYPT_SHA256_ALGORITHM = "SHA256";
        public const int ERROR_INSUFFICIENT_BUFFER = 122;

        const uint CERT_NAME_SIMPLE_DISPLAY_TYPE = 4;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminReleaseCatalogContext(IntPtr hCatAdmin, IntPtr hCatInfo, int dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminReleaseContext(IntPtr hCatAdmin, int dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminCalcHashFromFileHandle(
            IntPtr hFile,
            ref uint hashLength,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pbHash,
            uint dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminCalcHashFromFileHandle2(
            IntPtr hCatAdmin,
            IntPtr hFile,
            ref uint hashLength,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pbHash,
            uint dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CryptCATAdminEnumCatalogFromHash(
            IntPtr hCatAdmin,
            [In][MarshalAs(UnmanagedType.LPArray)] byte[] pbHash,
            uint cbHash,
            uint dwFlags,
            IntPtr phPrevCatInfo);

        [DllImport("wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminAcquireContext(out IntPtr phCatAdmin, [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID, int dwFlags);

        [DllImport("wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminAcquireContext2(
            out IntPtr phCatAdmin,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID,
            [In][MarshalAs(UnmanagedType.LPWStr)] string pwszHashAlgorithm,
            IntPtr pStrongHashPolicy,
            int dwFlags);

        [DllImport("wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATCatalogInfoFromContext(IntPtr hCatalog, ref CATALOG_INFO psCatInfo, int dwFlags);

        [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
        static extern WinVerifyTrustResult WinVerifyTrust(
            [In] IntPtr hwnd,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID,
            [In] WinTrustData pWVTData
        );

        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern uint CertGetNameString(IntPtr pCertContext, uint dwType, uint dwFlags, IntPtr pvTypePara, StringBuilder pszNameString, uint cchNameString);

        public WinVerifyTrustResult VerifyEmbeddedSignature(string filePath, bool displayIfNotSigned = false)
        {
            try
            {
                WinTrustFileInfo trustFileInfo = new WinTrustFileInfo(filePath);
                WinTrustData wtd = new WinTrustData(trustFileInfo); // Using block will handle dispose
                Guid guidAction = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2);

                WinVerifyTrustResult result;
                // Use a using block for WinTrustData for proper disposal of native resources
                using (wtd)
                {
                    wtd.StateAction = WinTrustDataStateAction.Verify; // Explicitly verify
                    result = WinVerifyTrust(INVALID_HANDLE_VALUE, guidAction, wtd);
                    wtd.StateAction = WinTrustDataStateAction.Close; // Tell WinTrust to cleanup state associated with this data
                } // `using` block ensures wtd.Dispose() is called here

                switch (result)
                {
                    case WinVerifyTrustResult.Success:
                        break; // Continue to return result

                    case WinVerifyTrustResult.ProviderUnknown:
                    case WinVerifyTrustResult.SubjectNotTrusted:
                    case WinVerifyTrustResult.SubjectExplicitlyDistrusted:
                    case WinVerifyTrustResult.SignatureOrFileCorrupt:
                    case WinVerifyTrustResult.SubjectCertExpired:
                    case WinVerifyTrustResult.SubjectCertificateRevoked:
                    case WinVerifyTrustResult.UntrustedRoot:
                    case WinVerifyTrustResult.DIGSIG_ENCODE:
                    case WinVerifyTrustResult.DIGSIG_DECODE:
                        // Log specific warnings for signature issues, but keep the original result.
                        LogWinTrustResult(result, filePath);
                        break; // Continue to return result

                    case WinVerifyTrustResult.ActionUnknown:
                    case WinVerifyTrustResult.SubjectFormUnknown:
                        // Log specific warnings if verbose
                        if (AppConfig.Instance.verbose)
                        {
                            LogWinTrustResult(result, filePath);
                        }
                        break; // Continue to return result

                    case WinVerifyTrustResult.FileNotSigned:
                        result = VerifyByCatalog(filePath);
                        break; // Continue to return result based on catalog check

                    default:
                        if (AppConfig.Instance.verbose)
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_CertUnknownResult", filePath);
                        }
                        break; // Continue to return result
                }

                if (result == WinVerifyTrustResult.FileNotSigned && (AppConfig.Instance.verbose || displayIfNotSigned))
                {
                    AppConfig.Instance.LL.LogWarnMessage("_CertFileNotSigned", filePath);
                }

                return result;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorVerifySignature", ex);
                return WinVerifyTrustResult.Error;
            }
        }

        // Helper method to log specific WinTrust results
        void LogWinTrustResult(WinVerifyTrustResult result, string filePath)
        {
            string logMessageKey;
            switch (result)
            {
                case WinVerifyTrustResult.ProviderUnknown: logMessageKey = "_CertSubjectNotTrusted"; break; // Often used for provider issues too
                case WinVerifyTrustResult.ActionUnknown: logMessageKey = "_CertActionUnknown"; break;
                case WinVerifyTrustResult.SubjectFormUnknown: logMessageKey = "_CertSubjectFormUnknown"; break;
                case WinVerifyTrustResult.DIGSIG_ENCODE: logMessageKey = "_CertDigsigEncode"; break;
                case WinVerifyTrustResult.DIGSIG_DECODE: logMessageKey = "_CertDigsigDecode"; break;
                case WinVerifyTrustResult.SubjectNotTrusted: logMessageKey = "_CertSubjectNotTrusted"; break;
                case WinVerifyTrustResult.SubjectExplicitlyDistrusted: logMessageKey = "_CertSubjectExplicitlyDistrusted"; break;
                case WinVerifyTrustResult.SignatureOrFileCorrupt: logMessageKey = "_CertSignatureOrFileCorrupt"; break;
                case WinVerifyTrustResult.SubjectCertExpired: logMessageKey = "_CertSubjectCertExpired"; break;
                case WinVerifyTrustResult.SubjectCertificateRevoked: logMessageKey = "_CertSubjectCertificateRevoked"; break;
                case WinVerifyTrustResult.UntrustedRoot: logMessageKey = "_CertUntrustedRoot"; break;
                case WinVerifyTrustResult.FileNotSigned: logMessageKey = "_CertFileNotSigned"; break; // Should be handled by the FileNotSigned branch
                default: logMessageKey = "_CertUnknownResult"; break;
            }
            AppConfig.Instance.LL.LogWarnMessage(logMessageKey, filePath); // Log the result code too
        }


        public static WinVerifyTrustResult VerifyByCatalog(string fileName)
        {
            WinVerifyTrustResult result = WinVerifyTrustResult.FileNotSigned; // Default result if not found/verified by catalog
            IntPtr hCatAdmin = IntPtr.Zero;
            IntPtr hFile = IntPtr.Zero;
            IntPtr catInfo = IntPtr.Zero; // HCERTSTORE for the catalog

            try
            {
                Guid guidAction = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2);
                Guid driverAction = new Guid(DRIVER_ACTION_VERIFY); // Often used for catalog verification context

                // Open the file
                hFile = CreateFile(
                    fileName,
                    FILE_READ_ATTRIBUTES | FILE_READ_DATA | STANDARD_RIGHTS_READ,
                    FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
                    IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

                if (hFile == INVALID_HANDLE_VALUE) // Check against INVALID_HANDLE_VALUE (-1), not IntPtr.Zero
                {
                    int lastError = Marshal.GetLastWin32Error();
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorOpenFileForCatalog", new System.ComponentModel.Win32Exception(lastError), fileName);
                    return WinVerifyTrustResult.Error; // Cannot open file
                }

                // Acquire Catalog Admin context
                if (IsWindows8OrGreater())
                {
                    // Try with specific hash algorithm first (SHA256 is common)
                    CryptCATAdminAcquireContext2(out hCatAdmin, driverAction, BCRYPT_SHA256_ALGORITHM, IntPtr.Zero, 0);
                }
                if (hCatAdmin == IntPtr.Zero)
                {
                    if (!CryptCATAdminAcquireContext(out hCatAdmin, driverAction, 0))
                    {
                        int lastError = Marshal.GetLastWin32Error();
                        return WinVerifyTrustResult.FileNotSigned; // Cannot get catalog admin context
                    }
                }

                // Calculate file hash
                byte[] hash = new byte[1]; // Start with small buffer
                uint hashLength = 0;
                bool bRet = false;

                // First call to get hash size
                if (IsWindows8OrGreater())
                {
                    bRet = CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, null, 0); // Pass null buffer to get size
                }
                if (!bRet || hashLength == 0) // If CryptCATAdminCalcHashFromFileHandle2 failed or older OS
                {
                    // Try CryptCATAdminCalcHashFromFileHandle (older API)
                    hashLength = 0; // Reset hashLength
                    bRet = CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, null, 0); // Pass null buffer to get size
                }

                if (!bRet || hashLength == 0)
                {
                    // Failed to get hash size
                    int lastError = Marshal.GetLastWin32Error();
                    AppConfig.Instance.LL.LogWarnMessage("_CertCannotGetHashSize", fileName);
                    return WinVerifyTrustResult.FileNotSigned; // Cannot get hash size
                }

                // Allocate buffer and get hash
                hash = new byte[hashLength];
                if (IsWindows8OrGreater())
                {
                    bRet = CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, hash, 0);
                }
                if (!bRet) // If CryptCATAdminCalcHashFromFileHandle2 failed or older OS
                {
                    bRet = CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, hash, 0);
                }

                if (!bRet)
                {
                    int lastError = Marshal.GetLastWin32Error();
                    AppConfig.Instance.LL.LogWarnMessage("_CertCannotCalcHash", fileName);
                    return WinVerifyTrustResult.FileNotSigned; // Cannot calculate hash
                }


                // Enumerate catalogs containing this hash
                catInfo = CryptCATAdminEnumCatalogFromHash(hCatAdmin, hash, hashLength, 0, IntPtr.Zero);

                if (catInfo == IntPtr.Zero)
                {
                    // Hash not found in any trusted catalog
                    return WinVerifyTrustResult.FileNotSigned;
                }

                // Found a catalog. Get catalog info.
                CATALOG_INFO ci = new CATALOG_INFO();
                ci.cbStruct = (UInt32)Marshal.SizeOf(typeof(CATALOG_INFO));

                if (!CryptCATCatalogInfoFromContext(catInfo, ref ci, 0))
                {
                    int lastError = Marshal.GetLastWin32Error();
                    AppConfig.Instance.LL.LogWarnMessage("_CertCannotGetCatalogInfo", fileName);
                    return WinVerifyTrustResult.FileNotSigned; // Cannot get catalog info
                }

                // Now verify the file against the catalog entry using WinVerifyTrust
                WINTRUST_CATALOG_INFO wci = new WINTRUST_CATALOG_INFO();
                wci.CatalogFilePath = ci.wszCatalogFile; // Path to the found catalog file
                wci.MemberFilePath = fileName;          // Path to the file being verified
                wci.hMemberFile = hFile;                // Handle to the file being verified
                wci.hCatAdmin = hCatAdmin;              // Pass the admin context
                // Pass the calculated hash bytes and size
                wci.SetHash(hash); // Use the helper to pin the hash

                WinTrustData trustData = new WinTrustData(wci);

                using (trustData) // Use using for WinTrustData disposal
                {
                    // Verify action
                    trustData.StateAction = WinTrustDataStateAction.Verify;
                    result = WinVerifyTrust(IntPtr.Zero, guidAction, trustData);

                    // Set Close action after verify
                    trustData.StateAction = WinTrustDataStateAction.Close;
                } // trustData.Dispose() is called here, which unpins the hash

                // Release Catalog Context handle
                if (catInfo != IntPtr.Zero)
                {
                    CryptCATAdminReleaseCatalogContext(hCatAdmin, catInfo, 0);
                    catInfo = IntPtr.Zero; // Nullify handle after releasing
                }

                // Release Catalog Admin handle
                if (hCatAdmin != IntPtr.Zero)
                {
                    CryptCATAdminReleaseContext(hCatAdmin, 0);
                    hCatAdmin = IntPtr.Zero; // Nullify handle after releasing
                }

                // Close file handle
                if (hFile != INVALID_HANDLE_VALUE)
                {
                    CloseHandle(hFile);
                    hFile = INVALID_HANDLE_VALUE; // Nullify handle after closing
                }

            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorVerifyByCatalog", ex, fileName);
                return WinVerifyTrustResult.Error;
            }
            finally
            {
                // Ensure all handles are closed/released even if exceptions occur within try block
                // (Although the structured handle release above is better, defensive cleanup here is good)
                if (catInfo != IntPtr.Zero)
                {
                    CryptCATAdminReleaseCatalogContext(hCatAdmin, catInfo, 0);
                }
                if (hCatAdmin != IntPtr.Zero)
                {
                    CryptCATAdminReleaseContext(hCatAdmin, 0);
                }
                if (hFile != INVALID_HANDLE_VALUE)
                {
                    CloseHandle(hFile);
                }
                // The WINTRUST_CATALOG_INFO wci's hash GCHandle is freed by trustData.Dispose() inside the using block.
            }

            return result;
        }

        public static bool IsWindows8OrGreater()
        {
            return Environment.OSVersion.Version.Major > 6 ||
                (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2);
        }

        public static bool IsWindows7SP1OrGreater()
        {
            return ((Environment.OSVersion.Version.Major > 6) ||
                ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor > 1)) ||
                ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1) && !String.IsNullOrEmpty(Environment.OSVersion.ServicePack)));
        }
    }
}
