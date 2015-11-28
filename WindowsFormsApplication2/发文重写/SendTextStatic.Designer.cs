namespace WindowsFormsApplication2
{
    partial class SendTextStatic
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
            this.components = new System.ComponentModel.Container();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxSingleTest = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblNowIni = new System.Windows.Forms.Label();
            this.lblAll = new System.Windows.Forms.Label();
            this.btnChangePreCout = new System.Windows.Forms.Button();
            this.btnCancelTime = new System.Windows.Forms.Button();
            this.lblNowTime = new System.Windows.Forms.Label();
            this.btnSendTime = new System.Windows.Forms.Button();
            this.btnOnceSC = new System.Windows.Forms.Button();
            this.btnFixStart = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFixNowTitle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTextSources = new System.Windows.Forms.Label();
            this.tbxNowStart = new System.Windows.Forms.TextBox();
            this.lblTextStyle = new System.Windows.Forms.Label();
            this.lblSendCounted = new System.Windows.Forms.Label();
            this.lblSendPCounted = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblLeastCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxSendC = new System.Windows.Forms.TextBox();
            this.tbxSendTime = new System.Windows.Forms.TextBox();
            this.tbxNowStartCount = new System.Windows.Forms.TextBox();
            this.gbstatic = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbstatic.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.DarkRed;
            this.btnStop.Location = new System.Drawing.Point(153, 350);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止发文";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存为配置";
            this.toolTip1.SetToolTip(this.btnSave, "将当前的发文状态保存为配置");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxSingleTest
            // 
            this.cbxSingleTest.AutoSize = true;
            this.cbxSingleTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSingleTest.Location = new System.Drawing.Point(12, 327);
            this.cbxSingleTest.Name = "cbxSingleTest";
            this.cbxSingleTest.Size = new System.Drawing.Size(45, 16);
            this.cbxSingleTest.TabIndex = 3;
            this.cbxSingleTest.Text = "独练";
            this.toolTip1.SetToolTip(this.cbxSingleTest, "点击转换独练与分享练习模式");
            this.cbxSingleTest.UseVisualStyleBackColor = true;
            this.cbxSingleTest.CheckedChanged += new System.EventHandler(this.cbxSingleTest_CheckedChanged);
            // 
            // lblNowIni
            // 
            this.lblNowIni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNowIni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNowIni.Location = new System.Drawing.Point(68, 0);
            this.lblNowIni.Name = "lblNowIni";
            this.lblNowIni.Size = new System.Drawing.Size(49, 20);
            this.lblNowIni.TabIndex = 1;
            this.lblNowIni.Text = "无";
            this.lblNowIni.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblNowIni, "当前配置序列，如果保存则会覆盖");
            // 
            // lblAll
            // 
            this.lblAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAll.Location = new System.Drawing.Point(180, 0);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(53, 20);
            this.lblAll.TabIndex = 3;
            this.lblAll.Text = "0";
            this.lblAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblAll, "已保存的配置总项目数量");
            // 
            // btnChangePreCout
            // 
            this.btnChangePreCout.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnChangePreCout.Location = new System.Drawing.Point(203, 257);
            this.btnChangePreCout.Margin = new System.Windows.Forms.Padding(0, 4, 0, 3);
            this.btnChangePreCout.Name = "btnChangePreCout";
            this.btnChangePreCout.Size = new System.Drawing.Size(24, 19);
            this.btnChangePreCout.TabIndex = 30;
            this.btnChangePreCout.Text = "修";
            this.toolTip1.SetToolTip(this.btnChangePreCout, "修改当前的段号");
            this.btnChangePreCout.UseVisualStyleBackColor = true;
            this.btnChangePreCout.Click += new System.EventHandler(this.btnChangePreCout_Click);
            // 
            // btnCancelTime
            // 
            this.btnCancelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancelTime.Location = new System.Drawing.Point(203, 233);
            this.btnCancelTime.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnCancelTime.Name = "btnCancelTime";
            this.btnCancelTime.Size = new System.Drawing.Size(24, 19);
            this.btnCancelTime.TabIndex = 27;
            this.btnCancelTime.Text = "停";
            this.toolTip1.SetToolTip(this.btnCancelTime, "取消周期发文");
            this.btnCancelTime.UseVisualStyleBackColor = true;
            this.btnCancelTime.TextChanged += new System.EventHandler(this.btnCancelTime_TextChanged);
            this.btnCancelTime.Click += new System.EventHandler(this.btnCancelTime_Click);
            // 
            // lblNowTime
            // 
            this.lblNowTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNowTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblNowTime.Location = new System.Drawing.Point(65, 233);
            this.lblNowTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblNowTime.Name = "lblNowTime";
            this.lblNowTime.Size = new System.Drawing.Size(135, 18);
            this.lblNowTime.TabIndex = 26;
            this.lblNowTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblNowTime, "点击我减少一秒时间");
            this.lblNowTime.Click += new System.EventHandler(this.lblNowTime_Click);
            // 
            // btnSendTime
            // 
            this.btnSendTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSendTime.Location = new System.Drawing.Point(203, 210);
            this.btnSendTime.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnSendTime.Name = "btnSendTime";
            this.btnSendTime.Size = new System.Drawing.Size(24, 19);
            this.btnSendTime.TabIndex = 24;
            this.btnSendTime.Text = "修";
            this.toolTip1.SetToolTip(this.btnSendTime, "修改当前周期，修改后重新计时");
            this.btnSendTime.UseVisualStyleBackColor = true;
            this.btnSendTime.Click += new System.EventHandler(this.btnSendTime_Click);
            // 
            // btnOnceSC
            // 
            this.btnOnceSC.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOnceSC.Location = new System.Drawing.Point(203, 187);
            this.btnOnceSC.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnOnceSC.Name = "btnOnceSC";
            this.btnOnceSC.Size = new System.Drawing.Size(24, 19);
            this.btnOnceSC.TabIndex = 21;
            this.btnOnceSC.Text = "修";
            this.toolTip1.SetToolTip(this.btnOnceSC, "修改当前一次发送字数");
            this.btnOnceSC.UseVisualStyleBackColor = true;
            this.btnOnceSC.Click += new System.EventHandler(this.btnOnceSC_Click);
            // 
            // btnFixStart
            // 
            this.btnFixStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFixStart.Location = new System.Drawing.Point(203, 164);
            this.btnFixStart.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnFixStart.Name = "btnFixStart";
            this.btnFixStart.Size = new System.Drawing.Size(24, 19);
            this.btnFixStart.TabIndex = 17;
            this.btnFixStart.Text = "修";
            this.toolTip1.SetToolTip(this.btnFixStart, "修改当前发文起始点");
            this.btnFixStart.UseVisualStyleBackColor = true;
            this.btnFixStart.Click += new System.EventHandler(this.btnFixStart_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(3, 164);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "当前标记";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label8, "文章起始");
            // 
            // btnFixNowTitle
            // 
            this.btnFixNowTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFixNowTitle.Location = new System.Drawing.Point(203, 3);
            this.btnFixNowTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnFixNowTitle.Name = "btnFixNowTitle";
            this.btnFixNowTitle.Size = new System.Drawing.Size(24, 19);
            this.btnFixNowTitle.TabIndex = 18;
            this.btnFixNowTitle.Text = "修";
            this.toolTip1.SetToolTip(this.btnFixNowTitle, "修改当前文章标题");
            this.btnFixNowTitle.UseVisualStyleBackColor = true;
            this.btnFixNowTitle.Click += new System.EventHandler(this.btnFixNowTitle_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.lblAll);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.lblNowIni);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 21);
            this.panel1.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(116, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "总序列：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "当前序列：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.btnFixNowTitle, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbxTitle, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblTextSources, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbxNowStart, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblTextStyle, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblSendCounted, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblSendPCounted, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalCount, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblLeastCount, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.btnFixStart, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.tbxSendC, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.btnOnceSC, 2, 8);
            this.tableLayoutPanel2.Controls.Add(this.tbxSendTime, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.btnSendTime, 2, 9);
            this.tableLayoutPanel2.Controls.Add(this.lblNowTime, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.btnCancelTime, 2, 10);
            this.tableLayoutPanel2.Controls.Add(this.tbxNowStartCount, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.btnChangePreCout, 2, 11);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 12;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(229, 279);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(3, 256);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 23);
            this.label12.TabIndex = 28;
            this.label12.Text = "当前段号";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 233);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 20);
            this.label11.TabIndex = 25;
            this.label11.Text = "周期计数";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 210);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "周期";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxTitle
            // 
            this.tbxTitle.BackColor = System.Drawing.Color.DarkGray;
            this.tbxTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxTitle.Font = new System.Drawing.Font("宋体", 9F);
            this.tbxTitle.Location = new System.Drawing.Point(65, 3);
            this.tbxTitle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tbxTitle.Name = "tbxTitle";
            this.tbxTitle.ReadOnly = true;
            this.tbxTitle.Size = new System.Drawing.Size(135, 21);
            this.tbxTitle.TabIndex = 12;
            this.tbxTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "文章标题";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "文章来源";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "文章类型";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "已发字数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 95);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "已发段数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 118);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "总字数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 141);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "剩余字数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextSources
            // 
            this.lblTextSources.AutoSize = true;
            this.lblTextSources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTextSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextSources.Font = new System.Drawing.Font("宋体", 9F);
            this.lblTextSources.Location = new System.Drawing.Point(65, 26);
            this.lblTextSources.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblTextSources.Name = "lblTextSources";
            this.lblTextSources.Size = new System.Drawing.Size(135, 18);
            this.lblTextSources.TabIndex = 10;
            this.lblTextSources.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxNowStart
            // 
            this.tbxNowStart.BackColor = System.Drawing.Color.DarkGray;
            this.tbxNowStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxNowStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxNowStart.Font = new System.Drawing.Font("宋体", 9F);
            this.tbxNowStart.Location = new System.Drawing.Point(65, 164);
            this.tbxNowStart.Name = "tbxNowStart";
            this.tbxNowStart.ReadOnly = true;
            this.tbxNowStart.Size = new System.Drawing.Size(135, 21);
            this.tbxNowStart.TabIndex = 11;
            this.tbxNowStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxNowStart.TextChanged += new System.EventHandler(this.tbxNowStart_TextChanged);
            this.tbxNowStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxNowStart_KeyPress);
            // 
            // lblTextStyle
            // 
            this.lblTextStyle.AutoSize = true;
            this.lblTextStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTextStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextStyle.Font = new System.Drawing.Font("宋体", 9F);
            this.lblTextStyle.Location = new System.Drawing.Point(65, 49);
            this.lblTextStyle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblTextStyle.Name = "lblTextStyle";
            this.lblTextStyle.Size = new System.Drawing.Size(135, 18);
            this.lblTextStyle.TabIndex = 12;
            this.lblTextStyle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSendCounted
            // 
            this.lblSendCounted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSendCounted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSendCounted.Font = new System.Drawing.Font("宋体", 9F);
            this.lblSendCounted.Location = new System.Drawing.Point(65, 72);
            this.lblSendCounted.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblSendCounted.Name = "lblSendCounted";
            this.lblSendCounted.Size = new System.Drawing.Size(135, 18);
            this.lblSendCounted.TabIndex = 13;
            this.lblSendCounted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSendPCounted
            // 
            this.lblSendPCounted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSendPCounted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSendPCounted.Font = new System.Drawing.Font("宋体", 9F);
            this.lblSendPCounted.Location = new System.Drawing.Point(65, 95);
            this.lblSendPCounted.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblSendPCounted.Name = "lblSendPCounted";
            this.lblSendPCounted.Size = new System.Drawing.Size(135, 18);
            this.lblSendPCounted.TabIndex = 14;
            this.lblSendPCounted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCount.Font = new System.Drawing.Font("宋体", 9F);
            this.lblTotalCount.Location = new System.Drawing.Point(65, 118);
            this.lblTotalCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(135, 18);
            this.lblTotalCount.TabIndex = 15;
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeastCount
            // 
            this.lblLeastCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeastCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLeastCount.Font = new System.Drawing.Font("宋体", 9F);
            this.lblLeastCount.Location = new System.Drawing.Point(65, 141);
            this.lblLeastCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblLeastCount.Name = "lblLeastCount";
            this.lblLeastCount.Size = new System.Drawing.Size(135, 18);
            this.lblLeastCount.TabIndex = 16;
            this.lblLeastCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeastCount.TextChanged += new System.EventHandler(this.lblLeastCount_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 187);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "发送字数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxSendC
            // 
            this.tbxSendC.BackColor = System.Drawing.Color.DarkGray;
            this.tbxSendC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxSendC.Font = new System.Drawing.Font("宋体", 9F);
            this.tbxSendC.Location = new System.Drawing.Point(65, 187);
            this.tbxSendC.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tbxSendC.Name = "tbxSendC";
            this.tbxSendC.ReadOnly = true;
            this.tbxSendC.Size = new System.Drawing.Size(135, 21);
            this.tbxSendC.TabIndex = 20;
            this.tbxSendC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxSendC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxSendC_KeyPress);
            // 
            // tbxSendTime
            // 
            this.tbxSendTime.BackColor = System.Drawing.Color.DarkGray;
            this.tbxSendTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxSendTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxSendTime.Font = new System.Drawing.Font("宋体", 9F);
            this.tbxSendTime.Location = new System.Drawing.Point(65, 210);
            this.tbxSendTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tbxSendTime.Name = "tbxSendTime";
            this.tbxSendTime.ReadOnly = true;
            this.tbxSendTime.Size = new System.Drawing.Size(135, 21);
            this.tbxSendTime.TabIndex = 23;
            this.tbxSendTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxNowStartCount
            // 
            this.tbxNowStartCount.BackColor = System.Drawing.Color.DarkGray;
            this.tbxNowStartCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxNowStartCount.Font = new System.Drawing.Font("宋体", 9F);
            this.tbxNowStartCount.Location = new System.Drawing.Point(65, 256);
            this.tbxNowStartCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tbxNowStartCount.Name = "tbxNowStartCount";
            this.tbxNowStartCount.ReadOnly = true;
            this.tbxNowStartCount.Size = new System.Drawing.Size(135, 21);
            this.tbxNowStartCount.TabIndex = 29;
            this.tbxNowStartCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbstatic
            // 
            this.gbstatic.Controls.Add(this.tableLayoutPanel2);
            this.gbstatic.Location = new System.Drawing.Point(0, 24);
            this.gbstatic.Name = "gbstatic";
            this.gbstatic.Size = new System.Drawing.Size(235, 299);
            this.gbstatic.TabIndex = 0;
            this.gbstatic.TabStop = false;
            this.gbstatic.Text = "当前发文";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(71, 327);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 16);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "自动";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label15.Location = new System.Drawing.Point(6, 345);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(220, 1);
            this.label15.TabIndex = 32;
            // 
            // SendTextStatic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 373);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cbxSingleTest);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbstatic);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendTextStatic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添雨跟打器发文状态";
            this.Load += new System.EventHandler(this.SendTextStatic_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbstatic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbxSingleTest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblNowIni;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnFixNowTitle;
        private System.Windows.Forms.TextBox tbxTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTextSources;
        public System.Windows.Forms.TextBox tbxNowStart;
        private System.Windows.Forms.Label lblTextStyle;
        public System.Windows.Forms.Label lblSendCounted;
        public System.Windows.Forms.Label lblSendPCounted;
        private System.Windows.Forms.Label lblTotalCount;
        public System.Windows.Forms.Label lblLeastCount;
        private System.Windows.Forms.Button btnFixStart;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tbxSendC;
        private System.Windows.Forms.Button btnOnceSC;
        private System.Windows.Forms.TextBox tbxSendTime;
        private System.Windows.Forms.Button btnSendTime;
        public System.Windows.Forms.Label lblNowTime;
        private System.Windows.Forms.Button btnCancelTime;
        public System.Windows.Forms.TextBox tbxNowStartCount;
        private System.Windows.Forms.Button btnChangePreCout;
        private System.Windows.Forms.GroupBox gbstatic;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label15;
    }
}