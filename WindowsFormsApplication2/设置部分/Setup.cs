using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions; //正则

namespace WindowsFormsApplication2
{
    public partial class TSetup : Form
    {
        Form1 frm;
        public Font fo1, fo2;
        public TSetup(Form1 frm1)
        {
            frm = frm1;
            InitializeComponent();
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            ShowInputLan();//显示所有的安装的输入法
            this.comboBox1.SelectedItem = IniRead("输入法", "惯用设置", "未设置");
            string gq = IniRead("个签", "标志", "0");
            if (gq != "0") //个签初始化
            {
                this.checkBox1.Checked = true;
            }
            else
            {
                this.checkBox1.Checked = false;
            }
            this.textBox1.Text = IniRead("个签", "签名", "");
            //延时初始化
            int delay = int.Parse(IniRead("发送", "延时", "50"));
            this.trackBar1.Value = delay;
            this.label19.Text = delay.ToString();

            //输入法签名初始化
            string srfsave = IniRead("输入法", "标志", "0");
            if (srfsave != "0")
            {
                this.checkBox3.Checked = true;
            }
            else
            {
                this.checkBox3.Checked = false;
            }
            this.textBox2.Text = IniRead("输入法", "签名", "");
            //排序顺序初始化
            sortsend();
            //载入字体
            FontConverter fc = new FontConverter();
            //            fo1 = (Font)fc.ConvertFromString(IniRead("外观", "对照区字体", "宋体, 12pt"));//todo 原为14.25f 根据命需求而改
            //            fo2 = (Font)fc.ConvertFromString(IniRead("外观", "跟打区字体", "宋体, 12pt"));
            fo1 = (Font)fc.ConvertFromString("宋体, 12pt");//todo 原为14.25f 根据命需求而改
            fo2 = (Font)fc.ConvertFromString("宋体, 12pt");
            this.button3.Text = fo1.FontFamily.GetName(0) + " - " + fo1.Size;
            this.button4.Text = fo2.FontFamily.GetName(0) + " - " + fo2.Size;
            //速度计初始化
            _Ini setupini = new _Ini("Ttyping.ty");
            //各个外观配置初始化
            buttoncolor1.BackColor = frm.richTextBox1.BackColor; //对照区底色
            buttoncolor2.BackColor = frm.textBoxEx1.BackColor; //跟打区底色
            this.pictureBoxRight.BackColor = Glob.Right;
            this.pictureBoxFalse.BackColor = Glob.False;

            //是否发送初始化
            this.checkBox16.Checked = Glob.sendOrNo;
            this.checkBoxGDQAction.Checked = bool.Parse(IniRead("发送", "激活", "false"));
            //载入初始化
            string pretext = IniRead("载入", "前导", "-----");
            string preduan = IniRead("载入", "段标", "第xx段");
            bool ison = bool.Parse(IniRead("载入", "开启", "false"));
            this.checkBox19.Checked = ison;
            this.textBoxPreText.Text = pretext;
            this.textBoxDuan.Text = preduan;
            bool get = bool.Parse(IniRead("载入", "方式", "false"));
            if (get)
            {
                this.radioButtonTab.Checked = true;
            }
            else
            {
                this.radioButton4.Checked = true;
            }
            //QQ
            this.checkBox21.Checked = bool.Parse(IniRead("发送", "QQSta", "false"));
            this.textBoxQQ.Text = IniRead("发送", "QQ", "");
            //曲线
            this.checkBox22.Checked = Glob.isShowSpline;
            //停止时间初始化
            int StopTime = int.Parse(IniRead("控制", "停止", "1"));
            if (StopTime < 1 || StopTime > 10)
            {
                StopTime = 1;
            }
            this.trackBar2.Value = StopTime;
            this.label17.Text = StopTime + "分";
            //极简设置
            this.checkBox23.Checked = Glob.simpleMoudle;
            this.textBox4.Text = Glob.simpleSplite;
            this.checkBox28.Checked = bool.Parse(IniRead("控制", "不显示即时", "False"));
            this.checkBox30.Checked = bool.Parse(IniRead("发送", "是否速度限制", "False"));
            this.numericUpDown1.Value = decimal.Parse(IniRead("发送", "速度限制", "0.00"));
            this.tbxName.Text = IniRead("发送", "昵称", this.tbxName.Text);
            //bool c;
            //this.checkBox31.Checked = bool.TryParse(IniRead("控制", "自动获取", "True"), out c) ? c : true;
        }

