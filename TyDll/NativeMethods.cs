using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TyDll
{
    /// <summary>
    /// Win32 API 方法
    /// </summary>
    public class NativeMethods
    {
        #region Kernel32.dll

        /// <summary>
        /// 关闭一个内核对象
        /// </summary>
        [DllImport("Kernel32.dll")]
        public static extern bool CloseHandle(int hObject);

        /// <summary>
        /// 针对之前调用的api函数，用这个函数取得扩展错误信息
        /// </summary>
        [DllImport("Kernel32.dll")]
        public static extern int GetLastError();

        /// <summary>
        /// 获取当前线程一个唯一的线程标识符
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        /// <summary>
        /// 获取一个应用程序或动态链接库的模块句柄
        /// </summary>
        /// <param name="name">指定模块名，这通常是与模块的文件名相同的一个名字。例如，NOTEPAD.EXE程序的模块文件名就叫作NOTEPAD</param>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        /// <summary>
        /// 用来打开一个已存在的进程对象，并返回进程的句柄
        /// </summary>
        /// <param name="dwDesiredAccess">访问权限</param>
        /// <param name="hInheritHandle">继承标志</param>
        /// <param name="dwProcessId">进程ID</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool hInheritHandle, int dwProcessId);
        // GetWindowThreadProcessId(hwnd, out calcID);   
        //calcProcess = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE, false, calcID);

        /// <summary>
        /// 从指定内存中读取字节集数据
        /// </summary>
        /// <param name="hProcess">进程句柄</param>
        /// <param name="lpBaseAddress">内存地址</param>
        /// <param name="lpBuffer">数据存储变量</param>
        /// <param name="nSize">长度</param>
        /// <param name="lpNumberOfBytesRead">读取长度</param>
        [DllImport("Kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out]byte[] lpBuffer, UInt32 nSize, UInt32 lpNumberOfBytesRead);

        /// <summary>
        /// 此函数能写入某一进程的内存区域
        /// </summary>
        /// <param name="hProcess">由OpenProcess返回的进程句柄</param>
        /// <param name="lpBaseAddress">要写的内存首地址</param>
        /// <param name="lpBuffer">指向要写的数据的指针</param>
        /// <param name="nSize">要写入的字节数</param>
        /// <param name="lpNumberOfByteRead"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfByteRead);

        /// <summary>
        /// 获取Windows目录的完整路径名
        /// </summary>
        /// <param name="lpBuffer">指定一个字符串缓冲区，用于装载Windows目录名</param>
        /// <param name="nSize">lpBuffer字符串的最大长度</param>
        /// <returns>复制到lpBuffer的一个字符串的长度。如果lpBuffer不够大，不能容下整个字符串，就会返回lpbuffer要求的长度。零表地失败。会设置GetLastError</returns>
        [DllImport("Kernel32.dll")]
        public static extern long GetWindowsDirectory(StringBuilder lpBuffer, int nSize);

        /// <summary>
        /// 获取System32文件夹的路径
        /// </summary>
        [DllImport("Kernel32.dll")]
        public static extern long GetSystemDirectory(StringBuilder lpBuffer, int nSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpuinfo"></param>
        [DllImport("Kernel32.dll")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meminfo"></param>
        [DllImport("Kernel32.dll")]
        public static extern void GlobalMemory(ref MEMORY_INFO meminfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stinfo"></param>
        [DllImport("Kernel32.dll")]
        public static extern void GetSystem(ref SYSTEMTIME_INFO stinfo);

        #endregion

        #region shell32.dll
        /// <summary>
        /// 返回系统设置的图标
        /// </summary>
        /// <param name="pszPath">指定的文件名。( 如果为"" 返回文件夹)</param>
        /// <param name="dwFileAttributes">
        /// <para>文件属性。仅当uFlags的取值中包含SHGFI_USEFILEATTRIBUTES时有效</para>
        /// <para>不般不用此参数：0</para>
        /// </param>
        /// <param name="psfi">返回获得的文件信息,是一个记录类型</param>
        /// <param name="cbfileInfo">psfi的byte值</param>
        /// <param name="uFlags">指明需要返回的文件信息标识符,请参见：enum SHGFI</param>
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbfileInfo, uint uFlags);
        #endregion

        #region user32.dll
        /// <summary>
        /// 返回hWnd参数所指定的窗口的设备环境
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        /// 创建一个圆角矩形区域
        /// </summary>
        /// <param name="nLeftRect">x坐标左上角</param>
        /// <param name="nTopRect">y坐标左上角</param>
        /// <param name="nRightRect">x坐标右上角</param>
        /// <param name="nBottomRect">y坐标右上角</param>
        /// <param name="nWidthEllipse">椭圆的宽度</param>
        /// <param name="nHeightEllipse">椭圆的高度</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        #region FindWindow

        /// <summary>
        /// <para>返回与指定字符串相匹配的窗口类名或窗口名的最顶层窗口的窗口句柄</para>
        /// <para>如果函数执行成功，则返回值是拥有指定窗口类名或窗口名的窗口的句柄。</para>
        /// <para>如果函数执行失败，则返回值为 NULL 。</para>
        /// <para>可以通过调用GetLastError函数获得更加详细的错误信息。</para>
        /// </summary>
        /// <param name="lpClassName">指向包含了窗口类名的空中止(C语言)字串的指针;或设为零,表示接收任何类</param>
        /// <param name="lpWindowName">指向包含了窗口文本(或标签)的空中止(C语言)字串的指针;或设
        /// 为零,表示接收任何窗口标题</param>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndParent">
        /// <para>在其中查找子的父窗口。</para>
        /// <para>如设为零，表示使用桌面窗口</para>
        /// <para>（通常说的顶级窗口都被认为是桌面的子窗口，所以也会对它们进行查找）</para>
        /// </param>
        /// <param name="hwndChildAfter">
        /// <para>，从这个窗口后开始查找。</para>
        /// <para>这样便可利用对FindWindowEx的多次调用找到符合条件的所有子窗口。</para>
        /// <para>如设为零，表示从第一个子窗口开始搜索</para>
        /// </param>
        /// <param name="lpClassName">欲搜索的类名。零表示忽略</param>
        /// <param name="lpWindowName">欲搜索的类名。零表示忽略</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        #endregion

        /// <summary>
        /// <para>该函数获取窗口客户区的坐标。</para>
        /// <para>客户区坐标指定客户区的左上角和右下角。</para>
        /// <para>由于客户区坐标是相对窗口客户区的左上角而言的，因此左上角坐标为（0，0）</para>
        /// </summary>
        /// <param name="hWnd">目标窗口</param>
        /// <param name="lpRect">指定一个矩形，用客户区域的大小载入（以像素为单位）</param>
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(int hWnd, ref RECT lpRect);

        /// <summary>
        /// <para>该函数检取光标的位置，以屏幕坐标表示。</para>
        /// <para>返回值：如果成功，返回值非零；如果失败，返回值为零。</para>
        /// </summary>
        /// <param name="lpPoint">随同指针在屏幕像素坐标中的位置载入的一个结构</param>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// 检索当前双击鼠标的时间
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetDoubleClickTime();

        /// <summary>
        /// 该函数返回指定窗口的边框矩形的尺寸。该尺寸以相对于屏幕坐标左上角的屏幕坐标给出。
        /// </summary>
        /// <param name="hWnd">想获得范围矩形的那个窗口的句柄</param>
        /// <param name="lpRect">屏幕坐标中随同窗口装载的矩形</param>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(int hWnd, ref RECT lpRect);

        /// <summary>
        /// 这个函数获得指定线程的标识符,此线程创建了指定的窗口,并且随机的产生了这个标识符.
        /// </summary>
        /// <param name="hwnd">指定窗口句柄</param>
        /// <param name="ID">用于装载拥有那个窗口的一个进程的标识符</param>
        /// <returns>拥有窗口的线程的标识符</returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        /// <summary>
        /// 合成一次击键事件
        /// </summary>
        /// <param name="bVk">定义一个虚拟键码,键码值必须在1～254之间</param>
        /// <param name="bScan">定义该键的硬件扫描码,一般为0</param>
        /// <param name="dwFlags">这里的整数类型0为按下，2为释放</param>
        /// <param name="dwExtraInfo">这里是整数类型，一般情况下为0(定义与击键相关的附加的32位值)</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// 综合鼠标击键和鼠标动作（会真的移动鼠标）
        /// </summary>
        /// <param name="dwFlags">标志位集，指定点击按钮和鼠标动作的多种情况</param>
        /// <param name="dx">鼠标的x坐标</param>
        /// <param name="dy">鼠标的y坐标</param>
        /// <param name="dwData">如果dwFlags为MOUSEEVENTF_WHEEL，则dwData指定鼠标轮移动的数量</param>
        /// <param name="dwExtraInfo">
        /// <para>指定与鼠标事件相关的附加32位值</para>
        /// <para>(应用程序调用函数GetMessageExtraInfo来获得此附加信息)</para>
        /// </param>
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        /// <summary>
        /// <para>该函数将一个消息放入（寄送）到与指定窗口创建的线程相联系消息队列里，不等待线程</para>
        /// <para>处理消息就返回，是异步消息模式。消息队列里的消息通过调用GetMessage和PeekMessage取得。</para>
        /// </summary>
        /// <param name="hWnd"><para>接收消息的那个窗口的句柄。</para>
        /// <para>如设为HWND_BROADCAST，表示投递给系统中的所有顶级窗口。</para>
        /// <para>如设为零，表示投递一条线程消息（参考PostThreadMessage）</para></param>
        /// <param name="Msg">消息标识符</param>
        /// <param name="wParam">具体由消息决定</param>
        /// <param name="lParam">具体由消息决定</param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hDC"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// <para>注册系统热键</para>
        /// <para>如果函数执行成功，返回值非0；如果函数执行失败，返回值为0</para>
        /// <para>要得到扩展错误信息，调用GetLastError</para>
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// <para>该函数从当前线程中的窗口释放鼠标捕获，并恢复通常的鼠标输入处理。捕获鼠标的窗口接收所有</para>
        /// <para>的鼠标输入（无论光标的位置在哪里），除非点击鼠标键时，光标热点在另一个线程的窗口中。</para>
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        #region SendMessage

        /// <summary>
        /// <para>该函数将指定的消息发送到一个或多个窗口。</para>
        /// <para>此函数为指定的窗口调用窗口程序直到窗口程序处理完消息再返回。</para>
        /// <para>而函数PostMessage不同，将一个消息寄送到一个线程的消息队列后立即返回。</para>
        /// return 返回值 : 指定消息处理的结果，依赖于所发送的消息。
        /// </summary>
        /// <param name="hWnd">要接收消息的那个窗口的句柄</param>
        /// <param name="Msg">消息的标识符</param>
        /// <param name="wParam">具体取决于消息</param>
        /// <param name="lParam">具体取决于消息</param>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// <para>该函数将指定的消息发送到一个或多个窗口。</para>
        /// <para>此函数为指定的窗口调用窗口程序直到窗口程序处理完消息再返回。</para>
        /// <para>而函数PostMessage不同，将一个消息寄送到一个线程的消息队列后立即返回。</para>
        /// return 返回值 : 指定消息处理的结果，依赖于所发送的消息。
        /// </summary>
        /// <param name="hWnd">要接收消息的那个窗口的句柄</param>
        /// <param name="Msg">消息的标识符</param>
        /// <param name="wParam">具体取决于消息</param>
        /// <param name="lParam">具体取决于消息</param>
        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessageA")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// <para>该函数将指定的消息发送到一个或多个窗口。</para>
        /// <para>此函数为指定的窗口调用窗口程序直到窗口程序处理完消息再返回。</para>
        /// <para>而函数PostMessage不同，将一个消息寄送到一个线程的消息队列后立即返回。</para>
        /// return 返回值 : 指定消息处理的结果，依赖于所发送的消息。
        /// </summary>
        /// <param name="hWnd">要接收消息的那个窗口的句柄</param>
        /// <param name="msg">消息的标识符</param>
        /// <param name="wParam">具体取决于消息</param>
        /// <param name="lParam">具体取决于消息</param>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);

        #endregion

        /// <summary>
        /// <para>该函数把光标移到屏幕的指定位置。</para>
        /// <para>如果新位置不在由 ClipCursor函数设置的屏幕矩形区域之内，</para>
        /// <para>则系统自动调整坐标，使得光标在矩形之内。</para>
        /// <para>返回值：如果成功，返回非零值；如果失败，返回值是零。</para>
        /// </summary>
        /// <param name="lpPoint">鼠标指针在屏幕像素坐标系统中的X，Y位置</param>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(POINT lpPoint);

        /// <summary>
        /// <para>该函数把光标移到屏幕的指定位置。</para>
        /// <para>如果新位置不在由 ClipCursor函数设置的屏幕矩形区域之内，</para>
        /// <para>则系统自动调整坐标，使得光标在矩形之内。</para>
        /// <para>返回值：如果成功，返回非零值；如果失败，返回值是零。</para>
        /// </summary>
        /// <param name="x">鼠标指针在屏幕像素坐标系统中的X位置</param>
        /// <param name="y">鼠标指针在屏幕像素坐标系统中的Y位置</param>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        /// <summary>
        /// 设置窗口在屏幕中的位置
        /// </summary>
        /// <param name="hWnd">指定窗口句柄</param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// 设置窗口的区域的窗口。窗口区域决定在窗户上的地区——该系统允许绘画。
        /// 该系统不显示任何部分是一个窗口,窗户外面地区
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hRgn">处理区域</param>
        /// <param name="bRedraw">重绘窗体选项</param>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hdcDst"></param>
        /// <param name="pptDst"></param>
        /// <param name="psize"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="ppSrc"></param>
        /// <param name="crKey"></param>
        /// <param name="pblend"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT ppSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        /// <summary>
        /// 该函数获得包含指定点的窗口的句柄。
        /// </summary>
        /// <param name="Point">Point指定一个被检测的点，该点为struct类型</param>
        /// <returns>返回值为包含该点的窗口的句柄。如果包含指定点的窗口不存在，返回值为NULL</returns>
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        #region hook
        /// <summary>
        ///     <para>该函数将一个应用程序定义的挂钩处理过程安装到挂钩链中去,您可以</para>
        ///     <para>通过安装挂钩处理过程来对系统的某些类型事件进行监控,这些事件与</para>
        ///     <para>某个特定的线程或系统中的所有事件相关.</para>
        ///     <para>返回值:若此函数执行成功,则返回值就是该挂钩处理过程的句柄;</para>
        ///     <para>若此函数执行失败,则返回值为NULL(0).</para>
        /// </summary>
        /// <param name="idHook">指示准备被安装的挂钩处理过程之类型，(详细：)</param>
        /// <param name="lpfn"> 
        ///     <para>指向相应的挂钩处理过程.若参数dwThreadId为0或者指示了一个其他进程</para>
        ///     <para>创建的线程之标识符,则参数lpfn必须指向一个动态链接中的挂钩处理过</para>
        ///     <para>程.否则,参数lpfn可以指向一个与当前进程相关的代码中定义的挂钩处理过程</para>
        /// </param>
        /// <param name="hInstance">
        ///     <para>指示了一个动态链接的句柄,该动态连接库包含了参数lpfn 所指向的挂钩</para>
        ///     <para>处理过程.若参数dwThreadId指示的线程由当前进程创建,并且相应的挂钩处</para>
        ///     <para>理过程定义于当前进程相关的代码中,则参数hMod必须被设置为NULL(0).</para>
        /// </param>
        /// <param name="threadId">
        ///     <para>指示了一个线程标识符,挂钩处理过程与线程相关.</para>
        ///     <para>若此参数值为0,则该挂钩处理过程与所有现存的线程相关.</para>
        /// </param>
        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(HookType idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        /// <summary>
        /// 卸载钩子
        /// <para>返回值：如果函数成功，返回值为非零值。</para>
        /// <para>如果函数失败，返回值为零。</para>
        /// </summary>
        /// <param name="idHook">要删除的钩子的句柄。这个参数是上一个函数SetWindowsHookEx的返回值.</param>
        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// <para>调用下一个钩子</para>
        /// <para>会返回下一个钩子执行后的返回值; 0 表示失败</para>
        /// </summary>
        /// <param name="idHook">当前钩子的句柄</param>
        /// <param name="nCode">钩子代码; 就是给下一个钩子要交待的</param>
        /// <param name="wParam">要传递的参数; 由钩子类型决定是什么参数</param>
        /// <param name="lParam">要传递的参数; 由钩子类型决定是什么参数</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        #endregion

        #endregion

        #region gdi32.dll
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        #endregion

        #region Other Methods
        /// <summary>
        /// 创建一个无符号的32位值作为lParam参数中使用一个消息
        /// </summary>
        public static int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        /// <summary>
        /// 获取GetLastError函数返回值对应的字符串
        /// </summary>
        /// <param name="code">GetLastError函数的返回值</param>
        public static string GetLastErrorString(int code)
        {
            switch (code)
            {
                case 0: return "操作成功完成";
                case 1: return "功能错误";
                case 2: return "系统找不到指定的文件";
                case 3: return "系统找不到指定的路径";
                case 4: return "系统无法打开文件";
                case 5: return "拒绝访问";
                case 6: return "句柄无效";
                case 7: return "存储控制块被损坏";
                case 8: return "存储空间不足，无法处理此命令";
                case 9: return "存储控制块地址无效";
                case 10: return "环境错误";
                case 11: return "试图加载格式错误的程序";
                case 12: return "访问码无效";
                case 13: return "数据无效";
                case 14: return "存储器不足，无法完成此操作";
                case 15: return "系统找不到指定的驱动器";
                case 16: return "无法删除目录";
                case 17: return "系统无法将文件移到不同的驱动器";
                case 18: return "没有更多文件";
                case 19: return "介质受写入保护";
                case 20: return "系统找不到指定的设备";
                case 21: return "设备未就绪";
                case 22: return "设备不识别此命令";
                case 23: return "数据错误 (循环冗余检查)";
                case 24: return "程序发出命令，但命令长度不正确";
                case 25: return "驱动器无法找出磁盘上特定区域或磁道的位置";
                case 26: return "无法访问指定的磁盘或软盘";
                case 27: return "驱动器找不到请求的扇区";
                case 28: return "打印机缺纸";
                case 29: return "系统无法写入指定的设备";
                case 30: return "系统无法从指定的设备上读取";
                case 31: return "连到系统上的设备没有发挥作用";
                case 32: return "进程无法访问文件，因为另一个程序正在使用此文件";
                case 33: return "进程无法访问文件，因为另一个程序已锁定文件的一部分";
                case 36: return "用来共享的打开文件过多";
                case 38: return "到达文件结尾";
                case 39: return "磁盘已满";
                case 50: return "不支持网络请求";
                case 51: return "远程计算机不可用 ";
                case 52: return "在网络上已有重复的名称";
                case 53: return "找不到网络路径";
                case 54: return "网络忙";
                case 55: return "指定的网络资源或设备不再可用";
                case 56: return "已到达网络 BIOS 命令限制";
                case 57: return "网络适配器硬件出错";
                case 58: return "指定的服务器无法运行请求的操作";
                case 59: return "发生意外的网络错误";
                case 60: return "远程适配器不兼容";
                case 61: return "打印机队列已满";
                case 62: return "无法在服务器上获得用于保存待打印文件的空间";
                case 63: return "删除等候打印的文件";
                case 64: return "指定的网络名不再可用";
                case 65: return "拒绝网络访问";
                case 66: return "网络资源类型错误";
                case 67: return "找不到网络名";
                case 68: return "超过本地计算机网卡的名称限制";
                case 69: return "超出网络 BIOS 会话限制";
                case 70: return "远程服务器已暂停，或正在启动过程中";
                case 71: return "当前已无法再同此远程计算机连接，因为已达到计算机的连接数目极限";
                case 72: return "已暂停指定的打印机或磁盘设备";
                case 80: return "文件存在";
                case 82: return "无法创建目录或文件";
                case 83: return "INT 24 失败";
                case 84: return "无法取得处理此请求的存储空间";
                case 85: return "本地设备名已在使用中";
                case 86: return "指定的网络密码错误";
                case 87: return "参数错误";
                case 88: return "网络上发生写入错误";
                case 89: return "系统无法在此时启动另一个进程";
                case 100: return "无法创建另一个系统信号灯";
                case 101: return "另一个进程拥有独占的信号灯";
                case 102: return "已设置信号灯且无法关闭";
                case 103: return "无法再设置信号灯";
                case 104: return "无法在中断时请求独占的信号灯";
                case 105: return "此信号灯的前一个所有权已结束";
                case 107: return "程序停止，因为替代的软盘未插入";
                case 108: return "磁盘在使用中，或被另一个进程锁定";
                case 109: return "管道已结束";
                case 110: return "系统无法打开指定的设备或文件";
                case 111: return "文件名太长";
                case 112: return "磁盘空间不足";
                case 113: return "无法再获得内部文件的标识";
                case 114: return "目标内部文件的标识不正确";
                case 117: return "应用程序制作的 IOCTL 调用错误";
                case 118: return "验证写入的切换参数值错误";
                case 119: return "系统不支持请求的命令";
                case 120: return "此功能只被此系统支持";
                case 121: return "信号灯超时时间已到";
                case 122: return "传递到系统调用的数据区太小";
                case 123: return "文件名、目录名或卷标语法不正确";
                case 124: return "系统调用级别错误";
                case 125: return "磁盘没有卷标";
                case 126: return "找不到指定的模块";
                case 127: return "找不到指定的程序";
                case 128: return "没有等候的子进程";
                case 130: return "试图使用操作(而非原始磁盘 I/O)的已打开磁盘分区的文件句柄";
                case 131: return "试图移动文件指针到文件开头之前";
                case 132: return "无法在指定的设备或文件上设置文件指针";
                case 133: return "包含先前加入驱动器的驱动器无法使用 JOIN 或 SUBST 命令";
                case 134: return "试图在已被合并的驱动器上使用 JOIN 或 SUBST 命令";
                case 135: return "试图在已被合并的驱动器上使用 JOIN 或 SUBST 命令";
                case 136: return "系统试图解除未合并驱动器的 JOIN";
                case 137: return "系统试图解除未替代驱动器的 SUBST";
                case 138: return "系统试图将驱动器合并到合并驱动器上的目录";
                case 139: return "系统试图将驱动器替代为替代驱动器上的目录";
                case 140: return "系统试图将驱动器合并到替代驱动器上的目录";
                case 141: return "系统试图替代驱动器为合并驱动器上的目录";
                case 142: return "系统无法在此时运行 JOIN 或 SUBST";
                case 143: return "系统无法将驱动器合并到或替代为相同驱动器上的目录";
                case 144: return "目录并非根目录下的子目录";
                case 145: return "目录非空";
                case 146: return "指定的路径已在替代中使用";
                case 147: return "资源不足，无法处理此命令";
                case 148: return "指定的路径无法在此时使用";
                case 149: return "企图将驱动器合并或替代为驱动器上目录是上一个替代的目标的驱动器";
                case 150: return "系统跟踪信息未在 CONFIG.SYS 文件中指定，或不允许跟踪";
                case 151: return "为 DosMuxSemWait 指定的信号灯事件个数错误";
                case 152: return "DosMuxSemWait 不可运行。已设置过多的信号灯";
                case 153: return "DosMuxSemWait 清单错误";
                case 154: return "输入的卷标超过目标文件系统的长度限制";
                case 155: return "无法创建另一个线程";
                case 156: return "接收进程已拒绝此信号";
                case 157: return "段已被放弃且无法锁定";
                case 158: return "段已解除锁定";
                case 159: return "线程标识的地址错误";
                case 160: return "传递到 DosExecPgm 的参数字符串错误";
                case 161: return "指定的路径无效";
                case 162: return "信号已暂停";
                case 164: return "无法在系统中创建更多的线程";
                case 167: return "无法锁定文件区域";
                case 170: return "请求的资源在使用中";
                case 173: return "对于提供取消区域进行锁定的请求不明显";
                case 174: return "文件系统不支持锁定类型的最小单元更改";
                case 180: return "系统检测出错误的段号";
                case 183: return "当文件已存在时，无法创建该文件";
                case 186: return "传递的标志错误";
                case 187: return "找不到指定的系统信号灯名称";
                case 196: return "操作系统无法运行此应用程序";
                case 197: return "操作系统当前的配置不能运行此应用程序";
                case 199: return "操作系统无法运行此应用程序";
                case 200: return "代码段不可大于或等于 64K";
                case 203: return "操作系统找不到已输入的环境选项";
                case 205: return "命令子树中的进程没有信号处理程序";
                case 206: return "文件名或扩展名太长";
                case 207: return "第 2 环堆栈已被占用";
                case 208: return "没有正确输入文件名通配符 * 或 ?，或指定过多的文件名通配符";
                case 209: return "正在发送的信号错误";
                case 210: return "无法设置信号处理程序";
                case 212: return "段已锁定且无法重新分配";
                case 214: return "连到该程序或动态链接模块的动态链接模块太多";
                case 215: return "无法嵌套调用 LoadModule";
                case 230: return "管道状态无效";
                case 231: return "所有的管道实例都在使用中";
                case 232: return "管道正在关闭中";
                case 233: return "管道的另一端上无任何进程";
                case 234: return "更多数据可用";
                case 240: return "取消会话";
                case 254: return "指定的扩展属性名无效";
                case 255: return "扩展属性不一致";
                case 258: return "等待的操作过时";
                case 259: return "没有可用的数据了";
                case 266: return "无法使用复制功能";
                case 267: return "目录名无效";
                case 275: return "扩展属性在缓冲区中不适用";
                case 276: return "装在文件系统上的扩展属性文件已损坏";
                case 277: return "扩展属性表格文件已满";
                case 278: return "指定的扩展属性句柄无效";
                case 282: return "装入的文件系统不支持扩展属性";
                case 288: return "企图释放并非呼叫方所拥有的多用户终端运行程序";
                case 298: return "发向信号灯的请求过多";
                case 299: return "仅完成部分的 ReadProcessMemoty 或 WriteProcessMemory 请求";
                case 300: return "操作锁定请求被拒绝";
                case 301: return "系统接收了一个无效的操作锁定确认";
                case 487: return "试图访问无效的地址";
                case 534: return "算术结果超过 32 位";
                case 535: return "管道的另一端有一进程";
                case 536: return "等候打开管道另一端的进程";
                case 994: return "拒绝访问扩展属性";
                case 995: return "由于线程退出或应用程序请求，已放弃 I/O 操作";
                case 996: return "重叠 I/O 事件不在信号状态中";
                case 997: return "重叠 I/O 操作在进行中";
                case 998: return "内存分配访问无效";
                case 999: return "错误运行页内操作";
                case 1001: return "递归太深；栈溢出";
                case 1002: return "窗口无法在已发送的消息上操作";
                case 1003: return "无法完成此功能";
                case 1004: return "无效标志";
                case 1005: return "此卷不包含可识别的文件系统。请确定所有请求的文件系统驱动程序已加载，且此卷未损坏";
                case 1006: return "文件所在的卷已被外部改变，因此打开的文件不再有效";
                case 1007: return "无法在全屏幕模式下运行请求的操作";
                case 1008: return "试图引用不存在的令牌";
                case 1009: return "配置注册表数据库损坏";
                case 1010: return "配置注册表项无效";
                case 1011: return "无法打开配置注册表项";
                case 1012: return "无法读取配置注册表项";
                case 1013: return "无法写入配置注册表项";
                case 1014: return "注册表数据库中的某一文件必须使用记录或替代复制来恢复。恢复成功完成";
                case 1015: return "注册表损坏。包含注册表数据的某一文件结构损坏，或系统的文件内存映像损坏，或因为替代副本、日志缺少或损坏而无法恢复文件";
                case 1016: return "由注册表启动的 I/O 操作恢复失败。注册表无法读入、写出或清除任意一个包含注册表系统映像的文件";
                case 1017: return "系统试图加载或还原文件到注册表，但指定的文件并非注册表文件格式";
                case 1018: return "试图在标记为删除的注册表项上运行不合法的操作";
                case 1019: return "系统无法配置注册表日志中所请求的空间";
                case 1020: return "无法在已有子项或值的注册表项中创建符号链接";
                case 1021: return "无法在易变父项下创建稳定子项";
                case 1022: return "通知更改请求正在完成中，且信息并未返回到呼叫方的缓冲区中。当前呼叫方必须枚举文件来查找更改";
                case 1051: return "已发送停止控制到服务，该服务被其它正在运行的服务所依赖";
                case 1052: return "请求的控件对此服务无效";
                case 1053: return "服务并未及时响应启动或控制请求";
                case 1054: return "无法创建此服务的线程";
                case 1055: return "锁定服务数据库";
                case 1056: return "服务的实例已在运行中";
                case 1057: return "帐户名无效或不存在，或者密码对于指定的帐户名无效";
                case 1058: return "无法启动服务，原因可能是它被禁用或与它相关联的设备没有启动";
                case 1059: return "指定了循环服务依存";
                case 1060: return "指定的服务并未以已安装的服务存在";
                case 1061: return "服务无法在此时接受控制信息";
                case 1062: return "服务未启动";
                case 1063: return "服务进程无法连接到服务控制器上";
                case 1064: return "当处理控制请求时，在服务中发生异常";
                case 1065: return "指定的数据库不存在";
                case 1066: return "服务已返回特定的服务错误码";
                case 1067: return "进程意外终止";
                case 1068: return "依存服务或组无法启动";
                case 1069: return "由于登录失败而无法启动服务";
                case 1070: return "启动后，服务停留在启动暂停状态";
                case 1071: return "指定的服务数据库锁定无效";
                case 1072: return "指定的服务已标记为删除";
                case 1073: return "指定的服务已存在";
                case 1074: return "系统当前以最新的有效配置运行";
                case 1075: return "依存服务不存在，或已被标记为删除";
                case 1076: return "已接受使用当前引导作为最后的有效控制设置";
                case 1077: return "上次启动之后，仍未尝试引导服务";
                case 1078: return "名称已用作服务名或服务显示名";
                case 1079: return "此服务的帐户不同于运行于同一进程上的其它服务的帐户";
                case 1080: return "只能为 Win32 服务设置失败操作，不能为驱动程序设置";
                case 1081: return "这个服务所运行的处理和服务控制管理器相同。所以，如果服务处理程序意外中止的话，服务控制管理器无法进行任何操作";
                case 1082: return "这个服务尚未设置恢复程序";
                case 1083: return "配置成在该可执行程序中运行的这个服务不能执行该服务";
                case 1100: return "已达磁带的实际结尾";
                case 1101: return "磁带访问已达文件标记";
                case 1102: return "已达磁带或磁盘分区的开头";
                case 1103: return "磁带访问已达一组文件的结尾";
                case 1104: return "磁带上不再有任何数据";
                case 1105: return "磁带无法分区";
                case 1106: return "在访问多卷分区的新磁带时，当前的块大小不正确";
                case 1107: return "当加载磁带时，找不到分区信息";
                case 1108: return "无法锁定媒体弹出功能";
                case 1109: return "无法卸载介质";
                case 1110: return "驱动器中的介质可能已更改";
                case 1111: return "复位 I/O 总线";
                case 1112: return "驱动器中没有媒体";
                case 1113: return "在多字节的目标代码页中，没有此 Unicode 字符可以映射到的字符";
                case 1114: return "动态链接库 (DLL) 初始化例程失败";
                case 1115: return "系统关机正在进行";
                case 1116: return "因为没有任何进行中的关机过程，所以无法中断系统关机";
                case 1117: return "因为 I/O 设备错误，所以无法运行此项请求";
                case 1118: return "没有串行设备被初始化成功。串行驱动程序将卸载";
                case 1119: return "无法打开正在与其他设备共享中断请求(IRQ)的设备。至少有一个使用该 IRQ 的其他设备已打开";
                case 1120: return "序列 I/O 操作已由另一个串行口的写入完成。(IOCTL_SERIAL_XOFF_COUNTER 已达零";
                case 1121: return "因为已过超时时间，所以串行 I/O 操作完成。(IOCTL_SERIAL_XOFF_COUNTER 未达零";
                case 1122: return "在软盘上找不到 ID 地址标记";
                case 1123: return "软盘扇区 ID 字符域与软盘控制器磁道地址不相符";
                case 1124: return "软盘控制器报告软盘驱动程序不能识别的错误";
                case 1125: return "软盘控制器返回与其寄存器中不一致的结果";
                case 1126: return "当访问硬盘时，重新校准操作失败，重试仍然失败";
                case 1127: return "当访问硬盘时，磁盘操作失败，重试仍然失败";
                case 1128: return "当访问硬盘时，即使失败，仍须复位磁盘控制器";
                case 1129: return "已达磁带结尾";
                case 1130: return "服务器存储空间不足，无法处理此命令";
                case 1131: return "检测出潜在的死锁状态";
                case 1132: return "指定的基址或文件偏移量没有适当对齐";
                case 1140: return "改变系统供电状态的尝试被另一应用程序或驱动程序否决";
                case 1141: return "系统 BIOS 改变系统供电状态的尝试失败";
                case 1142: return "试图在一文件上创建超过系统允许数额的链接";
                case 1150: return "指定程序要求更新的 Windows 版本";
                case 1151: return "指定程序不是 Windows 或 MS-DOS 程序";
                case 1152: return "只能启动该指定程序的一个实例";
                case 1153: return "该指定程序适用于旧的 Windows 版本";
                case 1154: return "执行该应用程序所需的库文件之一被损坏";
                case 1155: return "没有应用程序与此操作的指定文件有关联";
                case 1156: return "在输送指令到应用程序的过程中出现错误";
                case 1157: return "执行该应用程序所需的库文件之一无法找到";
                case 1158: return "当前程序已使用了 Window 管理器对象的系统允许的所有句柄";
                case 1159: return "消息只能与同步操作一起使用";
                case 1160: return "指出的源元素没有媒体";
                case 1161: return "指出的目标元素已包含媒体";
                case 1162: return "指出的元素不存在";
                case 1163: return "指出的元素是未显示的存储资源的一部分";
                case 1164: return "显示设备需要重新初始化，因为硬件有错误";
                case 1165: return "设备显示在尝试进一步操作之前需要清除";
                case 1166: return "设备显示它的门仍是打开状态";
                case 1167: return "设备没有连接";
                case 1168: return "找不到元素";
                case 1169: return "索引中没有同指定项相匹配的项";
                case 1170: return "在对象上不存在指定的属性集";
                case 1171: return "传递到 GetMouseMovePoints 的点不在缓冲区中";
                case 1172: return "跟踪(工作站)服务没运行";
                case 1173: return "找不到卷 ID";
                case 1175: return "无法删除要被替换的文件";
                case 1176: return "无法将替换文件移到要被替换的文件。要被替换的文件保持原来的名称";
                case 1177: return "无法将替换文件移到要被替换的文件。要被替换的文件已被重新命名为备份名称";
                case 1178: return "卷更改记录被删除";
                case 1179: return "卷更改记录服务不处于活动中";
                case 1180: return "找到一份文件，但是可能不是正确的文件";
                case 1181: return "日志项从日志中被删除";
                case 1200: return "指定的设备名无效";
                case 1201: return "设备当前未连接上，但其为一个记录连接";
                case 1202: return "企图记录先前已被记录的设备";
                case 1203: return "无任何网络提供程序接受指定的网络路径";
                case 1204: return "指定的网络提供程序名称无效";
                case 1205: return "无法打开网络连接配置文件";
                case 1206: return "网络连接配置文件损坏";
                case 1207: return "无法枚举空载体";
                case 1208: return "发生扩展错误";
                case 1209: return "指定的组名格式无效";
                case 1210: return "指定的计算机名格式无效";
                case 1211: return "指定的事件名格式无效";
                case 1212: return "指定的域名格式无效";
                case 1213: return "指定的服务名格式无效";
                case 1214: return "指定的网络名格式无效";
                case 1215: return "指定的共享名格式无效";
                case 1216: return "指定的密码格式无效";
                case 1217: return "指定的消息名格式无效";
                case 1218: return "指定的消息目标格式无效";
                case 1219: return "提供的凭据与已存在的凭据集冲突";
                case 1220: return "企图创建网络服务器的会话，但已对该服务器创建过多的会话";
                case 1221: return "工作组或域名已由网络上的另一部计算机使用";
                case 1222: return "网络未连接或启动";
                case 1223: return "操作已被用户取消";
                case 1224: return "请求的操作无法在使用用户映射区域打开的文件上执行";
                case 1225: return "远程系统拒绝网络连接";
                case 1226: return "网络连接已被适当地关闭了";
                case 1227: return "网络传输终结点已有与其关联的地址";
                case 1228: return "地址仍未与网络终结点关联";
                case 1229: return "企图在不存在的网络连接上进行操作";
                case 1230: return "企图在使用中的网络连接上进行无效的操作";
                case 1231: return "不能访问网络位置。有关网络排除故障的信息，请参阅 Windows 帮助";
                case 1232: return "不能访问网络位置。有关网络排除故障的信息，请参阅 Windows 帮助";
                case 1233: return "不能访问网络位置。有关网络排除故障的信息，请参阅 Windows 帮助";
                case 1234: return "没有任何服务正在远程系统上的目标网络终结点上操作";
                case 1235: return "请求被终止";
                case 1236: return "由本地系统终止网络连接";
                case 1237: return "操作无法完成。应该重试";
                case 1238: return "因为已达到此帐户的最大同时连接数限制，所以无法连接服务器";
                case 1239: return "试图在这个帐户未被授权的时间内登录";
                case 1240: return "此帐户并未得到从这个工作站登录的授权";
                case 1241: return "请求的操作不能使用这个网络地址";
                case 1242: return "服务器已经注册";
                case 1243: return "指定的服务不存在";
                case 1244: return "因为用户还未被验证，不能执行所要求的操作";
                case 1245: return "因为用户还未登录网络，不能执行所要求的操作。指定的服务不存在";
                case 1246: return "正在继续工作";
                case 1247: return "试图进行初始操作，但是初始化已完成";
                case 1248: return "没有更多的本地设备";
                case 1249: return "指定的站点不存在";
                case 1250: return "具有指定名称的域控制器已经存在";
                case 1251: return "只有连接到服务器上时，该操作才受支持";
                case 1252: return "即使没有改动，组策略框架也应该调用扩展";
                case 1253: return "指定的用户没有一个有效的配置文件";
                case 1254: return "Microsoft Small Business Server 不支持此操作";
                case 1300: return "并非所有被引用的特权都指派给呼叫方";
                case 1301: return "帐户名和安全标识间的某些映射未完成";
                case 1302: return "没有为该帐户特别设置系统配额限制";
                case 1303: return "没有可用的加密密钥。返回了一个已知加密密钥";
                case 1304: return "密码太复杂，无法转换成 LAN Manager 密码。返回的 LAN Manager 密码为空字符串";
                case 1305: return "修订级别未知";
                case 1306: return "表明两个修订级别是不兼容的";
                case 1307: return "这个安全标识不能指派为此对象的所有者";
                case 1308: return "这个安全标识不能指派为对象的主要组";
                case 1309: return "当前并未模拟客户的线程试图操作模拟令牌";
                case 1310: return "组可能未被禁用";
                case 1311: return "当前没有可用的登录服务器来服务登录请求";
                case 1312: return "指定的登录会话不存在。可能已被终止";
                case 1313: return "指定的特权不存在";
                case 1314: return "客户没有所需的特权";
                case 1315: return "提供的名称并非正确的帐户名形式";
                case 1316: return "指定的用户已存在";
                case 1317: return "指定的用户不存在";
                case 1318: return "指定的组已存在";
                case 1319: return "指定的组不存在";
                case 1320: return "指定的用户帐户已是指定组的成员，或是因为组包含成员所以无法删除指定的组";
                case 1321: return "指定的用户帐户不是指定组帐户的成员";
                case 1322: return "无法禁用或删除最后剩余的系统管理帐户";
                case 1323: return "无法更新密码。提供作为当前密码的值不正确";
                case 1324: return "无法更新密码。提供给新密码的值包含密码中不允许的值";
                case 1325: return "无法更新密码。为新密码提供的值不符合字符域的长度、复杂性或历史要求";
                case 1326: return "登录失败: 未知的用户名或错误密码";
                case 1327: return "登录失败: 用户帐户限制";
                case 1328: return "登录失败: 违反帐户登录时间限制";
                case 1329: return "登录失败: 不允许用户登录到此计算机";
                case 1330: return "登录失败: 指定的帐户密码已过期";
                case 1331: return "登录失败: 禁用当前的帐户";
                case 1332: return "帐户名与安全标识间无任何映射完成";
                case 1333: return "一次请求过多的本地用户标识符(LUIDs)";
                case 1334: return "无更多可用的本地用户标识符(LUIDs)";
                case 1335: return "对于该特别用法，安全 ID 的次级授权部分无效";
                case 1336: return "访问控制列表(ACL)结构无效";
                case 1337: return "安全 ID 结构无效";
                case 1338: return "安全描述符结构无效";
                case 1340: return "无法创建固有的访问控制列表(ACL)或访问控制项目(ACE)";
                case 1341: return "服务器当前已禁用";
                case 1342: return "服务器当前已启用";
                case 1343: return "提供给识别代号颁发机构的值为无效值";
                case 1344: return "无更多可用的内存以更新安全信息";
                case 1345: return "指定属性无效，或与整个群体的属性不兼容";
                case 1346: return "指定的模拟级别无效， 或所提供的模拟级别无效";
                case 1347: return "无法打开匿名级安全令牌";
                case 1348: return "请求的验证信息类别无效";
                case 1349: return "令牌的类型对其尝试使用的方法不适当";
                case 1350: return "无法在与安全性无关联的对象上运行安全性操作";
                case 1351: return "未能从域控制器读取配置信息，或者是因为机器不可使用，或者是访问被拒绝";
                case 1352: return "安全帐户管理器(SAM)或本地安全颁发机构(LSA)服务器处于运行安全操作的错误状态";
                case 1353: return "域处于运行安全操作的错误状态";
                case 1354: return "此操作只对域的主要域控制器可行";
                case 1355: return "指定的域不存在，或无法联系";
                case 1356: return "指定的域已存在";
                case 1357: return "试图超出每服务器域个数的限制";
                case 1358: return "无法完成请求操作，因为磁盘上的严重介质失败或数据结构损坏";
                case 1359: return "出现了内部错误";
                case 1360: return "通用访问类型包含于已映射到非通用类型的访问掩码中";
                case 1361: return "安全描述符格式不正确 (绝对或自相关的)";
                case 1362: return "请求操作只限制在登录进程中使用。调用进程未注册为一个登录进程";
                case 1363: return "无法使用已在使用中的标识启动新的会话";
                case 1364: return "未知的指定验证数据包";
                case 1365: return "登录会话并非处于与请求操作一致的状态中";
                case 1366: return "登录会话标识已在使用中";
                case 1367: return "登录请求包含无效的登录类型值";
                case 1368: return "在使用命名管道读取数据之前，无法经由该管道模拟";
                case 1369: return "注册表子树的事务处理状态与请求状态不一致";
                case 1370: return "安全性数据库内部出现损坏";
                case 1371: return "无法在内置帐户上运行此操作";
                case 1372: return "无法在内置特殊组上运行此操作";
                case 1373: return "无法在内置特殊用户上运行此操作";
                case 1374: return "无法从组中删除用户，因为当前组为用户的主要组";
                case 1375: return "令牌已作为主要令牌使用";
                case 1376: return "指定的本地组不存在";
                case 1377: return "指定的帐户名不是本地组的成员";
                case 1378: return "指定的帐户名已是本地组的成员";
                case 1379: return "指定的本地组已存在";
                case 1380: return "登录失败: 未授予用户在此计算机上的请求登录类型";
                case 1381: return "已超过在单一系统中可保存机密的最大个数";
                case 1382: return "机密的长度超过允许的最大长度";
                case 1383: return "本地安全颁发机构数据库内部包含不一致性";
                case 1384: return "在尝试登录的过程中，用户的安全上下文积累了过多的安全标识";
                case 1385: return "登录失败: 未授予用户在此计算机上的请求登录类型";
                case 1386: return "更改用户密码时需要交叉加密密码";
                case 1387: return "由于成员不存在，无法将成员添加到本地组中，也无法从本地组将其删除";
                case 1388: return "无法将新成员加入到本地组中，因为成员的帐户类型错误";
                case 1389: return "已指定过多的安全标识";
                case 1390: return "更改此用户密码时需要交叉加密密码";
                case 1391: return "表明 ACL 未包含任何可承继的组件";
                case 1392: return "文件或目录损坏且无法读取";
                case 1393: return "磁盘结构损坏且无法读取";
                case 1394: return "无任何指定登录会话的用户会话项";
                case 1395: return "正在访问的服务有连接数目标授权限制。这时候已经无法再连接，原因是已经到达可接受的连接数目上限";
                case 1396: return "登录失败: 该目标帐户名称不正确";
                case 1397: return "相互身份验证失败。该服务器在域控制器的密码过期";
                case 1398: return "在客户机和服务器之间有一个时间差";
                case 1400: return "无效的窗口句柄";
                case 1401: return "无效的菜单句柄";
                case 1402: return "无效的光标句柄";
                case 1403: return "无效的加速器表句柄";
                case 1404: return "无效的挂钩句柄";
                case 1405: return "无效的多重窗口位置结构句柄";
                case 1406: return "无法创建最上层子窗口";
                case 1407: return "找不到窗口类别";
                case 1408: return "无效窗口；它属于另一线程";
                case 1409: return "热键已注册";
                case 1410: return "类别已存在";
                case 1411: return "类别不存在";
                case 1412: return "类别仍有打开的窗口";
                case 1413: return "无效索引";
                case 1414: return "无效的图标句柄";
                case 1415: return "使用专用 DIALOG 窗口字";
                case 1416: return "找不到列表框标识";
                case 1417: return "找不到通配字符";
                case 1418: return "线程没有打开的剪贴板";
                case 1419: return "没有注册热键";
                case 1420: return "窗口不是合法的对话窗口";
                case 1421: return "找不到控件 ID";
                case 1422: return "因为没有编辑控制，所以组合框的消息无效";
                case 1423: return "窗口不是组合框";
                case 1424: return "高度必须小于 256";
                case 1425: return "无效的设备上下文(DC)句柄";
                case 1426: return "无效的挂接程序类型";
                case 1427: return "无效的挂接程序";
                case 1428: return "没有模块句柄无法设置非本机的挂接";
                case 1429: return "此挂接程序只可整体设置";
                case 1430: return "Journal Hook 程序已安装";
                case 1431: return "挂接程序尚未安装";
                case 1432: return "单一选择列表框的无效消息";
                case 1433: return "LB_SETCOUNT 发送到非被动的列表框";
                case 1434: return "此列表框不支持 Tab 键宽度";
                case 1435: return "无法毁坏由另一个线程创建的对象";
                case 1436: return "子窗口没有菜单";
                case 1437: return "窗口没有系统菜单";
                case 1438: return "无效的消息对话框样式";
                case 1439: return "无效的系统范围内的 (SPI_*) 参数";
                case 1440: return "已锁定屏幕";
                case 1441: return "多重窗口位置结构中窗口的所有句柄必须具有相同的上层";
                case 1442: return "窗口不是子窗口";
                case 1443: return "无效的 GW_* 命令";
                case 1444: return "无效的线程标识";
                case 1445: return "无法处理非多重文档界面 (MDI) 窗口中的消息";
                case 1446: return "弹出式菜单已经激活";
                case 1447: return "窗口没有滚动条";
                case 1448: return "滚动条范围不可大于 MAXLONG";
                case 1449: return "无法以指定的方式显示或删除窗口";
                case 1450: return "系统资源不足，无法完成请求的服务";
                case 1451: return "系统资源不足，无法完成请求的服务";
                case 1452: return "系统资源不足，无法完成请求的服务";
                case 1453: return "配额不足，无法完成请求的服务";
                case 1454: return "配额不足，无法完成请求的服务";
                case 1455: return "页面文件太小，无法完成操作";
                case 1456: return "找不到菜单项";
                case 1457: return "键盘布局句柄无效";
                case 1458: return "不允许使用挂钩类型";
                case 1459: return "该操作需要交互式窗口工作站";
                case 1460: return "由于超时时间已过，该操作返回";
                case 1461: return "无效监视器句柄";
                case 1500: return "事件日志文件损坏";
                case 1501: return "无法打开事件日志文件，事件日志服务没有启动";
                case 1502: return "事件日志文件已满";
                case 1503: return "事件日志文件已在读取间更改";
                case 1601: return "无法访问 Windows 安装服务。请与技术支持人员联系，确认 Windows 安装服务是否注册正确";
                case 1602: return "用户取消了安装";
                case 1603: return "安装时发生严重错误";
                case 1604: return "安装已挂起，未完成";
                case 1605: return "这个操作只对当前安装的产品有效";
                case 1606: return "功能 ID 未注册";
                case 1607: return "组件 ID 并未注册";
                case 1608: return "未知属性";
                case 1609: return "句柄处于不正确的状态";
                case 1610: return "这个产品的配置数据已损坏。请与技术支持人员联系";
                case 1611: return "组件限制语不存在";
                case 1612: return "这个产品的安装来源无法使用。请验证来源是否存在，是否可以访问";
                case 1613: return "Windows 安装服务无法安装这个安装程序包。您必须安装含有 Windows 安装服务新版本的 Windows Service Park";
                case 1614: return "没有卸载产品";
                case 1615: return "SQL 查询语法不正确或不被支持";
                case 1616: return "记录字符域不存在";
                case 1617: return "设备已被删除";
                case 1618: return "正在进行另一个安装操作。请在继续这个安装操作之前完成那个操作";
                case 1619: return "未能打开这个安装程序包。请验证程序包是否存在，是否可以访问；或者与应用程序供应商联系，验证这是否是有效的 Windows 安装服务程序包";
                case 1620: return "未能打开这个安装程序包。请与应用程序供应商联系，验证这是否是有效的 Windows 安装服务程序包";
                case 1621: return "启动 Windows 安装服务用户界面时有错误。请与技术支持人员联系";
                case 1622: return "打开安装日志文件的错误。请验证指定的日志文件位置是否存在，是否可以写入";
                case 1623: return "安装程序包的语言不受系统支持";
                case 1624: return "应用变换时的错误。请验证指定的变换路径是否有效";
                case 1625: return "系统策略禁止这个安装。请与系统管理员联系";
                case 1626: return "无法执行函数";
                case 1627: return "执行期间，函数出了问题";
                case 1628: return "指定了无效的或未知的表格";
                case 1629: return "提供的数据类型不对";
                case 1630: return "这个类型的数据不受支持";
                case 1631: return "Windows 安装服务未能启动。请与技术支持人员联系";
                case 1632: return "临时文件夹已满或无法使用。请验证临时文件夹是否存在，是否可以写入";
                case 1633: return "这个处理器类型不支持该安装程序包。请与产品供应商联系";
                case 1634: return "组件没有在这台计算机上使用";
                case 1635: return "无法打开修补程序包。请验证修补程序包是否存在，是否可以访问；或者与应用程序供应商联系，验证这是否是 Windows 安装服务的修补程序包";
                case 1636: return "无法打开修补程序包。请与应用程序供应商联系，验证这是否是 Windows 安装服务的修补程序包";
                case 1637: return "Windows 安装服务无法处理这个插入程序包。您必须安装含有 Windows 安装服务新版本的 Windows Service Pack";
                case 1638: return "已安装这个产品的另一个版本。这个版本的安装无法继续。要配置或删除这个产品的现有版本，请用“控制面板”上的“添加/删除程序”";
                case 1639: return "无效的命令行参数。有关详细的命令行帮助，请查阅 Windows 安装服务的 SDK";
                case 1640: return "在终端服务远程会话期间，只有管理员有添加、删除或配置服务器软件的权限。如果您要在服务器上安装或配置软件，请与网络管理员联系";
                case 1641: return "要求的操作已成功结束。要使改动生效，必须重新启动系统";
                case 1642: return "Windows 安装服务无法安装升级修补程序，因为被升级的程序可能会丢失或是升级修补程序可能更新此程序的一个不同版本。请确认要被升级的程序在您的计算机上且您的升级修补程序是正确的";
                case 1700: return "串绑定无效";
                case 1701: return "绑定句柄类型不正确";
                case 1702: return "绑定句柄无效";
                case 1703: return "不支持 RPC 协议序列";
                case 1704: return "RPC 协议序列无效";
                case 1705: return "字符串通用唯一标识 (UUID) 无效";
                case 1706: return "终结点格式无效";
                case 1707: return "网络地址无效";
                case 1708: return "找不到终结点";
                case 1709: return "超时值无效";
                case 1710: return "找不到对象通用唯一标识(UUID)";
                case 1711: return "对象通用唯一标识 (UUID)已注册";
                case 1712: return "类型通用唯一标识(UUID)已注册";
                case 1713: return "RPC 服务器已在侦听";
                case 1714: return "未登记任何协议序列";
                case 1715: return "RPC 服务器未在侦听";
                case 1716: return "未知的管理器类型";
                case 1717: return "未知的界面";
                case 1718: return "没有任何链接";
                case 1719: return "无任何协议顺序";
                case 1720: return "无法创建终结点";
                case 1721: return "资源不足，无法完成此操作";
                case 1722: return "RPC 服务器不可用";
                case 1723: return "RPC 服务器过忙以致无法完成此操作";
                case 1724: return "网络选项无效";
                case 1725: return "在此线程中，没有使用中的远程过程调用";
                case 1726: return "远程过程调用失败";
                case 1727: return "远程过程调用失败且未运行";
                case 1728: return "远程过程调用(RPC)协议出错";
                case 1730: return "RPC 服务器不支持传送语法";
                case 1732: return "不支持通用唯一标识(UUID)类型";
                case 1733: return "标记无效";
                case 1734: return "数组边界无效";
                case 1735: return "链接不包含项目名称";
                case 1736: return "名称语法无效";
                case 1737: return "不支持名称语法";
                case 1739: return "没有可用来创建通用唯一标识 (UUID)的网络地址";
                case 1740: return "终结点是一份备份";
                case 1741: return "未知的验证类型";
                case 1742: return "调用的最大个数太小";
                case 1743: return "字符串太长";
                case 1744: return "找不到 RPC 协议顺序";
                case 1745: return "过程号超出范围";
                case 1746: return "绑定不包含任何验证信息";
                case 1747: return "未知的验证服务";
                case 1748: return "未知的验证级别";
                case 1749: return "安全上下文无效";
                case 1750: return "未知的授权服务";
                case 1751: return "项目无效";
                case 1752: return "服务器终结点无法运行操作";
                case 1753: return "终结点映射表中无更多的可用终结点";
                case 1754: return "未导出任何界面";
                case 1755: return "项目名称不完整";
                case 1756: return "版本选项无效";
                case 1757: return "没有其他成员";
                case 1758: return "没有内容未导出";
                case 1759: return "接口没有找到";
                case 1760: return "项目已存在";
                case 1761: return "找不到项目";
                case 1762: return "无可用的名称服务";
                case 1763: return "网络地址族无效";
                case 1764: return "不支持请求的操作";
                case 1765: return "无可用的安全上下文以允许模拟";
                case 1766: return "远程过程调用(RPC)中发生内部错误";
                case 1767: return "RPC 服务器试图以零除整数";
                case 1768: return "RPC 服务器中发生地址错误";
                case 1769: return "RPC 服务器上的浮点操作导至以零做除数";
                case 1770: return "RPC 服务器上发生浮点下溢";
                case 1771: return "RPC 服务器上发生浮点上溢";
                case 1772: return "自动句柄绑定的可用 RPC 服务器列表已用完";
                case 1773: return "无法打开字符翻译表文件";
                case 1774: return "包含字符翻译表的文件少于512 字节";
                case 1775: return "在远程过程调用时，将空的上下文句柄从客户传递到主机";
                case 1777: return "在远程过程调用时，上下文句柄已更改";
                case 1778: return "传递到远程过程调用的绑定句柄不相符";
                case 1779: return "承接体无法获得远程过程调用句柄";
                case 1780: return "传递空引用指针到承接体";
                case 1781: return "列举值超出范围";
                case 1782: return "字节计数太小";
                case 1783: return "承接体接收到坏数据";
                case 1784: return "提供给请求操作的用户缓冲区无效";
                case 1785: return "磁盘媒体无法识别。可能未被格式化";
                case 1786: return "工作站没有信任机密";
                case 1787: return "服务器上的安全数据库没有此工作站信任关系的计算机帐户";
                case 1788: return "主域和受信域间的信任关系失败";
                case 1789: return "此工作站和主域间的信任关系失败";
                case 1790: return "网络登录失败";
                case 1791: return "此线程的远程过程调用已在进行中";
                case 1792: return "试图登录，但是网络登录服务没有启动";
                case 1793: return "用户帐户到期";
                case 1794: return "转发程序已被占用且无法卸载";
                case 1795: return "指定的打印机驱动程序已安装";
                case 1796: return "指定的端口未知";
                case 1797: return "未知的打印机驱动程序";
                case 1798: return "未知的打印机处理器";
                case 1799: return "指定的分隔页文件无效";
                case 1800: return "指定的优先级无效";
                case 1801: return "打印机名无效";
                case 1802: return "打印机已存在";
                case 1803: return "打印机命令无效";
                case 1804: return "指定的数据类型无效";
                case 1805: return "指定的环境无效";
                case 1806: return "没有更多的绑定";
                case 1807: return "所用帐户为域间信任帐户。请使用您的全局用户帐户或本地用户帐户来访问这台服务器";
                case 1808: return "所用帐户是一个计算机帐户。使用您的全局用户帐户或本地用户帐户来访问此服务器";
                case 1809: return "已使用的帐户为服务器信任帐户。使用您的全局用户帐户或本地用户帐户来访问此服务器";
                case 1810: return "指定域的名称或安全标识(SID)与该域的信任信息不一致";
                case 1811: return "服务器在使用中且无法卸载";
                case 1812: return "指定的映像文件不包含资源区域";
                case 1813: return "找不到映像文件中指定的资源类型";
                case 1814: return "找不到映像文件中指定的资源名";
                case 1815: return "找不到映像文件中指定的资源语言标识";
                case 1816: return "配额不足，无法处理此命令";
                case 1817: return "未登记任何界面";
                case 1818: return "远程过程调用被取消";
                case 1819: return "绑定句柄不包含所有需要的信息";
                case 1820: return "在远程过程调用过程中通讯失败";
                case 1821: return "不支持请求的验证级别";
                case 1822: return "未登记任何主名称";
                case 1823: return "指定的错误不是有效的 Windows RPC 错误码";
                case 1824: return "已配置一个只在这部计算机上有效的 UUID";
                case 1825: return "发生一个安全包特有的错误";
                case 1826: return "线程未取消";
                case 1827: return "无效的编码/解码句柄操作";
                case 1828: return "序列化包装的版本不兼容";
                case 1829: return "RPC 承接体的版本不兼容";
                case 1830: return "RPC 管道对象无效或已损坏";
                case 1831: return "试图在 RPC 管道物件上进行无效操作";
                case 1832: return "不被支持的 RPC 管道版本";
                case 1898: return "找不到该组成员";
                case 1899: return "无法创建终结点映射表数据库项";
                case 1900: return "对象通用唯一标识 (UUID) 为 nil UUID";
                case 1901: return "指定的时间无效";
                case 1902: return "指定的格式名称无效";
                case 1903: return "指定的格式大小无效";
                case 1904: return "指定的打印机句柄正等候在";
                case 1905: return "已删除指定的打印机";
                case 1906: return "打印机的状态无效";
                case 1907: return "在第一次登录之前，必须更改用户密码";
                case 1908: return "找不到此域的域控制器";
                case 1909: return "引用的帐户当前已锁定，且可能无法登录";
                case 1910: return "没有发现指定的此对象导出者";
                case 1911: return "没有发现指定的对象";
                case 1912: return "没有发现指定的对象解析器";
                case 1913: return "一些待发数据仍停留在请求缓冲区内";
                case 1914: return "无效的异步远程过程调用句柄";
                case 1915: return "这个操作的异步 RPC 调用句柄不正确";
                case 1916: return "RPC 管道对象已经关闭";
                case 1917: return "在 RPC 调用完成之前全部的管道都已处理完成";
                case 1918: return "没有其他可用的数据来自 RPC 管道";
                case 1919: return "这个机器没有可用的站点名";
                case 1920: return "系统无法访问此文件";
                case 1921: return "系统无法辨识文件名";
                case 1922: return "项目不是所要的类型";
                case 1923: return "无法将所有对象的 UUID 导出到指定的项";
                case 1924: return "无法将界面导出到指定的项";
                case 1925: return "无法添加指定的配置文件项";
                case 1926: return "无法添加指定的配置文件元素";
                case 1927: return "无法删除指定的配置文件元素";
                case 1928: return "无法添加组元素";
                case 1929: return "无法删除组元素";
                case 2000: return "无效的像素格式";
                case 2001: return "指定的驱动程序无效";
                case 2002: return "窗口样式或类别属性对此操作无效";
                case 2003: return "不支持请求的图元操作";
                case 2004: return "不支持请求的变换操作";
                case 2005: return "不支持请求的剪切操作";
                case 2010: return "指定的颜色管理模块无效";
                case 2011: return "制定的颜色文件配置无效";
                case 2012: return "找不到指定的标识";
                case 2013: return "找不到所需的标识";
                case 2014: return "指定的标识已经存在";
                case 2015: return "指定的颜色文件配置与任何设备都不相关";
                case 2016: return "找不到该指定的颜色文件配置";
                case 2017: return "指定的颜色空间无效";
                case 2018: return "图像颜色管理没有启动";
                case 2019: return "在删除该颜色传输时有一个错误";
                case 2020: return "该指定的颜色传输无效";
                case 2021: return "该指定的变换与位图的颜色空间不匹配";
                case 2022: return "该指定的命名颜色索引在配置文件中不存在";
                case 2102: return "没有安装工作站驱动程序";
                case 2103: return "无法定位服务器";
                case 2104: return "发生内部错误，网络无法访问共享内存段";
                case 2105: return "网络资源不足";
                case 2106: return "工作站不支持这项操作";
                case 2107: return "设备没有连接";
                case 2108: return "网络连接已成功，但需要提示用户输入一个不同于原始指定的密码";
                case 2114: return "没有启动服务器服务";
                case 2115: return "队列空";
                case 2116: return "设备或目录不存在";
                case 2117: return "无法在重定向的资源上执行这项操作";
                case 2118: return "名称已经共享";
                case 2119: return "服务器当前无法提供所需的资源";
                case 2121: return "额外要求的项目超过允许的上限";
                case 2122: return "对等服务只支持两个同时操作的用户 ";
                case 2123: return "API 返回缓冲区太小";
                case 2127: return "远程 API 错误";
                case 2131: return "打开或读取配置文件时出错";
                case 2136: return "发生一般网络错误";
                case 2137: return "工作站服务的状态不一致。重新启动工作站服务之前，请先重新启动计算机";
                case 2138: return "工作站服务没有启动";
                case 2139: return "所需信息不可用";
                case 2140: return "发生 Windows 2000 内部错误";
                case 2141: return "服务器没有设置事务处理";
                case 2142: return "远程服务器不支持请求的 API";
                case 2143: return "事件名无效";
                case 2144: return "网络上已经有此计算机名。请更名后重新启动";
                case 2146: return "配置信息中找不到指定的组件";
                case 2147: return "配置信息中找不到指定的参数";
                case 2149: return "配置文件中有一个命令行太长";
                case 2150: return "打印机不存在";
                case 2151: return "打印作业不存在";
                case 2152: return "打印机目标找不到";
                case 2153: return "打印机目标已经存在";
                case 2154: return "打印机队列已经存在";
                case 2155: return "无法添加其它的打印机";
                case 2156: return "无法添加其它的打印作业";
                case 2157: return "无法添加其它的打印机目标";
                case 2158: return "此打印机目标处于空闲中，不接受控制操作";
                case 2159: return "此“打印机目标请求”包含无效的控制函数";
                case 2160: return "打印处理程序没有响应";
                case 2161: return "后台处理程序没有运行";
                case 2162: return "打印目标当前的状况，无法执行这项操作";
                case 2163: return "打印机队列当前的状况，无法执行这项操作";
                case 2164: return "打印作业当前的状况，无法执行这项操作";
                case 2165: return "无法为后台处理程序分配内存";
                case 2166: return "设备驱动程序不存在";
                case 2167: return "打印处理程序不支持这种数据类型";
                case 2168: return "没有安装打印处理程序";
                case 2180: return "锁定服务数据库";
                case 2181: return "服务表已满";
                case 2182: return "请求的服务已经启动";
                case 2183: return "这项服务没有响应控制操作";
                case 2184: return "服务仍未启动";
                case 2185: return "服务名无效";
                case 2186: return "服务没有响应控制功能";
                case 2187: return "服务控制处于忙碌状态";
                case 2188: return "配置文件包含无效的服务程序名";
                case 2189: return "在当前的状况下无法控制服务";
                case 2190: return "服务异常终止";
                case 2191: return "这项服务无法接受请求的 \"暂停\" 或 \"停止\" 操作";
                case 2192: return "服务控制“计划程序”在“计划表”中找不到服务名";
                case 2193: return "无法读取服务控制计划程序管道";
                case 2194: return "无法创建新服务的线程";
                case 2200: return "此工作站已经登录到局域网";
                case 2201: return "工作站没有登录到局域网";
                case 2202: return "指定的用户名无效";
                case 2203: return "密码参数无效";
                case 2204: return "登录处理器没有添加消息别名";
                case 2205: return "登录处理器没有添加消息别名";
                case 2206: return "注销处理器没有删除消息别名";
                case 2207: return "注销处理器没有删除消息别名";
                case 2209: return "暂停网络登录";
                case 2210: return "中心登录服务器发生冲突";
                case 2211: return "服务器没有设置正确的用户路径";
                case 2212: return "加载或运行登录脚本时出错";
                case 2214: return "没有指定登录服务器，计算机的登录状态是单机操作";
                case 2215: return "登录服务器找不到";
                case 2216: return "此计算机已经有一个登录域";
                case 2217: return "登录服务器无法验证登录";
                case 2219: return "安全数据库找不到";
                case 2220: return "组名找不到";
                case 2221: return "用户名找不到";
                case 2222: return "资源名找不到";
                case 2223: return "组已经存在";
                case 2224: return "帐户已经存在";
                case 2225: return "资源使用权限清单已经存在";
                case 2226: return "此操作只能在该域的主域控制器上执行";
                case 2227: return "安全数据库没有启动";
                case 2228: return "用户帐户数据库中的名称太多";
                case 2229: return "磁盘 I/O 失败";
                case 2230: return "已经超过每个资源 64 个项目的限制";
                case 2231: return "不得删除带会话的用户";
                case 2232: return "上层目录找不到";
                case 2233: return "无法添加到安全数据库会话高速缓存段";
                case 2234: return "这项操作不能在此特殊的组上执行";
                case 2235: return "用户帐户数据库会话高速缓存没有记录此用户";
                case 2236: return "用户已经属于此组";
                case 2237: return "用户不属于此组";
                case 2238: return "此用户帐户尚未定义";
                case 2239: return "此用户帐户已过期";
                case 2240: return "此用户不得从此工作站登录网络";
                case 2241: return "这时候不允许用户登录网络";
                case 2242: return "此用户的密码已经过期";
                case 2243: return "此用户的密码无法更改";
                case 2244: return "现在无法使用此密码";
                case 2245: return "密码不满足密码策略的需要。检查最小密码长度、密码复杂性和密码历史的需求";
                case 2246: return "此用户的密码最近才启用，现在不能更改";
                case 2247: return "安全数据库已损坏";
                case 2248: return "不需要更新此副本复制的网络/本地安全数据库";
                case 2249: return "此副本复制的数据库已过时；请同步处理其中的数据";
                case 2250: return "此网络连接不存在";
                case 2251: return "此 asg_type 无效";
                case 2252: return "此设备当前正在共享中";
                case 2270: return "计算机名无法作为消息别名添加。网络上可能已经有此名称";
                case 2271: return "信使服务已经启动";
                case 2272: return "信使服务启动失败";
                case 2273: return "网络上找不到此消息别名";
                case 2274: return "此消息别名已经转发出去";
                case 2275: return "已经添加了此消息别名，但是仍被转发";
                case 2276: return "此消息别名已在本地存在";
                case 2277: return "添加的消息别名已经超过数目上限";
                case 2278: return "无法删除计算机名";
                case 2279: return "消息无法转发回到同一个工作站";
                case 2280: return "域消息处理器出错";
                case 2281: return "消息已经发送出去，但是收件者已经暂停信使服务";
                case 2282: return "消息已经发送出去，但尚未收到";
                case 2283: return "消息别名当前正在使用中。请稍候片刻再试";
                case 2284: return "信使服务尚未启动";
                case 2285: return "该名称不在本地计算机上";
                case 2286: return "网络上找不到转发的消息别名";
                case 2287: return "远程通讯站的消息别名表已经满了";
                case 2288: return "此别名的消息当前没有在转发中";
                case 2289: return "广播的消息被截断";
                case 2294: return "设备名无效";
                case 2295: return "写入出错";
                case 2297: return "网络上的消息别名重复";
                case 2298: return "此消息别名会在稍后删除";
                case 2299: return "没有从所有的网络删除消息别名";
                case 2300: return "这项操作无法在使用多种网络的计算机上执行";
                case 2310: return "此共享的资源不存在";
                case 2311: return "设备没有共享";
                case 2312: return "带此计算机名的会话不存在";
                case 2314: return "没有用此识别号打开的文件";
                case 2315: return "执行远程管理命令失败";
                case 2316: return "打开远程临时文件失败";
                case 2317: return "从远程管理命令返回的数据已经被截断成 64K";
                case 2318: return "此设备无法同时共享为后台处理资源和非后台处理资源";
                case 2319: return "服务器清单中的信息可能不正确";
                case 2320: return "计算机在此域未处于活动状态";
                case 2321: return "在删除共享之前，需要将该共享从分布式文件系统中删除";
                case 2331: return "无法在此设备执行这项操作";
                case 2332: return "此设备无法共享";
                case 2333: return "此设备未打开";
                case 2334: return "此设备名清单无效";
                case 2335: return "队列优先级无效";
                case 2337: return "没有任何共享的通讯设备";
                case 2338: return "指定的队列不存在";
                case 2340: return "此设备清单无效";
                case 2341: return "请求的设备无效";
                case 2342: return "后台处理程序正在使用此设备";
                case 2343: return "此设备已经被当成通讯设备来使用";
                case 2351: return "此计算机名无效";
                case 2354: return "指定的字符串及前缀太长";
                case 2356: return "此路径组成部分无效";
                case 2357: return "无法判断输入类型";
                case 2362: return "类型缓冲区不够大";
                case 2370: return "配置文件不得超过 64K";
                case 2371: return "初始偏移量越界";
                case 2372: return "系统无法删除当前到网络资源的连接";
                case 2373: return "系统无法分析此文件中的命令行";
                case 2374: return "加载配置文件时出错";
                case 2375: return "保存配置文件时出错，只部份地保存了配置文件";
                case 2378: return "此日志文件在前后两次读取之间已经发生变化";
                case 2380: return "资源路径不可以是目录";
                case 2381: return "资源路径无效";
                case 2382: return "目标路径无效";
                case 2383: return "源路径及目标路径分属不同的服务器";
                case 2385: return "请求的 Run 服务器现在暂停";
                case 2389: return "与 Run 服务器通讯时出错";
                case 2391: return "启动后台处理时出错";
                case 2392: return "找不到您连接的共享资源";
                case 2400: return "LAN 适配器号码无效";
                case 2401: return "此网络连接有文件打开或请求挂起";
                case 2402: return "使用中的连接仍存在";
                case 2403: return "此共享名或密码无效";
                case 2404: return "设备正由活动进程使用，无法断开";
                case 2405: return "此驱动器号已在本地使用";
                case 2430: return "指定的客户已经在指定的事件注册";
                case 2431: return "警报表已满";
                case 2432: return "发出的警报名称无效或不存在";
                case 2433: return "警报接收者无效";
                case 2434: return "用户的登录时间长短不再合法。所以已经删除用户与该服务器的会话";
                case 2440: return "日志文件中没有请求的记录号";
                case 2450: return "用户帐户数据库没有正确配置";
                case 2451: return "当 Netlogon 服务正在运行时，不允许执行这项操作";
                case 2452: return "这项操作无法在最后的管理帐户上执行";
                case 2453: return "找不到此域的域控制器";
                case 2454: return "无法设置此用户的登录信息";
                case 2455: return "Netlogon 服务尚未启动";
                case 2456: return "无法添加到用户帐户数据库";
                case 2457: return "此服务器的时钟与主域控制器的时钟不一致";
                case 2458: return "检测到密码不匹配";
                case 2460: return "服务器识别码没有指定有效的服务器";
                case 2461: return "会话标识没有指定有效的会话";
                case 2462: return "连接识别码没有指定有效的连接";
                case 2463: return "可用服务器表中无法再加上其它项";
                case 2464: return "服务器已经到了支持的会话数目上限";
                case 2465: return "服务器已经到了支持的连接数目上限";
                case 2466: return "服务器打开的文件到了上限，无法打开更多文件";
                case 2467: return "这台服务器没有登记替换的服务器";
                case 2470: return "请用低级的 API (远程管理协议)";
                case 2480: return "UPS 服务无法访问 UPS 驱动程序";
                case 2481: return "UPS 服务设置错误";
                case 2482: return "UPS 服务无法访问指定通讯端口 (Comm Port)";
                case 2483: return "UPS 显示线路中断或电池不足，服务没有启动";
                case 2484: return "UPS 服务无法执行系统关机的操作";
                case 2500: return "下面的程序返回一个 MS-DOS 错误码";
                case 2501: return "下面的程序需要更多的内存";
                case 2502: return "下面程序调用了不支持的 MS-DOS 函数";
                case 2503: return "工作站无法启动";
                case 2504: return "下面的文件已损坏";
                case 2505: return "启动块定义文件中没有指定引导程序";
                case 2506: return "NetBIOS 返回错误: NCB 及 SMB 数据转储";
                case 2507: return "磁盘 I/O 错误";
                case 2508: return "无法替换映像参数";
                case 2509: return "跨越磁盘扇区范围的映像参数太多";
                case 2510: return "不是从用 /S 格式化的 MS-DOS软盘产生的映像";
                case 2511: return "稍后会从远程重新启动";
                case 2512: return "无法调用远程启动服务器";
                case 2513: return "无法连接到远程启动服务器";
                case 2514: return "无法打开远程启动服务器上的映像文件";
                case 2515: return "正在连接到远程启动服务器";
                case 2516: return "正在连接到远程启动服务器";
                case 2517: return "远程启动服务已经停止，请检测错误记录文件，查明出错的原因";
                case 2518: return "远程启动失败，请检查错误日志文件，查明出错的原因";
                case 2519: return "不允许第二个远程启动 (Remoteboot) 资源连接";
                case 2550: return "浏览服务设置成 MaintainServerList=No";
                case 2610: return "因为没有网卡与这项服务一起启动，所以无法启动服务";
                case 2611: return "因为注册表中的启动信息不正确，所以无法启动服务";
                case 2612: return "无法启动服务，原因是它的数据库找不到或损坏";
                case 2613: return "因为找不到 RPLFILES 共享的资源，所以无法启动服务";
                case 2614: return "因为找不到 RPLUSER 组，所以无法启动服务";
                case 2615: return "无法枚举服务记录";
                case 2616: return "工作站记录信息已损坏";
                case 2617: return "工作站记录找不到";
                case 2618: return "其它的工作站正在使用此工作站名";
                case 2619: return "配置文件记录已损坏";
                case 2620: return "配置文件记录找不到";
                case 2621: return "其它的配置文件正在使用此名称";
                case 2622: return "有很多工作站正在使用此配置文件";
                case 2623: return "配置记录已损坏";
                case 2624: return "配置记录找不到";
                case 2625: return "适配器识别记录已损坏";
                case 2626: return "内部服务出错";
                case 2627: return "供应商识别记录已损坏";
                case 2628: return "启动块记录已损坏";
                case 2629: return "找不到此工作站的用户帐户记录";
                case 2630: return "RPLUSER 本地组找不到";
                case 2631: return "找不到启动块记录";
                case 2632: return "所选的配置文件与此工作站不兼容";
                case 2633: return "其它的工作站正在使用所选的网卡";
                case 2634: return "有些配置文件正在使用此配置";
                case 2635: return "有数个工作站、配置文件或配置正在使用此启动块";
                case 2636: return "服务无法制作远程启动数据库的备份";
                case 2637: return "找不到适配器记录";
                case 2638: return "找不到供应商记录";
                case 2639: return "其它供应商记录正在使用此供应商名称";
                case 2640: return "其它的启动区记录正在使用启动名称或供应商识别记录";
                case 2641: return "其它的配置正在使用此配置名称";
                case 2660: return "由 Dfs 服务所维护的内部数据库已损坏";
                case 2661: return "内部数据库中的一条记录已损坏";
                case 2662: return "输入项路径与卷路径不匹配";
                case 2663: return "给定卷名已存在";
                case 2664: return "指定的服务器共享已在 Dfs 中共享";
                case 2665: return "所显示的服务器共享不支持所显示的 Dfs 卷";
                case 2666: return "此操作在非叶卷上无效";
                case 2667: return "此操作在叶卷上无效";
                case 2668: return "此操作不明确，因为该卷存在多服务器";
                case 2669: return "无法创建连接点";
                case 2670: return "该服务器不是 Dfs 可识别的";
                case 2671: return "指定的重命名目标路径无效";
                case 2672: return "指定 Dfs 卷脱线";
                case 2673: return "指定的服务器不为此卷服务";
                case 2674: return "检测到 Dfs 名中的环路";
                case 2675: return "在基于服务器的 Dfs 上不支持该操作";
                case 2676: return "这个卷已经受该指定服务器共享支持";
                case 2677: return "无法删除这个卷的上一个服务器共享支持";
                case 2678: return "Inter-Dfs 卷不支持该操作";
                case 2679: return "Dfs 服务的内部状态已经变得不一致";
                case 2680: return "Dfs 服务已经安装在指定的服务器上";
                case 2681: return "被协调的 Dfs 数据是一样的";
                case 2682: return "无法删除 Dfs 根目录卷 - 如需要请卸载 Dfs";
                case 2683: return "该共享的子目录或父目录已经存在在一个 Dfs 中";
                case 2690: return "Dfs 内部错误";
                case 2691: return "这台机器已经加入域 ";
                case 2692: return "这个机器目前未加入域";
                case 2693: return "这台机器是域控制器，而且无法从域中退出";
                case 2694: return "目标域控制器不支持在 OU 中创建的机器帐户";
                case 2695: return "指定的工作组名无效";
                case 2696: return "指定的计算机名与域控制器上使用的默认语言不兼容";
                case 2697: return "找不到指定的计算机帐户";
                case 2999: return "这是 NERR 范围内的最后一个错误";
                case 3000: return "指定了未知的打印监视器";
                case 3001: return "指定的打印机驱动程序当前正在使用";
                case 3002: return "找不到缓冲文件";
                case 3003: return "未发送 StartDocPrinter 调用";
                case 3004: return "未发送 AddJob 调用";
                case 3005: return "指定的打印处理器已经安装";
                case 3006: return "指定的打印监视器已经安装";
                case 3007: return "该指定的打印监视器不具备所要求的功能";
                case 3008: return "该指定的打印监视器正在使用中";
                case 3009: return "当打印机有作业排成队列时此操作请求是不允许的";
                case 3010: return "请求的操作成功。直到重新启动系统前更改将不会生效";
                case 3011: return "请求的操作成功。直到重新启动服务前更改将不会生效";
                case 3012: return "找不到打印机";
                case 3023: return "用户指定的关机命令文件，它的配置有问题。不过 UPS 服务已经启动";
                case 3029: return "因为用户帐户数据库 (NET.ACC) 找不到或损坏，而且也没有可用的备份数据库，所以不能启动本地安全机制。系统不安全";
                case 3037: return "@I *登录小时数";
                case 3039: return "已经超过一个目录中文件的副本复制的限制";
                case 3040: return "已经超过副本复制的目录树深度限制";
                case 3046: return "无法登录。用户当前已经登录，同时参数 TRYUSER设置为 NO";
                case 3052: return "命令行或配置文件中没有提供必要的参数";
                case 3054: return "无法满足资源的请求";
                case 3055: return "系统配置有问题";
                case 3056: return "系统出错";
                case 3057: return "发生内部一致性的错误";
                case 3058: return "配置文件或命令行的选项不明确";
                case 3059: return "配置文件或命令行的参数重复";
                case 3060: return "服务没有响应控制，DosKillProc 函数已经停止服务";
                case 3061: return "运行服务程序时出错";
                case 3062: return "无法启动次级服务";
                case 3064: return "文件有问题";
                case 3070: return "内存";
                case 3071: return "磁盘空间";
                case 3072: return "线程";
                case 3073: return "过程";
                case 3074: return "安全性失败";
                case 3075: return "LAN Manager 根目录不正确或找不到";
                case 3076: return "未安装网络软件";
                case 3077: return "服务器未启动";
                case 3078: return "服务器无法访问用户帐户数据库 (NET.ACC)";
                case 3079: return "LANMAN 树中安装的文件不兼容";
                case 3080: return "LANMAN/LOGS 目录无效";
                case 3081: return "指定的域无法使用";
                case 3082: return "另一计算机正将此计算机名当作消息别名使用";
                case 3083: return "宣布服务器名失败";
                case 3084: return "用户帐户数据库没有正确配置";
                case 3085: return "服务器没有运行用户级安全功能";
                case 3087: return "工作站设置不正确";
                case 3088: return "查看您的错误日志文件以了解详细信息";
                case 3089: return "无法写入此文件";
                case 3090: return "ADDPAK 文件损坏。请删除 LANMAN/NETPROG/ADDPAK.SER后重新应用所有的 ADDPAK";
                case 3091: return "因为没有运行 CACHE.EXE，所以无法启动 LM386 服务器";
                case 3092: return "安全数据库中找不到这台计算机的帐户";
                case 3093: return "这台计算机不是 SERVERS 组的成员";
                case 3094: return "SERVERS 组没有在本地安全数据库中";
                case 3095: return "此 Windows NT 计算机被设置为某个组的成员，并不是域的成员。此种配置下不需要运行 Netlogon 服务";
                case 3096: return "找不到此域的 Windows NT 域控制器";
                case 3098: return "服务无法与主域控制器进行验证";
                case 3099: return "安全数据库文件创建日期或序号有问题";
                case 3100: return "因为网络软件出错，所以无法执行操作";
                case 3102: return "这项服务无法长期锁定网络控制块 (NCB) 的段。错误码就是相关数据";
                case 3103: return "这项服务无法解除网络控制块 (NCB) 段的长期锁定。错误码就是相关数据";
                case 3106: return "收到意外的网络控制块 (NCB)。NCB 就是相关数据";
                case 3107: return "网络没有启动";
                case 3108: return "NETWKSTA.SYS 的 DosDevIoctl 或 DosFsCtl 调用失败。显示的数据为以下格式:DWORD 值代表调用 Ioctl 或 FsCtl 的 CS:IP WORD 错误代码WORD Ioctl 或 FsCtl 号";
                case 3111: return "发生意外的 NetBIOS 错误。错误码就是相关数据";
                case 3112: return "收到的服务器消息块 (SMB) 无效。SMB 就是相关数据";
                case 3114: return "因为缓冲区溢出，所以错误日志文件中部份的项目丢失";
                case 3120: return "控制网络缓冲区以外资源用量的初始化参数被设置大小，因此需要的内存太多";
                case 3121: return "服务器无法增加内存段的大小";
                case 3124: return "服务器启动失败。三个 chdev参数必须同时为零或者同时不为零";
                case 3129: return "服务器无法更新 AT 计划文件。文件损坏";
                case 3130: return "服务器调用 NetMakeLMFileName 时出错。错误码就是相关数据";
                case 3132: return "无法长期锁定服务器缓冲区。请检查交换磁盘的可用空间，然后重新启动系统以启动服务器";
                case 3140: return "因为多次连续出现网络控制块 (NCB) 错误，所以停止服务。最后一个坏的 NCB 以原始数据形式出现";
                case 3141: return "因为消息服务器共享的数据段被锁住，所以消息服务器已经停止运行";
                case 3151: return "因为 VIO 调用出错，所以无法弹出显示消息。错误码就是相关数据";
                case 3152: return "收到的服务器消息块 (SMB) 无效。SMB 就是相关数据";
                case 3160: return "工作站信息段大于 64K。大小如下(以 DWORD 值的格式)";
                case 3161: return "工作站无法取得计算机的名称号码";
                case 3162: return "工作站无法初始化 Async NetBIOS 线程。错误码就是相关数据";
                case 3163: return "工作站无法打开最前面的共享段。错误码就是相关数据";
                case 3164: return "工作站主机表已满";
                case 3165: return "收到的邮筒服务器消息块 (SMB) 有问题，SMB 就是相关数据";
                case 3166: return "工作站启动用户帐户数据库时出错。错误码就是相关数据";
                case 3167: return "工作站响应 SSI 重新验证请求时出错。函数码及错误码就是相关数据";
                case 3174: return "服务器无法读取 AT 计划文件";
                case 3175: return "服务器发现错误的 AT 计划记录";
                case 3176: return "服务器找不到 AT 计划文件，所以创建一个计划文件";
                case 3185: return "因为用户帐户数据库 (NET.ACC) 找不到或损坏，而且也没有可用的备份数据库，所以不能启动本地安全机制。系统不安全";
                case 3204: return "服务器无法创建线程。CONFIG.SYS 中的 THREADS 参数必须加大";
                case 3213: return "已经超过一个目录中文件的副本复制的限制";
                case 3214: return "已经超过副本复制的目录树深度限制";
                case 3215: return "邮筒中收到的消息无法识别";
                case 3217: return "无法登录。用户当前已经登录，同时参数 TRYUSER设置为 NO";
                case 3230: return "检测到服务器的电源中断";
                case 3231: return "UPS 服务已经关掉服务器";
                case 3232: return "UPS 服务没有完成执行用户指定的关机命令文件";
                case 3233: return "无法打开 UPS 驱动程序。错误码就是相关数据";
                case 3234: return "电源已经恢复";
                case 3235: return "用户指定的关机命令文件有问题";
                case 3256: return "该项服务的动态链接库发生无法修复的错误";
                case 3257: return "系统返回意外的错误码。错误码就是相关数据";
                case 3258: return "容错错误日志文件 - LANROOT/LOGS/FT.LOG超过 64K";
                case 3259: return "容错错误日志文件 - LANROOT/LOGS/FT.LOG，在被打开时就已设置更新进度位，这表示上次使用错误日志时，系统死机";
                case 3301: return "Remote IPC";
                case 3302: return "Remote Admin";
                case 3303: return "Logon server share";
                case 3304: return "网络出错";
                case 3400: return "内存不足，无法启动工作站服务";
                case 3401: return "读取 LAMAN.INI 文件的 NETWORKS 项目出错";
                case 3404: return "LAMAN.INI 文件中的 NETWORKS 项目太多";
                case 3408: return "程序无法用在此操作系统";
                case 3409: return "已经安装转发程序";
                case 3411: return "安装 NETWKSTA.SYS 时出错。请按 ENTER 继续";
                case 3412: return "求解程序链接问题";
                case 3419: return "您已经打开文件或设备，强制断开会造成数据丢失";
                case 3420: return "内部用的默认共享";
                case 3421: return "信使服务";
                case 3500: return "命令成功完成";
                case 3501: return "使用的选项无效";
                case 3503: return "命令包含无效的参数个数";
                case 3504: return "命令运行完毕，但发生一个或多个错误";
                case 3505: return "使用的选项数值不正确";
                case 3510: return "命令使用了冲突的选项";
                case 3512: return "软件需要新版的操作系统";
                case 3513: return "数据多于 Windows 2000 所能够返回的";
                case 3515: return "此命令只能用在 Windows 2000 域控制器";
                case 3516: return "这个指令不能用于一个 Windows 2000 域控制器";
                case 3520: return "已经启动以下 Windows 2000 服务";
                case 3525: return "停止工作站服务也会同时停止服务器服务 ";
                case 3526: return "工作站有打开的文件";
                case 3533: return "服务正在启动或停止中，请稍候片刻后再试一次";
                case 3534: return "服务没有报告任何错误";
                case 3535: return "正在控制设备时出错";
                case 3660: return "这些工作站在这台服务器上有会话";
                case 3661: return "这些工作站有会话打开了此台服务器上的文件";
                case 3666: return "消息别名已经转发出去";
                case 3670: return "您有以下的远程连接";
                case 3671: return "继续运行会取消连接";
                case 3676: return "会记录新的网络连接";
                case 3677: return "不记录新的网络连接";
                case 3678: return "保存配置文件时出错，原先记录的网络连接状态没有更改";
                case 3679: return "读取配置文件时出错";
                case 3682: return "没有启动任何网络服务";
                case 3683: return "清单是空的";
                case 3689: return "工作站服务已经在运行中，Windows 2000 会忽略工作站的命令选项";
                case 3694: return "在打印作业正在后台处理到队列时，无法删除共享的队列";
                case 3710: return "打开帮助文件时出错";
                case 3711: return "帮助文件是空的";
                case 3712: return "帮助文件已经损坏";
                case 3714: return "这是专为那些安装旧版软件的系统提供的操作";
                case 3716: return "设备类型未知";
                case 3717: return "日志文件已经损坏";
                case 3718: return "程序文件名后必须以 .EXE 结束";
                case 3719: return "找不到匹配的共享，因此没有删除";
                case 3720: return "用户记录中的 “单位/星期” 的值不正确";
                case 3725: return "删除共享时出错";
                case 3726: return "用户名无效";
                case 3727: return "密码无效";
                case 3728: return "密码不匹配";
                case 3729: return "永久连接没有完全还原";
                case 3730: return "计算机名或域名错误";
                case 3732: return "无法设置该资源的默认权限";
                case 3734: return "没有输入正确的密码";
                case 3735: return "没有输入正确的名称";
                case 3736: return "该资源无法共享";
                case 3737: return "权限字符串包含无效的权限";
                case 3738: return "您只能在打印机或通讯设备上执行这项操作";
                case 3743: return "服务器没有设置远程管理的功能";
                case 3752: return "这台服务器上没有用户的会话";
                case 3756: return "响应无效";
                case 3757: return "没有提供有效的响应";
                case 3758: return "提供的目标清单与打印机队列目标清单不匹配";
                case 3761: return "指定的时间范围中结束的时间比开始的时间早";
                case 3764: return "提供的时间不是整点";
                case 3765: return "12 与 24 小时格式不能混用";
                case 3767: return "提供的日期格式无效";
                case 3768: return "提供的日期范围无效";
                case 3769: return "提供的时间范围无效";
                case 3770: return "NET USER 的参数无效。请检查最短的密码长度和/或提供参数";
                case 3771: return "ENABLESCRIPT 的值必须是 YES";
                case 3773: return "提供的国家(地区)代码无效";
                case 3774: return "用户已经创建成功，但是无法添加到USERS 本地组中";
                case 3775: return "提供的用户上下文无效";
                case 3777: return "文件发送功能已不再支持";
                case 3778: return "您可能没有指定 ADMIN$ 及 IPC$ 共享的路径";
                case 3784: return "只有磁盘共享可以标记为可以缓存";
                case 3802: return "此计划日期无效";
                case 3803: return "LANMAN 根目录无法使用";
                case 3804: return "SCHED.LOG 文件无法打开";
                case 3805: return "服务器服务尚未启动";
                case 3806: return "AT 作业标识不存在";
                case 3807: return "AT 计划文件已损坏";
                case 3808: return "因为 AT 计划文件发生问题，所以无法运行删除操作";
                case 3809: return "命令行不得超过 259 个字符";
                case 3810: return "因为磁盘已满，所以 AT 计划文件无法更新";
                case 3812: return "AT 计划文件无效。请删除此文件并创建新的文件";
                case 3813: return "AT 计划文件已经删除";
                case 3814: return "此命令的语法是:AT [id] [/DELETE]AT 时间 [/EVERY:日期 | /NEXT:日期] 命令AT 命令会在以后的指定日期及时间，安排程序在服务器上运行。它也会显示安排运行的程序及命令的清单。您可以将日期指定为M、T、W、Th、F、Sa、Su 或 1-31 的格式。您可以将时间指定为HH:MM的二十四小时格式";
                case 3815: return "AT 命令已经超时。请稍后再试一次";
                case 3816: return "用户帐户的密码使用最短期限不得大于密码最长使用期限";
                case 3817: return "指定的数值与安装下层软件的服务器不兼容。请指定较小的值";
                case 3901: return "****";
                case 3902: return "**** 意外到达消息的结尾 ****";
                case 3905: return "请按 ESC 退出";
                case 3906: return "...";
                case 3912: return "找不到时间服务器";
                case 3915: return "无法判断用户的主目录";
                case 3916: return "没有指定用户的主目录";
                case 3920: return "已经没有可用的驱动器号";
                case 3936: return "这台计算机目前没有配置成使用一个指定的 SNTP 服务器";
                case 3953: return "语法错误";
                case 3960: return "指定的文件号码无效";
                case 3961: return "指定的打印作业号码无效";
                case 3963: return "指定的用户或组帐户找不到";
                case 3965: return "已添加用户，但 NetWare 的文件和打印服务无法启用";
                case 3966: return "没有安装 NetWare 的文件和打印服务";
                case 3967: return "无法为 NetWare 的文件和打印服务设置用户属性";
                case 3969: return "NetWare 兼容登录";
                case 4000: return "WINS 在处理命令时遇到错误";
                case 4001: return "本地的 WINS 不能删除";
                case 4002: return "文件导入操作失败";
                case 4003: return "备份操作失败。是否先前已作过完整备份";
                case 4004: return "备份操作失败。请检查您备份数据库的目录";
                case 4005: return "WINS 数据库中没有这个名称";
                case 4006: return "不允许复制一个尚未配置的伙伴";
                case 4100: return "DHCP 客户获得一个在网上已被使用的 IP 地址。直到 DHCP 客户可以获得新的地址前，本地接口将被禁用";
                case 4200: return "无法识别传来的 GUID 是否为有效的 WMI 数据提供程序";
                case 4201: return "无法识别传来的实例名是否为有效的 WMI 数据提供程序";
                case 4202: return "无法识别传来的数据项目标识符是否为有效的 WMI 数据提供程序";
                case 4203: return "无法完成 WMI 请求，应该重试一次";
                case 4204: return "找不到 WMI 数据提供程序";
                case 4205: return "WMI 数据提供程序引用到一个未注册的实例组";
                case 4206: return "WMI 数据块或事件通知已启用";
                case 4207: return "WMI 数据块不再可用";
                case 4208: return "WMI 数据服务无法使用";
                case 4209: return "WMI 数据提供程序无法完成要求";
                case 4210: return "WMI MOF 信息无效";
                case 4211: return "WMI 注册信息无效";
                case 4212: return "WMI 数据块或事件通知已禁用";
                case 4213: return "WMI 数据项目或数据块为只读";
                case 4214: return "WMI 数据项目或数据块不能更改";
                case 4300: return "媒体标识符没有表示一个有效的媒体";
                case 4301: return "库标识符没有表示一个有效的库";
                case 4302: return "媒体缓冲池标识符没有表示一个有效的媒体缓冲池";
                case 4303: return "驱动器和媒体不兼容或位于不同的库中";
                case 4304: return "媒体目前在脱机库中，您必须联机才能运行这个操作";
                case 4305: return "操作无法在脱机库中运行";
                case 4306: return "库、驱动器或媒体缓冲池是空的";
                case 4307: return "库、磁盘或媒体缓冲池必须是空的，才能运行这个操作";
                case 4308: return "在这个媒体缓冲池或库中目前没有可用的媒体";
                case 4309: return "这个操作所需的资源已禁用";
                case 4310: return "媒体标识符没有表示一个有效的清洗器";
                case 4311: return "无法清洗驱动器或不支持清洗";
                case 4312: return "对象标识符没有表示一个有效的对象";
                case 4313: return "无法读取或写入数据库";
                case 4314: return "数据库已满";
                case 4315: return "媒体与设备或媒体缓冲池不兼容";
                case 4316: return "这个操作所需的资源不存在";
                case 4317: return "操作标识符不正确";
                case 4318: return "媒体未被安装，或未就绪";
                case 4319: return "设备未就绪";
                case 4320: return "操作员或系统管理员拒绝了请求";
                case 4321: return "驱动器标识符不代表一个有效的驱动器";
                case 4322: return "程序库已满。没有可使用的插槽";
                case 4323: return "传输程序不能访问媒体";
                case 4324: return "无法将媒体加载到驱动器中";
                case 4325: return "无法检索有关驱动器的状态";
                case 4326: return "无法检索有关插槽的状态";
                case 4327: return "无法检索传输的状态";
                case 4328: return "因为传输已在使用中，所以无法使用";
                case 4329: return "无法打开或关闭弹入/弹出端口";
                case 4330: return "因为媒体在驱动器中，无法将其弹出";
                case 4331: return "清洗器插槽已被保留";
                case 4332: return "没有保留清洗器插槽";
                case 4333: return "清洗器墨盒已进行了最大次数的驱动器清洗";
                case 4334: return "意外媒体标识号";
                case 4335: return "在这个组或源中最后剩下的项目不能被删除";
                case 4336: return "提供的消息超过了这个参数所允许的最大尺寸";
                case 4337: return "该卷含有系统和页面文件";
                case 4338: return "由于库中至少有一个驱动器可以支持该媒体类型，不能从库中删除媒体类型";
                case 4339: return "由于没有可以使用的已被启动的驱动器，无法将该脱机媒体装入这个系统";
                case 4340: return "(Y/N) [Y]";
                case 4341: return "(Y/N) [N]";
                case 4342: return "错误";
                case 4343: return "OK";
                case 4344: return "Y";
                case 4345: return "N";
                case 4346: return "任何";
                case 4347: return "A";
                case 4348: return "P";
                case 4349: return "(找不到)";
                case 4350: return "远程存储服务无法撤回文件";
                case 4351: return "远程存储服务此时不可操作";
                case 4352: return "远程存储服务遇到一个媒体错误";
                case 4354: return "请键入密码";
                case 4358: return "请键入用户的密码";
                case 4359: return "请键入共享资源的密码";
                case 4360: return "请键入您的密码";
                case 4361: return "请再键入一次密码以便确认";
                case 4362: return "请键入用户的旧密码";
                case 4363: return "请键入用户的新密码";
                case 4364: return "请键入您的新密码";
                case 4365: return "请键入复制器服务密码";
                case 4368: return "请键入您的用户名";
                case 4372: return "打印作业详细信息";
                case 4378: return "控制下列正在运行的服务";
                case 4379: return "统计数据可用于正在运行的下列服务";
                case 4381: return "此命令的语法是";
                case 4382: return "此命令的选项是";
                case 4383: return "请键入主域控制器的名称";
                case 4385: return "Sunday";
                case 4386: return "Monday";
                case 4387: return "Tuesday";
                case 4388: return "Wednesday";
                case 4389: return "Thursday";
                case 4390: return "此文件或目录不是一个重解析点";
                case 4391: return "重解析点的属性不能被设置，因为它与已有的属性冲突";
                case 4392: return "在重解析点缓冲区中的数据无效";
                case 4393: return "在重解析点缓冲区中的标签无效";
                case 4394: return "请求中指定的标签和重解析点中的不匹配";
                case 4395: return "W";
                case 4396: return "Th";
                case 4397: return "F";
                case 4398: return "S";
                case 4399: return "Sa";
                case 4401: return "组名";
                case 4402: return "注释";
                case 4403: return "成员";
                case 4406: return "别名";
                case 4407: return "注释";
                case 4408: return "成员";
                case 4411: return "用户名";
                case 4412: return "全名";
                case 4413: return "注释";
                case 4414: return "用户的注释";
                case 4415: return "参数";
                case 4416: return "国家(地区)代码";
                case 4417: return "权限等级";
                case 4418: return "操作员权限";
                case 4419: return "帐户启用";
                case 4420: return "帐户到期";
                case 4421: return "上次设置密码";
                case 4422: return "密码到期";
                case 4423: return "密码可更改";
                case 4424: return "允许的工作站";
                case 4425: return "磁盘空间上限";
                case 4426: return "无限制";
                case 4427: return "本地组会员";
                case 4428: return "域控制器";
                case 4429: return "登录脚本";
                case 4430: return "上次登录";
                case 4431: return "全局组成员";
                case 4432: return "可允许的登录小时数";
                case 4433: return "全部";
                case 4434: return "无";
                case 4436: return "主目录";
                case 4437: return "需要密码";
                case 4438: return "用户可以更改密码";
                case 4439: return "用户配置文件";
                case 4440: return "已锁定";
                case 4450: return "计算机名";
                case 4451: return "用户名";
                case 4452: return "软件版本";
                case 4453: return "工作站活动在";
                case 4454: return "Windows NT 根目录";
                case 4455: return "工作站域";
                case 4456: return "登录域";
                case 4457: return "其它域";
                case 4458: return "COM 打开超时 (秒)";
                case 4459: return "COM 发送计数 (字节)";
                case 4460: return "COM 发送超时 (毫秒)";
                case 4461: return "DOS 会话打印超时 (秒)";
                case 4462: return "错误日志文件大小上限 (K)";
                case 4463: return "高速缓存上限 (K)";
                case 4464: return "网络缓冲区数";
                case 4465: return "字符缓冲区数";
                case 4466: return "域缓冲区大小";
                case 4467: return "字符缓冲区大小";
                case 4468: return "计算机全名";
                case 4469: return "工作站域 DNS 名称";
                case 4470: return "Windows 2000";
                case 4481: return "服务器名称";
                case 4482: return "服务器注释";
                case 4483: return "发送管理警报到";
                case 4484: return "软件版本";
                case 4485: return "对等服务器";
                case 4486: return "Windows NT";
                case 4487: return "服务器等级";
                case 4488: return "Windows NT Server";
                case 4489: return "服务器正运行于";
                case 4492: return "服务器已隐藏";
                case 4500: return "零备份存储在这个卷上不可用";
                case 4506: return "登录的用户数量上限";
                case 4507: return "同时可并存的管理员数量上限";
                case 4508: return "资源共享数量上限";
                case 4509: return "资源连接数量上限";
                case 4510: return "服务器打开的文件数量上限";
                case 4511: return "每个会话打开的文件数量上限";
                case 4512: return "文件锁定数量上限";
                case 4520: return "空闲的会话时间 (分)";
                case 4526: return "共享等级";
                case 4527: return "用户等级";
                case 4530: return "未限制的服务器";
                case 4570: return "强制用户在时间到期之后多久必须注销";
                case 4571: return "多少次密码不正确后锁住帐户";
                case 4572: return "密码最短使用期限 (天)";
                case 4573: return "密码最长使用期限 (天)";
                case 4574: return "密码长度下限";
                case 4575: return "保持的密码历史记录长度";
                case 4576: return "计算机角色";
                case 4577: return "工作站域的主域控制器";
                case 4578: return "锁定阈值";
                case 4579: return "锁定持续时间(分)";
                case 4580: return "锁定观测窗口(分)";
                case 4600: return "统计开始于";
                case 4601: return "接受的会话";
                case 4602: return "会话超时";
                case 4603: return "会话出错";
                case 4604: return "发送的 KB";
                case 4605: return "接收的 KB";
                case 4606: return "平均响应时间 (毫秒)";
                case 4607: return "网络错误";
                case 4608: return "访问的文件";
                case 4609: return "后台处理的打印作业";
                case 4610: return "系统出错";
                case 4611: return "密码违规";
                case 4612: return "权限违规";
                case 4613: return "访问的通讯设备";
                case 4614: return "会话已启动";
                case 4615: return "重新连接的会话";
                case 4616: return "会话启动失败";
                case 4617: return "断开的会话";
                case 4618: return "网络 I/O 执行";
                case 4619: return "文件及管道被访问";
                case 4620: return "时间缓冲区耗尽";
                case 4621: return "大缓冲区";
                case 4622: return "请求缓冲区";
                case 4626: return "已做连接";
                case 4627: return "连接失败";
                case 4630: return "接收的字节数";
                case 4631: return "接收的服务器消息块 (SMB)";
                case 4632: return "传输的字节数";
                case 4633: return "传输的服务器消息块 (SMB)";
                case 4634: return "读取操作";
                case 4635: return "写入操作";
                case 4636: return "拒绝原始读取";
                case 4637: return "拒绝原始写入";
                case 4638: return "网络错误";
                case 4639: return "已做连接";
                case 4640: return "重新连接";
                case 4641: return "服务器断开";
                case 4642: return "会话已启动";
                case 4643: return "会话挂起";
                case 4644: return "失败的会话";
                case 4645: return "操作失败";
                case 4646: return "使用计数";
                case 4647: return "使用计数失败";
                case 4655: return "消息名称转发已经取消";
                case 4661: return "密码已经更改成功";
                case 4664: return "消息已经发给网络上所有的用户";
                case 4666: return "消息已经送到此服务器上的所有用户";
                case 4696: return "Windows NT Server";
                case 4697: return "Windows NT Workstation";
                case 4698: return "MS-DOS 增强型工作站";
                case 4700: return "服务器名称 注释";
                case 4701: return "资源共享名 类型 用途 注释";
                case 4702: return "(UNC)";
                case 4703: return "...";
                case 4704: return "Domain";
                case 4706: return "其它可用的网络:";
                case 4710: return "Disk";
                case 4711: return "Print";
                case 4712: return "Comm";
                case 4713: return "IPC";
                case 4714: return "状态 本地 远程 网络";
                case 4715: return "OK";
                case 4716: return "休止";
                case 4717: return "已暂停";
                case 4718: return "断开";
                case 4719: return "错误";
                case 4720: return "正在连接";
                case 4721: return "正在重新连接";
                case 4722: return "状态";
                case 4723: return "本地名称";
                case 4724: return "远程名称";
                case 4725: return "资源类型";
                case 4726: return "# 打开";
                case 4727: return "# 连接";
                case 4728: return "不可用";
                case 4730: return "共享名 资源 注释";
                case 4731: return "共享名";
                case 4732: return "资源";
                case 4733: return "后台处理";
                case 4734: return "权限";
                case 4735: return "最多用户";
                case 4736: return "无限制";
                case 4737: return "用户";
                case 4740: return "识别码 路径 用户名 # 锁定";
                case 4741: return "文件识别码";
                case 4742: return "锁定";
                case 4743: return "权限";
                case 4750: return "计算机 用户名 客户类型 打开空闲时间";
                case 4751: return "计算机";
                case 4752: return "会话时间";
                case 4753: return "空闲时间";
                case 4754: return "资源共享名 类型 # 打开";
                case 4755: return "客户类型";
                case 4756: return "来宾登录";
                case 4770: return "脱机缓存被启用:手动恢复";
                case 4771: return "脱机缓存被启用:自动恢复";
                case 4772: return "脱机缓存被启用:用户之间没有共享";
                case 4773: return "脱机缓存被停用";
                case 4774: return "自动";
                case 4775: return "手动";
                case 4800: return "名称";
                case 4801: return "转发到";
                case 4802: return "已经从下列位置转发给您";
                case 4803: return "这台服务器的用户";
                case 4804: return "用户已经按 Ctrl+Break 中断网络发送";
                case 4810: return "名称 作业编号 大小 状态";
                case 4811: return "作业";
                case 4812: return "打印";
                case 4813: return "名称";
                case 4814: return "作业 #";
                case 4815: return "大小";
                case 4816: return "状态";
                case 4817: return "分隔文件";
                case 4818: return "注释";
                case 4819: return "优先级";
                case 4820: return "打印后于";
                case 4821: return "打印直到";
                case 4822: return "打印处理程序";
                case 4823: return "附加信息";
                case 4824: return "参数";
                case 4825: return "打印设备";
                case 4826: return "打印机活动中";
                case 4827: return "打印机搁置";
                case 4828: return "打印机出错";
                case 4829: return "正在删除打印机";
                case 4830: return "打印机状态未知";
                case 4841: return "作业 #";
                case 4842: return "正在提交用户";
                case 4843: return "通知";
                case 4844: return "作业数据类型";
                case 4845: return "作业参数";
                case 4846: return "正在等候";
                case 4847: return "搁置于队列";
                case 4848: return "正在后台处理";
                case 4849: return "已暂停";
                case 4850: return "脱机";
                case 4851: return "错误";
                case 4852: return "缺纸";
                case 4853: return "需要干预";
                case 4854: return "正在打印";
                case 4855: return "on";
                case 4862: return "驱动程序";
                case 4930: return "用户名 类型 日期";
                case 4931: return "锁定";
                case 4932: return "服务";
                case 4933: return "服务器";
                case 4934: return "服务器已启动";
                case 4935: return "服务器已暂停";
                case 4936: return "服务器已继续操作";
                case 4937: return "服务器已停止";
                case 4938: return "会话";
                case 4939: return "登录来宾";
                case 4940: return "登录用户";
                case 4941: return "登录管理员";
                case 4942: return "正常注销";
                case 4943: return "登录";
                case 4944: return "注销错误";
                case 4945: return "注销自动断开";
                case 4946: return "注销管理员断开";
                case 4947: return "注销受登录限制";
                case 4948: return "服务";
                case 4957: return "帐户";
                case 4964: return "已修改帐户系统设置";
                case 4965: return "登录限制";
                case 4966: return "超过限制: 未知";
                case 4967: return "超过限制: 登录时间";
                case 4968: return "超过限制: 帐户过期";
                case 4969: return "超过限制: 工作站识别码无效";
                case 4970: return "超过限制: 帐户停用";
                case 4971: return "超过限制: 帐户已删除";
                case 4972: return "资源";
                case 4978: return "密码不正确";
                case 4979: return "需要管理员特权";
                case 4980: return "访问";
                case 4984: return "拒绝访问";
                case 4985: return "未知";
                case 4986: return "其它";
                case 4987: return "持续时间:";
                case 4988: return "持续时间: 无效";
                case 4989: return "持续时间: 1 秒以下";
                case 4990: return "(无)";
                case 4994: return "访问结束";
                case 4995: return "登录到网络";
                case 4996: return "拒绝登录";
                case 4997: return "程序 消息 时间";
                case 4999: return "管理员已解除帐户的锁定状态";
                case 5000: return "注销网络";
                case 5001: return "因为其它资源需要它，不能将群集资源移到另一个组";
                case 5002: return "找不到此群集资源的依存";
                case 5003: return "因为已经处于依存状态，此群集资源不能依存于指定的资源";
                case 5004: return "此群集资源未联机";
                case 5005: return "此操作没有可用的群集节点";
                case 5006: return "没有群集资源";
                case 5007: return "找不到群集资源";
                case 5008: return "正在关闭群集";
                case 5009: return "因为联机，群集节点无法从群集中脱离";
                case 5010: return "对象已存在";
                case 5011: return "此对象已在列表中";
                case 5012: return "新请求没有可用的群集组";
                case 5013: return "找不到群集组";
                case 5014: return "因为群集组未联机，此操作不能完成";
                case 5015: return "群集节点不是此资源的所有者";
                case 5016: return "群集节点不是此资源的所有者";
                case 5017: return "群集资源不能在指定的资源监视器中创建";
                case 5018: return "群集资源不能通过资源监视器来联机";
                case 5019: return "因为群集资源联机，此操作不能完成";
                case 5020: return "由于是仲裁资源，群集资源不能被删除或脱机";
                case 5021: return "由于没有能力成为仲裁资源，此群集不能使指定资源成为仲裁资源";
                case 5022: return "群集软件正关闭";
                case 5023: return "组或资源的状态不是执行请求操作的正确状态";
                case 5024: return "属性已被存储，但在下次资源联机前，不是所有的修改将生效";
                case 5025: return "由于不属于共享存储类别，群集不能使指定资源成为仲裁资源";
                case 5026: return "由于是内核资源，无法删除群集资源";
                case 5027: return "仲裁资源联机失败";
                case 5028: return "无法成功创建或装入仲裁日志";
                case 5029: return "群集日志损坏";
                case 5030: return "由于该日志已超出最大限量，无法将记录写入群集日志";
                case 5031: return "群集日志已超出最大限量";
                case 5032: return "群集日志没有发现检查点记录";
                case 5033: return "不满足日志所需的最小磁盘空间";
                case 5034: return "群集节点未能控制仲裁资源，因为它被另一个活动节点拥有";
                case 5035: return "这个操作的群集网络无效";
                case 5036: return "此操作没有可用的群集节点";
                case 5037: return "所有群集节点都必须运行才能执行这个操作";
                case 5038: return "群集资源失败";
                case 5039: return "该群集节点无效";
                case 5040: return "该群集节点已经存在";
                case 5041: return "一个节点正在加入该群集";
                case 5042: return "找不到群集节点";
                case 5043: return "找不到群集本地节点信息";
                case 5044: return "群集网络已经存在";
                case 5045: return "找不到群集网络";
                case 5046: return "群集网络界面已经存在";
                case 5047: return "找不到群集网络界面";
                case 5048: return "群集请求在这个对象中无效";
                case 5049: return "群集网络提供程序无效";
                case 5050: return "群集节点坏了";
                case 5051: return "无法连接到群集节点";
                case 5052: return "该群集节点不是群集的一个成员";
                case 5053: return "群集加入操作正在进行中";
                case 5054: return "该群集网络无效";
                case 5055: return "Mar";
                case 5056: return "该群集节点可以使用";
                case 5057: return "该群集 IP 地址已在使用中";
                case 5058: return "该群集节点没有中止";
                case 5059: return "没有有效的群集安全上下文";
                case 5060: return "该群集网络不是为内部群集通讯配置的";
                case 5061: return "群集节点已经开始";
                case 5062: return "群集节点已经坏了";
                case 5063: return "群集网络已经联机";
                case 5064: return "群集网络已经脱机";
                case 5065: return "群集节点已经是该群集的成员";
                case 5066: return "该群集网络是唯一个为两个或更多活动群集节点进行内部群集通讯的配置。不能从网络上删除内部通讯能力";
                case 5067: return "一个或更多的群集资源依靠网络来向客户提供服务。不能从网络上删除客户访问能力";
                case 5068: return "该操作不能在群集资源上作为仲裁资源执行。您不能将仲裁资源脱机或修改它的所有者名单";
                case 5069: return "该群集仲裁资源不允许有任何依存关系";
                case 5070: return "该群集节点暂停";
                case 5071: return "群集资源不能联机。所有者节点不能在这个资源上运行";
                case 5072: return "群集节点没有准备好，不能执行所请求的操作";
                case 5073: return "群集节点正在关闭";
                case 5074: return "放弃群集节点加入操作";
                case 5075: return "由于加入节点和支持者之间的软件版本不兼容，该群集加入操作失败";
                case 5076: return "由于该群集已经到达其所能监督的资源限制，不能创建这个资源";
                case 5077: return "系统配置在群集加入或形成操作时已更改。放弃加入或形成操作";
                case 5078: return "找不到指定的资源种类";
                case 5079: return "指定的节点不支持这种资源，这也许是由于版本不一致或是由于在这个节点上没有资源 DLL";
                case 5080: return "该资源 DLL 不支持指定的资源名称。这可能是由于一个提供给源 DLL 名称是错误的(或经过更改的)";
                case 5081: return "不能在 RPC 服务器上注册任何身份验证包";
                case 5082: return "由于组的所有者不在组的首选列表中，不能将组联机。要改变组的所有者节点，请移动组";
                case 5083: return "群集数据库的系列号已改变，或者与锁定程序节点不相容，因此加入操作没有成功。如果在加入操作期间群集数据库有任何改动，这都可能发生";
                case 5084: return "资源在其当前状态下，资源监视器不允许执行失败操作。资源处于挂起状态时，这都可能发生";
                case 5085: return "非锁定程序代码收到一个为全局更新保留锁定的请求";
                case 5086: return "群集服务找不到仲裁磁盘";
                case 5087: return "已备份的群集数据库可能已损坏";
                case 5088: return "DFS 根目录已在这个群集节点中";
                case 5089: return "由于与另一个现有属性冲突，未能修改资源属性";
                case 5090: return "西班牙";
                case 5091: return "丹麦";
                case 5092: return "瑞典";
                case 5093: return "挪威";
                case 5094: return "德国";
                case 5095: return "澳大利亚";
                case 5096: return "日本";
                case 5097: return "韩国";
                case 5098: return "中国";
                case 5099: return "台湾";
                case 5100: return "亚洲";
                case 5101: return "葡萄牙";
                case 5102: return "芬兰";
                case 5103: return "阿拉伯";
                case 5104: return "希伯莱";
                case 5153: return "UPS 服务即将执行最后的关机操作";
                case 5170: return "工作站必须用 NET START 才能启动";
                case 5175: return "远程 IPC";
                case 5176: return "远程管理";
                case 5177: return "默认共享";
                case 5291: return "永不";
                case 5292: return "永不";
                case 5293: return "永不";
                case 5295: return "NETUS.HLP";
                case 5296: return "NET.HLP";
                case 5300: return "网络控制块 (NCB) 请求运行成功。NCB 是相关数据";
                case 5301: return "SEND DATAGRAM、SEND BROADCAST、ADAPTER STATUS 或 SESSION STATUS 的网络控制块 (NCB) 缓冲区长度无效。CB 是相关数据";
                case 5302: return "网络控制块 (NCB) 指定的数据描述数组无效。NCB 是相关数据";
                case 5303: return "网络控制块 (NCB) 指定的命令无效。NCB 是相关数据";
                case 5304: return "网络控制块 (NCB) 指定的消息交换码无效。NCB 是相关数据";
                case 5305: return "网络控制块 (NCB) 命令超时。会话可能异常终止。NCB 是相关数据";
                case 5306: return "接收的网络控制块 (NCB) 消息不完整。NCB 是相关数据";
                case 5307: return "网络控制块 (NCB) 指定的缓冲区无效。NCB 是相关数据";
                case 5308: return "网络控制块 (NCB) 指定的会话号码没有作用。NCB 是相关数据";
                case 5309: return "网卡没有任何资源可用。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5310: return "网络控制块 (NCB) 指定的会话已经关闭。NCB 是相关数据";
                case 5311: return "网络控制块 (NCB) 命令已经取消。NCB 是相关数据";
                case 5312: return "网络控制块 (NCB) 指定的消息块不合逻辑。NCB 是相关数据";
                case 5313: return "该名称已经存在于本地适配器名称表中。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5314: return "网卡名称表已满。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5315: return "网络名称已经有活动的会话，现在取消注册。网络控制块 (NCB) 命令运行完毕。NCB 是相关数据";
                case 5316: return "先前发出的 Receive Lookahead 命令对此会话仍起作用。网络控制块 (NCB) 命令被拒绝。NCB 是相关数据";
                case 5317: return "本地会话表已满。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5318: return "拒绝打开网络控制块 (NCB) 会话，远程计算机上没有侦听命令在执行。NCB 是相关数据";
                case 5319: return "网络控制块 (NCB) 指定的名称号码无效。NCB 是相关数据";
                case 5320: return "网络控制块 (NCB) 中指定的调用名称找不到，或者没有应答。NCB 是相关数据";
                case 5321: return "网络控制块 (NCB) 中指定的名称找不到。无法将“*”或00h 填入 NCB 名称。NCB 是相关数据";
                case 5322: return "网络控制块 (NCB) 中指定的名称正用于远程适配器。NCB 是相关数据";
                case 5323: return "网络控制块 (NCB) 中指定的名称已经删除。NCB 是相关数据";
                case 5324: return "网络控制块 (NCB) 中指定的会话异常终止。NCB 是相关数据";
                case 5325: return "网络协议在网络上检测两个或数个相同的名称。 网络控制块 (NCB) 是相关数据";
                case 5326: return "收到意外的协议数据包。远程设备可能不兼容。网络控制块 (NCB) 是相关数据";
                case 5333: return "NetBIOS 界面正忙。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5334: return "未完成的网络控制块 (NCB) 命令太多。NCB 请求被拒绝。NCB 是相关数据";
                case 5335: return "网络控制块 (NCB) 中指定的适配器号无效。NCB 是相关数据";
                case 5336: return "网络控制块 (NCB) 命令在取消的同时运行完毕。NCB 是相关数据";
                case 5337: return "网络控制块 (NCB) 指定的名称已经保留。NCB 是相关数据";
                case 5338: return "网络控制块 (NCB) 命令无法取消。NCB 是相关数据";
                case 5351: return "同一个会话有多个网络控制块 (NCB)。NCB 请求被拒绝。NCB 是相关数据";
                case 5352: return "网卡出错。唯一可能发出的 NetBIOS 命令是 NCB RESET。网络控制块 (NCB) 是相关数据";
                case 5354: return "超过应用程序数目上限。网络控制区 (NCB) 请求被拒绝，NCB 是相关数据";
                case 5356: return "请求的资源无法使用。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5364: return "系统出错。网络控制块 (NCB) 请求被拒绝。NCB 即为数据";
                case 5365: return "“ROM 校验和”失败。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5366: return "RAM 测试失败。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5367: return "数字式环回失败。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5368: return "模拟式环回失败。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5369: return "界面失败。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5370: return "收到的网络控制块 (NCB) 返回码无法识别。NCB 是相关数据";
                case 5380: return "网卡故障。网络控制块 (NCB) 请求被拒绝。NCB 是相关数据";
                case 5381: return "网络控制块 (NCB) 命令仍然处于搁置状态。NCB 是相关数据";
                case 5509: return "Windows 2000 无法按指定的配置启动，将换用先前可工作的配置";
                case 5600: return "无法共享用户或脚本路径";
                case 5601: return "计算机的密码在本地安全数据库中找不到";
                case 5602: return "访问计算机的本地或网络安全数据库时，发生内部错误";
                case 5705: return "Netlogon 服务用于记录数据库更改数据的日志高速缓存已损坏。Netlogon 服务正在复位更改日志文件";
                case 5728: return "无法加载任何传输";
                case 5739: return "此域的全局组数目超过可以复制到 LanMan BDC 的限制。请删除部分的全局组或删除域中的 LanManBDC";
                case 5742: return "服务无法检索必要的消息，所以无法运行远程启动客户";
                case 5743: return "服务发生严重的错误，无法从远程启动3Com 3Start 远程启动客户";
                case 5744: return "服务发生严重的系统错误，即将关机";
                case 5760: return "服务在分析 RPL 配置时出错";
                case 5761: return "服务在创建所有配置的 RPL 配置文件时出错";
                case 5762: return "服务在访问注册表时出错";
                case 5763: return "服务在替换可能过期的 RPLDISK.SYS 时出错";
                case 5764: return "服务在添加安全帐户或设置文件权限时出错。这些帐户是独立 RPL 工作站的 RPLUSER 本地组以及用户帐户";
                case 5765: return "服务无法备份它的数据库";
                case 5766: return "服务无法从它的数据库初始化。数据库可能找不到或损坏。服务会试图从备份数据库恢复该数据库";
                case 5767: return "服务无法从备份数据库还原它的数据库。服务将不启动";
                case 5768: return "服务无法从备份数据库还原它的数据库";
                case 5769: return "服务无法从它还原的数据库初始化。服务将不启动";
                case 5771: return "远程启动数据库采用 NT 3.5 / NT 3.51 格式。NT 正在转换其为 NT 4.0 格式。完成转换后，JETCONV 转换器将写出应用事件日志";
                case 5773: return "该 DC 的 DNS 服务器不支持动态 DNS。将文件 $1$SystemRoot/System32/Config/netlogon.dns$1$中的 DNS 记录添加到伺服那个文件中引用的域的 DNS 服务器";
                case 5781: return "由于没有可以使用的 DNS 服务器，一个或更多 DNS 记录的动态注册和注销未成功";
                case 6000: return "无法加密指定的文件";
                case 6001: return "指定的文件无法解密";
                case 6002: return "指定的文件已加密，而且用户没有能力解密";
                case 6003: return "这个系统没有有效的加密恢复策略配置";
                case 6004: return "所需的加密驱动程序并未加载到系统中";
                case 6005: return "文件加密所使用的加密驱动程序与目前加载的加密驱动程序不同";
                case 6006: return "没有为用户定义的 EFS 关键字";
                case 6007: return "指定的文件并未加密";
                case 6008: return "指定的文件不是定义的 EFS 导出格式";
                case 6009: return "指定的文件是只读文件";
                case 6010: return "已为加密而停用目录";
                case 6011: return "不信任服务器来进行远程加密操作";
                case 6118: return "此工作组的服务器列表当前无法使用";
                case 6200: return "要正常运行，任务计划程序服务的配置必须在系统帐户中运行。单独的任务可以被配置成在其他帐户中运行";
                case 7001: return "指定的会话名称无效";
                case 7002: return "指定的协议驱动程序无效";
                case 7003: return "在系统路径上找不到指定的协议驱动程序";
                case 7004: return "在系统路径上找不到指定的终端连接";
                case 7005: return "不能为这个会话创建一个事件日志的注册键";
                case 7006: return "同名的一个服务已经在系统中存在";
                case 7007: return "在会话上一个关闭操作挂起";
                case 7008: return "没有可用的输出缓冲器";
                case 7009: return "找不到 MODEM.INF 文件";
                case 7010: return "在 MODEM.INF 中没有找到调制解调器名称";
                case 7011: return "调制解调器没有接受发送给它的指令。验证配置的调制解调器与连接的调制解调器是否匹配";
                case 7012: return "调制解调器没有接受发送给它的指令。验证该调制解调器是否接线正确并且打开了电源开关";
                case 7013: return "运载工具检测失败或者由于断开连接，运载工具已被丢弃";
                case 7014: return "在要求的时间内没有发现拨号音。 确定电话线连接正确并可使用";
                case 7015: return "在远程站点回叫时检测到了占线信号";
                case 7016: return "在回叫时远程站点上检测到了声音";
                case 7017: return "传输驱动程序错误";
                case 7022: return "找不到指定的会话";
                case 7023: return "指定的会话名称已处于使用中";
                case 7024: return "由于终端连接目前正在忙于处理一个连接、断开连接、复位或删除操作，无法完成该请求的操作";
                case 7025: return "试图连接到其视频模式不受当前客户支持的会话";
                case 7035: return "应用程序尝试启动 DOS 图形模式。不支持 DOS 图形模式";
                case 7037: return "您的交互式登录权限已被禁用。请与您的管理员联系";
                case 7038: return "该请求的操作只能在系统控制台上执行。这通常是一个驱动程序或系统 DLL 要求直接控制台访问的结果";
                case 7040: return "客户未能对服务器连接消息作出响应";
                case 7041: return "不支持断开控制台会话";
                case 7042: return "不支持重新将一个断开的会话连接到控制台";
                case 7044: return "远程控制另一个会话的请求被拒绝";
                case 7045: return "拒绝请求的会话访问";
                case 7049: return "指定的终端连接驱动程序无效";
                case 7050: return "不能远程控制该请求的会话。这也许是由于该会话被中断或目前没有一个用户登录。而且，您不能从该系统控制台远程控制一个会话或远程控制系统控制台。并且，您不能远程控制您自己的当前会话";
                case 7051: return "该请求的会话没有配置成允许远程控制";
                case 7052: return "拒绝连接到这个终端服务器。终端服务器客户许可证目前正在被另一个用户使用。请与系统管理员联系，获取一份新的终端服务器客户，其许可证号码必须是有效的、唯一的";
                case 7053: return "拒绝连接到这个终端服务器。还没有为这份终端服务器客户输入您的终端服务器客户许可证号码。请与系统管理员联系，为该终端服务器客户输入一个有效的、唯一的许可证号码";
                case 7054: return "系统已达到其授权的登录限制。请以后再试一次";
                case 7055: return "您正在使用的客户没有使用该系统的授权。您的登录请求被拒绝";
                case 7056: return "系统许可证已过期。您的登录请求被拒绝";
                case 8001: return "文件复制服务 API 被错误调用";
                case 8002: return "无法启动文件复制服务";
                case 8003: return "无法停止文件复制服务";
                case 8004: return "文件复制服务 API 终止了请求。事件日志可能有详细信息";
                case 8005: return "该文件复制服务中断了该请求。事件日志可能有详细信息";
                case 8006: return "无法联系文件复制服务。事件日志可能有详细信息";
                case 8007: return "由于该用户没有足够特权，文件复制服务不能满足该请求。事件日志可能有详细信息";
                case 8008: return "由于验证的 RPC 无效，文件复制服务不能满足该请求。事件日志可能有详细信息";
                case 8009: return "由于该用户在域控制器上没有足够特权，文件复制服务不能满足该请求。事件日志可能有详细信息";
                case 8010: return "由于在域控制器上的验证的 RPC 无效，文件复制服务不能满足该请求。事件日志可能有详细信息";
                case 8011: return "该文件复制服务无法与在域控制器上的文件复制服务通讯。事件日志可能有详细信息";
                case 8012: return "在域控制器上的文件复制服务无法与这台计算机上的文件复制服务通讯。事件日志可能有详细信息";
                case 8013: return "由于内部错误，该文件复制服务不能进入该系统卷中。事件日志可能有详细信息";
                case 8014: return "由于内部超时，该文件复制服务不能进入该系统卷中。事件日志可能有详细信息";
                case 8015: return "该文件复制服务无法处理此请求。该系统卷仍在忙于前一个请求";
                case 8016: return "由于内部错误，该文件复制服务无法停止复制该系统卷。事件日志可能有详细信息";
                case 8017: return "该文件复制服务检测到一个无效参数";
                case 8200: return "在安装目录服务时出现一个错误。有关详细信息，请查看事件日志";
                case 8201: return "目录服务在本地评估组成员身份";
                case 8202: return "指定的目录服务属性或值不存在";
                case 8203: return "指定给目录服务的属性语法无效";
                case 8204: return "指定给目录服务的属性类型未定义";
                case 8205: return "指定的目录服务属性或值已经存在";
                case 8206: return "目录服务忙";
                case 8207: return "该目录服务无效";
                case 8208: return "目录服务无法分配相对标识号";
                case 8209: return "目录服务已经用完了相对标识号池";
                case 8210: return "由于目录服务不是该类操作的主控，未能执行操作";
                case 8211: return "目录服务无法初始化分配相对标识号的子系统";
                case 8212: return "该请求的操作没有满足一个或多个与该对象的类别相关的约束";
                case 8213: return "目录服务只可以在一个页状对象上运行要求的操作";
                case 8214: return "目录服务不能在一个对象的 RDN 属性上执行该请求的操作";
                case 8215: return "目录服务检测出修改对象类别的尝试";
                case 8216: return "不能执行请求的通过域的移动操作";
                case 8217: return "无法联系全局编录服务器";
                case 8218: return "策略对象是共享的并只可在根目录上修改";
                case 8219: return "策略对象不存在";
                case 8220: return "请求的策略信息只在目录服务中";
                case 8221: return "域控制器升级目前正在使用中";
                case 8222: return "域控制器升级目前不在使用中";
                case 8224: return "出现一个操作错误";
                case 8225: return "出现一个协议错误";
                case 8226: return "已经超过这个请求的时间限制";
                case 8227: return "已经超过这个请求的大小限制";
                case 8228: return "已经超过这个请求的管理限制";
                case 8229: return "比较的响应为假";
                case 8230: return "比较的响应为真";
                case 8231: return "这个服务器不支持请求的身份验证方式";
                case 8232: return "这台服务器需要一个更安全的身份验证方式";
                case 8233: return "不适当的身份验证";
                case 8234: return "未知的身份验证机制";
                case 8235: return "从服务器返回了一个建议";
                case 8236: return "该服务器不支持该请求的关键扩展";
                case 8237: return "这个请求需要一个安全的连接";
                case 8238: return "不恰当的匹配";
                case 8239: return "出现一个约束冲突";
                case 8240: return "在服务器上没有这样一个对象";
                case 8241: return "有一个别名问题";
                case 8242: return "指定了一个无效的 dn 语法";
                case 8243: return "该对象为叶对象";
                case 8244: return "有一个别名废弃问题";
                case 8245: return "该服务器不愿意处理该请求";
                case 8246: return "检查到一个循环";
                case 8247: return "有一个命名冲突";
                case 8248: return "结果设置太大";
                case 8249: return "该操作会影响到多个 DSA";
                case 8250: return "该服务器不可操作";
                case 8251: return "出现一个本地错误";
                case 8252: return "出现一个编码错误";
                case 8253: return "出现一个解码错误";
                case 8254: return "无法识别寻找筛选器";
                case 8255: return "一个或多个参数非法";
                case 8256: return "不支持指定的方式";
                case 8257: return "没有返回结果";
                case 8258: return "该服务器不支持该指定的控制";
                case 8259: return "客户检测到一个参考循环";
                case 8260: return "超过当前的参考限制";
                case 8301: return "根目录对象必须是一个命名上下文的头。该根目录对象不能有实例父类";
                case 8302: return "不能执行添加副本操作。名称上下文必须可写才能创建副本";
                case 8303: return "出现一个对架构中未定义的一个属性的参考";
                case 8304: return "超过了一个对象的最大尺寸";
                case 8305: return "尝试向目录中添加一个已在使用中的名称的对象";
                case 8306: return "尝试添加一个对象，该对象属于那类在架构中没有一个 RDN 定义的类别";
                case 8307: return "尝试添加一个使用 RDN 的对象，但该 RDN 不是一个在架构中定义的 RDN ";
                case 8308: return "在对象中找不到任何请求的属性";
                case 8309: return "用户缓冲区太小";
                case 8310: return "在操作中指定的属性不出现在对象上";
                case 8311: return "修改操作非法。不允许该修改的某个方面";
                case 8312: return "指定的对象太大";
                case 8313: return "指定的实例类别无效";
                case 8314: return "操作必须在主控 DSA 执行";
                case 8315: return "必须指定对象类别属性";
                case 8316: return "一个所需的属性丢失";
                case 8317: return "尝试修改一个对象，将一个对该类别来讲是非法的属性包括进来";
                case 8318: return "在对象上指定的属性已经存在";
                case 8320: return "指定的属性不存在或没有值";
                case 8321: return "为只有一个值的属性指定了多个值";
                case 8322: return "属性值不在接受范围内";
                case 8323: return "指定的值已存在";
                case 8324: return "由于不存在于对象上，不能删除该属性";
                case 8325: return "由于不存在于对象上，不能删除该属性值";
                case 8326: return "指定的根对象不能是子参考";
                case 8327: return "不允许链接";
                case 8328: return "不允许链接的评估";
                case 8329: return "由于对象的父类不是未实例化就是被删除了，所以不能执行操作";
                case 8330: return "不允许有一个用别名的父类。别名是叶对象";
                case 8331: return "对象和父类必须是同一种类，不是都是原件就是都是副本";
                case 8332: return "由于子对象存在，操作不能执行。这个操作只能在叶对象上执行";
                case 8333: return "没有找到目录对象";
                case 8334: return "别名对象丢失";
                case 8335: return "对象名语法不对";
                case 8336: return "不允许一个别名参考另一个别名";
                case 8337: return "别名不能解除参考";
                case 8338: return "操作超出范围";
                case 8340: return "不能删除 DSA 对象";
                case 8341: return "出现一个目录服务错误";
                case 8342: return "操作只能在内部主控 DSA 对象上执行";
                case 8343: return "对象必须为 DSA 类别";
                case 8344: return "访问权不够不能执行该操作";
                case 8345: return "由于父类不在可能的上级列表上，不能添加该对象";
                case 8346: return "由于该属性处于“安全帐户管理器” (SAM)，不允许访问该属性";
                case 8347: return "名称有太多部分";
                case 8348: return "名称太长";
                case 8349: return "名称值太长";
                case 8350: return "目录服务遇到了一个错误分列名称";
                case 8351: return "目录服务找不到一个名称的属性种类";
                case 8352: return "该名称不能识别一个对象; 该名称识别一个幻象";
                case 8353: return "安全描述符太短";
                case 8354: return "安全描述符无效";
                case 8355: return "为删除的对象创建名称失败";
                case 8356: return "一个新子参考的父类必须存在";
                case 8357: return "该对象必须是一个命名上下文";
                case 8358: return "不允许添加一个不属于系统的属性";
                case 8359: return "对象的类别必须是有结构的; 您不能实例化一个抽象的类别";
                case 8360: return "找不到架构的对象";
                case 8361: return "有这个 GUID (非活动的的或活动的)的本地对象已经存在";
                case 8362: return "操作不能在一个后部链接上执行";
                case 8363: return "找不到指定的命名上下文的互交参考";
                case 8364: return "由于目录服务关闭，操作不能执行";
                case 8365: return "目录服务请求无效";
                case 8366: return "无法读取角色所有者属性";
                case 8367: return "请求的 FSMO 操作失败。不能连接当前的 FSMO 盒";
                case 8368: return "不允许跨过一个命名上下文修改 DN";
                case 8369: return "由于属于系统，不能修改该属性";
                case 8370: return "只有复制器可以执行这个功能";
                case 8371: return "指定的类别没有定义";
                case 8372: return "指定的类别不是一个子类别";
                case 8373: return "名称参考无效";
                case 8374: return "交叉参考已经存在";
                case 8375: return "不允许删除一个主控交叉参考";
                case 8376: return "只在 NC 头上支持子目录树通知";
                case 8377: return "通知筛选器太复杂";
                case 8378: return "架构更新失败: 重复的 RDN";
                case 8379: return "架构更新失败: 重复的 OID";
                case 8380: return "架构更新失败: 重复的 MAPI 识别符";
                case 8381: return "架构更新失败: 复制架构 id GUID";
                case 8382: return "架构更新失败: 重复的 LDAP 显示名称";
                case 8383: return "架构更新失败: 范围下部少于范围上部";
                case 8384: return "架构更新失败: 语法不匹配";
                case 8385: return "架构更新失败: 属性在必须包含中使用";
                case 8386: return "架构更新失败: 属性在可能包含中使用";
                case 8387: return "架构更新失败: 可能包含中的属性不存在";
                case 8388: return "架构更新失败:必须包含中的属性不存在";
                case 8389: return "架构更新失败: 在辅助类别列表中的类别不存在或不是一个辅助类别";
                case 8390: return "架构更新失败: poss-superior 中的类别不存在";
                case 8391: return "架构更新失败: 在 subclassof 列表中的类别不存在或不能满足等级规则";
                case 8392: return "架构更新失败: Rdn-Att-Id 语法不对";
                case 8393: return "架构更新失败: 类别作为辅助类别使用";
                case 8394: return "架构更新失败: 类别作为子类别使用";
                case 8395: return "架构更新失败: 类别作为 poss superior 使用";
                case 8396: return "架构更新在重新计算验证缓存时失败";
                case 8397: return "目录树删除没有完成。要继续删除目录树，必须再次发出请求";
                case 8398: return "不能执行请求的删除操作";
                case 8399: return "不能读取架构记录管理类别识别符";
                case 8400: return "属性架构语法不对";
                case 8401: return "不能缓存属性";
                case 8402: return "不能缓存类别";
                case 8403: return "不能从缓存删除属性";
                case 8404: return "无法从缓存中删除类别";
                case 8405: return "无法读取特殊名称的属性";
                case 8406: return "丢失一个所需的子参考";
                case 8407: return "不能检索范例种类属性";
                case 8408: return "出现一个内部错误";
                case 8409: return "出现一个数据错误";
                case 8410: return "丢失一个属性 GOVERNSID";
                case 8411: return "丢失一个所需要的属性";
                case 8412: return "指定的命名上下文丢失了一个交叉参考";
                case 8413: return "出现一个安全检查错误";
                case 8414: return "没有加载架构";
                case 8415: return "架构分配失败。请检查机器内存是否不足";
                case 8416: return "为属性架构获得所需语法失败";
                case 8417: return "全局编录验证失败。全局编录无效或不支持操作。目录的某些部分目前无效";
                case 8418: return "由于有关服务器之间的架构不匹配，复制操作失败";
                case 8419: return "找不到 DSA 对象";
                case 8420: return "找不到命名上下文";
                case 8421: return "在缓存中找不到命名上下文";
                case 8422: return "无法检索子对象";
                case 8423: return "由于安全原因不允许修改";
                case 8424: return "操作不能替换该隐藏的记录";
                case 8425: return "等级无效";
                case 8426: return "尝试建立等级表失败";
                case 8427: return "目录配置参数在注册中丢失";
                case 8428: return "尝试计算地址簿索引失败";
                case 8429: return "等级表的分配失败";
                case 8430: return "目录服务遇到一个内部失败";
                case 8431: return "目录服务遇到一个未知失败";
                case 8432: return "根对象需要一个 $1$top$1$ 类别";
                case 8433: return "这个目录服务器已关闭，并且不能接受新上浮单一主机操作角色的所有权";
                case 8434: return "目录服务没有必需的配置信息，并且不能决定新上浮单一主机操作角色的所有权";
                case 8435: return "该目录服务无法将一个或多个上浮单一主机操作角色传送给其它服务器";
                case 8436: return "复制操作失败";
                case 8437: return "为这个复制操作指定了一个无效的参数";
                case 8438: return "目录服务太忙，现在无法完成这个复制操作";
                case 8439: return "为这个复制操作指定的单一名称无效";
                case 8440: return "为这一个复制操作所指定的命名上下文无效";
                case 8441: return "为这个复制操作指定的单一名称已经存在";
                case 8442: return "复制系统遇到一个内部错误";
                case 8443: return "复制操作遇到数据库不一致问题";
                case 8444: return "不能连接到为这个复制操作指定的服务器上";
                case 8445: return "复制操作遇到一个有无效范例类型的对象";
                case 8446: return "复制操作无法分配内存";
                case 8447: return "复制操作遇到一个邮件系统错误";
                case 8448: return "目标服务器的复制参考信息已经存在";
                case 8449: return "目标服务器的复制参考信息不存在";
                case 8450: return "由于是由另一台服务器上复制的，因此不能删除命名上下文";
                case 8451: return "复制操作遇到一个数据库错误";
                case 8452: return "命名上下文要被删除或没有从指定的服务器上复制";
                case 8453: return "复制访问被拒绝";
                case 8454: return "这个版本的目录服务不支持请求的操作";
                case 8455: return "取消复制远程过程呼叫";
                case 8456: return "源服务器目前拒绝复制请求";
                case 8457: return "目标服务器当前拒绝复制请求";
                case 8458: return "由于对象名称冲突，复制操作失败";
                case 8459: return "复制源已被重新安装";
                case 8460: return "由于一个所需父对象丢失，复制操作失败";
                case 8461: return "复制操作被抢先";
                case 8462: return "由于缺乏更新，放弃复制同步尝试";
                case 8463: return "由于系统正在关闭，复制操作被中断了";
                case 8464: return "由于目标部分属性设置不是一个源部分属性设置的子设置，复制同步尝试失败";
                case 8465: return "由于主复制尝试从部分复制同步，复制同步尝试失败";
                case 8466: return "已经与为这个复制操作的指定的服务器联系，但是该服务器无法与完成这个操作所需的另外一个服务器联系";
                case 8467: return "在副本安装时，检测到一个使用的源和内部版本之间的架构不匹配，不能安装该副本";
                case 8468: return "架构更新失败: 有同一连接标识符的属性已经存在";
                case 8469: return "名称翻译: 常见处理错误";
                case 8470: return "名称翻译: 不能找到该名称或权限不够，不能看到名称";
                case 8471: return "名称翻译: 输入名称映射到多个输出名称";
                case 8472: return "名称翻译: 找到输出名称，但是找不到相应的输出格式";
                case 8473: return "名称翻译: 不能完全解析，只找到了域";
                case 8474: return "名称翻译: 不接到线上，无法在客户机上执行纯粹的语法映射";
                case 8475: return "不允许一个构造 att 修改";
                case 8476: return "指定的 OM-Object 类别对指定语法的一个属性是不正确的";
                case 8477: return "复制请求已暂停; 等待回答";
                case 8478: return "要求的操作需要一个目录服务，但没有可用的";
                case 8479: return "类别或属性的 LDAP 显示名称含有非 ASCII 字符";
                case 8480: return "请求的查找操作只支持基本查找";
                case 8481: return "查找未能从数据库检索属性";
                case 8482: return "架构更新操作试图添加一个反向链接，但该反向链接没有相应的正向链接";
                case 8483: return "跨域移动的来源和目标在对象日期上不一致。或者是来源，或者是目标没有对象的最后一个版本";
                case 8484: return "跨域移动的来源和目标在对象当前的名称上不一致。或者是来源，或者是目标没有对象的最后一个版本";
                case 8485: return "域间移动的来源和目标是一样的。调用程序应该使用本地移动操作，而不是域间移动操作";
                case 8486: return "域间移动的来源和目标与目录林中的命名上下文不一致。来源或目标没有分区容器的最近版本";
                case 8487: return "跨域移动的目标不是目标命名上下文的权威";
                case 8488: return "跨域移动的来源和目标提供的来源对象的身份不一样。 来源或目标没有来源对象的最近版本";
                case 8489: return "跨域移动的对象应该已经被目标服务器删除。来源服务器没有来源对象的最近版本";
                case 8490: return "要求对 PDC FSMO 的专门访问权的另一个操作正在进行中";
                case 8491: return "跨域移动没有成功，导致被移动对象有两个版本 - 一个在来源域，一个在目标域。需要删除目标对象，将系统还原到一致状态";
                case 8492: return "因为不允许这个类别的跨域移动，或者对象有一些特点，如: 信任帐户或防止移动的受限制的 RID；所以不能将该对象跨域移动";
                case 8493: return "一旦移动，不能将带有成员身份的对象跨域移动，这会侵犯帐户组的成员身份条件。从帐户组成员身份删除对象，再试一次";
                case 8494: return "命名上下文标题必须是另一个命名上下文标题的直接子标题，而不是一个内节点的子标题";
                case 8495: return "因为目录没有提议的命名上下文上面的命名上下文的副本，所以无法验证所提议的命名上下文的名称。请保证充当域命名主机的服务器已配置成全局编录服务器，并且服务器及其复制伙伴是最新的";
                case 8496: return "目标域必须在本机模式中";
                case 8497: return "因为服务器在指定域中没有基础结构容器，所以无法执行操作";
                case 8498: return "不允许跨域移动帐户组";
                case 8499: return "不允许跨域移动资源组";
                case 8500: return "属性的搜索标志无效。ANR 位只在 Unicode 或 Teletex 字符串的属性上有效";
                case 8501: return "不允许在将 NC 头作为子体的对象开始删除目录树";
                case 8502: return "因为目录树在使用中，目录服务未能为删除目录树而将其锁定";
                case 8503: return "删除目录树时，目录服务未能识别要删除的对象列表";
                case 8505: return "只有管理员才能修改管理组的成员列表";
                case 8506: return "不能改变域控制器帐户的主要组 ID";
                case 8507: return "试图修改基础架构";
                case 8508: return "不允许进行下列操作: 为现有类别添加新的强制属性；从现有类别删除强制属性；为没有向回链接属性的特殊类别 \"Top\" 添加可选属性，向回链接属性指的是直接或通过继承。例如: 添加或删除附属类别";
                case 8509: return "该域控制器上不允许架构更新。没有设置注册表项，或者 DC 不是架构 FSMO 角色所有者";
                case 8510: return "无法在架构容器下创建这个类别的对象。在架构容器下，您只能创建属性架构和类别架构对象";
                case 8511: return "副本/子项安装未能获取源 DC 上的架构容器的 objectVersion 属性。架构容器上的属性不存在，或者提供的凭据没有读取属性的权限";
                case 8512: return "副本/子项安装未能读取 system32 目录中的文件 schema.ini 的 SCHEMA 段中的 objectVersion 属性";
                case 8513: return "指定的组类型无效";
                case 8514: return "如果域是安全启用的，在混合型域中不能嵌套全局组";
                case 8515: return "如果域是安全启用的，在混合型域中不能嵌套本地组";
                case 8516: return "全局组不能将本地组作为成员";
                case 8517: return "全局组不能将通用组作为成员";
                case 8518: return "通用组不能将本地组作为成员";
                case 8519: return "全局组不能有跨域成员";
                case 8520: return "本地组不能将另一个跨域本地组作为成员";
                case 8521: return "包含主要成员的组不能改变为安全停用的组";
                case 8522: return "架构缓冲加载未能转换类架构对象上的字符串默认值 SD";
                case 8523: return "只有配置成全局编录服务器的 DSAs 才能充当域命名主机 FSMO 的角色";
                case 8524: return "由于 DNS 查找故障，DSA 操作无法进行";
                case 8525: return "处理一个对象的 DNS 主机名改动时，服务主要名称数值无法保持同步";
                case 8526: return "未能读取安全描述符属性";
                case 8527: return "没有找到请求的对象，但找到了具有那个密钥的对象";
                case 8528: return "正在添加的链接属性的语法不正确。正向链接只能有语法 2.5.5.1、2.5.5.7 和 2.5.5.14，而反向链接只能有语法 2.5.5.1";
                case 8529: return "安全帐户管理员需要获得启动密码";
                case 8530: return "安全帐户管理员需要从软盘获得启动密钥";
                case 8531: return "目录服务无法启动";
                case 8532: return "未能启动目录服务";
                case 8533: return "客户和服务器之间的连接要求数据包保密性";
                case 8534: return "来源域跟目标域不在同一个目录林中";
                case 8535: return "目标域必须在目录林中";
                case 8536: return "该操作要求启用目标域审核";
                case 8537: return "该操作无法为来源域找到 DC";
                case 8538: return "来源对象必须是一个组或用户";
                case 8539: return "来源对象的 SID 已经在目标目录林中";
                case 8540: return "来源对象和目标对象必须属于同一类型";
                case 8542: return "在复制请求中不能包括架构信息";
                case 8543: return "由于架构不兼容性，无法完成复制操作";
                case 8544: return "由于前一个架构的不兼容性，无法完成复制操作";
                case 8545: return "因为源和目标都没有收到有关最近跨域启动操作的信息，所以无法应用复制更新";
                case 8546: return "因为还有主控这个域的域控制器，所以无法删除请求的域";
                case 8547: return "只能在全局编录服务器上执行请求的操作";
                case 8548: return "本地组只能是同一个域中其他本地组的成员";
                case 8549: return "外部安全主要成员不能是通用组的成员";
                case 8550: return "出于安全，无法将属性复制到 GC";
                case 8551: return "由于目前正在处理的修改太多，无法采取 PDC 的检查点";
                case 8552: return "操作需要启用那个源域审核";
                case 8553: return "安全主要对象仅能在域命名环境菜单中创建";
                case 8554: return "服务主要名称(SPN) 无法建造，因为提供的主机名格式不适合";
                case 8555: return "筛选器已传递建造的属性";
                case 8556: return "unicodePwd 属性值必须括在双引号中";
                case 8557: return "您的计算机无法加入域。已超出此域上允许创建的计算机帐户的最大值。请同系统管理员联系，复位或增加此限定值";
                case 8558: return "由于安全原因，操作必须在目标 DC 上运行";
                case 8559: return "由于安全原因，源 DC 必须是 Service Pack 4 或更新版本";
                case 8560: return "在树目录删除的操作中不能删除“关键目录服务系统”对象。数目录删除操作可能只进行了一部分";
                case 9001: return "DNS 服务器无法解释格式";
                case 9002: return "DNS 服务器失败";
                case 9003: return "DNS 名称不存在";
                case 9004: return "名称服务器不支持 DNS 请求";
                case 9005: return "拒绝 DNS 操作";
                case 9006: return "不应该存在的 DNS 名称仍然存在";
                case 9007: return "不应该存在的 DNS RR 集仍然存在";
                case 9008: return "应该存在的 DNS RR 集不存在";
                case 9009: return "DNS 服务器对区域没有权威";
                case 9010: return "在更新或 prereq 中的 DNS 名称不在区域中";
                case 9016: return "DNS 签名验证失败";
                case 9017: return "DNS 不正确密钥";
                case 9018: return "DNS 签名验证过期";
                case 9501: return "为 DNS 查询找不到记录";
                case 9502: return "无效 DNS 包";
                case 9503: return "没有 DNS 包";
                case 9504: return "DNS 错误，请检查 rcode";
                case 9505: return "为保险的 DNS 包";
                case 9551: return "无效的 DNS 种类";
                case 9552: return "无效的 IP 地址";
                case 9553: return "无效的属性";
                case 9554: return "稍后再试一次 DNS 操作";
                case 9555: return "给出的记录名称和种类不是单一的";
                case 9556: return "DNS 名称不符合 RFC 说明";
                case 9557: return "DNS 名称是一个完全合格的 DNS 名称";
                case 9558: return "DNS 名称以“.”分隔(多标签)";
                case 9559: return "DNS 名称是单一部分名称";
                case 9560: return "DNS 名称含有无效字符";
                case 9561: return "DNS 名称完全是数字的";
                case 9601: return "DNS 区域不存在";
                case 9602: return "DNS 区域信息无效";
                case 9603: return "DNS 区域无效操作";
                case 9604: return "无效 DNS 区域配置";
                case 9605: return "DNS 区域没有颁发机构记录的开始(SOA)";
                case 9606: return "DNS 区域没有“名称服务器” (NS)的记录";
                case 9607: return "DNS 区域已锁定";
                case 9608: return "DNS 区域创建失败";
                case 9609: return "DNS 区域已经存在";
                case 9610: return "DNS 自动区域已经存在";
                case 9611: return "无效的 DNS 区域种类";
                case 9612: return "次要 DNS 区域需要主 IP 地址";
                case 9613: return "DNS 区域不是次要的";
                case 9614: return "需要一个次要 IP 地址";
                case 9615: return "WINS 初始化失败";
                case 9616: return "需要 WINS 服务器";
                case 9617: return "NBTSTAT 初始化呼叫失败";
                case 9618: return "颁发机构起始(SOA)删除无效";
                case 9651: return "主要 DNS 区域需要数据文件";
                case 9652: return "DNS 区域的无效数据文件名称";
                case 9653: return "为 DNS 区域打开数据文件失败";
                case 9654: return "为 DNS 区域写数据文件失败";
                case 9655: return "为 DNS 区域读取数据文件时失败";
                case 9701: return "DNS 记录不存在";
                case 9702: return "DNS 记录格式错误";
                case 9703: return "DNS 中节点创建失败";
                case 9704: return "未知 DNS 记录类型";
                case 9705: return "DNS 记录超时";
                case 9706: return "名称不在 DNS 区域";
                case 9707: return "检测到 CNAME 循环";
                case 9708: return "节点为一个 CNAME DNS 记录";
                case 9709: return "指定名称的 CNAME 记录已经存在";
                case 9710: return "记录不在 DNS 区域根目录";
                case 9711: return "DNS 记录已经存在";
                case 9712: return "次要 DNS 区域数据错误";
                case 9713: return "不能创建 DNS 缓存数据";
                case 9714: return "DNS 名称不存在";
                case 9715: return "不能创建指针(PTR)记录";
                case 9716: return "DNS 域没有被删除";
                case 9717: return "该目录服务无效";
                case 9718: return "DNS 区域已经在目录服务中存在";
                case 9719: return "DNS 服务器没有为目录服务集合 DNS 区域创建或读取启动文件";
                case 9751: return "完成 DNS AXFR (区域复制)";
                case 9752: return "DNS 区域复制失败";
                case 9753: return "添加了本地 WINS 服务器";
                case 9801: return "安全更新呼叫需要继续更新请求";
                case 9851: return "TCP/IP 没有安装网络协议";
                case 9852: return "没有为本地系统配置 DNS 服务器";
                case 10004: return "一个封锁操作被对 WSACancelBlockingCall 的调用中断";
                case 10009: return "提供的文件句柄无效";
                case 10013: return "以一种访问权限不允许的方式做了一个访问套接字的尝试";
                case 10014: return "系统检测到在一个调用中尝试使用指针参数时的无效指针地址";
                case 10022: return "提供了一个无效的参数";
                case 10024: return "打开的套接字太多";
                case 10035: return "无法立即完成一个非阻挡性套接字操作";
                case 10036: return "目前正在执行一个阻挡性操作";
                case 10037: return "在一个非阻挡套接字上尝试了一个已经在进行的操作";
                case 10038: return "在一个非套接字上尝试了一个操作";
                case 10039: return "请求的地址在一个套接字中从操作中忽略";
                case 10040: return "一个在数据报套接字上发送的消息大于内部消息缓冲器或其它一些网络限制，或该用户用于接收数据报的缓冲器比数据报小";
                case 10041: return "在套接字函数调用中指定的一个协议不支持请求的套接字类别的语法";
                case 10042: return "在 getsockopt 或 setsockopt 调用中指定的一个未知的、无效的或不受支持的选项或层次";
                case 10043: return "请求的协议还没有在系统中配置，或者没有它存在的迹象";
                case 10044: return "在这个地址家族中不存在对指定的插槽种类的支持";
                case 10045: return "参考的对象种类不支持尝试的操作";
                case 10046: return "协议家族尚未配置到系统中或没有它的存在迹象";
                case 10047: return "使用了与请求的协议不兼容的地址";
                case 10048: return "通常每个套接字地址 (协议/网络地址/端口)只允许使用一次";
                case 10049: return "在其上下文中，该请求的地址无效";
                case 10050: return "套接字操作遇到了一个已死的网络";
                case 10051: return "向一个无法连接的网络尝试了一个套接字操作";
                case 10052: return "当该操作在进行中，由于保持活动的操作检测到一个故障，该连接中断";
                case 10053: return "您的主机中的软件放弃了一个已建立的连接";
                case 10054: return "远程主机强迫关闭了一个现有的连接";
                case 10055: return "由于系统缓冲区空间不足或列队已满，不能执行套接字上的操作";
                case 10056: return "在一个已经连接的套接字上做了一个连接请求";
                case 10057: return "由于套接字没有连接并且 (当使用一个 sendto 调用发送数据报套接字时) 没有提供地址，发送或接收数据的请求没有被接受";
                case 10058: return "由于以前的关闭调用，套接字在那个方向已经关闭，发送或接收数据的请求没有被接受";
                case 10059: return "对某个内核对象的引用过多";
                case 10060: return "由于连接方在一段时间后没有正确的答复或连接的主机没有反应，连接尝试失败";
                case 10061: return "不能做任何连接，因为目标机器积极地拒绝它";
                case 10062: return "无法翻译名称";
                case 10063: return "名称组件或名称太长";
                case 10064: return "由于目标主机坏了，套接字操作失败";
                case 10065: return "套接字操作尝试一个无法连接的主机";
                case 10066: return "不能删除目录，除非它是空的";
                case 10067: return "一个 Windows 套接字操作可能在可以同时使用的应用程序数目上有限制";
                case 10068: return "超过限额";
                case 10069: return "超过磁盘限额";
                case 10070: return "文件句柄引用不再有效";
                case 10071: return "项目在本地不可用";
                case 10091: return "因为它使用提供网络服务的系统目前无效，WSAStartup 目前不能正常工作";
                case 10092: return "不支持请求的 Windows 套接字版本";
                case 10093: return "应用程序没有调用 WSAStartup，或者 WSAStartup 失败";
                case 10101: return "由 WSARecv 或 WSARecvFrom 返回表示远程方面已经开始了关闭步骤";
                case 10102: return "WSALookupServiceNext 不能返回更多的结果";
                case 10103: return "在处理这个调用时，就开始调用 WSALookupServiceEnd。该调用被删除";
                case 10104: return "过程调用无效";
                case 10105: return "请求的服务提供程序无效";
                case 10106: return "没有加载或初始化请求的服务提供程序";
                case 10107: return "从来不应失败的系统调用失败了";
                case 10108: return "没有已知的此服务。在指定的名称空间中找不这个服务";
                case 10109: return "找不到指定的类别";
                case 10110: return "WSALookupServiceNext 不能返回更多的结果";
                case 10111: return "在处理这个调用时，就开始调用 WSALookupServiceEnd。该调用被删除";
                case 10112: return "由于被拒绝，数据查询失败";
                case 11001: return "不知道这样的主机";
                case 11002: return "这是在主机名解析时常出现的暂时错误，并且意味着本地服务器没有从权威服务器上收到响应";
                case 11003: return "在数据寻找中出现一个不可恢复的错误";
                case 11004: return "请求的名称有效并且是在数据库中找到，但是它没有相关的正确的数据";
                case 11005: return "至少到达了一个保留";
                case 11006: return "至少到达了一个路径";
                case 11007: return "没有发送方";
                case 11008: return "没有接受方";
                case 11009: return "保留已经确认";
                case 11010: return "错误是由于资源不足造成";
                case 11011: return "由于管理原因被拒绝 - 无效凭据";
                case 11012: return "未知或有冲突类型";
                case 11013: return "某一部分的 filterspec 或 providerspecific 缓冲区有问题";
                case 11014: return "flowspec 的某部分有问题";
                case 11015: return "一般性 QOS 错误";
                case 11016: return "在流程规格中发现一个无效的或不可识别的服务类型";
                case 11017: return "在 QOS 结构中发现一个无效的或不一致的流程规格";
                case 11018: return "无效的 QOS 提供程序特定缓冲区";
                case 11019: return "使用了无效的 QOS 筛选器样式";
                case 11020: return "使用了无效的 QOS 筛选器类型";
                case 11021: return "FLOWDESCRIPTOR 中指定的 QOS FILTERSPEC 数量不正确";
                case 11022: return "在 QOS 提供程序特定缓冲区中指定了一个 ObjectLength 字符域无效的对象";
                case 11023: return "QOS 结构中指定的流程描述符数量不正确";
                case 11024: return "在 QOS 提供程序特定缓冲区中发现一个不可识别的对象";
                case 11025: return "在 QOS 提供程序特定缓冲区中发现一个无效的策略对象";
                case 11026: return "在流程描述符列表中发现一个无效的 QOS 流程描述符";
                case 11027: return "在 QOS 提供程序特定缓冲区中发现一个无效的或不一致的流程规格";
                case 11028: return "在 QOS 提供程序特定缓冲区中发现一个无效的 FILTERSPEC";
                case 11029: return "在 QOS 提供程序特定缓冲区中发现一个无效的波形丢弃模式对象";
                case 11030: return "在 QOS 提供程序特定缓冲区中发现一个无效的成形速率对象";
                case 11031: return "在 QOS 提供程序特定缓冲区中发现一个保留的策略因素";
            }
            return "错误编号：" + code;
        }
        #endregion
    }
}
