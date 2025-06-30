using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TestManager.Utility.Misc;

namespace TestManager.Utility.GenericForm
{
	internal class MessageBoxExCore : Form
	{
		private class ButtonLabel
		{
			public const string OK = "&OK";

			public const string Cancel = "&Cancel";

			public const string Yes = "&Yes";

			public const string No = "&No";

			public const string Retry = "&Retry";

			public const string Abort = "&Abort";

			public const string Ignore = "&Ignore";
		}

		private DialogResult _formClosedDialogResult;

		private MessageBoxButtons _buttons;

		private IContainer components;

		private Button button1;

		private TableLayoutPanel tableLayoutPanel_Button;

		private Button button2;

		private Button button3;

		private PictureBox pictureBox_Icon;

		private Label label_Message;

		private TextBox textBox;

		private Panel panel_Message;

		private Panel panel_Message_Left;

		private Panel panel_Message_Right;

		public MessageBoxIcon MessageIcon
		{
			set
			{
				Icon icon = null;
				switch (value)
				{
				case MessageBoxIcon.Hand:
					icon = SystemIcons.Error;
					break;
				case MessageBoxIcon.Question:
					icon = SystemIcons.Question;
					break;
				case MessageBoxIcon.Exclamation:
					icon = SystemIcons.Warning;
					break;
				case MessageBoxIcon.Asterisk:
					icon = SystemIcons.Information;
					break;
				}
				if (icon != null)
				{
					pictureBox_Icon.Image = icon.ToBitmap();
				}
			}
		}

		public string MessageTitle
		{
			set
			{
				Text = value;
			}
		}

		public string PromptText
		{
			get
			{
				return textBox.Text;
			}
			set
			{
				textBox.Visible = true;
				textBox.Text = value;
				textBox.Focus();
				textBox.SelectAll();
			}
		}

		public string MessageText
		{
			set
			{
				label_Message.Text = value;
			}
		}

		public MessageBoxButtons Buttons
		{
			set
			{
				_buttons = value;
				switch (value)
				{
				case MessageBoxButtons.AbortRetryIgnore:
					button1.Text = "&Ignore";
					button1.Tag = DialogResult.Ignore;
					button1.Visible = true;
					button2.Text = "&Abort";
					button2.Tag = DialogResult.Abort;
					button2.Visible = true;
					button3.Text = "&Retry";
					button3.Tag = DialogResult.Retry;
					button3.Visible = true;
					_formClosedDialogResult = DialogResult.Ignore;
					base.AcceptButton = button3;
					break;
				case MessageBoxButtons.OK:
					button1.Text = "&OK";
					button1.Tag = DialogResult.OK;
					button1.Visible = true;
					_formClosedDialogResult = DialogResult.OK;
					base.AcceptButton = button1;
					base.CancelButton = button1;
					break;
				case MessageBoxButtons.OKCancel:
					button2.Text = "&Cancel";
					button2.Tag = DialogResult.Cancel;
					button2.Visible = true;
					button3.Text = "&OK";
					button3.Tag = DialogResult.OK;
					button3.Visible = true;
					_formClosedDialogResult = DialogResult.Cancel;
					base.AcceptButton = button3;
					base.CancelButton = button2;
					break;
				case MessageBoxButtons.RetryCancel:
					button2.Text = "&Cancel";
					button2.Tag = DialogResult.Cancel;
					button2.Visible = true;
					button3.Text = "&Retry";
					button3.Tag = DialogResult.Retry;
					button3.Visible = true;
					_formClosedDialogResult = DialogResult.Cancel;
					base.AcceptButton = button3;
					base.CancelButton = button2;
					break;
				case MessageBoxButtons.YesNo:
					button2.Text = "&No";
					button2.Tag = DialogResult.No;
					button2.Visible = true;
					button3.Text = "&Yes";
					button3.Tag = DialogResult.Yes;
					button3.Visible = true;
					_formClosedDialogResult = DialogResult.No;
					base.AcceptButton = button3;
					base.CancelButton = button2;
					break;
				case MessageBoxButtons.YesNoCancel:
					button1.Text = "&No";
					button1.Tag = DialogResult.No;
					button1.Visible = true;
					button2.Text = "&Cancel";
					button2.Tag = DialogResult.Cancel;
					button2.Visible = true;
					button3.Text = "&Yes";
					button3.Tag = DialogResult.Yes;
					button3.Visible = true;
					_formClosedDialogResult = DialogResult.Cancel;
					base.AcceptButton = button3;
					break;
				}
			}
		}

		public MessageBoxExCore()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			button1.Click += button_Click;
			button2.Click += button_Click;
			button3.Click += button_Click;
			Utilities.SetControlFont(this, SystemFonts.MessageBoxFont);
		}

