//Thanx for https://www.pinvoke.net/default.aspx/wintrust.winverifytrust
// credit to Alex Dragokas https://github.com/dragokas/Verify-Signature-Cpp/blob/master/verify.cpp

using MSearch.Core;
using System;
using System.Runtime.InteropServices;

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
        Catalog = 2
    }

    enum WinTrustDataStateAction : uint
    {
        Ignore = 0x00000000,
        Verify = 0x00000001,
        Close = 0x00000002
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
        SaferFlag = 0x00000100,
        HashOnlyFlag = 0x00000200,
        UseDefaultOsverCheck = 0x00000400,
        LifetimeSigningFlag = 0x00000800,
        CacheOnlyUrlRetrieval = 0x00001000,
        DisableMD2andMD4 = 0x00002000,
    }


    enum WinTrustDataUIContext : uint
    {
        Execute = 0,
        Install = 1
    }
    #endregion

    #region WinVerifyTrustResult
    public enum WinVerifyTrustResult : uint
    {
        Error = 0xffffffff,
        Success = 0,
        ProviderUnknown = 0x800b0001,
        ActionUnknown = 0x800b0002,
        SubjectFormUnknown = 0x800b0003,
        SubjectNotTrusted = 0x800b0004,
        DIGSIG_ENCODE = 0x800b0005,
        DIGSIG_DECODE = 0x800b0006,
        FileNotSigned = 0x800B0100,
        SubjectExplicitlyDistrusted = 0x800B0111,
        SignatureOrFileCorrupt = 0x80096010,
        SubjectCertExpired = 0x800B0101,
        SubjectCertificateRevoked = 0x800B010C,
        UntrustedRoot = 0x800B0109,
        Unknown = 0x8000000D,
        CryptFileError = 0x80092003,
        CertIsNotValidForUsage = 0x800b0107
    }
    #endregion

    #region WinTrust structures
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    class WinTrustFileInfo : IDisposable
    {
        uint StructSize = (uint)Marshal.SizeOf(typeof(WinTrustFileInfo));
        IntPtr pszFilePath;
        IntPtr hFile = IntPtr.Zero;
        IntPtr pgKnownSubject = IntPtr.Zero;

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
        public IntPtr pbCalculatedFileHash;
        public UInt32 cbCalculatedFileHash;
        public IntPtr CatalogContext;
        public IntPtr hCatAdmin;

        private GCHandle? _hashHandle = null;

        public void SetHash(byte[] hash)
        {
            if (_hashHandle.HasValue)
            {
                _hashHandle.Value.Free();
                _hashHandle = null;
            }

            if (hash != null && hash.Length > 0)
            {
                _hashHandle = GCHandle.Alloc(hash, GCHandleType.Pinned);
                pbCalculatedFileHash = _hashHandle.Value.AddrOfPinnedObject();
                cbCalculatedFileHash = (uint)hash.Length;
            }
            else
            {
                pbCalculatedFileHash = IntPtr.Zero;
                cbCalculatedFileHash = 0;
            }
        }

        public void Dispose()
        {
            if (_hashHandle.HasValue)
            {
                _hashHandle.Value.Free();
                _hashHandle = null;
                pbCalculatedFileHash = IntPtr.Zero;
                cbCalculatedFileHash = 0;
            }
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
        public WinTrustDataChoice UnionChoice = WinTrustDataChoice.File;
        public IntPtr UnionInfoPtr;
        public WinTrustDataStateAction StateAction = WinTrustDataStateAction.Ignore;
        public IntPtr StateData = IntPtr.Zero;
        public string URLReference = null;
        public WinTrustDataProvFlags ProvFlags = WinTrustDataProvFlags.RevocationCheckNone | WinTrustDataProvFlags.CacheOnlyUrlRetrieval;
        public WinTrustDataUIContext UIContext = WinTrustDataUIContext.Execute;

        private object _unionInfo = null;

        public WinTrustData(WinTrustFileInfo fileInfo)
        {
            UnionChoice = WinTrustDataChoice.File;
            _unionInfo = fileInfo;
            UnionInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WinTrustFileInfo)));
            Marshal.StructureToPtr(fileInfo, UnionInfoPtr, false);
        }

        public WinTrustData(WINTRUST_CATALOG_INFO catalogInfo)
        {
            UnionChoice = WinTrustDataChoice.Catalog;
            _unionInfo = catalogInfo;
            UnionInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINTRUST_CATALOG_INFO)));
            Marshal.StructureToPtr(catalogInfo, UnionInfoPtr, false);
        }

        public void Dispose()
        {
            if (_unionInfo is IDisposable disposableUnionInfo)
            {
                disposableUnionInfo.Dispose();
            }
            _unionInfo = null;

            if (UnionInfoPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(UnionInfoPtr);
                UnionInfoPtr = IntPtr.Zero;
            }

            StateData = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~WinTrustData()
        {
            Dispose();
        }
    }
    #endregion

    public class WinTrust
    {
        public const int MAX_PATH = 260;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private static readonly Guid WINTRUST_ACTION_GENERIC_VERIFY_V2 = new Guid("00AAC56B-CD44-11d0-8CC2-00C04FC295EE");
        private static readonly Guid DRIVER_ACTION_VERIFY = new Guid("F750E6C3-38EE-11D1-85E5-00C04FC295EE");

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CATALOG_INFO
        {
            public UInt32 cbStruct;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string wszCatalogFile;
        }

        #region Native
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminReleaseCatalogContext(IntPtr hCatAdmin, IntPtr hCatInfo, int dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminReleaseContext(IntPtr hCatAdmin, int dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminCalcHashFromFileHandle(IntPtr hFile, ref uint hashLength, [Out] byte[] pbHash, uint dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminCalcHashFromFileHandle2(IntPtr hCatAdmin, IntPtr hFile, ref uint hashLength, [Out] byte[] pbHash, uint dwFlags);

        [DllImport("Wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CryptCATAdminEnumCatalogFromHash(IntPtr hCatAdmin, [In] byte[] pbHash, uint cbHash, uint dwFlags, IntPtr phPrevCatInfo);

        [DllImport("wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminAcquireContext(out IntPtr phCatAdmin, [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID, int dwFlags);

        [DllImport("wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATAdminAcquireContext2(out IntPtr phCatAdmin, [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID, [In][MarshalAs(UnmanagedType.LPWStr)] string pwszHashAlgorithm, IntPtr pStrongHashPolicy, int dwFlags);

        [DllImport("wintrust.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CryptCATCatalogInfoFromContext(IntPtr hCatalog, ref CATALOG_INFO psCatInfo, int dwFlags);

        [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
        static extern WinVerifyTrustResult WinVerifyTrust(IntPtr hwnd, [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID, WinTrustData pWVTData);
        #endregion

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

        public WinVerifyTrustResult VerifyEmbeddedSignature(string filePath, bool showUnsigned = false)
        {
            try
            {
                using (var trustFileInfo = new WinTrustFileInfo(filePath))
                using (var wtd = new WinTrustData(trustFileInfo))
                {
                    wtd.StateAction = WinTrustDataStateAction.Verify;
                    var result = WinVerifyTrust(INVALID_HANDLE_VALUE, WINTRUST_ACTION_GENERIC_VERIFY_V2, wtd);
                    wtd.StateAction = WinTrustDataStateAction.Close;

                    if (result == WinVerifyTrustResult.FileNotSigned)
                        result = VerifyByCatalog(filePath);


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

                    if (result == WinVerifyTrustResult.FileNotSigned && (showUnsigned || AppConfig.Instance.verbose))
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_CertFileNotSigned", filePath);
                        Logger.WriteLog($"\t\t[SHA1: {FileChecker.CalculateSHA1(filePath)}]", ConsoleColor.White, false);
                    }



                    return result;
                }
            }
            catch
            {
                return WinVerifyTrustResult.Error;
            }
        }

        public static WinVerifyTrustResult VerifyByCatalog(string fileName)
        {
            IntPtr hCatAdmin = IntPtr.Zero;
            IntPtr hFile = IntPtr.Zero;
            IntPtr catInfo = IntPtr.Zero;

            try
            {
                hFile = CreateFile(fileName, 0x20000 | 0x80 | 1, 1 | 2 | 4, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
                if (hFile == INVALID_HANDLE_VALUE) return WinVerifyTrustResult.Error;

                bool isWin7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;
                bool bRet = false;
                uint hashLength = 0;

                if (isWin7)
                {
                    // Win7: SHA1
                    if (!CryptCATAdminAcquireContext(out hCatAdmin, DRIVER_ACTION_VERIFY, 0))
                        return WinVerifyTrustResult.FileNotSigned;

                    bRet = CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, null, 0);
                }
                else
                {
                    // Win8+: SHA256
                    if (!CryptCATAdminAcquireContext2(out hCatAdmin, DRIVER_ACTION_VERIFY, "SHA256", IntPtr.Zero, 0))
                    {
                        if (!CryptCATAdminAcquireContext(out hCatAdmin, DRIVER_ACTION_VERIFY, 0))
                            return WinVerifyTrustResult.FileNotSigned;

                        bRet = CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, null, 0);
                    }
                    else
                    {
                        bRet = CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, null, 0);
                    }
                }

                if (!bRet || hashLength == 0)
                    return WinVerifyTrustResult.FileNotSigned;

                byte[] hash = new byte[hashLength];

                if (isWin7 || (hCatAdmin != IntPtr.Zero && hashLength > 0 && !CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, hash, 0)))
                {
                    // Win7 SHA1
                    bRet = CryptCATAdminCalcHashFromFileHandle(hFile, ref hashLength, hash, 0);
                }
                else
                {
                    // Win8+: SHA256
                    bRet = CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, hFile, ref hashLength, hash, 0);
                }

                if (!bRet)
                    return WinVerifyTrustResult.FileNotSigned;

                catInfo = CryptCATAdminEnumCatalogFromHash(hCatAdmin, hash, hashLength, 0, IntPtr.Zero);
                if (catInfo == IntPtr.Zero)
                    return WinVerifyTrustResult.FileNotSigned;

                CATALOG_INFO ci = new CATALOG_INFO { cbStruct = (uint)Marshal.SizeOf(typeof(CATALOG_INFO)) };
                if (!CryptCATCatalogInfoFromContext(catInfo, ref ci, 0))
                    return WinVerifyTrustResult.FileNotSigned;

                using (var wci = new WINTRUST_CATALOG_INFO { CatalogFilePath = ci.wszCatalogFile, MemberFilePath = fileName, hMemberFile = hFile, hCatAdmin = hCatAdmin })
                {
                    wci.SetHash(hash);
                    using (var trustData = new WinTrustData(wci))
                    {
                        trustData.StateAction = WinTrustDataStateAction.Verify;
                        var result = WinVerifyTrust(IntPtr.Zero, DRIVER_ACTION_VERIFY, trustData);
                        trustData.StateAction = WinTrustDataStateAction.Close;
                        return result;
                    }
                }
            }
            catch
            {
                return WinVerifyTrustResult.Error;
            }
            finally
            {
                if (catInfo != IntPtr.Zero) CryptCATAdminReleaseCatalogContext(hCatAdmin, catInfo, 0);
                if (hCatAdmin != IntPtr.Zero) CryptCATAdminReleaseContext(hCatAdmin, 0);
                if (hFile != IntPtr.Zero && hFile != INVALID_HANDLE_VALUE) CloseHandle(hFile);
            }
        }
    }
}

