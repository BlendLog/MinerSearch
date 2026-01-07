using DBase;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using MSearch.Core;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool PrivilegeCheck(IntPtr ClientToken, ref PRIVILEGE_SET RequiredPrivileges, out bool pfResult);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern uint SuspendThread(IntPtr hThread);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("ntdll.dll")]
        internal static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

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

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int NetUserDel(string serverName, string userName);

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

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetFileAttributesEx(string lpFileName, GET_FILEEX_INFO_LEVELS fInfoLevelId, out WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern SafeFileHandle CreateFile(string lpFileName, [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess, [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode, IntPtr lpSecurityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, byte[] lpOutBuffer, uint nOutBufferSize, out uint lpBytesReturned, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetFileAttributes(string lpFileName, [MarshalAs(UnmanagedType.U4)] FileAttributes dwFileAttributes);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WIN32_FILE_ATTRIBUTE_DATA
        {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
        }

        internal enum GET_FILEEX_INFO_LEVELS
        {
            GetFileExInfoStandard,
            GetFileExMaxInfoLevel
        }

        internal const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        public const uint FSCTL_GET_REPARSE_POINT = 0x000900A8;
        public const uint IO_REPARSE_TAG_SYMLINK = 0xA000000C;
        public const uint IO_REPARSE_TAG_MOUNT_POINT = 0xA0000003;
        public const uint IO_REPARSE_TAG_APPEXECLINK = 0x8000001B;

        public const int MAXIMUM_REPARSE_DATA_BUFFER_SIZE = 16 * 1024;


        internal const uint GENERIC_READ = 0x80000000;
        internal const uint FILE_SHARE_READ = 0x00000001;
        internal const uint FILE_SHARE_WRITE = 0x00000002;
        internal const uint FILE_SHARE_DELETE = 0x00000004;

        internal const uint OPEN_EXISTING = 3;

        internal const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;
        internal const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteFile(string lpFileName);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern int SetNamedSecurityInfo(string pObjectName, int objectType, int securityInfo, IntPtr psidOwner, IntPtr psidGroup, IntPtr pDacl, IntPtr pSacl);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool InitializeAcl(IntPtr pAcl, int nAclLength, int dwAclRevision);

        internal const int SE_FILE_OBJECT = 1;
        internal const int OWNER_SECURITY_INFORMATION = 0x00000001;
        internal const int DACL_SECURITY_INFORMATION = 0x00000004;
        internal const int UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000;
        internal const int ACL_REVISION = 2;
        internal const int ACL_SIZE = 1024; // Достаточно для пустой ACL

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
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpFilename, [In][MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetMappedFileName(
            IntPtr hProcess,
            IntPtr lpv, // Address in the process
            [Out] StringBuilder lpFilename,
            [In][MarshalAs(UnmanagedType.U4)] int nSize
        );

        [DllImport("kernel32.dll")]
        public static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetLogicalDrives();


        [DllImport("ntdll.dll")]
        public static extern int NtQuerySystemInformation(
            int SystemInformationClass,
            IntPtr SystemInformation,
            int SystemInformationLength,
            ref int ReturnLength);

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
        public static extern int NtQueryObject(
            IntPtr ObjectHandle,
            int ObjectInformationClass,
            IntPtr ObjectInformation,
            int ObjectInformationLength,
            ref int ReturnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DuplicateHandle(
            IntPtr hSourceProcessHandle,
            IntPtr hSourceHandle,
            IntPtr hTargetProcessHandle,
            out IntPtr lpTargetHandle,
            uint dwDesiredAccess,
            bool bInheritHandle,
            uint dwOptions);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_HANDLE_INFORMATION
        {
            public int ProcessId;
            public byte ObjectTypeNumber;
            public byte Flags;
            public ushort Handle;
            public IntPtr Object;
            public uint GrantedAccess;
        }

        public const int SystemHandleInformation = 16;
        public const int ObjectNameInformation = 1;
        public const uint DUPLICATE_SAME_ACCESS = 0x0002;


        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int LsaOpenPolicy(ref LSA_OBJECT_ATTRIBUTES ObjectAttributes, ref UNICODE_STRING SystemName, int AccessMask, out IntPtr PolicyHandle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int LsaAddAccountRights(IntPtr PolicyHandle, IntPtr AccountSid, UNICODE_STRING[] UserRights, int CountOfRights);

        [DllImport("advapi32.dll")]
        public static extern int LsaClose(IntPtr PolicyHandle);

        [DllImport("advapi32.dll")]
        public static extern int LsaNtStatusToWinError(int status);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool LookupAccountName(string lpSystemName, string lpAccountName, IntPtr Sid, ref int cbSid, System.Text.StringBuilder ReferencedDomainName, ref int cchReferencedDomainName, out int peUse);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool LookupAccountSid(string lpSystemName, IntPtr Sid, StringBuilder Name, ref int cchName, StringBuilder ReferencedDomainName, ref int cchReferencedDomainName, out int peUse);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ConvertStringSidToSid(string StringSid, out IntPtr Sid);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        [StructLayout(LayoutKind.Sequential)]
        public struct LSA_OBJECT_ATTRIBUTES
        {
            public int Length;
            public IntPtr RootDirectory;
            public IntPtr ObjectName;
            public int Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int RegOpenKeyEx(IntPtr hKey, string subKey, int ulOptions, int samDesired, out IntPtr hkResult);

        [DllImport("Advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint RegOpenKeyEx(IntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, ref IntPtr phkResult);


        [DllImport("advapi32.dll", EntryPoint = "RegDeleteTreeW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int RegDeleteTree(IntPtr hKey, string lpSubKey);

        public const int HKEY_CLASSES_ROOT = unchecked((int)0x80000000);
        public const int HKEY_CURRENT_USER = unchecked((int)0x80000001);
        public const int HKEY_LOCAL_MACHINE = unchecked((int)0x80000002);

        public const int KEY_READ = 0x20019;
        public const int KEY_WRITE = 0x20006;
        public const int KEY_WOW64_64KEY = 0x0100;
        public const int KEY_ENUMERATE_SUB_KEYS = 0x0008;
        public const int WRITE_OWNER = 0x00080000;
        public const int WRITE_DAC = 0x00040000;
        public const int DELETE = 0x00010000;
        public const int READ_CONTROL = 0x00020000;
        public const int KEY_ALL_ACCESS = 0xF003F;

        [DllImport("Advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint RegCloseKey(IntPtr hKey);

        public const int TOKEN_ASSIGN_PRIMARY = 0x1;
        public const int TOKEN_DUPLICATE = 0x2;
        public const int TOKEN_IMPERSONATE = 0x4;
        public const int TOKEN_QUERY_SOURCE = 0x10;
        public const int TOKEN_ADJUST_GROUPS = 0x40;
        public const int TOKEN_ADJUST_DEFAULT = 0x80;

        [DllImport("Advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint SetSecurityInfo(IntPtr handle, SE_OBJECT_TYPE ObjectType, uint SecurityInfo,
            IntPtr psidOwner, IntPtr psidGroup, IntPtr pDacl, IntPtr pSacl);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint SetEntriesInAcl(uint cCountOfExplicitEntries, ref EXPLICIT_ACCESS pListOfExplicitEntries, IntPtr OldAcl, out IntPtr NewAcl);

        public enum SE_OBJECT_TYPE
        {
            SE_UNKNOWN_OBJECT_TYPE = 0,
            SE_FILE_OBJECT,
            SE_SERVICE,
            SE_PRINTER,
            SE_REGISTRY_KEY,
            SE_LMSHARE,
            SE_KERNEL_OBJECT,
            SE_WINDOW_OBJECT,
            SE_DS_OBJECT,
            SE_DS_OBJECT_ALL,
            SE_PROVIDER_DEFINED_OBJECT,
            SE_WMIGUID_OBJECT,
            SE_REGISTRY_WOW64_32KEY,
            SE_REGISTRY_WOW64_64KEY,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EXPLICIT_ACCESS
        {
            public uint grfAccessPermissions;
            public ACCESS_MODE grfAccessMode;
            public uint grfInheritance;
            public TRUSTEE Trustee;
        }

        public enum ACCESS_MODE
        {
            NOT_USED_ACCESS = 0,
            GRANT_ACCESS,
            SET_ACCESS,
            DENY_ACCESS,
            REVOKE_ACCESS,
            SET_AUDIT_SUCCESS,
            SET_AUDIT_FAILURE
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TRUSTEE
        {
            public IntPtr pMultipleTrustee;
            public MULTIPLE_TRUSTEE_OPERATION MultipleTrusteeOperation;
            public TRUSTEE_FORM TrusteeForm;
            public TRUSTEE_TYPE TrusteeType;
            public IntPtr ptstrName;
        }

        public enum TRUSTEE_FORM
        {
            TRUSTEE_IS_SID,
            TRUSTEE_IS_NAME,
            TRUSTEE_BAD_FORM,
            TRUSTEE_IS_OBJECTS_AND_SID,
            TRUSTEE_IS_OBJECTS_AND_NAME
        }

        public enum TRUSTEE_TYPE
        {
            TRUSTEE_IS_UNKNOWN,
            TRUSTEE_IS_USER,
            TRUSTEE_IS_GROUP,
            TRUSTEE_IS_DOMAIN,
            TRUSTEE_IS_ALIAS,
            TRUSTEE_IS_WELL_KNOWN_GROUP,
            TRUSTEE_IS_DELETED,
            TRUSTEE_IS_INVALID,
            TRUSTEE_IS_COMPUTER
        }

        public enum MULTIPLE_TRUSTEE_OPERATION
        {
            NO_MULTIPLE_TRUSTEE,
            TRUSTEE_IS_IMPERSONATE
        }

        internal static bool GrantPrivilegeToGroup(string groupName, string privilege)
        {
            IntPtr policyHandle = IntPtr.Zero;
            IntPtr sid = IntPtr.Zero;
            try
            {
                LSA_OBJECT_ATTRIBUTES objectAttributes = new Native.LSA_OBJECT_ATTRIBUTES();
                UNICODE_STRING systemName = new Native.UNICODE_STRING();

                int result = LsaOpenPolicy(ref objectAttributes, ref systemName, Native.POLICY_ALL_ACCESS, out policyHandle);
                if (result != 0)
                {
                    throw new Exception("Cannot open security descriptor: " + Native.LsaNtStatusToWinError(result));
                }

                int sidSize = 0;
                int domainNameLength = 0;
                int use;
                LookupAccountName(null, groupName, sid, ref sidSize, null, ref domainNameLength, out use);

                sid = Marshal.AllocHGlobal(sidSize);
                var domainName = new System.Text.StringBuilder(domainNameLength);

                if (!LookupAccountName(null, groupName, sid, ref sidSize, domainName, ref domainNameLength, out use))
                {
                    throw new Exception("Error getting admin group SID");
                }

                UNICODE_STRING[] userRights = new Native.UNICODE_STRING[1];
                userRights[0] = new Native.UNICODE_STRING
                {
                    Buffer = Marshal.StringToHGlobalUni(privilege),
                    Length = (ushort)(privilege.Length * UnicodeEncoding.CharSize),
                    MaximumLength = (ushort)((privilege.Length + 1) * UnicodeEncoding.CharSize)
                };

                result = LsaAddAccountRights(policyHandle, sid, userRights, userRights.Length);
                if (result != 0)
                {
                    throw new Exception("Error assign privilege:" + LsaNtStatusToWinError(result));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (policyHandle != IntPtr.Zero)
                {
                    LsaClose(policyHandle);
                }

                if (sid != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(sid);
                }
            }
        }

        internal static string ConvertWellKnowSIDToGroupName(string GroupSid)
        {
            IntPtr pSid;
            if (!ConvertStringSidToSid(GroupSid, out pSid))
            {
                return null;
            }

            string groupName = OSExtensions.GetAccountNameFromSid(pSid);
            if (groupName != null)
            {
                return groupName;
            }

            LocalFree(pSid);
            return null;

        }

        public const int POLICY_ALL_ACCESS = 0x000F0FFF;
        public const int ERROR_SUCCESS = 0;

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
        public const uint PROCESS_CREATE_THREAD = 0x0002;
        public const uint PROCESS_VM_OPERATION = 0x0008;
        public const int PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;
        public const uint PROCESS_DUP_HANDLE = 0x0040;

        public const uint THREAD_QUERY_INFORMATION = 0x0040;


        public const int OffsetProcessParametersx32 = 0x10;
        public const int OffsetCommandLinex32 = 0x40;
        public const uint TH32CS_SNAPPROCESS = 0x00000002;
        public const int STATUS_SUCCESS = 0;
        public const int ProcessBasicInformation = 0;

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

        [StructLayout(LayoutKind.Sequential)]
        public struct PRIVILEGE_SET
        {
            public uint PrivilegeCount;
            public uint Control;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privilege;
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
            Windows_11_25H2 = 26200,
        };



        internal static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return new IntPtr((long)GetClassLong32(hWnd, nIndex));
            else
                return GetClassLong64(hWnd, nIndex);
        }

        internal struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        internal struct SECURITY_ATTRIBUTES
        {
            public int Length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        internal enum LogonFlags
        {
            WithProfile = 1,
            NetCredentialsOnly = 2,
        }

        internal enum CreationFlags
        {
            Suspended = 4,
            NewConsole = 16, // 0x00000010
            NewProcessGroup = 512, // 0x00000200
            UnicodeEnvironment = 1024, // 0x00000400
            SeparateWOWVDM = 2048, // 0x00000800
            ExtendedStartupInfoPresent = 524288, // 0x00080000
            DefaultErrorMode = 67108864, // 0x04000000
        }

        internal enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation = 2,
        }

        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        internal const uint MAXIMUM_ALLOWED = 33554432U;

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, ref SECURITY_ATTRIBUTES lpTokenAttributes, int SECURITY_IMPERSONATION_LEVEL, TOKEN_TYPE TokenType, out IntPtr phNewToken);
        [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool CreateProcessWithTokenW(IntPtr hToken, LogonFlags dwLogonFlags, string lpApplicationName, string lpCommandLine, CreationFlags dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        internal static void RunAs(string filename, string cmdLine)
        {
            string location = Assembly.GetEntryAssembly().Location;
            IntPtr handle = Process.GetProcessesByName(filename)[0].Handle;
            IntPtr TokenHandle;
            OpenProcessToken(handle, 2U, out TokenHandle);
            STARTUPINFO lpStartupInfo = new STARTUPINFO
            {
                dwFlags = 1,
                wShowWindow = 1
            };
            SECURITY_ATTRIBUTES lpTokenAttributes = new SECURITY_ATTRIBUTES();
            IntPtr phNewToken;
            DuplicateTokenEx(TokenHandle, MAXIMUM_ALLOWED, ref lpTokenAttributes, 2, TOKEN_TYPE.TokenPrimary, out phNewToken);
            CreateProcessWithTokenW(phNewToken, LogonFlags.NetCredentialsOnly, location, cmdLine, CreationFlags.NewConsole, IntPtr.Zero, null, ref lpStartupInfo, out _);
        }

        internal static void RunAsUser(string filename, string cmdLine)
        {
            string location = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32", "cmd.exe");

            IntPtr handle = IntPtr.Zero;
            if (AppConfig.Instance.WinPEMode)
            {
                handle = Process.GetProcessesByName(filename)[0].Handle;
            }
            else
            {
                foreach (Process process in Process.GetProcessesByName(filename))
                {
                    try
                    {
                        if (!ProcessManager.IsSystemProcess(process.Id))
                        {
                            handle = process.Handle;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }
            }

            IntPtr TokenHandle;
            OpenProcessToken(handle, 2U, out TokenHandle);
            STARTUPINFO lpStartupInfo = new STARTUPINFO
            {
                dwFlags = 1,
                wShowWindow = 0
            };
            SECURITY_ATTRIBUTES lpTokenAttributes = new SECURITY_ATTRIBUTES();
            IntPtr phNewToken;
            DuplicateTokenEx(TokenHandle, MAXIMUM_ALLOWED, ref lpTokenAttributes, 2, TOKEN_TYPE.TokenPrimary, out phNewToken);
            CreateProcessWithTokenW(phNewToken, LogonFlags.WithProfile, location, cmdLine, 0, IntPtr.Zero, null, ref lpStartupInfo, out _);
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
            catch (Exception)
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

        internal static void CheckWMI(bool restartCheck)
        {
            string serviceName = "wi~nm~gm~t".Replace("~", "");
            try
            {
                if (ServiceIsInstalled(serviceName))
                {
                    var serviceinfo = GetServiceInfo(serviceName);

                    if ((ServiceBootFlag)serviceinfo.StartType != ServiceBootFlag.AutoStart)
                    {
                        ChangeStartMode(serviceName, ServiceBootFlag.AutoStart);
                        AppConfig.Instance.LL.LogSuccessMessage("_CriticalServiceStartup");
                    }

                    if (GetServiceState(serviceName) != ServiceState.Running)
                    {
                        StartService(serviceName);
                        AppConfig.Instance.LL.LogSuccessMessage("_CriticalServiceRestart");
                    }

                    try
                    {
                        WmiTestQuery("Dhcp");
                    }
                    catch (ManagementException me)
                    {
                        if (me.ErrorCode == ManagementStatus.InvalidClass || me.ErrorCode == ManagementStatus.ProviderLoadFailure || me.ErrorCode == ManagementStatus.ProviderFailure)
                        {
                            RestoreWMICorruption();
                            if (!restartCheck)
                            {
                                CheckWMI(true);
                            }
                        }
                    }

                }
                else
                {
                    LocalizedLogger.LogError_СriticalServiceNotInstalled();
                }

            }
            catch (MissingMethodException)
            {
                if (OSExtensions.IsDotNetInstalled())
                {
                    DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNoDotNet"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[xxx] {serviceName}: {ex.Message}", ConsoleColor.DarkRed, false);
            }


        }

        static void RestoreWMICorruption()
        {
            if (AppConfig.Instance.RestoredWMI)
            {
                throw new Exception("WMI_Corruption");
            }

            AppConfig.Instance.LL.LogMessage("\t\t[xxx]", "_WMICorruption", "", ConsoleColor.Red, false);

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            AppConfig.Instance.LL.LogHeadMessage("_WMIRecompilation");

            string wbemPath = Path.Combine(Environment.SystemDirectory, "Wbem");

            List<string> filteredMof = Directory
                    .EnumerateFiles(wbemPath, "*.*", SearchOption.AllDirectories)
                    .Where(file => new FileInfo(file).Extension.Equals(".mof") || new FileInfo(file).Extension.Equals(".mfl"))
                    .ToList();

            foreach (var file in filteredMof)
            {
                if (File.Exists(file))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "mofcomp.exe",
                        Arguments = $"\"{file}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }).WaitForExit();
                }
            }

            Logger.WriteLog($"\t\t[OK]", Logger.success, false, true);
            AppConfig.Instance.LL.LogHeadMessage("_WMIRegister");

            foreach (var file in Directory.EnumerateFiles(wbemPath, "*.dll", SearchOption.AllDirectories))
            {
                if (File.Exists(file))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "regsvr32.exe",
                        Arguments = $"-s \"{file}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }).WaitForExit();
                }
            }


            Logger.WriteLog($"\t\t[OK]", Logger.success, false, true);
            AppConfig.Instance.LL.LogHeadMessage("_WMIRestartService");

            ServiceController service = new ServiceController("winmgmt");
            if (service.Status != ServiceControllerStatus.Stopped)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            Thread.Sleep(3000);
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running);


            Logger.WriteLog($"\t\t[OK]", Logger.success, false, true);
        }

        internal static string WmiTestQuery(string serviceName)
        {

            using (var service = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                service.Get();
                string sPath = service["PathName"]?.ToString();

                return string.IsNullOrEmpty(sPath) ? "" : sPath;
            }
        }

        internal static void CheckTermService()
        {

            string registryPath = MSData.Instance.queries["TermServiceParameters"]; //SYSTEM\CurrentControlSet\Services\TermService\Parameters
            string desiredValue = MSData.Instance.queries["TermsrvDll"]; //%SystemRoot%\System32\termsrv.dll
            string paramName = "Ser/vice/Dll".Replace("/", "");

            using (var regkey = Registry.LocalMachine.OpenSubKey(registryPath, true))
            {
                if (regkey != null)
                {
                    string currentValue = (string)regkey.GetValue(paramName);
                    if (currentValue != null)
                    {
                        if (currentValue != Environment.ExpandEnvironmentVariables(desiredValue))
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_TermServiceInvalidPath", currentValue);
                            AppConfig.Instance.totalFoundSuspiciousObjects++;
                            MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Suspicious, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + " TermService", ScanActionType.Skipped, AppConfig.Instance.LL.GetLocalizedString("_TermServiceInvalidPath") + " " + currentValue));


                            bool isInfectedService = false;
                            foreach (ScanResult res in MinerSearch.scanResults)
                            {
                                foreach (string pattern in MSData.Instance.JohnPatterns)
                                {
                                    if (res.Path.IndexOf(pattern) >= 0)
                                    {
                                        isInfectedService = true;
                                    }
                                }
                            }

                            if (isInfectedService)
                            {

                                AppConfig.Instance.totalFoundThreats++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    try
                                    {
                                        string termsrv = "TermService";
                                        string UmRdpSrv = "UmRdpService";

                                        var UmRdpSrvInfo = GetServiceInfo(UmRdpSrv);
                                        var termSrvInfo = GetServiceInfo(termsrv);

                                        if (GetServiceState(UmRdpSrv) == ServiceState.Running)
                                        {
                                            StopService(UmRdpSrv);
                                        }

                                        if ((ServiceBootFlag)UmRdpSrvInfo.StartType != ServiceBootFlag.DemandStart)
                                        {
                                            ChangeStartMode(UmRdpSrv, ServiceBootFlag.DemandStart);
                                        }

                                        if (GetServiceState(termsrv) == ServiceState.Running)
                                        {
                                            StopService(termsrv);
                                        }

                                        if ((ServiceBootFlag)termSrvInfo.StartType != ServiceBootFlag.DemandStart)
                                        {
                                            ChangeStartMode(termsrv, ServiceBootFlag.DemandStart);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                                        return;
                                    }

                                    regkey.SetValue(paramName, desiredValue, RegistryValueKind.ExpandString);
                                    currentValue = (string)regkey.GetValue(paramName);
                                    if (currentValue == Environment.ExpandEnvironmentVariables(desiredValue))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_TermServiceRestored");
                                        MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Infected, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + " TermService", ScanActionType.Cured));

                                    }
                                    else
                                    {
                                        AppConfig.Instance.LL.LogErrorMessage("_TermServiceFailedRestore", new Exception(""));
                                        MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Infected, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + " TermService", ScanActionType.Error));

                                    }
                                }
                                else
                                {
                                    MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Infected, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + " TermService", ScanActionType.Skipped));
                                    LocalizedLogger.LogScanOnlyMode();
                                }
                            }
                            else
                            {
                                LocalizedLogger.LogNoThreatsFound();
                            }
                        }
                        else
                        {
                            LocalizedLogger.LogNoThreatsFound();
                        }
                    }
                }
                else
                {
                    AppConfig.Instance.LL.LogWarnMediumMessage("_ServiceNotInstalled", "T?ermS?ervi?ce".Replace("?", ""));
                }
            }

        }
    }

    public static class NativeServiceController
    {
        const int SERVICE_NO_CHANGE = -1;
        const int SERVICE_QUERY_CONFIG = 0x0001;
        const int SERVICE_CHANGE_CONFIG = 0x0002;

        const int SERVICE_AUTO_START = 0x00000002;
        const int SERVICE_DEMAND_START = 0x00000003;
        const int SERVICE_DISABLED = 0x00000004;

        public enum ServiceStartMode
        {
            Automatic = SERVICE_AUTO_START,
            Manual = SERVICE_DEMAND_START,
            Disabled = SERVICE_DISABLED
        }

        [StructLayout(LayoutKind.Sequential)]
        struct QUERY_SERVICE_CONFIG
        {
            public int dwServiceType;
            public int dwStartType;
            public int dwErrorControl;
            public IntPtr lpBinaryPathName;
            public IntPtr lpLoadOrderGroup;
            public int dwTagId;
            public IntPtr lpDependencies;
            public IntPtr lpServiceStartName;
            public IntPtr lpDisplayName;
        }

        private const int SC_MANAGER_CONNECT = 0x0001;
        private const int SERVICE_QUERY_STATUS = 0x0004;
        private const int SERVICE_STOPPED = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        private struct SERVICE_STATUS_PROCESS
        {
            public int dwServiceType;
            public int dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
            public int dwProcessId;
            public int dwServiceFlags;
        }


        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, int dwDesiredAccess);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, int dwDesiredAccess);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool QueryServiceConfig(IntPtr hService, IntPtr lpServiceConfig, int cbBufSize, out int pcbBytesNeeded);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool QueryServiceStatusEx(IntPtr hService, int InfoLevel, IntPtr lpBuffer, int cbBufSize, out int pcbBytesNeeded);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LocalAlloc(uint uFlags, UIntPtr uBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool ChangeServiceConfig(
            IntPtr hService,
            int dwServiceType,
            int dwStartType,
            int dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            IntPtr lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword,
            string lpDisplayName);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool CloseServiceHandle(IntPtr hSCObject);

        public static void SetServiceStartType(string serviceName, ServiceStartMode startMode)
        {
            IntPtr scManager = OpenSCManager(null, null, SERVICE_CHANGE_CONFIG);
            if (scManager == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "SetServiceStartType(): Cannot open SCManager");
            }

            IntPtr serviceHandle = OpenService(scManager, serviceName, SERVICE_CHANGE_CONFIG);
            if (serviceHandle == IntPtr.Zero)
            {
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "SetServiceStartType(): Cannot open service");
            }

            if (!ChangeServiceConfig(
                serviceHandle,
                SERVICE_NO_CHANGE,
                (int)startMode,
                SERVICE_NO_CHANGE,
                null,
                null,
                IntPtr.Zero,
                null,
                null,
                null,
                null))
            {
                CloseServiceHandle(serviceHandle);
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "SetServiceStartType(): Error on getting service config");
            }

            CloseServiceHandle(serviceHandle);
            CloseServiceHandle(scManager);
        }

        public static ServiceStartMode GetServiceStartType(string serviceName)
        {
            IntPtr scManager = OpenSCManager(null, null, SERVICE_QUERY_CONFIG);
            if (scManager == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceStartType(): Cannot open SCManager");
            }

            IntPtr serviceHandle = OpenService(scManager, serviceName, SERVICE_QUERY_CONFIG);
            if (serviceHandle == IntPtr.Zero)
            {
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceStartType(): Cannot open service");
            }

            int bufferSize = 8192;
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);

            if (!QueryServiceConfig(serviceHandle, buffer, bufferSize, out int bytesNeeded))
            {
                Marshal.FreeHGlobal(buffer);
                CloseServiceHandle(serviceHandle);
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceStartType(): Error on getting service config");
            }

            QUERY_SERVICE_CONFIG config = Marshal.PtrToStructure<QUERY_SERVICE_CONFIG>(buffer);
            Marshal.FreeHGlobal(buffer);
            CloseServiceHandle(serviceHandle);
            CloseServiceHandle(scManager);

            return (ServiceStartMode)config.dwStartType;
        }

        public static string GetServiceImagePath(string serviceName)
        {
            IntPtr scManager = OpenSCManager(null, null, SERVICE_QUERY_CONFIG);
            if (scManager == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceImagePath(): Cannot open SCManager");
            }

            IntPtr serviceHandle = OpenService(scManager, serviceName, SERVICE_QUERY_CONFIG);
            if (serviceHandle == IntPtr.Zero)
            {
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceImagePath(): Cannot open service");
            }

            int bufferSize = 8192;
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);

            if (!QueryServiceConfig(serviceHandle, buffer, bufferSize, out int bytesNeeded))
            {
                Marshal.FreeHGlobal(buffer);
                CloseServiceHandle(serviceHandle);
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceImagePath(): Error on getting service config");
            }

            QUERY_SERVICE_CONFIG config = Marshal.PtrToStructure<QUERY_SERVICE_CONFIG>(buffer);
            string imagePath = Marshal.PtrToStringUni(config.lpBinaryPathName) ?? string.Empty;

            Marshal.FreeHGlobal(buffer);
            CloseServiceHandle(serviceHandle);
            CloseServiceHandle(scManager);

            return imagePath;
        }

        public static int GetServiceId(string serviceName)
        {
            IntPtr scManager = OpenSCManager(null, null, SERVICE_QUERY_STATUS);
            if (scManager == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceId(): can't open scmanager");
            }

            IntPtr serviceHandle = OpenService(scManager, serviceName, SERVICE_QUERY_STATUS);
            if (serviceHandle == IntPtr.Zero)
            {
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceId(): can't open svc");
            }

            int bufferSize = Marshal.SizeOf(typeof(SERVICE_STATUS_PROCESS));
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);

            if (!QueryServiceStatusEx(serviceHandle, 0, buffer, bufferSize, out int bytesNeeded))
            {
                Marshal.FreeHGlobal(buffer);
                CloseServiceHandle(serviceHandle);
                CloseServiceHandle(scManager);
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetServiceId(): can't query svc status");
            }

            SERVICE_STATUS_PROCESS status = Marshal.PtrToStructure<SERVICE_STATUS_PROCESS>(buffer);
            int processId = status.dwProcessId;

            Marshal.FreeHGlobal(buffer);
            CloseServiceHandle(serviceHandle);
            CloseServiceHandle(scManager);

            return processId;
        }

        public static bool IsServiceMarkedToDelete(string serviceName)
        {
            IntPtr scmHandle = IntPtr.Zero;
            IntPtr serviceHandle = IntPtr.Zero;
            IntPtr buffer = IntPtr.Zero;

            try
            {
                scmHandle = OpenSCManager(null, null, SC_MANAGER_CONNECT);
                if (scmHandle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "IsServiceMarkedToDelete(): can't open scmanager");

                serviceHandle = OpenService(scmHandle, serviceName, SERVICE_QUERY_STATUS);
                if (serviceHandle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "IsServiceMarkedToDelete() can't open svc");

                int bytesNeeded;
                buffer = LocalAlloc(0x0040, (UIntPtr)Marshal.SizeOf(typeof(SERVICE_STATUS_PROCESS)));
                if (!QueryServiceStatusEx(serviceHandle, 0, buffer, Marshal.SizeOf(typeof(SERVICE_STATUS_PROCESS)), out bytesNeeded))
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "IsServiceMarkedToDelete() can't query svc status");

                SERVICE_STATUS_PROCESS status = Marshal.PtrToStructure<SERVICE_STATUS_PROCESS>(buffer);

                return (status.dwCurrentState == SERVICE_STOPPED) && ((status.dwServiceFlags & 0x00000001) != 0);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                    LocalFree(buffer);
                if (serviceHandle != IntPtr.Zero)
                    CloseServiceHandle(serviceHandle);
                if (scmHandle != IntPtr.Zero)
                    CloseServiceHandle(scmHandle);
            }
        }
    }
}
