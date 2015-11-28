namespace WindowsFormsApplication2.检查更新
{
    partial class UpgradePro
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
            this.btnCheckUpgrade = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.btnSeeContext = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheckUpgrade
            // 
            this.btnCheckUpgrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckUpgrade.Location = new System.Drawing.Point(11, 289);
            this.btnCheckUpgrade.Name = "btnCheckUpgrade";
            this.btnCheckUpgrade.Size = new System.Drawing.Size(170, 73);
            this.btnCheckUpgrade.TabIndex = 0;
            this.btnCheckUpgrade.Text = "检查更新";
            this.btnCheckUpgrade.UseVisualStyleBackColor = true;
            this.btnCheckUpgrade.Click += new System.EventHandler(this.btnCheckUpgrade_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rtbInfo);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 271);
            this.panel1.TabIndex = 1;
            // 
            // rtbInfo
            // 
            this.rtbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(303, 271);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.Visible = false;
            this.rtbInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbInfo_LinkClicked);
            // 
            // btnSeeContext
            // 
            this.btnSeeContext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSeeContext.Enabled = false;
            this.btnSeeContext.Location = new System.Drawing.Point(187, 289);
            this.btnSeeContext.Name = "btnSeeContext";
            this.btnSeeContext.Size = new System.Drawing.Size(123, 73);
            this.btnSeeContext.TabIndex = 2;
            this.btnSeeContext.Text = "查看更新内容";
            this.btnSeeContext.UseVisualStyleBackColor = true;
            this.btnSeeContext.Click += new System.EventHandler(this.btnSeeContext_Click);
            // 
            // UpgradePro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(327, 374);
            this.Controls.Add(this.btnSeeContext);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCheckUpgrade);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpgradePro";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "程序更新";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckUpgrade;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSeeContext;
        private System.Windows.Forms.RichTextBox rtbInfo;
    }
}