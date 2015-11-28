using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication2
{
    public class ShowMessage
    {
        Label L_Message;
        Point P;//显示位置
        Size S;//大小
        Form F;
        Form frm;
        MagneticMagnager mm;
        /// <summary>
        /// 初始化操作
        /// </summary>
        public ShowMessage(Size s, Point p,Form frm1)
        {
            //显示的位置
            P = p;
            S = s;
            F = new Form();//新建
            try
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item.Name == "MessageShow")
                    {
                        item.Close();
                    }
                }
            }
            catch { }
            F.Name = "MessageShow";
            frm = frm1;
            //设置窗体的样式为空
            F.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //初始化标签
            L_Message = new Label();
            L_Message.AutoSize = true;
            L_Message.TextAlign = ContentAlignment.MiddleCenter;
            L_Message.BackColor = Color.FromArgb(95,112,122);
            L_Message.ForeColor = Color.FromArgb(236,252,255);
            L_Message.Font = new System.Drawing.Font("微软雅黑",11f);
            L_Message.Dock = DockStyle.Fill;
            L_Message.MaximumSize = new Size(frm.Width,frm.Height);
            L_Message.AutoEllipsis = true;
            L_Message.Click += new EventHandler(L_Message_Click);
            //添加控件
            F.Controls.Add(L_Message);
            F.ShowInTaskbar = false;
            F.TopMost = true;
            F.Load += new EventHandler(ShowMessage_Load);
            F.Opacity = 0;
            F.Location = new Point(P.X + S.Width / 2 - L_Message.Width / 2, P.Y - S.Height - 30);
            mm = new MagneticMagnager(frm, F, MagneticPosition.BottomUp);
            //设置布局方式
            L_Message.Location = new Point(0,0);
        }

        //点击关闭
        void L_Message_Click(object sender, EventArgs e)
        {
            F.Close();
        }

        void ShowMessage_Load(object sender, EventArgs e)
        {
            
            F.Size = L_Message.Size;
            Timer t = new Timer();
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        public double Time = 2.3;
        public string TEXT;
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="text">需要显示的内容</param>
        public void Show(string text) {
            L_Message.Text = text;
            TEXT = text;
            L_Message.Paint += new PaintEventHandler(L_Message_Paint);
            
            if (text.Length > 15)
            {
                Time += (text.Length / 5);
            }
            else Time = 2.5;
            mm.LengthMessage = L_Message.Size;
            F.Show();
            frm.Activate();
        }

        void L_Message_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //画渐变色
            System.Drawing.Drawing2D.LinearGradientBrush lgb = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, L_Message.Height), Color.FromArgb(20, Color.White), Color.FromArgb(0, Color.FromArgb(80, 95, 103)));
            System.Drawing.Drawing2D.GraphicsPath gp = GetRoundedRectPath(new Rectangle(new Point(0, 0), L_Message.Size), 5);
            g.FillPath(lgb, gp);
            //g.Clear(Color.FromArgb(63,63,63));
            //g.DrawString(TEXT, new Font("宋体", 11f), Brushes.White, Point.Empty);
            //g.DrawLine(new Pen(Theme.ThemeColorBG, 2), 0, L_Message.Height - 1, L_Message.Width, L_Message.Height - 1);
            g.DrawRectangle(new Pen(Color.FromArgb(47, 56, 61)),0,0,e.ClipRectangle.Width - 1,e.ClipRectangle.Height - 1);
        }

        public static System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        //渐入
        void t_Tick(object sender, EventArgs e)
        {
            //L_Message.Text = F.Opacity.ToString();
            if (F.Opacity >= 1)
            {
                (sender as Timer).Stop();
                StartT = DateTime.Now;//用于显示时间
                Timer T_ = new Timer();
                T_.Tick += new EventHandler(T__Tick);
                T_.Start();
            }
            else {
                if (!F.IsDisposed)
                    F.Opacity += 0.3;
                else
                    (sender as Timer).Stop();
            }
        }

        //渐出
        private DateTime StartT;
        void T__Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - StartT).TotalSeconds >= 2.5)
            {
                F.Opacity -= 0.1;
                if (F.Opacity <= 0)
                {
                    (sender as Timer).Stop();
                    F.Close();
                }
            }
        }
    }
}
