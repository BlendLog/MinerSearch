//Thanx for https://www.pinvoke.net/default.aspx/wintrust.winverifytrust
// credit to Alex Dragokas 

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MinerSearch
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

    [StructLayout(LayoutKind.Sequential)]
    public class WinTrustCatalogInfo
    {
        public UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof(WinTrustCatalogInfo));
        public UInt32 CatalogVersion = 0;
        public string CatalogFilePath;
        public string MemberTag;
        public string MemberFilePath;
        public IntPtr hMemberFile;
        public IntPtr pbCalculatedFileHash;
        public UInt32 cbCalculatedFileHash;
        public IntPtr CatalogContext;
        public IntPtr hCatAdmin;
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
        public IntPtr FileInfoPtr;
        public WinTrustDataStateAction StateAction = WinTrustDataStateAction.Ignore;
        public IntPtr StateData = IntPtr.Zero;
        public String URLReference = null;
        public WinTrustDataProvFlags ProvFlags = WinTrustDataProvFlags.RevocationCheckNone;
        public WinTrustDataUIContext UIContext = WinTrustDataUIContext.Execute;

        private void InitFlags()
        {
            // On Win7SP1+, don't allow MD2 or MD4 signatures
            if ((Environment.OSVersion.Version.Major > 6) ||
                ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor > 1)) ||
                ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1) && !String.IsNullOrEmpty(Environment.OSVersion.ServicePack)))
            {
                ProvFlags |= WinTrustDataProvFlags.DisableMD2andMD4;
            }
        }

        public WinTrustData(WinTrustFileInfo _fileInfo)
        {
            InitFlags();
            WinTrustFileInfo wtfiData = _fileInfo;
            FileInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WinTrustFileInfo)));
            Marshal.StructureToPtr(wtfiData, FileInfoPtr, false);
        }

        public void Dispose()
        {
            if (FileInfoPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(FileInfoPtr);
                FileInfoPtr = IntPtr.Zero;

                if (UnionChoice == WinTrustDataChoice.File)
                {
                    WinTrustFileInfo fileInfo = (WinTrustFileInfo)Marshal.PtrToStructure(FileInfoPtr, typeof(WinTrustFileInfo));
                    if (fileInfo != null)
                    {
                        fileInfo.Dispose();
                    }
                }
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

    public static class WinTrust
    {
        public const int MAX_PATH = 260;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private const string WINTRUST_ACTION_GENERIC_VERIFY_V2 = "{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}"; // GUID of the action to perform
        private const string DRIVER_ACTION_VERIFY = "{F750E6C3-38EE-11D1-85E5-00C04FC295EE}"; // GUID of the action to perform

        [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
        static extern WinVerifyTrustResult WinVerifyTrust(
            [In] IntPtr hwnd,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID,
            [In] WinTrustData pWVTData
        );

        public static WinVerifyTrustResult VerifyEmbeddedSignature(string filePath)
        {
            try
            {
                WinTrustFileInfo trustFileInfo = new WinTrustFileInfo(filePath);
                WinTrustData wtd = new WinTrustData(trustFileInfo);
                Guid guidAction = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2);
                WinVerifyTrustResult result = WinVerifyTrust(INVALID_HANDLE_VALUE, guidAction, wtd);
                wtd.StateAction = WinTrustDataStateAction.Close;
                WinVerifyTrust(IntPtr.Zero, guidAction, wtd);
                trustFileInfo.Dispose();
                wtd.Dispose();

                string fileName = Path.GetFileName(filePath);

                switch (result)
                {
                    case WinVerifyTrustResult.ProviderUnknown:
                        Logger.WriteLog($"\t[!] {fileName}: Trust provider is not recognized on this system", Logger.warn);
                        break;
                    case WinVerifyTrustResult.ActionUnknown:
                        Logger.WriteLog($"\t[!] {fileName}: Trust provider does not support the specified action", Logger.warn);
                        break;
                    case WinVerifyTrustResult.SubjectFormUnknown:
                        Logger.WriteLog($"\t[!] {fileName}: Trust provider does not support the form specified for the subject", Logger.warn);
                        break;
                    case WinVerifyTrustResult.SubjectNotTrusted:
                        Logger.WriteLog($"\t[!] {fileName}: Subject failed the specified verification action", Logger.warn);
                        break;
                    case WinVerifyTrustResult.SubjectExplicitlyDistrusted:
                        Logger.WriteLog($"\t[!] {fileName}: Signer's certificate is in the Untrusted Publishers store", Logger.warn);
                        break;
                    case WinVerifyTrustResult.SignatureOrFileCorrupt:
                        Logger.WriteLog($"\t[!] {fileName}: file was probably corrupt", Logger.warn);
                        break;
                    case WinVerifyTrustResult.SubjectCertExpired:
                        Logger.WriteLog($"\t[!] {fileName}: Signer's certificate was expired", Logger.warn);
                        break;
                    case WinVerifyTrustResult.SubjectCertificateRevoked:
                        Logger.WriteLog($"\t[!] {fileName}: Subject's certificate was revoked", Logger.warn);
                        break;
                    case WinVerifyTrustResult.UntrustedRoot:
                        Logger.WriteLog($"\t[!] {fileName}: Root certificate is not trusted", Logger.warn);
                        break;
                    case WinVerifyTrustResult.Success:
                        break;
                    case WinVerifyTrustResult.FileNotSigned:
                        string fileStatus = SilDev.FileEx.GetSignatureStatus(filePath);
                        if (fileStatus == "NotSigned")
                        {
                            Logger.WriteLog($"\t[!] {fileName}: File is not signed", Logger.warn);
                            return WinVerifyTrustResult.FileNotSigned;
                        }
                        else if (fileStatus == "Valid")
                        {
                            return WinVerifyTrustResult.Success;
                        }
                        else Logger.WriteLog($"\t[!] {fileName}: {fileStatus}", Logger.warn);
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error verify signature: {ex.Message}", Logger.error);
                return WinVerifyTrustResult.Error;
            }
        }

    }
}