		private void button_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			_formClosedDialogResult = (DialogResult)button.Tag;
			Close();
		}

		private void MessageBoxExCore_SizeChanged(object sender, EventArgs e)
		{
			button1.Width = (int)(0.3 * (double)tableLayoutPanel_Button.Width * 0.8);
			button2.Width = button1.Width;
			button3.Width = button1.Width;
		}

		private void MessageBoxExCore_FormClosed(object sender, FormClosedEventArgs e)
		{
			CloseReason closeReason = e.CloseReason;
			if (closeReason == CloseReason.UserClosing)
			{
				base.DialogResult = _formClosedDialogResult;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel_Button = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.label_Message = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel_Message = new System.Windows.Forms.Panel();
            this.panel_Message_Right = new System.Windows.Forms.Panel();
            this.panel_Message_Left = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_Button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.panel_Message.SuspendLayout();
            this.panel_Message_Right.SuspendLayout();
            this.panel_Message_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(170, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // tableLayoutPanel_Button
            // 
            this.tableLayoutPanel_Button.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel_Button.ColumnCount = 3;
            this.tableLayoutPanel_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel_Button.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel_Button.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel_Button.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel_Button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel_Button.Location = new System.Drawing.Point(0, 57);
            this.tableLayoutPanel_Button.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Button.Name = "tableLayoutPanel_Button";
            this.tableLayoutPanel_Button.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tableLayoutPanel_Button.RowCount = 1;
            this.tableLayoutPanel_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Button.Size = new System.Drawing.Size(452, 35);
            this.tableLayoutPanel_Button.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(45, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.AutoSize = true;
            this.button3.Location = new System.Drawing.Point(296, 4);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 25);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Location = new System.Drawing.Point(12, 8);
            this.pictureBox_Icon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(32, 30);
            this.pictureBox_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Icon.TabIndex = 0;
            this.pictureBox_Icon.TabStop = false;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Message.Location = new System.Drawing.Point(4, 8);
            this.label_Message.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(67, 17);
            this.label_Message.TabIndex = 1;
            this.label_Message.Text = "Message";
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox.Location = new System.Drawing.Point(4, 28);
            this.textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(388, 25);
            this.textBox.TabIndex = 0;
            this.textBox.Visible = false;
            // 
            // panel_Message
            // 
            this.panel_Message.AutoSize = true;
            this.panel_Message.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_Message.Controls.Add(this.panel_Message_Right);
            this.panel_Message.Controls.Add(this.panel_Message_Left);
            this.panel_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Message.Location = new System.Drawing.Point(0, 0);
            this.panel_Message.Name = "panel_Message";
            this.panel_Message.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.panel_Message.Size = new System.Drawing.Size(452, 57);
            this.panel_Message.TabIndex = 2;
            // 
            // panel_Message_Right
            // 
            this.panel_Message_Right.AutoSize = true;
            this.panel_Message_Right.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_Message_Right.Controls.Add(this.textBox);
            this.panel_Message_Right.Controls.Add(this.label_Message);
            this.panel_Message_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Message_Right.Location = new System.Drawing.Point(52, 0);
            this.panel_Message_Right.Name = "panel_Message_Right";
            this.panel_Message_Right.Padding = new System.Windows.Forms.Padding(4, 8, 8, 0);
            this.panel_Message_Right.Size = new System.Drawing.Size(400, 53);
            this.panel_Message_Right.TabIndex = 1;
            // 
            // panel_Message_Left
            // 
            this.panel_Message_Left.AutoSize = true;
            this.panel_Message_Left.Controls.Add(this.pictureBox_Icon);
            this.panel_Message_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Message_Left.Location = new System.Drawing.Point(0, 0);
            this.panel_Message_Left.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Message_Left.Name = "panel_Message_Left";
            this.panel_Message_Left.Padding = new System.Windows.Forms.Padding(12, 8, 8, 3);
            this.panel_Message_Left.Size = new System.Drawing.Size(52, 53);
            this.panel_Message_Left.TabIndex = 0;
            // 
            // MessageBoxExCore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(452, 92);
            this.Controls.Add(this.panel_Message);
            this.Controls.Add(this.tableLayoutPanel_Button);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 80);
            this.Name = "MessageBoxExCore";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MessageBoxExCore_FormClosed);
            this.tableLayoutPanel_Button.ResumeLayout(false);
            this.tableLayoutPanel_Button.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.panel_Message.ResumeLayout(false);
            this.panel_Message.PerformLayout();
            this.panel_Message_Right.ResumeLayout(false);
            this.panel_Message_Right.PerformLayout();
            this.panel_Message_Left.ResumeLayout(false);
            this.panel_Message_Left.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
