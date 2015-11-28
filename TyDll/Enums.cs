using System;
using System.Text;

namespace TyDll
{
    /// <summary>
    /// Windows 消息列表
    /// </summary>
    public enum WindowsMessage : int
    {
        /// <summary>
        /// 
        /// </summary>
        WM_NULL = 0x0000,
        /// <summary>
        /// 应用程序创建一个窗口 
        /// </summary>
        WM_CREATE = 0x0001,
        /// <summary>
        /// 一个窗口被销毁 
        /// </summary>
        WM_DESTROY = 0x0002,
        /// <summary>
        /// 移动一个窗口
        /// </summary>
        WM_MOVE = 0x0003,
        /// <summary>
        /// 改变一个窗口的大小
        /// </summary>
        WM_SIZE = 0x0005,
        /// <summary>
        /// 一个窗口被激活或失去激活状态；
        /// </summary>
        WM_ACTIVATE = 0x0006,
        /// <summary>
        /// 获得焦点后 
        /// </summary>
        WM_SETFOCUS = 0x0007,
        /// <summary>
        /// 失去焦点
        /// </summary>
        WM_KILLFOCUS = 0x0008,
        /// <summary>
        /// 改变enable状态
        /// </summary>
        WM_ENABLE = 0x000A,
        /// <summary>
        /// 设置窗口是否能重画
        /// </summary>
        WM_SETREDRAW = 0x000B,
        /// <summary>
        /// 应用程序发送此消息来设置一个窗口的文本
        /// </summary>
        WM_SETTEXT = 0x000C,
        /// <summary>
        /// 应用程序发送此消息来复制对应窗口的文本到缓冲区
        /// </summary>
        WM_GETTEXT = 0x000D,
        /// <summary>
        /// 得到与一个窗口有关的文本的长度（不包含空字符）
        /// </summary>
        WM_GETTEXTLENGTH = 0x000E,
        /// <summary>
        /// 要求一个窗口重画自己
        /// </summary>
        WM_PAINT = 0x000F,
        /// <summary>
        /// 当一个窗口或应用程序要关闭时发送一个信号
        /// </summary>
        WM_CLOSE = 0x0010,
        /// <summary>
        /// 当用户选择结束对话框或程序自己调用ExitWindows函数
        /// </summary>
        WM_QUERYENDSESSION = 0x0011,
        /// <summary>
        /// 用来结束程序运行或当程序调用postquitmessage函数 
        /// </summary>
        WM_QUIT = 0x0012,
        /// <summary>
        /// 当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
        /// </summary>
        WM_QUERYOPEN = 0x0013,
        /// <summary>
        /// 当窗口背景必须被擦除时（例在窗口改变大小时）
        /// </summary>
        WM_ERASEBKGND = 0x0014,
        /// <summary>
        /// 当系统颜色改变时，发送此消息给所有顶级窗口
        /// </summary>
        WM_SYSCOLORCHANGE = 0x0015,
        /// <summary>
        /// 当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束 
        /// </summary>
        WM_ENDSESSION = 0x0016,
        /// <summary>
        /// 
        /// </summary>
        WM_SYSTEMERROR = 0x0017,
        /// <summary>
        /// 当隐藏或显示窗口是发送此消息给这个窗口 
        /// </summary>
        WM_SHOWWINDOW = 0x0018,
        /// <summary>
        /// 发此消息给应用程序哪个窗口是激活的，哪个是非激活的；
        /// </summary>
        WM_ACTIVATEAPP = 0x001C,
        /// <summary>
        /// 当系统的字体资源库变化时发送此消息给所有顶级窗口 
        /// </summary>
        WM_FONTCHANGE = 0x001D,
        /// <summary>
        /// 当系统的时间变化时发送此消息给所有顶级窗口
        /// </summary>
        WM_TIMECHANGE = 0x001E,
        /// <summary>
        /// 发送此消息来取消某种正在进行的摸态（操作） 
        /// </summary>
        WM_CANCELMODE = 0x001F,
        /// <summary>
        /// 如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口 
        /// </summary>
        WM_SETCURSOR = 0x0020,
        /// <summary>
        /// 当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口 
        /// </summary>
        WM_MOUSEACTIVATE = 0x0021,
        /// <summary>
        /// 发送此消息给MDI子窗口当用户点击此窗口的标题栏，或当窗口被激活，移动，改变大小 
        /// </summary>
        WM_CHILDACTIVATE = 0x0022,
        /// <summary>
        /// 此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息
        /// </summary>
        WM_QUEUESYNC = 0x0023,
        /// <summary>
        /// 此消息发送给窗口当它将要改变大小或位置； 
        /// </summary>
        WM_GETMINMAXINFO = 0x0024,
        /// <summary>
        /// 发送给最小化窗口当它图标将要被重画 
        /// </summary>
        WM_PAINTICON = 0x0026,
        /// <summary>
        /// 此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画
        /// </summary>
        WM_ICONERASEBKGND = 0x0027,
        /// <summary>
        /// 发送此消息给一个对话框程序去更改焦点位置
        /// </summary>
        WM_NEXTDLGCTL = 0x0028,
        /// <summary>
        /// 每当打印管理列队增加或减少一条作业时发出此消息 
        /// </summary>
        WM_SPOOLERSTATUS = 0x002A,
        /// <summary>
        /// 当button，combobox，listbox，menu的可视外观改变时发送此消息给这些空件的所有者
        /// </summary>
        WM_DRAWITEM = 0x002B,
        /// <summary>
        /// 当button, combo box, list box, list view control, or menu item 被创建时 
        /// 发送此消息给控件的所有者 
        /// </summary>
        WM_MEASUREITEM = 0x002C,
        /// <summary>
        /// 当the list box 或 combo box 被销毁 或 当某些项被删除通过LB_DELETESTRING, LB_RESETCONTENT, 
        /// CB_DELETESTRING, or CB_RESETCONTENT 消息 
        /// </summary>
        WM_DELETEITEM = 0x002D,
        /// <summary>
        /// 此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息 
        /// </summary>
        WM_VKEYTOITEM = 0x002E,
        /// <summary>
        /// 此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息
        /// </summary>
        WM_CHARTOITEM = 0x002F,
        /// <summary>
        /// 当绘制文本时程序发送此消息得到控件要用的颜色
        /// </summary>
        WM_SETFONT = 0x0030,
        /// <summary>
        /// 应用程序发送此消息得到当前控件绘制文本的字体 
        /// </summary>
        WM_GETFONT = 0x0031,
        /// <summary>
        /// 应用程序发送此消息让一个窗口与一个热键相关连 
        /// </summary>
        WM_SETHOTKEY = 0x0032,
        /// <summary>
        /// 应用程序发送此消息来判断热键与某个窗口是否有关联
        /// </summary>
        WM_GETHOTKEY = 0x0033,
        /// <summary>
        /// 此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能 
        /// 返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标 
        /// </summary>
        WM_QUERYDRAGICON = 0x0037,
        /// <summary>
        /// 发送此消息来判定combobox或listbox新增加的项的相对位置 
        /// </summary>
        WM_COMPAREITEM = 0x0039,
        /// <summary>
        /// 
        /// </summary>
        WM_GETOBJECT = 0x003D,
        /// <summary>
        /// 显示内存已经很少了
        /// </summary>
        WM_COMPACTING = 0x0041,
        /// <summary>
        /// 发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数 
        /// </summary>
        WM_WINDOWPOSCHANGING = 0x0046,
        /// <summary>
        /// 发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数 
        /// </summary>
        WM_WINDOWPOSCHANGED = 0x0047,
        /// <summary>
        /// 当系统将要进入暂停状态时发送此消息 （适用于16位的windows）
        /// </summary>
        WM_POWER = 0x0048,
        /// <summary>
        /// 当一个应用程序传递数据给另一个应用程序时发送此消息
        /// </summary>
        WM_COPYDATA = 0x004A,
        /// <summary>
        /// 当某个用户取消程序日志激活状态，提交此消息给程序
        /// </summary>
        WM_CANCELJOURNAL = 0x004B,
        /// <summary>
        /// 当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口 
        /// </summary>
        WM_NOTIFY = 0x004E,
        /// <summary>
        /// 当用户选择某种输入语言，或输入语言的热键改变
        /// </summary>
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        /// <summary>
        /// 当平台现场已经被改变后发送此消息给受影响的最顶级窗口
        /// </summary>
        WM_INPUTLANGCHANGE = 0x0051,
        /// <summary>
        /// 当程序已经初始化windows帮助例程时发送此消息给应用程序
        /// </summary>
        WM_TCARD = 0x0052,
        /// <summary>
        /// 此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就 
        /// 发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口 
        /// </summary>
        WM_HELP = 0x0053,
        /// <summary>
        /// 当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体 
        /// 设置信息，在用户更新设置时系统马上发送此消息； 
        /// </summary>
        WM_USERCHANGED = 0x0054,
        /// <summary>
        /// 公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNI CODE结构 
        /// 在WM_NOTIFY消息，使用此控件能使某个控件与它的父控件之间进行相互通信 
        /// </summary>
        WM_NOTIFYformAT = 0x0055,
        /// <summary>
        /// 当用户某个窗口中点击了一下右键就发送此消息给这个窗口 
        /// </summary>
        WM_CONTEXTMENU = 0x007B,
        /// <summary>
        /// 当调用SETWINDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口 
        /// </summary>
        WM_styleCHANGING = 0x007C,
        /// <summary>
        /// 当调用SETWINDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口 
        /// </summary>
        WM_styleCHANGED = 0x007D,
        /// <summary>
        /// 当显示器的分辨率改变后发送此消息给所有的窗口 
        /// </summary>
        WM_DISPLAYCHANGE = 0x007E,
        /// <summary>
        /// 此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄； 
        /// </summary>
        WM_GETICON = 0x007F,
        /// <summary>
        /// 程序发送此消息让一个新的大图标或小图标与某个窗口关联； 
        /// </summary>
        WM_SETICON = 0x0080,
        /// <summary>
        /// 当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送；
        /// </summary>
        WM_NCCREATE = 0x0081,
        /// <summary>
        /// 此消息通知某个窗口，非客户区正在销毁 
        /// </summary>
        WM_NCDESTROY = 0x0082,
        /// <summary>
        ///  当某个窗口的客户区域必须被核算时发送此消息 
        /// </summary>
        WM_NCCALCSIZE = 0x0083,
        /// <summary>
        /// 移动鼠标，按住或释放鼠标时发生 
        /// </summary>
        WM_NCHITTEST = 0x0084,
        /// <summary>
        /// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时；
        /// </summary>
        WM_NCPAINT = 0x0085,
        /// <summary>
        /// 此消息发送给某个窗口 仅当它的非客户区需要被改变来显示是激活还是非激活状态； 
        /// </summary>
        WM_NCACTIVATE = 0x0086,
        /// <summary>
        /// 发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件 
        /// 通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它 
        /// </summary>
        WM_GETDLGCODE = 0x0087,
        /// <summary>
        /// 当光标在一个窗口的非客户区内移动时发送此消息给这个窗口(非客户区 为：窗体的标题栏及窗的边框体)
        /// </summary>
        WM_NCMOUSEMOVE = 0x00A0,
        /// <summary>
        /// 当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
        /// </summary>
        WM_NCLBUTTONDOWN = 0x00A1,
        /// <summary>
        /// 当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息； 
        /// </summary>
        WM_NCLBUTTONUP = 0x00A2,
        /// <summary>
        /// 当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息 
        /// </summary>
        WM_NCLBUTTONDBLCLK = 0x00A3,
        /// <summary>
        /// 当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息 
        /// </summary>
        WM_NCRBUTTONDOWN = 0x00A4,
        /// <summary>
        /// 当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息 
        /// </summary>
        WM_NCRBUTTONUP = 0x00A5,
        /// <summary>
        /// 当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息 
        /// </summary>
        WM_NCRBUTTONDBLCLK = 0x00A6,
        /// <summary>
        /// 当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息 
        /// </summary>
        WM_NCMBUTTONDOWN = 0x00A7,
        /// <summary>
        /// 当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息 
        /// </summary>
        WM_NCMBUTTONUP = 0x00A8,
        /// <summary>
        /// 当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息 
        /// </summary>
        WM_NCMBUTTONDBLCLK = 0x00A9,
        /// <summary>
        /// 
        /// </summary>
        WM_KEYFIRST = 0x0100,
        /// <summary>
        /// 按下一个键 
        /// </summary>
        WM_KEYDOWN = 0x0100,
        /// <summary>
        /// 释放一个键
        /// </summary>
        WM_KEYUP = 0x0101,
        /// <summary>
        /// 按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息 
        /// </summary>
        WM_CHAR = 0x0102,
        /// <summary>
        /// 当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口 
        /// </summary>
        WM_DEADCHAR = 0x0103,
        /// <summary>
        /// 当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口； 
        /// </summary>
        WM_SYSKEYDOWN = 0x0104,
        /// <summary>
        /// 当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口 
        /// </summary>
        WM_SYSKEYUP = 0x0105,
        /// <summary>
        /// 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口 
        /// </summary>
        WM_SYSCHAR = 0x0106,
        /// <summary>
        /// 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口 
        /// </summary>
        WM_SYSDEADCHAR = 0x0107,
        /// <summary>
        /// 
        /// </summary>
        WM_KEYLAST = 0x0108,
        /// <summary>
        /// 在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务 
        /// </summary>
        WM_INITDIALOG = 0x0110,
        /// <summary>
        /// 当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译 
        /// </summary>
        WM_COMMAND = 0x0111,
        /// <summary>
        /// 当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息 
        /// </summary>
        WM_SYSCOMMAND = 0x0112,
        /// <summary>
        /// 发生了定时器事件
        /// </summary>
        WM_TIMER = 0x0113,
        /// <summary>
        /// 当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件 
        /// </summary>
        WM_HSCROLL = 0x0114,
        /// <summary>
        /// 当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口，发送给拥有它的控件
        /// </summary>
        WM_VSCROLL = 0x0115,
        /// <summary>
        /// 当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单 
        /// </summary>
        wm_initmenu = 0x0116,
        /// <summary>
        /// 当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部
        /// </summary>
        WM_INITMENUPOPUP = 0x0117,
        /// <summary>
        /// 当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口） 
        /// </summary>
        WM_MENUSELECT = 0x011F,
        /// <summary>
        /// 当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者；
        /// </summary>
        WM_MENUCHAR = 0x0120,
        /// <summary>
        /// 当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载
        /// 状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待
        /// </summary>
        WM_ENTERIDLE = 0x0121,
        /// <summary>
        /// 
        /// </summary>
        WM_MENURBUTTONUP = 0x0122,
        /// <summary>
        /// 
        /// </summary>
        WM_MENUDRAG = 0x0123,
        /// <summary>
        /// 
        /// </summary>
        WM_MENUGETOBJECT = 0x0124,
        /// <summary>
        /// 
        /// </summary>
        WM_UNINITMENUPOPUP = 0x0125,
        /// <summary>
        /// 
        /// </summary>
        WM_MENUCOMMAND = 0x0126,
        /// <summary>
        /// 
        /// </summary>
        WM_CHANGEUISTATE = 0x0127,
        /// <summary>
        /// 
        /// </summary>
        WM_UPDATEUISTATE = 0x0128,
        /// <summary>
        /// 
        /// </summary>
        WM_QUERYUISTATE = 0x0129,
        /// <summary>
        /// 在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息， 所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色 
        /// </summary>
        WM_CTLCOLORMSGBOX = 0x0132,
        /// <summary>
        /// 当一个编辑型控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色 
        /// </summary>
        WM_CTLCOLOREDIT = 0x0133,
        /// <summary>
        /// 当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色 
        /// </summary>
        WM_CTLCOLORLISTBOX = 0x0134,
        /// <summary>
        /// 当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色 
        /// </summary>
        WM_CTLCOLORBTN = 0x0135,
        /// <summary>
        /// 当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色 
        /// </summary>
        WM_CTLCOLORDLG = 0x0136,
        /// <summary>
        /// 当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色 
        /// </summary>
        WM_CTLCOLORSCROLLBAR = 0x0137,
        /// <summary>
        /// 当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以 
        /// 通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色 
        /// </summary>
        WM_CTLCOLORSTATIC = 0x0138,
        /// <summary>
        /// 
        /// </summary>
        WM_MOUSEFIRST = 0x0200,
        /// <summary>
        /// 移动鼠标
        /// </summary>
        WM_MOUSEMOVE = 0x0200,
        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        WM_LBUTTONDOWN = 0x0201,
        /// <summary>
        /// 释放鼠标左键 
        /// </summary>
        WM_LBUTTONUP = 0x0202,
        /// <summary>
        /// 双击鼠标左键 
        /// </summary>
        WM_LBUTTONDBLCLK = 0x0203,
        /// <summary>
        /// 按下鼠标右键 
        /// </summary>
        WM_RBUTTONDOWN = 0x0204,
        /// <summary>
        /// 释放鼠标右键
        /// </summary>
        WM_RBUTTONUP = 0x0205,
        /// <summary>
        /// 双击鼠标右键
        /// </summary>
        WM_RBUTTONDBLCLK = 0x0206,
        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        WM_MBUTTONDOWN = 0x0207,
        /// <summary>
        /// 释放鼠标中键 
        /// </summary>
        WM_MBUTTONUP = 0x0208,
        /// <summary>
        /// 双击鼠标中键 
        /// </summary>
        WM_MBUTTONDBLCLK = 0x0209,
        /// <summary>
        /// 当鼠标轮子转动时发送此消息给当前有焦点的控件 
        /// </summary>
        WM_MOUSEWHEEL = 0x020A,
        /// <summary>
        /// 
        /// </summary>
        WM_MOUSELAST = 0x020A,
        /// <summary>
        /// 当MDI子窗口被创建或被销毁，或用户按了一下鼠标键而光标在子窗口上时发送此消息给它的父窗口 
        /// </summary>
        WM_PARENTNOTIFY = 0x0210,
        /// <summary>
        /// 发送此消息通知应用程序的主窗口that已经进入了菜单循环模式 
        /// </summary>
        WM_ENTERMENULOOP = 0x0211,
        /// <summary>
        /// 发送此消息通知应用程序的主窗口that已退出了菜单循环模式 
        /// </summary>
        WM_EXITMENULOOP = 0x0212,
        /// <summary>
        /// 
        /// </summary>
        WM_NEXTMENU = 0x0213,
        /// <summary>
        /// 当用户正在调整窗口大小时发送此消息给窗口；通过此消息应用程序可以监视窗口大小和位置也可以修改他们 
        /// </summary>
        WM_SIZING = 0x0214,
        /// <summary>
        /// 发送此消息 给窗口当它失去捕获的鼠标时；
        /// </summary>
        WM_CAPTURECHANGED = 0x0215,
        /// <summary>
        /// 当用户在移动窗口时发送此消息，通过此消息应用程序可以监视窗口大小和位置也可以修改他们； 
        /// </summary>
        WM_MOVING = 0x0216,
        /// <summary>
        /// 此消息发送给应用程序来通知它有关电源管理事件； 
        /// </summary>
        WM_POWERBROADCAST = 0x0218,
        /// <summary>
        /// 当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序 
        /// </summary>
        WM_DEVICECHANGE = 0x0219,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_STARTCOMPOSITION = 0x010D,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_ENDCOMPOSITION = 0x010E,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_COMPOSITION = 0x010F,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_KEYLAST = 0x010F,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_SETCONTEXT = 0x0281,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_NOTIFY = 0x0282,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_CONTROL = 0x0283,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_COMPOSITIONFULL = 0x0284,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_SELECT = 0x0285,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_CHAR = 0x0286,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_REQUEST = 0x0288,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_KEYDOWN = 0x0290,
        /// <summary>
        /// 
        /// </summary>
        WM_IME_KEYUP = 0x0291,
        /// <summary>
        /// 应用程序发送此消息给多文档的客户窗口来创建一个MDI 子窗口 
        /// </summary>
        WM_MDICREATE = 0x0220,
        /// <summary>
        /// 应用程序发送此消息给多文档的客户窗口来关闭一个MDI 子窗口 
        /// </summary>
        WM_MDIDESTROY = 0x0221,
        /// <summary>
        /// 应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到 
        /// 此消息后，它发出WM_MDIACTIVE消息给MDI子窗口（未激活）激活它； 
        /// </summary>
        WM_MDIACTIVATE = 0x0222,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口让子窗口从最大最小化恢复到原来大小 
        /// </summary>
        WM_MDIRESTORE = 0x0223,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口激活下一个或前一个窗口 
        /// </summary>
        WM_MDINEXT = 0x0224,
        /// <summary>
        /// 程序发送此消息给MDI客户窗口来最大化一个MDI子窗口； 
        /// </summary>
        WM_MDIMAXIMIZE = 0x0225,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口 
        /// </summary>
        WM_MDITILE = 0x0226,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口 
        /// </summary>
        WM_MDICASCADE = 0x0227,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口 
        /// </summary>
        WM_MDIICONARRANGE = 0x0228,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口来找到激活的子窗口的句柄 
        /// </summary>
        WM_MDIGETACTIVE = 0x0229,
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单 
        /// </summary>
        WM_MDISETMENU = 0x0230,
        /// <summary>
        /// 
        /// </summary>
        WM_ENTERSIZEMOVE = 0x0231,
        /// <summary>
        /// 
        /// </summary>
        WM_EXITSIZEMOVE = 0x0232,
        /// <summary>
        /// 
        /// </summary>
        WM_DROPFILES = 0x0233,
        /// <summary>
        /// 
        /// </summary>
        WM_MDIREFRESHMENU = 0x0234,
        /// <summary>
        /// 
        /// </summary>
        WM_MOUSEHOVER = 0x02A1,
        /// <summary>
        /// 
        /// </summary>
        WM_MOUSELEAVE = 0x02A3,
        /// <summary>
        /// 程序发送此消息给一个编辑框或combobox来删除当前选择的文本 
        /// </summary>
        WM_CUT = 0x0300,
        /// <summary>
        /// 程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板 
        /// </summary>
        WM_COPY = 0x0301,
        /// <summary>
        /// 程序发送此消息给editcontrol或combobox从剪贴板中得到数据 
        /// </summary>
        WM_PASTE = 0x0302,
        /// <summary>
        /// 程序发送此消息给editcontrol或combobox清除当前选择的内容；
        /// </summary>
        WM_CLEAR = 0x0303,
        /// <summary>
        /// 程序发送此消息给editcontrol或combobox撤消最后一次操作
        /// </summary>
        WM_UNDO = 0x0304,
        /// <summary>
        /// 
        /// </summary>
        WM_RENDERformAT = 0x0305,
        /// <summary>
        /// 
        /// </summary>
        WM_RENDERALLformATS = 0x0306,
        /// <summary>
        /// 当调用ENPTYCLIPBOARD函数时 发送此消息给剪贴板的所有者 
        /// </summary>
        WM_DESTROYCLIPBOARD = 0x0307,
        /// <summary>
        /// 当剪贴板的内容变化时发送此消息给剪贴板观察链的第一个窗口；它允许用剪贴板观察窗口来显示剪贴板的新内容； 
        /// </summary>
        WM_DRAWCLIPBOARD = 0x0308,
        /// <summary>
        /// 当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区需要重画； 
        /// </summary>
        WM_PAINTCLIPBOARD = 0x0309,
        /// <summary>
        /// 
        /// </summary>
        WM_VSCROLLCLIPBOARD = 0x030A,
        /// <summary>
        /// 当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区域的大小已经改变是此消息通过剪
        /// 贴板观察窗口发送给剪贴板的所有者； 
        /// </summary>
        WM_SIZECLIPBOARD = 0x030B,
        /// <summary>
        /// 通过剪贴板观察窗口发送此消息给剪贴板的所有者来请求一个CF_OWNERDISPLAY格式的剪贴板的名字 
        /// </summary>
        WM_ASKCBformATNAME = 0x030C,
        /// <summary>
        /// 当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链的第一个窗口；
        /// </summary>
        WM_CHANGECBCHAIN = 0x030D,
        /// <summary>
        /// 此消息通过一个剪贴板观察窗口发送给剪贴板的所有者 ；它发生在当剪贴板包含CFOWNERDISPALY格式的数据
        /// 并且有个事件在剪贴板观察窗的水平滚动条上；所有者应滚动剪贴板图象并更新滚动条的值； 
        /// </summary>
        WM_HSCROLLCLIPBOARD = 0x030E,
        /// <summary>
        /// 此消息发送给将要收到焦点的窗口，此消息能使窗口在收到焦点时同时有机会实现他的逻辑调色板 
        /// </summary>
        WM_QUERYNEWPALETTE = 0x030F,
        /// <summary>
        /// 当一个应用程序正要实现它的逻辑调色板时发此消息通知所有的应用程序 
        /// </summary>
        WM_PALETTEISCHANGING = 0x0310,
        /// <summary>
        /// 此消息在一个拥有焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此来改变系统调色板 
        /// </summary>
        WM_PALETTECHANGED = 0x0311,
        /// <summary>
        /// 当用户按下由REGISTERHOTKEY函数注册的热键时提交此消息 
        /// </summary>
        WM_HOTKEY = 0x0312,
        /// <summary>
        /// 应用程序发送此消息仅当WINDOWS或其它应用程序发出一个请求要求绘制一个应用程序的一部分； 
        /// </summary>
        WM_PRINT = 0x0317,
        /// <summary>
        /// 
        /// </summary>
        WM_PRINTCLIENT = 0x0318,
        /// <summary>
        /// 
        /// </summary>
        WM_HANDHELDFIRST = 0x0358,
        /// <summary>
        /// 
        /// </summary>
        WM_HANDHELDLAST = 0x035F,
        /// <summary>
        /// 
        /// </summary>
        WM_PENWINFIRST = 0x0380,
        /// <summary>
        /// 
        /// </summary>
        WM_PENWINLAST = 0x038F,
        /// <summary>
        /// 
        /// </summary>
        WM_COALESCE_FIRST = 0x0390,
        /// <summary>
        /// 
        /// </summary>
        WM_COALESCE_LAST = 0x039F,
        /// <summary>
        /// 
        /// </summary>
        WM_DDE_FIRST = 0x03E0,
        /// <summary>
        /// 
        /// </summary>
        WM_THEMECHNAGED = 0x31A
    }

