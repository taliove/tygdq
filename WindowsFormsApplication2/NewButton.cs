using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication2
{
    public class NewButton:Label
    {
        public NewButton() {
            this.BackColor = _defaultBGColor;
            this.MouseEnter += new EventHandler(NewButton_MouseEnter);
            this.MouseLeave += new EventHandler(NewButton_MouseLeave);
            this.MouseDown += new MouseEventHandler(NewButton_MouseDown);
            this.MouseUp += new MouseEventHandler(NewButton_MouseUp);
            
        }
        
        private ContentAlignment _SS = ContentAlignment.MiddleRight;
        /// <summary>
        /// 方向
        /// </summary>
        public ContentAlignment SS {
            get { return _SS; }
            set { _SS = value; }
        }

        void NewButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.TextAlign = ContentAlignment.MiddleCenter;
        }

        void NewButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.TextAlign = _SS;
        }

        void NewButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _defaultBGColor;
        }

        void NewButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = _EnterBGColor;
        }
        /// <summary>
        /// 默认的背景色
        /// </summary>
        private Color _defaultBGColor = Color.Gray;
        public Color 默认背景色 {
            get {
                return _defaultBGColor;
            }
            set {
                _defaultBGColor = value;
            }
        }

        /// <summary>
        /// 移动时背景色
        /// </summary>
        private Color _EnterBGColor = Color.Red;
        public Color 进入背景色
        {
            get
            {
                return _EnterBGColor;
            }
            set
            {
                _EnterBGColor = value;
            }
        }

        
    }
}
