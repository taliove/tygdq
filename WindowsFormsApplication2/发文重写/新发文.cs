using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions; //正则
using System.Collections;
using TyDll;
namespace WindowsFormsApplication2
{
    public partial class 新发文 : Form
    {
        Assembly _assembly;
        StreamReader _textStreamReader;
        Form1 frm;
        public 新发文(Form1 frm1)
        {
            frm = frm1;
            InitializeComponent();
        }

        private void 新发文_Load(object sender, EventArgs e)
        {
            //DriveInfo[] Drives = DriveInfo.GetDrives();
            //HeaderFresh("我的电脑");
            //foreach (DriveInfo Dirs in Drives) {
            //    listViewFile.Items.Add(new ListViewItem(new string[] {Dirs.Name,"磁盘"}));
            //}
            NewSendText.已发段数 = 0;
            NewSendText.起始段号 = 1;
            NewSendText.当前配置序列 = "";
            ReadAll(Application.StartupPath);
            this.Text += "[当前群：" + frm.lblQuan.Text + "]";
            _Ini t2 = new _Ini("Ttyping.ty");
            this.cbxTickOut.Checked = bool.Parse(t2.IniReadValue("发文面板配置", "自动剔除空格", "True"));
            this.cbx乱序全段不重复.Checked = bool.Parse(t2.IniReadValue("发文面板配置", "乱序全段不重复", "False"));
            if (!File.Exists(Application.StartupPath + "\\TyDll.dll")) {
                this.lbxTextList.Items.Clear();
                this.lbxTextList.SelectedIndexChanged -= new EventHandler(lbxTextList_SelectedIndexChanged);
                MessageBox.Show("未找到TyDll.dll文件！");

            }
        }

        #region 改变TAB页时发生
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            int getid = (sender as TabControl).SelectedIndex;
            if (getid == 2) { //剪切板
                try
                {
                    rtbClipboard.Text = Clipboard.GetText();
                }
                catch (Exception err)
                {
                    rtbClipboard.Text = err.Message + "，请自行粘贴！";
                }
            }
            else if (getid == 3) {
                iniRead();//更新配置
            }
        }
        #endregion

