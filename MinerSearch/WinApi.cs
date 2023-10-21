
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MinerSearch
{
    internal class winapi
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

        [DllImport("Advapi32.dll")]
        public static extern IntPtr OpenSCManager(string machineName, string databaseName, int dwAccess);

        [DllImport("Advapi32.dll")]
        public static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, int dwDesiredAccess);

        [DllImport("Advapi32.dll")]
        public static extern bool QueryServiceConfig(IntPtr hService, IntPtr lpServiceConfig, int cbBufSize, out int pcbBytesNeeded);

        [DllImport("Advapi32.dll")]
        public static extern bool ChangeServiceConfig(IntPtr hService, int nServiceType, int nStartType, int nErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword, string lpDisplayName);

        [DllImport("Advapi32.dll")]
        public static extern bool StartService(IntPtr hService, int dwNumServiceArgs, string[] lpServiceArgVectors);

        [DllImport("Advapi32.dll")]
        public static extern bool CloseServiceHandle(IntPtr hSCObject);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TcpTableClass tblClass, int reserved);

        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_CONFIG
        {
            public uint dwServiceType;
            public uint dwStartType;
            public uint dwErrorControl;
            public uint dwCurrentState;
            public IntPtr lpBinaryPathName;
            public IntPtr lpLoadOrderGroup;
            public uint dwTagId;
            public IntPtr lpDependencies;
            public IntPtr lpServiceStartName;
            public IntPtr lpDisplayName;
        }

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
        public const uint TH32CS_SNAPPROCESS = 0x00000002;
        public const int STATUS_SUCCESS = 0;

        public const uint TOKEN_QUERY = 0x0008;
        public const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        public static string SE_SECURITY_NAME = "SeSe?cur?ity?Pr?ivi?leg?e".Replace("?","");
        public static string SE_TAKE_OWNERSHIP_NAME = "S?eTa?keOw?ner?ship?Privi?leg?e".Replace("?", "");
        public static string SE_DEBUG_PRIVILEGE = "SeDe?bug?P?riv?ile?ge".Replace("?", "");

        public const int SM_CLEANBOOT = 67;

        // Константы для управления службами
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

        public struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public uint Attributes;
        }

        public struct TOKEN_PRIVILEGES
        {
            public uint PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

    }
}
