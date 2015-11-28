using System;
using System.Text;

namespace TyDll
{
    /// <summary>
    /// Win32 API 常量
    /// </summary>
    public class Consts
    {
        #region Result
        /// <summary>
        /// 1 真
        /// </summary>
        public static readonly IntPtr TRUE = new IntPtr(1);
        /// <summary>
        /// 0 假
        /// </summary>
        public static readonly IntPtr FALSE = new IntPtr(0);
        #endregion

        #region mouse_event
        /// <summary>
        /// 移动鼠标
        /// </summary>
        public const int MOUSEEVENTF_MOVE = 0x0001;
        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        /// <summary>
        /// 按下鼠标右键
        /// </summary>
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        /// <summary>
        /// 释放鼠标中键
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        /// <summary>
        /// 标示是否采用绝对坐标 
        /// </summary>
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        #endregion

        #region keybd_event
        /// <summary>
        /// 抬起按键
        /// </summary>
        public const int KEYEVENTF_KEYUP = 0x0002;
        /// <summary>
        /// 按下按键
        /// </summary>
        public const int KEYEVENTF_KEYDOWN = 0x0000;

        #endregion

        #region OpenProcess
        /// <summary>
        /// 所有可能的进程对象的访问权限
        /// </summary>
        public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        /// <summary>
        /// 需要在内存中读取进程应使用ReadProcessMemory
        /// </summary>
        public const int PROCESS_VM_READ = 0x0010;
        /// <summary>
        /// 需要在需要在内存中写入进程应使用WriteProcessMemory
        /// </summary>
        public const int PROCESS_VM_WRITE = 0x0020;
        #endregion

        #region ULW
        /// <summary>
        /// 
        /// </summary>
        public const Int32 ULW_COLORKEY = 0x00000001;
        /// <summary>
        /// 
        /// </summary>
        public const Int32 ULW_ALPHA = 0x00000002;
        /// <summary>
        /// 
        /// </summary>
        public const Int32 ULW_OPAQUE = 0x00000004;
        #endregion

        #region AC_SRC
        /// <summary>
        /// 
        /// </summary>
        public const byte AC_SRC_OVER = 0x00;
        /// <summary>
        /// 
        /// </summary>
        public const byte AC_SRC_ALPHA = 0x01;
        #endregion
    }
}
