using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace ListTrayBarInfo
{
    public class SysTrayWnd
    {
        public struct TrayItemData
        {
            public int dwProcessID;
            public byte fsState;
            public byte fsStyle;
            public IntPtr hIcon;
            public IntPtr hProcess;
            public IntPtr hWnd;
            public int idBitmap;
            public int idCommand;
            public string lpProcImagePath;
            public string lpTrayToolTip;
        }
        public static IntPtr GetTrayWnd()
        {
            IntPtr handle = Win32.FindWindow("Shell_TrayWnd", null);
            handle = Win32.FindWindowEx(handle, IntPtr.Zero, "TrayNotifyWnd", null);
            handle = Win32.FindWindowEx(handle, IntPtr.Zero, "SysPager", null);
            handle = Win32.FindWindowEx(handle, IntPtr.Zero, "ToolbarWindow32", null);
            return handle;
        }
        public static TrayItemData[] GetTrayWndDetail()
        {
            SortedList<string, TrayItemData> stlTrayItems = new SortedList<string, TrayItemData>();
            try
            {
                Win32.TBBUTTON tbButtonInfo = new Win32.TBBUTTON();
                IntPtr hTrayWnd = IntPtr.Zero;
                IntPtr hTrayProcess = IntPtr.Zero;
                int iTrayProcessID = 0;
                int iAllocBaseAddress = 0;
                int iRet = 0;
                int iTrayItemCount = 0;
                hTrayWnd = GetTrayWnd();
                Win32.GetWindowThreadProcessId(hTrayWnd, ref iTrayProcessID);
                hTrayProcess = Win32.OpenProcess(
                (uint)Win32.ProcessAccessFlags.All |
                (uint)Win32.ProcessAccessFlags.VMOperation |
                (uint)Win32.ProcessAccessFlags.VMRead |
                (uint)Win32.ProcessAccessFlags.VMWrite, 0, (uint)iTrayProcessID);
                iAllocBaseAddress = Win32.VirtualAllocEx(hTrayProcess, 0, Marshal.SizeOf(tbButtonInfo), Win32.MEM_COMMIT, Win32.PAGE_READWRITE);
                iTrayItemCount = Win32.SendMessage(hTrayWnd, Win32.TB_BUTTONCOUNT, 0, 0);
                for (int i = 0; i < iTrayItemCount; i++)
                {
                    try
                    {
                        TrayItemData trayItem = new TrayItemData();
                        Win32.TRAYDATA trayData = new Win32.TRAYDATA();
                        int iOut = 0;
                        int dwProcessID = 0;
                        IntPtr hRelProcess = IntPtr.Zero;
                        string strTrayToolTip = string.Empty;
                        iRet = Win32.SendMessage(hTrayWnd, Win32.TB_GETBUTTON, i, iAllocBaseAddress);
                        IntPtr hButtonInfo = Marshal.AllocHGlobal(Marshal.SizeOf(tbButtonInfo));
                        IntPtr hTrayData = Marshal.AllocHGlobal(Marshal.SizeOf(trayData));
                        iRet = Win32.ReadProcessMemory(hTrayProcess, iAllocBaseAddress, hButtonInfo, Marshal.SizeOf(tbButtonInfo), out iOut);
                        Marshal.PtrToStructure(hButtonInfo, tbButtonInfo);
                        iRet = Win32.ReadProcessMemory(hTrayProcess, tbButtonInfo.dwData, hTrayData, Marshal.SizeOf(trayData), out iOut);
                        Marshal.PtrToStructure(hTrayData, trayData);
                        byte[] bytTextData = new byte[1024];
                        iRet = Win32.ReadProcessMemory(hTrayProcess, tbButtonInfo.iString, bytTextData, 1024, out iOut);
                        strTrayToolTip = Encoding.Unicode.GetString(bytTextData);
                        if (!string.IsNullOrEmpty(strTrayToolTip))
                        {
                            int iNullIndex = strTrayToolTip.IndexOf('\0');
                            strTrayToolTip = strTrayToolTip.Substring(0, iNullIndex);
                        }
                        Win32.GetWindowThreadProcessId(trayData.hwnd, ref dwProcessID);
                        hRelProcess = Win32.OpenProcess((uint)Win32.ProcessAccessFlags.QueryInformation, 0, (uint)dwProcessID);
                        StringBuilder sbProcImagePath = new StringBuilder(256);
                        if (hRelProcess != IntPtr.Zero)
                        {
                            Win32.GetProcessImageFileName(hRelProcess, sbProcImagePath, sbProcImagePath.Capacity);
                        }
                        string strImageFilePath = string.Empty;
                        if (sbProcImagePath.Length > 0)
                        {
                            int iDeviceIndex = sbProcImagePath.ToString().IndexOf("\\", "\\Device\\HarddiskVolume".Length);
                            string strDevicePath = sbProcImagePath.ToString().Substring(0, iDeviceIndex);
                            int iStartDisk = (int)'A';
                            while (iStartDisk <= (int)'Z')
                            {
                                StringBuilder sbWindowImagePath = new StringBuilder(256);
                                iRet = Win32.QueryDosDevice(((char)iStartDisk).ToString() + ":", sbWindowImagePath, sbWindowImagePath.Capacity);
                                if (iRet != 0)
                                {
                                    if (sbWindowImagePath.ToString() == strDevicePath)
                                    {
                                        strImageFilePath = ((char)iStartDisk).ToString() + ":" + sbProcImagePath.ToString().Replace(strDevicePath, "");
                                        break;
                                    }
                                }
                                iStartDisk++;
                            }
                        }
                        trayItem.dwProcessID = dwProcessID;
                        trayItem.fsState = tbButtonInfo.fsState;
                        trayItem.fsStyle = tbButtonInfo.fsStyle;
                        trayItem.hIcon = trayData.hIcon;
                        trayItem.hProcess = hRelProcess;
                        trayItem.hWnd = trayData.hwnd;
                        trayItem.idBitmap = tbButtonInfo.iBitmap;
                        trayItem.idCommand = tbButtonInfo.idCommand;
                        trayItem.lpProcImagePath = strImageFilePath;
                        trayItem.lpTrayToolTip = strTrayToolTip;
                        stlTrayItems[string.Format("{0:d8}", tbButtonInfo.idCommand)] = trayItem;
                    }
                    catch { continue; }
                }
                Win32.VirtualFreeEx(hTrayProcess, iAllocBaseAddress, Marshal.SizeOf(tbButtonInfo), Win32.MEM_RELEASE);
                Win32.CloseHandle(hTrayProcess);
                TrayItemData[] trayItems = new TrayItemData[stlTrayItems.Count];
                stlTrayItems.Values.CopyTo(trayItems, 0);
                return trayItems;
            }
            catch (SEHException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void RefreshTrayWnd()
        {
            TrayItemData[] trayItems = GetTrayWndDetail();
            IntPtr hTrayWnd = GetTrayWnd();
            for (int i = trayItems.Length - 1; i >= 0; i--)
            {
                int iProcessExitCode = 0;
                Win32.GetExitCodeProcess(trayItems[i].hProcess, ref iProcessExitCode);
                if (iProcessExitCode != Win32.STILL_ACTIVE)
                {
                    //通过隐藏图标来达到刷新的动作
                    int iRet = Win32.SendMessage(hTrayWnd, Win32.TB_HIDEBUTTON, trayItems[i].idCommand, 0);
                }
            }
        }
    }
}
