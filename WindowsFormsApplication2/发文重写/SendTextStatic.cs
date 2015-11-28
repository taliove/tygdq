using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
namespace WindowsFormsApplication2
{
    public partial class SendTextStatic : Form
    {
        private Point MainPos;
        private Form1 frm;
        private int AllIniCount = 0;
        public SendTextStatic(Point pos,Form1 frm1)
        {
            MainPos = pos;
            frm = frm1;
            InitializeComponent();
        }

        private void SendTextStatic_Load(object sender, EventArgs e)
        {
            this.Location = new Point(MainPos.X - this.Width, MainPos.Y);
            //MessageBox.Show(MainPos + "\n" + this.Width);
            FillData();
            frm.ShowFlowText("发文已开启，全局F6 或者 Ctrl+R 发下一段");
        }

        //填充数据
        private void FillData() {
            tbxTitle.Text = NewSendText.标题;
            lblTextSources.Text = 文章来源;
            lblTextStyle.Text = 文章类型;
            lblSendCounted.Text = NewSendText.已发字数.ToString();//已发字数
            lblSendPCounted.Text = NewSendText.已发段数.ToString();
            tbxSendC.Text = NewSendText.字数.ToString();
            lblTotalCount.Text = NewSendText.文章全文.Length.ToString();
            tbxNowStart.Text = NewSendText.标记.ToString();//当前标记
            tbxNowStartCount.Text = NewSendText.起始段号.ToString();
            this.cbxSingleTest.Checked = NewSendText.是否独练;
            if (NewSendText.是否周期)
            {
                tbxSendTime.Text = NewSendText.周期.ToString();
            }
            else {
                tbxSendTime.Text = "-";
                lblNowTime.Text = "无周期手动";
                btnCancelTime.Text = "开";
            }
            AllIniCount = ReadKeys("发文配置").Count;
            this.lblAll.Text = AllIniCount.ToString();
            if (NewSendText.当前配置序列.Length != 0)
                this.lblNowIni.Text = NewSendText.当前配置序列;
        }

        private string 文章来源 {
            get {
                int i = NewSendText.文章来源;
                switch (i) {
                    case 0: return "自带文章";
                    case 1: return "自定义文章";
                    case 2: return "来自剪切板";
                    default: return "未知来源";
                }
            }
        }

        private string 文章类型 {
            get {
                string i = "";
                if (NewSendText.类型 == "单字")
                {
                    if (NewSendText.是否乱序) i = "单字/乱序";
                    else i = "单字/顺序";
                }
                else {
                    i = "文章";
                }
                return i;
            }
        }

        //剩余字数
        private void tbxNowStart_TextChanged(object sender, EventArgs e)
        {
            if (NewSendText.类型 == "单字")
            {
                if (NewSendText.是否乱序)
                {
                    if (NewSendText.乱序全段不重复)
                        lblLeastCount.Text = NewSendText.发文全文.Length.ToString();
                    else
                        lblLeastCount.Text = "乱序无限";
                }
                else {
                    int total = int.Parse(lblTotalCount.Text);
                    int now = int.Parse(tbxNowStart.Text);
                    lblLeastCount.Text = (total - now).ToString();
                }
            }
            else
            {
                int total = int.Parse(lblTotalCount.Text);
                int now = int.Parse(tbxNowStart.Text);
                lblLeastCount.Text = (total - now).ToString();
            }
        }

        //停止发文
        private void btnStop_Click(object sender, EventArgs e)
        {
            NewSendText.发文状态 = false;
            if (NewSendText.是否周期) frm.timerTSend.Stop();
            this.Close();
        }

        #region 修改字段
        private void btnCancelTime_TextChanged(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            if (text == "开")
                toolTip1.SetToolTip(btnCancelTime,"打开周期发文");
            else
                toolTip1.SetToolTip(btnCancelTime, "关闭周期发文");
        }
        //文章标题
        private void btnFixNowTitle_Click(object sender, EventArgs e)
        {
            string now = (sender as Button).Text;
            if (now == "修")
            {
                this.tbxTitle.ReadOnly = false;
                this.tbxTitle.BackColor = Color.White;
                this.tbxTitle.Focus();
                this.btnFixNowTitle.Text = "定";
            }
            else {
                this.tbxTitle.ReadOnly = true;
                this.tbxTitle.BackColor = Color.DarkGray;
                this.btnFixNowTitle.Text = "修";
                NewSendText.标题 = this.tbxTitle.Text;
            }
        }