        public string IniRead(string section, string key, string def)
        { //ini的快捷读取
            _Ini sing = new _Ini("Ttyping.ty");
            return sing.IniReadValue(section, key, def);
        }

        public void ShowInputLan()
        {
            InputLanguageCollection iLc = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage iL in iLc)
            {
                this.comboBox1.Items.Add(iL.LayoutName);
            }
            int InputCount = this.comboBox1.SelectedIndex;
            if (InputCount != 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Ini Setupini = new _Ini("Ttyping.ty");
            string srf = Setupini.IniReadValue("输入法", "惯用设置", "0");
            if (srf != this.comboBox1.Text)
            {
                Setupini.IniWriteValue("输入法", "惯用设置", this.comboBox1.Text);
                Glob.InstraSrf = this.comboBox1.Text;
            }
            gQ();//保存个签
            //保存延时
            Setupini.IniWriteValue("发送", "延时", this.trackBar1.Value.ToString());
            //保存QQ
            Setupini.IniWriteValue("发送", "QQ", this.textBoxQQ.Text);
            Setupini.IniWriteValue("发送", "QQSta", this.checkBox21.Checked.ToString());
            //保存输入法签名
            srfSave();
            //颜色设置 todo 根据命需求而改
            //            Setupini.IniWriteValue("外观","对照区颜色",buttoncolor1.BackColor.ToArgb().ToString());
            //            Glob.r1Back = buttoncolor1.BackColor;
            //            Setupini.IniWriteValue("外观", "跟打区颜色", buttoncolor2.BackColor.ToArgb().ToString());
            //            Setupini.IniWriteValue("外观", "打对颜色", this.pictureBoxRight.BackColor.ToArgb().ToString());
            //            Glob.Right = this.pictureBoxRight.BackColor;
            //            Setupini.IniWriteValue("外观", "打错颜色", this.pictureBoxFalse.BackColor.ToArgb().ToString());
            //            Glob.Right = this.pictureBoxFalse.BackColor;
            //            //字体设置
            //            FontConverter fc = new FontConverter();
            //            Setupini.IniWriteValue("外观", "对照区字体", fc.ConvertToInvariantString(fo1));
            //            Setupini.IniWriteValue("外观", "跟打区字体", fc.ConvertToInvariantString(fo2));
            //Point a1 = frm.richTextBox1.GetPositionFromCharIndex(1);
            //Point a2 = frm.richTextBox1.GetPositionFromCharIndex(frm.richTextBox1.GetFirstCharIndexFromLine(1));
            frm.richTextBox1.Font = fo1;
            frm.textBoxEx1.Font = fo2;
            //前导
            if (checkBox19.Checked)
            {
                if (this.textBoxDuan.Text.Contains("xx"))
                {
                    Setupini.IniWriteValue("载入", "前导", this.textBoxPreText.Text);
                    Setupini.IniWriteValue("载入", "段标", this.textBoxDuan.Text);
                    Setupini.IniWriteValue("载入", "开启", this.checkBox19.Checked.ToString());
                }
                else
                {
                    MessageBox.Show("段标输入错误！未保存！", "警告");
                    return;
                }
            }
            else
            {
                Setupini.IniWriteValue("载入", "开启", this.checkBox19.Checked.ToString());
            }
            //跟打完后 是否 激活
            if (checkBoxGDQAction.Checked)
            {
                Glob.GDQActon = true;
                Setupini.IniWriteValue("发送", "激活", "true");
            }
            else
            {
                Glob.GDQActon = false;
                Setupini.IniWriteValue("发送", "激活", "false");
            }
            //是否显示曲线
            if (checkBox22.Checked)
            {
                frm.splitContainer4.Panel1Collapsed = true;
                Setupini.IniWriteValue("拖动条", "曲线", "true");
            }
            else
            {
                frm.splitContainer4.Panel1Collapsed = false;
                Setupini.IniWriteValue("拖动条", "曲线", "false");
            }

            //停止时间
            Setupini.IniWriteValue("控制", "停止", this.trackBar2.Value.ToString());
            Glob.StopUse = this.trackBar2.Value;
            frm.toolTip1.SetToolTip(frm.lblAutoReType, "跟打停止时间，大于" + this.trackBar2.Value + "分钟时自动停止跟打");

            //速度限制
            Setupini.IniWriteValue("发送", "速度限制", this.numericUpDown1.Value.ToString());
            Glob.速度限制 = (double)this.numericUpDown1.Value;
            Glob.是否速度限制 = this.checkBox30.Checked;
            if (Glob.是否速度限制)
            {
                Glob.是否速度限制 = true;
                Setupini.IniWriteValue("发送", "是否速度限制", "True");
                frm.toolStripBtnLS.ForeColor = Color.White;
                string tips = frm.toolStripBtnLS.ToolTipText;
                frm.toolStripBtnLS.ToolTipText = tips.Remove(tips.IndexOf('：') + 1) + Glob.速度限制;
            }
            else
            {
                Glob.是否速度限制 = false;
                Setupini.IniWriteValue("发送", "是否速度限制", "False");
                frm.toolStripBtnLS.ForeColor = Color.Silver;
            }
            //极简模式
            Setupini.IniWriteValue("发送", "状态", this.checkBox23.Checked.ToString());
            Setupini.IniWriteValue("发送", "分隔符", this.textBox4.Text);
            Glob.simpleMoudle = this.checkBox23.Checked;
            Glob.simpleSplite = this.textBox4.Text;
            frm.toolStripButton2.Checked = this.checkBox23.Checked;
            if (!saveSort())
            {
                MessageBox.Show(this, "含有错误排序字符，请重新检查！", "添雨跟打器排序提示");
                return;
            }
            else
            {
                Setupini.IniWriteValue("发送", "顺序", textBox3.Text);
            }
            //跟打过程中不显示即时数据
            if (this.checkBox28.Checked)
            {
                Setupini.IniWriteValue("控制", "不显示即时", "True");
                Glob.notShowjs = true;
            }
            else
            {
                Setupini.IniWriteValue("控制", "不显示即时", "False");
                Glob.notShowjs = false;
            }

            if (this.checkBox1.Checked)
            {
                Glob.InstraPre_ = "1";
            }
            else
            {
                Glob.InstraPre_ = "0";
            }
            Glob.InstraPre = this.textBox1.Text; //个签
            Glob.InstraSrf = this.textBox2.Text; //输入法签名
            Glob.InstraSrf_ = IniRead("输入法", "标志", "0");
            Glob.binput = true;//输入法修改
            Glob.DelaySend = int.Parse(IniRead("发送", "延时", "50"));

            Glob.sortSend = this.textBox3.Text;
            Glob.Right = pictureBoxRight.BackColor;
            Glob.False = pictureBoxFalse.BackColor;
            frm.richTextBox1.BackColor = this.buttoncolor1.BackColor;
            frm.textBoxEx1.BackColor = this.buttoncolor2.BackColor;
            if (checkBox19.Checked)
            {
                Glob.PreText = this.textBoxPreText.Text;
                Glob.PreDuan = this.textBoxDuan.Text.Replace("xx", @"\d+");
                Glob.isZdy = true;
            }
            else
            {
                Glob.PreText = "-----";
                Glob.PreDuan = @"第\d+段";
            }
            Glob.isQQ = this.checkBox21.Checked;
            Glob.QQnumber = this.textBoxQQ.Text;

            //图片成绩发送昵称
            Setupini.IniWriteValue("发送", "昵称", this.tbxName.Text);
            Glob.PicName = this.tbxName.Text;
            // Setupini.IniWriteValue("控制", "自动获取", this.checkBox31.Checked.ToString());
            if (File.Exists("Ttyping.ty"))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("文件丢失！");
            }
        }

