using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//using System.Xml;
using System.IO;
using System.Text.RegularExpressions; //正则
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Security.Cryptography;
using System.Reflection;
using System.Drawing.Text;
//秒表
using IWshRuntimeLibrary;
using WindowsFormsApplication2.检查更新;
using WindowsFormsApplication2.编码提示;

//发送桌面的快捷方式

public delegate bool CallBack(int hwnd, int lParam);

namespace WindowsFormsApplication2
{
    public partial class Form1 : NewForm
    {
        public int[] HisSave = new int[2]; //得到每次输入的字符数量
        public int[] HisLine = new int[2]; //调整滚动条
        public int Sw = 0, sw = 0; //开关
        public DateTime sTime, eTime, startTime;
        public double ts;
        private Series SeriesSpeed = new Series("速度");
        public ChartArea ChartArea1 = new ChartArea();
        public Title title1 = new Title();
        private KeyBordHook KH = new KeyBordHook();
        public TimeSpan TimeStopAll = new TimeSpan();//暂停时间的累加
        private WordInfoUtil _wordInfoUtil = new WordInfoUtil();
        private RichEditBoxLineRender _render = new RichEditBoxLineRender();
        private FormBMTipsModel bmTips;//编码提示
        //private Stopwatch UseStopTime = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            int spX, spY;
            int spW, spH;
            /*
             this.Size = new Size(443,443);
            this.splitContainer1.Panel1Collapsed = false;
            this.splitContainer1.Panel2Collapsed = false;
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer3.Panel1Collapsed = false;
            this.splitContainer3.Panel2Collapsed = false;
            this.splitContainer3.SplitterDistance = 80;
            this.splitContainer4.Panel1Collapsed = false;
            this.splitContainer4.Panel2Collapsed = false;
            this.splitContainer4.SplitterDistance = 206;
            this.textBoxEx1.Focus();
             */
            spX = int.TryParse(IniRead("窗口位置", "横", "200"), out spX) ? spX < 0 ? 200 : spX : 200;
            spY = int.TryParse(IniRead("窗口位置", "纵", "200"), out spY) ? spY < 0 ? 200 : spY : 200;
            spW = int.TryParse(IniRead("窗口位置", "宽", "482"), out spW) ? spW < 200 ? 443 : spW : 443;
            spH = int.TryParse(IniRead("窗口位置", "高", "450"), out spH) ? spH < 50 ? 443 : spH : 443;
            Point pos = new Point(spX, spY);
            this.Location = pos;
            this.Size = new Size(spW, spH);
            //this.Show();
            bool t4;
            this.toolStripButton4.Checked = bool.TryParse(IniRead("程序控制", "详细信息", "True"), out t4) ? t4 : true;
            int p11H = int.TryParse(IniRead("拖动条", "高1", "142"), out p11H) ? p11H : 142;
            int p31H = int.TryParse(IniRead("拖动条", "高2", "89"), out p31H) ? p31H : 89;
            this.splitContainer1.SplitterDistance = p11H;
            this.splitContainer3.SplitterDistance = p31H;
            this.UIThread(LoadSetup);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //改变窗口标题
            this.Text = Glob.Form + Glob.Ver;
            Glob.Text = richTextBox1.Text;
            //Control.CheckForIllegalCrossThreadCalls = false;
            //Thread oThread = new Thread(new ThreadStart(this.LoadSetup));
            //oThread.Start();
            //oThread.Join();
            //oThread.Abort();
            //MessageBox.Show(oThread.ThreadState.ToString());
            RegisterHotKey(this.Handle, 2, (int)KeyModifiers.None, (Keys.F4)); //获取
            RegisterHotKey(this.Handle, 3, (int)KeyModifiers.None, (Keys.F3)); //重打
            RegisterHotKey(this.Handle, 4, (int)KeyModifiers.None, (Keys.F6)); //发文测试

            //RegisterHotKey(this.Handle, 6, (int)KeyModifiers.None, (Keys.F8)); //接收挑战
            F5();
            this.textBoxEx1.Select();
            try
            {
                KH.Start();
                KH.OnKeyDownEvent += new KeyEventHandler(KH_OnKeyDownEvent);
            }
            catch { this.lbl键准.Text = "NA"; }
            this.textBoxEx1.LostFocus += new System.EventHandler(textBoxEx1_LostFocus);
            //载入主题
            GetTheme();
            if (Theme.ThemeApply)
            {
                if (Theme.isBackBmp)
                    LoadTheme(Theme.ThemeBackBmp, Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
                else
                    LoadTheme("纯色", Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
            }
            else
            {
                LoadTheme("纯色", Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
                //采用默认的图片显示
                //LoadTheme("", Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
            }
        }

        //钩子退格
        void KH_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (sw != 0 && this.textBoxEx1.Focused)
            {
                Glob.TextMc++; //计数 用于计量回车及回车产生前的量
                int k = e.KeyValue;
                if (k == 8)
                {
                    Glob.TextBg++;
                }
                if (k >= 65 && k <= 71 || k >= 81 && k <= 84 || k == 88 || k == 90)
                { //左手键法
                    Glob.leftHand++;
                }
                else if (k >= 72 && k <= 80 || k == 85 || k == 89)
                { //右手键法
                    Glob.rightHand++;
                }
                else if (k == 13)
                {
                    Glob.回车++;
                    //触发回车时 计算
                    跟打地图步进++;
                    Type_Map(Color.HotPink, 跟打地图步进, 1);
                    Glob.TextMcc += Glob.TextMc;
                    Glob.TextMc = 0;

                }
                else if (k == 186 || k == 222 || k >= 48 && k <= 57)
                {
                    ; if (Glob.文段类型)
                        if (Glob.是否选重)
                        {
                            var s = richTextBox1.SelectionStart;
                            var text = s + 1 <= this.richTextBox1.TextLength ? this.richTextBox1.Text.Substring(s, 1) : "";
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                if (text == ";" || text == "'")
                                {
                                    System.Diagnostics.Debug.WriteLine("由于是打了标点，所以不计选重");
                                }
                                else
                                {
                                    Glob.选重++;
                                    System.Diagnostics.Debug.WriteLine("记录一次");
                                }
                            }
                        }
                }
            }
        }

        /// <summary>
        /// 获取主题数据
        /// </summary>
        public void GetTheme()
        {
            _Ini ini = new _Ini("Ttyping.ty");
            Theme.ThemeApply = bool.Parse(ini.IniReadValue("主题", "是否启用主题", "False"));
            Theme.isBackBmp = bool.Parse(ini.IniReadValue("主题", "是否应用主题背景", "False"));
            Theme.ThemeBackBmp = ini.IniReadValue("主题", "背景路径", "程序默认");
            Theme.ThemeBG = Color.FromArgb(int.Parse(ini.IniReadValue("主题", "纯色", "-13089719")));
            Theme.ThemeColorBG = Color.FromArgb(int.Parse(ini.IniReadValue("主题", "主题颜色", "-12365738")));//-12500671
            Theme.ThemeColorFC = Color.FromArgb(int.Parse(ini.IniReadValue("主题", "字体颜色", "-1")));
            Theme.ReView = bool.Parse(ini.IniReadValue("主题", "预览", "False"));
        }

        //图标及标题的设置
        /*
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawImage(this.Icon.ToBitmap(),0,0,24,24);
            Font F = new System.Drawing.Font("微软雅黑", 12f, FontStyle.Bold);
            Font F_ = new Font("微软雅黑", 9f);
            SizeF sf = g.MeasureString(Glob.Form, F);
            g.DrawString(Glob.Form, F, new SolidBrush(Theme.ThemeColorFC), 22, 2);
            if (Glob.Ver.Length != 0)
            {
                string s = "测试版本" + Glob.Ver;
                g.DrawString(s, F_, new SolidBrush(Theme.ThemeColorFC), sf.Width + 22, sf.Height - g.MeasureString(s,F_).Height + 2);
            }
        }
        */
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="BGround">背景设置的路径</param>
        /// <param name="BG">背景色</param>
        /// <param name="FC">前景色</param>
        /// <param name="BGr">纯色</param>
        public void LoadTheme(string BGround, Color BG, Color FC, Color BGr)
        {
            //载入图标或者颜色
            if (BGround != "")
            {
                if (BGround == "纯色")
                {
                    this.BackColor = BGr;
                    this.BackgroundImage = null;
                }
                else if (BGround == "程序默认")
                {
                    this.BackColor = Theme.ThemeBG;
                    try
                    {
                        this.BackgroundImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApplication2.Resources.3.jpg"));
                    }
                    catch
                    {
                        this.BackgroundImage = null;
                        ShowFlowText("未知错误，已采用纯色设置!");
                    }
                }
                else
                { //自定义
                    try
                    {
                        this.BackgroundImage = Image.FromFile(BGround);
                    }
                    catch
                    {
                        this.BackgroundImage = null;
                    }
                }
            }
            else
            {
                this.BackColor = Theme.ThemeBG;
                try
                {
                    this.BackgroundImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApplication2.Resources.3.jpg"));
                }
                catch
                {
                    this.BackgroundImage = null;
                }
            }
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            // this.mS1.ThemeColor = BG;
            // this.mS1.BackColor = BG;
            this.mS1.ForeColor = FC;
            this.dataGridView2.GridColor = BG;
            //this.toolStrip1.BackColor = BG;
            this.splitContainer1.BackColor = BG;
            this.splitContainer2.BackColor = BG;
            this.splitContainer3.BackColor = BG;
            this.splitContainer4.BackColor = BG;
            this.tableLayoutPanel2.BackColor = BG;
            this.tableLayoutPanel1.BackColor = FC;
            this.lblDuan.BackColor = BG;
            this.lblTitle.BackColor = BG;
            this.lblCount.BackColor = BG;
            this.lblQuan.BackColor = BG;
            this.lblSpeedText.BackColor = BG;
            this.lblJJText.BackColor = BG;
            this.lblMCText.BackColor = BG;
            this.lblMatchCount.BackColor = BG;

            this.dataGridView1.Rows[0].DefaultCellStyle.BackColor = BG;
            this.dataGridView1.Rows[0].DefaultCellStyle.ForeColor = FC;

            this.lblDuan.ForeColor = FC;
            this.lblTitle.ForeColor = FC;
            this.lblCount.ForeColor = FC;
            this.lblQuan.ForeColor = FC;
            this.lblSpeedText.ForeColor = FC;
            this.lblJJText.ForeColor = FC;
            this.lblMCText.ForeColor = FC;
            this.lblMatchCount.ForeColor = FC;

            this.TSMI1.ForeColor = FC;
            this.TSMI2.ForeColor = FC;
            this.TSMI3.ForeColor = FC;
            this.TSMI4.ForeColor = FC;
            this.TSMI5.ForeColor = FC;
            this.TSMI6.ForeColor = FC;

            //击键评定
            this.labelJiCheck.ForeColor = FC;
            this.labelCheckUD.ForeColor = FC;
            this.labelmcing.ForeColor = FC;

            Color C = new Color();
            C = Color.FromArgb(ColorTran(BG.R), ColorTran(BG.G), ColorTran(BG.B));
            this.labelSpeeding.BackColor = C;
            this.labelJjing.BackColor = C;
            this.labelJiCheck.BackColor = C;
            this.labelCheckUD.BackColor = C;
            this.labelmcing.BackColor = C;
            this.ForeColor = FC;
            Rectangle rect = new Rectangle(0, 0, 220, 24);
            this.Invalidate(rect, true);

        }
        private int ColorTran(int c)
        {
            if (c + 20 > 255)
                return 255;
            else return c + 20;
        }
        public void LoadSetup()
        {
            //创建表头
            this.dataGridView1.Rows.Add("序", "时间", "段", "速度", "击键", "码长", "回改", "错字", "键数", "字数", "打词", "用时", "群");
            this.dataGridView1.Rows[0].Frozen = true;
            this.dataGridView1.Rows[0].DefaultCellStyle.Font = new Font("微软雅黑", 11f);
            this.dataGridView1.Rows[0].DefaultCellStyle.BackColor = Theme.ThemeColorBG;
            this.dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Theme.ThemeColorFC;
            this.dataGridView1.Rows[0].Height = 20;
            //跟打地图
            Bitmap bmp_ = new Bitmap(this.picMap.ClientRectangle.Width, this.picMap.ClientRectangle.Height);
            this.picMap.Image = bmp_;
            //tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            this.chartSpeed.ChartAreas.Add(ChartArea1);
            this.ChartArea1.BackColor = Color.FromArgb(150, 150, 150);
            this.ChartArea1.AxisX.LineColor = Color.White;
            this.ChartArea1.AxisX.MajorGrid.LineColor = Color.FromArgb(127, 127, 127);
            this.ChartArea1.AxisX.MajorTickMark.LineColor = Color.FromArgb(10, 10, 35);
            this.ChartArea1.AxisX.LabelStyle.ForeColor = Color.Black;
            this.ChartArea1.AxisX2.LineDashStyle = ChartDashStyle.Dash;

            this.ChartArea1.AxisY.MajorGrid.LineColor = Color.FromArgb(127, 127, 127);
            this.ChartArea1.AxisY.MajorTickMark.LineColor = Color.Black;
            this.ChartArea1.AxisY.LabelStyle.ForeColor = Color.Black;
            this.ChartArea1.AxisY.LineColor = Color.White;

            Type type = dataGridView1.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView1, true, null);

            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel2, true, null);
            this.ChartArea1.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            this.ChartArea1.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            this.ChartArea1.AxisX.LabelAutoFitMaxFontSize = 7;
            this.ChartArea1.AxisY.LabelAutoFitMaxFontSize = 7;

            this.chartSpeed.Titles.Add(title1);
            this.title1.ForeColor = Color.Black;
            this.title1.Font = new Font("Verdana", 8.25f);

            this.chartSpeed.Series.Add(SeriesSpeed); //增加图表
            this.SeriesSpeed.ChartType = SeriesChartType.SplineArea;
            this.SeriesSpeed.BorderWidth = 2;
            this.SeriesSpeed.Color = Color.White;
            this.SeriesSpeed.BackSecondaryColor = Color.Black;

            this.richTextBox1.ForeColor = Color.Black;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题居中显示
            this.dataGridView1.ForeColor = Color.DarkSlateGray;
            for (int i = 0; i <= 14; i += 2)
            {
                this.dataGridView2.Rows[0].Cells[i].Value = 4 + i / 2;
                this.dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(217, 217, 217);
            }
            for (int i = 0; i < 9; i++)
            {
                Glob.jjPer[i] = int.Parse(IniRead("记录", i.ToString(), "0"));
            }
            Glob.jjAllC = int.TryParse(IniRead("记录", "总数", "0"), out Glob.jjAllC) ? Glob.jjAllC : 0;
            jjPerCheck(0);
            this.dataGridView2.Rows[0].Cells[16].Value = "12+";
            this.dataGridView2.Rows[0].Cells[16].Style.BackColor = Color.FromArgb(219, 219, 219);
            //字体
            //载入颜色设置
            Glob.r1Back = Color.FromArgb(int.Parse(IniRead("外观", "对照区颜色", "-722948")));
            richTextBox1.BackColor = Glob.r1Back;
            textBoxEx1.BackColor = Color.FromArgb(int.Parse(IniRead("外观", "跟打区颜色", "-722948")));
            //            Glob.Right = richTextBox1.BackColor;// Color.FromArgb(int.Parse(IniRead("外观", "打对颜色", "-8355712")));
            //            Glob.False = richTextBox1.BackColor;// Color.FromArgb(int.Parse(IniRead("外观", "打错颜色", "-38294")));
            Glob.Right = Color.FromArgb(int.Parse(IniRead("外观", "打对颜色", "-8355712")));
            Glob.False = Color.FromArgb(int.Parse(IniRead("外观", "打错颜色", "-38294")));
            //下方工具条颜色
            //this.toolStripButton1.BorderColor = Color.FromArgb(253,144,91);//替换
            //this.toolStripBtnLS.BorderColor = Color.FromArgb(255,127,24);//限制
            //this.toolStripButton3.BorderColor = Color.Yellow;//潜水
            //this.toolStripButton4.BorderColor = Color.FromArgb(0, 198, 37);//详细
            //this.tsb标注.BorderColor = Color.FromArgb(255,0,10);
            //this.toolButton1.BorderColor = Color.FromArgb(0,192,180);//精五

            //载入设置
            savesetup.srf = IniRead("输入法", "惯用设置", ""); //输入法
            Glob.DelaySend = int.Parse(IniRead("发送", "延时", "50")); //延时
            //载入个签
            Glob.InstraPre = IniRead("个签", "签名", "");
            Glob.InstraPre_ = IniRead("个签", "标志", "0");
            //载入输入法签名
            Glob.InstraSrf = IniRead("输入法", "签名", "");
            Glob.InstraSrf_ = IniRead("输入法", "标志", "0");
            FontConverter fc = new FontConverter();
            Glob.font_1 = (Font)fc.ConvertFromString(IniRead("外观", "对照区字体", "宋体, 21.75pt"));
            Glob.font_2 = (Font)fc.ConvertFromString(IniRead("外观", "跟打区字体", "宋体, 12pt"));
            richTextBox1.ForeColor = Color.Black;
            textBoxEx1.Font = Glob.font_2;
            this.richTextBox1.FontChanged += new EventHandler(richTextBox1_FontChanged);
            richTextBox1.Font = Glob.font_1;
            //if (Glob.qqSersion == "2012") { this.toolStripDropDownButton1.Text = "QQ12"; qQ2012ToolStripMenuItem.Checked = true; qQ2009ToolStripMenuItem.Checked = false; } else { qQ2012ToolStripMenuItem.Checked = false; qQ2009ToolStripMenuItem.Checked = true; this.toolStripDropDownButton1.Text = "QQ09+"; }
            //计算字大小
            //Point a1 = richTextBox1.GetPositionFromCharIndex(1);
            //Point a2 = richTextBox1.GetPositionFromCharIndex(richTextBox1.GetFirstCharIndexFromLine(1));
            // Glob.oneH = (int)Glob.font_1.GetHeight();//a2.Y - a1.Y;
            //MessageBox.Show(a1.Y + "\n" + a2.Y + "\n" + Glob.oneH);
            //获取发送成绩的排序顺序
            Glob.sortSend = IniRead("发送", "顺序", "ABCVDTSEFULGNOPRQ");

            //载入前导
            Glob.isZdy = bool.Parse(IniRead("载入", "开启", "False"));
            if (Glob.isZdy)
            {
                Glob.PreText = IniRead("载入", "前导", "-----");
                Glob.PreDuan = IniRead("载入", "段标", "第xx段");
            }
            else
            {
                Glob.PreText = "-----";
                Glob.PreDuan = "第xx段";
            }
            GetInfo(); //获取文段信息
            Glob.TextHgAll = int.Parse(IniRead("记录", "总回改", "0"));

            Glob.GDQActon = bool.Parse(IniRead("发送", "激活", "false")); //是否激活载入
            //载入方式
            Glob.getStyle = bool.Parse(IniRead("载入", "方式", "false"));
            //今日跟打
            _Ini iniSetup = new _Ini("Ttyping.ty");
            //记录开始时的总字数
            Glob.TextRecLenAll = int.Parse(IniRead("记录", "记录总字数", "0"));
            Glob.TextRecDays = int.Parse(IniRead("记录", "记录天数", "1"));

            ArrayList a = ReadKeys("今日跟打");
            if (a.Count > 0)
            {
                Glob.TodayDate = a[0].ToString();
            }
            else
                Glob.TodayDate = DateTime.Now.ToShortDateString();

            if (Glob.TodayDate != DateTime.Now.ToShortDateString())
            {
                iniSetup.IniWriteValue("今日跟打", Glob.TodayDate, null);
                Glob.TodayDate = DateTime.Now.ToShortDateString();
                Glob.TextRecDays++;//记录天数自增
            }
            Glob.todayTyping = int.Parse(IniRead("今日跟打", DateTime.Today.ToShortDateString(), "0"));
            Glob.TextLenAll = int.Parse(IniRead("记录", "总字数", "0"));

            lblMatchCount.Text = Glob.Instration.Trim();
            labelHaveTyping.Text = Glob.todayTyping + "/" + 字数格式化(Glob.TextRecLenAll) + "/" + Glob.TextRecDays + "天/" + 字数格式化(Glob.TextLenAll);
            //FileInfo ty = new FileInfo(Application.StartupPath + "\\Ttyping.ty");
            // double totaldays = (double)(DateTime.Today - ty.LastAccessTime).TotalDays;
            //toolTip1.SetToolTip(this.labelHaveTyping,"今日跟打/总计数\n开始时间：" + ty.LastAccessTime.ToShortDateString() + "\n已开始第：" + totaldays.ToString("0.00") + "天\n平均每天：" + ((double)(Glob.TextLenAll/totaldays)).ToString("0.00") + "字\n本信息程序启动时更新");
            //QQ号
            Glob.isQQ = bool.Parse(IniRead("发送", "QQSta", "false"));
            Glob.QQnumber = IniRead("发送", "QQ", "");
            //曲线
            Glob.isShowSpline = bool.Parse(iniSetup.IniReadValue("拖动条", "曲线", "false"));
            this.splitContainer4.Panel1Collapsed = Glob.isShowSpline;
            this.tbnSpline.Checked = !Glob.isShowSpline;
            //停止用时
            int StopTime = int.Parse(IniRead("控制", "停止", "1"));
            if (StopTime < 1 || StopTime > 10)
            {
                StopTime = 1;
            }
            Glob.StopUse = StopTime;
            this.toolTip1.SetToolTip(this.lblAutoReType, "跟打停止时间，大于" + Glob.StopUse + "分钟时自动停止跟打");
            //极简设置
            Glob.simpleMoudle = bool.Parse(IniRead("发送", "状态", "False"));
            Glob.simpleSplite = IniRead("发送", "分隔符", "|");
            this.toolStripButton2.Checked = Glob.simpleMoudle;
            //自动替换
            Glob.autoReplaceBiaodian = bool.Parse(IniRead("程序控制", "自动替换", "False"));
            this.toolStripButton1.Checked = Glob.autoReplaceBiaodian;
            //潜水
            Glob.isSub = bool.Parse(IniRead("发送", "潜水", "false"));
            this.toolStripButton3.Checked = Glob.isSub;
            //开始时间记录

            //不显示即时
            Glob.notShowjs = bool.Parse(IniRead("控制", "不显示即时", "False"));
            //速度限制
            Glob.速度限制 = double.Parse(IniRead("发送", "速度限制", "0.00"));
            Glob.是否速度限制 = bool.Parse(IniRead("发送", "是否速度限制", "False"));
            this.toolStripBtnLS.ToolTipText += "\n当前设置：" + Glob.速度限制;
            if (Glob.是否速度限制)
            {
                this.toolStripBtnLS.Checked = true;
            }
            else
            {
                this.toolStripBtnLS.Checked = false;
            }
            //载入词组信息
            _render = new RichEditBoxLineRender();
            InitCiKu();
            bool tick;
            bool.TryParse(IniRead("程序控制", "标记", "false"), out tick);
            if (tick)
            {
                this.tsb标注.Checked = true;
                Glob.isPointIt = true;
            }
            else
            {
                Glob.isPointIt = false;
            }
            //编码提示
            this.picBmTips.Checked = bool.Parse(IniRead("程序控制", "编码", "False"));
            if (this.picBmTips.Checked) CheckBmFile();

