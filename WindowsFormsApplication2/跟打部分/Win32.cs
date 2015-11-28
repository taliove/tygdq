using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace ListTrayBarInfo
{
    public class Win32
    {
        public const int MEM_RELEASE = 0x8000;
        public const int MEM_COMMIT = 0x1000;
        public const int PAGE_READWRITE = 0x04;
        public const int WM_USER = 0x0400;
        public const int TB_GETBUTTON = (WM_USER + 23);
        public const int TB_BUTTONCOUNT = (WM_USER + 24);
        public const int TB_HIDEBUTTON = (WM_USER + 4);
        public const int STILL_ACTIVE = 0x0103;
        [StructLayout(LayoutKind.Sequential)]
        public class TRAYDATA
        {
            public IntPtr hwnd;
            public uint uID;
            public uint uCallbackMessage;
            public int Reserved0;
            public int Reserved1;
            public IntPtr hIcon;                //托盘图标的句柄 
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class TBBUTTON
        {
            public int iBitmap;
            public int idCommand;
            public byte fsState;
            public byte fsStyle;
            public byte bReserved0;
            public byte bReserved1;
            public int dwData;
            public int iString;
        }
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern int VirtualAllocEx(IntPtr hProcess,
            int lpAddress,
            int dwSize,
            int flAllocationType,
            int flProtect);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, int lpAddress,
           int dwSize, uint dwFreeType);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress,
             IntPtr lpBuffer, int nSize, out int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress,
             byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, ref int ProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
        [DllImport("psapi.dll", SetLastError = true)]
        public static extern int GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, int nSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetExitCodeProcess(IntPtr hProcess, ref int lpExitCode);

    }
}