        public void gQ()
        {
            _Ini Setupini = new _Ini("Ttyping.ty");
            if (this.checkBox1.Checked)
            {
                if (this.textBox1.Text != "")
                {
                    Setupini.IniWriteValue("个签", "签名", this.textBox1.Text);
                    Setupini.IniWriteValue("个签", "标志", "1");
                }
                else
                {
                    Setupini.IniWriteValue("个签", "标志", "0"); // 0 表示未设置
                }
            }
            else
            {
                Setupini.IniWriteValue("个签", "标志", "0"); // 0 表示未设置
            }
        }//个签

        public void srfSave()
        {
            _Ini Setupini = new _Ini("Ttyping.ty");
            if (this.checkBox3.Checked)
            {
                if (this.textBox2.Text != "")
                {
                    Setupini.IniWriteValue("输入法", "签名", this.textBox2.Text);
                    Setupini.IniWriteValue("输入法", "标志", "1");
                }
                else
                {
                    Setupini.IniWriteValue("输入法", "标志", "0");
                }
            }
            else
            {
                Setupini.IniWriteValue("输入法", "标志", "0");
            }
        } //输入法
        //排序顺序
        public void sortsend()
        {
            string sort = IniRead("发送", "顺序", "ABCVDTSEFULGNOPRQ");
            textBox3.Text = sort;
            try
            {
                char[] g = sort.ToArray();
                checkallout();//清空所有选择
                for (int i = 0; i < g.Length; i++)
                {
                    testit(g[i]); //根据当前输入 选中 或者取消选中
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBox1.ReadOnly = false;
                this.textBox1.BackColor = Color.White;
            }
            else
            {
                this.textBox1.ReadOnly = true;
                this.textBox1.BackColor = Color.Gray;
            }
        }
        private void CloseRefresh(object sender, FormClosedEventArgs e)
        {

            //Glob.isAutoGet = this.checkBox31.Checked;
        } //关闭时 更新

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label19.Text = this.trackBar1.Value.ToString();
        }

