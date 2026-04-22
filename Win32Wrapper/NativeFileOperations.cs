using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Win32Wrapper
{
    public static class NativeFileOperations
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetFileAttributes(string lpFileName, uint dwFileAttributes);

        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        public static bool DeleteFileWithRetry(string path, int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    // 1. Снять атрибуты
                    SetFileAttributes(path, FILE_ATTRIBUTE_NORMAL);
                    
                    // 2. Удалить файл через WinAPI
                    if (DeleteFile(path))
                    {
                        return true;
                    }
                    
                    int lastError = Marshal.GetLastWin32Error();
                    if (lastError == 2) // ERROR_FILE_NOT_FOUND
                    {
                        return true; // Файл уже удалён
                    }
                    
                    // Подождать перед следующей попыткой
                    if (i < maxRetries - 1)
                    {
                        System.Threading.Thread.Sleep(100 * (i + 1));
                    }
                }
                catch (Exception ex) when (!(ex is Win32Exception))
                {
                    // Логировать, но продолжить попытки
                    // Логирование выполняется на стороне вызывающего кода
                    
                    if (i < maxRetries - 1)
                    {
                        System.Threading.Thread.Sleep(100 * (i + 1));
                    }
                }
            }
            
            return false;
        }
    }
}
