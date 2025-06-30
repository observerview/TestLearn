namespace xwx.TDP.Library.Common.Forms
{
    partial class DevSettings<T>
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
            this.comboBoxInsAddr = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxSerAddr = new System.Windows.Forms.TextBox();
            this.checkBoxGlobal = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxRemote = new System.Windows.Forms.CheckBox();
            this.groupBoxFileSelect = new System.Windows.Forms.GroupBox();
            this.textBoxSwitchFile = new System.Windows.Forms.TextBox();
            this.buttonFileSelect = new System.Windows.Forms.Button();
            this.comboBoxInsType = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxFileSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxInsAddr
            // 
            this.comboBoxInsAddr.FormattingEnabled = true;
            this.comboBoxInsAddr.Items.AddRange(new object[] {
            "TCPIP0::172.27.45.100::INSTR",
            "GPIB0::10::INSTR",
            "ASRL1::INSTR"});
            this.comboBoxInsAddr.Location = new System.Drawing.Point(113, 88);
            this.comboBoxInsAddr.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxInsAddr.Name = "comboBoxInsAddr";
            this.comboBoxInsAddr.Size = new System.Drawing.Size(380, 23);
            this.comboBoxInsAddr.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 354);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 55);
            this.panel1.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(286, 15);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 29);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(394, 15);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 29);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "仪表地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "仪表类型：";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(510, 28);
            this.label1.TabIndex = 8;
            this.label1.Text = "仪表信息配置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxSerAddr);
            this.groupBox1.Controls.Add(this.checkBoxGlobal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.checkBoxRemote);
            this.groupBox1.Controls.Add(this.groupBoxFileSelect);
            this.groupBox1.Location = new System.Drawing.Point(16, 154);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(479, 184);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共享配置";
            // 
            // textBoxSerAddr
            // 
            this.textBoxSerAddr.Location = new System.Drawing.Point(152, 72);
            this.textBoxSerAddr.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSerAddr.Name = "textBoxSerAddr";
            this.textBoxSerAddr.Size = new System.Drawing.Size(317, 25);
            this.textBoxSerAddr.TabIndex = 2;
            // 
            // checkBoxGlobal
            // 
            this.checkBoxGlobal.AutoSize = true;
            this.checkBoxGlobal.Location = new System.Drawing.Point(8, 25);
            this.checkBoxGlobal.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxGlobal.Name = "checkBoxGlobal";
            this.checkBoxGlobal.Size = new System.Drawing.Size(149, 19);
            this.checkBoxGlobal.TabIndex = 0;
            this.checkBoxGlobal.Text = "使用全局信息配置";
            this.checkBoxGlobal.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "远程服务器地址：";
            // 
            // checkBoxRemote
            // 
            this.checkBoxRemote.AutoSize = true;
            this.checkBoxRemote.Location = new System.Drawing.Point(200, 25);
            this.checkBoxRemote.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRemote.Name = "checkBoxRemote";
            this.checkBoxRemote.Size = new System.Drawing.Size(134, 19);
            this.checkBoxRemote.TabIndex = 0;
            this.checkBoxRemote.Text = "使用远程服务器";
            this.checkBoxRemote.UseVisualStyleBackColor = true;
            // 
            // groupBoxFileSelect
            // 
            this.groupBoxFileSelect.Controls.Add(this.textBoxSwitchFile);
            this.groupBoxFileSelect.Controls.Add(this.buttonFileSelect);
            this.groupBoxFileSelect.Enabled = false;
            this.groupBoxFileSelect.Location = new System.Drawing.Point(0, 124);
            this.groupBoxFileSelect.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxFileSelect.Name = "groupBoxFileSelect";
            this.groupBoxFileSelect.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxFileSelect.Size = new System.Drawing.Size(479, 60);
            this.groupBoxFileSelect.TabIndex = 7;
            this.groupBoxFileSelect.TabStop = false;
            this.groupBoxFileSelect.Text = "开关文件";
            this.groupBoxFileSelect.Visible = false;
            // 
            // textBoxSwitchFile
            // 
            this.textBoxSwitchFile.Location = new System.Drawing.Point(9, 21);
            this.textBoxSwitchFile.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSwitchFile.Name = "textBoxSwitchFile";
            this.textBoxSwitchFile.Size = new System.Drawing.Size(407, 25);
            this.textBoxSwitchFile.TabIndex = 0;
            // 
            // buttonFileSelect
            // 
            this.buttonFileSelect.AutoEllipsis = true;
            this.buttonFileSelect.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonFileSelect.FlatAppearance.BorderSize = 0;
            this.buttonFileSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFileSelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonFileSelect.Location = new System.Drawing.Point(421, 21);
            this.buttonFileSelect.Margin = new System.Windows.Forms.Padding(0);
            this.buttonFileSelect.Name = "buttonFileSelect";
            this.buttonFileSelect.Size = new System.Drawing.Size(48, 26);
            this.buttonFileSelect.TabIndex = 1;
            this.buttonFileSelect.Text = "...";
            this.buttonFileSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFileSelect.UseVisualStyleBackColor = false;
            this.buttonFileSelect.Click += new System.EventHandler(this.buttonFileSelect_Click);
            // 
            // comboBoxInsType
            // 
            this.comboBoxInsType.FormattingEnabled = true;
            this.comboBoxInsType.Location = new System.Drawing.Point(113, 49);
            this.comboBoxInsType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxInsType.Name = "comboBoxInsType";
            this.comboBoxInsType.Size = new System.Drawing.Size(380, 23);
            this.comboBoxInsType.TabIndex = 12;
            // 
            // DevSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 409);
            this.Controls.Add(this.comboBoxInsAddr);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxInsType);
            this.Name = "DevSettings";
            this.Text = "DevSettings";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxFileSelect.ResumeLayout(false);
            this.groupBoxFileSelect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxInsAddr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxSerAddr;
        private System.Windows.Forms.CheckBox checkBoxGlobal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxRemote;
        private System.Windows.Forms.GroupBox groupBoxFileSelect;
        private System.Windows.Forms.TextBox textBoxSwitchFile;
        private System.Windows.Forms.Button buttonFileSelect;
        private System.Windows.Forms.ComboBox comboBoxInsType;
    }
}