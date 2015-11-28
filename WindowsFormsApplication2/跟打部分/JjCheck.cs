using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication2
{
    public partial class JjCheck : Form
    {
        Form1 frm;
        private ChartArea CA_ = new ChartArea();//创建图表区域
        private Series SJJ_ = new Series("比例");//创建线条
        private Title Ti = new Title("击键评定");
        public JjCheck(Form1 frm1)
        {
            frm = frm1;
            InitializeComponent();
            this.chart1.ChartAreas.Add(CA_);
            this.chart1.Series.Add(SJJ_);
            SJJ_.ChartType = SeriesChartType.Column;//柱状形
            this.CA_.AxisX.LabelAutoFitMaxFontSize = 7;
            this.CA_.AxisY.LabelAutoFitMaxFontSize = 8;
            this.CA_.AxisX.MajorGrid.LineColor = Color.LightGray;
            this.CA_.AxisY.MajorGrid.LineColor = Color.LightGray;
            this.chart1.Titles.Add(Ti);
            this.Ti.ForeColor = Color.Black;
        }

        private void JjCheck_Load(object sender, EventArgs e)
        {
            double jjP;
            int jjC = 0;
            double jjC_ = 0;
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("击键历史总计及分析：\r\n您总共跟打了" + Glob.jjAllC + "段");
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

                this.SJJ_.Points.AddXY(i+4,jjP*100);
            }
            if (jjC != 0)
            {
                Ti.Text = "您总共跟打了[ " + Glob.jjAllC + " ]段 击键评定为[ " + (jjC + jjC_).ToString("0.000")+" ]";
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //发送
        private void SendText_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(getPic());
            frm.SendClipBoardToQQ();
        }

        //复制
        private void GetText_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(Ti.Text);
        }

        //截图
        private void GetPIC_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(getPic());
        }

        private Bitmap getPic() {
            Clipboard.Clear();
            Bitmap bmp = new Bitmap(this.chart1.Width + 2, this.chart1.Height + 21);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.DimGray);
            Rectangle rect = new Rectangle(1, 1, this.chart1.Width, this.chart1.Height);//定义矩形
            this.chart1.DrawToBitmap(bmp, rect);
            Font F = new Font("宋体", 9f);
            string s = Glob.Form + "(" + Glob.Instration.Trim() + Glob.Ver + ")";
            SizeF sF = g.MeasureString(s, F);
            g.DrawString(s, F, Brushes.White, this.chart1.Width - sF.Width + 2, this.chart1.Height + 4);
            g.DrawString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(), F, Brushes.LightGray, 2, this.chart1.Height + 4);
            return bmp;
        }
    }
}
