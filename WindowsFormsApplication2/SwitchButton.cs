using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication2
{
    public class SwitchButton:Label {
        //事件初始化
        public delegate void CheckedChange(object sender,EventArgs e);
        public event CheckedChange CChange;
        /// <summary>
        /// 初始化
        /// </summary>
        public SwitchButton() {
            //设置颜色
            this.BackColor = _BG;
            //设置格式
            this.AutoSize = false;
            //设置文本
            this.Text = "";
            //设置重绘
            this.Paint += new PaintEventHandler(SwitchButton_Paint);
            //设置点击效果
            this.Click += new EventHandler(SwitchButton_Click);
        }

        private bool _checked = false;
        /// <summary>
        /// 属性值
        /// </summary>
        public bool Checked {
            get { return _checked; }
            set { _checked = value; }
        }

        /// <summary>
        /// 两个值的设置
        /// </summary>
        private string _ValueA = "A";
        private string _ValueB = "B";
        public string ValueA { get { return _ValueA; } set { _ValueA = value; } }
        public string ValueB { get { return _ValueB; } set { _ValueB = value; } }

        /// <summary>
        /// 设置背景颜色
        /// </summary>
        private Color _BG = Color.DimGray;
        public Color BG { get { return _BG; } set { _BG = value; } }

        /// <summary>
        /// 设置前景颜色
        /// </summary>
        private Color _FC = Color.LightGray;
        public Color FC { get { return _FC; } set { _FC = value; } }

        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchButton_Paint(object sender, PaintEventArgs e)
        {
            Font F = new Font("微软雅黑",9);
            SizeF A = e.Graphics.MeasureString(ValueA,F);
            SizeF B = e.Graphics.MeasureString(ValueB, F);
            //由属性控制重绘
            if (_checked) //为真时
            {
                e.Graphics.FillRectangle(new SolidBrush(_FC),0,0,this.Width /2,this.Height);
                //左值
                e.Graphics.DrawString(ValueA, F, Brushes.White, 0, this.Height/2 - A.Height/2);
                //右值
                e.Graphics.DrawString(ValueB, F, Brushes.White, this.Width - e.Graphics.MeasureString(ValueB,F).Width, this.Height/2 - B.Height/2);
            }
            else { //为假时
                e.Graphics.FillRectangle(new SolidBrush(_FC), this.Width / 2, 0, this.Width, this.Height);
                //左值
                e.Graphics.DrawString(ValueA, F, Brushes.White, 0, this.Height / 2 - A.Height / 2);
                //右值
                e.Graphics.DrawString(ValueB, F, Brushes.White, this.Width - e.Graphics.MeasureString(ValueB, F).Width, this.Height/2 - B.Height/2);
            }
        }

        /// <summary>
        /// 点击时的重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchButton_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
            if (CChange != null)
                CChange(sender,new EventArgs());
            this.Invalidate();
        }

    }
}
