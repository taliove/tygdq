namespace WindowsFormsApplication2
{
    partial class SpeedCheckOut
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxC = new System.Windows.Forms.TextBox();
            this.btnOK = new WindowsFormsApplication2.NewButton();
            this.btnCancel = new WindowsFormsApplication2.NewButton();
            this.btnReget = new WindowsFormsApplication2.NewButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkedListBox1.ForeColor = System.Drawing.Color.White;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(89, 140);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "寻找标记：";
            // 
            // tbxP
            // 
            this.tbxP.Location = new System.Drawing.Point(158, 4);
            this.tbxP.MaxLength = 2;
            this.tbxP.Name = "tbxP";
            this.tbxP.Size = new System.Drawing.Size(70, 21);
            this.tbxP.TabIndex = 2;
            this.tbxP.Text = "：";
            this.tbxP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "提取数量：";
            // 
            // tbxC
            // 
            this.tbxC.Location = new System.Drawing.Point(158, 33);
            this.tbxC.MaxLength = 1;
            this.tbxC.Name = "tbxC";
            this.tbxC.ReadOnly = true;
            this.tbxC.Size = new System.Drawing.Size(70, 21);
            this.tbxC.TabIndex = 4;
            this.tbxC.Text = "2";
            this.tbxC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Gray;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(191, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(40, 16);
            this.btnOK.SS = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.进入背景色 = System.Drawing.Color.OrangeRed;
            this.btnOK.默认背景色 = System.Drawing.Color.Gray;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(198, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(41, 16);
            this.btnCancel.SS = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.进入背景色 = System.Drawing.Color.Red;
            this.btnCancel.默认背景色 = System.Drawing.Color.Gray;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReget
            // 
            this.btnReget.BackColor = System.Drawing.Color.Gray;
            this.btnReget.ForeColor = System.Drawing.Color.White;
            this.btnReget.Location = new System.Drawing.Point(92, 84);
            this.btnReget.Name = "btnReget";
            this.btnReget.Size = new System.Drawing.Size(133, 20);
            this.btnReget.SS = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReget.TabIndex = 7;
            this.btnReget.Text = "重新获取";
            this.btnReget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnReget.进入背景色 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnReget.默认背景色 = System.Drawing.Color.Gray;
            this.btnReget.Click += new System.EventHandler(this.btnReget_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "标记前第三项 = 测速点";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnReget);
            this.panel1.Controls.Add(this.tbxP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.tbxC);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(8, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 140);
            this.panel1.TabIndex = 9;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "测试点自动寻找";
            // 
            // SpeedCheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(247, 174);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpeedCheckOut";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测速点自动设置";
            this.Load += new System.EventHandler(this.SpeedCheckOut_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpeedCheckOut_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxC;
        private NewButton btnOK;
        private NewButton btnCancel;
        private NewButton btnReget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
    }
}