        private void btnFixStart_Click(object sender, EventArgs e)
        {
            string now = (sender as Button).Text;
            if (now == "修")
            {
                this.tbxNowStart.ReadOnly = false;
                this.tbxNowStart.BackColor = Color.White;
                this.tbxNowStart.Focus();
                (sender as Button).Text = "定";
            }
            else
            {
                int get = int.Parse(this.tbxNowStart.Text);
                if (get + NewSendText.字数 <= NewSendText.文章全文.Length)
                {
                    this.tbxNowStart.ReadOnly = true;
                    this.tbxNowStart.BackColor = Color.DarkGray;
                    (sender as Button).Text = "修";
                    NewSendText.标记 = get;
                }
                else {
                    MessageBox.Show("定义超出总范围，请重设！");
                }
            }
        }

        private void btnOnceSC_Click(object sender, EventArgs e)
        {
            string now = (sender as Button).Text;
            if (now == "修")
            {
                this.tbxSendC.ReadOnly = false;
                this.tbxSendC.BackColor = Color.White;
                this.tbxSendC.Focus();
                (sender as Button).Text = "定";
            }
            else
            {
                int get = int.Parse(this.tbxSendC.Text);
                if (get + NewSendText.标记 <= NewSendText.文章全文.Length)
                {
                    this.tbxSendC.ReadOnly = true;
                    this.tbxSendC.BackColor = Color.DarkGray;
                    (sender as Button).Text = "修";
                    NewSendText.字数 = get;
                }
                else
                {
                    MessageBox.Show("定义超出总范围，请重设！");
                }
            }
        }

        private void btnSendTime_Click(object sender, EventArgs e)
        {
            string now = (sender as Button).Text;
            if (now == "修")
            {
                this.tbxSendTime.ReadOnly = false;
                this.tbxSendTime.BackColor = Color.White;
                this.tbxSendTime.Focus();
                (sender as Button).Text = "定";
            }
            else
            {
                try
                {
                    int get = int.Parse(this.tbxSendTime.Text);
                    if (get > 0 && get < 1800)
                    {
                        this.tbxSendTime.ReadOnly = true;
                        this.tbxSendTime.BackColor = Color.DarkGray;
                        (sender as Button).Text = "修";
                        NewSendText.周期 = get;
                        NewSendText.周期计数 = get;
                    }
                    else
                    {
                        MessageBox.Show("定义超出总范围，请重设！");
                    }
                }
                catch { }
            }
        }

        private void btnCancelTime_Click(object sender, EventArgs e)
        {
            if (NewSendText.是否周期)
            {
                NewSendText.是否周期 = false;
                lblNowTime.Text = "无周期手动";
                btnCancelTime.Text = "开";
            }
            else {
                NewSendText.是否周期 = true;
                tbxSendTime.Text = NewSendText.周期.ToString();
                lblNowTime.Text = NewSendText.周期计数.ToString();
                btnCancelTime.Text = "停";
                frm.SendTTest();
            }
        }

        private void btnChangePreCout_Click(object sender, EventArgs e)
        {
            string now = (sender as Button).Text;
            if (now == "修")
            {
                this.tbxNowStartCount.ReadOnly = false;
                this.tbxNowStartCount.BackColor = Color.White;
                this.tbxNowStartCount.Focus();
                (sender as Button).Text = "定";
            }
            else
            {
                int get = int.Parse(this.tbxNowStartCount.Text);
                if (get > 0)
                {
                    this.tbxNowStartCount.ReadOnly = true;
                    this.tbxNowStartCount.BackColor = Color.DarkGray;
                    (sender as Button).Text = "修";
                    NewSendText.起始段号 = get;
                    NewSendText.已发段数 = 0;
                }
                else
                {
                    MessageBox.Show("定义超出总范围，请重设！");
                }
            }
        }

        private void lblLeastCount_TextChanged(object sender, EventArgs e)
        {
            if (lblLeastCount.Text.Contains("乱序"))
            {
                btnFixStart.Enabled = false;
            }
            else {
                btnFixStart.Enabled = true;
            }
        }

        private void lblNowTime_Click(object sender, EventArgs e)
        {
            NewSendText.周期计数--;
        }
        #endregion

        #region 限制输入
        private void tbxNowStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbxSendC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion 

