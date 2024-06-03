//Thanx for https://www.pinvoke.net/default.aspx/wintrust.winverifytrust
// credit to Alex Dragokas 

using System;
using System.IO;
using System.Runtime.InteropServices;
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
    class WinTrustFileInfo
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
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class WINTRUST_CATALOG_INFO
    {
        public UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof(WINTRUST_CATALOG_INFO));
        public UInt32 CatalogVersion = 0;
        public string CatalogFilePath;
        public string MemberTag;
        public string MemberFilePath;
        public IntPtr hMemberFile;
        public IntPtr pbCalculatedFileHash;
        public UInt32 cbCalculatedFileHash;
        public IntPtr CatalogContext;
        public IntPtr hCatAdmin;

        public WINTRUST_CATALOG_INFO() { }

        public void Dispose() { }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    class WinTrustData
    {
        public UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof(WinTrustData));
        public IntPtr PolicyCallbackData = IntPtr.Zero;
        public IntPtr SIPClientData = IntPtr.Zero;
        public WinTrustDataUIChoice UIChoice = WinTrustDataUIChoice.None;
        public WinTrustDataRevocationChecks RevocationChecks = WinTrustDataRevocationChecks.None;
        public WinTrustDataChoice UnionChoice = WinTrustDataChoice.File; // required: which structure is being passed in?
        public IntPtr UnionInfoPtr;
        public WinTrustDataStateAction StateAction = WinTrustDataStateAction.Ignore;
        public IntPtr StateData = IntPtr.Zero;
        public String URLReference = null;
        public WinTrustDataProvFlags ProvFlags = WinTrustDataProvFlags.RevocationCheckChainExcludeRoot;
        public WinTrustDataUIContext UIContext = WinTrustDataUIContext.Execute;

        private void InitFlags()
        {
            // On Win7SP1+, don't allow MD2 or MD4 signatures
            if (WinTrust.IsWindows7SP1OrGreater())
            {
                ProvFlags |= WinTrustDataProvFlags.DisableMD2andMD4;
            }
        }

        public WinTrustData(WinTrustFileInfo _fileInfo)
        {
            InitFlags();
            WinTrustFileInfo wtfiData = _fileInfo;
            UnionInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WinTrustFileInfo)));
            Marshal.StructureToPtr(wtfiData, UnionInfoPtr, false);
        }

        public WinTrustData(WINTRUST_CATALOG_INFO _catalogInfo)
        {
            InitFlags();
            UnionChoice = WinTrustDataChoice.Catalog;
            UnionInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINTRUST_CATALOG_INFO)));
            Marshal.StructureToPtr(_catalogInfo, UnionInfoPtr, false);
        }

        public void Dispose()
        {
            if (UnionInfoPtr != IntPtr.Zero)
            {
                if (UnionChoice == WinTrustDataChoice.File)
                {
                    WinTrustFileInfo fileInfo = (WinTrustFileInfo)Marshal.PtrToStructure(UnionInfoPtr, typeof(WinTrustFileInfo));
                    fileInfo.Dispose();
                }
                else if (UnionChoice == WinTrustDataChoice.Catalog)
                {
                    WINTRUST_CATALOG_INFO catalog = (WINTRUST_CATALOG_INFO)Marshal.PtrToStructure(UnionInfoPtr, typeof(WINTRUST_CATALOG_INFO));
                    catalog.Dispose();
                }

                Marshal.FreeHGlobal(UnionInfoPtr);
                UnionInfoPtr = IntPtr.Zero;
            }
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
        TrustProviderFailed = 0x800b0005,
        ActionFailed = 0x800b0006,
        FileNotSigned = 0x800B0100,         // TRUST_E_NOSIGNATURE - File was not signed
        SubjectExplicitlyDistrusted = 0x800B0111,   // Signer's certificate is in the Untrusted Publishers store
        SignatureOrFileCorrupt = 0x80096010,    // TRUST_E_BAD_DIGEST - file was probably corrupt
        SubjectCertExpired = 0x800B0101,        // CERT_E_EXPIRED - Signer's certificate was expired
        SubjectCertificateRevoked = 0x800B010C,     // CERT_E_REVOKED Subject's certificate was revoked
        UntrustedRoot = 0x800B0109,          // CERT_E_UNTRUSTEDROOT - A certification chain processed correctly but terminated in a root certificate that is not trusted by the trust provider.
        Unknown = 0x8000000D,
        CryptFileError = 0x80092003
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

        public WinVerifyTrustResult VerifyEmbeddedSignature(string filePath)
        {
            try
            {
                WinTrustFileInfo trustFileInfo = new WinTrustFileInfo(filePath);
                WinTrustData wtd = new WinTrustData(trustFileInfo);
                Guid guidAction = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2);
                WinVerifyTrustResult result = WinVerifyTrust(INVALID_HANDLE_VALUE, guidAction, wtd);
                wtd.StateAction = WinTrustDataStateAction.Close;
                wtd.Dispose();

                switch (result)
                {
                    case WinVerifyTrustResult.SubjectNotTrusted:
                        Program.LL.LogWarnMessage("_CertSubjectNotTrusted", filePath);
                        break;
                    case WinVerifyTrustResult.SubjectExplicitlyDistrusted:
                        Program.LL.LogWarnMessage("_CertSubjectExplicitlyDistrusted", filePath);
                        break;
                    case WinVerifyTrustResult.SignatureOrFileCorrupt:
                        Program.LL.LogWarnMessage("_CertSignatureOrFileCorrupt", filePath);
                        break;
                    case WinVerifyTrustResult.SubjectCertExpired:
                        Program.LL.LogWarnMessage("_CertSubjectCertExpired", filePath);
                        break;
                    case WinVerifyTrustResult.SubjectCertificateRevoked:
                        Program.LL.LogWarnMessage("_CertSubjectCertificateRevoked", filePath);
                        break;
                    case WinVerifyTrustResult.UntrustedRoot:
                        Program.LL.LogWarnMessage("_CertUntrustedRoot", filePath);
                        break;
                    case WinVerifyTrustResult.FileNotSigned:
                        result = VerifyByCatalog(filePath);
                        break;
                    default:
                        break;
                        
                }
                return result;
            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_ErrorVerifySignature", ex);
                return WinVerifyTrustResult.Error;
            }
        }

        public static WinVerifyTrustResult VerifyByCatalog(string fileName)
        {
            WinVerifyTrustResult result;
            try
            {
                Guid guidAction = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2);
                Guid driverAction = new Guid(DRIVER_ACTION_VERIFY);
                IntPtr hCatAdmin = IntPtr.Zero;

                IntPtr hFile = CreateFile(
                    fileName,
                    FILE_READ_ATTRIBUTES | FILE_READ_DATA | STANDARD_RIGHTS_READ,
                    FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
                    IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
                if (hFile == IntPtr.Zero)
                {
                    return WinVerifyTrustResult.Error;
                }

                if (IsWindows8OrGreater())
                {
                    CryptCATAdminAcquireContext2(out hCatAdmin, driverAction, BCRYPT_SHA256_ALGORITHM, IntPtr.Zero, 0);
                }
                if (hCatAdmin == IntPtr.Zero)
                {
                    if (!CryptCATAdminAcquireContext(out hCatAdmin, driverAction, 0))
                    {
                        CloseHandle(hFile);
                        return WinVerifyTrustResult.FileNotSigned;
                    }
                }

                byte[] hash = new byte[1];
                uint hashLength = 0;
                bool bRet = false;

                if (IsWindows8OrGreater())
                {
                    bRet = CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, hash, 0);

                    if (Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                    {
                        hash = new byte[hashLength];
                        bRet = CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, hash, 0);
                    }
                }
                if (!bRet || hashLength == 0)
                {
                    if (!CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, hash, 0))
                    {
                        hash = new byte[hashLength];
                        if (!CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, hash, 0))
                        {
                            CloseHandle(hFile);
                            return WinVerifyTrustResult.FileNotSigned;
                        }
                    }
                }

                StringBuilder memberTag = new StringBuilder((int)hashLength * 2);

                for (int i = 0; i < hashLength; i++)
                    memberTag.Append(hash[i].ToString("X2"));

                IntPtr catInfo = CryptCATAdminEnumCatalogFromHash(hCatAdmin, hash, hashLength, 0, IntPtr.Zero);

                if (catInfo == IntPtr.Zero)
                {
                    CryptCATAdminReleaseContext(hCatAdmin, 0);
                    CloseHandle(hFile);
                    return WinVerifyTrustResult.FileNotSigned;
                }

                CATALOG_INFO ci = new CATALOG_INFO();
                ci.cbStruct = (UInt32)Marshal.SizeOf(typeof(CATALOG_INFO));

                if (!CryptCATCatalogInfoFromContext(catInfo, ref ci, 0))
                {
                    CryptCATAdminReleaseCatalogContext(hCatAdmin, catInfo, 0);
                    CryptCATAdminReleaseContext(hCatAdmin, 0);
                    CloseHandle(hFile);
                    return WinVerifyTrustResult.FileNotSigned;
                }

                WINTRUST_CATALOG_INFO wci = new WINTRUST_CATALOG_INFO();
                wci.CatalogFilePath = ci.wszCatalogFile;
                wci.MemberFilePath = fileName;
                wci.hMemberFile = hFile; // you can either pass the file path or already opened file handle
                wci.hCatAdmin = hCatAdmin;
                // see: https://www.appsloveworld.com/cplus/100/22/how-do-i-marshal-a-struct-that-contains-a-variable-sized-array-to-c
                GCHandle pin = GCHandle.Alloc(hash, GCHandleType.Pinned);
                IntPtr pHash = pin.AddrOfPinnedObject();
                wci.pbCalculatedFileHash = pHash;
                wci.cbCalculatedFileHash = hashLength;
                wci.MemberTag = memberTag.ToString(); // you can either use pre-calculated file hash or MemberTag

                WinTrustData trustData = new WinTrustData(wci);

                try
                {
                    result = WinVerifyTrust(IntPtr.Zero, guidAction, trustData);
                    trustData.StateAction = WinTrustDataStateAction.Close;
                }
                finally
                {
                    CryptCATAdminReleaseCatalogContext(hCatAdmin, catInfo, 0);

                    trustData.StateAction = WinTrustDataStateAction.Close;

                    CryptCATAdminReleaseContext(hCatAdmin, 0);
                    CloseHandle(hFile);

                    pin.Free();
                }
                return result;
            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_ErrorVerifySignature", ex);
                return WinVerifyTrustResult.Error;
            }
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
