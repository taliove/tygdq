using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication2
{
    /// <summary>
    /// 跟据已经分好的分词信息，给RichEditBox 添加下划线显示。
    /// 目前因为是用的动态增加控件的方式来模拟画线，所以性能上有点慢。
    /// Create by hwjmyz,2012-10-18
    /// </summary>
    public class RichEditBoxLineRender : RichTextBox
    {
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;


        //停止控件的重绘 
        private void BeginPaint()
        {
            SendMessage(_rich.Handle, WM_SETREDRAW, 0, IntPtr.Zero);

        }

        //允许控件重绘. 
        private void EndPaint()
        {
            SendMessage(_rich.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            _rich.Refresh();
        }

        /// <summary>
        /// 为了以后显示的线能够区分词的开始、结尾、中间，所以定义 LineMode
        /// </summary>
        public enum LineMode
        {
            Start = 0,   //词的开始
            Middle = 1, //词的中间
            End = 2,    //词的结束
            Single = 3, //单个字的词
        };

        public class LineInfo
        {
            public int index;//对应的开始字符序号
            public Color color;
            public int left;
            public int width;
            public int top;
            public int len;
        };

        private IList<WordInfo> _wordInfos; //分词的信息数组或者List
        private RichTextBox _rich = null;

        private List<LineInfo> _lineInfos = new List<LineInfo>();//用以记录当前要划的各下划线列表
        private int _currIndex = 0; //当前打到第几个字了，避免之前的下划线颜色出现问题

        private List<Label> _floatControls = new List<Label>(); //用于显示下划线的浮动控件组
        private int _floatIndex = 0;    //当前浮动控件组的已使用个数 （为了提高效率，控件组创建后作为池子放着，不会删除掉）
        public SizeF _charSize = new Size(); //一个字的尺寸,为了统一，并且提高效率，使用RichEditBox本身的Font来计算

        private Color _bkRightColor; //已打过文字的背景色

        public RichEditBoxLineRender()
        {
        }

        /// <summary>
        /// 关闭标记
        /// </summary>
        public void ClearLabel()
        {
            foreach (Label L in _floatControls)
            {
                L.Visible = false;
            }
        }
        /// <summary>
        /// 设置分词信息、RichTextBox、已打过的正确文字的背景色
        /// </summary>
        public void Init(IList<WordInfo> wordInfos, RichTextBox rich, Color bkRightColor)
        {
            _wordInfos = wordInfos;
            _rich = rich;
            _bkRightColor = bkRightColor;
        }

        /// <summary>
        /// 根据已设置的分词信息，进行下划线的显示
        /// </summary>
        public void Render()
        {
            if (_rich == null) return;

            //先根据字体大小取有代表性的单个字的宽、高
            using (System.Drawing.Graphics graph = _rich.CreateGraphics())
            {
                _charSize = graph.MeasureString("测", _rich.Font);
            }

            _lineInfos.Clear();
            foreach (WordInfo info in _wordInfos)
            {
                AddLine_OneWord(_lineInfos, info.start, info.end, info.color,info.len);
            }

            ShowLines(_lineInfos);
        }

        /// <summary>
        /// 设置当前打到第几个字了，以便能正确的显示之前的字对应的下划线颜色（不出现白的条条）
        /// </summary>
        public void SetCurrIndex(int currIndex)
        {
            _currIndex = currIndex;
            for (int i = 0; i < _floatIndex; i++)
            {
                _floatControls[i].Refresh();//强制重画
            }
        }

        /// <summary>
        /// 绘制下划线
        /// </summary>
        private void ShowLines(List<LineInfo> lineInfos)
        {
            //BeginPaint();

            _floatIndex = 0;

            for (int i = 0; i < _floatControls.Count; i++)
            {
                _floatControls[i].Visible = false;
            }

            foreach (int top in lineInfos.Select(o => o.top).Distinct())
            {
                Label floatControl = GetFreeFloatControl();
                floatControl.Tag = lineInfos.Where(o => o.top == top).ToList(); //设置tab为需要划的相应线列表，在Paint的时候好去划它

                floatControl.Top = top;
                floatControl.Left = 0;
                floatControl.Height = 1;
                floatControl.Width = _rich.ClientSize.Width;
                floatControl.BackColor = _rich.BackColor;// Color.FromArgb(203, 238, 249);

                //floatControl.Visible = true;
            }

            for (int i = 0; i < _floatIndex; i++)
            {
                _floatControls[i].Visible = true;
            }

            if (_rich == null) return;
            _rich.SendToBack();
            //EndPaint();
        }

        /// <summary>
        /// 获取新的浮动控件（如果池子中已经有空闲的，那就直接使用，否则创建）
        /// </summary>
        /// <returns></returns>
        private Label GetFreeFloatControl()
        {

            Label floatControl;

            if (_floatControls.Count <= _floatIndex)
            {
                floatControl = new Label();
                _floatControls.Add(floatControl);
                _rich.Controls.Add(floatControl);
                floatControl.Paint += new PaintEventHandler(floatControl_Paint);
            }
            else
            {
                floatControl = _floatControls[_floatIndex];

            }

            _floatIndex++;

            return floatControl;
        }

        /// <summary>
        /// 绘制线，sender.tag 是 List<LineInfo>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void floatControl_Paint(object sender, PaintEventArgs e)
        {
            Label ctl = sender as Label;
            try
            {
                List<LineInfo> lineInfos = (sender as Label).Tag as List<LineInfo>;
                if (lineInfos.Count <= 0) return;

                //绘制背景
                PaintCtlBack(e, ctl, lineInfos);

                //绘制每个字符
                foreach (LineInfo li in lineInfos)
                {
                    using (var pen = new Pen(li.color,2))
                    {
                        if (li.len != 4 && li.len != 0) //非全码用虚线显示
                        {
                            pen.DashStyle = DashStyle.Dash;
                            pen.DashPattern = new float[] {2, 2};
                        }
                        //if (li.len > 0)
                        //{
                        //    int j = 0;
                        //    var color = li.color;
                        //    for (int i = 0; i < li.len; i++)
                        //    {
                        //        if (i > 0) color = Color.LightGray;
                        //        e.Graphics.DrawLine(new Pen(color), li.left - 4, j, li.left + li.width, j); //注：因为纵向的坐标，floatControl已经做了偏离了，所以绘制线的时候top=0
                        //        j += 2;
                        //    }
                        //}
                        //else
                        //{
                            e.Graphics.DrawLine(pen, li.left - 4, 0, li.left + li.width, 0); //注：因为纵向的坐标，floatControl已经做了偏离了，所以绘制线的时候top=0
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private void PaintCtlBack(PaintEventArgs e, Label ctl, List<LineInfo> lineInfos)
        {
            //根据 _currIndex 来绘制背景，在_currIndex之前的，按打过的背景色来绘制
            int ctlLine = _rich.GetLineFromCharIndex(lineInfos[0].index);
            int curLine = _rich.GetLineFromCharIndex(_currIndex);

            if (ctlLine < curLine)
            {
                //整行都已经打过，背景色直接设置为已打过
                using (Pen pen = new Pen(_bkRightColor,1))
                {
                    e.Graphics.DrawLine(pen, 1, 0, ctl.Width, 0); //注：因为纵向的坐标，floatControl已经做了偏离了，所以绘制线的时候top=0
                }
            }
            else if (ctlLine > curLine)
            {
                //整行都已经未打，背景色直接设置为未打过的
                using (Pen pen = new Pen(_rich.BackColor,1))
                {
                    e.Graphics.DrawLine(pen, 1, 0, ctl.Width, 0); //注：因为纵向的坐标，floatControl已经做了偏离了，所以绘制线的时候top=0
                }
            }
            else
            {
                //正在打该行，因此先绘制已打过的部分，再绘制未打过的部分
                Point pt = _rich.GetPositionFromCharIndex(_currIndex);
                //pt.X += (int)(_charSize.Width);

                using (Pen pen = new Pen(_bkRightColor,2))
                {
                    e.Graphics.DrawLine(pen, 1, 0, pt.X, 0); //注：因为纵向的坐标，floatControl已经做了偏离了，所以绘制线的时候top=0
                }

                using (Pen pen = new Pen(_rich.BackColor,2))
                {
                    e.Graphics.DrawLine(pen, pt.X, 0, ctl.Width, 0); //注：因为纵向的坐标，floatControl已经做了偏离了，所以绘制线的时候top=0
                }
            }
        }

        /// <summary>
        /// 对一个词添加下划线
        /// </summary>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        private void AddLine_OneWord(List<LineInfo> lineInfos, int indexStart, int indexEnd, Color color,int len)
        {
            if (indexEnd - indexStart == 0)
            {
                AddLine_OneChar(lineInfos, indexStart, LineMode.Single, color,len);
            }


            AddLine_OneChar(lineInfos, indexStart, LineMode.Start, color,len);
            for (int i = indexStart + 1; i < indexEnd; i++)
            {

                AddLine_OneChar(lineInfos, i, LineMode.Middle, color,len);
            }
            AddLine_OneChar(lineInfos, indexEnd, LineMode.End, color,len);

        }

        /// <summary>
        /// 对一个字符添加下划线，其中 mode 表示是词的开始（0）、中间（1）、结束（2）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mode"></param>
        private void AddLine_OneChar(List<LineInfo> lineInfos, int index, LineMode mode, Color color,int len)
        {
            try
            {
                SizeF size = _charSize;
                Point pt = _rich.GetPositionFromCharIndex(index); //当前字的位置
                if (pt.X < 0 || pt.Y < 0 || (pt.Y + size.Height) > _rich.Height)
                {
                    //位置不完整的不予显示
                    return;
                }

                int left, top, width;

                left = pt.X;
                top = pt.Y + (int)size.Height + 1;
                width = Math.Min((int)size.Width, _rich.ClientRectangle.Width - left);


                //以下是示例，可以对开始、截止不同的地方给断点
                int space = width / 5;
                if (mode == LineMode.Start)
                {
                    left += space;
                    width -= space;
                }
                else if (mode == LineMode.End)
                {
                    left -= space;
                    width -= space;
                }
                else if (mode == LineMode.Single)
                {
                    left += space;
                    width -= space * 2;
                }

                LineInfo line = new LineInfo();
                line.index = index;
                line.color = color;
                line.width = width;
                line.top = top;
                line.left = left;
                line.len = len;
                lineInfos.Add(line);
            }
            catch
            {
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        const int WM_MOUSEWHEEL = 0x020A;
        const int EM_SETZOOM = 0x04E1;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_MOUSEWHEEL)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    SendMessage(this.Handle, EM_SETZOOM, IntPtr.Zero, IntPtr.Zero);
                    //Glob.font_1 = this.Font;
                }
            }
        }
    }
}

