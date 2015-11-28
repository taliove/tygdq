namespace WindowsFormsApplication2
{
    partial class 精五成绩生成
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
            this.label1 = new System.Windows.Forms.Label();
            this.期数 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.速度 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.击键 = new System.Windows.Forms.TextBox();
            this.码长 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.秋秋号 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.生成框 = new System.Windows.Forms.RichTextBox();
            this.复制 = new System.Windows.Forms.Button();
            this.发送 = new System.Windows.Forms.Button();
            this.关闭 = new System.Windows.Forms.Button();
            this.生成 = new System.Windows.Forms.Button();
            this.用时 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "您参加的是精五第";
            // 
            // 期数
            // 
            this.期数.Location = new System.Drawing.Point(108, 8);
            this.期数.MaxLength = 3;
            this.期数.Name = "期数";
            this.期数.Size = new System.Drawing.Size(49, 21);
            this.期数.TabIndex = 1;
            this.期数.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.期数_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "期比赛，您的成绩是：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "速度";
            // 
            // 速度
            // 
            this.速度.Enabled = false;
            this.速度.Location = new System.Drawing.Point(39, 42);
            this.速度.MaxLength = 6;
            this.速度.Name = "速度";
            this.速度.Size = new System.Drawing.Size(49, 21);
            this.速度.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "，击键";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "，码长";
            // 
            // 击键
            // 
            this.击键.Enabled = false;
            this.击键.Location = new System.Drawing.Point(136, 42);
            this.击键.MaxLength = 5;
            this.击键.Name = "击键";
            this.击键.Size = new System.Drawing.Size(49, 21);
            this.击键.TabIndex = 7;
            // 
            // 码长
            // 
            this.码长.Enabled = false;
            this.码长.Location = new System.Drawing.Point(231, 42);
            this.码长.MaxLength = 4;
            this.码长.Name = "码长";
            this.码长.Size = new System.Drawing.Size(49, 21);
            this.码长.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "您的QQ号码是：";
            // 
            // 秋秋号
            // 
            this.秋秋号.Location = new System.Drawing.Point(93, 74);
            this.秋秋号.MaxLength = 10;
            this.秋秋号.Name = "秋秋号";
            this.秋秋号.Size = new System.Drawing.Size(186, 21);
            this.秋秋号.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "您的跟打用时(秒)：";
            // 
            // 生成框
            // 
            this.生成框.Location = new System.Drawing.Point(7, 133);
            this.生成框.Name = "生成框";
            this.生成框.Size = new System.Drawing.Size(272, 98);
            this.生成框.TabIndex = 16;
            this.生成框.Text = "";
            // 
            // 复制
            // 
            this.复制.Location = new System.Drawing.Point(75, 289);
            this.复制.Name = "复制";
            this.复制.Size = new System.Drawing.Size(65, 23);
            this.复制.TabIndex = 13;
            this.复制.Text = "复制";
            this.复制.UseVisualStyleBackColor = true;
            this.复制.Click += new System.EventHandler(this.复制_Click);
            // 
            // 发送
            // 
            this.发送.Location = new System.Drawing.Point(144, 289);
            this.发送.Name = "发送";
            this.发送.Size = new System.Drawing.Size(65, 23);
            this.发送.TabIndex = 14;
            this.发送.Text = "发送至群";
            this.发送.UseVisualStyleBackColor = true;
            this.发送.Click += new System.EventHandler(this.发送_Click);
            // 
            // 关闭
            // 
            this.关闭.Location = new System.Drawing.Point(214, 289);
            this.关闭.Name = "关闭";
            this.关闭.Size = new System.Drawing.Size(65, 23);
            this.关闭.TabIndex = 15;
            this.关闭.Text = "关闭";
            this.关闭.UseVisualStyleBackColor = true;
            this.关闭.Click += new System.EventHandler(this.关闭_Click);
            // 
            // 生成
            // 
            this.生成.Location = new System.Drawing.Point(7, 289);
            this.生成.Name = "生成";
            this.生成.Size = new System.Drawing.Size(65, 23);
            this.生成.TabIndex = 12;
            this.生成.Text = "生成";
            this.生成.UseVisualStyleBackColor = true;
            this.生成.Click += new System.EventHandler(this.生成_Click);
            // 
            // 用时
            // 
            this.用时.Enabled = false;
            this.用时.Location = new System.Drawing.Point(116, 105);
            this.用时.MaxLength = 10;
            this.用时.Name = "用时";
            this.用时.Size = new System.Drawing.Size(163, 21);
            this.用时.TabIndex = 11;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(7, 237);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(271, 43);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "成绩须含有：速度、击键、码长、错字、键数、用时、回改、输入法、校验码、QQ号";
            // 
            // 精五成绩生成
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 315);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.用时);
            this.Controls.Add(this.生成);
            this.Controls.Add(this.关闭);
            this.Controls.Add(this.发送);
            this.Controls.Add(this.复制);
            this.Controls.Add(this.生成框);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.秋秋号);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.码长);
            this.Controls.Add(this.击键);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.速度);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.期数);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "精五成绩生成";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "精五成绩生成";
            this.Load += new System.EventHandler(this.精五成绩生成_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox 期数;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox 速度;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox 击键;
        private System.Windows.Forms.TextBox 码长;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox 秋秋号;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox 生成框;
        private System.Windows.Forms.Button 复制;
        private System.Windows.Forms.Button 发送;
        private System.Windows.Forms.Button 关闭;
        private System.Windows.Forms.Button 生成;
        private System.Windows.Forms.TextBox 用时;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}