    /// <summary>
    /// Windows 窗口样式
    /// </summary>
    [Flags]
    public enum WindowStyle : int
    {
        /// <summary>
        /// 
        /// </summary>
        WS_OVERLAPPED = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        WS_POPUP = unchecked((int)0x80000000),
        /// <summary>
        /// 
        /// </summary>
        WS_CHILD = 0x40000000,
        /// <summary>
        /// 
        /// </summary>
        WS_MINIMIZE = 0x20000000,
        /// <summary>
        /// 
        /// </summary>
        WS_VISIBLE = 0x10000000,
        /// <summary>
        /// 
        /// </summary>
        WS_DISABLED = 0x08000000,
        /// <summary>
        /// 
        /// </summary>
        WS_CLIPSIBLINGS = 0x04000000,
        /// <summary>
        /// 
        /// </summary>
        WS_CLIPCHILDREN = 0x02000000,
        /// <summary>
        /// 
        /// </summary>
        WS_MAXIMIZE = 0x01000000,
        /// <summary>
        /// 
        /// </summary>
        WS_CAPTION = 0x00C00000,
        /// <summary>
        /// 
        /// </summary>
        WS_BORDER = 0x00800000,
        /// <summary>
        /// 
        /// </summary>
        WS_DLGFRAME = 0x00400000,
        /// <summary>
        /// 
        /// </summary>
        WS_VSCROLL = 0x00200000,
        /// <summary>
        /// 
        /// </summary>
        WS_HSCROLL = 0x00100000,
        /// <summary>
        /// 
        /// </summary>
        WS_SYSMENU = 0x00080000,
        /// <summary>
        /// 
        /// </summary>
        WS_THICKFRAME = 0x00040000,
        /// <summary>
        /// 
        /// </summary>
        WS_GROUP = 0x00020000,
        /// <summary>
        /// 
        /// </summary>
        WS_TABSTOP = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        WS_MINIMIZEBOX = 0x00020000,
        /// <summary>
        /// 
        /// </summary>
        WS_MAXIMIZEBOX = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        WS_TILED = WS_OVERLAPPED,
        /// <summary>
        /// 
        /// </summary>
        WS_ICONIC = WS_MINIMIZE,
        /// <summary>
        /// 
        /// </summary>
        WS_SIZEBOX = WS_THICKFRAME,
        /// <summary>
        /// 
        /// </summary>
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        /// <summary>
        /// 
        /// </summary>
        WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
                                WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
        /// <summary>
        /// 
        /// </summary>
        WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
        /// <summary>
        /// 
        /// </summary>
        WS_CHILDWINDOW = (WS_CHILD)
    }

