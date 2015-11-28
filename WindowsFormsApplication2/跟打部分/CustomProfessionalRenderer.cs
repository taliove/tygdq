using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication2
{
    public class CustomProfessionalRenderer:ToolStripProfessionalRenderer
    {
        private Color _color = Color.Red;
        public CustomProfessionalRenderer():base()
        {
        }
        public CustomProfessionalRenderer(Color color):base()
        {
            _color = color;
        }

        //获取圆角矩形区域  radius=直径
        //画鼠标经过时
        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
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

        //渲染背景 包括menustrip背景 toolstripDropDown背景
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;//抗锯齿
            Rectangle bounds = e.AffectedBounds;
            LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, toolStrip.Height), Color.FromArgb(255,Color.White), Color.FromArgb(150,_color));
            if (toolStrip is MenuStrip)
            {
                //由menuStrip的Paint方法定义 这里不做操作
            }
            else if (toolStrip is ToolStripDropDown)
            {
                int diameter = 1;//直径
                GraphicsPath path = new GraphicsPath();
                Rectangle rect = new Rectangle(Point.Empty, toolStrip.Size);
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                
                path.AddLine(0, 0, 10, 0);
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
                toolStrip.Region = new Region(path);
                g.FillPath(lgbrush, path);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        //渲染边框 不绘制边框
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //不调用基类的方法 屏蔽掉该方法 去掉边框
        }

        //渲染箭头 更改箭头颜色
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Color.DimGray;
            base.OnRenderArrow(e);
        }

        //渲染项 不调用基类同名方法
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            ToolStripItem item = e.Item;
            ToolStrip toolstrip = e.ToolStrip;
            
            
            //渲染顶级项
            if (toolstrip is MenuStrip)
            {
                LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, item.Height), Color.FromArgb(100, Color.White), Color.FromArgb(0, Color.White));
                SolidBrush brush=new SolidBrush(Color.FromArgb(255,Color.White));
                if (e.Item.Selected)
                {
                    //顶级
                    GraphicsPath gp = GetRoundedRectPath(new Rectangle(new Point(0, 0), item.Size), 1);
                    g.FillPath(lgbrush, gp);
                    g.DrawRectangle(new Pen(Color.LightGray, 1f), new Rectangle(Point.Empty, new Size(item.Size.Width - 1, item.Size.Height - 1)));
                }
                if (item.Pressed)
                {
                    ////创建上面左右2圆角的矩形路径
                    //GraphicsPath path = new GraphicsPath();
                    //int diameter = 8;
                    //Rectangle rect = new Rectangle(Point.Empty, item.Size);
                    //Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                    //// 左上角
                    //path.AddArc(arcRect, 180, 90);
                    //// 右上角
                    //arcRect.X = rect.Right - diameter;
                    //path.AddArc(arcRect, 270, 90);
                    //path.AddLine(new Point(rect.Width, rect.Height), new Point(0, rect.Height));
                    //path.CloseFigure();
                    ////填充路径
                    //g.FillPath(brush, path);
                    //g.DrawString(e.Item.Text,new Font("宋体",9f),Brushes.Black,2,5);
                    g.DrawRectangle(new Pen(Color.LightGray,1f),new Rectangle(Point.Empty,new Size(item.Size.Width - 1,item.Size.Height - 1)));
                }
            }
            //渲染下拉项
            else if (toolstrip is ToolStripDropDown)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(item.Width, 0), Color.FromArgb(200, Color.DimGray), Color.FromArgb(0, Color.White));
                if (item.Selected)
                {
                    GraphicsPath gp = GetRoundedRectPath(new Rectangle(0, 0, item.Width, item.Height), 1);
                    g.FillPath(lgbrush, gp);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

       protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
       {
           Graphics g = e.Graphics;
           LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(e.Item.Width, 0), Color.DimGray, Color.FromArgb(0, Color.DimGray));
           g.FillRectangle(lgbrush, new Rectangle(3,e.Item.Height/2,e.Item.Width,1));
           //base.OnRenderSeparator(e);
       }

       //渲染图片区域 下拉菜单左边的图片区域
       protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
       {
           //base.OnRenderImageMargin(e);
           //屏蔽掉左边图片竖条
       }
    }
}