        #region 自带文章
        private string GetText = "";//获取到的文章
        private void lbxTextList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (sender as ListBox).SelectedIndex;
            if ((sender as ListBox).Text.Length != 0)
            {
                //_assembly = Assembly.GetExecutingAssembly();
                GetText = TyDll.GetResources.GetText("Resources.TXT." + (sender as ListBox).Text + ".txt");
                lblTitle.Text = (sender as ListBox).Text;
                
                ComText(GetText); //确认文章信息
            }
            switch (index)
            {
                case 0: rtbInfo.Text = "选用标准常用字前一千五百的【前五百】个单字"; break;
                case 1: rtbInfo.Text = "选用标准常用字前一千五百的【中五百】个单字"; break;
                case 2: rtbInfo.Text = "选用标准常用字前一千五百的【后五百】个单字"; break;
                case 3: rtbInfo.Text = "选用标准常用词组前三个常用词组"; break;
                case 4: rtbInfo.Text = "选【为人民服务】现代文一篇"; break;
                case 5: rtbInfo.Text = "选【岳阳楼记】古文一篇"; break;
                case 6: rtbInfo.Text = "前1500字整体"; break;
                default: rtbInfo.Text = "有需求内置文章的跟友，请联系作者：taliove@vip.qq.com"; break;
            }
            NewSendText.文章地址 = index.ToString();
            //else {
            //    (sender as ListBox).ClearSelected();
           // }
        }

        public void ComText(string Text) { //确认文章信息
            if (Text.Length != 0) {
                if (Text.Length > 300)
                {
                    rtbShowText.Text = GetText.Substring(0, 300) + "[......未完]";
                }
                else {
                    rtbShowText.Text = GetText + "[已完]";
                }
                if (Text.Length > 25)
                {
                    this.tbxSendCount.Text = "25";
                }
                else {
                    this.tbxSendCount.Text = (Text.Length/2).ToString();
                }
                //确认文章类型
                isWords(Text);
                //确认字数信息
                if (this.lblStyle.Text == "词组")
                {
                    FindWords();
                }
                else //非词组的时候
                {
                    lblTextCount.Text = GetText.Length.ToString();
                }
                tbxSendCount.Select();
                tbxSendCount.MaxLength = lblTextCount.Text.Length;
                tbxSendStart.MaxLength = lblTextCount.Text.Length;
                if (this.cbxTickOut.Checked)
                    GetText = TickBlock(Text, "");
            }
        }

        /// <summary>
        /// 找到词组信息
        /// </summary>
        private void FindWords() {
            string[] getWords;
            if (cbxSplit.SelectedIndex == 1)
                getWords = GetText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            else
                getWords = GetText.Split(split);

            getWords = GetText.Split(split);
            if (getWords.Length > 1)
            {
                lblTextCount.Text = getWords.Length.ToString();
                ShowFlowText("找到" + getWords.Length + "个词组");
            }
            else
                ShowFlowText("未找到词组，请确定您所选择的文件");
        }
        /// <summary>
        /// 显示浮动的信息
        /// </summary>
        /// <param name="text">需要显示的信息</param>
        public void ShowFlowText(string text)
        {
            ShowMessage sm = new ShowMessage(this.Size, this.Location, this);
            sm.Show(text);
        }

        //确信文章类型
        public bool isWords(string text)
        {
            Regex regexAll = new Regex(@"，|。|！|…|：|“|”|？");
            if (regexAll.IsMatch(text))
            {
                tabControl2.SelectedIndex = 1;
                if (this.lblStyle.Text == "") this.lblStyle.Text = "文章";
                return true;
            }
            else
            {
                tabControl2.SelectedIndex = 0;
                if (this.lblStyle.Text == "") this.lblStyle.Text = "单字";
                return false;
            }
        }

        /// <summary>
        /// 分隔的默认定义
        /// </summary>
        private char split = ' ';
        /// <summary>
        /// 词组分隔符确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSplit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (sender as ComboBox).SelectedIndex;
            switch (index) {
                case 0: split = ' '; break;
                case 1: split = '\n'; break;
                case 2: split = '\t'; break;
                case 3: split = (this.tbxsplit.TextLength > 0) ? this.tbxsplit.Text.ToCharArray()[0] : ' '; break;
            }
            FindWords();
        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            string getnow = (sender as TabControl).SelectedTab.Text;
            if (lblStyle.Text == "单字") {
                if (getnow == "文章") e.Cancel = true;
            }

            if (lblStyle.Text == "文章") {
                if (getnow == "单字") e.Cancel = true;
            }

            if ((sender as TabControl).SelectedIndex == 2) ShowFlowText("词组发送默认的乱序模式，暂不支持顺序。");
            this.lblStyle.Text = (sender as TabControl).TabPages[this.tabControl2.SelectedIndex].Text;
        }

        private void lblStyle_TextChanged(object sender, EventArgs e)
        {
            string getit = (sender as Label).Text;
            if (getit == "单字") {
                (sender as Label).ForeColor = Color.DarkOliveGreen;
            }
            else if (getit == "文章")
            {
                (sender as Label).ForeColor = Color.IndianRed;
            }
            else if (getit == "词组") {
                (sender as Label).ForeColor = Color.DeepPink;
            }
            else
            {
                (sender as Label).ForeColor = Color.Black;
            }
        }
        //显示改变时
        private void rtbShowText_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region 词组信息
        /// <summary>
        /// 选择词组时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxWordsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxWordsCheck.Checked)
            {
                this.tabControl2.SelectedIndex = 2;
                if (this.cbxSplit.SelectedIndex == -1)
                    this.cbxSplit.SelectedIndex = 0;

                string[] get = GetText.Split(split);
                if (get.Length > 1)
                {
                    lblTextCount.Text = get.Length.ToString();
                    ShowFlowText("找到" + get.Length + "个词组");
                }
                else
                    ShowFlowText("未找到词组，请确定您所选择的文件");
            }
            else {
                ComText(GetText);
            }
        }
        #endregion
        #region 清除所有信息
        private void ClearAll() {
            if (lblTitle.Text.Length != 0)
            {
                lblStyle.ResetText();
                lblTextCount.ResetText();
                lblTitle.ResetText();
                rtbInfo.ResetText();
                rtbShowText.ResetText();
                GetText = "";
                NewSendText.当前配置序列 = "";
            }
        }
        #endregion

        #region 程序控制
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                nudSendTimer.Enabled = true;
                speedfill();
                this.cbxAuto.Enabled = false;
            }
            else {
                nudSendTimer.Enabled = false;
                this.lblspeed.Text = "0";
                this.cbxAuto.Enabled = true;
            }
        }

        //自动发文
        private void cbxAuto_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                this.checkBox1.Enabled = false;
            }
            else
            {
                this.checkBox1.Enabled = true;
            }
        }

        private void btnAllText_Click(object sender, EventArgs e)
        {
            if (GetText.Length != 0) {
                this.tbxSendStart.Text = "0";
                this.tbxSendCount.Text = GetText.Length.ToString();
            }
        }

        //速度显示 由 周期
        private void nudSendTimer_ValueChanged(object sender, EventArgs e)
        {
            speedfill();
        }

        private void speedfill() {
            try
            {
                int textcount = int.Parse(this.tbxSendCount.Text);
                int nowtime = (int)this.nudSendTimer.Value;
                double speed;
                if (nowtime > 0)
                {
                    speed = (double)textcount * 60 / nowtime;
                    if (speed < 300)
                        this.lblspeed.Text = speed.ToString("0");
                    else
                        this.lblspeed.Text = "0";
                }
            }
            catch { this.lblspeed.Text = "0"; }
        }
        #endregion

        #region 自定义文章
        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listViewFile.HitTest(e.X, e.Y); ;
            if (info.Item != null) {
                string Hearder = listViewFile.Columns[0].Text;
                if (info.Item.Text == "..") //返回上一层
                {
                    string path = "";
                    try
                    {
                        if (Hearder.Length != 3)
                            path = Directory.GetParent(Hearder).FullName;//上一级目录
                    }
                    catch {}
                    if (path.Length != 0)
                    {
                        ReadAll(path);
                    }
                    else
                    {
                        HeaderFresh("我的电脑");
                        DriveInfo[] Drives = DriveInfo.GetDrives();
                        foreach (DriveInfo Dirs in Drives)
                        {
                            listViewFile.Items.Add(new ListViewItem(new string[] { Dirs.Name, "磁盘" }));
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(info.Item.Text))
                    {
                        ReadAll(info.Item.Text);//返回到我的电脑
                    }
                    else {
                        string Dir = "";
                        if (Hearder[Hearder.Length - 1] == '\\')
                        {
                            Dir = Hearder + info.Item.Text;
                        }
                        else {
                            Dir = Hearder + "\\" + info.Item.Text;
                        }
                        if (Dir.Length != 0)
                            ReadAll(Dir);//点开内容文件夹
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadAll(Application.StartupPath);
            this.listViewFile.Focus();
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            string Hearder = listViewFile.Columns[0].Text;
            string path = "";
            try
            {
                if (Hearder.Length != 3)
                    path = Directory.GetParent(Hearder).FullName;//上一级目录
            }
            catch { }
            if (path.Length != 0)
            {
                ReadAll(path);
            }
            else
            {
                HeaderFresh("我的电脑");
                DriveInfo[] Drives = DriveInfo.GetDrives();
                foreach (DriveInfo Dirs in Drives)
                {
                    listViewFile.Items.Add(new ListViewItem(new string[] { Dirs.Name, "磁盘" }));
                }
            }
            this.listViewFile.Focus();
        }

        private int txtLocation = 0;
        private void ReadAll(string path) {
            if (Directory.Exists(path))
            {
                try
                {
                    string[] dirs = Directory.GetDirectories(path);
                    HeaderFresh(path);
                    listViewFile.Items.Add(new ListViewItem(new string[] { "..", "文件夹" }));
                    foreach (string dir in dirs)
                    {
                        listViewFile.Items.Add(new ListViewItem(new string[] { Path.GetFileName(dir), "文件夹" }));
                    }
                    string[] files = Directory.GetFiles(path);
                    int findCount = 0;
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.Extension == ".txt")
                        {
                            ListViewItem item = new ListViewItem(new string[] { fi.Name, "文本" });
                            listViewFile.Items.Add(item);
                            item.ForeColor = Color.Green;
                            findCount++;
                            if (findCount == 1) txtLocation = listViewFile.Items.Count;
                        }
                    }
                    this.lblFindTXTCount.Text = findCount.ToString();//找到文章数量
                }
                catch (Exception err) { MessageBox.Show(err.Message,"跟打器提示！"); }
            }
            else { //获取到文章
                if (File.Exists(path)) {
                    StreamReader fm = new StreamReader(path, System.Text.Encoding.Default);
                    GetText = fm.ReadToEnd();
                    lblTitle.Text = Path.GetFileNameWithoutExtension(path);
                    ComText(GetText);
                    NewSendText.文章地址 = path;
                }
            }
        }

        private void lblFindTXTCount_Click(object sender, EventArgs e)
        {
            try
            {
                this.listViewFile.TopItem = this.listViewFile.Items[txtLocation - 1];
            }
            catch { }
        }

        private void lblFindTXTCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int get = int.Parse((sender as Label).Text);
                if (get > 0)
                {
                    this.lblFindTXTCount.ForeColor = Color.IndianRed;
                }
                else
                {
                    this.lblFindTXTCount.ForeColor = Color.Black;
                }
            }
            catch {
                this.lblFindTXTCount.ForeColor = Color.Black;
            }
        }

        private void HeaderFresh(string dir) {
            listViewFile.Clear();
            ColumnHeader header1 = new ColumnHeader();
            header1.Width = 245;
            header1.Text = dir;
            header1.TextAlign = HorizontalAlignment.Left;
            ColumnHeader header2 = new ColumnHeader();
            header2.Width = 65;
            header2.Text = "类型";
            header2.TextAlign = HorizontalAlignment.Center;
            listViewFile.Columns.Add(header1);
            listViewFile.Columns.Add(header2);
        }

        //鼠标右键
        private void listViewFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && listViewFile.SelectedItems.Count > 0)
            {
                cmsMenu.Items[0].Text = listViewFile.SelectedItems[0].Text;
                cmsMenu.Show(this,e.Location);
            }
        }

        private void cmsMenu_Opening(object sender, CancelEventArgs e)
        {
            if (listViewFile.SelectedItems.Count == 0) {
                e.Cancel = true;
                return;
            }
        }
        #endregion

        #region 开始发文
        private void btnGoSend_Click(object sender, EventArgs e)
        {
            rtbShowText.Text = "处理中...";
            if (GetText.Length == 0) { MessageBox.Show("未获取到文章！"); return; }
            NewSendText.标题 = lblTitle.Text;
            NewSendText.文章全文 = GetText;
            NewSendText.发文全文 = NewSendText.文章全文;
            NewSendText.类型 = lblStyle.Text;
            if (NewSendText.类型 == "词组")
            {
                if (cbxSplit.SelectedIndex == 1)
                    NewSendText.词组 = GetText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    NewSendText.词组 = GetText.Split(split);

                NewSendText.词组发送分隔符 = this.tbxSendSplit.Text;
            }

            NewSendText.是否乱序 = rbnOutOrder.Checked;
            NewSendText.乱序全段不重复 = this.cbx乱序全段不重复.Checked;
            NewSendText.是否一句结束 = cbxOneEnd.Checked;
            try
            {
                NewSendText.字数 = int.Parse(tbxSendCount.Text);
                NewSendText.标记 = int.Parse(tbxSendStart.Text);
                NewSendText.起始段号 = int.Parse(this.tbxQisduan.Text);
            }
            catch { MessageBox.Show("请检查字数、标记或者起始段号是否设置错误？"); return; }
            NewSendText.是否周期 = checkBox1.Checked;
            NewSendText.周期 = (int)nudSendTimer.Value;
            NewSendText.是否独练 = checkBox2.Checked;
            NewSendText.是否自动 = cbxAuto.Checked;
            NewSendText.文章来源 = tabControl1.SelectedIndex;
            NewSendText.发文状态 = true;
            if (NewSendText.是否周期)
            {
                frm.SendTTest();                
            }
            else {
                frm.SendAOnce();
            }
            frm.发文状态ToolStripMenuItem.PerformClick();
            this.Close();
        }
        #endregion

        #region 剪切板获取
        private void btnReGet_Click(object sender, EventArgs e)
        {
            try
            {
                rtbClipboard.Text = Clipboard.GetText();
            }
            catch (Exception err) {
                rtbClipboard.Text = err.Message + "，请自行粘贴！";
            }
        }

        private void rtbClipboard_TextChanged(object sender, EventArgs e)
        {
            GetText = rtbClipboard.Text;
            lblTitle.Text = "来自剪切板";
            ComText(GetText);
        }

        //文章标题
        private void tbxTitle_TextChanged(object sender, EventArgs e)
        {
            lblTitle.Text = (sender as TextBox).Text;
            if (lblTitle.Text.Length == 0) {
                lblTitle.Text = "来自剪切板";
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            tbxTitle.Text = "来自剪切板";
        }

        private void btnTickBlock_Click(object sender, EventArgs e)
        {
            rtbClipboard.Text = TickBlock(rtbClipboard.Text,"");
        }

        private void btnFillIt_Click(object sender, EventArgs e)
        {
            rtbClipboard.Text = TickBlock(rtbClipboard.Text, ",");
        }
        #endregion

        #region 文章处理
        private string TickBlock(string text,string target) {
            string s = text;
            s = s.Replace(" ", target);
            s = s.Replace("　", target);
            s = s.Replace("\r\n", target);
            s = s.Replace("\r", target);
            s = s.Replace("\n", target);
            return s;
        }
        #endregion

        #region 标记处理
        private void tbxSendStart_TextChanged(object sender, EventArgs e)
        {
            if (GetText.Length != 0)
            {
                string temp = (sender as TextBox).Text;
                if (temp.Length != 0)
                {
                    int c = int.Parse(temp);
                    int cou = int.Parse(tbxSendCount.Text);
                    //if (cou == 0) { } else {
                    try
                    {
                        rtbShowText.Text = GetText.Substring(c, cou) + "\r\n[...当前设置文段预览(非实际)]";
                        rtbShowText.ForeColor = Color.Black;
                        btnGoSend.Enabled = true;
                    }
                    catch
                    {
                        rtbShowText.Text = "标记起始点设置错误，因为设置数值超出总字数，请重设！";
                        rtbShowText.ForeColor = Color.IndianRed;
                        btnGoSend.Enabled = false;
                    }
                }
            }
        }

        private void tbxSendCount_TextChanged(object sender, EventArgs e)
        {
            Confrim();
            if (this.checkBox1.Checked) speedfill();
        }

        private void lblTextCount_TextChanged(object sender, EventArgs e)
        {
            string te = this.tbxSendCount.Text;
            if (te.Length != 0)
            {
                int cou = int.Parse(te);
                if (cou > 0)
                {
                    int c = int.Parse(tbxSendStart.Text);
                    if (c + cou > GetText.Length)
                    {
                        rtbShowText.Text = "发送字数设置错误！";
                        rtbShowText.ForeColor = Color.IndianRed;
                        btnGoSend.Enabled = false;
                    }
                    else {
                        rtbShowText.ForeColor = Color.Black;
                        btnGoSend.Enabled = true;
                    }
                }
            }
        }

        private void Confrim() {
            if (GetText.Length != 0)
            {
                string te = this.tbxSendCount.Text;
                if (te.Length != 0)
                {
                    int cou = int.Parse(te);
                    if (cou > 0)
                    {
                        int c = int.Parse(tbxSendStart.Text);
                        if (c + cou <= GetText.Length)
                        {
                            try
                            {
                                rtbShowText.Text = GetText.Substring(c, cou) + "\r\n[...当前设置文段预览(非实际)]";
                                rtbShowText.ForeColor = Color.Black;
                                btnGoSend.Enabled = true;
                            }
                            catch
                            {
                                rtbShowText.Text = "在当前标起始点下，字数设置超出限制！";
                                rtbShowText.ForeColor = Color.IndianRed;
                                btnGoSend.Enabled = false;
                            }
                        }
                        else
                        {
                            rtbShowText.Text = "发送字数设置错误！";
                            rtbShowText.ForeColor = Color.IndianRed;
                            btnGoSend.Enabled = false;
                        }
                    }
                    else
                    {
                        rtbShowText.Text = "发送字数设置错误！";
                        rtbShowText.ForeColor = Color.IndianRed;
                        btnGoSend.Enabled = false;
                    }
                }
            }
        }

        private void tbxSendStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbxSendCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbxQisduan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region 配置载入
        private void btnRefreshIni_Click(object sender, EventArgs e)
        {
            iniRead();
        }

        private bool iniRead() {
            try
            {
                this.lbxIni.Items.Clear();
                _Ini getData = new _Ini("Ttyping.ty");
                ArrayList getini = ReadKeys("发文配置");
                int count = getini.Count;
                for (int i = 0; i < count; i++)
                {
                    string getit = getData.IniReadValue("发文配置",getini[i].ToString(),"0");
                    if (getit == "0") return false;
                    string[] get = getit.ToString().Split('|');
                    this.lbxIni.Items.Add(getini[i] + " | " + get[3] + " | " + get[12]);
                }
                return true;
            }
            catch (Exception err){
                MessageBox.Show(err.Message);
                return false;
            }
        }

        private void lbxIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = "";
            _Ini getdata = new _Ini("Ttyping.ty");
            try
            {
                select = (sender as ListBox).SelectedItem.ToString();
            }
            catch {
                return;
            }
            string[] idget = select.Split('|');
            string get = getdata.IniReadValue("发文配置",idget[0].Trim(), "0");
            if (get != "0")
            {
                string[] getAll = get.Split('|');
                //确定文章
                switch (getAll[1])
                {
                    case "0":
                        _assembly = Assembly.GetExecutingAssembly();
                        _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("WindowsFormsApplication2.Resources." + this.lbxTextList.Items[int.Parse(getAll[2])].ToString() + ".txt"));
                        if (_textStreamReader.Peek() != -1)
                        {
                            GetText = _textStreamReader.ReadLine(); //文
                            ComText(GetText); //确认文章信息
                        }
                        break;//自带文章
                    case "1":
                        string path = getAll[2];
                        if (File.Exists(path))
                        {
                            StreamReader fm = new StreamReader(path, System.Text.Encoding.Default);
                            GetText = fm.ReadToEnd();
                            lblTitle.Text = Path.GetFileNameWithoutExtension(path);
                            ComText(GetText);
                            NewSendText.文章地址 = path;
                        }
                        break;//自定义文章
                }
                //显式信息写入
                lblTitle.Text = getAll[3];//文章标题
                if (bool.Parse(getAll[8])) //是否乱序
                    rbnOutOrder.PerformClick();
                else
                    rbninOrder.PerformClick();
                tbxSendCount.Text = getAll[5];//字数
                tbxSendStart.Text = getAll[4];//标记
                this.checkBox1.Checked = bool.Parse(getAll[9]); //周期
                if (this.checkBox1.Checked) nudSendTimer.Value = int.Parse(getAll[10]);
                this.checkBox2.Checked = bool.Parse(getAll[11]);
                //隐式信息写入
                NewSendText.起始段号 = int.Parse(getAll[7]);
                NewSendText.文章来源 = int.Parse(getAll[1]);
                NewSendText.已发段数 = int.Parse(getAll[6]);
                NewSendText.当前配置序列 = getAll[0];
            }
        }
        //删除
        private void btnDelini_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lbxIni.Items.Count > 0)
                {
                    _Ini ini = new _Ini("Ttyping.ty");
                    string select = this.lbxIni.SelectedItem.ToString();
                    string[] idget = select.Split('|');
                    string get = idget[0].Trim();
                    ini.IniWriteValue("发文配置", get, null);
                    string temp = ini.IniReadValue("发文面板配置", "总序列", "0");
                    if (temp != "0")
                    {
                        if (ini.IniReadValue("发文配置", get, "NO") == "NO")
                        {
                            if (temp.Contains(get))
                            {
                                temp = temp.Remove(temp.IndexOf(get), 2);
                                ini.IniWriteValue("发文面板配置", "总序列", temp);
                            }
                            iniRead();
                            ClearAll();
                        }
                    }
                }
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

        #region 发文整体配置
        private void cbxTickOut_CheckedChanged(object sender, EventArgs e)
        {
            bool temp = (sender as CheckBox).Checked;
            _Ini t2 = new _Ini("Ttyping.ty");
            if (temp)
            {
                t2.IniWriteValue("发文面板配置", "自动剔除空格", "True");
            }
            else {
                t2.IniWriteValue("发文面板配置", "自动剔除空格", "False");
            }
        }

        private void cbx乱序全段不重复_CheckedChanged(object sender, EventArgs e)
        {
            bool temp = (sender as CheckBox).Checked;
            _Ini t2 = new _Ini("Ttyping.ty");
            if (temp)
            {
                t2.IniWriteValue("发文面板配置", "乱序全段不重复", "True");
            }
            else
            {
                t2.IniWriteValue("发文面板配置", "乱序全段不重复", "False");
            }
        }
        #endregion  
    }
}
