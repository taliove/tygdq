using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace WindowsFormsApplication2
{
    public partial class SpeedCheckOut : Form
    {
        Form1 frm;
        public string getText;
        private Regex findIndex;
        private Regex matchIndex = new Regex("单字|散文|小说|古文|新闻|政论|名言|笑话|短信");
        private List<int> getIndex = new List<int>();
        public SpeedCheckOut(Form1 frm1)
        {
            frm = frm1;
            InitializeComponent();
        }

        private void SpeedCheckOut_Load(object sender, EventArgs e)
        {
            Get(false);
        }

        private void Get(bool r) { //r 用来控制是否显示提示
            try
            {
                int c;
                int count = int.TryParse(this.tbxC.Text, out c) ? c : 2;
                getText = frm.richTextBox1.Text;
                findIndex = new Regex(this.tbxP.Text);
                MatchCollection m = findIndex.Matches(getText);
                if (m.Count > 0)
                {
                    getIndex.Clear();
                    this.checkedListBox1.Items.Clear();
                    for (int i = 0; i < m.Count; i++)
                    {
                        int index = (m[i].Index - count < 0) ? 0 : (m[i].Index - count);
                        string g = getText.Substring(index, count);
                        bool check = matchIndex.IsMatch(g);
                        getIndex.Add(index - 1);//跟列表同步写入
                        this.checkedListBox1.Items.Add(g, check);
                    }
                }
                else {
                    if (r)
                        MessageBox.Show("啥都没有找到。要不换个？");
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void btnReget_Click(object sender, EventArgs e)
        {
            Get(true);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int selectC = this.checkedListBox1.CheckedItems.Count;
            //MessageBox.Show(this.checkedListBox1.CheckedIndices[0] + "\n" + getIndex[2]);
            if (selectC > 0 && getIndex.Count > 0 && selectC <= 10)
            {
                int idx;
                int idc = 0;//设置了几个
                for (int i = 0; i < selectC; i++)
                {
                    idx = getIndex[this.checkedListBox1.CheckedIndices[i]];
                    if (idx > 0)
                    {
                        frm.setSpeedPoint(idx);
                        idc++;
                    }
                }
                if (idc > 0)
                {
                    //MessageBox.Show("设置完成！共" + selectC + "个测速点！\n(注：第一个测速点不显示；测速点以浅灰底色显示。)");
                    frm.lblspeedcheck.Text = "测速" + idc;
                    this.Close();
                }
                else {
                    MessageBox.Show("啥都没有，怎么设置吖？亲？");
                }
                
            }
            else {
                if (selectC > 10)
                    MessageBox.Show("最多只能设置10个测速点！");
                else
                    MessageBox.Show("啥都没有，怎么设置吖？亲？");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(System.IntPtr ptr, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        //移动窗口
        private void SpeedCheckOut_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