        private void OnlyInputMath(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxQQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void buttonfont1_Click(object sender, EventArgs e)
        {

        }


        private void checkBox3_CheckedChanged(object sender, EventArgs e) //输入法签名开关
        {
            if (checkBox3.Checked)
            {
                textBox2.BackColor = Color.White;
                textBox2.ReadOnly = false;
            }
            else
            {
                textBox2.BackColor = Color.Gray;
                textBox2.ReadOnly = true;
            }
        }

        private void buttonfont1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttoncolor1_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = buttoncolor1.BackColor;
            if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.buttoncolor1.BackColor = colorDialog1.Color;
            }
        }

        private void buttoncolor2_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = buttoncolor2.BackColor;
            if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.buttoncolor2.BackColor = colorDialog1.Color;
            }
        }

        #region 发送排序
        private void textBox3Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3KDown(object sender, KeyEventArgs e)
        {

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            string get = "", output = "";
            foreach (var item in this.panel9.Controls)
            {
                if (item is CheckBox)
                {
                    if ((item as CheckBox).Checked)
                    { //如果是选中状态
                        get = get + (item as CheckBox).Text.Substring(3, 1);
                    }
                }
            }
            for (int i = get.Length; i > 0; i--)
            {
                output += get.Substring(i - 1, 1);
            }
            textBox3.Text = output;
        }

        private bool saveSort()
        {
            string get = textBox3.Text.ToUpper();
            textBox3.Text = get;
            char[] g = get.ToArray();
            string get2 = "";
            if (get != "")
            {
                checkallout();//清空所有选择
                for (int i = 0; i < g.Length; i++)
                {
                    get2 += getit(g[i]);
                    testit(g[i]); //根据当前输入 选中 或者取消选中
                }

                if (!get2.Contains("[错误]"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        void checkallout()
        {
            foreach (var item in this.panel9.Controls)
            {
                if (item is CheckBox)
                {
                    if ((item as CheckBox).Checked)
                    {
                        (item as CheckBox).Checked = false;
                    }
                }
            }
        } //清空所有选择
        void testit(char a)
        {
            foreach (var item in this.panel9.Controls)
            {
                if (item is CheckBox)
                {
                    string checktext = checkit(a);
                    if (checktext != "")
                    {
                        if ((item as CheckBox).Text == checktext)
                        {
                            if (!(item as CheckBox).Checked)
                            {
                                (item as CheckBox).Checked = true;
                            }
                            break;
                        }
                    }
                }
            }
        }
        string checkit(char a)
        {
            switch (a)
            {
                case 'A': return "速度[A]";
                case 'B': return "击键[B]";
                case 'C': return "码长[C]";
                case 'D': return "回改[D]";
                case 'E': return "错字[E]";
                case 'F': return "错情[F]";
                case 'G': return "字数[G]";
                case 'H': return "键数[H]";
                case 'I': return "用时[I]";
                case 'J': return "重打[J]";
                case 'K': return "峰值[K]";
                case 'L': return "打词[L]";
                case 'M': return "回率[M]";
                case 'N': return "停留[N]";
                case 'O': return "效率[O]";
                case 'P': return "验证[P]";
                case 'Q': return "撤销[Q]";
                case 'R': return "键法[R]";
                case 'S': return "退格[S]";
                case 'T': return "回车[T]";
                case 'U': return "选重[U]";
                case 'V': return "键准[V]";
                default: return "";
            }
        }
        string getit(char a)
        {//返回成绩
            switch (a)
            {
                case 'A': return "速度161.53 ";
                case 'B': return "击键8.38 ";
                case 'C': return "码长3.11 ";
                case 'D': return "回改0 ";
                case 'E': return "错字0 ";
                case 'F': return "错情：无 ";
                case 'G': return "字数30 ";
                case 'H': return "键数00 ";
                case 'I': return "用时00秒 ";
                case 'J': return "重打2 ";
                case 'K': return "峰值 ";
                case 'L': return "打词2 ";
                case 'M': return "回改率0.00% ";
                case 'N': return "停留[字]XX秒";
                case 'O': return "效率100%";
                case 'P': return "添雨验证:05555";
                case 'Q': return "撤销2";
                case 'R': return "键法";
                case 'S': return " 退格";
                case 'T': return " 回车";
                case 'U': return " 选重";
                case 'V': return " 键准";
                default: return "[错误] ";
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e) //全选
        {
            foreach (var item in this.panel9.Controls)
            {
                if (item is CheckBox)
                {
                    if (!(item as CheckBox).Checked)
                    {
                        (item as CheckBox).Checked = true;
                    }
                }
            }
        }

        private void buttonCleanAll_Click(object sender, EventArgs e)
        {
            foreach (var item in this.panel9.Controls)
            {
                if (item is CheckBox)
                {
                    if ((item as CheckBox).Checked)
                    {
                        (item as CheckBox).Checked = false;
                    }
                }
            }
        }
        //发送排序
        #endregion

        //字体

        private void button3_Click(object sender, EventArgs e)
        {
            this.fontDialog1.ShowEffects = false;
            this.fontDialog1.Font = fo1;
            if (this.fontDialog1.ShowDialog(this) == DialogResult.OK)
            {

                this.button3.Text = fontDialog1.Font.FontFamily.GetName(0) + " - " + fontDialog1.Font.Size;
                fo1 = fontDialog1.Font;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.fontDialog1.ShowEffects = false;
            this.fontDialog1.Font = fo2;
            if (this.fontDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.button4.Text = fontDialog1.Font.FontFamily.GetName(0) + " - " + fontDialog1.Font.Size;
                fo2 = fontDialog1.Font;
            }
        }

        #region 是否发送 消息不保存
        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                Glob.sendOrNo = true;
            }
            else
            {
                Glob.sendOrNo = false;
            }
        }
        #endregion

        #region 载入
        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                this.panelTextIn.Enabled = true;
                this.textBoxPreText.Focus();
            }
            else
            {
                this.panelTextIn.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.checkBox19.Checked = true;
            this.textBoxPreText.Text = "-----";
            this.textBoxDuan.Text = "第xx段";
        }
        #endregion

        //获取方式
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                _Ini Setupini = new _Ini("Ttyping.ty");
                Glob.getStyle = false;
                Setupini.IniWriteValue("载入", "方式", "false");
            }
        }

        private void radioButtonTab_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTab.Checked)
            {
                _Ini Setupini = new _Ini("Ttyping.ty");
                Glob.getStyle = true;
                Setupini.IniWriteValue("载入", "方式", "true");
            }
        }

        #region 验证
        private void button7_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = Clipboard.GetText();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.labelTF.Text = frm.checkTF(richTextBox2.Text);
        }
        #endregion

        //QQ
        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                this.textBoxQQ.BackColor = Color.White;
                this.textBoxQQ.ReadOnly = false;
            }
            else
            {
                this.textBoxQQ.BackColor = Color.Gray;
                this.textBoxQQ.ReadOnly = true;
            }
        }

        //颜色设置
        private void pictureBoxRight_DoubleClick(object sender, EventArgs e)
        {
            this.colorDialog1.Color = Glob.Right;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.pictureBoxRight.BackColor = colorDialog1.Color;
            }
        }

        private void pictureBoxFalse_DoubleClick(object sender, EventArgs e)
        {
            this.colorDialog1.Color = Glob.False;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.pictureBoxFalse.BackColor = colorDialog1.Color;
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            Glob.isShowSpline = this.checkBox22.Checked;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.label17.Text = trackBar2.Value + "分";
        }

        #region 极简模式
        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox23.Checked) //开
            {
                this.panel极简模式.Enabled = true;
                this.panel9.Enabled = false;
            }
            else
            {
                this.panel极简模式.Enabled = false;
                this.panel9.Enabled = true;
            }
        }

        private void simpleSort()
        {
            string thisSort = textBox3.Text; //排列依据
            string total = "88";
            string splite = " " + textBox4.Text + " ";//分隔符
            if (thisSort.Length != 0)
            {
                char[] sort = thisSort.ToArray();
                for (int i = 0; i < sort.Length; i++)
                {
                    total += splite + getit(sort[i]);
                }
            }
        }
        #endregion

        #region 各项顺利控制
        //速度
        private void checkBoxSpeed_Click(object sender, EventArgs e)
        {
            Change(checkBoxSpeed);
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            Change(checkBox4);
        }

        private void checkBox5_Click(object sender, EventArgs e)
        {
            Change(checkBox5);
        }

        private void checkBox6_Click(object sender, EventArgs e)
        {
            Change(checkBox6);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox7);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox8);
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox25);
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox9);
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox10);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox11);
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox12);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox2);
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox15);
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox26);
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox14);
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox17);
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox18);
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox13);
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox20);
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox24);
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            Change(checkBox27);
        }


        private void checkBox29_Click(object sender, EventArgs e)
        {
            Change(checkBox29);
        }

        private void Change(CheckBox C)
        {
            string w = C.Text.Substring(3, 1);
            if (C.Checked)
            {
                if (!textBox3.Text.Contains(w))
                {
                    this.textBox3.Text = this.textBox3.Text.Insert(this.textBox3.TextLength, w);
                }
            }
            else
            {
                if (textBox3.Text.Contains(w))
                {
                    this.textBox3.Text = this.textBox3.Text.Replace(w, "");
                }
            }
        }
        #endregion

        #region 速度限制发送
        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                this.numericUpDown1.Enabled = true;
            }
            else
            {
                this.numericUpDown1.Enabled = false;
            }
        }
        #endregion

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(System.IntPtr ptr, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void TSetup_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
