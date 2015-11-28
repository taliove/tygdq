using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //正则
using System.Collections;
using System.Globalization;
using System.Reflection;
using WindowsFormsApplication2.编码提示;

namespace WindowsFormsApplication2
{
    public class Glob
    { //一些全局变量
        private const string _ver = "0.94";
        public static string Ver = ".16";

        public static string Form = "添雨跟打器v" + _ver;
        public static string Instration = " t46 [看打版]"; //尾发送字符
        public static string VerInstance = _ver + Ver;
        //public static int su = 0;//测试用的
        public static string BianMa = ""; //编码查询
        /// <summary>
        /// 编码码表
        /// </summary>
        public static List<List<string>> BmTips = new List<List<string>>();

        //控制类
        public static bool notShowjs = false;//显示
        public static bool getStyle = false;//默认鼠标

        /// <summary>
        /// 当前段
        /// </summary>
        public static string Pre_Cout = "1225";//当前段号
        
        public static int Time = 0;
        public static string[,] GetWin = new string[40, 2];//获取的窗口
        public static int GetWinC = 0;//获取窗口的总数
        public static int WinSwitch = 0;//窗口切换控制
        public static string Text;//跟打文字*****
        public static string TypeText;//跟打文章
        public static int TypeTextCount = 0;//已跟打字数
        public static double TextSpeed; //以下为上次成绩
        public static double Textjj;
        public static double Textmc;
        public static string TextPreCout = "";//上次段号
        public static bool ReTypePD = false;//重打判断
        public static int TextLen; //总字数
        public static int TextJc = 0;//需要减去的数量
        public static int TextCz = 0; //错字
        public static int TextiCz = 0; //正字计数（用来    获取错字数量）
        public static int TextJs = 0; //键数

        public static int TextMc = 0;  //码长完美计数
        public static int TextMcc = 0; //完美计数总量

        public static int TextJj = 0; //击键
        public static ArrayList TextHgPlace = new ArrayList(); //显示回改地点
        public static int TextHgPlace_Skip = 0; //点击跳转的标记
        public static int TextBg = 0;//退格 + 回改量
        public static int 回车 = 0;
        public static int 选重 = 0;
        public static bool 是否选重 = true;
        public static bool 文段类型 = true; //真 为中文 假为英文
        public static int leftHand = 0;
        public static int rightHand = 0;
        public static int 撤销 = 0;
        public static int 撤销用量 = 0;
        public static double 速度限制 = 0.00;
        public static bool 是否速度限制 = false;
        //检查过程是否一直持续
        public static DateTime nowStart;
        //段数
        public static Match getDuan;
        public static Regex regexCout;
        //颜色 
        public static Color Right;
        public static Color False;

        public static Color r1Back;
        //峰值
        public static double MaxSpeed = 0;
        public static double MaxJj = 0;
        public static double MaxMc = 10; //码长


        public static int TextHg = 0; //回改
        public static int TextHgAll = 0; //总回改
        /// <summary>
        /// 回改用时
        /// </summary>
        public static double hgAllUse;
        public static int TextLenAll; //跟打的总字数
        /// <summary>
        /// 记录开始时的字数
        /// </summary>
        public static int TextRecLenAll;//
        /// <summary>
        /// 记录天数
        /// </summary>
        public static int TextRecDays = 0;
        /// <summary>
        /// 今日时间    
        /// </summary>
        public static string TodayDate;
        public static double TextHg_ = 0;//回改率

        public static string InstraPre = "";//个签
        public static string InstraPre_ = "";//是否启用了个签

        public static int LoadCount = 0;//载入次数 暂时是用来确定是否开启输入法
        public static double typeUseTime; //跟打用时
        public static int HaveTypeCount = 0;//已跟打段数
        public static int HaveTypeCount_ = 0;//实际跟打段数
        public static double TotalUse = 0;//总用时
        public static string InstraSrf = "";//输入法签名
        public static string InstraSrf_ = "";//是否启用了输入法签名

        public static int aTypeWords = 0;//打词

        public static Font font_1; //对照区字体大小
        public static Font font_2; //跟打区字体大小

        public static bool binput = true;
        public static int oneH; //一行高度
        public static int reTypeCount = 0;//重打次数

