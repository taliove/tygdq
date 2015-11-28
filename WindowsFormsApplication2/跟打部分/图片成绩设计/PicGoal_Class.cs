using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
namespace WindowsFormsApplication2
{
    /*
     * 这是一个图片成绩的初始类
     * 用于测试部分
     * 
     */
    public class PicGoal_Class:IDisposable
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title = "无标题";
        /// <summary>
        /// 需要显示的内容
        /// </summary>
        public Size Pic_Size = new Size(280,200);
        public Bitmap Pic_Bmp;
        /// <summary>
        /// 初始化类
        /// </summary>
        public PicGoal_Class() {
            if (Pic_Bmp == null) Pic_Bmp = new Bitmap(Pic_Size.Width,Pic_Size.Height);
        }

        /// <summary>
        /// 取得图片
        /// </summary>
        /// <param name="jz">键准取得</param>
        /// 
        /// <returns></returns>
        public Bitmap GetPic(float jz,string title){
            //取得画布
            Graphics g = Graphics.FromImage(Pic_Bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            //填充颜色
            g.Clear(Color.FromArgb(240, 240, 240));//一种中蓝色
            Font F = new Font("宋体",12f);
            int StartH = 30;
            //外框画笔
            Pen BorderP =new Pen(Brushes.DimGray);
            //画重打颜色 绿色为新打。红色为重打
            //以空格分隔需要显示的项目
            //段
            Font L_ = new Font("Verdana", 9f);
            if (Glob.ReTypePD) //重打 红
            {
                g.FillPie(Brushes.IndianRed, Pic_Bmp.Width - 20, 0, 20, 20, -90, 360);
                g.DrawString("重", L_, Brushes.White, Pic_Bmp.Width - 20 + 2, 3);
            }
            else { //新打绿
                g.FillPie(Brushes.ForestGreen, Pic_Bmp.Width - 20, 0, 20, 20, -90, 360);
                g.DrawString("新", L_, Brushes.White, Pic_Bmp.Width - 20 + 2,3);
            }
            //成绩列表X值
            int Left = 190;
            //画段号外框
            g.DrawRectangle(BorderP,Left - 1,StartH - 1,82,22);
            g.FillRectangle(Brushes.DimGray, Left, StartH, 80, 20);//画底色
            g.DrawString("第" + Glob.Pre_Cout + "段",L_,Brushes.White,Left + 1,StartH + 3);
            //速度
            //以80为限
            //算出理论速度
            double 理论速度 = Glob.Textjj * 60 / (Glob.TextJs * jz / Glob.TextLen);
            //画出当前速度
            StartH += 30;
            int width = (int)(Glob.TextSpeed * 80 / 理论速度);
            g.DrawRectangle(BorderP, Left - 1, StartH - 1, 82, 22);//外框
            g.FillRectangle(Brushes.DimGray, Left, StartH, 80, 20);//画底色
            g.FillRectangle(Brushes.DarkGreen, Left, StartH, width, 20);
            g.DrawString("当前" + Glob.TextSpeed.ToString("0.00"), L_, (width < 40) ? Brushes.Black : Brushes.White, Left + 1, StartH + 3);//画当前速度
            //画出理论速度
            StartH += 30;
            g.DrawRectangle(BorderP,Left - 1,StartH - 1,82,22);
            g.FillRectangle(Brushes.DarkRed, Left, StartH, 80, 20);
            g.DrawString("理论" + 理论速度.ToString("0.00"),L_,Brushes.White,Left + 1,StartH + 3);

            //画出击键
            StartH += 30;
            g.DrawRectangle(BorderP, Left - 1, StartH - 1, 82, 22);
            g.FillRectangle(Brushes.DarkSlateBlue, Left, StartH, 80, 20);//画底色
            g.DrawString("击键" + Glob.Textjj.ToString("0.00"), L_, Brushes.White, Left + 1, StartH + 3);

            //画出码长
            StartH += 30;
            g.DrawRectangle(BorderP, Left - 1, StartH - 1, 82, 22);
            g.FillRectangle(Brushes.DarkCyan, Left, StartH, 80, 20);//画底色
            g.DrawString("码长" + Glob.Textmc.ToString("0.00"), L_, Brushes.White, Left + 1, StartH + 3);

            
            //饼形图为键准
            int Jz_Rect = 80;
            int Jz_X = 5,Jz_Y = 35;
            g.FillPie(Brushes.LightGray, Jz_X, Jz_Y, Jz_Rect, Jz_Rect, -90, 360);
            if (jz > 0)
                g.FillPie(Brushes.DarkCyan, Jz_X, Jz_Y, Jz_Rect, Jz_Rect, -90, -(360f * jz));
            //显示键准
            Font JzNum_Font = new Font("Verdana",12,FontStyle.Bold);
            string JzNum_Text = (jz > 0) ? (jz == 1) ? "PERFECT" : (jz * 100).ToString("0.00") + "%" : "Null";
            SizeF JzNum_Size = g.MeasureString(JzNum_Text,JzNum_Font);
            g.DrawString(JzNum_Text, JzNum_Font, Brushes.White, Jz_Rect / 2 + Jz_X - JzNum_Size.Width / 2, Jz_Rect / 2 + Jz_Y - JzNum_Size.Height / 2);
            g.DrawString("键准", L_, Brushes.White, Jz_X + 25, Jz_Y + Jz_Rect - 20);
            //效率 
            int Xl_Rect = 80;//效率半径
            int Xl_StartX = 100,Xl_StartY = 35;
            g.FillPie(Brushes.LightGray, Xl_StartX, Xl_StartY, Xl_Rect, Xl_Rect, -90, 360);//底色绘画
            if (Glob.效率 <= 100)
                g.FillPie(new SolidBrush(Color.FromArgb(206,97,0)), Xl_StartX, Xl_StartY, Xl_Rect, Xl_Rect, -90, -(int)(360 * Glob.效率 / 100));//底色绘画

            if (Glob.效率 > 100) //大于一百时
            {
                double Xl_Temp = Glob.效率 - 100;
                g.FillPie(Brushes.DarkSlateBlue, Xl_StartX, Xl_StartY, Xl_Rect, Xl_Rect, -90, 360);//底色绘画
                g.FillPie(Brushes.Tomato, Xl_StartX, Xl_StartY, Xl_Rect, Xl_Rect, -90, -(int)(360 * Xl_Temp/100));
            }
            string Xl_Text = Glob.效率 + "%";
            Font Xl_Font = new Font("Verdana",12f,FontStyle.Bold);
            SizeF Xl_Size = g.MeasureString(Xl_Text,Xl_Font);
            //写效率 
            g.DrawString(Xl_Text, Xl_Font, Brushes.White, Xl_StartX + Xl_Rect / 2 - Xl_Size.Width / 2 + 5, Xl_StartY + Xl_Rect / 2 - Xl_Size.Height / 2);

            g.DrawString("效率", L_, Brushes.White, Xl_StartX + 25 , Xl_StartY + Xl_Rect - 20);
            //图例
            //键准
            //g.DrawRectangle(BorderP, Left - 1, 9, 32, 12);
            //g.FillRectangle(Brushes.DarkCyan, Left, 10, 30, 10);
            //g.DrawString("键准",L_,Brushes.Black,Left + 33,8);
            //时间标 左上角
            Font text_Font = new Font("Verdana",8);

            //左下角
            //画出字数
            Pen B_Table = new Pen(Color.Gray);
            int ZiSH = Pic_Bmp.Height - 80;
            int ZiSX = 30;
            g.DrawString("字数",text_Font,Brushes.DimGray,ZiSX + 10,ZiSH);
            string zis = 字数格式化(Glob.TextLen);
            g.DrawString(zis,text_Font,Brushes.DimGray, 105 - g.MeasureString(zis,text_Font).Width - 3,ZiSH);
            g.DrawLine(B_Table, ZiSX + 5, ZiSH + 16, 100, ZiSH + 16);
            //画出回改
            ZiSH += 18;
            g.DrawString("回改",text_Font,Brushes.DimGray,ZiSX + 10,ZiSH);
            g.DrawString(Glob.TextHg.ToString(), text_Font, (Glob.TextHg > 0) ? Brushes.OrangeRed : Brushes.DimGray, 105 - g.MeasureString(Glob.TextHg.ToString(), text_Font).Width - 3, ZiSH);
            g.DrawLine(B_Table, ZiSX + 5, ZiSH + 16, 100, ZiSH + 16);
            //画出错字
            ZiSH += 18;
            g.DrawString("错字", text_Font, Brushes.DimGray, ZiSX + 10, ZiSH);
            g.DrawString(Glob.TextCz.ToString(), text_Font, (Glob.TextCz > 0) ? Brushes.DeepPink:Brushes.DimGray, 105 - g.MeasureString(Glob.TextCz.ToString(), text_Font).Width - 3, ZiSH);
            g.DrawLine(B_Table, ZiSX + 5, ZiSH + 16, 100, ZiSH + 16);
            //今日跟打
            ZiSH -= 36;
            ZiSX = 105;
            double PerTypeCount = (double)Glob.TextRecLenAll / Glob.TextRecDays;

            g.DrawString("今日", text_Font, Brushes.DimGray, ZiSX + 10, ZiSH);
            zis = 字数格式化(Glob.todayTyping);
            g.DrawString(zis, text_Font, (Glob.todayTyping > PerTypeCount) ? Brushes.DarkBlue:Brushes.DimGray, 190 - g.MeasureString(zis, text_Font).Width - 3, ZiSH);
            g.DrawLine(B_Table, ZiSX + 80, ZiSH + 16, 100, ZiSH + 16);
            //平均跟打  
            ZiSH += 18;
            g.DrawString("平均", text_Font, Brushes.DimGray, ZiSX + 10, ZiSH);
            zis = 字数格式化((int)PerTypeCount);
            g.DrawString(zis, text_Font, Brushes.DimGray, 190 - g.MeasureString(zis, text_Font).Width - 3, ZiSH);
            g.DrawLine(B_Table, ZiSX + 80, ZiSH + 16, 100, ZiSH + 16);
            //总字数 记录+总
            //平均跟打
            ZiSH += 18;
            g.DrawString("总共", text_Font, Brushes.DimGray, ZiSX + 10, ZiSH);
            zis = 字数格式化(Glob.TextLenAll);
            g.DrawString(zis, text_Font, Brushes.DimGray, 190 - g.MeasureString(zis, text_Font).Width - 3, ZiSH);
            //g.DrawLine(B_Table, ZiSX + 80, ZiSH + 16, 100, ZiSH + 16);

            //外边框
            g.DrawRectangle(B_Table, 35, Pic_Bmp.Height - 80,151,52);
            //标题处理
            //底色
            
            string T = title.Replace(":","").Replace("：","");
            Font title_Font = new Font("Verdana",8,FontStyle.Bold);
            //判断是否为标题
            System.Text.RegularExpressions.Regex isTitle = new System.Text.RegularExpressions.Regex(@"\(\d+\)|<.+>");
            SizeF label_title = g.MeasureString("文章标题:",text_Font);
            string T_ = !isTitle.IsMatch(T) ? ((T.Length > 15) ? T.Substring(0, 15) : T) : "无标题";
            SizeF T_SizeF = g.MeasureString(T_,title_Font);
            //g.DrawRectangle(new Pen(Brushes.Tomato), 0, 0, label_title.Width + T_SizeF.Width + 5, 30);

            g.DrawString("文章标题:", text_Font, Brushes.DimGray, 0, 1);
            g.DrawString(T_,title_Font,Brushes.PaleVioletRed,label_title.Width,1);
            g.DrawString("当前日期: " + DateTime.Now.ToShortDateString(),text_Font,Brushes.DimGray,0,label_title.Height + 1);
            //尾标
            string text_ = Glob.Form + Glob.Ver + "(" + Glob.Instration.Trim() + ")";
            SizeF text_Size = g.MeasureString(text_, text_Font);
            int LastFlagHeight = (int)(Pic_Bmp.Height - text_Size.Height);
            g.DrawString(text_,text_Font,Brushes.DimGray,Pic_Bmp.Width - text_Size.Width,LastFlagHeight);
            //用户
            int user_Rect = 100;//半径
            g.FillPie(Brushes.YellowGreen, -user_Rect / 2, Pic_Bmp.Height - user_Rect/2, user_Rect, user_Rect, -90, 360);
            g.DrawString("跟打者", text_Font, Brushes.DimGray, 0, LastFlagHeight - text_Size.Height);
            //获取用户 如果没有设置则找 QQ号，如果未设置QQ号，则返回空
            string user = (Glob.PicName.Length > 0) ? Glob.PicName : (Glob.QQnumber.Length > 0) ? Glob.QQnumber : "";
            if (user.Length > 0)
                g.DrawString(user, new Font("Verdana", 8,FontStyle.Bold), Brushes.DimGray, 0, LastFlagHeight);
            else
            {
                g.DrawString("未设置", text_Font, Brushes.DimGray, 0, LastFlagHeight);
            }
            //边框
            g.DrawRectangle(new Pen(Brushes.DimGray),0,0,Pic_Bmp.Width - 1,Pic_Bmp.Height - 1);
            return Pic_Bmp;
        }

        /// <summary>
        /// 字数格式
        /// </summary>
        /// <param name="zis"></param>
        /// <returns></returns>
        private string 字数格式化(int zis) {
            if (zis > 9999)
                return Math.Round((double)zis / 10000, 1) + "万";
            else
                return zis.ToString();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
            //释放当前的图片资源
            Pic_Bmp = null;
        }
    }
}