            //ini.IniWriteValue("程序控制", "编码", "False");
            //图片发送
            Glob.PicName = IniRead("发送", "昵称", "");
            this.PicSend.Checked = bool.Parse(IniRead("发送", "图片", "false"));
            this.比赛时自动打开寻找测速点ToolStripMenuItem.Checked = bool.Parse(IniRead("程序控制", "自动打开寻找", "False"));
            LblHaveTypingChange();
            捐助ToolStripMenuItem.Visible = !(Glob.TextRecLenAll > 5000);
            //if (Glob.TextRecLenAll == 0)
            //{
            //    Thread tr = new Thread(new ThreadStart(dnote));
            //    tr.Start();
            //}
        }

        //private void dnote()
        //{
        //    捐助作者 d捐助作者 = new 捐助作者();
        //    d捐助作者.ShowDialog();
        //}
        /// <summary>
        /// 已跟打数据的改变
        /// </summary>
        private void LblHaveTypingChange()
        {
            this.UIThread(() => toolTip1.SetToolTip(labelHaveTyping, "今日跟打：" + Glob.todayTyping + "字\n" +
                                                                     "记录天数：" + Glob.TextRecDays + "天\n" +
                                                                     "记录跟打：" + Glob.TextRecLenAll + "字\n" +
                                                                     "平均每天：" + (Glob.TextRecLenAll / Glob.TextRecDays).ToString("0.00") + "字\n" +
                                                                     "总跟打数：" + Glob.TextLenAll + "字\n" +
                                                                     "跟打段数：" + Glob.jjAllC + "段"));
        }
        void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            Glob.oneH = (int)this.richTextBox1.Font.GetHeight() + 4;
            if (Glob.isPointIt)
            {
                using (System.Drawing.Graphics graph = this.richTextBox1.CreateGraphics())
                {
                    _render._charSize = graph.MeasureString("测", this.richTextBox1.Font);
                }
                _render.Render();
            }
        }

        #region dll
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(int hwnd, StringBuilder lpString, int cch);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumWindows")]
        public static extern int EnumWindows(CallBack x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetClassName")]
        public static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "SwitchToThisWindow")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, Keys vk);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(System.IntPtr ptr, int wMsg, int wParam, int lParam);
        //输入法
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC,
        ref int conversion, ref int sentence);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);
        #endregion

        #region HookKey
        public class KeyBordHook
        {
            private const int WM_KEYDOWN = 0x100;
            private const int WM_KEYUP = 0x101;
            private const int WM_SYSKEYDOWN = 0x104;
            private const int WM_SYSKEYUP = 0x105;

            //全局的事件 
            public event KeyEventHandler OnKeyDownEvent;
            public event KeyEventHandler OnKeyUpEvent;
            public event KeyPressEventHandler OnKeyPressEvent;
            static int hKeyboardHook = 0;   //键盘钩子句柄 
            //鼠标常量 
            public const int WH_KEYBOARD_LL = 13;   //keyboard   hook   constant   
            HookProc KeyboardHookProcedure;   //声明键盘钩子事件类型. 
            //声明键盘钩子的封送结构类型 
            [StructLayout(LayoutKind.Sequential)]
            public class KeyboardHookStruct
            {
                public int vkCode;   //表示一个在1到254间的虚似键盘码 
                public int scanCode;   //表示硬件扫描码 
                public int flags;
                public int time;
                public int dwExtraInfo;
            }
            //装置钩子的函数 
            [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
            //卸下钩子的函数 
            [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern bool UnhookWindowsHookEx(int idHook);

            //下一个钩挂的函数 
            [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
            [DllImport("user32 ")]
            public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
            [DllImport("user32 ")]
            public static extern int GetKeyboardState(byte[] pbKeyState);
            public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
            ///   <summary> 
            ///   墨认的构造函数构造当前类的实例并自动的运行起来. 
            ///   </summary> 
            public KeyBordHook()
            {
                Start();
            }
            //析构函数. 
            ~KeyBordHook()
            {
                Stop();
            }
            public void Start()
            {
                //安装键盘钩子   
                if (hKeyboardHook == 0)
                {
                    KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                    hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule), 0);
                    if (hKeyboardHook == 0)
                    {
                        Stop();
                        //throw new Exception("SetWindowsHookEx   ist   failed. ");
                    }
                }
            }
            public void Stop()
            {
                bool retKeyboard = true;

                if (hKeyboardHook != 0)
                {
                    retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                    hKeyboardHook = 0;
                }
                //如果卸下钩子失败 
                if (!(retKeyboard)) throw new Exception("UnhookWindowsHookEx   failed. ");
            }
            private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
            {
                if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
                {
                    KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                    //引发OnKeyDownEvent 
                    if (OnKeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                    {
                        Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                        KeyEventArgs e = new KeyEventArgs(keyData);
                        OnKeyDownEvent(this, e);
                    }

                    //引发OnKeyPressEvent 
                    if (OnKeyPressEvent != null && wParam == WM_KEYDOWN)
                    {
                        byte[] keyState = new byte[256];
                        GetKeyboardState(keyState);
                        byte[] inBuffer = new byte[2];
                        if (ToAscii(MyKeyboardHookStruct.vkCode,
                            MyKeyboardHookStruct.scanCode,
                            keyState,
                            inBuffer,
                            MyKeyboardHookStruct.flags) == 1)
                        {
                            KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                            OnKeyPressEvent(this, e);
                        }
                    }

                    //引发OnKeyUpEvent 
                    if (OnKeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                    {
                        Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                        KeyEventArgs e = new KeyEventArgs(keyData);
                        OnKeyUpEvent(this, e);
                    }
                }
                return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
            }
        }

        #endregion

        #region 新发文
        public void SendAOnce()
        {
            this.textBoxEx1.TextChanged -= new System.EventHandler(textBoxEx1_TextChanged);
            if (NewSendText.发文状态)
            {
                //输入法状态
                Glob.binput = true;
                string TextAll = ""; //要发送的信息
                int TextLen = NewSendText.发文全文.Length;
                this.lblTitle.Text = NewSendText.标题;
                if (NewSendText.类型 == "单字")
                {
                    if (NewSendText.是否乱序)
                    {
                        int[] numlist;
                        //乱序的话
                        if (TextLen < NewSendText.字数 && TextLen > 0)
                        {
                            numlist = GetRandomUnrepeatArray(0, TextLen - 1, TextLen);
                        }
                        else if (TextLen >= NewSendText.字数)
                        {
                            numlist = GetRandomUnrepeatArray(0, TextLen - 1, NewSendText.字数);
                        }
                        else
                        {
                            if (!NewSendText.是否独练)
                            {
                                sendtext("文已发空！");
                            }
                            else
                            {
                                ShowFlowText("跟打完毕，请重新换文！");
                            }
                            NewSendText.标记 = 0;
                            if (NewSendText.乱序全段不重复)
                                NewSendText.发文全文 = NewSendText.文章全文;
                            TextLen = NewSendText.发文全文.Length;
                            numlist = GetRandomUnrepeatArray(0, TextLen - 1, NewSendText.字数);
                        }
                        Random ro = new Random((int)DateTime.Now.Ticks);
                        foreach (int item in numlist)
                        {
                            TextAll += NewSendText.发文全文[item];
                            if (NewSendText.乱序全段不重复)
                            {
                                NewSendText.发文全文 = NewSendText.发文全文.Replace(NewSendText.发文全文[item].ToString(), " ");
                            }
                        }
                        if (NewSendText.乱序全段不重复)
                            NewSendText.发文全文 = NewSendText.发文全文.Replace(" ", "");

                        //MessageBox.Show("已结束:" + numlist.Length + "\n当前度：" + NewSendText.发文全文.Length);
                        this.textBoxEx1.Clear();
                        richTextBox1.SelectAll();
                        richTextBox1.SelectionBackColor = Glob.r1Back;
                        richTextBox1.Text = TextAll;
                        Initialize(1);
                        Initialize(2);
                        this.textBoxEx1.ReadOnly = false;
                        textBoxEx1.Select();
                        Glob.Pre_Cout = NewSendText.起始段号.ToString();//起始段号
                        lblDuan.Text = "第" + NewSendText.起始段号 + "段";
                        GetInfo();//获取信息
                        Glob.reTypeCount = 0; //重打归零
                        if (!NewSendText.是否独练)
                        {
                            NewSendTextToQQ(TextAll, NewSendText.起始段号, NewSendText.标题, TextLen);
                            SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                        }
                        NewSendText.起始段号++;
                        NewSendText.已发段数++;
                    }
                    else
                    {
                        int least = TextLen - NewSendText.标记 + 1 - NewSendText.字数;
                        int limit = TextLen / NewSendText.字数;//总共只能发送多少段
                        //MessageBox.Show(limit + "\n" + NewSendText.标记);
                        if (NewSendText.已发段数 < limit)
                        {
                            if (NewSendText.已发段数 == limit - 1)
                            {
                                TextAll = NewSendText.发文全文.Substring(NewSendText.标记, TextLen - NewSendText.标记);
                                NewSendText.标记 = TextLen;
                            }
                            else
                            {
                                TextAll = NewSendText.发文全文.Substring(NewSendText.标记, NewSendText.字数);
                                NewSendText.标记 += NewSendText.字数;
                            }
                            this.textBoxEx1.Clear();
                            richTextBox1.SelectAll();
                            richTextBox1.SelectionBackColor = Glob.r1Back;
                            richTextBox1.Text = TextAll;
                            Initialize(1);
                            Initialize(2);
                            this.textBoxEx1.ReadOnly = false;
                            textBoxEx1.Select();
                            Glob.Pre_Cout = (NewSendText.起始段号 + NewSendText.已发段数).ToString();
                            lblDuan.Text = "第" + Glob.Pre_Cout + "段";
                            GetInfo();//获取信息
                            Glob.reTypeCount = 0; //重打归零
                            if (!NewSendText.是否独练)
                            {
                                NewSendTextToQQ(TextAll, NewSendText.起始段号 + NewSendText.已发段数, NewSendText.标题, TextLen);
                                this.Activate();
                                //SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                            }
                            NewSendText.已发段数++;
                        }
                        else
                        {
                            if (!NewSendText.是否独练)
                            {
                                sendtext("文已发空！");
                            }
                            else
                            {
                                ShowFlowText("跟打完毕，请重新换文！");
                            }
                            NewSendText.标记 = 0;
                            NewSendText.已发段数 = 0;
                            NewSendText.已发字数 = 0;
                            if (NewSendText.乱序全段不重复)
                                NewSendText.发文全文 = NewSendText.文章全文;
                        }
                    }
                }
                else if (NewSendText.类型 == "词组")
                {
                    Random ro = new Random((int)DateTime.Now.Ticks);
                    for (int i = 0; i < NewSendText.字数; i++)
                    {
                        TextAll += NewSendText.词组[ro.Next(0, NewSendText.词组.Length - 1)] + NewSendText.词组发送分隔符;
                    }
                    if (NewSendText.词组发送分隔符.Length > 0)
                        TextAll = TextAll.Remove(TextAll.Length - NewSendText.词组发送分隔符.Length, NewSendText.词组发送分隔符.Length) + "。";
                    //MessageBox.Show("已结束:" + numlist.Length + "\n当前度：" + NewSendText.发文全文.Length);
                    this.textBoxEx1.Clear();
                    richTextBox1.SelectAll();
                    richTextBox1.SelectionBackColor = Glob.r1Back;
                    richTextBox1.Text = TextAll;
                    Initialize(1);
                    Initialize(2);
                    this.textBoxEx1.ReadOnly = false;
                    textBoxEx1.Select();
                    Glob.Pre_Cout = NewSendText.起始段号.ToString();//起始段号
                    lblDuan.Text = "第" + NewSendText.起始段号 + "段";
                    GetInfo();//获取信息
                    Glob.reTypeCount = 0; //重打归零
                    if (!NewSendText.是否独练)
                    {
                        NewSendTextToQQ(TextAll, NewSendText.起始段号, NewSendText.标题, TextLen);
                        SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                    }
                    NewSendText.起始段号++;
                    NewSendText.已发段数++;
                }
                else if (NewSendText.类型 == "文章")
                {
                    if (NewSendText.是否一句结束)
                    {
                        if (NewSendText.标记 < TextLen)
                        {  //标记必须小于长度
                            int now = NewSendText.标记 + NewSendText.字数;
                            if (now < TextLen)
                            {
                                int textlen = NewSendText.字数;
                                if (zdSendText.isDot.IsMatch(NewSendText.文章全文.Substring(now - 1, 1))) //最后一个字不是汉字或数字
                                {
                                    for (int i = now; i < now + 50; i++)
                                    {
                                        string nowit = NewSendText.文章全文.Substring(i, 1);
                                        if (!zdSendText.isDot.IsMatch(nowit))
                                        {  //如果找到
                                            try
                                            {
                                                if (nowit == "。")
                                                    if (NewSendText.文章全文.Substring(i + 1, 1) == "”")
                                                        i++;

                                                if (nowit == "”")
                                                    if (NewSendText.文章全文.Substring(i + 1, 1) == "。")
                                                        i++;

                                                if (nowit == "—")
                                                    if (NewSendText.文章全文.Substring(i + 1, 1) == "—")
                                                        i++;

                                                if (nowit == "…")
                                                    if (NewSendText.文章全文.Substring(i + 1, 1) == "…")
                                                        i++;

                                                if (nowit == "：")
                                                    if (NewSendText.文章全文.Substring(i + 1, 1) == "“")
                                                        i++;
                                            }
                                            catch { }
                                            textlen = i - NewSendText.标记 + 1;
                                            break;
                                        }
                                    }
                                }
                                TextAll = NewSendText.文章全文.Substring(NewSendText.标记, textlen);
                                NewSendText.标记 += textlen;
                            }
                            else
                            {
                                TextAll = NewSendText.文章全文.Substring(NewSendText.标记, TextLen - NewSendText.标记);
                                NewSendText.标记 = TextLen;
                            }
                            this.textBoxEx1.Clear();
                            richTextBox1.SelectAll();
                            richTextBox1.SelectionBackColor = Glob.r1Back;
                            richTextBox1.Text = TextAll;
                            Initialize(1);
                            Initialize(2);
                            this.textBoxEx1.ReadOnly = false;
                            textBoxEx1.Select();
                            Glob.Pre_Cout = (NewSendText.起始段号 + NewSendText.已发段数).ToString();
                            lblDuan.Text = "第" + Glob.Pre_Cout + "段";
                            GetInfo();//获取信息
                            Glob.reTypeCount = 0; //重打归零
                            if (!NewSendText.是否独练)
                            {
                                NewSendTextToQQ(TextAll, NewSendText.起始段号 + NewSendText.已发段数, NewSendText.标题, TextLen);
                                this.Activate();
                            }
                            NewSendText.已发段数++;
                        }
                        else
                        {
                            if (!NewSendText.是否独练)
                            {
                                sendtext("文已发空！继续则重复发送！");
                            }
                            else
                            {
                                ShowFlowText("跟打完毕，请重新换文！");
                            }
                            NewSendText.已发段数 = 0;
                            NewSendText.标记 = 0;
                            NewSendText.已发字数 = 0;
                        }
                    }
                }
                this.textBoxEx1.TextChanged += new System.EventHandler(textBoxEx1_TextChanged);
                NewSendText.已发字数 += TextAll.Length;
                发文状态后处理();
                this.Activate();
            }
        }

        public void NewSendTextToQQ(string text, int duan, string header, int TextAllCount)
        {  //给QQ发送消息
            if (text != "")
            {
                string title = lblQuan.Text.ToString();
                if (title != "所在群")
                {
                    Clipboard.Clear();
                    string pre = "-----第" + duan + "段";

                    string least_pre = "-余" + (TextAllCount - NewSendText.标记) + "字-";

                    string least = (NewSendText.类型 == "词组") ? "-共" + NewSendText.词组.Length + "词" : least_pre;
                    if (NewSendText.类型 == "单字" & NewSendText.是否乱序) { least = "-乱序循环"; }
                    if (NewSendText.是否周期) { least += "-" + NewSendText.周期 + "秒"; }
                    //标题是否含有冒号
                    header = (header.Contains(":") || header.Contains("：")) ? header : header + ":";
                    string texttotal = header + "\r\n" + text + "\r\n" + pre + least + "-" + Glob.Instration.Trim();
                    ClipboardHandler.SetTextToClipboard(texttotal);
                    SwitchToThisWindow(FindWindow(null, title), true); //激活窗口
                    Delay(Glob.DelaySend);
                    SendKeys.SendWait("^v");
                    Delay(20);
                    SendKeys.SendWait("%s");
                }
            }
        }

        private void 发文状态后处理()
        {
            if (发文状态窗口 != null)
            {
                if (!发文状态窗口.IsDisposed)
                {
                    发文状态窗口.lblSendCounted.Text = NewSendText.已发字数.ToString();//已发字数
                    发文状态窗口.lblSendPCounted.Text = NewSendText.已发段数.ToString();//已发段数
                    发文状态窗口.tbxNowStart.Text = NewSendText.标记.ToString();//当前标记
                    发文状态窗口.tbxSendC.Text = NewSendText.字数.ToString();//一次发送字数
                    发文状态窗口.tbxNowStartCount.Text = Glob.Pre_Cout;//当前段号
                    if (NewSendText.乱序全段不重复)
                        发文状态窗口.lblLeastCount.Text = NewSendText.发文全文.Length.ToString();
                    if (NewSendText.是否周期)
                    {
                        发文状态窗口.lblNowTime.Text = NewSendText.周期计数.ToString();
                        this.lblNowTime_.Text = NewSendText.周期计数.ToString();
                    }
                }
            }
        }

        private SendTextStatic 发文状态窗口;
        private void 发文状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!NewSendText.发文状态) return;
            if (发文状态窗口 != null)
            {
                if (发文状态窗口.IsDisposed)
                    发文状态窗口 = new SendTextStatic(this.Location, this);
                MagneticMagnager mm = new MagneticMagnager(this, 发文状态窗口, MagneticPosition.Left);
                发文状态窗口.Show(this);
            }
            else
            {
                发文状态窗口 = new SendTextStatic(this.Location, this);
                MagneticMagnager mm = new MagneticMagnager(this, 发文状态窗口, MagneticPosition.Left);
                发文状态窗口.Show(this);
                this.Focus();
            }
        }
        #endregion

        #region 自带发文
        public class zdSendText
        {  //自带发文的全局变量
            public static Regex isDot = new Regex(@"[\u4e00-\u9fa5]|\d");//判断是否为符号
        }


        public void sendtext(string text)
        {
            if (text != "")
            {
                string title = lblQuan.Text.ToString();
                if (title != "所在群")
                {
                    SwitchToThisWindow(FindWindow(null, this.Text), true); //激活窗口
                    Clipboard.Clear();
                    Delay(20);
                    ClipboardHandler.SetTextToClipboard(text);
                    SwitchToThisWindow(FindWindow(null, title), true); //激活窗口
                    Delay(Glob.DelaySend);
                    SendKeys.SendWait("^a^v%s");
                    ClipboardHandler.SetTextToClipboard(Glob.theLastGoal);
                }
            }
        }

        public void SendClipBoardToQQ()
        {
            string title = lblQuan.Text.ToString();
            if (title != "所在群")
            {
                SwitchToThisWindow(FindWindow(null, this.Text), true); //激活窗口
                Delay(20);
                SwitchToThisWindow(FindWindow(null, title), true); //激活窗口
                Delay(Glob.DelaySend);
                SendKeys.SendWait("^a^v%s");
            }
        }
        //周期发文
        public void SendTTest()
        {
            NewSendText.周期计数 = NewSendText.周期;
            SendAOnce();
            //if (!NewSendText.是否独练)
            // {
            //     sendtext("周期发文开始，发文周期：" +  NewSendText.周期 + "秒");
            // }
            timerTSend.Start();
        }

        private void timerTSend_Tick(object sender, EventArgs e)
        {
            NewSendText.周期计数 -= 1;
            if (发文状态窗口 != null)
            {
                if (!发文状态窗口.IsDisposed)
                {
                    if (NewSendText.是否周期)
                    {
                        发文状态窗口.lblNowTime.Text = NewSendText.周期计数.ToString();
                        this.lblNowTime_.Text = NewSendText.周期计数.ToString();
                    }
                }
            }
            if (NewSendText.周期计数 <= 0)
            {
                SendAOnce();
                NewSendText.周期计数 = NewSendText.周期;
            }
            if (!NewSendText.是否周期) timerTSend.Stop();
        }
        #endregion
        public string IniRead(string section, string key, string def)
        { //ini的快捷读取
            _Ini sing = new _Ini("Ttyping.ty");
            return sing.IniReadValue(section, key, def);
        }

        public class savesetup
        {
            public static string srf;
        }

        public void Initialize(int Contr)
        {  //初始化操作
            if (Contr == 1)
            { //数值初始化
                Glob.TextJc = 0;
                Glob.TextCz = 0; //错字
                Glob.TextJs = 0; //键数
                Glob.TextJj = 0; //击键
                Glob.TextHg = 0; //回改
                Glob.LastInput = 0; //最后输入 
                //Glob.HaveTypeCount = 0;//已跟打字数 用于编码提示部分
                Sw = 0; //控制
                sw = 0;
                Glob.aTypeWords = 0;//打词
                sTime = new DateTime();
                startTime = new DateTime();
                Glob.MaxSpeed = 0;
                Glob.MaxJj = 0;
                Glob.MaxMc = 10; //峰值
                Glob.MinSplite = 500;
                Glob.FWords.Clear();
                Glob.TextBg = 0;//退格
                Glob.leftHand = 0;//键法
                Glob.rightHand = 0;
                Glob.回车 = 0;
                Glob.选重 = 0;
                Glob.FWordsSkip = 0;
                Glob.撤销 = 0;
                Glob.撤销用量 = 0;
                Glob.MinSplite = 500;
                Glob.TextMc = 0;
                Glob.TextMcc = 0;
                TimeStopAll = new TimeSpan();
                Glob.PauseTimes = 0;
                Glob.Use分析 = false;
                Glob.Type_Map_C = 200;
                跟打地图步进 = 0;
                Glob.地图长度 = 0;
                Glob.Type_map_C_1 = Color.FromArgb(220, 220, 220);
                Glob.TextHgPlace.Clear();
                Glob.TypeReport.Clear();
            }
            else if (Contr == 2)
            {  //显示初始化
                richTextBox1.SelectionStart = 0;
                richTextBox1.ScrollToCaret();
                richTextBox1.SelectionLength = richTextBox1.TextLength;
                richTextBox1.SelectionBackColor = Glob.r1Back;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.SelectionFont = richTextBox1.Font;

                labelJjing.Text = "";
                labelmcing.Text = "";
                labelSpeeding.Text = "";
                labelTimeFlys.Text = "00:00.00";
                labelJsing.Text = "0";
                labelhgstatus.Text = "0";
                HisSave[0] = HisSave[1] = 0;
                HisLine[0] = HisLine[1] = 0;
                this.labelBM.Text = "-";
                if (isPause)
                {
                    EndPause();
                }
                //跟打地图
                Bitmap bmp_ = new Bitmap(this.picMap.ClientRectangle.Width, this.picMap.ClientRectangle.Height);
                this.picMap.Image = bmp_;
                this.lbl地图长度.Text = "";
                //跟打长度
                Bitmap bmp_1 = new Bitmap(this.picBar.ClientRectangle.Width, this.picBar.ClientRectangle.Height);
                this.picBar.Image = bmp_1;
                this.lbl回改显示.Text = "回改";
                this.lbl错字显示.Text = "错字";
                //编码显示归零
                PicSetBmTips("", "", 0);
                //拆分条进度
                using (Graphics g = this.splitContainer1.CreateGraphics())
                {
                    g.Clear(Theme.ThemeColorBG);
                }
                if (Glob.isPointIt)
                {
                    //_render.Render();
                    _render.SetCurrIndex(0);
                }
            }
            try
            {
                KH.Start();
            }
            catch { }
            System.GC.Collect();
        }

        #region 跟打过程
        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="count"></param>
        private void RecTextTypeCount(int count)
        {
            if (Glob.TodayDate != DateTime.Now.ToShortDateString())
            {
                Glob.TextRecDays++;//记录天数自增
                Glob.todayTyping = 0;//今日跟打归零
                Glob.TodayDate = DateTime.Now.ToShortDateString();
            }
            Glob.todayTyping += count; //今日跟打
            Glob.TextRecLenAll += count;//记录字数
            Glob.TextLenAll += count;//总字数
            labelHaveTyping.Text = Glob.todayTyping + "/" + 字数格式化(Glob.TextRecLenAll) + "/" + Glob.TextRecDays + "天/" + 字数格式化(Glob.TextLenAll);
        }
        private int 上次输入标记 = 1;
        private int 跟打地图步进 = 0;
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            int TextLen = Glob.TextLen;
            if (TextLen > 1) //第一个字不算。如果文章超过2个字符才启用
            {
                Application.DoEvents();

                //获取文章
                string TextAlticle = richTextBox1.Text;
                //MessageBox.Show(Glob.Text);
                string TextType = richTextBox2.Text;
                int TextLenNow = TextType.Length;
                if (TextLenNow <= TextLen)
                {
                    //progressbar1.Size = new Size(TextLenNow * panel2.Size.Width / TextLen, 5);
                    int shengyu = TextLen - TextLenNow;
                    picBar_Draw((double)TextLenNow / TextLen, shengyu + "|" + shengyu * 100 / TextLen + "%");
                }
                //Application.DoEvents();
                //MessageBox.Show(TextType + "\n" + Sw.ToString());
                //再比较 当时的字符数量-1 就是原数组的序列
                if (TextLenNow >= 0 & TextLenNow <= TextLen)
                {
                    int getstart = richTextBox1.GetLineFromCharIndex(TextLenNow);
                    int getExend = richTextBox1.GetLineFromCharIndex(TextLen - 1);//获取最后一行的行号 也就是 总行号
                    HisLine[1] = getstart;
                    if (HisLine[1] != HisLine[0])
                    {
                        this.richTextBox2.BeginInvoke(new MethodInvoker(delegate
                        {
                            int sizeH = richTextBox1.ClientSize.Height; //一屏高度
                            int onePHan = (int)Math.Ceiling((double)sizeH / Glob.oneH);//一屏行数
                            int sizeH_ = onePHan * Glob.oneH;
                            int nowHan = richTextBox1.GetPositionFromCharIndex(TextLenNow).Y; //当前
                            int allH = richTextBox1.GetPositionFromCharIndex(TextLen).Y + Glob.oneH; //末行像素
                            //MessageBox.Show("末行高度：" + allH.ToString() + "\n一屏高度：" + sizeH.ToString() + "\n一行高度：" + Glob.oneH + "\n当前高度：" + nowHan + "\n可见总数：" + onePHan + "\n第一像素：" + richTextBox1.GetPositionFromCharIndex(0).Y);
                            if (nowHan > 0)
                            {
                                if (allH > sizeH) //末行高度超出 一屏高度时才启用滚屏
                                {
                                    if (nowHan >= (sizeH_ - Glob.oneH * 2)) //走到倒数第二行时
                                    {
                                        this.richTextBox1.SelectionStart = TextLenNow - 上次输入标记;
                                        this.richTextBox1.ScrollToCaret();
                                    }
                                }
                            }
                            else
                            {
                                this.richTextBox1.SelectionStart = TextLenNow;
                                this.richTextBox1.ScrollToCaret();
                            }
                        }));
                        HisLine[0] = HisLine[1];
                    } //此上是对 滚动条的控制

                    Sw += 1; //每打一个字就加1
                    sw++;
                    if (Sw == 1)
                    {
                        sTime = DateTime.Now;
                        startTime = sTime;
                        timer1.Start(); //没有开启则
                        //UseStopTime.Start();
                        timer2.Start();
                        timer3.Start(); //图表
                        timer5.Start();
                        Point pmos = new Point(), rbox = new Point(), rbbbox = new Point(), toXY = new Point();
                        GetCursorPos(ref pmos);
                        rbox = this.richTextBox1.PointToClient(pmos);
                        Size rbbox = new Size();
                        rbbox = this.richTextBox1.ClientSize;
                        rbbbox = new Point(rbbox.Width, rbbox.Height);
                        toXY = this.richTextBox1.PointToScreen(rbbbox);
                        if (rbox.X < rbbox.Width && rbox.Y < rbbox.Height)
                            SetCursorPos(toXY.X + 30, toXY.Y);
                        if (richTextBox1.Text.Substring(0, 1) == richTextBox2.Text.Substring(0, 1))
                        {
                            RecTextTypeCount(richTextBox2.TextLength);
                        }
                        HisSave[1] = TextLenNow;
                        Glob.TextJc = TextLenNow;
                        Glob.TextJs = 0;//键数重新置空
                        // MessageBox.Show(Glob.TextJc.ToString());
                        跟打地图步进 = 0;
                        //跟打报告
                        Glob.TypeReport.Add(new TypeDate { Index = Sw, Start = 0, End = TextLenNow, Length = HisSave[1] - HisSave[0], NowTime = 0, TotalTime = 0, Tick = 0, TotalTick = 0 });
                    }
                    else
                    {
                        HisSave[0] = HisSave[1];
                        HisSave[1] = TextLenNow;
                        //跟打报告
                        Glob.TypeReport.Add(new TypeDate { Index = Sw, Start = HisSave[0], End = HisSave[1], Length = HisSave[1] - HisSave[0], NowTime = Glob.typeUseTime, TotalTime = Glob.typeUseTime - Glob.TypeReport[Glob.TypeReport.Count - 1].NowTime, Tick = Glob.TextJs, TotalTick = Glob.TextJs - Glob.TypeReport[Glob.TypeReport.Count - 1].Tick });
                        int last = HisSave[0];
                        跟打地图步进++;
                        Glob.地图长度++;
                        this.lbl地图长度.Text = (TextLen * 100 / sw).ToString("0") + "%";
                        if (跟打地图步进 > this.picMap.Width)
                        {
                            Glob.Type_Map_C -= 40;
                            if (Glob.Type_Map_C < 40)
                                Glob.Type_Map_C = 200;
                            Glob.Type_map_C_1 = Color.FromArgb(Glob.Type_Map_C, Glob.Type_Map_C, Glob.Type_Map_C);
                            跟打地图步进 = 0;
                        }
                        if (HisSave[1] > HisSave[0]) //非回改的情况下
                        {
                            //Thread stay = new Thread(getStay);
                            //stay.Start((object)last);
                            int iPP = HisSave[1] - HisSave[0]; //长度
                            Glob.撤销用量 = iPP;
                            try
                            {
                                if (last >= richTextBox1.TextLength) { last = richTextBox1.TextLength - 1; }
                                string lastinput = richTextBox2.Text.Substring(last, iPP);
                                if (richTextBox1.Text.Substring(last, iPP) == lastinput) //所有的字非错的情况下
                                {
                                    RecTextTypeCount(iPP);
                                }
                            }
                            catch { }
                        }
                        //MessageBox.Show(stayTimeUse.ToString());
                    }
                    int least = TextLen - TextLenNow;
                    //labelleastwords.Text = least + "/" + (least * 100/TextLen) + "%";//剩余字数


                    int iP = HisSave[1] - HisSave[0];
                    if (iP > 0) //非退格情况往前打字
                    {
                        Glob.TextMc = 0;//完美计数
                        上次输入标记 = iP;
                        //Glob.TextCz = 0;//每次都归零
                        //MessageBox.Show(iP.ToString());
                        int Istart = textBoxEx1.SelectionStart; //在非回改情况下获取当前光标所在位置
                        int Glast = TextLenNow;//当前字数
                        if (Istart == Glast) //当前后面没有 字符的情况。
                        {
                            int g = 0;
                            for (int i = HisSave[0]; i < HisSave[1]; i++)
                            {
                                if (TextType[i] == TextAlticle[i])
                                {
                                    richTextBox1.SelectionStart = i;
                                    richTextBox1.SelectionLength = 1;
                                    richTextBox1.SelectionBackColor = Glob.Right;
                                    if (Glob.FWords.Contains(i))//以标识来计算错误量
                                    {
                                        Glob.FWords.Remove(i);
                                    }
                                    Glob.Type_Map_Color = Glob.Type_map_C_1;
                                }
                                else
                                {
                                    richTextBox1.SelectionStart = i;
                                    richTextBox1.SelectionLength = 1;
                                    richTextBox1.SelectionBackColor = Glob.False;
                                    if (!Glob.FWords.Contains(i))//以标识来计算错误量
                                    {
                                        Glob.FWords.Add(i);
                                    }
                                    Glob.Type_Map_Color = Color.OrangeRed;//todo 根据命需求，回改时不显示
                                                                          //                                    Glob.Type_Map_Color = Glob.Type_map_C_1;
                                    g++;

                                }
                            }

                            if (iP >= 2)
                            {
                                //MessageBox.Show(g.ToString());
                                if (iP == 2)
                                {
                                    string nowinput = richTextBox1.Text.Substring(HisSave[0], iP);
                                    if (nowinput == "……" || nowinput == "——")
                                    {
                                        g++;
                                    }
                                    else
                                    {
                                        Glob.aTypeWords++;
                                    } //排除符号
                                }
                                Glob.aTypeWords++;
                            } //打词记录
                        }
                        else
                        { //插入输入的情况
                            //MessageBox.Show("当前字数：" + Glast + "\n当前光标：" + Istart);
                            for (int i = Istart - iP; i < Glast; i++)
                            {
                                if (TextType[i] == TextAlticle[i])
                                {
                                    richTextBox1.SelectionStart = i;
                                    richTextBox1.SelectionLength = 1;
                                    richTextBox1.SelectionBackColor = Glob.Right;
                                    if (Glob.FWords.Contains(i))//以标识来计算错误量
                                    {
                                        Glob.FWords.Remove(i);
                                    }
                                }
                                else
                                {
                                    richTextBox1.SelectionStart = i;
                                    richTextBox1.SelectionLength = 1;
                                    richTextBox1.SelectionBackColor = Glob.False;
                                    if (!Glob.FWords.Contains(i))//以标识来计算错误量
                                    {
                                        Glob.FWords.Add(i);
                                    }
                                }
                            }
                        }

                        //测速点
                        if (Glob.SpeedPointCount > 0)
                        {
                            for (int i = 0; i < Glob.SpeedPointCount; i++)
                            {
                                for (int j = HisSave[0]; j < HisSave[1]; j++)
                                {
                                    if (Glob.SpeedPoint_[i] == j)
                                    {
                                        Glob.SpeedTime[i] = Glob.typeUseTime;
                                        Glob.SpeedJs[i] = Glob.TextJs;
                                        Glob.SpeedHg[i] = Glob.TextHg;
                                        Glob.SpeedControl++;
                                        //Glob.SpeedPoint.Add(Glob.typeUseTime);
                                        //Glob.KickPoint.Add(Glob.TextJs);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        //这是一种回改的情况呵呵
                        //Glob.TextCz = 0;//每次都归零
                        Glob.Type_Map_Color = Color.DeepSkyBlue;//回改橙色
                        int istart = textBoxEx1.SelectionStart; //获取当前光标所在的编号
                        int istep = Math.Abs(iP);//获取一次退格的 量
                        //MessageBox.Show(HisSave[1] + "\n" + HisSave[0]);
                        Glob.TextHgAll++;
                        if (istep > 0)
                        {
                            richTextBox1.SelectionStart = HisSave[1];
                            richTextBox1.SelectionLength = istep;
                            richTextBox1.SelectionBackColor = Glob.r1Back;
                            for (int i = HisSave[1]; i <= HisSave[0]; i++)
                            {
                                if (Glob.FWords.Contains(i))//以标识来计算错误量
                                {
                                    Glob.FWords.Remove(i);
                                }
                            }
                        }
                        //else
                        //{
                        //MessageBox.Show(istart + "\n" + HisSave[1]);
                        for (int i = istart; i < HisSave[1]; i++) //从当前光标处再继续往后比较正错
                        {
                            if (TextType[i] == TextAlticle[i])
                            {
                                richTextBox1.SelectionStart = i;
                                richTextBox1.SelectionLength = 1;
                                richTextBox1.SelectionBackColor = Glob.Right;
                                if (Glob.FWords.Contains(i))//以标识来计算错误量
                                {
                                    Glob.FWords.Remove(i);
                                }
                            }
                            else
                            {
                                richTextBox1.SelectionStart = i;
                                richTextBox1.SelectionLength = 1;
                                richTextBox1.SelectionBackColor = Glob.False;
                                if (!Glob.FWords.Contains(i))//以标识来计算错误量
                                {
                                    Glob.FWords.Add(i);
                                }
                            }
                        }

                        //}
                    }
                    Type_Map(Glob.Type_Map_Color, 跟打地图步进, 1);
                    //更新错字
                    this.labelBM.Text = Glob.FWords.Count.ToString();
                    if (Glob.isPointIt)
                        _render.SetCurrIndex(TextLenNow);
                }
                LblHaveTypingChange();//
                if (TextLenNow == TextLen)
                {
                    for (int i = HisSave[0]; i < HisSave[1]; i++)
                    {
                        //末尾最后一次输入的字符有错字时
                        if (TextAlticle[i] != TextType[i])
                        {
                            Glob.LastInput = 1;
                            break;
                        }
                    }
                    eTime = DateTime.Now;
                    //UseStopTime.Stop();
                    //MessageBox.Show("TextLenNow:" + TextLenNow + "\n TextLen:" + TextLen + "\n LastInput:" + Glob.LastInput);
                    //timer1.Enabled = false;
                    if (Glob.LastInput == 1)
                    {
                        Glob.LastInput = 0;
                        return;
                    }
                    timer1.Enabled = false; //关闭计时器
                    timer2.Enabled = false;
                    timer3.Enabled = false; //图表
                    timer5.Stop(); //长时间不跟打自动重打
                    //MessageBox.Show("精确计时：" + " " + "\n标识计时：" + (eTime - sTime).TotalSeconds);
                    this.lblAutoReType.Text = "0";
                    ///已跟打赋值
                    Glob.HaveTypeCount++;//已跟打段数

                    #region 打完处理
                    //toolStripStatusLabelTest.Text = "起打时间：" + sTime.ToShortTimeString() + " 终止时间：" + eTime.ToShortTimeString() + " 标志用时：" + (eTime - sTime).TotalSeconds + "s";
                    textBoxEx1.ReadOnly = true;
                    ts = Glob.typeUseTime;
                    Sw = 0; //初始化
                    Glob.TotalUse += Glob.typeUseTime;
                    //处理数据
                    double speed = Math.Round((double)((TextLen - Glob.TextJc) * 60) / ts, 2); //错一罚五
                    double mc = Math.Round((double)Glob.TextJs / (TextLen - Glob.TextJc), 2);
                    double jj = Math.Round((double)Glob.TextJs / ts, 2);
                    //以下三列数据为测速准备
                    Glob.TextSpeed = speed;
                    Glob.Textjj = jj;
                    Glob.Textmc = mc;
                    //击键数据排列
                    int j = (int)jj;
                    if (j > 3 && j < 12)
                    {
                        Glob.jjPer[j - 4]++;
                    }
                    else if (j >= 12)
                    {
                        Glob.jjPer[8]++;
                    }
                    //平均速度
                    Glob.TypeCount++; //跟打次数
                    Glob.jjAllC++;//跟打总段数
                    //错情 与 错字
                    string RightAndFault = "", RFSplit = "|"; //分隔符
                    String fa = "";
                    Glob.TextCz = Glob.FWords.Count;
                    for (int i = 0; i < Glob.FWords.Count; i++)
                    {
                        int s = (int)Glob.FWords[i];
                        fa = TextType.Substring(s, 1);
                        if (fa == " ") { fa = "□"; }
                        RightAndFault += TextAlticle.Substring(s, 1) + "√ " + fa + "×";
                        if (Glob.TextCz > 0 && i < Glob.TextCz - 1) { RightAndFault += RFSplit; }
                    }
                    //MessageBox.Show(RightAndFault);
                    string Cz, Spsend; //错字和速度的输出
                    string Spsend1;
                    //string instration = Glob.Instration;//Glob.Instration = " ＠添1A"; ;
                    string FalutIns = "";//错情
                    double speed2 = Math.Round((double)((TextLen - Glob.TextJc - Glob.TextCz * 5) * 60) / ts, 2);
                    if (Glob.TextCz != 0)
                    {
                        if (speed2 < 0) { speed2 = 0.00; }
                        //命需求 错1罚5 速度除半
                        var half = speed2 / 2;
                        Spsend = half.ToString("0.00") + "/" + speed.ToString("0.00");
                        Spsend1 = half.ToString("0.00");
                        FalutIns = " 错情：[" + RightAndFault + "]";
                    }
                    else
                    {
                        Spsend = speed.ToString("0.00");
                        Spsend1 = Spsend;
                    }
                    Cz = " 错字" + Glob.TextCz.ToString();
                    //末尾描述
                    if (Glob.TextCz <= (int)TextLen / 10)
                    {
                        //段号
                        string duanhao = "第" + Glob.Pre_Cout + "段";
                        if (!Glob.isMatch)
                        {
                            if (duanhao == "段号")
                            {
                                duanhao = "自测";
                            }
                        }
                        if (Glob.jwMatchMoudle) { duanhao = "第" + Glob.Pre_Cout + "期精五门比赛文段"; }
                        //个签
                        string inistra;
                        if (Glob.InstraPre_ != "0")
                        {
                            inistra = " 个签:" + Glob.InstraPre;
                        }
                        else
                        {
                            inistra = "";
                        }
                        //输入法签名
                        string inistraSrf;
                        if (Glob.InstraSrf_ != "0")
                        {
                            inistraSrf = " 输入法:" + Glob.InstraSrf;
                        }
                        else
                        {
                            inistraSrf = "";
                        }
                        //打词
                        string atypewords;
                        atypewords = " 打词" + Glob.aTypeWords;
                        //Glob.TextTypeWs = 0;//初始化打词总计
                        //重打
                        string reTypeing;
                        int typecount = Glob.reTypeCount + 1;
                        if (Glob.reTypeCount > 1)
                        {
                            reTypeing = " 重打" + typecount;
                        }
                        else
                        {
                            reTypeing = "";
                        }
                        //回改率
                        Glob.TextHg_ = (double)Glob.TextHgAll * 100 / Glob.TextLenAll;
                        //停留
                        //MessageBox.Show(stayHighTime[0,0].ToString());

                        var stay = "";
                        try
                        {
                            const string bd =
                                @"，。“”！（）()~·#￥%&*_[]{}‘’/\<>,.《》？：；、—…1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ";
                            var findall = Glob.TypeReport.OrderByDescending(o => o.TotalTime);
                            foreach (
                                var typeDate in
                                    from typeDate in findall
                                    let s = TextAlticle[typeDate.Start + typeDate.Length - 1]
                                    where !bd.Contains(s)
                                    select typeDate)
                            {
                                stay = string.Format(" 停留[{0}]{1}",
                                                     TextAlticle.Substring(typeDate.Start, typeDate.Length),
                                                     typeDate.TotalTime.ToString("0.00") + "s");
                                richTextBox1.SelectionStart = typeDate.Start;
                                richTextBox1.SelectionLength = typeDate.Length;
                                richTextBox1.SelectionBackColor = Color.YellowGreen;
                                break;
                            }
                        }
                        catch
                        {
                            stay = "";
                        }
                        //跟打效率
                        string awordper;
                        Glob.效率 = TextLen * 100 / sw;
                        if (sw != 0)
                        {
                            awordper = " 效率" + Glob.效率 + "%";
                        }
                        else
                        {
                            awordper = "";
                        }
                        this.lbl地图长度.Text = Glob.效率 + "%";
                        //回改用时
                        Glob.hgAllUse = Glob.TypeReport.Where(o => o.Length < 0).Sum(o => o.TotalTime);
                        sw = 0;
                        //键准
                        this.lbl键准.Text = (键准 == 0) ? "-" : 键准 + "%";
                        //顺序及发送
                        string sortsend = "", qidayu = "";
                        string TotalSend = "";
                        if (!Glob.isMatch)
                        { //非比赛模式
                            if (Glob.simpleMoudle)
                            {  //极简模式
                                string string1 = Glob.Pre_Cout + Glob.simpleSplite + Spsend + Glob.simpleSplite + jj.ToString("0.00") + Glob.simpleSplite + mc.ToString("0.00") + Glob.simpleSplite + Glob.TextHg;
                                TotalSend += string1 + Glob.simpleSplite;// +instration;
                                if (Glob.InstraPre_ != "0") { TotalSend += Glob.InstraPre + Glob.simpleSplite; }
                                TotalSend += Glob.Instration.Trim();
                            }
                            else if (Glob.jwMatchMoudle)
                            {
                                sortsend = "ABCDEGIH";
                            }
                            else
                            {
                                sortsend = Glob.sortSend; qidayu = "";
                            }
                        }
                        else { sortsend = "ABDSTVCUEFGHIJKLMNOPQR"; qidayu = " 起打于" + startTime.ToLongTimeString(); } //比赛全显示
                        if (sortsend.Length != 0) //用 顺序是否为空为控制 流程
                        {
                            char[] TSend = sortsend.ToArray();
                            TotalSend = duanhao;
                            for (int i = 0; i < TSend.Length; i++)
                            {
                                switch (TSend[i])
                                {
                                    case 'A': TotalSend += " 速度" + Spsend; break;
                                    case 'B':
                                        if (jj != 0)
                                        {
                                            if (!(jj <= 3.2 && mc <= 1.3))
                                            {
                                                TotalSend += " 击键" + jj.ToString("0.00");
                                                //if (NowjjCount != -1)
                                                //{
                                                //    TotalSend += "(" + nowjjPer.ToString("0.00") + "%)";
                                                //}
                                            }
                                        }
                                        break;
                                    case 'C':
                                        if (mc != 0)
                                        {
                                            if (!(jj <= 3.2 && mc <= 1.3))
                                            {
                                                var v = "";
                                                if (Glob.词库理论码长 != 0) v = " 词库理论" + Glob.词库理论码长.ToString("0.00");
                                                TotalSend += " 码长" + mc.ToString("0.00") + v;
                                            }
                                        }
                                        break;
                                    case 'D':
                                        TotalSend += 回改量 + 连改;
                                        break;

                                    case 'S': TotalSend += 退格; break;
                                    case 'T': TotalSend += 回车; break;
                                    case 'U': TotalSend += 选重; break;
                                    case 'V': TotalSend += " 键准" + this.lbl键准.Text; break;
                                    case 'E': TotalSend += Cz; break;
                                    case 'F': TotalSend += FalutIns; break;
                                    case 'G': TotalSend += " 字数" + TextLen; break;
                                    case 'H':
                                        if (Glob.TextJs != 0)
                                        {
                                            if (!(jj <= 3.2 && mc <= 1.3))
                                                TotalSend += " 键数" + Glob.TextJs;
                                        }
                                        break;
                                    case 'I': TotalSend += 跟打用时; break;
                                    case 'J': TotalSend += reTypeing; break;
                                    case 'K':
                                        //峰值
                                        string MaxValue = "";
                                        if (this.textBoxEx1.TextLength > 10 && jj > 3.2 && mc > 1.3)
                                        {
                                            if (speed2 > Glob.MaxSpeed) Glob.MaxSpeed = speed2;
                                            if (jj > Glob.MaxJj) Glob.MaxJj = jj;
                                            if (mc < Glob.MaxMc) Glob.MaxMc = mc;
                                            MaxValue = " 峰值" + Glob.MaxSpeed.ToString("0.00") + "/" + Glob.MaxJj.ToString("0.00") + "/" + Glob.MaxMc.ToString("0.00");
                                        }
                                        TotalSend += MaxValue; break;
                                    case 'L': TotalSend += atypewords; break;
                                    case 'M': TotalSend += " 回改率" + Glob.TextHg_.ToString("0.00") + "%"; break;
                                    case 'N': TotalSend += stay; break;
                                    case 'O': TotalSend += awordper; break;

                                    case 'Q': TotalSend += 撤销; break;
                                    case 'R': TotalSend += 键法; break;
                                    default: break;
                                }
                            }
                            TotalSend += inistraSrf;
                            if (Glob.jwMatchMoudle)
                            {
                                TotalSend += " 校验码:" + 精五验证((int)speed2, (int)jj, (int)mc, (int)(Glob.typeUseTime * 1000.0), Glob.QQnumber.ToString());
                            }
                            if (Glob.isQQ)
                                TotalSend += " QQ:" + Glob.QQnumber;

                            if (Glob.isMatch)
                            {
                                TotalSend += qidayu + " 赛文验证:" + lblMatchCount.Text;
                            }

                            if (Glob.isCheat)
                            {
                                TotalSend += " [CHEAT]";
                                Glob.isCheat = false;
                            }
                            //if (sortsend.Contains("P") || Glob.isMatch) //如果含有添雨验证则 或开启了比赛模式
                            // {
                            //TotalSend += " 校验码:" + 精五验证((int)speed2, (int)jj, (int)mc, (int)Glob.typeUseTime);//
                            if (!Glob.jwMatchMoudle)
                                TotalSend += " 校验:" + 添雨验证(TotalSend);
                            // }
                            TotalSend += inistra + 暂停 + 版本;
                            //TotalSend += 版本 ; //版本
                            Glob.theLastGoal = TotalSend;
                            //MessageBox.Show(Convert.ToInt32(speed).ToString());
                            //成绩
                            //if (NewSendText.发文状态)
                            //{
                            //    TotalSend += " [发文人]";
                            //}
                        }
                        //try
                        //{
                        picBar_Draw(0.0, Glob.TextLen + ",100%");
                        labelSpeeding.Text = speed.ToString("0.00");
                        //changelabelcolor(jj, 1); //改变击键的颜色
                        labelJjing.Text = jj.ToString("0.00");
                        labelmcing.Text = mc.ToString("0.00");
                        string title = lblQuan.Text == "所在群" ? "" : lblQuan.Text;

                        //平均数据
                        //this.SeriesSpeed.Points.AddXY(Glob.typeUseTime,speed2);
                        Glob.Per_Speed += speed2;
                        Glob.Per_Jj += jj;
                        Glob.Per_Mc += mc;

                        double touse = Glob.TotalUse;
                        if (dataGridView1.RowCount > 1)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.RowCount - 1);
                        }
                        //当前为重打段时 与上次文章验证一样时
                        if (Glob.TextPreCout != this.lblMatchCount.Text)
                        {//如果此次和上次 文章验证 不相同
                            Glob.HaveTypeCount_++; //实际跟打段数加一
                        }
                        //显示打完信息
                        //ShowFlowText("第" + Glob.Pre_Cout + "段" + " 速度" + Glob.TextSpeed + " 击键" + Glob.Textjj + " 码长" + Glob.Textmc + " 用时" + new DateTime().AddSeconds(Glob.typeUseTime).ToString("mm.ss.ff"));
                        if (Glob.TextPreCout == this.lblMatchCount.Text)
                        {
                            Glob.ReTypePD = true;//重打判断 为重打
                            int RowCount = this.dataGridView1.Rows.Count - 1;
                            double speed_Plus = Glob.TextSpeed - double.Parse(this.dataGridView1.Rows[RowCount].Cells[3].Value.ToString());
                            double jj_Plus = Glob.Textjj - double.Parse(this.dataGridView1.Rows[RowCount].Cells[4].Value.ToString());
                            double mc_Plus = Glob.Textmc - double.Parse(this.dataGridView1.Rows[RowCount].Cells[5].Value.ToString());
                            dataGridView1.Rows.Add("", "", "",
                                //速度
                                speed_Plus > 0 ? "+" + speed_Plus.ToString("0.00") : speed_Plus.ToString("0.00"),
                                //击键
                                jj_Plus > 0 ? "+" + jj_Plus.ToString("0.00") : jj_Plus.ToString("0.00"),
                                //码长
                                mc_Plus > 0 ? "+" + mc_Plus.ToString("0.00") : mc_Plus.ToString("0.00")
                                );
                            RowCount++;
                            dataGridView1.Rows[RowCount].Height = 10;
                            dataGridView1.Rows[RowCount].DefaultCellStyle.Font = new Font("Arial", 6.8f);
                            dataGridView1.Rows[RowCount].DefaultCellStyle.ForeColor = Color.LightGray;
                            //各个项目样式 
                            if (speed_Plus > 0) dataGridView1.Rows[RowCount].Cells[3].Style.ForeColor = Color.FromArgb(253, 108, 108);
                            if (jj_Plus > 0) dataGridView1.Rows[RowCount].Cells[4].Style.ForeColor = Color.FromArgb(255, 129, 233);
                            if (mc_Plus < 0) dataGridView1.Rows[RowCount].Cells[5].Style.ForeColor = Color.FromArgb(124, 222, 255);
                            for (int i = 0; i < 13; i++)
                            {
                                if (i != 3 && i != 4 && i != 5)
                                {
                                    this.dataGridView1.Rows[RowCount].Cells[i].Style.BackColor = Color.FromArgb(61, 61, 61);
                                }
                            }
                            //重打
                            dataGridView1.Rows.Add("", DateTime.Now.ToLongTimeString(), Glob.Pre_Cout, Spsend1, jj.ToString("0.00"), mc.ToString("0.00"), Glob.TextHg.ToString(), Glob.TextCz.ToString(), Glob.TextJs.ToString(), TextLen.ToString(), Glob.aTypeWords, ts.ToString("0.00"), title);//增加行
                        }
                        else
                        {
                            //新打
                            Glob.ReTypePD = false;//重打判断 为新打
                            dataGridView1.Rows.Add(Glob.HaveTypeCount_, DateTime.Now.ToLongTimeString(), Glob.Pre_Cout, Spsend1, jj.ToString("0.00"), mc.ToString("0.00"), Glob.TextHg.ToString(), Glob.TextCz.ToString(), Glob.TextJs.ToString(), TextLen.ToString(), Glob.aTypeWords, ts.ToString("0.00"), title);//增加行
                        }
                        Glob.TextPreCout = this.lblMatchCount.Text;//认证是不是重打
                        Glob.TextTime = DateTime.Now.ToLongTimeString();
                        //成绩信息底色黑
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(61, 61, 61);
                        if (speed2 >= 180.00)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Style.ForeColor = Color.FromArgb(255, 175, 228);
                        }
                        if (jj > 9.9)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Style.ForeColor = Color.FromArgb(97, 223, 255);
                        }
                        if (mc > 1.30 && mc <= 2.00 && jj > 5.00)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Style.ForeColor = Color.FromArgb(194, 255, 121);
                        }
                        if (Glob.TextCz > 0.00)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Style.ForeColor = Color.IndianRed;
                        }
                        double jjPer_ = Glob.Per_Jj / Glob.HaveTypeCount;
                        Glob.Total_Type += Glob.TextLen;
                        string dis = "00:00:00";
                        if (Glob.HaveTypeCount > 1)
                        {
                            DateTime dt = new DateTime().AddSeconds(touse);
                            dis = dt.ToString("HH:mm:ss");
                        }
                        dataGridView1.Rows.Add("", dis, Glob.HaveTypeCount + "#", (Glob.Per_Speed / Glob.HaveTypeCount).ToString("0.00"), jjPer_.ToString("0.00"), (Glob.Per_Mc / Glob.HaveTypeCount).ToString("0.00"), "", "", "", (Glob.Total_Type / Glob.HaveTypeCount).ToString("0.00"), "", (touse / Glob.HaveTypeCount).ToString("0.00"), "");//增加行
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                        dataGridView1.ClearSelection();
                        DataGridViewRow dgr = dataGridView1.Rows[dataGridView1.RowCount - 1];
                        //dgr.DefaultCellStyle.BackColor = Theme.ThemeColorBG;
                        dgr.DefaultCellStyle.ForeColor = Theme.ThemeColorFC;
                        jjPerCheck((int)jj);//击键占率
                        Show_Hg_Place();//显示回改地点
                        Glob.Use分析 = true;//F3时 分析不可用 只有在跟打结束后分析才可用
                        try
                        {
                            KH.Stop();
                        }
                        catch { }
                        Clipboard.Clear();
                        ClipboardHandler.SetTextToClipboard(TotalSend);
                        if (NewSendText.发文状态)
                        {
                            if (NewSendText.是否自动)
                            {  //自动模式
                                F3();
                                SendAOnce();
                            }
                            if (NewSendText.是否独练)
                            { //自我练习
                                return;
                            }
                        }
                        if (Glob.isSub) { timerSubFlash.Start(); return; }
                        if (title != "所在群")
                        {
                            if (Glob.是否速度限制)
                            {
                                if (speed2 >= Glob.速度限制)
                                {
                                    isornoSend(title, TotalSend);
                                }
                                else
                                {
                                    timerBtnFlash.Start();
                                }
                            }
                            else
                            {
                                isornoSend(title, TotalSend);
                            }
                        }
                        //跟打完毕后，是否激活问题
                        if (Glob.GDQActon) { SwitchToThisWindow(FindWindow(null, Glob.Form), true); }
                    }
                    #endregion
                }

            }
            else
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                timer5.Stop();
                this.lblAutoReType.Text = "0";
                ShowFlowText("字数过少！");
            }
            // }
            //catch (Exception err) { MessageBox.Show(err.Message); }
        }

        //显示回改地点
        private void Show_Hg_Place()
        {
            if (Glob.TextHgPlace.Count > 0)
            {
                foreach (int i in Glob.TextHgPlace)
                {
                    if (i < Glob.TextLen)
                    { //确定少于总字数
                        this.richTextBox1.SelectionStart = i - 1;
                        this.richTextBox1.SelectionLength = 1;
                        //Font font = new Font(this.richTextBox1.SelectionFont, FontStyle.Underline);
                        //this.richTextBox1.SelectionFont = font;
                        this.richTextBox1.SelectionColor = Color.GreenYellow;
                    }
                }
            }
        }

        //跟打地图
        private void Type_Map(Color C, float X, float W)
        {
            Bitmap bmp = new Bitmap(this.picMap.Image);//new Bitmap(this.pictureBox1.ClientRectangle.Width, this.pictureBox1.ClientRectangle.Height);
            Glob.Type_Map = Graphics.FromImage(bmp);
            Glob.Type_Map.DrawLine(new Pen(C, W), X, 0, X, bmp.Height);
            //Glob.Type_Map.DrawRectangle(new Pen(C, 1),X,0,W,bmp.Height);
            //Glob.Type_Map.FillRectangle(new (C, 1), X, 0, W, bmp.Height);
            this.picMap.Image = bmp;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitterBar(this.textBoxEx1.TextLength / this.richTextBox1.TextLength);
        }

        /// <summary>
        /// 图片进度重绘
        /// </summary>
        /// <param name="pro">百分比</param>
        /// <param name="text">绘制文字</param>
        private void picBar_Draw(double pro, string text)
        {
            Bitmap bmp = new Bitmap(this.picBar.Width, this.picBar.Height);
            Graphics g = Graphics.FromImage(bmp);
            double width = this.picBar.Width * pro;
            //MessageBox.Show("宽度：" + this.picBar.Width + "\n计算：" + width + "\n比例：" + pro);
            Color C;//进度线条
            float f = 1f;//进度宽度
            C = Theme.ThemeColorBG;

            //画进度
            Rectangle rect = new Rectangle(0, 0, (int)width, this.picBar.Height);
            g.FillRectangle(Brushes.GhostWhite, rect);
            g.DrawLine(new Pen(C, f), rect.Width - f + 1, 0, rect.Width - f + 1, (float)rect.Height);
            //画字
            Font F = new Font("宋体", 9f);
            SizeF s = g.MeasureString(text, F);
            int fontWidth = (int)Math.Ceiling(s.Width);
            //if (width >= fontWidth)
            //{
            //    g.DrawString(text, F, Brushes.Black, (float)(width - fontWidth + 2), 1.0f);
            //}
            //else
            g.DrawString(text, F, Brushes.Brown, (float)(this.picBar.Width / 2 - fontWidth / 2), this.picBar.Height / 2 - s.Height / 2);

            this.picBar.Image = bmp;
            SplitterBar(pro);
        }

        private void SplitterBar(double pro)
        {
            //测试 拆分条 的 绘图
            Graphics g_ = this.splitContainer1.CreateGraphics();
            Color Show = Color.DeepSkyBlue;//Color.FromArgb(255 - Theme.ThemeColorBG.R, 255 - Theme.ThemeColorBG.G, 255 - Theme.ThemeColorBG.B);
            g_.Clear(Theme.ThemeColorBG);
            using (SolidBrush sb = new SolidBrush(Show))
            {
                Rectangle r = this.splitContainer1.SplitterRectangle;
                Rectangle r_ = new Rectangle(r.X, r.Y, (int)(r.Width * pro), r.Height);
                g_.FillRectangle(sb, r_);
            }
            g_.Dispose();
        }
        //成绩分析
        private void 跟打分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.Use分析)
            {
                SpeedAn sa = new SpeedAn(成绩分析(), this);
                sa.ShowDialog();
            }
        }

        private string[] 成绩分析()
        {
            string[] SpeedAnGet = new string[2];
            Array.Clear(SpeedAnGet, 0, 2);
            if (Glob.HaveTypeCount <= 0) return SpeedAnGet;
            double plus = 0;
            //回改影响速度值
            double jj_1 = (double)Glob.TextJs / Glob.typeUseTime;
            double mc_1 = (double)Glob.TextJs / (Glob.TextLen - Glob.TextJc);
            double speed_1 = (double)(Glob.TextLen - Glob.TextJc) * 60 / (Glob.typeUseTime - Glob.hgAllUse);
            double speed_ = (double)(Glob.TextLen - Glob.TextJc) * 60 / Glob.typeUseTime;
            double Hg_speed = speed_1 - speed_;
            //退格影响速度值
            double mc_2 = (double)(Glob.TextJs - Math.Abs(Glob.TextBg - Glob.TextHg)) / (Glob.TextLen - Glob.TextJc);
            double speed_3 = jj_1 * 60 / mc_2;
            double Bg_speed = speed_3 - speed_;
            //回车影响速度值
            double mc_3 = (double)(Glob.TextJs - Glob.回车) / (Glob.TextLen - Glob.TextJc);
            double speed_4 = jj_1 * 60 / mc_3;
            double En_speed = speed_4 - speed_;
            //停留影响速度值
            double 平均停留 = (double)Glob.typeUseTime / (Glob.TextLen - Glob.TextJc);
            double 停留 = Glob.TypeReport.Where(o => o.Length > 0).Max(o => o.TotalTime) - 平均停留;
            double speed_5 = (double)(Glob.TextLen - Glob.TextJc) * 60 / (Glob.typeUseTime - 停留);
            double St_speed = Math.Abs(speed_5 - speed_);
            //错字影响速度值
            double speed_6 = (double)(Glob.TextLen - Glob.TextJc - Glob.TextCz * 5) * 60 / Glob.typeUseTime;
            double Cz_speed = speed_ - speed_6;
            //键准理论值
            int Low = Glob.TextJs - Math.Abs((Glob.TextBg - Glob.TextHg)) * 2 - Glob.TextMcc;
            double Jz_mc = (double)Low / (Glob.TextLen - Glob.TextJc);
            double Jz_speed = jj_1 * 60 / Jz_mc;
            if (Glob.PauseTimes > 0)
                plus = Hg_speed + Bg_speed + En_speed + Cz_speed;//有暂停时间，不显示停留
            else
                plus = Hg_speed + Bg_speed + En_speed + St_speed + Cz_speed;
            StringBuilder sb = new StringBuilder();
            string 码长理论 = Jz_speed.ToString("0.00");
            string 完美理论 = (speed_6 + plus).ToString("0.00");
            string 跟打实际 = speed_6.ToString("0.00");
            double 实际比较 = speed_6 - Jz_speed;
            double 完美比较 = speed_6 + plus - Jz_speed;
            string 实码比 = 实际比较 > 0 ? "+" + 实际比较.ToString("0.00") : 实际比较.ToString("0.00");
            string 完码比 = 完美比较 > 0 ? "+" + 完美比较.ToString("0.00") : 完美比较.ToString("0.00");
            SpeedAnGet[0] = 完美理论 + "|" + 码长理论 + "|" + 跟打实际 + "|" + Hg_speed + "|" +
                            Bg_speed + "|" + St_speed + "|" + Cz_speed + "|" + En_speed;
            sb.AppendLine("     第" + Glob.Pre_Cout + "段跟打分析：");
            sb.AppendLine("+----------------------------------+");
            sb.AppendLine(" 速度码长理论值：" + 码长理论);
            sb.AppendLine(" 速度完美理论值：" + 完美理论 + "(" + 完码比 + ")");
            sb.AppendLine(" 速度跟打实际值：" + 跟打实际 + "(" + 实码比 + ")");
            sb.AppendLine("+----------------------------------+");
            sb.AppendLine(" 回改影响：-" + Hg_speed.ToString("0.00") + " 回改：" + Glob.hgAllUse.ToString("0.00") + "s");
            sb.AppendLine(" 退格影响：-" + Bg_speed.ToString("0.00") + " 退格：" + Math.Abs(Glob.TextBg - Glob.TextHg));
            if (Glob.PauseTimes == 0)
                sb.AppendLine(" 停留影响：-" + St_speed.ToString("0.00") + " 停留：" + 停留.ToString("0.00"));
            sb.AppendLine(" 错字影响：-" + Cz_speed.ToString("0.00") + " 错字：" + Glob.TextCz);
            sb.AppendLine(" 回车影响：-" + En_speed.ToString("0.00") + " 回车：" + Glob.回车);
            sb.AppendLine("+----------------------------------+");
            SpeedAnGet[1] = sb.ToString();
            return SpeedAnGet;
            //MessageBox.Show("实际：" + speed_6 + "\n理论：" + Jz_speed + "\n回改：" + Hg_speed + " " + Glob.hgAllUse +"\n退格：" + Bg_speed + "   " + Math.Abs(Glob.TextBg - Glob.TextHg) + "\n回车：" + En_speed + "    " + Glob.回车 +  "\n停留：" + St_speed + "   " + 停留 + "\n错字：" + Cz_speed + "\n" + speed_ + "\n" + speed_4 + "\n" + Glob.回车);
        }
        //击键占比
        private int GetMaxAndIndex(int[] pa)
        {
            int index = -1;//定义变量存最大值的索引
            int c = pa.Length;
            if (c != 0)
            {
                int Max = pa[0];
                for (int i = 0; i < c; i++)
                {
                    int nowP = pa[i];
                    if (Max < nowP)
                    {
                        index = i;
                        Max = nowP;
                    }
                }
            }
            return index;
        } //得到数组最大值索引

        private int thepre = -1;
        private Color theForeColor;
        private void jjPerCheck(int jP)
        {
            if (Glob.jjAllC <= 0) return;
            for (int i = 0; i <= 17; i += 2)
            {
                this.dataGridView2.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(127, 127, 127);
            }
            double 评定击键 = 0, 评定计数 = 0;
            for (int i = 0, j = 1; i < 9; i++, j += 2)
            {
                double jjP = Glob.jjPer[i] * 100.0 / Glob.jjAllC;
                string jj;
                if (jjP != 0)
                {
                    if (jjP >= 10) { 评定击键 += i + jjP / 100.0; 评定计数++; }
                    //this.dataGridView2.Rows[0].Cells[j].ToolTipText = Glob.jjPer[i] + "/" + Glob.jjAllC;
                    if (j >= 13) this.dataGridView2.Rows[0].Cells[j].Style.ForeColor = Color.Black;
                    if (jjP > 0 && jjP < 1)
                        jj = Math.Round(jjP, 1).ToString();
                    else
                        jj = ((int)jjP).ToString();

                    this.dataGridView2.Rows[0].Cells[j].Value = jj;//Math.Round(jjP, 2);
                    if (jjP >= 20 && jjP < 30)
                    {
                        this.dataGridView2.Rows[0].Cells[j - 1].Style.ForeColor = Color.FromArgb(63, 63, 63);
                        this.dataGridView2.Rows[0].Cells[j].Style.ForeColor = Color.FromArgb(223, 77, 85);
                    }
                    else if (jjP >= 30 && jjP < 50)
                    {
                        this.dataGridView2.Rows[0].Cells[j - 1].Style.ForeColor = Color.FromArgb(63, 63, 63);
                        this.dataGridView2.Rows[0].Cells[j].Style.ForeColor = Color.FromArgb(82, 0, 208);
                    }
                    else if (jjP >= 50 && jjP < 60)
                    {
                        this.dataGridView2.Rows[0].Cells[j - 1].Style.ForeColor = Color.FromArgb(63, 63, 63);
                        this.dataGridView2.Rows[0].Cells[j].Style.ForeColor = Color.FromArgb(255, 64, 0);
                    }
                    else if (jjP >= 60)
                    {
                        this.dataGridView2.Rows[0].Cells[j - 1].Style.ForeColor = Color.FromArgb(44, 44, 44);
                        this.dataGridView2.Rows[0].Cells[j].Style.ForeColor = Color.FromArgb(0, 150, 75);
                    }
                    else
                    {
                        if (j < 13)
                            this.dataGridView2.Rows[0].Cells[j].Style.ForeColor = Color.FromArgb(35, 35, 35); //普通击键颜色
                        this.dataGridView2.Rows[0].Cells[j - 1].Style.ForeColor = Color.FromArgb(127, 127, 127);
                    }
                    if (jP >= 4)
                    {
                        if (jP > 12) jP = 12;
                        if (thepre == -1)
                        {
                            thepre = 2 * jP - 8;
                            theForeColor = this.dataGridView2.Rows[0].Cells[thepre].Style.ForeColor;

                            this.dataGridView2.Rows[0].Cells[thepre].Style.BackColor = Color.Black;
                            this.dataGridView2.Rows[0].Cells[thepre].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            this.dataGridView2.Rows[0].Cells[thepre].Style.BackColor = Color.FromArgb(217, 217, 217);
                            this.dataGridView2.Rows[0].Cells[thepre].Style.ForeColor = theForeColor;

                            thepre = 2 * jP - 8;
                            theForeColor = this.dataGridView2.Rows[0].Cells[thepre].Style.ForeColor;

                            this.dataGridView2.Rows[0].Cells[thepre].Style.BackColor = Color.Black;
                            this.dataGridView2.Rows[0].Cells[thepre].Style.ForeColor = Color.White;
                        }
                    }
                }
            }
            JjCheck(jP); //显示击键
            //this.dataGridView2.Rows[0].Cells[(jP - 4) * 2 + 1].Selected = true;
        }
        //表格模式判断八位是否
        private string check10(string sou)
        {
            if (sou.Length < 10)
            {
                for (int i = sou.Length; i <= 10; i++)
                {
                    sou += " ";
                }
            }
            return sou;
        }
        //各项属性(待建)
        private string 回改量
        {
            get
            {
                if (Glob.TextHg != 0)
                {
                    return " 回改" + Glob.TextHg + "(" + Glob.hgAllUse.ToString("0.00") + "s)";
                }
                else
                {
                    return " 回改" + Glob.TextHg;
                }
            }
        }
        private string 跟打用时
        {
            get
            {
                if (Glob.typeUseTime > 0)
                {
                    DateTime dt = new DateTime().AddSeconds(Glob.typeUseTime);
                    if (dt.Hour == 0)
                        return " 用时" + dt.ToString("m:ss.fff");
                    else
                        return " 用时" + dt.ToString("hh:mm:ss.fff");
                }
                else { return ""; }
            }
        }
        private string 键法
        {
            get { if (Glob.leftHand > Glob.rightHand) return " [左" + Glob.leftHand + ":" + Glob.rightHand + "]"; else if (Glob.leftHand < Glob.rightHand) return " [右" + Glob.rightHand + ":" + Glob.leftHand + "]"; else return ""; }
        }
        private string 回车
        {
            get { return " 回车" + Glob.回车; }
        }
        private string 退格
        {
            get { return " 退格" + Math.Abs(Glob.TextBg - Glob.TextHg); }
        }
        private string 选重
        {
            get { if (Glob.选重 > 0) { return " 选重" + Glob.选重; } else { return ""; } }
        }
        private string 撤销
        {
            get { if (Glob.撤销 > 0) { return " 撤销" + Glob.撤销; } else { return ""; } }
        }
        private string 暂停
        {
            get { if (Glob.PauseTimes > 0) return " 暂停" + Glob.PauseTimes + "次"; else return ""; }
        }
        /// <summary>
        /// 键准
        /// 键准度计算方法：退格一次，相当于两次
        /// </summary>
        private double 键准
        {
            get
            {
                if (Glob.TextJs <= 0) return 0;
                int Low = Glob.TextJs - Math.Abs((Glob.TextBg - Glob.TextHg)) * 2 - Glob.TextMcc;
                if (Low <= 0 || Glob.TextJs <= 0) return 0;
                double 键准度 = (double)Low * 100 / Glob.TextJs;
                //MessageBox.Show(Low + "\n" + Glob.TextJs + "\n" + 键准度 );
                if (键准度 > 0.00 && 键准度 <= 100)
                    return Math.Round(键准度, 2);
                else
                    return 0;
            }
        }

        /// <summary>
        /// 连续回改超过1次
        /// </summary>
        private string 连改
        {
            get
            {
                List<TypeDate> find = Glob.TypeReport.FindAll(o => o.Length < 0);
                int count = 0;
                int c_temp = 0;
                for (int i = 0; i < find.Count - 1; i++)
                {
                    for (int j = i + 1; j < find.Count - 2; j++)
                    {
                        if (find[i].Index != find[j].Index - (j - i))
                        {
                            break;
                        }
                        else
                        {
                            c_temp++;
                        }
                    }
                    if (c_temp > 0)
                    {
                        count++;//连改自增
                        i += c_temp;
                        if (i >= find.Count - 1) i = find.Count - 2;
                        c_temp = 0;
                    }
                }
                if (count != 0)
                    return " 连改" + count;//+ "/" + .Sum(o => o.Length)) + "/" + find.Sum(o => o.TotalTime).ToString("0.00") + "s";
                else
                    return "";
            }
        }

        /// <summary>
        /// 版本输出
        /// </summary>
        private string 版本
        {
            get
            {
                string ins = Glob.Instration + Glob.Ver;
                if (Glob.TextCz > 0)
                {
                    ins += " [错1罚5]";
                }
                return ins;
            }
        }
        //属性END
        private void labelBM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int g = int.Parse(this.labelBM.Text);
                if (g > 0)
                {
                    this.labelBM.ForeColor = Color.IndianRed;
                }
                else
                {
                    this.labelBM.ForeColor = Color.FromArgb(63, 63, 63);
                }
            }
            catch
            {
                this.labelBM.Text = "0";
                this.labelBM.ForeColor = Color.FromArgb(63, 63, 63);
            }
        }

        private void labelhgstatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int g = int.Parse((sender as Label).Text);
                if (g > 0)
                {
                    (sender as Label).ForeColor = Color.DarkRed;
                }
                else
                {
                    (sender as Label).ForeColor = Color.FromArgb(191, 191, 191);
                }
            }
            catch
            {
                (sender as Label).Text = "0";
                (sender as Label).ForeColor = Color.FromArgb(191, 191, 191);
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (sw > 1)
            {
                TimeSpan span = DateTime.Now - Glob.nowStart;
                int now = (int)span.TotalSeconds;
                this.lblAutoReType.Text = now.ToString();
                if (now > Glob.StopUse * 60)
                {
                    F3();

                    this.lblAutoReType.Text = "0";
                    MessageBox.Show("长时间未跟打，已自动重打！\n可在《设置》-《发送与控制》 - 《停止时间》处调整。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void lblAutoReType_TextChanged(object sender, EventArgs e)
        {
            int get = int.Parse(lblAutoReType.Text);
            if (get <= Glob.StopUse * 18) { lblAutoReType.ForeColor = Color.Black; }
            else if (get > Glob.StopUse * 18 && get <= Glob.StopUse * 36) { lblAutoReType.ForeColor = Color.DarkGreen; }
            else { lblAutoReType.ForeColor = Color.IndianRed; }
        }

        public string 字数格式化(int 字数)
        {
            if (字数 >= 10000)
            {
                return Math.Round((double)字数 / 10000, 2).ToString("0.00") + "万";
            }
            else
            {
                return 字数 + "";
            }
        }

        public string 添雨验证(string a)
        {
            byte[] result = Encoding.Default.GetBytes(a);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string get = BitConverter.ToInt64(output, 0).ToString();
            return get.Substring(get.Length - 6, 5);
        }

        public string 精五验证(int speed, int pressSpeed, int perLen, int totTime, string QQ)//速度、击键、码长、用时
        {
            string str = "";
            str = str + (((speed + perLen) + pressSpeed) % 10);
            int num = 4;
            int actLen = this.richTextBox1.TextLength;
            if (actLen < 5) //总字数
            {
                num = 0;
            }
            int num2 = (this.richTextBox1.Text[num] + (speed / 100)) % 10;
            int num3 = (Convert.ToInt32((int)(QQ[QQ.Length - 1] - '0')) + actLen) % 10;
            return ((str + num2.ToString()) + num3.ToString() + string.Format("{0:000}", (((totTime % 0x3e8) * actLen) + actLen) % 0x3e5));
        }

        public void isornoSend(string title, string TotalSend)
        {
            //IntPtr QQwinn = FindWindow(null, title);
            if (Glob.sendOrNo)
            {
                if (NewSendText.发文状态)
                {
                    if (!NewSendText.是否独练)
                    {
                        if (MessageBox.Show(TotalSend, "是否发送成绩？是发送，否放入剪切板。", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //SwitchToThisWindow(QQwinn, true);
                            if (this.PicSend.Checked & !Glob.isMatch)
                                send_picGoal();
                            else
                                sendtext(TotalSend);
                        }
                        else
                        {
                            ClipboardHandler.SetTextToClipboard(TotalSend);
                        }

                    }
                }
                else
                {
                    if (MessageBox.Show(TotalSend, "是否发送成绩？是发送，否放入剪切板。", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (this.PicSend.Checked & !Glob.isMatch)
                            send_picGoal();
                        else
                            sendtext(TotalSend);
                    }
                    else
                    {
                        ClipboardHandler.SetTextToClipboard(TotalSend);
                    }
                }
            }
            else
            {
                if (NewSendText.发文状态)
                {
                    if (!NewSendText.是否独练)
                    {
                        //SwitchToThisWindow(QQwinn, true);
                        if (this.PicSend.Checked & !Glob.isMatch)
                            send_picGoal();
                        else
                            sendtext(TotalSend);
                        SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                    }
                }
                else
                {
                    if (this.PicSend.Checked & !Glob.isMatch)
                        send_picGoal();
                    else
                        sendtext(TotalSend);
                }
            }
        }

        private void 复制图片成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.HaveTypeCount > 0)
                send_picGoal();
        }

        private void send_picGoal()
        {
            using (PicGoal_Class pgc = new PicGoal_Class())
            {
                Clipboard.SetImage(pgc.GetPic((float)键准 / 100, this.lblTitle.Text));
            }
            SendClipBoardToQQ();
        }

        public class ClipboardHandler
        {
            /**/
            /// <summary>
            /// 是否可以从操作系统剪切板获得文本
            /// </summary>
            /// <returns>true 可以从操作系统剪切板获得文本,false 不可以</returns>
            public static bool CanGetText()
            {
                // Clipboard.GetDataObject may throw an exception...
                try
                {
                    System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
                    return data != null && data.GetDataPresent(System.Windows.Forms.DataFormats.Text);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            //　　　　/// <summary>
            //　　　　/// 是否可以向操作系统剪切板设置文本
            //　　　　/// </summary>
            //　　　　/// <returns></returns>
            //　　　　public static bool CanSetText()
            //　　　　{
            //　　　　　　return true;
            //　　　　}
            /**/
            /// <summary>
            /// 向操作系统剪切板设置文本数据
            /// </summary>
            /// <param name="strText">文本数据</param>
            /// <returns>操作是否成功</returns>
            public static bool SetTextToClipboard(string strText)
            {
                if (strText != null && strText.Length > 0)
                {
                    try
                    {
                        var dataObject = new DataObject();
                        dataObject.SetData(DataFormats.UnicodeText, true, strText);
                        Clipboard.SetDataObject(dataObject, true);
                        return true;
                    }
                    catch
                    {

                    }
                }
                return false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Alt)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void maChangGet(object sender, KeyEventArgs e)
        {
            if (e.Alt)
                e.Handled = true;

            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.F3 && e.KeyCode != Keys.F4 && e.KeyCode != Keys.F5)
            {
                Glob.TextJs++;
                labelJsing.Text = Glob.TextJs.ToString();
                Glob.nowStart = DateTime.Now; //停止用时
            }
            //MessageBox.Show(e.KeyCode.ToString());
            if (e.KeyCode == Keys.ProcessKey)
            {
                Glob.是否选重 = true;
            }
            else
            {
                Glob.是否选重 = false;
            }

            if (e.KeyCode == Keys.Back)
            {
                Glob.TextHg++;
                //回改地点
                if (!Glob.TextHgPlace.Contains(this.textBoxEx1.TextLength))
                    Glob.TextHgPlace.Add(this.textBoxEx1.TextLength);

                Glob.TextMcc += Glob.TextMc; //在此回退的情况 键准处理
                labelhgstatus.Text = Glob.TextHg.ToString();//回改
                Glob.nowStart = DateTime.Now; //停止用时
                if (this.textBoxEx1.TextLength == 0)
                {
                    F3();
                }
            }

            if (isPause)
            {
                //暂停关闭了
                sTime = DateTime.Now;
                timer1.Start();
                timer2.Start();
                timer3.Start();
                timer5.Start();
                EndPause();
            }
        }

        private void textBoxEx1_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = textBoxEx1.Text;
            Glob.TypeTextCount = textBoxEx1.TextLength;
            if (this.picBmTips.Checked && Glob.BmTips.Count > 0)
            {
                查询当前编码ToolStripMenuItem2_Click(sender, null);
            }
        }
        #endregion



        #region 全局快捷键设置
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            control = 2,
            Shift = 4,
            Windows = 8
        }

        [DllImport("User32")]
        public extern static bool GetCursorPos(ref Point cPoint);
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr fGetForegroundWindow();
        private string GetCheck()
        {
            string get_ = Clipboard.GetDataObject().GetData(DataFormats.StringFormat).ToString();
            Regex checkIf = new Regex("\r(?!\n)");
            if (checkIf.IsMatch(get_))
            {
                get_ = get_.Replace("\r", "\r\n");  //匹配则是QQ2009+
            }
            return get_;
        }
        public static string GetTextFromClipboard()
        {
            try
            {
                System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
                if (data.GetDataPresent(System.Windows.Forms.DataFormats.UnicodeText))
                {
                    string strText = (string)data.GetData(System.Windows.Forms.DataFormats.UnicodeText);
                    return strText;
                }
            }
            catch
            { }
            return null;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            const int WM_NCHITTEST = 0x0084;


            int HTCLIENT = 1;
            int HTLEFT = 10;
            int HTRIGHT = 11;
            int HTTOP = 12;
            int HTTOPLEFT = 13;
            int HTTOPRIGHT = 14;
            int HTBOTTOM = 15;
            int HTBOTTOMLEFT = 16;
            int HTBOTTOMRIGHT = 17;

            int offset = 3;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if ((int)m.WParam == 2)
                    { //F4 获取文字
                        F4();
                    }
                    else if ((int)m.WParam == 3) //重打全局
                    {
                        SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                        if (GetForegroundWindow().ToInt32() != this.Handle.ToInt32())
                        {
                            this.Show();
                            this.Activate();
                            this.textBoxEx1.Select();
                            if (Glob.TypeTextCount > 0 && this.textBoxEx1.TextLength != this.richTextBox2.TextLength)
                                ShowFlowText("已激活跟打器，未设重打~");
                            return;
                        }
                        F3();
                    }
                    else if ((int)m.WParam == 4)
                    { //F6
                        if (NewSendText.发文状态)// (zdSendText.isHand) //手动模式
                        {
                            //this.textBox1.TextChanged -= new System.EventHandler(textBoxEx1_TextChanged);
                            SendAOnce();
                            //SendAOnce();
                            F3();
                            //this.textBox1.TextChanged += new System.EventHandler(textBoxEx1_TextChanged);
                        }
                    }
                    break;

                case WM_NCHITTEST:
                    int px = Form.MousePosition.X - this.Left;
                    int py = Form.MousePosition.Y - this.Top;

                    int temp;

                    if (px >= this.Width - offset)
                    {
                        if (py <= offset) temp = HTTOPRIGHT;
                        else if (py >= this.Height - offset) temp = HTBOTTOMRIGHT;
                        else temp = HTRIGHT;
                    }
                    else if (px <= offset)
                    {
                        if (py <= offset) temp = HTTOPLEFT;
                        else if (py >= this.Height - offset) temp = HTBOTTOMLEFT;
                        else temp = HTLEFT;
                    }
                    else
                    {
                        if (py <= offset) temp = HTTOP;
                        else if (py >= this.Height - offset) temp = HTBOTTOM;
                        else temp = HTCLIENT;
                    }
                    m.Result = (IntPtr)temp;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            //base.WndProc(ref m);
        }

        private void richText2Event(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Z & e.Control)
            {
                Glob.撤销++;
                this.textBoxEx1.Undo();
                //MessageBox.Show(this.textBox1.CanUndo.ToString());
            }
        }

        public string getQQText()
        {
            string title = lblQuan.Text;
            if (title != "所在群")
            {
                //string isQQ2012 = FindWindow("TXGuiFoundation", "QQ2012").ToString();
                //string isQQ2011 = FindWindow("TXGuiFoundation", "QQ2011").ToString();
                //if (isQQ2011 != "0" && isQQ2012 != "0") { MessageBox.Show("含有多个版本的QQ，请关闭其他非跟打版本QQ，或者统一用同一版本QQ！"); return false; }
                //if (isQQ2011 == "0" && isQQ2012 == "0") { isQQ2012 == "0"; }
                Point mpos = new Point();
                SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                IntPtr win = FindWindow(null, title);
                if (win.ToString() != "0")
                {
                    Clipboard.Clear();
                    SwitchToThisWindow(FindWindow(null, title), true); //激活窗口
                    Delay(Glob.DelaySend);
                    if (Glob.getStyle) //获取方式
                    {
                        SendKeys.SendWait("{TAB}");//跳到聊天窗口
                    }
                    else
                    {
                        GetCursorPos(ref mpos);
                        RECT rect = new RECT();
                        IntPtr get = FindWindow("TXGuiFoundation", title);
                        GetWindowRect(get, ref rect);
                        SetCursorPos(rect.Left + 20, rect.Top + 200);
                        Delay(Glob.DelaySend);
                        mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, win);
                        mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, win);
                        //Application.DoEvents();
                    }

                    Delay(Glob.DelaySend);
                    SendKeys.SendWait("^a");//全选
                    Delay(50);
                    SendKeys.SendWait("^c");//复制
                    Delay(80); //必须给定的延迟
                    string get_ = "";
                    try
                    {
                        get_ = Clipboard.GetDataObject().GetData(DataFormats.StringFormat).ToString();
                        Regex checkIf = new Regex("\r(?!\n)");
                        if (checkIf.IsMatch(get_))
                        {
                            get_ = get_.Replace("\r", "\r\n");  //匹配则是QQ2009+
                        }
                    }
                    catch
                    {
                        this.Activate();
                        Delay(Glob.DelaySend);
                        SendKeys.SendWait("^s");
                        Glob.F4Cut = false;
                        return "";
                        //}
                    }
                    if (!Glob.getStyle)
                        SetCursorPos(mpos.X, mpos.Y); //放回鼠标
                    return get_;
                }
                else
                {
                    if (!Glob.getStyle)
                        SetCursorPos(mpos.X, mpos.Y); //放回鼠标
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public void F4()
        {
            if (sw != 0) { F3(); return; }
            if (Glob.F4Cut) { Glob.F4Cut = false; return; }
            Glob.F4Cut = true;
            Point p = new Point(1, 1);
            if (Sw != 0) { Sw = 0; F3(); }
            string title = lblQuan.Text;
            if (title != "所在群")
            {
                GetCursorPos(ref p);
                string text_ = Glob.Text;
                Glob.Text = getQQText(); //获取到跟打文字
                if (Glob.Text.Length == 0)
                {
                    return;
                }
                text_ = Glob.Text;
                //MessageBox.Show(text_);
                //SwitchToThisWindow(FindWindow(null, Glob.Form), true);
                string pretext, preduan;
                if (Glob.isZdy)
                {
                    pretext = Glob.PreText.Replace(@"\", @"\\");
                    preduan = Glob.PreDuan.Replace("xx", @"\d+");
                }
                else
                {
                    pretext = "-----";
                    preduan = @"第\d+段";
                }

                Regex regexAll = new Regex(@".+\s.+\s" + pretext + preduan + ".+", RegexOptions.RightToLeft); //获取发送的全部信息
                Glob.getDuan = regexAll.Match(text_);
                if (Glob.getDuan.Length == 0) //为空
                {
                    toolStripStatusLabelStatus.Text = "No";
                    toolTip1.SetToolTip(toolStripStatusLabelStatus, "没有找到文段");
                    ShowFlowText(Glob.jwMatchMoudle ? "精五模式状态下未找到文段" : "未找到文段");
                    toolStripStatusLabelStatus.ForeColor = Color.IndianRed;
                    Glob.F4Cut = false;
                    return;
                }
                string getDuanAll = Glob.getDuan.ToString();
                if (Glob.isZdy)
                {
                    Glob.regexCout = new Regex(@"(?<=" + preduan.Substring(0, 1) + @")\d+(?=" + preduan.Substring(4, 1) + ")", RegexOptions.RightToLeft);
                }
                else
                    Glob.regexCout = new Regex(@"(?<=第)\d+(?=段)", RegexOptions.RightToLeft);
                LoadText(pretext, preduan, Glob.regexCout, getDuanAll);
                Glob.F4Cut = false;
            }
        }

        public void PutText()
        {
            string text_ = Glob.Text;
            Glob.Text = Clipboard.GetText(); //获取到跟打文字
            if (Glob.Text.Length == 0) { return; }
            text_ = Glob.Text;
            //MessageBox.Show(text_);
            //SwitchToThisWindow(FindWindow(null, Glob.Form), true);
            string pretext, preduan;
            if (Glob.isZdy)
            {
                pretext = Glob.PreText.Replace(@"\", @"\\");
                preduan = Glob.PreDuan.Replace("xx", @"\d+");
            }
            else
            {
                pretext = "-----";
                preduan = @"第\d+段";
            }

            Regex regexAll = new Regex(@".+\s.+\s" + pretext + preduan + ".+", RegexOptions.RightToLeft); //获取发送的全部信息
            Glob.getDuan = regexAll.Match(text_);
            if (Glob.getDuan.Length == 0) //为空
            {
                toolStripStatusLabelStatus.Text = "No";
                toolTip1.SetToolTip(toolStripStatusLabelStatus, "没有找到文段");
                ShowFlowText(Glob.jwMatchMoudle ? "精五模式状态下未找到文段" : "未找到文段");
                toolStripStatusLabelStatus.ForeColor = Color.IndianRed;
                return;
            }
            string getDuanAll = Glob.getDuan.ToString();
            if (Glob.isZdy)
            {
                Glob.regexCout = new Regex(@"(?<=" + preduan.Substring(0, 1) + @")\d+(?=" + preduan.Substring(4, 1) + ")", RegexOptions.RightToLeft);
            }
            else
                Glob.regexCout = new Regex(@"(?<=第)\d+(?=段)", RegexOptions.RightToLeft);
            LoadText(pretext, preduan, Glob.regexCout, getDuanAll);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //按钮更新位置
            //this.lblClose.Location = new Point(this.Width - 50,0);
        }

        public void LoadText(string pretext, string preduan, Regex regexCout, string getDuanAll)
        {
            Initialize(1);//数值初始化
            string PerText = richTextBox1.Text; //之前的文段
            Regex regexText, regexTitle;
            Match getText, getCout, getTitle;
            regexText = new Regex(@".+(?=\s" + pretext + ")");
            //MessageBox.Show(preduan);
            regexTitle = new Regex(@".+(?=\s)");
            getText = regexText.Match(getDuanAll); //获取文章
            string ExgetText = getText.ToString().Trim();
            getCout = regexCout.Match(getDuanAll);//获取段号
            getTitle = regexTitle.Match(getDuanAll); //获取标题
            //填入及初始化各项值
            timer1.Enabled = false;

            this.SeriesSpeed.Points.Clear();
            if (ExgetText != "")
            {
                if (ExgetText != PerText) //获取新文段
                {
                    richTextBox1.Text = ExgetText.ToString(); //填入文章
                    Glob.LoadCount++; //载入次数已载
                    toolStripStatusLabelStatus.Text = Glob.LoadCount.ToString();
                    toolStripStatusLabelStatus.ForeColor = Color.FromArgb(63, 63, 63);
                    toolTip1.SetToolTip(toolStripStatusLabelStatus, "已载第" + Glob.LoadCount + "段");

                    Glob.reTypeCount = 0; //重打
                    this.textBoxEx1.TextChanged -= new System.EventHandler(textBoxEx1_TextChanged);
                    textBoxEx1.Clear();
                    this.textBoxEx1.TextChanged += new System.EventHandler(textBoxEx1_TextChanged); //重新绑定
                    Initialize(2);//显示初始化
                    //Initialize(1);
                    //处理文章
                    lblTitle.Text = getTitle.ToString().Trim(); //文段标题
                    toolTip1.SetToolTip(lblTitle, getTitle.ToString().Trim());
                    try
                    {
                        lblDuan.Text = preduan[0].ToString() + getCout + preduan[preduan.Length - 1];
                    }
                    catch
                    {
                        lblDuan.Text = "第" + getCout + "段";
                    }
                    this.title1.Text = lblDuan.Text + " -" + Glob.Instration;
                    Glob.Pre_Cout = getCout.ToString();
                    textBoxEx1.ReadOnly = false;
                    //richTextBox2.MaxLength = richTextBox1.TextLength; //设置最大输入字符数量

                    //MessageBox.Show(Glob.LoadCount.ToString());

                    if (Glob.SpeedControl > 0)
                    {   //找到新文段时间关闭测速
                        Glob.SpeedPoint_ = new int[10];//测速点控制
                        Glob.SpeedTime = new double[10];//测速点时间控制
                        Glob.SpeedJs = new int[10];//键数
                        Glob.SpeedHg = new int[10];//回改
                        Glob.SpeedPointCount = 0;//测速点数量控制
                        Glob.SpeedControl = 0;
                        this.lblspeedcheck.Text = "时间";
                    }
                    if (getCout.ToString() == "999") setMatch(true);//比赛认证段
                    else setMatch(false);

                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        ListDuan(getCout.ToString());//列出段号
                    }));

                }
                else
                {
                    if (textBoxEx1.Text != "")
                    {
                        toolStripStatusLabelStatus.Text = "NA";
                        toolTip1.SetToolTip(toolStripStatusLabelStatus, "未找到新文段");
                        ShowFlowText("未找到新文段");
                        toolStripStatusLabelStatus.ForeColor = Color.PaleVioletRed;
                    }
                }
            }
            else
            {
                toolStripStatusLabelStatus.Text = "No";
                toolTip1.SetToolTip(toolStripStatusLabelStatus, "没有找到文段");
                ShowFlowText(Glob.jwMatchMoudle ? "精五模式状态下未找到文段" : "未找到文段");
                toolStripStatusLabelStatus.ForeColor = Color.Red;

            }
            GetInfo(); //获取文字信息
            richTextBox1.ForeColor = Color.Black;
            //Glob.Text = null;
            Clipboard.Clear();
            this.Activate();
            //SwitchToThisWindow(FindWindow(null, Glob.Form), true);
            //BlockMark();//空格标记
        }

        public void setMatch(bool set)
        {
            if (set)
            {
                Glob.isMatch = true; lblDuan.Text = "比赛认证段";
                if (this.比赛时自动打开寻找测速点ToolStripMenuItem.Checked)
                    this.自动寻找赛文标记ToolStripMenuItem.PerformClick();
                ShowFlowText("当前为比赛认证段，已启用所有设置。");
            }
            else
            {
                Glob.isMatch = false;
                lblDuan.Text = "第" + Glob.Pre_Cout + "段";
            }
        }

        private void 转换为比赛文段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Glob.isMatch)
            {
                setMatch(true);
            }
            else
            {
                setMatch(false);
            }
        }

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        public void F3()
        {
            this.textBoxEx1.TextChanged -= new System.EventHandler(textBoxEx1_TextChanged);
            textBoxEx1.Clear();
            this.textBoxEx1.TextChanged += new System.EventHandler(textBoxEx1_TextChanged);
            //MessageBox.Show(richTextBox2.Text);
            if (Sw != 0)
            {
                int typecount;
                Glob.reTypeCount++;
                typecount = Glob.reTypeCount + 1;
                toolStripStatusLabelStatus.Text = typecount.ToString();
                toolStripStatusLabelStatus.ForeColor = Color.DarkGreen;
                toolTip1.SetToolTip(toolStripStatusLabelStatus, "重打" + typecount + "次");
            }

            timer1.Enabled = false;
            timer3.Enabled = false;
            this.SeriesSpeed.Points.Clear();
            textBoxEx1.ReadOnly = false;
            Initialize(1);//数值初始化
            Initialize(2);//显示初始化

            textBoxEx1.Select();
            GetInfo();
            timer5.Stop();
            Glob.nowStart = DateTime.Now;
            this.lblAutoReType.Text = "0";
            //设置输入状态
            //SetIMM(1026, 0);
            if (Glob.SpeedPointCount > 0)
            {
                for (int i = 0; i < Glob.SpeedPointCount; i++)
                {
                    this.richTextBox1.SelectionStart = Glob.SpeedPoint_[i];
                    this.richTextBox1.SelectionLength = 1;
                    this.richTextBox1.SelectionBackColor = Color.LightGray;
                }
                Array.Clear(Glob.SpeedTime, 0, Glob.SpeedTime.Length);
                Array.Clear(Glob.SpeedJs, 0, Glob.SpeedJs.Length);
                Array.Clear(Glob.SpeedHg, 0, Glob.SpeedHg.Length);
                Glob.SpeedControl = 0;
                this.lblspeedcheck.Text = "测速" + Glob.SpeedPointCount;
            }
            else
            {
                this.lblspeedcheck.Text = "时间";
            }
            //GC.Collect();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            F5();
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, IntPtr extraInfo);

        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);

        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        public void F5()
        {
            //mouse_event(MouseEventFlag.Move, 0, 0, 0, get);
            try
            {
                Glob.GetWinC = 0;//初始化 获取窗口的数量
                Glob.GetWin = new string[40, 2];//数组 初始化
                CallBack myCallBack = new CallBack(EnumWindowsApp.Report);
                EnumWindows(myCallBack, 0);
                if (Glob.GetWinC > 0)
                {
                    lblQuan.Text = Glob.GetWin[Glob.WinSwitch, 0];//显示该群名
                    Glob.WinSwitch++;
                    if (Glob.WinSwitch >= Glob.GetWinC)
                    {
                        Glob.WinSwitch = 0;
                    }
                    if (Glob.GetWinC == 1) ShowFlowText("只找到一个群");
                }
                else
                {
                    lblQuan.Text = "所在群";
                }
                refreshQun();//更新换群菜单项目
            }
            catch (Exception err2)
            {
                lblQuan.Text = err2.Message;
            }
        }
        public void GetInfo()
        {
            if (Glob.autoReplaceBiaodian)
            {
                this.richTextBox1.Text = reText(this.richTextBox1.Text);
            }
            var tl = richTextBox1.TextLength;
            Glob.TextLen = tl;
            Glob.TypeText = richTextBox1.Text;//跟打文字 存储
            textBoxEx1.MaxLength = tl;
            lblCount.Text = tl.ToString() + "字";
            lblMatchCount.Text = 添雨验证(添雨验证(richTextBox1.Text));
            if (Glob.isPointIt && !Glob.是否智能测词)
                TickIt();
            //智能测词
            if (Glob.TextPreCout != this.lblMatchCount.Text)
                if (Glob.是否智能测词)
                {
                    if (已测 != this.lblMatchCount.Text)
                    {
                        委托测词();
                    }
                }
            //标记功能
            if (!Glob.binput) return;
            var chineseRegex = new Regex(@"[\u4E00-\u9FA5]");
            if (chineseRegex.IsMatch(this.richTextBox1.Text))
            {
                //InputLan(savesetup.srf, this.textBoxEx1.Handle);
                //SetIMM(1026, 0);//中文输入
                Glob.binput = false;
                Glob.文段类型 = true;
            }
            else
            {
                //InputLan("简体中文 - 美式键盘", this.textBoxEx1.Handle);
                //fSetIMM(257, 0);//转换为英文输入
                Glob.binput = true;
                Glob.文段类型 = false;
            }
        }
        public class EnumWindowsApp
        {
            public static bool Report(int hwnd, int lParam)
            {
                var r = new Regex(@"QQ20\d{2}");
                var ex = new[]
                    {
                        "TXMenuWindow",
                        "FaceSelector",
                        "TXFloatingWnd",
                        "腾讯",
                        "消息盒子",
                        "来自",
                        "分类推荐",
                        "更换房间头像",
                        "网络设置",
                        "消息管理器",
                        "QQ数据线",
                        "骏伯网络科技"
                    };
                var s = new StringBuilder(512);
                GetWindowText(hwnd, s, s.Capacity);
                var title = s.ToString();
                if (!r.IsMatch(title) && !ex.Contains(title) && !string.IsNullOrEmpty(title))
                //if (!title.Contains("QQ") && !title.Contains("TX") && !title.Contains("FaceSelector") && !title.Contains("腾讯网") && !title.Contains("消息盒子"))
                {
                    var g = new StringBuilder(512);
                    GetClassName(hwnd, g, 256);
                    if (g.ToString() == "TXGuiFoundation")
                    {
                        Glob.GetWin[Glob.GetWinC, 0] = title;//窗口标题
                        Glob.GetWin[Glob.GetWinC, 1] = hwnd.ToString();
                        Glob.GetWinC += 1;
                    }
                }
                return true;
            }
        }

        #endregion

        #region 跟打过程中的控制
        //颜色
        private void labelSpeeding_TextChanged(object sender, EventArgs e)
        {
            /*double s = double.TryParse(this.labelSpeeding.Text, out s) ? s : 0;
            if (s >= 180 && s < 220) {
                this.labelSpeeding.ForeColor = Color.FromArgb(0,0,255);
            }
            else if (s >= 220)
            {
                this.labelSpeeding.ForeColor = Color.FromArgb(0,70,176);
            }
            else {
                this.labelSpeeding.ForeColor = Color.FromArgb(63, 63, 63);
            }*/
        }

        private void labelJjing_TextChanged(object sender, EventArgs e)
        {
            /*
            double s = double.TryParse((sender as Label).Text, out s) ? s : 0;
            if (s > 9.99 && s <= 11) {
                this.labelJjing.ForeColor = Color.FromArgb(163,29,143);
            }
            else if (s > 11)
            {
                this.labelJjing.ForeColor = Color.FromArgb(153,14,14);
            }
            else {
                this.labelJjing.ForeColor = Color.FromArgb(63,63,63);
            }*/
        }

        public static bool Delay(int delayTime) //延时函数
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = (int)spand.TotalMilliseconds;
                //Application.DoEvents();
            }
            while (s < delayTime);
            return true;

        }


        #region 暂停处理
        private TimeSpan TimeStopA_ = new TimeSpan();
        private bool isPause = false;
        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PauseType())
            {
                MessageBox.Show("暂停启动失败！或已暂停！");
            }
        }

        public bool PauseType()
        {
            if (!isPause && sw > 0 && this.richTextBox1.TextLength != this.textBoxEx1.TextLength)
            {
                try
                {
                    timer1.Stop();//计时
                    timer2.Stop();//限时速度显示
                    timer3.Stop();//即时图表
                    timer5.Stop();//重打时间计时
                    TimeStopAll += TimeStopA_;
                    isPause = true;
                    this.labelSpeeding.Text = (this.textBoxEx1.TextLength * 60 / Glob.typeUseTime).ToString("0.00");
                    //labelTimeFlys.ForeColor = Color.IndianRed;
                    timerLblTime.Start();
                    this.Text += " [已暂停]";
                    Glob.PauseTimes++;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //失去焦点自动暂停
        private void textBoxEx1_LostFocus(object sender, EventArgs e)
        {
            PauseType();
        }

        private bool LblTimeFlash = true;
        private void timerLblTime_Tick(object sender, EventArgs e)
        {
            if (LblTimeFlash)
            {
                labelTimeFlys.ForeColor = Color.IndianRed;
                LblTimeFlash = false;
            }
            else
            {
                labelTimeFlys.ForeColor = Color.FromArgb(244, 244, 244);
                LblTimeFlash = true;
            }
        }

        private void EndPause()
        {
            timerLblTime.Stop();
            LblTimeFlash = false;
            labelTimeFlys.ForeColor = Color.Black;
            isPause = false;
            this.Text = Glob.Form;
        }
        private void labelTimeFlys_Click(object sender, EventArgs e)
        {
            PauseType();
        }
        #endregion


        private void timer1_Tick(object sender, EventArgs e) //时间计时
        {
            TimeSpan span = DateTime.Now - sTime;
            TimeStopA_ = span;
            span += TimeStopAll;
            DateTime n = new DateTime(span.Ticks);
            labelTimeFlys.Text = n.ToString("mm:ss.ff"); //显示时间
            Glob.typeUseTime = span.TotalSeconds; //计算总秒数 小数点后两位
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int inputL = textBoxEx1.TextLength;
            if (inputL > 5)
            {
                int len = richTextBox2.TextLength - Glob.TextJc;
                double speed2 = (double)len * 60 / Glob.typeUseTime;
                if (speed2 > 999) { speed2 = 999; }
                Glob.chartSpeedTo = speed2;
                double mc = (double)Glob.TextJs / (inputL - Glob.TextJc);
                double jj = (double)Glob.TextJs / Glob.typeUseTime;

                if (!Glob.notShowjs)
                {
                    labelmcing.Text = mc.ToString("0.00");
                    labelSpeeding.Text = speed2.ToString("0.00");
                    labelJjing.Text = jj.ToString("0.00");
                }

                if (inputL > 10)
                {
                    if (speed2 > Glob.MaxSpeed)
                        Glob.MaxSpeed = speed2;
                    if (jj > Glob.MaxJj)
                        Glob.MaxJj = jj;
                    if (mc < Glob.MaxMc)
                        Glob.MaxMc = mc;
                }
            }
        }
        #endregion

        #region 输入法


        public void InputLan(string InputL, IntPtr intptr)
        {
            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                if (iL.LayoutName == InputL)
                {
                    InputLanguage.CurrentInputLanguage = iL;
                    break;
                }
            }
            //SetIMM(1026,0);
        }

        private void SetIMM(int iMode, int iSentence)
        {
            //iMode = 1025;
            //iSentence = 0;
            IntPtr prt = ImmGetContext(this.textBoxEx1.Handle);
            ImmSetConversionStatus(prt, iMode, iSentence);
        }
        #endregion

        #region 设置窗口的打开
        private void button1_Click(object sender, EventArgs e)
        {
            TSetup SetupA = new TSetup(this);
            SetupA.ShowDialog();
        }
        #endregion

        #region 关闭后的设置
        private void CloseTyping(object sender, FormClosedEventArgs e)
        {
            int tX = this.Location.X;//横坐标
            int tY = this.Location.Y;
            int tW = this.Size.Width;
            int tH = this.Size.Height;
            Rectangle rect = SystemInformation.WorkingArea;
            int width = rect.Width;
            int height = rect.Height;

            _Ini iniSetup = new _Ini("Ttyping.ty");
            iniSetup.IniWriteValue("窗口位置", "横", tX.ToString());
            iniSetup.IniWriteValue("窗口位置", "纵", tY.ToString());
            if (tW <= width / 2)
            {
                iniSetup.IniWriteValue("窗口位置", "宽", tW.ToString());
                iniSetup.IniWriteValue("窗口位置", "高", tH.ToString());
            }

            if (!isShowAll)
            {
                if (tW <= width / 2)
                {
                    int p11H = this.splitContainer1.Panel1.ClientSize.Height;
                    int p31H = this.splitContainer3.Panel1.ClientSize.Height;
                    //if (this.toolStripButton4.Checked)
                    //{
                    //    p11H = Glob.p1;
                    //    p31H = Glob.p2;
                    //}

                    iniSetup.IniWriteValue("拖动条", "高1", p11H.ToString());
                    iniSetup.IniWriteValue("拖动条", "高2", p31H.ToString());
                }
            }
            //iniSetup.IniWriteValue("发送", "起始", zdSendText.tSendTimes.ToString());
            iniSetup.IniWriteValue("记录", "总字数", Glob.TextLenAll.ToString());
            iniSetup.IniWriteValue("记录", "总回改", Glob.TextHgAll.ToString());
            iniSetup.IniWriteValue("今日跟打", DateTime.Today.ToShortDateString(), Glob.todayTyping.ToString());
            iniSetup.IniWriteValue("记录", "记录总字数", Glob.TextRecLenAll.ToString());
            iniSetup.IniWriteValue("记录", "记录天数", Glob.TextRecDays.ToString());
            /*Glob.TextRecLenAll = int.Parse(IniRead("记录", "记录总字数", "0"));
        Glob.TextRecDays = int.Parse(IniRead("记录", "记录天数", "0"));*/
            for (int i = 0; i < 9; i++)
            {
                iniSetup.IniWriteValue("记录", i.ToString(), Glob.jjPer[i].ToString());
            }
            iniSetup.IniWriteValue("记录", "总数", Glob.jjAllC.ToString());
        }

        #endregion

        #region 标记功能
        private void richtextBoxEx1_TextChanged(object sender, EventArgs e) //对照区的值改变时
        {
            //MarkIt();
            //richTextBox1.Rtf += @"";// + richTextBox1.Rtf;
            //Application.DoEvents();
        }

        public void MarkIt()
        {
            string x = richTextBox1.Text;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == ' ')
                {
                    Color c = Color.FromName(getStrColor());
                    richTextBox1.SelectionStart = i;
                    richTextBox1.SelectionLength = 1;
                    richTextBox1.SelectionBackColor = Color.LightGray;
                }
            }
        }

        private static Random rand = new Random();
        public static string getStrColor()
        {
            Color color = Color.FromArgb(rand.Next());
            string strColor = "#" + Convert.ToString(color.ToArgb(), 16).PadLeft(8, '0').Substring(2, 6);
            return strColor;
        }

        /*public void BlockMark() { //空格标记
            string strRTF = richTextBox1.Rtf;
            richTextBox1.Clear();

            int iCTableStart = strRTF.IndexOf("colortbl;");
            if (iCTableStart != -1)
            {
                int iCTableEnd = strRTF.IndexOf('}', iCTableStart);
                strRTF = strRTF.Remove(iCTableStart, iCTableEnd - iCTableStart);
                strRTF = strRTF.Insert(iCTableStart, "colortbl;\\red255\\green0\\blue0;\\red0\\green128\\blue0;red0\\green0\\blue255;}");
            }
            else
            {
                int iRTFloc = strRTF.IndexOf("\\rtf");
                int iInsertLoc = strRTF.IndexOf('{', iRTFloc);
                if (iInsertLoc == -1) iInsertLoc = strRTF.IndexOf('}', iRTFloc - 1);
                strRTF = strRTF.Insert(iInsertLoc, "{\\colortbl;\\red128\\green0\\blue0;\\red0\\green128\\blue0;red0\\green0\\blue255;}");
            }
            //MarkIt(strRTF);
            int s = strRTF.IndexOf(' ');
            //MessageBox.Show(s.ToString());
            strRTF = strRTF.Replace(" ", @"{\ulwave\cf3  }");
            richTextBox1.Rtf = strRTF;
        }
        */

        #endregion  //尝试

        #region 跟打历史
        private void CellContentClick(object sender, DataGridViewCellEventArgs e)//表格按钮点击
        {
            // if (e.RowIndex >= 0) {
            //   DataGridViewColumn column = dataGridView1.Columns[e.RowIndex];
            // if (column is DataGridViewButtonColumn) {
            //    MessageBox.Show(column.DataPropertyName);
            //}
            // }
        }
        #endregion

        #region 历史曲线
        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName); // ANSI版本

        public ArrayList ReadKeys(string sectionName)
        {
            string str1 = Environment.CurrentDirectory;
            byte[] buffer = new byte[5120];
            int rel = GetPrivateProfileStringA(sectionName, null, "", buffer, buffer.GetUpperBound(0), str1 + "\\Ttyping.ty");

            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.SeriesSpeed.Points.Clear();
            ArrayList a = ReadKeys("历史");
            foreach (string i in a)
            {
                double speed = double.Parse(IniRead("历史", i, "0"));
                this.SeriesSpeed.Points.AddXY(i, speed);
            }
        }
        #endregion

        #region 屏蔽输入
        private void maKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }
        #endregion


        #region 即时图表 1000ms
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (this.textBoxEx1.TextLength >= 10)
            {
                try
                {
                    this.SeriesSpeed.Points.AddY(Glob.chartSpeedTo);
                    if (Glob.chartSpeedTo > 0)
                    {
                        if (Glob.chartSpeedTo < Glob.MinSplite)
                        {
                            Glob.MinSplite = Glob.chartSpeedTo;
                            this.ChartArea1.AxisY.Minimum = (int)(Glob.MinSplite / 10) * 10;
                        }
                        this.ChartArea1.AxisX.Interval = this.SeriesSpeed.Points.Count / 5;
                    }
                }
                catch { }
            }
        }
        #endregion

        #region 点击显示
        private void SkipToType(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            char g = richTextBox1.GetCharFromPosition(p);
            int idex = richTextBox1.GetCharIndexFromPosition(p);
            if (e.Button == MouseButtons.Left)
                if (textBoxEx1.TextLength != 0)
                {
                    textBoxEx1.SelectionStart = idex;
                    textBoxEx1.SelectionLength = 1;
                    textBoxEx1.ScrollToCaret();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    MessageBox.Show(g.ToString());
                }
        }
        #endregion

        #region 功能菜单
        private void 设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.TopMost) { this.TopMost = false; 保持窗口最前ToolStripMenuItem1.Checked = false; }
            TSetup SetupA = new TSetup(this);
            SetupA.ShowDialog();
        }

        private void 新发文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TopMost) { this.TopMost = false; 保持窗口最前ToolStripMenuItem1.Checked = false; }
            if (NewSendText.发文状态)
            {
                switch (MessageBox.Show("正在发文中，请问你要做什么？\r\n选择是：停止当前发文，打开新发文\r\n选择否：打开当前发文状态", "发文询问", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        NewSendText.发文状态 = false;
                        if (发文状态窗口 != null)
                        {
                            if (!发文状态窗口.IsDisposed)
                                发文状态窗口.Close();
                        }
                        新发文 NewSendTextForm = new 新发文(this);
                        NewSendTextForm.ShowDialog();
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        if (发文状态窗口 != null)
                        {
                            if (发文状态窗口.IsDisposed)
                                发文状态窗口 = new SendTextStatic(this.Location, this);
                            if (!发文状态窗口.Visible)
                                发文状态窗口.Show(this);
                            this.Focus();
                        }
                        else
                        {
                            发文状态窗口 = new SendTextStatic(this.Location, this);
                            发文状态窗口.Show(this);
                            this.Focus();
                        }
                        break;
                }
            }
            else
            {
                新发文 NewSendTextForm = new 新发文(this);
                //NewSendText.MdiParent = this;
                NewSendTextForm.ShowDialog();
            }
        }

        private void 发送正在跟打的文段ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            if (lblDuan.Text != "段号")
            {
                if (text != "")
                {
                    string title = lblQuan.Text;
                    if (title != "所在群")
                    {
                        string textTitle = "";
                        if (lblTitle.Text != "标题") { textTitle = lblTitle.Text; }
                        string pre = "-----第" + Glob.Pre_Cout + "段";
                        if (Glob.Pre_Cout == "999")
                            pre += "-赛文验证:" + 添雨验证(添雨验证(text));
                        string texttotal = textTitle + "\r\n" + text + "\r\n" + pre + "-" + Glob.Instration.Trim() + "-分享发文";
                        Clipboard.Clear();
                        ClipboardHandler.SetTextToClipboard(texttotal);
                        SwitchToThisWindow(FindWindow(null, title), true); //激活窗口
                        Delay(Glob.DelaySend);
                        SendKeys.SendWait("^v");
                        Delay(20);
                        SendKeys.SendWait("%s");
                    }
                }
            }
            else
            {
                MessageBox.Show("当前无文段！");
            }
        }

        private void 关于添雨跟打器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        private void Form_Action(object sender, EventArgs e)
        {
            this.textBoxEx1.SelectionStart = this.textBoxEx1.TextLength;
        }

        private void 保持窗口最前ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!保持窗口最前ToolStripMenuItem1.Checked)
            {
                保持窗口最前ToolStripMenuItem1.Checked = true;
                this.TopMost = true;
            }
            else
            {
                保持窗口最前ToolStripMenuItem1.Checked = false;
                this.TopMost = false;
            }
        }

        private void 上一次成绩ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Glob.theLastGoal.Length != 0)
            {
                sendtext(Glob.theLastGoal + " *");
            }
        }

        private void 平均成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int table_c = this.dataGridView1.RowCount;
            StringBuilder sb = new StringBuilder();
            if (table_c > 1)
            {
                sb.Append("#平均 共" + (table_c - 1) + "段 速度" + this.dataGridView1.Rows[table_c - 1].Cells[3].Value);
                sb.Append(" 击键" + this.dataGridView1.Rows[table_c - 1].Cells[4].Value);
                sb.Append(" 码长" + this.dataGridView1.Rows[table_c - 1].Cells[5].Value);
                sb.Append(" 均时" + this.dataGridView1.Rows[table_c - 1].Cells[11].Value + "秒");
                sb.Append(" 今日已跟" + Glob.todayTyping + "字 总跟打" + Glob.TextLenAll + "字" + Glob.Instration);
                if (MessageBox.Show(sb + "\n是否发送至群？", "当前平均成绩", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    sendtext(sb.ToString());
                }
            }
        }

        private void 复制官方QQ群号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardHandler.SetTextToClipboard("129842316");
        }

        private void 帮助问答释疑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://taliove.diandian.com/TYWD");
        }
        #endregion

        #region 表格右键
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > 0 & e.RowIndex < dataGridView1.RowCount - 1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                    }

                    //只选一行
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    string index = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string duan = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    string text = "复制当前成绩";
                    if (index == "" && duan == "")
                    {
                    }
                    else
                    {
                        if (index == "")
                            text = "复制[第" + duan + "段]的[重打]成绩";
                        else
                            text = "复制[序" + index + "][第" + duan + "段]的成绩";

                        复制成绩ToolStripMenuItem.Text = text;
                        contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
        }

        private void 复制成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string goal = "";
            goal = "第" + this.dataGridView1.CurrentRow.Cells[2].Value + "段 ";
            for (int i = 3; i < this.dataGridView1.Columns.Count - 1; i++)
            {
                goal += this.dataGridView1.Columns[i].Name + this.dataGridView1.CurrentRow.Cells[i].Value + " ";
            }
            goal += " 校验:" + 添雨验证(goal);
            goal += Glob.Instration + " [复制成绩]";
            ClipboardHandler.SetTextToClipboard(goal);

        }
        #endregion

        #region 换群菜单

        private void refreshQun()
        {
            this.TSMI2.DropDownItems.Clear();
            //MessageBox.Show(((int)(Keys.D2)).ToString());
            this.TSMI2.DropDownItems.Add("刷新", null, new EventHandler(refreshQun2));
            try
            {
                Glob.GetWinC = 0;//初始化 获取窗口的数量
                Glob.GetWin = new string[10, 2];//数组 初始化
                CallBack myCallBack = new CallBack(EnumWindowsApp.Report);
                EnumWindows(myCallBack, 0);
                this.TSMI2.DropDownItems.Add("-");
                if (Glob.GetWinC > 0)
                {
                    ToolStripMenuItem[] ChangeQunList = new ToolStripMenuItem[Glob.GetWinC];
                    for (int i = 0; i < Glob.GetWinC; i++)
                    {
                        ChangeQunList[i] = new ToolStripMenuItem(Glob.GetWin[i, 0], null, new EventHandler(ChangeQun), Keys.Control | (Keys)(49 + i));
                    }
                    //MessageBox.Show(Glob.GetWinC.ToString());
                    this.TSMI2.DropDownItems.AddRange(ChangeQunList);
                }
                else
                {
                    this.TSMI2.DropDownItems.Add("未找到群");
                }
            }
            catch
            {
            }
        }

        private void ChangeQun(object sender, EventArgs e)
        {
            this.lblQuan.Text = sender.ToString();
        }

        private void refreshQun2(object sender, EventArgs e)
        {
            refreshQun();
            this.TSMI2.ShowDropDown();
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            if (lblQuan.Text.Contains("五林风"))
            {
                lblQuan.ForeColor = Color.FromArgb(244, 207, 0);
            }
            else if (lblQuan.Text.Contains("舞指"))
            {
                lblQuan.ForeColor = Color.FromArgb(72, 255, 0);
            }
            else
            {
                lblQuan.ForeColor = Color.White;
            }
        }
        #endregion



        #region 检查更新
        private void 检查更新情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://taliove.ys168.com");
        }

        private void 访问官方网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://taliove.diandian.com");
        }
        #endregion

        #region 编码查询

        private delegate void 测词委托();
        /// <summary>
        /// 菜单栏重绘用于显示理论码长 及 词组的编码提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mS1_Paint(object sender, PaintEventArgs e)
        {
            if (!Glob.isPointIt || !Glob.是否智能测词) return;
            if (Glob.词库理论码长 == 0) return;
            var g = e.Graphics;
            var str = Glob.词组编码 + "理论：" + Glob.词库理论码长.ToString("0.00");
            var siz = g.MeasureString(str, mS1.Font);
            g.DrawString(str, mS1.Font, new SolidBrush(Theme.ThemeColorFC), mS1.Width - siz.Width,
                         mS1.Height - siz.Height);
        }

        private bool 是否正在测词中 = false;
        private void 智能测词()
        {
            if (Glob.BmTips.Count == 0) return;
            if (Glob.TypeText.Length == 0) return;
            if (是否正在测词中) return;
            是否正在测词中 = true;
            var count = Glob.TypeText.Length;
            Glob.BmAlls.Clear();
            Glob.词库理论码长 = 0;
            var startTime = DateTime.Now;
            const string bd =
                @"，。“”！（）()~·#￥%&*_[]{}‘’/\<>,.《》？：；、—…1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ";
            const string bd2 = @"“”（）！《》？";
            BeginInvoke(new MethodInvoker(() =>
                {
                    _render.ClearLabel();
                    picDoing.BackColor = richTextBox1.BackColor;
                    toolTip1.SetToolTip(picDoing, "正在智能测词中...");
                    picDoing.Location = new Point(Width - picDoing.Width - 22, splitContainer1.Location.Y + splitContainer1.SplitterDistance - picDoing.Height);
                    picDoing.Visible = true;
                }));
            var counts = 0;
            for (var i = 0; i < count; i++)
            {
                var 起点字符 = Glob.TypeText[i].ToString();
                if (bd.Contains(起点字符))
                {
                    //标点
                    counts++;
                    Glob.BmAlls.Add(new BmAll { 查询的字 = 起点字符, 编码 = 起点字符, 重数 = 0, 起点 = i, 终点 = i + 1 });
                    continue;
                }
                var end = i + 1;
                var temp_search = 0;
                for (int j = i + 1; j < count; j++)
                {
                    if (temp_search >= 10) break;
                    var temp = Glob.TypeText[j].ToString();
                    if (bd.Contains(temp))
                    {
                        end = j;
                        break;
                    }
                    temp_search++;
                }
                end += temp_search;
                if (end >= count) end = count - 1;
                for (int j = end; j > i; j--)
                {
                    var str = Glob.TypeText.Substring(i, j - i);
                    var find = Glob.BmTips.FindAll(o => o.Contains(str));
                    var min = find.FindIndex(k => k[0].Length == find.Min(o => o[0].Length));
                    if (min == -1)
                    {
                        if (str.Length == 1)
                        {
                            if (bd.Contains(str))
                            {
                                counts++;
                                Glob.BmAlls.Add(new BmAll { 查询的字 = str, 编码 = str, 重数 = 0, 起点 = i, 终点 = j - 1 });
                                break;
                            }
                        }
                    }
                    else
                    {
                        counts++;
                        Glob.BmAlls.Add(new BmAll { 查询的字 = str, 编码 = find[min][0].Trim(), 重数 = find[min].FindIndex(o => o.Contains(str)), 起点 = i, 终点 = j - 1 });
                        i = j - 1;
                        break;
                    }
                }
            }

            //for (var i = 0; i < count; i++)
            //{
            //    var v = i + 1;
            //    if (i < count - 1)
            //    {
            //        for (var j = i + 1; j < count; j++)
            //        {
            //            var str = Glob.TypeText.Substring(i, j - i);
            //            var find = Glob.BmTips.FindAll(o => o.Contains(str));
            //            var min = find.FindIndex(k => k[0].Length == find.Min(o => o[0].Length));
            //            if (min == -1)
            //            {
            //                if (bd.Contains(str))
            //                {
            //                    if (str.Length == 1)
            //                    {
            //                        bmTemp = str;
            //                        bm重数 = 0;
            //                    }
            //                }
            //                break;
            //            }
            //            v = j;
            //            bmTemp = find[min][0];
            //            bm重数 = find[min].FindIndex(o => o == str);
            //        }
            //    }
            //    var temp = Glob.TypeText.Substring(i, v - i);
            //    Glob.BmAlls.Add(new BmAll { 查询的字 = temp, 编码 = bmTemp, 重数 = bm重数 });
            //    if (v - 1 < count) i = v - 1;
            //}

            if (Glob.BmAlls.Count == 0)
            {
                已测 = "";
                this.UIThread(() =>
                    {
                        ShowFlowText("没有找到词组~~");
                        picDoing.Visible = false;
                        _render.ClearLabel();
                    });
                是否正在测词中 = false;
                return;
            }
            try
            {
                var total = Glob.BmAlls.Sum(o => o.查询的字.Length);
                if (total < Glob.TypeText.Length)
                {
                    var temp = Glob.TypeText.Substring(total, Glob.TypeText.Length - total);
                    Glob.BmAlls.Add(new BmAll { 查询的字 = temp, 编码 = temp, 重数 = 0 });
                }
            }
            catch { }
            _wordInfoUtil = new WordInfoUtil();
            _wordInfoUtil.SetCiKu();
            var wordInfos = _wordInfoUtil.GetWordInfos(Glob.TypeText);
            _render.Init(wordInfos, richTextBox1, Glob.Right);
            BeginInvoke(new MethodInvoker(() =>
                {
                    // ShowFlowText(string.Format("counts{0}\nGlob.BmAlls:{1}", counts, Glob.BmAlls.Count));
                    //计算理论码长 
                    var strLen = ""; var tempMc = 0;
                    for (int index = 1; index < Glob.BmAlls.Count; index++)
                    {
                        var bmAll = Glob.BmAlls[index];
                        var v = "";
                        if (bmAll.重数 == 0) if (bd2.Contains(bmAll.查询的字)) v = "  ";
                        if (bmAll.重数 == 1) if (bmAll.编码.Length < 4) v = " ";
                        if (bmAll.重数 == 2) v = ";";
                        if (bmAll.重数 == 3) v = "'";
                        if (bmAll.重数 >= 4) v = bmAll.重数.ToString();
                        strLen += bmAll.编码 + v;
                    }
                    Glob.词库理论码长 = (strLen.Length * 1.00 / (Glob.TypeText.Length - Glob.BmAlls[0].查询的字.Length));
                    _render.Render();
                    ShowFlowText(string.Format("第{0}段，计算码长为：{1}，用时：{2}", Glob.Pre_Cout, Glob.词库理论码长.ToString("0.00"), (DateTime.Now - startTime).TotalSeconds.ToString("0.00")));
                    picDoing.Visible = false;
                    已测 = this.lblMatchCount.Text;
                    mS1.Invalidate();
                    是否正在测词中 = false;
                }));
        }

        private string 已测 = "";
        private 测词委托 开始测词委托;
        private void 委托测词()
        {
            if (开始测词委托 == null)
                开始测词委托 = new 测词委托(智能测词);
            开始测词委托.BeginInvoke(null, null);
        }
        private void 智能测词ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ini = new _Ini("Ttyping.ty");
            if (Glob.是否智能测词)
            {
                Glob.是否智能测词 = false;
                this.智能测词ToolStripMenuItem.Checked = false;
                ini.IniWriteValue("程序控制", "智能测词", "False");
                InitCiKu();
                TickIt();
            }
            else
            {
                委托测词();
                this.智能测词ToolStripMenuItem.Checked = true;
                ini.IniWriteValue("程序控制", "智能测词", "True");
            }
        }

        public delegate void BianMaCheck(string param, int flag);

        public void BimaCheck(string word, int flag)
        {
            var bm = word;
            var findit = 0;
            const string bd =
                @"，。“”！（）()~·#￥%&*_[]{}‘’/\<>,.《》？：；、—…1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ";
            if (!bd.Contains(word))
            {
                var find = Glob.BmTips.FindAll(o => o.Contains(word));
                var min = find.FindIndex(k => k[0].Length == find.Min(o => o[0].Length));
                if (min != -1)
                {
                    findit = find[min].FindIndex(o => o == word);
                    bm = find[min][0];
                }
                else
                {
                    findit = 0;
                    bm = "";
                }
            }
            if (flag != 1)
            {
                BeginInvoke(new MethodInvoker(() => ShowBmTips(word, bm, findit)));
            }
            else
            {
                var s = "";
                if (findit != 0)
                {
                    s = string.Format("【{0}】 · 【{1}】 · 【{2}重】", word, bm, findit);
                }
                if (findit == 0)
                {
                    s = string.Format("【{0}】 的编码未找到。", word);
                }
                if (s.Length != 0)
                {
                    BeginInvoke(new MethodInvoker(() => ShowFlowText(s)));
                }
            }
        }

        private void ShowBmTips(string word, string s, int flag)
        {
            lblBmTips.Text = s;
            PicSetBmTips(word, s, flag);
        }
        /// <summary>
        /// 设置编码提示的显示
        /// </summary>
        /// <param name="zi">字</param>
        /// <param name="bm">编码</param>
        /// <param name="flag">选重</param>
        private void PicSetBmTips(string zi, string bm, int flag)
        {
            this.UIThread(() =>
                {
                    var bmp = new Bitmap(lblBmTips.Width, lblBmTips.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        int splitLineWidth = (bmp.Width - bmp.Height) * 2 / 5; //字起点
                        int splitLineWidth2 = (bmp.Width - splitLineWidth - bmp.Height) * 3 / 5;
                        var solidBrush = new SolidBrush(Color.FromArgb(99, 91, 91));
                        var ziFont = new Font("宋体", 9f);
                        var BasePen = new Pen(Theme.ThemeBG);
                        g.DrawLine(BasePen, bmp.Height + 3, 0, bmp.Height + 3, bmp.Height);
                        g.DrawLine(BasePen, splitLineWidth + bmp.Height, 0, splitLineWidth + bmp.Height, bmp.Height);
                        //画重
                        int radius = bmp.Height - 2;
                        g.FillPie(new SolidBrush(bmCong(flag)), 2, 1, radius, radius, -360, 360);
                        g.FillRectangle(new SolidBrush(bmCong(flag)), 1, 1, bmp.Height + 1, bmp.Height - 2);
                        //画字
                        SizeF ziSizeF = g.MeasureString(zi, ziFont);
                        g.DrawString(zi, ziFont, solidBrush, splitLineWidth / 2 - ziSizeF.Width / 2 + bmp.Height + 2,
                                     bmp.Height / 2 - ziSizeF.Height / 2 + 1);
                        //画编码
                        var bmFont = new Font("宋体", 9f);
                        SizeF bmSizeF = g.MeasureString(bm, bmFont);
                        if (flag != 0)
                        {
                            solidBrush = new SolidBrush(Color.DarkBlue);
                        }
                        g.DrawString(bm, bmFont, solidBrush,
                                     splitLineWidth2 / 2 - bmSizeF.Width / 2 + bmp.Height + splitLineWidth + 3,
                                     bmp.Height / 2 - bmSizeF.Height / 2 + 1);
                    }
                    lblBmTips.Image = bmp;

                    if (Glob.BmAlls.Count != 0)
                    {
                        var total = 0;
                        var count = Glob.BmAlls.Count;
                        var str = "";
                        for (int index = 0; index < count; index++)
                        {
                            var bmAll = Glob.BmAlls[index];
                            var now = this.textBoxEx1.TextLength;
                            if (now == bmAll.起点)
                            {
                                //显示当前
                                str = string.Format("【{0}】 {1} {2}重 ", Glob.BmAlls[index].查询的字, Glob.BmAlls[index].编码, Glob.BmAlls[index].重数);
                                break;
                            }

                            if (now == bmAll.终点)
                            {
                                //显示下一个
                                var ind = (index + 1 >= count) ? count - 1 : index + 1;
                                str = string.Format("【{0}】 {1} {2}重 ", Glob.BmAlls[ind].查询的字, Glob.BmAlls[ind].编码, Glob.BmAlls[ind].重数);
                                break;
                            }
                        }
                        Glob.词组编码 = str;
                        mS1.Invalidate();
                    }
                });
        }
        private Color bmCong(int flag)
        {
            return Glob.BmColors[flag > 4 ? 3 : flag == 0 ? 0 : flag - 1];
        }
        private void 查询当前编码ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CheckBmFile();
            var bianMa = new BianMaCheck(BimaCheck);
            var s =
                Glob.TypeText[Glob.TypeTextCount == Glob.TextLen ? Glob.TypeTextCount - 1 : Glob.TypeTextCount].ToString();
            bianMa.BeginInvoke(s, 0, null, null);
        }
        /// <summary>
        /// 检查编码文件
        /// </summary>
        private void CheckBmFile()
        {
            if (Glob.BmTips.Count != 0) return;
            //读取编码
            using (bmTips = new FormBMTipsModel())
            {
                if (bmTips.ReadState != State.Done)
                {
                    ShowFlowText("编码引擎异常，请检查文件！或按CTRL+F重载尝试！");
                    this.picBmTips.Enabled = false;
                    return;
                }
                Glob.BmTips = bmTips.Dic.ToList();
                toolTip1.SetToolTip(lblBmTips, "当前找到" + Glob.BmTips.Count + "个编码。\n显示格式：\n重数|查询的字|编码");
                this.picBmTips.Checked = true;
            }

            //智能测词
            var ini = new _Ini("Ttyping.ty");
            var b = false;
            bool.TryParse(ini.IniReadValue("程序控制", "智能测词", "False"), out b);
            Glob.是否智能测词 = b;
            智能测词ToolStripMenuItem.Checked = b;
        }
        private void picBmTips_Click(object sender, EventArgs e)
        {
            var ini = new _Ini("Ttyping.ty");
            if (this.picBmTips.Checked)
            {
                //取消
                this.picBmTips.Checked = false;
                ini.IniWriteValue("程序控制", "编码", "False");

            }
            else
            {
                //开启
                CheckBmFile();
                this.picBmTips.Checked = true;
                ini.IniWriteValue("程序控制", "编码", "True");
            }
        }

        /// <summary>
        /// 查询选中文字的编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFindSelectionBm_Click(object sender, EventArgs e)
        {
            CheckBmFile();
            var bianMa = new BianMaCheck(BimaCheck);
            var s = this.richTextBox1.SelectedText;
            bianMa.BeginInvoke(s, 1, null, null);
        }
        #endregion

        #region 尾边按钮
        private void toolStripStatusLabel3_Click_1(object sender, EventArgs e)
        {
            F4();
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            F3();
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {
            F5();
        }
        #endregion

        #region 按钮 快捷键
        delegate void testt(string s);
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //右
            if (e.KeyCode == Keys.Right && e.Control)
            {
                if (this.cmsDuanList.Items.Count <= 0) return;
                int index = this.cmsDuanList.Items.IndexOfKey(Glob.Pre_Cout);
                index++;
                if (index >= this.cmsDuanList.Items.Count)
                    index = 0;
                this.cmsDuanList.Items[index].PerformClick();
            }
            else if (e.KeyCode == Keys.Left && e.Control)
            {//左
                if (this.cmsDuanList.Items.Count <= 0) return;
                int index = this.cmsDuanList.Items.IndexOfKey(Glob.Pre_Cout);
                index--;
                if (index < 0)
                    index = this.cmsDuanList.Items.Count - 1;
                this.cmsDuanList.Items[index].PerformClick();
            }
            else if (e.KeyCode == Keys.Q && e.Control)
            {
                this.将目前文章乱序ToolStripMenuItem.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                F5();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                int c = this.TSMI2.DropDownItems.Count;
                if (c > 2)
                {
                    if (c == 3)
                    {
                        if (this.TSMI2.DropDownItems[2].Text == "未找到群")
                        {
                            F5();
                        }
                    }
                    for (int i = 2; i < c; i++)
                    {
                        string getN = this.TSMI2.DropDownItems[i].Text;
                        if (getN == this.lblQuan.Text)
                        {
                            i++;
                            if (i >= c) { i = 2; }
                            this.TSMI2.DropDownItems[i].PerformClick();
                            break;
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //this.textBoxEx1.Focus();
                //testt t = new testt(Test);
                //t.BeginInvoke(this.richTextBox1.Text,null,null);
                //double d = double.Parse(this.labelJiCheck.Text);
                //d--;
                //this.labelJiCheck.Text = d.ToString();
                //int iMode = 0;
                //int iSentence = 0;
                //IntPtr prt = ImmGetContext(this.textBoxEx1.Handle);
                //ImmGetConversionStatus(prt,ref iMode,ref iSentence);
                //SetIMM(1026, 0);
                //this.label1.Text = iMode + "/" + iSentence;
                //活动信息显示
            }
            else if (e.KeyCode == Keys.End)
            {
            }
            else if (e.KeyCode == Keys.F1)
            {
                //string s = this.SeriesSpeed.Points.
                if (sw != 0) return;
                this.设置ToolStripMenuItem1.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (sw != 0) return;
                this.新发文ToolStripMenuItem.PerformClick();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {

            }
            //MessageBox.Show(e.KeyCode.ToString());
        }

        /// <summary>
        /// 显示浮动的信息
        /// </summary>
        /// <param name="text">需要显示的信息</param>
        public void ShowFlowText(string text)
        {
            var sm = new ShowMessage(this.Size, this.Location, this);
            sm.Show(text);
        }
        private static void Test(string s)
        {
            var str = "";
            foreach (var bmAll in Glob.BmAlls)
            {
                var v = "";
                if (bmAll.重数 == 0) v = "";
                if (bmAll.重数 == 1) if (bmAll.编码.Length < 4) v = " ";
                if (bmAll.重数 == 2) v = ";";
                if (bmAll.重数 == 3) v = "'";
                if (bmAll.重数 >= 4) v = bmAll.重数.ToString();
                str += bmAll.编码 + v;
            }
            //MessageBox.Show(str);
            for (int i = 0; i < str.Length; i++)
            {
                SendKeys.SendWait(str[i].ToString());
                Delay(70);
            }
        }

        private void 击键统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.jjAllC >= 100)
            {
                JjCheck JjC = new JjCheck(this);
                JjC.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("击键记录段数小于100项，请多多参打哟。项目越多计算越准！", "提示");
            }
        }

        //击键评定 显示
        private void JjCheck(int jP)
        {
            if (Glob.jjAllC >= 100)
            {
                double jjP;
                int jjC = 0;
                double jjC_ = 0;
                for (int i = 0; i < 9; i++)
                {
                    jjP = (double)Glob.jjPer[i] / Glob.jjAllC;
                    if (jjP >= 0.1 && jjC == 0)
                        jjC = (i + 4) > 12 ? 12 : i + 4;

                    if (jjC != 0)
                    {
                        if ((i + 4) >= jjC)
                            jjC_ += jjP;
                    }
                }
                if (jjC != 0)
                {
                    string ud;
                    if (jjC < jP) ud = "↑";
                    else if (jjC > jP) ud = "↓";
                    else ud = "-";
                    this.labelCheckUD.Text = ud;
                    this.labelJiCheck.Text = (jjC + jjC_).ToString("0.000");
                }
                else
                {
                    this.labelJiCheck.Text = "-";
                    this.labelCheckUD.Text = "";
                }
            }
            else
            {
                this.labelJiCheck.Text = "-";
                this.labelCheckUD.Text = "";
            }
            toolTip1.SetToolTip(this.labelJiCheck, "击键等级评定\n已跟打" + Glob.jjAllC + "段");
        }

        private void labelJiCheck_TextChanged(object sender, EventArgs e)
        {
            /* double jj = double.TryParse((sender as Label).Text, out jj) ? jj : 0;
             if (jj >= 4.80 && jj <= 5.80) {
                 this.labelJiCheck.ForeColor = Color.White;
             }
             else if (jj > 5.80 && jj <= 6.80) {
                 this.labelJiCheck.ForeColor = Color.GreenYellow;
             }
             else if (jj > 6.80 && jj <= 7.80) {
                 this.labelJiCheck.ForeColor = Color.DarkBlue;
             }
             else if (jj > 7.80 && jj <= 8.80)
             {
                 this.labelJiCheck.ForeColor = Color.HotPink;
             }
             else if (jj > 8.80)
             {
                 this.labelJiCheck.ForeColor = Color.Red;
             }
             else {
                 this.labelJiCheck.ForeColor = Color.Gray;
             }*/
        }
        #endregion

        #region 发文菜单部分
        private void 发下一段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendAOnce();
            F3();
        }

        private void 停止发文ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //关闭发文
            if (NewSendText.发文状态)
            {
                if (NewSendText.是否周期)
                {
                    timerTSend.Stop();
                }
                NewSendText.发文状态 = false;
                //if (!NewSendText.是否独练)
                //  sendtext("停止发文，感谢跟打~~（" + Glob.Instration.Trim() + "）");
                NewSendText.已发段数 = 0;
                NewSendText.已发字数 = 0;
                NewSendText.标记 = 0;
            }
        }
        #endregion

        #region 载文途径
        private void 从QQ窗口手动ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PutText();
        }

        //粘贴测速
        private void 从剪切板ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string get = Clipboard.GetText().Trim();//获取剪贴板内的文字
            if (get != "")
            {
                Glob.Pre_Cout = Glob.AZpre.ToString();
                this.lblDuan.Text = "第" + Glob.Pre_Cout + "段";
                Glob.AZpre++;
                Initialize(1);
                Glob.reTypeCount = 0; //重打
                this.textBoxEx1.TextChanged -= new System.EventHandler(textBoxEx1_TextChanged);
                textBoxEx1.Clear();
                this.textBoxEx1.TextChanged += new System.EventHandler(textBoxEx1_TextChanged); //重新绑定
                F3();
                this.richTextBox1.Text = get;
                GetInfo();
                timer1.Stop();
                timer3.Stop();

            }
        }
        #endregion

        #region 关闭详细信息
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            bool temp = (sender as ToolStripButton).Checked;
            if (temp)
            {
                this.toolStripButton4.Checked = false;
            }
            else
            {
                this.toolStripButton4.Checked = true;
            }
        }
        private void toolStripButton4_CheckedChanged(object sender, EventArgs e)
        {
            bool temp = (sender as ToolStripButton).Checked;
            CloseDetail(temp);
            _Ini inisetup = new _Ini("Ttyping.ty");
            inisetup.IniWriteValue("程序控制", "详细信息", temp.ToString());
        }

        private void CloseDetail(bool temp)
        {
            switch (temp)
            {
                case false:
                    Glob.p1 = this.splitContainer1.SplitterDistance;
                    Glob.p2 = this.splitContainer3.SplitterDistance;

                    this.splitContainer3.Panel2Collapsed = true;
                    //this.toolStripButton4.Checked = false;
                    this.splitContainer1.SplitterDistance = Glob.p1 + Glob.p2;
                    this.splitContainer3.SplitterDistance = 100;
                    break;
                case true:
                    this.splitContainer3.Panel2Collapsed = false;
                    //this.toolStripButton4.Checked = true;
                    this.splitContainer1.SplitterDistance = Glob.p1;
                    this.splitContainer3.SplitterDistance = 100;
                    break;
            }
        }
        #endregion

        #region 文章处理
        public int[] GetRandomUnrepeatArray(int minValue, int maxValue, int count)
        {
            Random rnd = new Random();
            int length = maxValue - minValue + 1;
            byte[] keys = new byte[length];
            rnd.NextBytes(keys);
            int[] items = new int[length];
            for (int i = 0; i < length; i++)
            {
                items[i] = i + minValue;
            }
            Array.Sort(keys, items);
            int[] result = new int[count];
            Array.Copy(items, result, count);
            return result;
        }

        private void 将目前文章乱序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F3();
            int textlen = richTextBox1.TextLength;
            string Text1 = "";
            if (textlen > 9)
            {
                int[] numlist = GetRandomUnrepeatArray(0, textlen - 1, textlen);
                foreach (int item in numlist)
                {
                    Text1 += richTextBox1.Text.Substring(item, 1);
                }
                richTextBox1.Text = Text1;
                GetInfo();
            }
            else
            {
                MessageBox.Show("字数过少！");
            }
        }

        public string reText(string text)
        {
            Regex english = new Regex(@"[a-zA-Z]");
            if (!english.IsMatch(text))
            {
                string[] Ebiaodian = new string[] { "\"", "\"", "'", "'", ".", ",", ";", ":", "?", "!", "-", "~", "(", ")", "<", ">", @"\(", @"\)" };
                string[] Cbiaodian = new string[] { "“", "”", "‘", "’", "。", "，", "；", "：", "？", "！", "—", "～", "（", "）", "《", "》", "（", "）" };
                for (int i = 0; i < Ebiaodian.Length; i++)
                {
                    text = text.Replace(Ebiaodian[i], Cbiaodian[i]);
                }
            }
            return text;
        }

        private void 英文标点换中文标点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Glob.autoReplaceBiaodian) //没有开自动的情况下
            {
                richTextBox1.Text = reText(richTextBox1.Text);
                GetInfo();
            }
        }

        private void 删除空格包含全角ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = richTextBox1.Text;
            s = s.Replace(" ", "");
            s = s.Replace("　", "");
            s = s.Replace("\r\n", "");
            s = s.Replace("\r", "");
            s = s.Replace("\n", "");
            richTextBox1.Text = s;
            GetInfo();
        }


        private void 打开练习ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string filename = this.openFileDialog1.FileName;
                StreamReader openfile = new StreamReader(filename, UnicodeEncoding.Default);//读取
                FileInfo fileinfo = new FileInfo(filename); //获取文件信息
                string opentxt = openfile.ReadToEnd();//读取整个文件
                if (opentxt.Length != 0)
                {
                    this.richTextBox1.Text = opentxt;
                    GetInfo();
                }
                else
                {
                    MessageBox.Show("文件空！", "添雨跟打器载文提示");
                }
            }
        }
        #endregion

        #region 下方工具条
        //极简
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            if (Glob.simpleMoudle)
            {
                Glob.simpleMoudle = false;
                this.toolStripButton2.Checked = false;
                ini.IniWriteValue("发送", "状态", "False");
            }
            else
            {
                Glob.simpleMoudle = true;
                this.toolStripButton2.Checked = true;
                ini.IniWriteValue("发送", "状态", "True");
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            if (Glob.autoReplaceBiaodian)
            {
                Glob.autoReplaceBiaodian = false;
                this.toolStripButton1.Checked = false;
                ini.IniWriteValue("程序控制", "自动替换", "False");
            }
            else
            {
                Glob.autoReplaceBiaodian = true;

                this.toolStripButton1.Checked = true;
                ini.IniWriteValue("程序控制", "自动替换", "True");
            }
            //MessageBox.Show(Glob.autoReplaceBiaodian + "\n" + this.toolStripButton1.Checked);
        }

        //潜水
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            if (Glob.isSub)
            {
                Glob.isSub = false;
                this.toolStripButton3.Checked = false;
                ini.IniWriteValue("发送", "潜水", "False");
            }
            else
            {
                Glob.isSub = true;
                this.toolStripButton3.Checked = true;
                ini.IniWriteValue("发送", "潜水", "true");
            }
        }

        private void PicSend_Click(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            bool b = (sender as ToolButton).Checked;
            if (b)
            {
                this.PicSend.Checked = false;
                ini.IniWriteValue("发送", "图片", "False");
            }
            else
            {
                this.PicSend.Checked = true;
                ini.IniWriteValue("发送", "图片", "true");
            }
        }
        #endregion

        #region 老板键及双击全显
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Show();
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                this.Activate();
                RegisterHotKey(this.Handle, 2, (int)KeyModifiers.None, (Keys.F4)); //获取
                RegisterHotKey(this.Handle, 3, (int)KeyModifiers.None, (Keys.F3)); //重打
                RegisterHotKey(this.Handle, 4, (int)KeyModifiers.None, (Keys.F6)); //发文测试
            }
        }

        private void 老板键ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterHotKey(this.Handle, 3, (int)KeyModifiers.None, (Keys.F3)); //重打
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
            RegisterHotKey(this.Handle, 2, (int)KeyModifiers.None, (Keys.F4)); //获取
            RegisterHotKey(this.Handle, 3, (int)KeyModifiers.None, (Keys.F3)); //重打
            RegisterHotKey(this.Handle, 4, (int)KeyModifiers.None, (Keys.F6)); //发文测试
        }

        private bool isShowAll = false;
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.splitContainer3.Panel1Collapsed)
            {
                isShowAll = false;
                if (!Glob.isShowSpline)
                    this.splitContainer4.Panel1Collapsed = false;
                this.splitContainer1.Panel1Collapsed = false;
                this.splitContainer3.Panel1Collapsed = false;
                if (Glob.HaveTypeCount > 0)
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }
            else
            {
                isShowAll = true;
                this.splitContainer4.Panel1Collapsed = true;
                this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer3.Panel1Collapsed = true;
            }
        }

        private void chartSpeed_DoubleClick(object sender, EventArgs e)
        {
            if (this.splitContainer4.Panel2Collapsed)
            {
                isShowAll = false;
                this.splitContainer4.Panel2Collapsed = false;
                this.splitContainer1.Panel1Collapsed = false;
                this.splitContainer3.Panel1Collapsed = false;
            }
            else
            {
                isShowAll = true;
                this.splitContainer4.Panel2Collapsed = true;
                this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer3.Panel1Collapsed = true;
            }
        }

        #endregion

        #region 检验真伪
        public string checkTF(string goal)
        {
            string jg;
            Regex getit = new Regex(@".+(?= 校验)");
            string all = getit.Match(goal).ToString();
            Regex getID = new Regex(@"(?<= 校验:)\d+");
            string getid = getID.Match(goal).ToString();
            //计算ID
            string nowgetid = 添雨验证(all);
            if (getid.Length != 0)
            {
                if (nowgetid == getid)
                {
                    jg = "真";
                }
                else
                {
                    jg = "假";
                }
            }
            else
            {
                jg = "缺少条件";
            }
            return jg;
        }

        private void 检验真伪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string getC = Clipboard.GetText();
                if (getC.Length != 0 && getC.Length <= 300)
                {
                    string g = checkTF(getC);
                    MessageBox.Show(this, "成绩数据：" + getC + "\n" + "检验结果：" + g, "添雨跟打器成绩检验结果");
                }
                else
                {
                    MessageBox.Show(this, "请检查是否为成绩？", "添雨跟打器检验真伪提示");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + "\n" + "说明：此错误并非由跟打器本身所导致！");
            }
        }
        #endregion

        #region 字符寻找
        //错字单击
        private void labelBM_MouseClick(object sender, MouseEventArgs e)
        {
            if (Glob.FWords.Count != 0)
            {
                this.richTextBox1.SelectionStart = (int)Glob.FWords[Glob.FWordsSkip];
                this.richTextBox1.SelectionLength = 1;
                this.richTextBox1.ScrollToCaret();
                this.lbl错字显示.Text = this.richTextBox1.SelectedText;
                if (e.Button == System.Windows.Forms.MouseButtons.Left) //左键向下
                {
                    Glob.FWordsSkip++;
                    if (Glob.FWordsSkip >= Glob.FWords.Count) Glob.FWordsSkip = 0;
                } //右键向上
                else
                {
                    Glob.FWordsSkip--;
                    if (Glob.FWordsSkip < 0) Glob.FWordsSkip = (int)Glob.FWords.Count - 1;
                }
            }
        }

        //回改单击
        private void labelhgstatus_MouseClick(object sender, MouseEventArgs e)
        {
            if (Glob.TextHgPlace.Count > 0)
            {
                int now = (int)Glob.TextHgPlace[Glob.TextHgPlace_Skip];
                this.richTextBox1.SelectionStart = now;
                this.richTextBox1.SelectionLength = 1;
                this.richTextBox1.ScrollToCaret();
                this.lbl回改显示.Text = this.richTextBox1.SelectedText;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Glob.TextHgPlace_Skip++;
                    if (Glob.TextHgPlace_Skip >= Glob.TextHgPlace.Count) Glob.TextHgPlace_Skip = 0;
                }
                else
                {
                    Glob.TextHgPlace_Skip--;
                    if (Glob.TextHgPlace_Skip < 0) Glob.TextHgPlace_Skip = (int)Glob.TextHgPlace.Count - 1;
                }
            }
        }
        #endregion

        #region 恢复默认显示
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int getW = this.dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.None) + 8;
            if (getW < 443) getW = 443;
            this.Size = new Size(getW, 443);

            this.splitContainer1.Panel2Collapsed = false;
            this.splitContainer1.SplitterDistance = 145;

            this.splitContainer3.Panel2Collapsed = false;
            this.splitContainer3.SplitterDistance = 80;

            this.splitContainer4.Panel2Collapsed = false;
            this.splitContainer4.SplitterDistance = 206;

            if (!Glob.isShowSpline)
                this.splitContainer4.Panel1Collapsed = false;
            this.splitContainer1.Panel1Collapsed = false;
            this.splitContainer3.Panel1Collapsed = false;

            this.textBoxEx1.Focus();
        }
        #endregion

        #region 闪烁
        //速度限制的闪烁
        private static bool BtnFlash = true;
        private static int BtnFlashCount = 0;
        private void timerBtnFlash_Tick(object sender, EventArgs e)
        {
            //tSBtnTableMoudle.Text = BtnFlashCount.ToString();
            if (BtnFlashCount > 5) { timerBtnFlash.Stop(); BtnFlash = true; BtnFlashCount = 0; this.toolStripBtnLS.ForeColor = Color.White; }
            if (BtnFlash)
            {
                this.toolStripBtnLS.Enabled = true;
                BtnFlash = false;
            }
            else
            {
                this.toolStripBtnLS.Enabled = false;
                BtnFlash = true;
            }
            BtnFlashCount++;
        }
        //潜水的闪烁
        private static bool BtnSubFlash = true;
        private static int BtnSubFlashCount = 0;
        private void timerSubFlash_Tick(object sender, EventArgs e)
        {
            if (BtnSubFlashCount > 5) { timerSubFlash.Stop(); BtnSubFlash = true; BtnSubFlashCount = 0; this.toolStripButton3.ForeColor = Color.White; }
            if (BtnSubFlash)
            {
                this.toolStripButton3.Enabled = true;
                BtnSubFlash = false;
            }
            else
            {
                this.toolStripButton3.Enabled = false;
                BtnSubFlash = true;
            }
            BtnSubFlashCount++;
        }
        #endregion

        #region 速度限制
        private void toolStripBtnLS_Click(object sender, EventArgs e)
        {
            if (Glob.是否速度限制)
            {
                _Ini Setupini = new _Ini("Ttyping.ty");
                this.toolStripBtnLS.Checked = false;
                Glob.是否速度限制 = false;
                Setupini.IniWriteValue("发送", "是否速度限制", "False");
            }
            else
            {
                _Ini Setupini = new _Ini("Ttyping.ty");
                this.toolStripBtnLS.Checked = true;
                Glob.是否速度限制 = true;
                Setupini.IniWriteValue("发送", "是否速度限制", "True");
            }
        }
        #endregion

        #region 精五比赛
        private void 精五比赛成绩生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.theLastGoal.Length != 0)
            {
                精五成绩生成 jw = new 精五成绩生成(this, Glob.theLastGoal, Glob.typeUseTime.ToString(), Glob.QQnumber);
                jw.ShowDialog();
            }
            else
            {
                //MessageBox.Show("请在跟打后，打开此项！","精五成绩转换提示");
                ShowFlowText("请在跟打后，打开此项！");
            }
        }
        #endregion

        #region 打开测速
        private void InTestSpeed(string text)
        {
            string getText = "";
            try
            {
                getText = TyDll.GetResources.GetText("Resources.TXT." + text + ".txt");
            }
            catch
            {
            }
            if (getText.Length != 0)
            {
                this.richTextBox1.Text = getText;
                GetInfo();
            }
        }
        private void 前五百单字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InTestSpeed((sender as ToolStripMenuItem).Text);
        }

        private void 中五百单字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InTestSpeed((sender as ToolStripMenuItem).Text);
        }

        private void 后五百单字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InTestSpeed((sender as ToolStripMenuItem).Text);
        }

        private void 岳阳楼记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InTestSpeed((sender as ToolStripMenuItem).Text);
        }

        private void 为人民服务节选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InTestSpeed((sender as ToolStripMenuItem).Text);
        }
        #endregion

        #region 表格处理
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor; //选中的时候，单元格颜色不变
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor; //选中的时候，单元格颜色不变
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region 测速点
        private void 比赛时自动打开寻找测速点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            ToolStripMenuItem ts = (sender as ToolStripMenuItem);
            if (ts.Checked)
            {
                ts.Checked = false;
                ini.IniWriteValue("程序控制", "自动打开寻找", "False");
            }
            else
            {
                ts.Checked = true;
                ini.IniWriteValue("程序控制", "自动打开寻找", "true");
            }
        }

        private void 添加测速点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sw != 0) { ShowFlowText("请勿在跟打时建立测速点！"); return; }
            if (Glob.TextLen > 20) { ShowFlowText("文章字数过少，不建议使用此功能！"); return; }
            int start = this.richTextBox1.SelectionStart - 1;
            int count = Glob.SpeedPointCount;//SpeedPoint.Count;
            if (count == 0)
            {
                if (start > 10 && start < Glob.TextLen - 10)
                {
                    setSpeedPoint(start);
                }
                else
                {
                    MessageBox.Show("请先鼠标左键点击确认光标位置，再选择添加此处为测速点！" +
                        "\n并且不能位于文章末尾2字符内\n（注：测速点不要离开始点及结束点太近！）", "测速提示");
                }
            }
            else
            {
                int len = start - Glob.SpeedPoint_[Glob.SpeedPointCount - 1];
                if (len > 0)
                {
                    if (len <= 10)
                    {
                        MessageBox.Show("相邻两测速点距离太近，请重新选择！", "测速提示");
                    }
                    else
                    {
                        setSpeedPoint(start);
                    }
                }
                else
                {
                    ShowFlowText("请按顺序安排测速点！");
                }
            }
        }

        public void setSpeedPoint(int start)
        {
            if (Glob.SpeedPointCount > 9)
            {
                MessageBox.Show("最多只能设置10个测速点！");
                return;
            }
            else
            {
                this.richTextBox1.SelectionStart = start;
                this.richTextBox1.SelectionLength = 1;
                this.richTextBox1.SelectionBackColor = Color.LightGray;
                Glob.SpeedPoint_[Glob.SpeedPointCount] = start;
                Glob.SpeedPointCount++;
            }
            //SpeedPoint.Add(start);
        }

        private void 测速数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.SpeedPointCount > 0 && Glob.SpeedControl > 0)
            {
                if (sw > 0) { return; }
                SpeedCheckPoint scp = new SpeedCheckPoint(this);
                scp.ShowDialog();
            }
            else
            {
                ShowFlowText("未找到测速信息！");
            }
            //MessageBox.Show(Glob.SpeedPointCount + "\n" + Glob.SpeedTime[Glob.SpeedPointCount - 1]);
        }

        private void 自动寻找赛文标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.isMatch)
            {
                if (Glob.SpeedPointCount == 0)
                {
                    SpeedCheckOut sco = new SpeedCheckOut(this);
                    sco.ShowDialog(this);
                }
                else
                {
                    ShowFlowText("测速点已存在！请取消后再进行本程序！");
                }
            }
            else
            {
                ShowFlowText("非比赛认证段！无法使用！");
            }
        }

        private void 清除测速点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Glob.SpeedPointCount > 0)
            {
                this.richTextBox1.SelectAll();
                this.richTextBox1.SelectionBackColor = this.richTextBox1.BackColor;
                Glob.SpeedPoint_ = new int[10];//测速点控制
                Glob.SpeedTime = new double[10];//测速点时间控制
                Glob.SpeedJs = new int[10];//键数
                Glob.SpeedHg = new int[10];//回改
                Glob.SpeedPointCount = 0;//测速点数量控制
                Glob.SpeedControl = 0;
                this.lblspeedcheck.Text = "时间";
            }
        }
        #endregion

        #region 精五比赛启用
        private void tsbJw_Click(object sender, EventArgs e)
        {
            if (Glob.jwMatchMoudle)
            {
                Glob.PreText = "-----";
                Glob.PreDuan = "第xx段";
                this.toolButton1.Checked = false;
                Glob.jwMatchMoudle = false;
                Glob.InstraSrf_ = "0";
                Glob.isQQ = false;
                Glob.isZdy = false;
                ShowFlowText("已取消精五模式");
            }
            else
            {
                if (Glob.InstraSrf.Length == 0) { ShowFlowText("请在设置里面填写输入法！"); return; }
                if (Glob.QQnumber.Length == 0) { ShowFlowText("请在设置里面填写QQ号！"); return; }
                Glob.jwMatchMoudle = true;
                Glob.PreText = "------";
                Glob.PreDuan = "第xx期精五门[比|补]赛文段";
                this.toolButton1.Checked = true;
                Glob.InstraSrf_ = "1";
                Glob.isQQ = true;
                Glob.isZdy = true;
                ShowFlowText("已设置为精五模式");
            }
        }
        #endregion

        #region 右键弹窗
        private void toolStripBtnLS_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

            }
        }
        #endregion

        #region 新段号列表
        private void cmsDuanList_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);
        }

        private void lblDuan_MouseClick(object sender, MouseEventArgs e)
        {
            cmsDuanList.Show((sender as Label), 0, (sender as Label).Height);
        }

        private static Regex getDuanList
        {
            get
            {
                if (Glob.isZdy)
                {
                    return new Regex(@"(?<=-----第)\d+(?=段)");
                }
                else
                {
                    string pretext = Glob.PreText.Replace(@"\", @"\\");
                    string preduan = Glob.PreDuan.Replace("xx", @"\d+");
                    return new Regex(@"(?<=" + pretext + preduan[0] + @")\d+(?=" + preduan[4] + ")");
                }
            }
        }

        /// <summary>
        /// 将段列出来
        /// </summary>
        private void ListDuan(string duan)
        {
            if (Glob.Text.Length != 0)
            {
                int c = 0;//控制项目十项
                MatchCollection mc = getDuanList.Matches(Glob.Text);
                if (mc.Count > 0)
                {
                    this.cmsDuanList.Items.Clear();
                    for (int i = mc.Count - 1; i > 0; i--)
                    {
                        if (c > 10) break;
                        this.cmsDuanList.Items.Add(mc[i].ToString(), null, new EventHandler(SelectDuan));
                        c++;
                        ToolStripItem tsi = (ToolStripItem)this.cmsDuanList.Items[this.cmsDuanList.Items.Count - 1];
                        tsi.Name = mc[i].ToString();
                        if (mc[i].ToString().Trim() == duan)
                            tsi.BackColor = Color.LightPink;
                        else
                            tsi.BackColor = Color.White;
                    }
                }
                else
                {
                    this.cmsDuanList.Items.Clear();
                }
            }
        }
        private void SelectDuan(object sender, EventArgs e)
        {
            string pretext, preduan;
            if (Glob.isZdy)
            {
                pretext = Glob.PreText.Replace(@"\", @"\\");
                preduan = Glob.PreDuan.Replace("xx", sender.ToString());
            }
            else
            {
                pretext = "-----";
                preduan = "第" + sender.ToString() + "段";
            }
            //MessageBox.Show(preduan);
            Regex regexAll = new Regex(@".+\s.+\s" + pretext + preduan + ".+", RegexOptions.RightToLeft); //获取发送的全部信息
            Glob.getDuan = regexAll.Match(Glob.Text);
            if (Glob.getDuan.Length == 0) //为空
            {
                toolStripStatusLabelStatus.Text = "No";
                toolTip1.SetToolTip(toolStripStatusLabelStatus, "没有找到文段");
                toolStripStatusLabelStatus.ForeColor = Color.IndianRed;
                //this.toolStripComboBoxDuan.Items.Clear();
                return;
            }
            string getDuanAll = Glob.getDuan.ToString();
            if (Glob.isZdy)
            {
                Glob.regexCout = new Regex(@"(?<=" + preduan[0] + @")" + sender.ToString() + "(?=" + preduan[4] + ")", RegexOptions.RightToLeft);
            }
            else
                Glob.regexCout = new Regex(@"(?<=第)" + sender.ToString() + "(?=段)", RegexOptions.RightToLeft);
            LoadText(pretext, preduan, Glob.regexCout, getDuanAll);
        }
        #endregion

        #region 标记


        private void PointIt(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            if (Glob.isPointIt)
            {
                this.tsb标注.Checked = false;
                Glob.isPointIt = false;
                _render.ClearLabel();
                ini.IniWriteValue("程序控制", "标记", "False");
            }
            else
            {
                this.tsb标注.Checked = true;
                Glob.isPointIt = true;
                TickIt();
                ini.IniWriteValue("程序控制", "标记", "True");
            }
        }

        private void TickIt()
        {
            List<WordInfo> wordInfos = _wordInfoUtil.GetWordInfos(this.richTextBox1.Text, Color.Blue, Color.Red);
            //string show = wordInfos.Count > 0 ? "词量" + wordInfos.Count : "剩余字数";
            //this.label8.Text = show;
            _render.Init(wordInfos, this.richTextBox1, Glob.Right);
            _render.Render();
        }

        private void InitCiKu()
        {
            string fileName = Application.StartupPath + "\\ci.txt";
            if (System.IO.File.Exists(fileName))
            {
                string ciku = System.IO.File.ReadAllText(fileName, System.Text.Encoding.Default);
                ciku = ciku.Replace("\r\n", "\r");
                ciku = ciku.Replace("\n", "\r");
                string[] astr = ciku.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);

                _wordInfoUtil = new WordInfoUtil();
                _wordInfoUtil.SetCiKu(astr);
            }
            else
            {
                this.tsb标注.Enabled = false;
                this.tsb标注.ToolTipText = "请将词组标记文件ci.txt放置跟打器根目录后重启";
                //this.label8.Text = "剩余字数";
            }

        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            if (Glob.isPointIt)
                _render.Render();
        }

        private void richTextBox1_HScroll(object sender, EventArgs e)
        {
            if (Glob.isPointIt)
                _render.Render();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Glob.isPointIt)
                _render.Render();
        }
        #endregion

        #region 语句
        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            Font F = new Font("宋体", 9f);
            Brush B = new SolidBrush(Color.LightGray);
            Brush B2 = new SolidBrush(Color.DimGray);
            string s = "添雨";
            e.Graphics.DrawString(s, F, B2, new Point(2, 3)); //上
            e.Graphics.DrawString(s, F, B2, new Point(2, 5)); //下
            e.Graphics.DrawString(s, F, B2, new Point(3, 4)); //左
            e.Graphics.DrawString(s, F, B2, new Point(3, 4)); //右
            e.Graphics.DrawString(s, F, B, new Point(3, 4));
        }
        #endregion

        #region 快捷键列表
        private void 重打ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F3();
        }

        private void 载文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F4();
        }

        private void 换群ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            F5();
        }

        //发送到桌面的快捷方式
        private void 发送到桌面的快捷方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shortcut();
        }

        /// <summary>
        /// 创建桌面快捷方式并开机启动的方法
        /// </summary>
        private void Shortcut()
        {
            //获取当前系统用户启动目录
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            //获取当前系统用户桌面目录
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileInfo fileStartup = new FileInfo(startupPath + "\\添雨跟打器.lnk");

            FileInfo fileDesktop = new FileInfo(desktopPath + "\\添雨跟打器.lnk");
            if (!fileDesktop.Exists)
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(
                      Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) +
                      "\\添雨跟打器.lnk");

                shortcut.TargetPath = Application.StartupPath + "\\" + Glob.Form + ".exe";//启动更新程序
                shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
                shortcut.WindowStyle = 1;
                shortcut.Description = "添雨跟打器";
                shortcut.IconLocation = Application.ExecutablePath;
                shortcut.Save();
            }
            /*
              if (!fileStartup.Exists)
               {
                    //获取可执行文件快捷方式的全部路径
                    string exeDir = desktopPath + "\\添雨跟打器.lnk";
                    //把程序快捷方式复制到启动目录
                     System.IO.File.Copy(exeDir, startupPath + "\\添雨跟打器.lnk", true);
               }*/
        }
        //////
        #endregion


        #region 透明背景处
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mS1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

        #region 主题的设置

        private void toolStrip1_Paint_1(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width - 1, this.toolStrip1.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }

        private void 外观ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTheme ft = new FormTheme(this);
            ft.ShowDialog();
        }
        #endregion

        private void tbnSpline_Click(object sender, EventArgs e)
        {
            _Ini Setupini = new _Ini("Ttyping.ty");
            //是否显示曲线
            if (this.tbnSpline.Checked)
            {
                splitContainer4.Panel1Collapsed = true;
                Setupini.IniWriteValue("拖动条", "曲线", "true");
                this.tbnSpline.Checked = false;
            }
            else
            {
                splitContainer4.Panel1Collapsed = false;
                Setupini.IniWriteValue("拖动条", "曲线", "false");
                this.tbnSpline.Checked = true;
            }
        }

        private void 编码提示设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formBmTips = new FormBMTips();
            formBmTips.ShowDialog();
        }

        #region 跟打报告
        private void 跟打报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication2.跟打报告.TypeAnalysis tya = new 跟打报告.TypeAnalysis();
            tya.ShowDialog();
        }
        #endregion

        #region 检查更新

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var upgrade = new UpgradePro();
            upgrade.ShowDialog();
        }

        #endregion

        private void 捐助作者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            捐助作者 d捐助作者 = new 捐助作者();
            d捐助作者.ShowDialog();
        }

        private void 捐助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            捐助作者 d捐助作者 = new 捐助作者();
            d捐助作者.ShowDialog();
        }
    }
}