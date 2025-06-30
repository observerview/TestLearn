namespace xwx.TDP.Editor.CustomControls
{
    partial class CustomBoardLabel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter4.Location = new System.Drawing.Point(0, 97);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(510, 3);
            this.splitter4.TabIndex = 3;
            this.splitter4.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(0, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(510, 3);
            this.splitter3.TabIndex = 2;
            this.splitter3.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(507, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 94);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 94);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point(3, 3);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(504, 94);
            this.label.TabIndex = 4;
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomBoardLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.splitter4);
            this.Name = "CustomBoardLabel";
            this.Size = new System.Drawing.Size(510, 100);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label;
    }
}
