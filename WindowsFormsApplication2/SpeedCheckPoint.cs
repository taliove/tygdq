using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication2
{
    public partial class SpeedCheckPoint : Form
    {
        Form1 frm;
        private ChartArea CA_ = new ChartArea();//创建图表区域
        private Series SJJ_ = new Series("");//创建线条
        private Title Ti = new Title("测速信息");
        public SpeedCheckPoint(Form1 frm1)
        {
            frm = frm1;
            InitializeComponent();
            this.chart1.ChartAreas.Add(CA_);
            this.chart1.Series.Add(SJJ_);
            this.CA_.AxisX.LabelAutoFitMaxFontSize = 9;
            this.CA_.AxisY.LabelAutoFitMaxFontSize = 9;
            this.CA_.AxisX.MajorGrid.LineColor = Color.LightGray;
            this.CA_.AxisY.MajorGrid.LineColor = Color.LightGray;
            this.chart1.Titles.Add(Ti);
            this.dgvAllData.Rows.Add("序", "说明", "起点", "终点", "字数", "时间", "速度", "击键", "码长", "回改");
            this.dgvAllData.Rows[0].Frozen = true;
            this.dgvAllData.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(7,131,162);
            this.dgvAllData.Rows[0].DefaultCellStyle.ForeColor = Color.White;
        }

        private void SpeedCheckPoint_Load(object sender, EventArgs e)
        {
            if (Glob.SpeedPointCount > 0) {
                this.Text = "第" + Glob.Pre_Cout + "段 测速信息 共" + Glob.SpeedPointCount + "个测速点";
                Ti.Text = "第" + Glob.Pre_Cout + "段 字数" + Glob.TextLen + " 测速信息";
                double GetSpeed = 0,MinSpeed = 500,MaxSpeed = 0;
                for (int i = 0; i < Glob.SpeedPointCount + 1; i++) {
                    if (i == 0)
                    {
                        int zis = Glob.SpeedPoint_[0] - Glob.TextJc;
                        GetSpeed = (zis * 60 / ((Glob.SpeedTime[0] <= 0) ? 1 : Glob.SpeedTime[0]));
                        //                                                                                                                                速度                                                       击键                       
                        this.dgvAllData.Rows.Add(i + 1,
                            frm.richTextBox1.Text.Substring(0,2), 
                            Glob.TextJc ,Glob.SpeedPoint_[0], 
                            zis + 1, 
                            Math.Round(Glob.SpeedTime[0], 3),
                            GetSpeed.ToString("0.00"),
                            (Glob.SpeedJs[0]/Glob.SpeedTime[0]).ToString("0.00"),
                            ((double)Glob.SpeedJs[0]/zis).ToString("0.00"),Glob.SpeedHg[0]);
                    }
                    else if (i < Glob.SpeedPointCount) {
                        int zis = Glob.SpeedPoint_[i] - Glob.SpeedPoint_[i - 1];
                        double shi = Glob.SpeedTime[i] - Glob.SpeedTime[i - 1];
                        GetSpeed = (zis * 60 / shi);
                        this.dgvAllData.Rows.Add(i + 1,
                                                 frm.richTextBox1.Text.Substring(Glob.SpeedPoint_[i - 1] + 1,2),
                                                 Glob.SpeedPoint_[i - 1] + 1 , Glob.SpeedPoint_[i],
                                                 zis - 1,
                                                 Math.Round(shi,3),
                                                 GetSpeed.ToString("0.00"),
                                                 ((Glob.SpeedJs[i] - Glob.SpeedJs[i-1])/shi).ToString("0.00"),
                                                 ((double)(Glob.SpeedJs[i] - Glob.SpeedJs[i - 1])/zis).ToString("0.00"),
                                                 Glob.SpeedHg[i] - Glob.SpeedHg[i - 1]);
                        
                    }
                    else if (i == Glob.SpeedPointCount) {
                        int zis  = Glob.TextLen - Glob.SpeedPoint_[i - 1];
                        double shi  = Glob.typeUseTime - Glob.SpeedTime[i - 1];
                        GetSpeed = (zis * 60 / shi);
                        int GetNow = Glob.SpeedPoint_[i - 1] + 1;
                        this.dgvAllData.Rows.Add(i + 1,
                            frm.richTextBox1.Text.Substring(GetNow, ((GetNow + 2 > Glob.TextLen) ? Glob.TextLen - GetNow : 2)),
                                                 Glob.SpeedPoint_[i - 1] + 1, Glob.TextLen,
                                                 zis - 1,
                                                 Math.Round(shi, 3),//时间
                                                 GetSpeed.ToString("0.00"),//速度
                                                 ((Glob.TextJs - Glob.SpeedJs[i - 1]) / shi).ToString("0.00"),//击键
                                                 ((double)(Glob.TextJs - Glob.SpeedJs[i - 1]) / zis).ToString("0.00"),
                                                 Glob.TextHg - Glob.SpeedHg[i - 1]);
                    }
                    //double Speed = double.Parse(this.dgvAllData.Rows[this.dgvAllData.Rows.Count - 1].Cells[5].Value.ToString());
                    this.SJJ_.Points.AddXY(this.dgvAllData.Rows[this.dgvAllData.Rows.Count - 1].Cells[1].Value,GetSpeed);
                    MinSpeed = Math.Min(GetSpeed,MinSpeed);
                    MaxSpeed = Math.Max(GetSpeed,MaxSpeed);
                    if (GetSpeed < Glob.TextSpeed) {
                        this.dgvAllData.Rows[this.dgvAllData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255,208,235);
                    }
                }
                int Min = (int)(MinSpeed - 10)/100 * 100;
                this.CA_.AxisY.Minimum = Min < 0 ? 0 : Min;//设定曲线最小值
                this.dgvAllData.Rows.Add(Glob.SpeedPointCount + 2,
                                         "全文",
                                         Glob.TextJc , Glob.TextLen, 
                                         Glob.TextLen - Glob.TextJc,
                                         Glob.typeUseTime.ToString("0.000"),
                                         Glob.TextSpeed,
                                         Glob.Textjj.ToString("0.00"),
                                         Glob.Textmc.ToString("0.00"),
                                         Glob.TextHg);
                this.dgvAllData.Rows[this.dgvAllData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(235,255,251);
                this.SJJ_.Points.AddXY("全文", Glob.TextSpeed);
                this.dgvAllData.ClearSelection();
                ReSetSize();//根据表格重设窗口大小
            }
        }

        //得到当前段的文字
        private void dgvAllData_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Glob.SpeedPointCount > 0) {
                int Index = e.RowIndex;
                if (Index > 0) {
                    if (Index > 0 && Index <= this.dgvAllData.Rows.Count - 1) {
                        this.richTextBox1.Text = frm.richTextBox1.Text.Substring(
                            (int)this.dgvAllData.Rows[Index].Cells[2].Value, 
                            (int)this.dgvAllData.Rows[Index].Cells[4].Value
                            );
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //重打当前
        private void btnReTypeNow_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.TextLength > 0)
            {
                frm.richTextBox1.Text = this.richTextBox1.Text;
                Glob.SpeedPoint_ = new int[10];//测速点控制
                Glob.SpeedTime = new double[10];//测速点时间控制
                Glob.SpeedJs = new int[10];//键数
                Glob.SpeedHg = new int[10];//回改
                Glob.SpeedPointCount = 0;//测速点数量控制
                frm.GetInfo();
                frm.F3();
                this.Close();
            }
        }

        //复制截图
        private void btnCopyPic_Click(object sender, EventArgs e)
        {
            GetPic();
        }

        private void btnSendPic_Click(object sender, EventArgs e)
        {
            GetPic();
            frm.SendClipBoardToQQ();
        }

        private void GetPic() {
            int getH = this.dgvAllData.Rows.GetRowsHeight(DataGridViewElementStates.None) - this.dgvAllData.Height;// this.dgvAllData.Rows[0].Height * this.dgvAllData.RowCount - this.dgvAllData.Height + 10;
            int getW = this.dgvAllData.Columns.GetColumnsWidth(DataGridViewElementStates.None) - this.dgvAllData.Width;
            using (Bitmap bmp = new Bitmap(this.Width + getW - 5, this.dgvAllData.Height + getH + 36))
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.DimGray);
                Font F = new Font("宋体", 9f);
                string title = this.Ti.Text;
                string detail = Glob.Form + "(" + Glob.Instration.Trim() + Glob.Ver + ")";
                string time = DateTime.Now.ToLongTimeString();
                SizeF detail_Sf = g.MeasureString(detail, F);
                SizeF title_Sf = g.MeasureString(title, F);
                g.DrawString(title, F, Brushes.Wheat, bmp.Width / 2 - title_Sf.Width / 2, 4);
                g.DrawString(detail, F, Brushes.Wheat, bmp.Width - detail_Sf.Width, bmp.Height - 14);
                g.DrawString(time,F,Brushes.Wheat,0,bmp.Height - 14);
                //g.FillRectangle(Brushes.White,new Rectangle(1,18,bmp.Width - 2,bmp.Height - 38));
                this.dgvAllData.DrawToBitmap(bmp, new Rectangle(1, 18, this.dgvAllData.Width + getW + 1, this.dgvAllData.Height + getH + 1));
                Clipboard.SetImage(bmp);
            }
        }
        /// <summary>
        /// 根据表格大小重设窗口大小
        /// </summary>
        private void ReSetSize() {
            int getH = this.dgvAllData.Rows.GetRowsHeight(DataGridViewElementStates.None) - this.dgvAllData.Height;// this.dgvAllData.Rows[0].Height * this.dgvAllData.RowCount - this.dgvAllData.Height + 10;
            int getW = 0;
            getW = this.dgvAllData.Columns.GetColumnsWidth(DataGridViewElementStates.None) - this.dgvAllData.Width;
            this.Size = new Size(this.Width + getW + 2, this.Height + getH + 1);
        }

        private void dgvAllData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor; //选中的时候，单元格颜色不变
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
            }
        }
    }
}