        /// <summary>
        /// 跟打效率
        /// </summary>
        public static int 效率 = 0;
        //发送的控制
        public static string sortSend = "ABCVDTSEFULGNOPRQ";
        public static int LastInput = 0;//末字错时不发送 可以继续跟打
        public static int DelaySend = 50;//打完发送延时
        public static bool sendOrNo = false;//是否 显示 发送框 默认 否
        public static bool GDQActon = false;//跟打完后 是否激活跟打器

        //跟打历史
        public static int TypeCount = 0;//跟打次数

        //发文标记
        public static int SendNow = 0;

        public static string PreText;//前导
        public static string PreDuan;//段标
        public static bool isZdy;//自定义开启

        public static string getName = "";//发文配置的名称

        //图表速度传递
        public static double chartSpeedTo = 0;
        public static bool chartShow = false;

        //表传递
        public static int Count = 0;

        //平均所有
        public static double Per_Speed = 0;//平均速度
        public static double Per_Jj = 0;//平均击键
        public static double Per_Mc = 0;//平均码长
        public static int Total_Type = 0;//跟打总字数

        //今日已跟打
        public static int todayTyping = 0;

        //比赛验证
        public static bool isMatch = false;//比赛

        //上一次跟打
        public static string theLastGoal = "";

        public static bool isQQ = false;
        public static string QQnumber;
        //随机段数
        public static int AZpre = 88;
        //错次
        public static int FalseCount = 0;
        public static ArrayList FWords = new ArrayList();
        public static int FWordsSkip = 0;//错字跳转标记

        //拖动条
        public static int p1;
        public static int p2;
        //曲线界面
        public static bool isShowSpline = false; //默认显示
        //停止用时
        public static int StopUse = 1;
        //曲线极值
        public static double MinSplite = 500;
        //极简模式
        public static bool simpleMoudle = false;
        public static string simpleSplite = "|";//分隔符
        public static bool jwMatchMoudle = false;//精五比赛模式
        //自动替换英转中
        public static bool autoReplaceBiaodian = false;
        //是否潜水
        public static bool isSub = false;
        //暂停次数
        public static int PauseTimes = 0;
        //击键比例
        public static int[] jjPer = new int[9];
        public static int jjAllC = 0;
        //对因剪切板问题导致的无法获取 采用自动手动功能
        public static bool F4Cut = false;//F4阻拦器
        //跟打地图
        public static Graphics Type_Map;
        public static Color Type_Map_Color = Color.Green;
        public static Color Type_map_C_1 = Color.FromArgb(220,220,220);
        public static int Type_Map_C = 200;
        public static int 地图长度 = 0;
        public static bool Type_Map_Level = true;//优先级

        //打开标记
        public static bool isPointIt = false;

        //作弊
        public static bool isCheat = false;

        //分析
        public static bool Use分析 = false;

        //测速点位置
        public static int[] SpeedPoint_ = new int[10];//测速点控制
        public static double[] SpeedTime = new double[10];//测速点时间控制
        public static int[] SpeedJs = new int[10];//键数
        public static int[] SpeedHg = new int[10];//回改
        public static int SpeedPointCount = 0;//测速点数量控制
        public static int SpeedControl = 0;

        //跟打报告
        public static List<TypeDate> TypeReport = new List<TypeDate>();

        //图片成绩发送昵称
        public static string PicName = "";

        public static string TextTime = "";

        //是否开启智能测词
        public static bool 是否智能测词 = false;
        public static List<BmAll> BmAlls = new List<BmAll>();
        public static double 词库理论码长 = 0;
        public static string 词组编码 = "";
        public static Color[] BmColors = new Color[] {Color.Blue,Color.Red,Color.Purple,Color.DeepPink};
    }

    /// <summary>
    /// 跟打报告
    /// </summary>
    public class TypeDate {
        /// <summary>
        /// 序
        /// </summary>
        public int Index { set; get; }
        /// <summary>
        /// 跟打起点
        /// </summary>
        public int Start { set; get; }
        /// <summary>
        /// 跟打终点
        /// </summary>
        public int End { set; get; }
        /// <summary>
        /// 跟打长度
        /// </summary>
        public int Length { set; get; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public double NowTime { set; get; }
        /// <summary>
        /// 总时间
        /// </summary>
        public double TotalTime { set; get; }
        /// <summary>
        /// 当前击键
        /// </summary>
        public int Tick { set; get; }
        /// <summary>
        /// 总击键
        /// </summary>
        public int TotalTick { set; get; }
    }
}
