using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace WindowsFormsApplication2
{
    public partial class 精五成绩生成 : Form
    {
        Form1 frm;
        string GetGoal;
        Regex speed = new Regex(@"(?<=速度)\d+\.\d+");
        Regex jj = new Regex(@"(?<=击键)\d+\.\d+");
        Regex mc = new Regex(@"(?<=码长)\d+\.\d+");
        string us;
        string QQ;
        public 精五成绩生成(Form1 frm1,string Goal,string ustime,string qq)
        {
            frm = frm1;
            GetGoal = Goal;
            us = ustime;
            QQ = qq;
            InitializeComponent();
        }


        #region 限制输入
        private void 期数_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void 生成_Click(object sender, EventArgs e)
        {
            if (检查各项是否填写()) {
                string str = "";
                if (GetGoal.Length != 0) {
                    try
                    {
                        string ch = frm.精五验证(convert(速度.Text), convert(击键.Text), convert(码长.Text), (int)(double.Parse(用时.Text) * 1000.0), 秋秋号.Text);
                        Regex s = new Regex(@"第\d+段");
                        string Get = s.Replace(GetGoal, "第" + 期数.Text + "期精五门比赛文段");
                        str = Get.Remove(Get.IndexOf("校验:")) + "校验码:" + ch;
                        if (str.Contains("QQ:"))
                        {
                            Regex rQQ = new Regex(@"(?<=QQ:)\d+");
                            str = rQQ.Replace(str, 秋秋号.Text);
                        }
                        else
                        {
                            str += " QQ:" + 秋秋号.Text;
                        }
                    }
                    catch (Exception err) {
                        str = err.Message;
                    }
                }
                生成框.Text = str + Glob.Instration; ;
            }
        }

        int convert(string data) {
            return (int)(double.Parse(data));
        }
        bool 检查各项是否填写() {
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                {
                    if ((item as TextBox).TextLength == 0)
                    {
                        生成框.Text = (item as TextBox).Name + "未填写！\n";
                        return false;
                    }
                }
            }
            return true;
        }

        //载文刚刚打完的成绩
        private void 精五成绩生成_Load(object sender, EventArgs e)
        {
            if (GetGoal.Length != 0) {
                速度.Text = speed.Match(GetGoal).ToString();
                击键.Text = jj.Match(GetGoal).ToString();
                码长.Text = mc.Match(GetGoal).ToString();
                用时.Text = us;
                秋秋号.Text = QQ;
            }
        }

        private void 复制_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(生成框.Text);
            }
            catch {
                MessageBox.Show("复制失败！");
            }
        }

        private void 发送_Click(object sender, EventArgs e)
        {
            frm.sendtext(生成框.Text);
        }

        private void 关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
