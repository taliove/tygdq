using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication2.检查更新
{
    public partial class UpgradePro : Form
    {
        public UpgradeModel UpgradeModel { get; private set; }
        private bool _start = false; //因为启动的时候，会显示“暂无更新”，所以加个BOOL判断
        public UpgradePro()
        {
            InitializeComponent();
            UpgradeModel = new UpgradeModel();
            this.panel1.Paint += panel1_Paint;
            UpgradeModel.HasUpdated += UpgradeModel_HasUpdated;
            this.rtbInfo.VisibleChanged += (sender, args) => btnSeeContext.Text = this.rtbInfo.Visible ? "返回" : "查看更新说明";
        }

        private void UpgradeModel_HasUpdated(object sender, bool b)
        {
            this.Invoke(new MethodInvoker(() => { btnSeeContext.Enabled = b; }));
        }

        void panel1_Paint(object sender, PaintEventArgs e)
        {
            var font1 = new Font("微软雅黑", 16f);
            var font2 = new Font(font1.FontFamily, 45);
            e.Graphics.DrawString("当前版本", font1, Brushes.Gray, 10, 10);
            e.Graphics.DrawString(Glob.VerInstance, font2, Brushes.Gray, 30, 40);
            if (UpgradeModel.是否有更新 && !UpgradeModel.是否有异常)
            {
                e.Graphics.DrawString("更新版本 " + UpgradeModel.日期.ToShortDateString(), font1, Brushes.DimGray, 10, 115);
                e.Graphics.DrawString(UpgradeModel.版本, font2, Brushes.HotPink, 30, 155);
            }

            if (UpgradeModel.是否有异常)
            {
                e.Graphics.DrawString("更新异常！", font1, Brushes.Red, 10, 135);
            }

            if (!UpgradeModel.是否有更新 && _start)
            {
                e.Graphics.DrawString("暂无更新！", font1, Brushes.Coral, 10, 155);
            }
        }

        private void btnCheckUpgrade_Click(object sender, EventArgs e)
        {

            //http://taliove.diandian.com/updateInfo
            if (this.rtbInfo.Visible) this.rtbInfo.Visible = false;
            btnCheckUpgrade.Enabled = false;
            UpgradeModel.是否有更新 = false;
            btnSeeContext.Enabled = false;
            this.btnCheckUpgrade.Text = "正在检测更新";
            panel1.Invalidate();
            Action action = () => UpgradeModel.GetWebRequest();
            action.BeginInvoke(GetCompleted, null);
        }

        private void GetCompleted(IAsyncResult iResult)
        {
            if (iResult.IsCompleted)
            {
                this.Invoke(new MethodInvoker(() =>
                    {
                        btnCheckUpgrade.Enabled = true;
                        _start = true; //显示
                        this.btnCheckUpgrade.Text = "检查更新";
                        panel1.Invalidate();
                        this.rtbInfo.AppendText("下载地址：http://taliove.ys168.com");
                        this.rtbInfo.Text = UpgradeModel.ToString();
                    }));
            }
        }

        private void btnSeeContext_Click(object sender, EventArgs e)
        {
            this.rtbInfo.Visible = !this.rtbInfo.Visible;
        }

        //点击链接
        private void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void UpgradePro_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UpgradeModel != null)
            {
                UpgradeModel.HasUpdated -= UpgradeModel_HasUpdated;
            }
        }
    }
}