    /// <summary>
    /// 定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举使用数值）
    /// </summary>
    [Flags]
    public enum KeyModifiers : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        Alt = 1,
        /// <summary>
        /// 
        /// </summary>
        Ctrl = 2,
        /// <summary>
        /// 
        /// </summary>
        Shift = 4,
        /// <summary>
        /// 
        /// </summary>
        WindowsKey = 8
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ImageListDrawFlags : int
    {
        /// <summary>
        /// 
        /// </summary>
        ILD_NORMAL = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        ILD_TRANSPARENT = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        ILD_BLEND25 = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        ILD_FOCUS = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        ILD_BLEND50 = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        ILD_SELECTED = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        ILD_BLEND = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        ILD_MASK = 0x00000010,
        /// <summary>
        /// 
        /// </summary>
        ILD_IMAGE = 0x00000020,
        /// <summary>
        /// 
        /// </summary>
        ILD_ROP = 0x00000040,
        /// <summary>
        /// 
        /// </summary>
        ILD_OVERLAYMASK = 0x00000F00,
        /// <summary>
        /// 
        /// </summary>
        ILD_PRESERVEALPHA = 0x00001000,
        /// <summary>
        /// 
        /// </summary>
        ILD_SCALE = 0x00002000,
        /// <summary>
        /// 
        /// </summary>
        ILD_DPISCALE = 0x00004000,
        /// <summary>
        /// 
        /// </summary>
        ILD_ASYNC = 0x00008000
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ImageListColorFlags : uint
    {
        /// <summary>
        /// 
        /// </summary>
        CLR_NONE = 0xFFFFFFFF,
        /// <summary>
        /// 
        /// </summary>
        CLR_DEFAULT = 0xFF000000,
        /// <summary>
        /// 
        /// </summary>
        CLR_HILIGHT = CLR_DEFAULT,
    }

    /// <summary>
    /// 挂钩处理过程的类型
    /// </summary>
    public enum HookType
    {
        /// <summary>
        /// 安装一个挂钩处理过程, 以监视由对话框、消息框、菜单条、或滚动条中的输入事件引发的消息
        /// </summary>
        WH_MSGFILTER = -1,
        /// <summary>
        /// 安装一个挂钩处理过程,对寄送至系统消息队列的输入消息进行纪录
        /// </summary>
        WH_JOURNALRECORD = 0,
        /// <summary>
        /// 安装一个挂钩处理过程,对此前由WH_JOURNALRECORD 挂钩处理过程纪录的消息进行寄送
        /// </summary>
        WH_JOURNALPLAYBACK = 1,
        /// <summary>
        /// 安装一个挂钩处理过程对击键消息进行监视
        /// </summary>
        WH_KEYBORARD = 2,
        /// <summary>
        /// 安装一个挂钩处理过程对寄送至消息队列的消息进行监视
        /// </summary>
        WH_GETMESSAGE = 3,
        /// <summary>
        /// 安装一个挂钩处理过程,在系统将消息发送至目标窗口处理过程之前,对该消息进行监视
        /// </summary>
        WH_CALLWNDPROC = 4,
        /// <summary>
        /// 安装一个挂钩处理过程,接受对CBT应用程序有用的消息
        /// </summary>
        WH_CBT = 5,
        /// <summary>
        /// <para>安装一个挂钩处理过程,以监视由对话框、消息框、菜单条、或滚动条中</para>
        /// <para>的输入事件引发的消息.这个挂钩处理过程对系统中所有应用程序的这类</para>
        /// <para>消息都进行监视</para>
        /// </summary>
        WH_SYSMSGFILTER = 6,
        /// <summary>
        /// 安装一个挂钩处理过程,对鼠标消息进行监视
        /// </summary>
        WH_MOUSE = 7,
        /// <summary>
        /// 安装一个挂钩处理过程以便对其他挂钩处理过程进行调试
        /// </summary>
        WH_DEBUG = 9,
        /// <summary>
        /// 安装一个挂钩处理过程以接受对外壳应用程序有用的通知
        /// </summary>
        WH_SHELL = 10,
        /// <summary>
        /// <para>安装一个挂钩处理过程,该挂钩处理过程当应用程序的前台线程即将</para>
        /// <para>进入空闲状态时被调用,它有助于在空闲时间内执行低优先级的任务</para>
        /// </summary>
        WH_FOREGROUNDIDLE = 11,
        /// <summary>
        /// 安装一个挂钩处理过程,它对已被目标窗口处理过程处理过了的消息进行监视
        /// </summary>
        WH_CALLWNDPROCRET = 12,
        /// <summary>
        /// 此挂钩只能在Windows NT中被安装,用来对底层的键盘输入事件进行监视
        /// </summary>
        WH_KEYBORARD_LL = 13,
        /// <summary>
        /// 此挂钩只能在Windows NT中被安装,用来对底层的鼠标输入事件进行监视
        /// </summary>
        WH_MOUSE_LL = 14,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SHGFI
    {
        /// <summary>
        /// 获取图标
        /// </summary>
        SHGFI_ICON = 0x000000100,
        /// <summary>
        /// 获取显示名
        /// </summary>
        SHGFI_DISPLAYNAM = 0x000000200,
        /// <summary>
        /// 获取类型名
        /// </summary>
        SHGFI_TYPENAME = 0x000000400,
        /// <summary>
        /// 获取属性
        /// </summary>
        SHGFI_ATTRIBUTES = 0x000000800,
        /// <summary>
        /// 获取图标位置
        /// </summary>
        SHGFI_ICONLOCATION = 0x000001000,
        /// <summary>
        /// 返回可执行文件的类型
        /// </summary>
        SHGFI_EXETYPE = 0x000002000,
        /// <summary>
        /// 获取系统图标索引
        /// </summary>
        SHGFI_SYSICONINDEX = 0x000004000,
        /// <summary>
        /// 把一个链接覆盖在图标
        /// </summary>
        SHGFI_LINKOVERLAY = 0x000008000,
        /// <summary>
        /// 显示图标在选中时的状态
        /// </summary>
        SHGFI_SELECTED = 0x000010000,
        /// <summary>
        /// 只能指定属性
        /// </summary>
        SHGFI_ATTR_SPECIFIED = 0x000020000,
        /// <summary>
        /// 获取大图标
        /// </summary>
        SHGFI_LARGEICON = 0x000000000,
        /// <summary>
        /// 获取小图标
        /// </summary>
        SHGFI_SMALLICON = 0x000000001,
        /// <summary>
        /// 修改SHGFI_ICON,导致函数来检索文件的打开图标
        /// </summary>
        SHGFI_OPENICON = 0x000000002,
        /// <summary>
        /// 修改SHGFI_ICON,导致函数来检索一个shell大小的图标。如果这个标志没有指定函数大小图标根据系统度量值。
        /// 注意：这个标志不支持Windows手机设备
        /// </summary>
        SHGFI_SHELLICONSIZE = 0x000000004,
        /// <summary>
        /// 
        /// </summary>
        SHGFI_PIDL = 0x000000008,
        /// <summary>
        /// 通过使用dwFileAttributes
        /// </summary>
        SHGFI_USEFILEATTRIBUTES = 0x000000010,
        /// <summary>
        /// 应用适当的覆盖
        /// </summary>
        SHGFI_ADDOVERLAYS = 0x000000020,
        /// <summary>
        /// 获得该指数的叠加
        /// </summary>
        SHGFI_OVERLAYINDEX = 0x000000040,
    }

    /// <summary>
    /// 发送到一个窗口，以确定鼠标在窗口的哪一部分，对应于一个特定的屏幕坐标
    /// </summary>
    public enum WM_NCHITTEST : int
    {
        /// <summary>
        /// 在屏幕背景或窗口之间的分界线
        /// </summary>
        HTERROR = -2,
        /// <summary>
        /// 在目前一个窗口，其他窗口覆盖在同一个线程
        /// （该消息将被发送到相关窗口在同一个线程，直到其中一个返回一个代码，是不是HTTRANSPARENT）
        /// </summary>
        HTTRANSPARENT = -1,
        /// <summary>
        /// 在屏幕背景或窗口之间的分界线上
        /// </summary>
        HTNOWHERE = 0,
        /// <summary>
        /// 在客户端区域
        /// </summary>
        HTCLIENT = 1,
        /// <summary>
        /// 在标题栏
        /// </summary>
        HTCAPTION = 2,
        /// <summary>
        /// 在窗口菜单中，或在一个子窗口的关闭按钮
        /// </summary>
        HTSYSMENU = 3,
        /// <summary>
        /// 在大小框（与HTGROWBO相同）
        /// </summary>
        HTSIZE = 4,
        /// <summary>
        /// 在大小框（与HTSIZE相同）
        /// </summary>
        HTGROWBOX = 4,
        /// <summary>
        /// 在一个菜单
        /// </summary>
        HTMENU = 5,
        /// <summary>
        /// 在水平滚动条
        /// </summary>
        HTHSCROLL = 6,
        /// <summary>
        /// 在垂直滚动条
        /// </summary>
        HTVSCROLL = 7,
        /// <summary>
        /// 在最小化按钮
        /// </summary>
        HTREDUCE = 8,
        /// <summary>
        /// 在最小化按钮
        /// </summary>
        HTMINBUTTON = 8,
        /// <summary>
        /// 在最大化按钮
        /// </summary>
        HTMAXBUTTON = 9,
        /// <summary>
        /// 在最大化按钮
        /// </summary>
        HTZOOM = 9,
        /// <summary>
        /// 在左边框可调整大小的窗口
        /// </summary>
        HTLEFT = 10,
        /// <summary>
        /// 在一个可调整大小的窗口的右边框
        /// </summary>
        HTRIGHT = 11,
        /// <summary>
        /// 在窗口的上边框水平线上
        /// </summary>
        HTTOP = 12,
        /// <summary>
        /// 在窗口的左上边框
        /// </summary>
        HTTOPLEFT = 13,
        /// <summary>
        /// 在窗口的右上边框
        /// </summary>
        HTTOPRIGHT = 14,
        /// <summary>
        /// （用户可以在较低的水平边界可调整大小的窗口单击鼠标，改变窗口的垂直大小）
        /// </summary>
        HTBOTTOM = 15,
        /// <summary>
        /// 在左下角的边框可调整大小的窗口（用户可以通过点击鼠标来调整窗口的大小，对角）
        /// </summary>
        HTBOTTOMLEFT = 16,
        /// <summary>
        /// 在右下角的边框可调整大小的窗口（用户可以通过点击鼠标来调整窗口的大小，对角）
        /// </summary>
        HTBOTTOMRIGHT = 17,
        /// <summary>
        /// 在一个不具有缩放边框的窗口
        /// </summary>
        HTBORDER = 18,
        /// <summary>
        /// 在关闭按钮
        /// </summary>
        HTCLOSE = 20,
        /// <summary>
        /// 在帮助按钮
        /// </summary>
        HTHELP = 21,
    }

    /// <summary>
    /// Windows 使用的256个虚拟键码
    /// </summary>
    public enum KEYS
    {
        /// <summary>
        /// 
        /// </summary>
        VK_LBUTTON = 0x1,
        /// <summary>
        /// 
        /// </summary>
        VK_RBUTTON = 0x2,
        /// <summary>
        /// 
        /// </summary>
        VK_CANCEL = 0x3,
        /// <summary>
        /// 
        /// </summary>
        VK_MBUTTON = 0x4,
        /// <summary>
        /// 
        /// </summary>
        VK_BACK = 0x8,
        /// <summary>
        /// 
        /// </summary>
        VK_TAB = 0x9,
        /// <summary>
        /// 
        /// </summary>
        VK_CLEAR = 0xC,
        /// <summary>
        /// 
        /// </summary>
        VK_RETURN = 0xD,
        /// <summary>
        /// 
        /// </summary>
        VK_SHIFT = 0x10,
        /// <summary>
        /// 
        /// </summary>
        VK_CONTROL = 0x11,
        /// <summary>
        /// 
        /// </summary>
        VK_MENU = 0x12,
        /// <summary>
        /// 
        /// </summary>
        VK_PAUSE = 0x13,
        /// <summary>
        /// 
        /// </summary>
        VK_CAPITAL = 0x14,
        /// <summary>
        /// 
        /// </summary>
        VK_ESCAPE = 0x1B,
        /// <summary>
        /// 
        /// </summary>
        VK_SPACE = 0x20,
        /// <summary>
        /// 
        /// </summary>
        VK_PRIOR = 0x21,
        /// <summary>
        /// 
        /// </summary>
        VK_NEXT = 0x22,
        /// <summary>
        /// 
        /// </summary>
        VK_END = 0x23,
        /// <summary>
        /// 
        /// </summary>
        VK_HOME = 0x24,
        /// <summary>
        /// 
        /// </summary>
        VK_LEFT = 0x25,
        /// <summary>
        /// 
        /// </summary>
        VK_UP = 0x26,
        /// <summary>
        /// 
        /// </summary>
        VK_RIGHT = 0x27,
        /// <summary>
        /// 
        /// </summary>
        VK_DOWN = 0x28,
        /// <summary>
        /// 
        /// </summary>
        VK_Select = 0x29,
        /// <summary>
        /// 
        /// </summary>
        VK_PRINT = 0x2A,
        /// <summary>
        /// 
        /// </summary>
        VK_EXECUTE = 0x2B,
        /// <summary>
        /// 
        /// </summary>
        VK_SNAPSHOT = 0x2C,
        /// <summary>
        /// 
        /// </summary>
        VK_Insert = 0x2D,
        /// <summary>
        /// 
        /// </summary>
        VK_Delete = 0x2E,
        /// <summary>
        /// 
        /// </summary>
        VK_HELP = 0x2F,
        /// <summary>
        /// 
        /// </summary>
        VK_0 = 0x30,
        /// <summary>
        /// 
        /// </summary>
        VK_1 = 0x31,
        /// <summary>
        /// 
        /// </summary>
        VK_2 = 0x32,
        /// <summary>
        /// 
        /// </summary>
        VK_3 = 0x33,
        /// <summary>
        /// 
        /// </summary>
        VK_4 = 0x34,
        /// <summary>
        /// 
        /// </summary>
        VK_5 = 0x35,
        /// <summary>
        /// 
        /// </summary>
        VK_6 = 0x36,
        /// <summary>
        /// 
        /// </summary>
        VK_7 = 0x37,
        /// <summary>
        /// 
        /// </summary>
        VK_8 = 0x38,
        /// <summary>
        /// 
        /// </summary>
        VK_9 = 0x39,
        /// <summary>
        /// 
        /// </summary>
        VK_A = 0x41,
        /// <summary>
        /// 
        /// </summary>
        VK_B = 0x42,
        /// <summary>
        /// 
        /// </summary>
        VK_C = 0x43,
        /// <summary>
        /// 
        /// </summary>
        VK_D = 0x44,
        /// <summary>
        /// 
        /// </summary>
        VK_E = 0x45,
        /// <summary>
        /// 
        /// </summary>
        VK_F = 0x46,
        /// <summary>
        /// 
        /// </summary>
        VK_G = 0x47,
        /// <summary>
        /// 
        /// </summary>
        VK_H = 0x48,
        /// <summary>
        /// 
        /// </summary>
        VK_I = 0x49,
        /// <summary>
        /// 
        /// </summary>
        VK_J = 0x4A,
        /// <summary>
        /// 
        /// </summary>
        VK_K = 0x4B,
        /// <summary>
        /// 
        /// </summary>
        VK_L = 0x4C,
        /// <summary>
        /// 
        /// </summary>
        VK_M = 0x4D,
        /// <summary>
        /// 
        /// </summary>
        VK_N = 0x4E,
        /// <summary>
        /// 
        /// </summary>
        VK_O = 0x4F,
        /// <summary>
        /// 
        /// </summary>
        VK_P = 0x50,
        /// <summary>
        /// 
        /// </summary>
        VK_Q = 0x51,
        /// <summary>
        /// 
        /// </summary>
        VK_R = 0x52,
        /// <summary>
        /// 
        /// </summary>
        VK_S = 0x53,
        /// <summary>
        /// 
        /// </summary>
        VK_T = 0x54,
        /// <summary>
        /// 
        /// </summary>
        VK_U = 0x55,
        /// <summary>
        /// 
        /// </summary>
        VK_V = 0x56,
        /// <summary>
        /// 
        /// </summary>
        VK_W = 0x57,
        /// <summary>
        /// 
        /// </summary>
        VK_X = 0x58,
        /// <summary>
        /// 
        /// </summary>
        VK_Y = 0x59,
        /// <summary>
        /// 
        /// </summary>
        VK_Z = 0x5A,
        /// <summary>
        /// 
        /// </summary>
        VK_STARTKEY = 0x5B,
        /// <summary>
        /// 
        /// </summary>
        VK_CONTEXTKEY = 0x5D,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD0 = 0x60,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD1 = 0x61,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD2 = 0x62,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD3 = 0x63,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD4 = 0x64,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD5 = 0x65,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD6 = 0x66,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD7 = 0x67,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD8 = 0x68,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMPAD9 = 0x69,
        /// <summary>
        /// 
        /// </summary>
        VK_MULTIPLY = 0x6A,
        /// <summary>
        /// 
        /// </summary>
        VK_ADD = 0x6B,
        /// <summary>
        /// 
        /// </summary>
        VK_SEPARATOR = 0x6C,
        /// <summary>
        /// 
        /// </summary>
        VK_SUBTRACT = 0x6D,
        /// <summary>
        /// 
        /// </summary>
        VK_DECIMAL = 0x6E,
        /// <summary>
        /// 
        /// </summary>
        VK_DIVIDE = 0x6F,
        /// <summary>
        /// 
        /// </summary>
        VK_F1 = 0x70,
        /// <summary>
        /// 
        /// </summary>
        VK_F2 = 0x71,
        /// <summary>
        /// 
        /// </summary>
        VK_F3 = 0x72,
        /// <summary>
        /// 
        /// </summary>
        VK_F4 = 0x73,
        /// <summary>
        /// 
        /// </summary>
        VK_F5 = 0x74,
        /// <summary>
        /// 
        /// </summary>
        VK_F6 = 0x75,
        /// <summary>
        /// 
        /// </summary>
        VK_F7 = 0x76,
        /// <summary>
        /// 
        /// </summary>
        VK_F8 = 0x77,
        /// <summary>
        /// 
        /// </summary>
        VK_F9 = 0x78,
        /// <summary>
        /// 
        /// </summary>
        VK_F10 = 0x79,
        /// <summary>
        /// 
        /// </summary>
        VK_F11 = 0x7A,
        /// <summary>
        /// 
        /// </summary>
        VK_F12 = 0x7B,
        /// <summary>
        /// 
        /// </summary>
        VK_F13 = 0x7C,
        /// <summary>
        /// 
        /// </summary>
        VK_F14 = 0x7D,
        /// <summary>
        /// 
        /// </summary>
        VK_F15 = 0x7E,
        /// <summary>
        /// 
        /// </summary>
        VK_F16 = 0x7F,
        /// <summary>
        /// 
        /// </summary>
        VK_F17 = 0x80,
        /// <summary>
        /// 
        /// </summary>
        VK_F18 = 0x81,
        /// <summary>
        /// 
        /// </summary>
        VK_F19 = 0x82,
        /// <summary>
        /// 
        /// </summary>
        VK_F20 = 0x83,
        /// <summary>
        /// 
        /// </summary>
        VK_F21 = 0x84,
        /// <summary>
        /// 
        /// </summary>
        VK_F22 = 0x85,
        /// <summary>
        /// 
        /// </summary>
        VK_F23 = 0x86,
        /// <summary>
        /// 
        /// </summary>
        VK_F24 = 0x87,
        /// <summary>
        /// 
        /// </summary>
        VK_NUMLOCK = 0x90,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_SCROLL = 0x91,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_1 = 0xBA,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_PLUS = 0xBB,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_COMMA = 0xBC,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_MINUS = 0xBD,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_PERIOD = 0xBE,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_2 = 0xBF,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_3 = 0xC0,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_4 = 0xDB,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_5 = 0xDC,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_6 = 0xDD,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_7 = 0xDE,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_8 = 0xDF,
        /// <summary>
        /// 
        /// </summary>
        VK_ICO_F17 = 0xE0,
        /// <summary>
        /// 
        /// </summary>
        VK_ICO_F18 = 0xE1,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM102 = 0xE2,
        /// <summary>
        /// 
        /// </summary>
        VK_ICO_HELP = 0xE3,
        /// <summary>
        /// 
        /// </summary>
        VK_ICO_00 = 0xE4,
        /// <summary>
        /// 
        /// </summary>
        VK_ICO_CLEAR = 0xE6,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_RESET = 0xE9,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_JUMP = 0xEA,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_PA1 = 0xEB,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_PA2 = 0xEC,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_PA3 = 0xED,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_WSCTRL = 0xEE,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_CUSEL = 0xEF,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_ATTN = 0xF0,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_FINNISH = 0xF1,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_COPY = 0xF2,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_AUTO = 0xF3,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_ENLW = 0xF4,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_BACKTAB = 0xF5,
        /// <summary>
        /// 
        /// </summary>
        VK_ATTN = 0xF6,
        /// <summary>
        /// 
        /// </summary>
        VK_CRSEL = 0xF7,
        /// <summary>
        /// 
        /// </summary>
        VK_EXSEL = 0xF8,
        /// <summary>
        /// 
        /// </summary>
        VK_EREOF = 0xF9,
        /// <summary>
        /// 
        /// </summary>
        VK_PLAY = 0xFA,
        /// <summary>
        /// 
        /// </summary>
        VK_ZOOM = 0xFB,
        /// <summary>
        /// 
        /// </summary>
        VK_NONAME = 0xFC,
        /// <summary>
        /// 
        /// </summary>
        VK_PA1 = 0xFD,
        /// <summary>
        /// 
        /// </summary>
        VK_OEM_CLEAR = 0xFE,
    }
}
