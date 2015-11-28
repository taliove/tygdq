using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication2
{
    public partial class FormTheme : Form
    {
        Form1 frm;
        public FormTheme(Form1 frm1)
        {
            frm = frm1;
            InitializeComponent();
        }

        private void FormTheme_Load(object sender, EventArgs e)
        {
            /*ini.IniWriteValue("主题","状态",Theme.isTheme.ToString());
            ini.IniWriteValue("主题","是否应用主题背景",this.SwitchB1.Checked.ToString());
            ini.IniWriteValue("主题", "背景路径", this.lblBGPath.Text);
            ini.IniWriteValue("主题","纯色",this.lblPicShow.BackColor.ToArgb().ToString());
            ini.IniWriteValue("主题","主题颜色",this.lblThemeBGShow.BackColor.ToArgb().ToString());
            ini.IniWriteValue("主题","字体颜色",this.lblThemeFCShow.BackColor.ToArgb().ToString());
            ini.IniWriteValue("主题","预览",this.SwitchB2.Checked.ToString());*/
            //初始化
            this.SwitchB1.Checked = Theme.isBackBmp;
            this.SwitchB1.Invalidate();
            if (!Theme.isBackBmp) {
                this.lblSelectPIC.Enabled = !Theme.isBackBmp;
                this.lblSelectBG.Enabled = Theme.isBackBmp;
            }
            this.lblPicShow.BackColor = Theme.ThemeBG;//纯色
            this.lblThemeBGShow.BackColor = Theme.ThemeColorBG;//主题色
            this.lblThemeFCShow.BackColor = Theme.ThemeColorFC; //字体色
            this.lblSelectBG.Enabled = this.SwitchB1.Checked;
            this.lblSelectBG.Enabled = !this.SwitchB1.Checked;

            this.SwitchB2.Checked = Theme.ReView;
            this.SwitchB2.Invalidate();
            //背景显示
            if (Theme.ThemeBackBmp.Length == 0)
            {
                this.lblBGPath.Text = "程序默认";
            }
            else {
                this.lblBGPath.Text = Theme.ThemeBackBmp;
            }
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblcls_MouseEnter(object sender, EventArgs e)
        {
            this.lblcls.BackColor = Color.FromArgb(199,12,52);
        }

        private void lblcls_MouseLeave(object sender, EventArgs e)
        {
            this.lblcls.BackColor = Color.Gray;
        }

        private void lblcls_MouseDown(object sender, MouseEventArgs e)
        {
            this.lblcls.TextAlign = ContentAlignment.BottomCenter;
        }

        private void lblcls_MouseUp(object sender, MouseEventArgs e)
        {
            this.lblcls.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void lblcls_Click(object sender, EventArgs e)
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

        private void FormTheme_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// 选择背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "图片|*.jpg;*.bmp;*.png;*.gif";
            openFileDialog1.FileName = Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                this.lblBGPath.Text = openFileDialog1.FileName;
                ReView();
            }
        }

        /// <summary>
        /// 更改属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchB1_CChange(object sender, EventArgs e)
        {
            if (this.SwitchB1.Checked)
            {
                this.lblSelectPIC.Enabled = true;
                this.lblSelectBG.Enabled = false;
            }
            else {
                this.lblSelectPIC.Enabled = false;
                this.lblSelectBG.Enabled = true;
            }
        }

        #region 主题设置
        /// <summary>
        /// 主题颜色的确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Theme.ThemeColorBG;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                this.lblThemeBGShow.BackColor = cd.Color;
                ReView();
            }
        }

        /// <summary>
        /// 主题颜色的确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSelectFCColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Theme.ThemeColorFC;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.lblThemeFCShow.BackColor = cd.Color;
                ReView();
            }
        }

        /// <summary>
        /// 纯色设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSelectBG_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Theme.ThemeColorBG;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.lblPicShow.BackColor = cd.Color;
                ReView();
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            _Ini ini = new _Ini("Ttyping.ty");
            ini.IniWriteValue("主题", "是否启用主题", "True");
            ini.IniWriteValue("主题","是否应用主题背景",this.SwitchB1.Checked.ToString());
            ini.IniWriteValue("主题", "背景路径", this.lblBGPath.Text);
            ini.IniWriteValue("主题","纯色",this.lblPicShow.BackColor.ToArgb().ToString());
            ini.IniWriteValue("主题","主题颜色",this.lblThemeBGShow.BackColor.ToArgb().ToString());
            ini.IniWriteValue("主题","字体颜色",this.lblThemeFCShow.BackColor.ToArgb().ToString());
            ini.IniWriteValue("主题","预览",this.SwitchB2.Checked.ToString());
            Theme.isBackBmp = this.SwitchB1.Checked;
            Theme.ThemeBackBmp = this.lblBGPath.Text;
            Theme.ThemeBG = this.lblPicShow.BackColor;
            Theme.ThemeColorBG = this.lblThemeBGShow.BackColor;
            Theme.ThemeColorFC = this.lblThemeFCShow.BackColor;
            Theme.ReView = this.SwitchB2.Checked;
            Theme.ThemeApply = true;
            this.Close();
        }

        /// <summary>
        /// 预览
        /// </summary>
        private void ReView() {
            if (this.SwitchB2.Checked)
            {
                string path;
                if (SwitchB1.Checked)
                {
                    path = this.lblBGPath.Text;
                    if (path == "程序默认") {
                        path = "程序默认";
                    }
                    else if (!System.IO.File.Exists(path)) {
                        path = "";
                    }
                }
                else {
                    path = "纯色";
                }
                frm.LoadTheme(path, this.lblThemeBGShow.BackColor, this.lblThemeFCShow.BackColor, this.lblPicShow.BackColor);
            }
        }

        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTheme_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Theme.ThemeApply)
                if (Theme.isBackBmp)
                {
                    string s = Theme.ThemeBackBmp;
                    if (!System.IO.File.Exists(s))
                        s = "程序默认";

                    frm.LoadTheme(s, Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
                }
                else
                {
                    frm.LoadTheme("纯色", Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
                }
        }
        #endregion

        #region 默认色
        //背景图片或颜色
        private void newButton4_Click(object sender, EventArgs e)
        {
            if (!this.SwitchB1.Checked)
            {
                //默认的背景
                this.SwitchB1.Checked = true;
                this.lblSelectPIC.Enabled = true;
                this.lblSelectBG.Enabled = false;
                this.SwitchB1.Invalidate();
            }
            this.lblBGPath.Text = "程序默认";
            ReView();
        }
        /*
        /// <summary>
        /// 从资源中获取图片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private System.Drawing.Image GetImageFromResources(string name)
        {
            //System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
            //System.IO.Stream imgStream = asm.GetManifestResourceStream("WindowsFormsApplication2.Resources.ButtonBG." + name);
            return TyDll.GetResources.GetImage("Resources.BG." + name);
        }*/
        /// <summary>
        /// 预览 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchB2_Click(object sender, EventArgs e)
        {
            if (this.SwitchB2.Checked)
                ReView();
            else {
                //默认
                //frm.LoadTheme(Theme.ThemeBackBmp, this.lblThemeBGShow.BackColor, this.lblThemeFCShow.BackColor, Color.White);
            }
        }

        //主题颜色的默认色
        private void newButton2_Click(object sender, EventArgs e)
        {
            this.lblThemeBGShow.BackColor = Theme.ThemeColorBG;
            ReView();
        }

        private void newButton1_Click(object sender, EventArgs e)
        {
            this.lblThemeFCShow.BackColor = Theme.ThemeColorFC;
            ReView();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Theme.ThemeApply = false;
            _Ini ini = new _Ini("Ttyping.ty");
            ini.IniWriteValue("主题", "是否启用主题", "False");
            frm.LoadTheme("", Theme.ThemeColorBG, Theme.ThemeColorFC, Theme.ThemeBG);
            this.Close();
        }
        #endregion

        //背景处理 预览
        private void SwitchB1_Click(object sender, EventArgs e)
        {
            ReView();
        }
    }
}