        #region 保存配置
        /// <summary>
        /// 保存配置说明
        /// 顺序：配置序号 | 文章来源 | 文章地址 | 文章标题 | 当前标记 |
        ///      发送字数 | 已发字数 | 当前段号 | 是否乱序 | 是否周期 | 周期时间 | 是否独练 |保存时间
        /// 配置名称：给配置的命名 可由用户自定义
        /// 发文标记：上一次发文断点位置
        /// 文章标题：发文的文章标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //检测当前存在几个配置
            try
            {
                int count = AllIniCount;
                int infoNo;
                bool SetupAll = true;
                if (NewSendText.当前配置序列.Length != 0)
                {
                    DialogResult temp = MessageBox.Show("当前配置序列为：" + NewSendText.当前配置序列 + "，确认要覆盖该配置？\n点击是：覆盖配置\n点击否：重新建立新配置", "保存配置提示", MessageBoxButtons.YesNoCancel);
                    switch (temp)
                    {
                        case DialogResult.Yes:
                            infoNo = int.Parse(NewSendText.当前配置序列);
                            SetupAll = false;
                            break;
                        case DialogResult.No:
                            infoNo = count + 1;//配置序号
                            break;
                        default: return;
                    }
                }
                else {
                    infoNo = count + 1;//配置序号
                }
                NewSendText.当前配置序列 = infoNo.ToString();
                string infoTextSources = NewSendText.文章来源.ToString();
                string infoTextAdd = NewSendText.文章地址;
                string infoTextTitle = NewSendText.标题;
                string infoNowStart = NewSendText.标记.ToString();
                string infoSendTextCount = NewSendText.字数.ToString();//发送字数
                string infoHaveSend = NewSendText.已发字数.ToString();
                string infoNowDuan = this.tbxNowStartCount.Text;//当前段号
                string infoIsInOrder = NewSendText.是否乱序.ToString();
                string infoIsT = NewSendText.是否周期.ToString();
                string infoTTime = NewSendText.周期.ToString();
                string infoisMyseft = NewSendText.是否独练.ToString();
                _Ini setupini = new _Ini("Ttyping.ty");
                string getALL_ = setupini.IniReadValue("发文面板配置","总序列","0");
                if (getALL_ != "0")
                {
                    if (getALL_.Contains(infoNo.ToString()))
                    {
                        if (SetupAll)
                        {
                            for (int i = 1; i <= 50; i++)
                            {
                                if (!getALL_.Contains(i.ToString()))
                                {
                                    //if (MessageBox.Show("找到未用到的配置序列：" + i + "\n是否使用？", "配置序列说明", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                    //{
                                        infoNo = i;
                                        NewSendText.当前配置序列 = i.ToString();
                                        getALL_ += infoNo + "|";
                                        setupini.IniWriteValue("发文面板配置", "总序列", getALL_);
                                        break;
                                    //}
                                    //else
                                    //{
                                    //    return;
                                   // }
                                }
                            }
                        }
                    }
                    else {
                        getALL_ += infoNo + "|";
                        setupini.IniWriteValue("发文面板配置","总序列",getALL_);
                    }
                }
                else {
                    setupini.IniWriteValue("发文面板配置", "总序列", infoNo + "|");
                }
                //配置集中
                string info = infoNo + "|" + infoTextSources + "|" + infoTextAdd + "|" + infoTextTitle +
                    "|" + infoNowStart + "|" + infoSendTextCount + "|" + infoHaveSend + "|" +
                    infoNowDuan + "|" + infoIsInOrder + "|" + infoIsT + "|" + infoTTime + "|" + infoisMyseft + "|" + DateTime.Now;
                setupini.IniWriteValue("发文配置", infoNo.ToString(), info.ToString());

                MessageBox.Show("保存成功！发文配置序列：" + infoNo,"保存提示",MessageBoxButtons.OK);
                this.lblAll.Text = ReadKeys("发文配置").Count.ToString();
                this.lblNowIni.Text = infoNo.ToString();
            }
            catch { }
        }

        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName); // ANSI版本
        //遍历键值
        public ArrayList ReadKeys(string sectionName)
        {
            string str1 = Application.StartupPath;
            byte[] buffer = new byte[5120];
            int rel = GetPrivateProfileStringA(sectionName, null, "", buffer, buffer.GetUpperBound(0), str1 + "\\Ttyping.ty");

            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }
        #endregion

        #region 独练
        private void cbxSingleTest_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxSingleTest.Checked)
            {
                NewSendText.是否独练 = true;
            }
            else {
                NewSendText.是否独练 = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                NewSendText.是否自动 = true;
            }
            else
            {
                NewSendText.是否自动 = false;
            }
        }
        #endregion



    }
}
