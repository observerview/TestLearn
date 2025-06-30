namespace xwx.TDP.Editor
{
    partial class SequenceInfoEditor
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_OKCancel = new System.Windows.Forms.TableLayoutPanel();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel_Info = new System.Windows.Forms.TableLayoutPanel();
            this.label_DisplayName = new System.Windows.Forms.Label();
            this.label_Author = new System.Windows.Forms.Label();
            this.label_CreatedTime = new System.Windows.Forms.Label();
            this.label_ModifiedTime = new System.Windows.Forms.Label();
            this.label_ExecutionMode = new System.Windows.Forms.Label();
            this.tableLayoutPanel_ExecutionMode = new System.Windows.Forms.TableLayoutPanel();
            this.label_ExecutionMode_Error = new System.Windows.Forms.Label();
            this.label_ExecutionMode_OK = new System.Windows.Forms.Label();
            this.label_ExecutionMode_Error_Retry = new System.Windows.Forms.Label();
            this.label_ExecutionMode_OK_Retry = new System.Windows.Forms.Label();
            this.comboBox_ExecutionMode_Error = new System.Windows.Forms.ComboBox();
            this.comboBox_ExecutionMode_OK = new System.Windows.Forms.ComboBox();
            this.numericUpDown_ExectionMode_Error = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ExectionMode_OK = new System.Windows.Forms.NumericUpDown();
            this.textBox_DisplayName = new System.Windows.Forms.TextBox();
            this.textBox_Author = new System.Windows.Forms.TextBox();
            this.textBox_CreatedTime = new System.Windows.Forms.TextBox();
            this.textBox_ModifiedTime = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel_OKCancel.SuspendLayout();
            this.tableLayoutPanel_Info.SuspendLayout();
            this.tableLayoutPanel_ExecutionMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_Error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_OK)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel_OKCancel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.textBox_Description, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel_Info, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(597, 407);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // tableLayoutPanel_OKCancel
            // 
            this.tableLayoutPanel_OKCancel.ColumnCount = 2;
            this.tableLayoutPanel_OKCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_OKCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_OKCancel.Controls.Add(this.button_Cancel, 0, 0);
            this.tableLayoutPanel_OKCancel.Controls.Add(this.button_OK, 1, 0);
            this.tableLayoutPanel_OKCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_OKCancel.Location = new System.Drawing.Point(3, 350);
            this.tableLayoutPanel_OKCancel.Name = "tableLayoutPanel_OKCancel";
            this.tableLayoutPanel_OKCancel.RowCount = 1;
            this.tableLayoutPanel_OKCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_OKCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel_OKCancel.Size = new System.Drawing.Size(591, 54);
            this.tableLayoutPanel_OKCancel.TabIndex = 1;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(80, 10);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(80, 10, 80, 10);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(135, 34);
            this.button_Cancel.TabIndex = 0;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(375, 10);
            this.button_OK.Margin = new System.Windows.Forms.Padding(80, 10, 80, 10);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(136, 34);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // textBox_Description
            // 
            this.textBox_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Description.Location = new System.Drawing.Point(3, 253);
            this.textBox_Description.Multiline = true;
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.Size = new System.Drawing.Size(591, 91);
            this.textBox_Description.TabIndex = 2;
            // 
            // tableLayoutPanel_Info
            // 
            this.tableLayoutPanel_Info.ColumnCount = 2;
            this.tableLayoutPanel_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel_Info.Controls.Add(this.label_DisplayName, 0, 0);
            this.tableLayoutPanel_Info.Controls.Add(this.label_Author, 0, 1);
            this.tableLayoutPanel_Info.Controls.Add(this.label_CreatedTime, 0, 2);
            this.tableLayoutPanel_Info.Controls.Add(this.label_ModifiedTime, 0, 3);
            this.tableLayoutPanel_Info.Controls.Add(this.label_ExecutionMode, 0, 4);
            this.tableLayoutPanel_Info.Controls.Add(this.tableLayoutPanel_ExecutionMode, 1, 4);
            this.tableLayoutPanel_Info.Controls.Add(this.textBox_DisplayName, 1, 0);
            this.tableLayoutPanel_Info.Controls.Add(this.textBox_Author, 1, 1);
            this.tableLayoutPanel_Info.Controls.Add(this.textBox_CreatedTime, 1, 2);
            this.tableLayoutPanel_Info.Controls.Add(this.textBox_ModifiedTime, 1, 3);
            this.tableLayoutPanel_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Info.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_Info.Name = "tableLayoutPanel_Info";
            this.tableLayoutPanel_Info.RowCount = 5;
            this.tableLayoutPanel_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel_Info.Size = new System.Drawing.Size(591, 244);
            this.tableLayoutPanel_Info.TabIndex = 3;
            // 
            // label_DisplayName
            // 
            this.label_DisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_DisplayName.AutoSize = true;
            this.label_DisplayName.Location = new System.Drawing.Point(3, 10);
            this.label_DisplayName.Name = "label_DisplayName";
            this.label_DisplayName.Size = new System.Drawing.Size(112, 15);
            this.label_DisplayName.TabIndex = 0;
            this.label_DisplayName.Text = "Name";
            // 
            // label_Author
            // 
            this.label_Author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Author.AutoSize = true;
            this.label_Author.Location = new System.Drawing.Point(3, 46);
            this.label_Author.Name = "label_Author";
            this.label_Author.Size = new System.Drawing.Size(112, 15);
            this.label_Author.TabIndex = 1;
            this.label_Author.Text = "Author";
            // 
            // label_CreatedTime
            // 
            this.label_CreatedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_CreatedTime.AutoSize = true;
            this.label_CreatedTime.Location = new System.Drawing.Point(3, 82);
            this.label_CreatedTime.Name = "label_CreatedTime";
            this.label_CreatedTime.Size = new System.Drawing.Size(112, 15);
            this.label_CreatedTime.TabIndex = 2;
            this.label_CreatedTime.Text = "CreatedTime";
            // 
            // label_ModifiedTime
            // 
            this.label_ModifiedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ModifiedTime.AutoSize = true;
            this.label_ModifiedTime.Location = new System.Drawing.Point(3, 118);
            this.label_ModifiedTime.Name = "label_ModifiedTime";
            this.label_ModifiedTime.Size = new System.Drawing.Size(112, 15);
            this.label_ModifiedTime.TabIndex = 3;
            this.label_ModifiedTime.Text = "ModifiedTime";
            // 
            // label_ExecutionMode
            // 
            this.label_ExecutionMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode.AutoSize = true;
            this.label_ExecutionMode.Location = new System.Drawing.Point(3, 186);
            this.label_ExecutionMode.Name = "label_ExecutionMode";
            this.label_ExecutionMode.Size = new System.Drawing.Size(112, 15);
            this.label_ExecutionMode.TabIndex = 4;
            this.label_ExecutionMode.Text = "ExecutionMode";
            // 
            // tableLayoutPanel_ExecutionMode
            // 
            this.tableLayoutPanel_ExecutionMode.ColumnCount = 4;
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_Error, 0, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_OK, 0, 1);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_Error_Retry, 2, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_OK_Retry, 2, 1);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.comboBox_ExecutionMode_Error, 1, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.comboBox_ExecutionMode_OK, 1, 1);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.numericUpDown_ExectionMode_Error, 3, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.numericUpDown_ExectionMode_OK, 3, 1);
            this.tableLayoutPanel_ExecutionMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_ExecutionMode.Location = new System.Drawing.Point(121, 147);
            this.tableLayoutPanel_ExecutionMode.Name = "tableLayoutPanel_ExecutionMode";
            this.tableLayoutPanel_ExecutionMode.RowCount = 2;
            this.tableLayoutPanel_ExecutionMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ExecutionMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ExecutionMode.Size = new System.Drawing.Size(467, 94);
            this.tableLayoutPanel_ExecutionMode.TabIndex = 5;
            // 
            // label_ExecutionMode_Error
            // 
            this.label_ExecutionMode_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_Error.AutoSize = true;
            this.label_ExecutionMode_Error.Location = new System.Drawing.Point(3, 16);
            this.label_ExecutionMode_Error.Name = "label_ExecutionMode_Error";
            this.label_ExecutionMode_Error.Size = new System.Drawing.Size(64, 15);
            this.label_ExecutionMode_Error.TabIndex = 0;
            this.label_ExecutionMode_Error.Text = "Error";
            // 
            // label_ExecutionMode_OK
            // 
            this.label_ExecutionMode_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_OK.AutoSize = true;
            this.label_ExecutionMode_OK.Location = new System.Drawing.Point(3, 63);
            this.label_ExecutionMode_OK.Name = "label_ExecutionMode_OK";
            this.label_ExecutionMode_OK.Size = new System.Drawing.Size(64, 15);
            this.label_ExecutionMode_OK.TabIndex = 1;
            this.label_ExecutionMode_OK.Text = "OK";
            // 
            // label_ExecutionMode_Error_Retry
            // 
            this.label_ExecutionMode_Error_Retry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_Error_Retry.AutoSize = true;
            this.label_ExecutionMode_Error_Retry.Location = new System.Drawing.Point(306, 16);
            this.label_ExecutionMode_Error_Retry.Name = "label_ExecutionMode_Error_Retry";
            this.label_ExecutionMode_Error_Retry.Size = new System.Drawing.Size(64, 15);
            this.label_ExecutionMode_Error_Retry.TabIndex = 2;
            this.label_ExecutionMode_Error_Retry.Text = "Retry";
            // 
            // label_ExecutionMode_OK_Retry
            // 
            this.label_ExecutionMode_OK_Retry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_OK_Retry.AutoSize = true;
            this.label_ExecutionMode_OK_Retry.Location = new System.Drawing.Point(306, 63);
            this.label_ExecutionMode_OK_Retry.Name = "label_ExecutionMode_OK_Retry";
            this.label_ExecutionMode_OK_Retry.Size = new System.Drawing.Size(64, 15);
            this.label_ExecutionMode_OK_Retry.TabIndex = 3;
            this.label_ExecutionMode_OK_Retry.Text = "Retry";
            // 
            // comboBox_ExecutionMode_Error
            // 
            this.comboBox_ExecutionMode_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_ExecutionMode_Error.FormattingEnabled = true;
            this.comboBox_ExecutionMode_Error.Location = new System.Drawing.Point(73, 12);
            this.comboBox_ExecutionMode_Error.Name = "comboBox_ExecutionMode_Error";
            this.comboBox_ExecutionMode_Error.Size = new System.Drawing.Size(227, 23);
            this.comboBox_ExecutionMode_Error.TabIndex = 4;
            this.comboBox_ExecutionMode_Error.SelectionChangeCommitted += new System.EventHandler(this.comboBox_ExecutionMode_Error_SelectionChangeCommitted);
            this.comboBox_ExecutionMode_Error.SelectedValueChanged += new System.EventHandler(this.comboBox_ExecutionMode_Error_SelectedValueChanged);
            // 
            // comboBox_ExecutionMode_OK
            // 
            this.comboBox_ExecutionMode_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_ExecutionMode_OK.FormattingEnabled = true;
            this.comboBox_ExecutionMode_OK.Location = new System.Drawing.Point(73, 59);
            this.comboBox_ExecutionMode_OK.Name = "comboBox_ExecutionMode_OK";
            this.comboBox_ExecutionMode_OK.Size = new System.Drawing.Size(227, 23);
            this.comboBox_ExecutionMode_OK.TabIndex = 5;
            this.comboBox_ExecutionMode_OK.SelectionChangeCommitted += new System.EventHandler(this.comboBox_ExecutionMode_OK_SelectionChangeCommitted);
            this.comboBox_ExecutionMode_OK.SelectedValueChanged += new System.EventHandler(this.comboBox_ExecutionMode_OK_SelectedValueChanged);
            // 
            // numericUpDown_ExectionMode_Error
            // 
            this.numericUpDown_ExectionMode_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_ExectionMode_Error.Location = new System.Drawing.Point(376, 11);
            this.numericUpDown_ExectionMode_Error.Name = "numericUpDown_ExectionMode_Error";
            this.numericUpDown_ExectionMode_Error.Size = new System.Drawing.Size(88, 25);
            this.numericUpDown_ExectionMode_Error.TabIndex = 6;
            // 
            // numericUpDown_ExectionMode_OK
            // 
            this.numericUpDown_ExectionMode_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_ExectionMode_OK.Location = new System.Drawing.Point(376, 58);
            this.numericUpDown_ExectionMode_OK.Name = "numericUpDown_ExectionMode_OK";
            this.numericUpDown_ExectionMode_OK.Size = new System.Drawing.Size(88, 25);
            this.numericUpDown_ExectionMode_OK.TabIndex = 7;
            // 
            // textBox_DisplayName
            // 
            this.textBox_DisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DisplayName.Location = new System.Drawing.Point(121, 5);
            this.textBox_DisplayName.Name = "textBox_DisplayName";
            this.textBox_DisplayName.Size = new System.Drawing.Size(467, 25);
            this.textBox_DisplayName.TabIndex = 6;
            // 
            // textBox_Author
            // 
            this.textBox_Author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Author.Location = new System.Drawing.Point(121, 41);
            this.textBox_Author.Name = "textBox_Author";
            this.textBox_Author.Size = new System.Drawing.Size(467, 25);
            this.textBox_Author.TabIndex = 7;
            // 
            // textBox_CreatedTime
            // 
            this.textBox_CreatedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CreatedTime.Location = new System.Drawing.Point(121, 77);
            this.textBox_CreatedTime.Name = "textBox_CreatedTime";
            this.textBox_CreatedTime.Size = new System.Drawing.Size(467, 25);
            this.textBox_CreatedTime.TabIndex = 8;
            // 
            // textBox_ModifiedTime
            // 
            this.textBox_ModifiedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ModifiedTime.Location = new System.Drawing.Point(121, 113);
            this.textBox_ModifiedTime.Name = "textBox_ModifiedTime";
            this.textBox_ModifiedTime.Size = new System.Drawing.Size(467, 25);
            this.textBox_ModifiedTime.TabIndex = 9;
            // 
            // SequenceInfoEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 407);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "SequenceInfoEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SequenceInfoEditor";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanel_OKCancel.ResumeLayout(false);
            this.tableLayoutPanel_Info.ResumeLayout(false);
            this.tableLayoutPanel_Info.PerformLayout();
            this.tableLayoutPanel_ExecutionMode.ResumeLayout(false);
            this.tableLayoutPanel_ExecutionMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_Error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_OK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_OKCancel;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Info;
        private System.Windows.Forms.Label label_DisplayName;
        private System.Windows.Forms.Label label_Author;
        private System.Windows.Forms.Label label_CreatedTime;
        private System.Windows.Forms.Label label_ModifiedTime;
        private System.Windows.Forms.Label label_ExecutionMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_ExecutionMode;
        private System.Windows.Forms.Label label_ExecutionMode_Error;
        private System.Windows.Forms.Label label_ExecutionMode_OK;
        private System.Windows.Forms.Label label_ExecutionMode_Error_Retry;
        private System.Windows.Forms.Label label_ExecutionMode_OK_Retry;
        private System.Windows.Forms.ComboBox comboBox_ExecutionMode_Error;
        private System.Windows.Forms.ComboBox comboBox_ExecutionMode_OK;
        private System.Windows.Forms.NumericUpDown numericUpDown_ExectionMode_Error;
        private System.Windows.Forms.NumericUpDown numericUpDown_ExectionMode_OK;
        private System.Windows.Forms.TextBox textBox_DisplayName;
        private System.Windows.Forms.TextBox textBox_Author;
        private System.Windows.Forms.TextBox textBox_CreatedTime;
        private System.Windows.Forms.TextBox textBox_ModifiedTime;
    }
}