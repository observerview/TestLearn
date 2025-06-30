using System.Windows.Forms;

namespace xwx.TDP.Editor.Misc
{
    partial class SplashWindow : Form
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
            this.label_Info = new System.Windows.Forms.Label();
            this.label_Version = new System.Windows.Forms.Label();
            this.label_Status = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Info.Location = new System.Drawing.Point(3, 253);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(622, 69);
            this.label_Info.TabIndex = 0;
            this.label_Info.Text = "label_Info";
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Version.Location = new System.Drawing.Point(3, 221);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(622, 32);
            this.label_Version.TabIndex = 1;
            this.label_Version.Text = "label_Version";
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Status.Location = new System.Drawing.Point(3, 322);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(622, 21);
            this.label_Status.TabIndex = 2;
            this.label_Status.Text = "label_Status";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label_Status, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_Info, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_Version, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.35178F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.64822F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(628, 343);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // SplashWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 343);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashWindow";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Label label_Status;
        private TableLayoutPanel tableLayoutPanel1;
    }
}