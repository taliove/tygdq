using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class MagneticMagnager
    {
        MagneticPosition Pos;//位置属性
        Form MainForm, ChildForm;
        //bool IsFirstPos;//是否第一次定位ChildForm子窗体
        public int step;//磁性子窗体ChildForm移动步长
        public Point LocationPt;//定位点
        delegate void LocationDel();//移动子窗体的委托
        int DesktopWidth = System.Windows.Forms.SystemInformation.WorkingArea.Width;
        int DesktopHeight = System.Windows.Forms.SystemInformation.WorkingArea.Height;

        //用时显示浮动信息的长度
        public Size LengthMessage = new Size(10,10);
        public MagneticMagnager(Form _MainForm, Form _ChildForm, MagneticPosition _pos)
        {
            //IsFirstPos = false;
            step = 20;
            MainForm = _MainForm;
            ChildForm = _ChildForm;
            Pos = _pos;
            MainForm.LocationChanged += new EventHandler(MainForm_LocationChanged);
            //ChildForm.LocationChanged += new EventHandler(ChildForm_LocationChanged);
            MainForm.SizeChanged += new EventHandler(MainForm_SizeChanged);
            ChildForm.SizeChanged += new EventHandler(ChildForm_SizeChanged);
            ChildForm.Load += new EventHandler(ChildForm_Load);
            MainForm.Load += new EventHandler(MainForm_Load);
        }
        void ChildForm_Load(object sender, EventArgs e)
        {
            GetLocation();
        }
        void MainForm_Load(object sender, EventArgs e)
        {
            GetLocation();
        }
        void MainForm_LocationChanged(object sender, EventArgs e)
        {
            GetLocation();
        }
        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            GetLocation();
        }
        void ChildForm_SizeChanged(object sender, EventArgs e)
        {
            GetLocation();
        }
        void GetLocation()//定位子窗体
        {
            if (ChildForm == null)
                return;
            //自动判断

            if (Pos != MagneticPosition.BottomUp)
            {
                if (ChildForm.Left < 0) Pos = MagneticPosition.Right;
                else
                {
                    if (MainForm.Left - ChildForm.Width >= 50) Pos = MagneticPosition.Left;
                }
                if (ChildForm.Right > DesktopWidth) Pos = MagneticPosition.Left;
            }
            //if (ChildForm.Top < 0) Pos = MagneticPosition.Bottom;
            //if (ChildForm.Bottom > DesktopHeight) Pos = MagneticPosition.Top;
            if (Pos == MagneticPosition.Left)
                LocationPt = new Point(MainForm.Left - ChildForm.Width, MainForm.Top);
            else if (Pos == MagneticPosition.Top)
                LocationPt = new Point(MainForm.Left, MainForm.Top - ChildForm.Height);
            else if (Pos == MagneticPosition.Right)
                LocationPt = new Point(MainForm.Right, MainForm.Top);
            else if (Pos == MagneticPosition.Bottom)
                LocationPt = new Point(MainForm.Left, MainForm.Bottom);
            else if (Pos == MagneticPosition.BottomUp)
            {
                int height;
                try
                {
                    var v = (Form1)MainForm;
                    height = MainForm.Top + v.splitContainer1.Location.Y + v.splitContainer1.SplitterDistance + 97 - LengthMessage.Height;
                }
                catch {
                    height = MainForm.Height - LengthMessage.Height - 30;
                }
                
                LocationPt = new Point(MainForm.Location.X + MainForm.Width / 2 - LengthMessage.Width/2, height);// 
            }
            ChildForm.Location = LocationPt;
        }
        void ChildForm_LocationChanged(object sender, EventArgs e)//当窗体位置移动后
        {
            //if (!IsFirstPos)
            //{
            //    IsFirstPos = true;
            //    return;
            //}
            OnMove();//委托
            //MainForm.BeginInvoke(del);//调用
        }

        void OnMove()//移动子窗体
        {
            int distance = Math.Abs(ChildForm.Right - MainForm.Left);
            if (distance <= 50 && distance > 0)
            {
                LocationPt = new Point(MainForm.Left - ChildForm.Width, MainForm.Top);
                ChildForm.Location = LocationPt;

            }
        }
    }
    public enum MagneticPosition//磁性窗体的位置属性
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3,
        BottomUp = 4
    }
}