using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class SpeedAn : Form
    {
        private string[] Data = new string[2];
        Form1 frm;
        public SpeedAn(string[] get,Form1 frm1)
        {
            Data = get;//传递数据
            frm = frm1;
            InitializeComponent();
        }

        private void SpeedAn_Load(object sender, EventArgs e)
        {
            if (Data[0] != "") { 
                string[] data = Data[0].Split('|');//获取各项数据
                if (data.Length == 8)
                {
                    this.Text = "第" + Glob.Pre_Cout + "段速度分析";
                    //准备画布
                    Bitmap bmp = new Bitmap(this.SpeedAnGet.Width + 2, this.SpeedAnGet.Height + 20);
                    Rectangle rect = new Rectangle(1, 1, this.SpeedAnGet.Width, this.SpeedAnGet.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    //g.Clear(Color.DimGray);
                    //准备数据
                    double 理论值 = double.TryParse(data[0], out 理论值) ? 理论值 : 0;//总长
                    //double TotalWidth2 = double.TryParse(data[1], out TotalWidth2) ? TotalWidth2 : 0;//总长2
                    //double ImpactWidth = double.TryParse(data[2], out ImpactWidth) ? ImpactWidth : 0;//实际长
                    //double Hg = double.TryParse(data[3], out Hg) ? Hg : 0;
                    //double Bg = double.TryParse(data[4], out Bg) ? Bg : 0;
                    //double St = double.TryParse(data[5], out St) ? St : 0;
                    //double Cz = double.TryParse(data[6], out Cz) ? Cz : 0;
                    //double En = double.TryParse(data[7], out En) ? En : 0;
                    //开始画
                    //矩形1 总长1
                    int BmpWidth = this.SpeedAnGet.Width - 150; //矩形终点长度
                    int X = 15, Y = 8, width = 18; //整体坐标
                    //码长理论长度
                    for (int i = 0; i < data.Length; i++)
                    {
                        SolidBrush SB_TotalWidth;
                        Color Colour = Color.FromArgb(10, 166, 146);
                        string MC_ = "";
                        double TotalWidth = double.TryParse(data[i], out TotalWidth) ? TotalWidth < 0 ? 0 : TotalWidth : 0;//总长
                        switch (i)
                        {
                            case 0:
                                Colour = Color.FromArgb(10, 166, 146);
                                MC_ = "完美理论值：" + TotalWidth.ToString("0.00");
                                break;
                            case 1:
                                Colour = Color.FromArgb(7, 153, 7);
                                MC_ = "码长理论值：" + TotalWidth.ToString("0.00");
                                break;
                            case 2:
                                Colour = Color.FromArgb(195, 31, 89);
                                MC_ = "跟打实际值：" + TotalWidth.ToString("0.00");
                                break;
                            case 3:
                                Colour = Color.FromArgb(150, 27, 181);
                                MC_ = "回改影响值：-" + TotalWidth.ToString("0.00");
                                break;
                            case 4:
                                Colour = Color.FromArgb(202, 122, 36);
                                MC_ = "退格影响值：-" + TotalWidth.ToString("0.00");
                                break;
                            case 5:
                                Colour = Color.FromArgb(222, 51, 51);
                                MC_ = "停留影响值：-" + TotalWidth.ToString("0.00");
                                break;
                            case 6:
                                Colour =Color.FromArgb(110, 88, 242);
                                MC_ = "错字影响值：-" + TotalWidth.ToString("0.00");
                                break;
                            case 7:
                                Colour =Color.FromArgb(164, 193, 65);
                                MC_ = "回车影响值：-" + TotalWidth.ToString("0.00");
                                break;
                            default:
                                Colour =Color.FromArgb(7, 153, 7);
                                MC_ = "XXX" + TotalWidth;
                                break;
                        }
                        Font F = new Font("宋体",9f);//画字字体
                        double Width = TotalWidth * BmpWidth / 理论值;
                        if (Glob.PauseTimes > 0 && i == 5) TotalWidth = 0; //有暂停时 ，停留不计
                        if (TotalWidth.ToString("0.00") == "0.00")
                        {
                            Width = 1;
                            Colour = Color.Gray;
                            F = new Font("宋体", 9f, FontStyle.Strikeout);//画字字体
                        }
                        else {
                            F = new Font("宋体", 9f);//画字字体
                        }
                        SB_TotalWidth = new SolidBrush(Colour);
                        Rectangle rect_TotalWidth = new Rectangle(X, Y, (int)Math.Floor(Width), width);
                        g.FillRectangle(SB_TotalWidth, rect_TotalWidth); //画码长理论 TotalWidth
                        //线条
                        float Start = (float)X;
                        Color PLine_ = Color.LightGray;
                        int HeighP = 1;//偏移量
                        Pen PLine = new Pen(Color.Gray,1);
                        Pen PLine2 = new Pen(Color.LightGray,1);
                        PLine2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        if (i == 2)
                        {//第三行数据列
                            PLine_ = Color.FromArgb(61, 61, 61);
                            HeighP = 3;
                            Start = this.SpeedAnGet.Width - 120;
                            g.DrawLine(PLine2, (float)X, Y + width + HeighP, Start, Y + width + HeighP);
                            g.DrawLine(PLine, Start, Y + width + HeighP, bmp.Width - 9, Y + width + HeighP);
                        }
                        else
                        {
                            PLine_ = Color.LightGray;
                            HeighP = 1;
                            Start = (float)X;
                        }
                        PLine = new Pen(PLine_, 1);
                        //线条终
                        if (i != 2)
                            g.DrawLine(PLine2, Start, Y + width + HeighP, bmp.Width - 9, Y + width + HeighP);
                        g.DrawString(MC_, F, SB_TotalWidth, this.SpeedAnGet.Width - 120, Y + 4);//画字
                        Y += 25;
                    }
                    /*
                    //完美值
                    Y += 30;
                    double Perfect_Width = TotalWidth2 * BmpWidth / TotalWidth;
                    Rectangle rect_PerfectWidth = new Rectangle(X, Y, (int)Perfect_Width, width);
                    SolidBrush SB_PerfectWidth = new SolidBrush(Color.FromArgb(7, 153, 7));
                    string PF_ = "完美理论值：" + TotalWidth2;
                    SizeF PF_Widht = g.MeasureString(PF_,F);
                    g.FillRectangle(SB_PerfectWidth,rect_PerfectWidth);
                    g.DrawString(PF_, F,Brushes.White,rect_TotalWidth.Width - PF_Widht.Width + 20, Y + 4);
                     */
                    //显示
                    this.SpeedAnGet.Image = bmp;
                }
                //MessageBox.Show(Data[0] + "\n" + data[0] + "\n" + data[data.Length - 1]);
            }
        }

        //复制文本
        private void GetText_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Data[1]);
        }

        private void SendText_Click(object sender, EventArgs e)
        {
            frm.sendtext(Data[1]);
        }

        //截图类
        private void GetPic_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(getPic());
        }

        private Bitmap getPic()
        {
            Clipboard.Clear();
            Bitmap bmp = new Bitmap(this.SpeedAnGet.Width + 2, this.SpeedAnGet.Height + 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.DimGray);
            var rect = new Rectangle(1, 21, this.SpeedAnGet.Width, this.SpeedAnGet.Height);//定义矩形
            this.SpeedAnGet.DrawToBitmap(bmp, rect);
            Font F = new Font("宋体", 9f);
            string s = Glob.Form + "(" + Glob.Instration.Trim() + Glob.Ver + ")";
            SizeF sF = g.MeasureString(s, F);
            g.DrawString(s, F, Brushes.White, this.SpeedAnGet.Width - sF.Width + 2, bmp.Height - 15);
            g.DrawString("第" + Glob.Pre_Cout + "段速度分析",F,Brushes.White,3,4);
            g.DrawString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(), F, Brushes.LightGray, 2, bmp.Height - 15);
            return bmp;
        }

        private void SendPic_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(getPic());
            frm.SendClipBoardToQQ();
        }
    }
}
