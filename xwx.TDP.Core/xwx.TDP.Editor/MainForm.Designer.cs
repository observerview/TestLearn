namespace xwx.TDP.Editor
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceExecutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.序列执行器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试CaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.ToolStripMenuItem_View,
            this.ToolStripMenuItem_Log,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            resources.ApplyResources(this.settingToolStripMenuItem, "settingToolStripMenuItem");
            // 
            // ToolStripMenuItem_View
            // 
            this.ToolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceExecutorToolStripMenuItem,
            this.序列执行器ToolStripMenuItem,
            this.测试CaseToolStripMenuItem,
            this.测试ToolStripMenuItem});
            this.ToolStripMenuItem_View.Name = "ToolStripMenuItem_View";
            resources.ApplyResources(this.ToolStripMenuItem_View, "ToolStripMenuItem_View");
            // 
            // sequenceExecutorToolStripMenuItem
            // 
            this.sequenceExecutorToolStripMenuItem.Name = "sequenceExecutorToolStripMenuItem";
            resources.ApplyResources(this.sequenceExecutorToolStripMenuItem, "sequenceExecutorToolStripMenuItem");
            this.sequenceExecutorToolStripMenuItem.Click += new System.EventHandler(this.sequenceExecutorToolStripMenuItem_Click);
            // 
            // 序列执行器ToolStripMenuItem
            // 
            this.序列执行器ToolStripMenuItem.Name = "序列执行器ToolStripMenuItem";
            resources.ApplyResources(this.序列执行器ToolStripMenuItem, "序列执行器ToolStripMenuItem");
            this.序列执行器ToolStripMenuItem.Click += new System.EventHandler(this.序列执行器ToolStripMenuItem_Click);
            // 
            // 测试CaseToolStripMenuItem
            // 
            this.测试CaseToolStripMenuItem.Name = "测试CaseToolStripMenuItem";
            resources.ApplyResources(this.测试CaseToolStripMenuItem, "测试CaseToolStripMenuItem");
            this.测试CaseToolStripMenuItem.Click += new System.EventHandler(this.测试CaseToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            resources.ApplyResources(this.测试ToolStripMenuItem, "测试ToolStripMenuItem");
            this.测试ToolStripMenuItem.Click += new System.EventHandler(this.测试ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_Log
            // 
            this.ToolStripMenuItem_Log.Name = "ToolStripMenuItem_Log";
            resources.ApplyResources(this.ToolStripMenuItem_Log, "ToolStripMenuItem_Log");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // dockPanel1
            // 
            resources.ApplyResources(this.dockPanel1, "dockPanel1");
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingMdi;
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.ShowDocumentIcon = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Log;
        private System.Windows.Forms.ToolStripMenuItem sequenceExecutorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 序列执行器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试CaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
    }
}

