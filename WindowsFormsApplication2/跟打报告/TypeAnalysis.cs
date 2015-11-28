using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApplication2.跟打报告
{
    public partial class TypeAnalysis : Form
    {
        public TypeAnalysis()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 载入跟打报告的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeAnalysis_Load(object sender, EventArgs e)
        {
            Type type = dataGridView1.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView1, true, null);

            //this.dataGridView1.DataSource = Glob.TypeReport.ToArray();
            this.dataGridView1.Rows.Add("序","起","止","文字","用时","键数");
            this.dataGridView1.Rows[0].Frozen = true;
            this.dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(64,128,128);
            this.dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.White;
            ShowToTable();
            ShowToPic();
        }

        /// <summary>
        /// 显示到表格上面  初始化操作
        /// </summary>
        private void ShowToTable()
        {
            foreach (var item in Glob.TypeReport)
            {
                this.dataGridView1.Rows.Add(item.Index,item.Start,item.End,(item.Start < item.End) ? Glob.TypeText.Substring(item.Start,item.Length):"□",item.TotalTime.ToString("0.0000"),item.TotalTick);
                if (item.Length < 0)
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightPink;
            }
            try
            {
                this.dataGridView1.Rows.Add("总", Glob.TypeReport[0].Start, Glob.TypeReport[Glob.TypeReport.Count - 1].End, "ALL", Glob.TypeReport.Sum(o => o.TotalTime).ToString("0.0000"), Glob.TypeReport.Sum(o => o.TotalTick));
            }
            catch { 
                
            }
        }

        /// <summary>
        /// 用PIC方式显示
        /// </summary>
        private void ShowToPic() {
            Bitmap bmp = new Bitmap(this.pic_analysis.Width,120 + Glob.TypeText.Length + Glob.TextHg);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White); //清洗画布
            
            //标题啦
            Font title_font = new Font("微软雅黑",14,FontStyle.Bold);
            g.DrawString("跟打报告",title_font,Brushes.ForestGreen,20,20);
            string text_speed = Glob.TextSpeed.ToString("0.00");
            g.DrawString(text_speed,title_font,Brushes.SeaGreen,bmp.Width - 20 - GetWH(g,text_speed,title_font).Width,20);//速度
            int title_height = (int)GetWH(g,"跟",title_font).Height + 23;
            g.DrawLine(new Pen(Color.DarkSlateBlue, 3), 10, title_height, bmp.Width - 10, title_height);
            //副级标题1 全文跟打详细过程
            int p_Start_X = 10;
            int p_Start_Y = title_height + 10;
            Font p_title_font = new Font("微软雅黑",10,FontStyle.Bold);
            Color p_title_color = Color.Black;
            g.DrawString(">跟打详细过程<" + ((Glob.PicName.Length > 0) ? Glob.PicName + ">" : ""), p_title_font, new SolidBrush(p_title_color), p_Start_X, p_Start_Y);
            /* ====================================== 关键代码 ======================================*/
            /*
             * 此处详细叙述打字过程
             * 显示信息有： 文字、速度、击键、码长、回改、升降等信息
             * ……
             */
            /* 声明 */
            Font t_font = new Font("宋体",11); //字体是必须的 么么哒
            Font t_font_hg = new System.Drawing.Font("Arial",6);//回改处的信息
            int t_Start_X = p_Start_X + 10; //纳尼，起点 坐标不搞，后期很混乱的说
            int t_Start_Y = p_Start_Y + (int)GetWH(g,"测",p_title_font).Height + 10;
            int t_width = bmp.Width - t_Start_X - 10;
            /* 先弄点地方以后写个注释啥的 
             * 开写
             */
            //获取用时最高的十个地方
            List<TypeDate> UseHighTime = Glob.TypeReport.OrderByDescending(o => o.TotalTime).Take(10).ToList();

            int t_nowX = t_Start_X + 10;
            int t_nowY = t_Start_Y;
            int t_distance = (int)GetWH(g,"测",t_font).Height + 12;//换行间隔
            //回改前
            int t_zis = 0;//字数累计
            int t_js = 0;//键数
            double t_time = 0;//时间累计
            double t_hg_time = 0;//回改时间累计
            int t_last_width = 0;//存储上次的宽度
            int t_splite_distance = 3;//文字间隔
            int t_splite_info = 5;//信息上下间隔
            bool t_control = true;//回改处显示信息的控制器
            int t_jump = 0;

            double speed = 0;
            double jj = 0; SolidBrush jj_Color = new SolidBrush(Color.Red);
            double mc = 0; SolidBrush mc_Color = new SolidBrush(Color.RoyalBlue);
            int info_splite = 26; //间隔

            for (int i = 1; i < this.dataGridView1.Rows.Count - 1; i++) {
                string t_text = this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                int t_text_width = 1;
                if (t_text != "□")
                    t_text_width = (int)GetWH(g, t_text, t_font).Width;

                if (t_zis > 0 && t_time > 0)
                {
                    //每行显示 速度
                    speed = (t_zis * 60 / t_time);
                    jj = (t_js / t_time);
                    mc = ((double)t_js / t_zis);

                }

                if (t_nowX + t_text_width > bmp.Width - 20)
                {
                    g.DrawString(speed.ToString("0.00"), t_font_hg, Brushes.DimGray, bmp.Width - info_splite, t_nowY - t_splite_info - 2);
                    g.DrawString(jj.ToString("0.00"), t_font_hg, (jj >= 8 ) ? jj_Color : Brushes.DimGray, bmp.Width - info_splite, t_nowY);
                    g.DrawString(mc.ToString("0.00"), t_font_hg, (mc <= 2.20 ) ? mc_Color : Brushes.DimGray, bmp.Width - info_splite, t_nowY + t_splite_info + 2);
                    t_nowX = t_Start_X;
                    t_nowY += t_distance;//换行

                    t_js = 0; t_zis = 0; t_time = 0;
                }
                //最后一行
                if (i == this.dataGridView1.Rows.Count - 2) {
                    g.DrawString(speed.ToString("0.00"), t_font_hg, Brushes.DimGray, bmp.Width - info_splite, t_nowY - t_splite_info - 2);
                    g.DrawString(jj.ToString("0.00"), t_font_hg, (jj >= 8) ? jj_Color : Brushes.DimGray, bmp.Width - info_splite, t_nowY);
                    g.DrawString(mc.ToString("0.00"), t_font_hg, (mc <= 2.20) ? mc_Color : Brushes.DimGray, bmp.Width - info_splite, t_nowY + t_splite_info + 2);
                }

                if (i != 1 && t_text != "□") //第一次输入不算
                    t_zis += t_text.Length;

                t_js += (int)this.dataGridView1.Rows[i].Cells["键数"].Value;
                t_time += double.Parse(this.dataGridView1.Rows[i].Cells["时间"].Value.ToString());

                if (t_text != "□")
                { //非回改
                    bool b_temp = UseHighTime.Exists(o => o.Index == (int)this.dataGridView1.Rows[i].Cells["序"].Value); 
                    //如果是最高用时 下，红，再如果 回改 黄
                    g.DrawString(t_text, t_font, b_temp ? t_control ? Brushes.DarkRed : Brushes.Gray : Brushes.Black, t_nowX, t_nowY);
                    if (b_temp) {
                        string text_temp = double.Parse(this.dataGridView1.Rows[i].Cells["时间"].Value.ToString()).ToString("0.00");
                        Font newFont = new Font("Arial",6);
                        Pen newPen = new Pen(Color.DarkRed);
                        g.DrawLine(newPen,t_nowX,t_nowY + 15,t_nowX + t_text_width,t_nowY + 15);
                        g.DrawLine(newPen, t_nowX + t_text_width/2, t_nowY + 15, t_nowX + t_text_width/2, t_nowY + 17);
                        g.DrawString(text_temp,newFont,Brushes.DarkRed,t_nowX + t_text_width/2 - GetWH(g,text_temp,newFont).Width/2,t_nowY + 18);
                    }

                    bool b_temp2 = (speed > Glob.TextSpeed);
                    if (!b_temp2) //速度低于平均值时
                        g.DrawLine(new Pen(Color.MediumVioletRed,2), t_nowX, t_nowY + 16, t_nowX + t_text_width, t_nowY + 16);
                    //高击键区 击键上8的情况
                    if (jj >= 8)
                        g.DrawLine(new Pen(Color.Green, 1), t_nowX, t_nowY + 16, t_nowX + t_text_width, t_nowY + 16);

                    t_control = true;//控制器回位
                }
                else { //回改处将显示部分信息 由于可能是连续回改，所以启用一个控制器 t_control
                    t_jump++;
                    double t_hg_time_ = double.Parse(this.dataGridView1.Rows[i].Cells["时间"].Value.ToString());
                    //t_js += (int)this.dataGridView1.Rows[i].Cells["键数"].Value;
                    if (t_control) {
                        int t_splite_info_ = t_splite_info; //用时换行时的调配
                        string t_speed = (t_zis * 60/t_time).ToString("0.00"); //回改前
                        int t_hg_Y = t_nowY, t_hg_X = t_nowX;
                        if (t_nowX + GetWH(g, t_speed, t_font_hg).Width > bmp.Width - 20) {
                            t_hg_X = t_Start_X;
                            t_hg_Y += t_distance;
                            t_splite_info_ = 12; //换行调配
                        } //换行
                        g.DrawString(t_speed,t_font_hg,Brushes.IndianRed,t_hg_X,t_hg_Y - t_splite_info_);
                        t_text_width = (int)GetWH(g, t_speed, t_font_hg).Width;
                        t_last_width = t_text_width;
                        //用于计算回改后
                        t_time += t_hg_time_;
                        g.DrawString((t_zis * 60 / t_time).ToString("0.00"), t_font_hg, Brushes.DarkBlue, t_hg_X, t_hg_Y + t_splite_info_);
                    }
                    t_control = false;
                    t_hg_time += t_hg_time_;//回改时间累计
                }
                //if (t_control)
                //{
                //    i += t_jump - 1;//跳过处理
                //    t_jump = 0;
               // }
                
                t_nowX += t_text_width - t_splite_distance; //减为间隔
            }
            /* ====================================== ******** ======================================*/
            
            //尾标
            Font Last_Font = new Font("Verdana",9f);
            string Last_Text = Glob.Form + Glob.Ver + "(" + Glob.Instration + ")";
            SizeF Last_Text_SizeF = GetWH(g,Last_Text,Last_Font);
            g.DrawString(Last_Text,Last_Font,Brushes.DimGray,bmp.Width - Last_Text_SizeF.Width,bmp.Height - Last_Text_SizeF.Height - 3);
            //给画布画上边框
            g.DrawRectangle(new Pen(Color.Green, 2), 1, 1, bmp.Width - 2, bmp.Height - 2);
            //显示出来
            this.pic_analysis.Height = bmp.Height;
            this.pic_analysis.Image = bmp;
        }

        private SizeF GetWH(Graphics g,string text,Font F) {
            return g.MeasureString(text,F);
        }

        private void btn_putintoclip_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(this.pic_analysis.Image);
        }
    }
}
