using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2.编码提示
{
    public partial class FormBMTips : Form
    {
        private DateTime dtstart;
        private FormBMTipsModel _formBmTipsModel;
        List<List<string>> DicList = new List<List<string>>();
        public FormBMTips()
        {
            InitializeComponent();
        }

        #region 点击事件

        private delegate void DReadEnded(object sender,EventArgs eventArgs);
        private event DReadEnded ReadEnded;

        protected virtual void OnReadEnded()
        {
            DReadEnded handler = ReadEnded;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            this.btnSelectFile.Enabled = false;
            this.lblReading.Text = "正在读取中...";
            dtstart = DateTime.Now;
            ReadEnded -= OnReadEnded;
            ReadEnded += OnReadEnded;
            this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        using (_formBmTipsModel = new FormBMTipsModel())
                        {
                            DicList = _formBmTipsModel.Dic.ToList();
                        }
                        OnReadEnded(sender, null);
                    }
                    catch (Exception exception)
                    {
                        DicList = null;
                        this.btnSelectFile.Enabled = true;
                        MessageBox.Show(exception.Message);
                    }
                }));
        }

        private void OnReadEnded(object sender, EventArgs eventArgs)
        {
            TimeSpan ts = DateTime.Now - dtstart;
            string s = "";
            switch (_formBmTipsModel.ReadState)
            {
                case State.Done:
                    s = "读取完成，共" + DicList.Count + "条，用时" + ts.TotalSeconds.ToString("0.00") + "s";
                    break;
                case State.FileNotExist:
                    s = "文件不存在！";
                    break;
                case State.CanNotFind:
                    s = "没有找到数据！";
                    break;
                case State.CanNotRead:
                    s = "未读取到数据！";
                    break;
                case State.Error:
                default:
                    s = "读取错误！请检查文件！";
                    break;
            }
            this.lblReading.Text = s;
            this.btnSelectFile.Enabled = true;
        }

        #endregion

    }
}
