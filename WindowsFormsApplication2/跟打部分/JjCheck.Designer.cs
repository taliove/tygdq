namespace WindowsFormsApplication2
{
    partial class JjCheck
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
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GetPIC = new System.Windows.Forms.Button();
            this.GetText = new System.Windows.Forms.Button();
            this.SendText = new System.Windows.Forms.Button();
            this.CloseW = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(342, 148);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // GetPIC
            // 
            this.GetPIC.ForeColor = System.Drawing.Color.Green;
            this.GetPIC.Location = new System.Drawing.Point(3, 154);
            this.GetPIC.Name = "GetPIC";
            this.GetPIC.Size = new System.Drawing.Size(61, 23);
            this.GetPIC.TabIndex = 1;
            this.GetPIC.Text = "复制图表";
            this.GetPIC.UseVisualStyleBackColor = true;
            this.GetPIC.Click += new System.EventHandler(this.GetPIC_Click);
            // 
            // GetText
            // 
            this.GetText.Location = new System.Drawing.Point(66, 154);
            this.GetText.Name = "GetText";
            this.GetText.Size = new System.Drawing.Size(62, 23);
            this.GetText.TabIndex = 2;
            this.GetText.Text = "复制标题";
            this.GetText.UseVisualStyleBackColor = true;
            this.GetText.Click += new System.EventHandler(this.GetText_Click);
            // 
            // SendText
            // 
            this.SendText.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SendText.ForeColor = System.Drawing.Color.Maroon;
            this.SendText.Location = new System.Drawing.Point(131, 154);
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(83, 23);
            this.SendText.TabIndex = 3;
            this.SendText.Text = "发送截图";
            this.SendText.UseVisualStyleBackColor = true;
            this.SendText.Click += new System.EventHandler(this.SendText_Click);
            // 
            // CloseW
            // 
            this.CloseW.Location = new System.Drawing.Point(275, 154);
            this.CloseW.Name = "CloseW";
            this.CloseW.Size = new System.Drawing.Size(62, 23);
            this.CloseW.TabIndex = 4;
            this.CloseW.Text = "关闭";
            this.CloseW.UseVisualStyleBackColor = true;
            this.CloseW.Click += new System.EventHandler(this.Close_Click);
            // 
            // JjCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.ClientSize = new System.Drawing.Size(342, 181);
            this.Controls.Add(this.CloseW);
            this.Controls.Add(this.SendText);
            this.Controls.Add(this.GetText);
            this.Controls.Add(this.GetPIC);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JjCheck";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "击键分析";
            this.Load += new System.EventHandler(this.JjCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button GetPIC;
        private System.Windows.Forms.Button GetText;
        private System.Windows.Forms.Button SendText;
        private System.Windows.Forms.Button CloseW;
    }
}