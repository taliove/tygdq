namespace WindowsFormsApplication2
{
    partial class SpeedCheckPoint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAllData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReTypeNow = new System.Windows.Forms.Button();
            this.btnCopyPic = new System.Windows.Forms.Button();
            this.btnSendPic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAllData
            // 
            this.dgvAllData.AllowUserToAddRows = false;
            this.dgvAllData.AllowUserToDeleteRows = false;
            this.dgvAllData.AllowUserToResizeColumns = false;
            this.dgvAllData.AllowUserToResizeRows = false;
            this.dgvAllData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAllData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAllData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAllData.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvAllData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAllData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvAllData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllData.ColumnHeadersVisible = false;
            this.dgvAllData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column9,
            this.Column8,
            this.Column10,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvAllData.EnableHeadersVisualStyles = false;
            this.dgvAllData.GridColor = System.Drawing.Color.Black;
            this.dgvAllData.Location = new System.Drawing.Point(0, 0);
            this.dgvAllData.MultiSelect = false;
            this.dgvAllData.Name = "dgvAllData";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllData.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvAllData.RowHeadersVisible = false;
            this.dgvAllData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.White;
            this.dgvAllData.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvAllData.RowTemplate.Height = 18;
            this.dgvAllData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllData.Size = new System.Drawing.Size(336, 140);
            this.dgvAllData.TabIndex = 7;
            this.dgvAllData.TabStop = false;
            this.dgvAllData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAllData_CellFormatting);
            this.dgvAllData.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllData_RowEnter);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序";
            this.Column1.Name = "Column1";
            this.Column1.Width = 5;
            // 
            // Column9
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle20;
            this.Column9.HeaderText = "说明";
            this.Column9.Name = "Column9";
            this.Column9.Width = 5;
            // 
            // Column8
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle21;
            this.Column8.HeaderText = "间隔起点";
            this.Column8.Name = "Column8";
            this.Column8.Width = 5;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "间隔终点";
            this.Column10.Name = "Column10";
            this.Column10.Width = 5;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "字数";
            this.Column2.Name = "Column2";
            this.Column2.Width = 5;
            // 
            // Column3
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle22;
            this.Column3.HeaderText = "时间";
            this.Column3.Name = "Column3";
            this.Column3.Width = 5;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "速度";
            this.Column4.Name = "Column4";
            this.Column4.Width = 5;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "击键";
            this.Column5.Name = "Column5";
            this.Column5.Width = 5;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "码长";
            this.Column6.Name = "Column6";
            this.Column6.Width = 5;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "回改";
            this.Column7.Name = "Column7";
            this.Column7.Width = 5;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.Location = new System.Drawing.Point(0, 212);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(336, 132);
            this.chart1.TabIndex = 2;
            this.chart1.TabStop = false;
            this.chart1.Text = "chart1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(0, 141);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(336, 70);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "点击上面表格显示测速段内容...";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(264, 349);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(56, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReTypeNow
            // 
            this.btnReTypeNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReTypeNow.Location = new System.Drawing.Point(3, 349);
            this.btnReTypeNow.Name = "btnReTypeNow";
            this.btnReTypeNow.Size = new System.Drawing.Size(69, 23);
            this.btnReTypeNow.TabIndex = 2;
            this.btnReTypeNow.Text = "重打当前";
            this.btnReTypeNow.UseVisualStyleBackColor = true;
            this.btnReTypeNow.Click += new System.EventHandler(this.btnReTypeNow_Click);
            // 
            // btnCopyPic
            // 
            this.btnCopyPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyPic.Location = new System.Drawing.Point(75, 349);
            this.btnCopyPic.Name = "btnCopyPic";
            this.btnCopyPic.Size = new System.Drawing.Size(64, 23);
            this.btnCopyPic.TabIndex = 1;
            this.btnCopyPic.Text = "复制截图";
            this.btnCopyPic.UseVisualStyleBackColor = true;
            this.btnCopyPic.Click += new System.EventHandler(this.btnCopyPic_Click);
            // 
            // btnSendPic
            // 
            this.btnSendPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendPic.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendPic.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSendPic.Location = new System.Drawing.Point(142, 349);
            this.btnSendPic.Name = "btnSendPic";
            this.btnSendPic.Size = new System.Drawing.Size(68, 23);
            this.btnSendPic.TabIndex = 8;
            this.btnSendPic.Text = "发送截图";
            this.btnSendPic.UseVisualStyleBackColor = true;
            this.btnSendPic.Click += new System.EventHandler(this.btnSendPic_Click);
            // 
            // SpeedCheckPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.ClientSize = new System.Drawing.Size(336, 375);
            this.Controls.Add(this.btnSendPic);
            this.Controls.Add(this.btnCopyPic);
            this.Controls.Add(this.btnReTypeNow);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dgvAllData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "SpeedCheckPoint";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测速信息";
            this.Load += new System.EventHandler(this.SpeedCheckPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReTypeNow;
        private System.Windows.Forms.Button btnCopyPic;
        private System.Windows.Forms.Button btnSendPic;
    }
}