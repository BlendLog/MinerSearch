
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

namespace MSearch
{
    internal class Native
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(IntPtr tokenHandle, bool disableAllPrivileges, ref TOKEN_PRIVILEGES newState, uint bufferLength, IntPtr previousState, IntPtr returnLength);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern uint SuspendThread(IntPtr hThread);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("ntdll.dll")]
        public static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern bool NtTerminateProcess(IntPtr hProcess, uint errorStatus);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int access, bool inheritHandle, int processId);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll")]
        public static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        public static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TcpTableClass tblClass, int reserved);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        internal static extern uint GetClassLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        internal static extern IntPtr GetClassLong64(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern int NetUserEnum(string servername, int level, int filter, out IntPtr bufptr, int prefmaxlen, out int entriesread, out int totalentries, ref int resume_handle);

        [DllImport("Netapi32.dll", SetLastError = true)]
        internal static extern int NetApiBufferFree(IntPtr Buffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        internal const int SW_HIDE = 0;
        internal const int SW_SHOW = 5;
        internal const int SW_MINIMIZE = 6;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USER_INFO_0
        {
            public string Username;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint GetFileAttributes(string lpFileName);

        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumProcesses([Out] uint[] processIds, int size, [Out] out int bytesReturned);

        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb, [Out] out uint lpcbNeeded, uint dwFilterFlag);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetFileInformationByHandleEx(IntPtr hFile, FILE_INFO_BY_HANDLE_CLASS FileInformationClass, out FILE_BASIC_INFO lpFileInformation, uint dwBufferSize);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [Flags]
        internal enum FILE_ATTRIBUTE
        {
            READONLY = 0x00000001, // Файл только для чтения
            HIDDEN = 0x00000002, // Файл скрыт
            SYSTEM = 0x00000004, // Системный файл
            DIRECTORY = 0x00000010, // Указывает на каталог
            ARCHIVE = 0x00000020, // Файл, требующий архивации
            DEVICE = 0x00000040, // Зарезервировано для системы, не используется
            NORMAL = 0x00000080, // Обычный файл, без других атрибутов
            TEMPORARY = 0x00000100, // Временный файл
            SPARSE_FILE = 0x00000200, // Разреженный файл
            REPARSE_POINT = 0x00000400, // Файл с точкой повторного анализа
            COMPRESSED = 0x00000800, // Сжатый файл
            OFFLINE = 0x00001000, // Файл недоступен, данные только в облаке (например, для OneDrive)
            NOT_CONTENT_INDEXED = 0x00002000, // Файл не индексируется службой поиска
            ENCRYPTED = 0x00004000, // Зашифрованный файл или каталог
            INTEGRITY_STREAM = 0x00008000, // Файл или каталог с потоком целостности
            VIRTUAL = 0x00010000, // Зарезервировано для будущего использования
            NO_SCRUB_DATA = 0x00020000, // Данные файла не подлежат очистке
            RECALL_ON_OPEN = 0x00040000, // Обратный вызов при открытии
            PINNED = 0x00080000, // Файл закреплен для хранения в кэше (OneDrive)
            UNPINNED = 0x00100000, // Файл откреплен, может быть удален из кэша (OneDrive)
            RECALL_ON_DATA_ACCESS = 0x00400000 // Обратный вызов при доступе к данным файла
        }


        internal enum FILE_INFO_BY_HANDLE_CLASS
        {
            FileBasicInfo = 0,
            FileStandardInfo = 1,
            FileNameInfo = 2,
            FileRemoteProtocolInfo = 3,
            FileCompressionInfo = 4,
            FileAttributeTagInfo = 5,
            FileIdBothDirectoryInfo = 6,
            FileIdBothDirectoryRestartInfo = 7,
            FileIoPriorityHintInfo = 8,
            FileRemoteInfo = 9,
            FileFullDirectoryInfo = 10,
            FileFullDirectoryRestartInfo = 11,
            FileStorageInfo = 12,
            FileAlignmentInfo = 13,
            FileIdInfo = 14,
            FileIdExtdDirectoryInfo = 15,
            FileIdExtdDirectoryRestartInfo = 16,
            MaximumFileInfoByHandlesClass
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FILE_BASIC_INFO
        {
            public long CreationTime;
            public long LastAccessTime;
            public long LastWriteTime;
            public long ChangeTime;
            public uint FileAttributes;
        }

        [DllImport("ntdll.dll")]
        internal static extern int NtCreateThreadEx(
            out IntPtr threadHandle,
            uint desiredAccess,
            IntPtr objectAttributes,
            IntPtr processHandle,
            IntPtr startAddress,
            IntPtr parameter,
            bool createSuspended,
            uint stackZeroBits,
            uint sizeOfStack,
            uint maximumStackSize,
            IntPtr attributeList);

        public enum TcpTableClass
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public uint state;
            public uint localAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] localPort;
            public uint remoteAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] remotePort;
            public int owningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_BASIC_INFORMATION
        {
            public IntPtr Reserved1;
            public IntPtr PebBaseAddress;
            public IntPtr Reserved2_0;
            public IntPtr Reserved2_1;
            public IntPtr UniqueProcessId;
            public IntPtr InheritedFromUniqueProcessId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }
        public static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");

        public const int PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
        public const int PROCESS_VM_READ = 0x0010;
        public const int OffsetProcessParametersx32 = 0x10;
        public const int OffsetCommandLinex32 = 0x40;
        public const uint TH32CS_SNAPPROCESS = 0x00000002;
        public const int STATUS_SUCCESS = 0;

        public const uint TOKEN_QUERY = 0x0008;
        public const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        public static string SE_SECURITY_NAME = "SeSe~cur~ity~Pr~ivi~leg~e".Replace("~", "");
        public static string SE_TAKE_OWNERSHIP_NAME = "S~eTa~keOw~ner~ship~Privi~leg~e".Replace("~", "");
        public const int SM_CLEANBOOT = 67;

        public const int SC_MANAGER_CONNECT = 0x0001;
        public const int SC_MANAGER_CREATE_SERVICE = 0x0002;
        public const int SERVICE_QUERY_CONFIG = 0x0001;
        public const int SERVICE_CHANGE_CONFIG = 0x0002;
        public const int SERVICE_START = 0x0010;
        public const int SERVICE_AUTO_START = 0x0002;
        public const int SERVICE_NO_CHANGE = -1;
        public const int SERVICE_QUERY_STATUS = 0x0004;
        public const int SERVICE_STOP = 0x0020;
        public const int SERVICE_STOPPED = 0x00000001;
        public const int SERVICE_RUNNING = 0x00000004;
        public const int SERVICE_DISABLED = 0x00000004;

        public const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        public const int STD_INPUT_HANDLE = -10;

        public const long PROCESS_CREATION_MITIGATION_POLICY_BLOCK_NON_MICROSOFT_BINARIES_ALWAYS_ON = 0x100000000000;
        public const int PROC_THREAD_ATTRIBUTE_MITIGATION_POLICY = 0x00020007;
        public const uint EXTENDED_STARTUPINFO_PRESENT = 0x00080000;
        public const uint CREATE_NEW_CONSOLE = 0x00000010;

        public static uint WM_GETICON = 0x007f;
        public static uint WM_SETICON = 0x80;
        public static IntPtr ICON_SMALL2 = new IntPtr(2);
        public static IntPtr IDI_APPLICATION = new IntPtr(0x7F00);
        public static int GCL_HICON = -14;


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public uint Attributes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TOKEN_PRIVILEGES
        {
            public uint PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        public enum BuildNumber : uint
        {
            Windows_7 = 7601,
            Windows_8 = 9200,
            Windows_8_1 = 9600,
            Windows_10_1507 = 10240,
            Windows_10_1511 = 10586,
            Windows_10_1607 = 14393,
            Windows_10_1703 = 15063,
            Windows_10_1709 = 16299,
            Windows_10_1803 = 17134,
            Windows_10_1809 = 17763,
            Windows_10_1903 = 18362,
            Windows_10_1909 = 18363,
            Windows_10_2004 = 19041,
            Windows_10_20H2 = 19042,
            Windows_10_21H1 = 19043,
            Windows_10_21H2 = 19044,
            Windows_10_22H2 = 19045,
            Windows_11_21H2 = 22000,
            Windows_11_22H2 = 22621,
            Windows_11_23H2 = 22631,
            Windows_11_24H2 = 26100,
        };

        internal static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return new IntPtr((long)GetClassLong32(hWnd, nIndex));
            else
                return GetClassLong64(hWnd, nIndex);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }

        public const uint R77_SIGNATURE = 0x7277;
        public const uint R77_SERVICE_SIGNATURE = 0x7273;
        public const uint R77_HELPER_SIGNATURE = 0x7268;
        public const int MaxProcesses = 10000;
        public const int MaxModules = 10000;

        [StructLayout(LayoutKind.Sequential)]
        internal struct R77_PROCESS
        {
            public int Signature;
            public uint ProcessId;
            public ulong DetachAddress;
        }
    }

    // https://github.com/DavidXanatos/priv10/blob/master/MiscHelpers/API/ServiceHelper.cs
    public static class ServiceHelper
    {
        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
        private const int ERROR_INSUFFICIENT_BUFFER = 0x0000007a;
        private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;
        private const int SC_STATUS_PROCESS_INFO = 0;

        #region OpenSCManager
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr OpenSCManager(string machineName, string databaseName, ScmAccessRights dwDesiredAccess);
        #endregion

        #region OpenService
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceAccessRights dwDesiredAccess);
        #endregion

        #region CreateService
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceAccessRights dwDesiredAccess, int dwServiceType, ServiceBootFlag dwStartType, ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);
        #endregion

        #region CloseServiceHandle
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseServiceHandle(IntPtr hSCObject);
        #endregion

        #region QueryServiceConfig
        [StructLayout(LayoutKind.Sequential)]
        public class ServiceConfigInfo
        {
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 ServiceType;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 StartType;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 ErrorControl;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String BinaryPathName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String LoadOrderGroup;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 TagID;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String Dependencies;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String ServiceStartName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String DisplayName;
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int QueryServiceConfig(IntPtr service, IntPtr queryServiceConfig, int bufferSize, ref int bytesNeeded);
        #endregion


        #region ChangeServiceConfig
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool ChangeServiceConfig(IntPtr hService, UInt32 dwServiceType, ServiceBootFlag dwStartType, UInt32 dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword, string lpDisplayName);
        #endregion

        #region QueryServiceStatus
        [StructLayout(LayoutKind.Sequential)]
        public class SERVICE_STATUS
        {
            public int dwServiceType = 0;
            public ServiceState dwCurrentState = 0;
            public int dwControlsAccepted = 0;
            public int dwWin32ExitCode = 0;
            public int dwServiceSpecificExitCode = 0;
            public int dwCheckPoint = 0;
            public int dwWaitHint = 0;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int QueryServiceStatus(IntPtr hService, SERVICE_STATUS lpServiceStatus);
        #endregion

        #region QueryServiceStatusEx
        [StructLayout(LayoutKind.Sequential)]
        public sealed class SERVICE_STATUS_PROCESS
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint dwServiceType;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwCurrentState;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwControlsAccepted;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwWin32ExitCode;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwServiceSpecificExitCode;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwCheckPoint;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwWaitHint;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwProcessId;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwServiceFlags;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool QueryServiceStatusEx(IntPtr hService, int infoLevel, IntPtr lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);
        #endregion

        #region DeleteService
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteService(IntPtr hService);
        #endregion

        #region ControlService
        [DllImport("advapi32.dll")]
        private static extern int ControlService(IntPtr hService, ServiceControl dwControl, SERVICE_STATUS lpServiceStatus);
        #endregion

        #region StartService
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);
        #endregion

        public static bool ServiceIsInstalled(string serviceName)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

            try
            {
                IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus);

                if (service == IntPtr.Zero)
                    return false;

                CloseServiceHandle(service);
                return true;
            }
            finally
            {
                CloseServiceHandle(scm);
            }
        }

        public static void ChangeStartMode(string serviceName, ServiceBootFlag mode)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.Connect | ScmAccessRights.EnumerateService);

            try
            {
                IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryConfig | ServiceAccessRights.ChangeConfig);
                if (service == IntPtr.Zero)
                    throw new ApplicationException("Could not open service.");

                try
                {
                    if (!ChangeServiceConfig(service, SERVICE_NO_CHANGE, mode, SERVICE_NO_CHANGE, null, null, IntPtr.Zero, null, null, null, null))
                        throw new ApplicationException("Could not configure service " + Marshal.GetLastWin32Error());
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scm);
            }
        }

        public static ServiceConfigInfo GetServiceInfo(string serviceName)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.Connect | ScmAccessRights.EnumerateService);

            try
            {
                IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryConfig | ServiceAccessRights.ChangeConfig);
                if (service == IntPtr.Zero)
                    throw new ApplicationException("Could not open service.");

                try
                {
                    int bytesNeeded = 0;
                    if (QueryServiceConfig(service, IntPtr.Zero, 0, ref bytesNeeded) == 0 && bytesNeeded == 0)
                        throw new ApplicationException("Could not query service configuration" + Marshal.GetLastWin32Error());

                    IntPtr qscPtr = Marshal.AllocCoTaskMem(bytesNeeded);
                    try
                    {
                        if (QueryServiceConfig(service, qscPtr, bytesNeeded, ref bytesNeeded) == 0)
                            throw new ApplicationException("Could not query service configuration" + Marshal.GetLastWin32Error());

                        return (ServiceConfigInfo)Marshal.PtrToStructure(qscPtr, typeof(ServiceConfigInfo));
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(qscPtr);
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scm);
            }
        }

        public static ServiceConfigInfo GetServiceInfoSafe(string serviceName)
        {
            try
            {
                return GetServiceInfo(serviceName);
            }
            catch
            {
                return null;
            }
        }

        public static bool StartService(string serviceName)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

            bool ret = false;
            try
            {
                IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus | ServiceAccessRights.Start);
                if (service == IntPtr.Zero)
                    throw new ApplicationException("Could not open service.");

                try
                {
                    StartService(service);
                    ret = true;
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scm);
            }
            return ret;
        }

        public static bool StopService(string serviceName)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

            bool ret = false;
            try
            {
                IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus | ServiceAccessRights.Stop);
                if (service == IntPtr.Zero)
                    throw new ApplicationException("Could not open service.");

                try
                {
                    StopService(service);
                    ret = true;
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scm);
            }
            return ret;
        }

        private static void StartService(IntPtr service)
        {

            StartService(service, 0, 0);

            var changedStatus = WaitForServiceStatus(service, ServiceState.StartPending, ServiceState.Running);
            if (!changedStatus)
                throw new ApplicationException("Unable to start service");
        }

        private static void StopService(IntPtr service)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            ControlService(service, ServiceControl.Stop, status);
            var changedStatus = WaitForServiceStatus(service, ServiceState.StopPending, ServiceState.Stopped);
            if (!changedStatus)
                throw new ApplicationException("Unable to stop service");
        }

        public static void Uninstall(string serviceName)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.AllAccess);

            try
            {
                IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.AllAccess);
                if (service == IntPtr.Zero)
                    throw new ApplicationException("Service not installed.");

                try
                {
                    StopService(service);

                    if (!DeleteService(service))
                        throw new ApplicationException("Could not delete service " + Marshal.GetLastWin32Error());
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scm);
            }
        }

        public static ServiceState GetServiceState(string serviceName)
        {
            SERVICE_STATUS_PROCESS ssp = GetServiceStatus(serviceName);
            if (ssp == null)
                return ServiceState.NotFound;
            return (ServiceState)ssp.dwCurrentState;
        }

        public static SERVICE_STATUS_PROCESS GetServiceStatus(string serviceName)
        {
            IntPtr scm = OpenSCManager(ScmAccessRights.Connect);
            IntPtr zero = IntPtr.Zero;
            IntPtr service = IntPtr.Zero;

            try
            {
                service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus);

                UInt32 dwBytesAlloc = 0;
                UInt32 dwBytesNeeded = 36;
                do
                {
                    dwBytesAlloc = dwBytesNeeded;
                    // Allocate required buffer and call again.
                    zero = Marshal.AllocHGlobal((int)dwBytesAlloc);
                    if (QueryServiceStatusEx(service, SC_STATUS_PROCESS_INFO, zero, dwBytesAlloc, out dwBytesNeeded))
                    {
                        var ssp = new SERVICE_STATUS_PROCESS();
                        Marshal.PtrToStructure(zero, ssp);
                        return ssp;
                    }
                    // retry with new size info
                } while (Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER && dwBytesAlloc < dwBytesNeeded);
            }
            finally
            {
                if (zero != IntPtr.Zero)
                    Marshal.FreeHGlobal(zero);
                if (service != IntPtr.Zero)
                    CloseServiceHandle(service);
                CloseServiceHandle(scm);
            }
            return null;
        }

        private static bool WaitForServiceStatus(IntPtr service, ServiceState waitStatus, ServiceState desiredStatus)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();

            QueryServiceStatus(service, status);
            if (status.dwCurrentState == desiredStatus) return true;

            int dwStartTickCount = Environment.TickCount;
            int dwOldCheckPoint = status.dwCheckPoint;

            while (status.dwCurrentState == waitStatus)
            {
                // Do not wait longer than the wait hint. A good interval is
                // one tenth the wait hint, but no less than 1 second and no
                // more than 10 seconds.

                int dwWaitTime = status.dwWaitHint / 10;

                if (dwWaitTime < 1000) dwWaitTime = 1000;
                else if (dwWaitTime > 10000) dwWaitTime = 10000;

                Thread.Sleep(dwWaitTime);

                // Check the status again.

                if (QueryServiceStatus(service, status) == 0) break;

                if (status.dwCheckPoint > dwOldCheckPoint)
                {
                    // The service is making progress.
                    dwStartTickCount = Environment.TickCount;
                    dwOldCheckPoint = status.dwCheckPoint;
                }
                else
                {
                    if (Environment.TickCount - dwStartTickCount > status.dwWaitHint)
                    {
                        // No progress made within the wait hint
                        break;
                    }
                }
            }
            return (status.dwCurrentState == desiredStatus);
        }

        private static IntPtr OpenSCManager(ScmAccessRights rights)
        {
            IntPtr scm = OpenSCManager(null, null, rights);
            if (scm == IntPtr.Zero)
                throw new ApplicationException("Could not connect to service control manager.");

            return scm;
        }

        public enum ServiceState
        {
            Unknown = -1, // The state cannot be (has not been) retrieved.
            NotFound = 0, // The service is not known on the host server.
            Stopped = 1,
            StartPending = 2,
            StopPending = 3,
            Running = 4,
            ContinuePending = 5,
            PausePending = 6,
            Paused = 7
        }

        [Flags]
        public enum ScmAccessRights
        {
            Connect = 0x0001,
            CreateService = 0x0002,
            EnumerateService = 0x0004,
            Lock = 0x0008,
            QueryLockStatus = 0x0010,
            ModifyBootConfig = 0x0020,
            StandardRightsRequired = 0xF0000,
            AllAccess = (StandardRightsRequired | Connect | CreateService |
                         EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
        }

        [Flags]
        public enum ServiceAccessRights
        {
            QueryConfig = 0x1,
            ChangeConfig = 0x2,
            QueryStatus = 0x4,
            EnumerateDependants = 0x8,
            Start = 0x10,
            Stop = 0x20,
            PauseContinue = 0x40,
            Interrogate = 0x80,
            UserDefinedControl = 0x100,
            Delete = 0x00010000,
            StandardRightsRequired = 0xF0000,
            AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
                         QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
                         Interrogate | UserDefinedControl)
        }

        public enum ServiceBootFlag
        {
            Start = 0x00000000,
            SystemStart = 0x00000001,
            AutoStart = 0x00000002,
            DemandStart = 0x00000003,
            Disabled = 0x00000004
        }

        public enum ServiceControl
        {
            Stop = 0x00000001,
            Pause = 0x00000002,
            Continue = 0x00000003,
            Interrogate = 0x00000004,
            Shutdown = 0x00000005,
            ParamChange = 0x00000006,
            NetBindAdd = 0x00000007,
            NetBindRemove = 0x00000008,
            NetBindEnable = 0x00000009,
            NetBindDisable = 0x0000000A
        }

        public enum ServiceError
        {
            Ignore = 0x00000000,
            Normal = 0x00000001,
            Severe = 0x00000002,
            Critical = 0x00000003
        }
    }

}
