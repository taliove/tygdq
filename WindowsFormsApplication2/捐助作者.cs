using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class 捐助作者 : Form
    {
        public 捐助作者()
        {
            InitializeComponent();
        }

        private DateTime Start = new DateTime(2012, 3, 20);
        private void 捐助作者_Load(object sender, EventArgs e)
        {
            var ts = DateTime.Now - Start;
            lblInfo.Text = string.Format("添雨跟打器从{0}至今已经更新了{1}天", Start.ToShortDateString(),
                                         ts.TotalDays.ToString("0.00"));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://me.alipay.com/taliove");
        }
    }
}
