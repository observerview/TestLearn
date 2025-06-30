namespace xwx.TDP.Editor
{
    partial class SequenceSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceSelector));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1_NewFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1_NewSequence = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1_Modify = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1_RefreshFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1_Browser = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView = new TestManager.Utility.ExtendedControl.TreeViewEx();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTestSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1_NewFolder,
            this.toolStripButton1_NewSequence,
            this.toolStripButton1_Delete,
            this.toolStripSeparator1,
            this.toolStripButton1_Modify,
            this.toolStripButton1_Edit,
            this.toolStripSeparator2,
            this.toolStripButton1_RefreshFolder,
            this.toolStripButton1_Browser});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(478, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1_NewFolder
            // 
            this.toolStripButton1_NewFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1_NewFolder.Image = global::xwx.TDP.Editor.Properties.Resources.newFolder;
            this.toolStripButton1_NewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_NewFolder.Name = "toolStripButton1_NewFolder";
            this.toolStripButton1_NewFolder.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1_NewFolder.Text = "NewFolder";
            this.toolStripButton1_NewFolder.Click += new System.EventHandler(this.toolStripButton1_NewFolder_Click);
            // 
            // toolStripButton1_NewSequence
            // 
            this.toolStripButton1_NewSequence.Image = global::xwx.TDP.Editor.Properties.Resources.newfile;
            this.toolStripButton1_NewSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_NewSequence.Name = "toolStripButton1_NewSequence";
            this.toolStripButton1_NewSequence.Size = new System.Drawing.Size(63, 28);
            this.toolStripButton1_NewSequence.Text = "新建";
            this.toolStripButton1_NewSequence.Click += new System.EventHandler(this.toolStripButton1_NewSequence_Click);
            // 
            // toolStripButton1_Delete
            // 
            this.toolStripButton1_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1_Delete.Image = global::xwx.TDP.Editor.Properties.Resources.delete;
            this.toolStripButton1_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_Delete.Name = "toolStripButton1_Delete";
            this.toolStripButton1_Delete.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1_Delete.Text = "Delete";
            this.toolStripButton1_Delete.Click += new System.EventHandler(this.toolStripButton1_Delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButton1_Modify
            // 
            this.toolStripButton1_Modify.Image = global::xwx.TDP.Editor.Properties.Resources.modify;
            this.toolStripButton1_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_Modify.Name = "toolStripButton1_Modify";
            this.toolStripButton1_Modify.Size = new System.Drawing.Size(63, 28);
            this.toolStripButton1_Modify.Text = "修改";
            this.toolStripButton1_Modify.Click += new System.EventHandler(this.toolStripButton1_Modify_Click);
            // 
            // toolStripButton1_Edit
            // 
            this.toolStripButton1_Edit.Image = global::xwx.TDP.Editor.Properties.Resources.edit;
            this.toolStripButton1_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_Edit.Name = "toolStripButton1_Edit";
            this.toolStripButton1_Edit.Size = new System.Drawing.Size(63, 28);
            this.toolStripButton1_Edit.Text = "编辑";
            this.toolStripButton1_Edit.Click += new System.EventHandler(this.toolStripButton1_Edit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButton1_RefreshFolder
            // 
            this.toolStripButton1_RefreshFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1_RefreshFolder.Image = global::xwx.TDP.Editor.Properties.Resources.refresh1;
            this.toolStripButton1_RefreshFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_RefreshFolder.Name = "toolStripButton1_RefreshFolder";
            this.toolStripButton1_RefreshFolder.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1_RefreshFolder.Text = "RefreshFolder";
            this.toolStripButton1_RefreshFolder.Click += new System.EventHandler(this.toolStripButton1_RefreshFolder_Click);
            // 
            // toolStripButton1_Browser
            // 
            this.toolStripButton1_Browser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1_Browser.Image = global::xwx.TDP.Editor.Properties.Resources.FolderOpened;
            this.toolStripButton1_Browser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1_Browser.Name = "toolStripButton1_Browser";
            this.toolStripButton1_Browser.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1_Browser.Text = "Browser";
            this.toolStripButton1_Browser.Click += new System.EventHandler(this.toolStripButton1_Browser_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "delete.png");
            this.imageList1.Images.SetKeyName(1, "edit.png");
            this.imageList1.Images.SetKeyName(2, "FolderClosed.png");
            this.imageList1.Images.SetKeyName(3, "FolderOpened.png");
            this.imageList1.Images.SetKeyName(4, "modify.png");
            this.imageList1.Images.SetKeyName(5, "new.png");
            this.imageList1.Images.SetKeyName(6, "newfile.png");
            this.imageList1.Images.SetKeyName(7, "newFolder.png");
            this.imageList1.Images.SetKeyName(8, "refresh.png");
            this.imageList1.Images.SetKeyName(9, "Sequence.png");
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList1;
            this.treeView.Location = new System.Drawing.Point(0, 31);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(478, 407);
            this.treeView.TabIndex = 0;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            this.treeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 438);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(478, 167);
            this.textBox1.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 438);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(478, 3);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Created);
            this.fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Deleted);
            this.fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher_Renamed);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.newTestSequenceToolStripMenuItem,
            this.deleteToolStripMenuItemToolStripMenuItem,
            this.toolStripSeparator3,
            this.modifyToolStripMenuItem,
            this.editToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(223, 130);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // newTestSequenceToolStripMenuItem
            // 
            this.newTestSequenceToolStripMenuItem.Name = "newTestSequenceToolStripMenuItem";
            this.newTestSequenceToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.newTestSequenceToolStripMenuItem.Text = "New Test Sequence";
            this.newTestSequenceToolStripMenuItem.Click += new System.EventHandler(this.newTestSequenceToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItemToolStripMenuItem
            // 
            this.deleteToolStripMenuItemToolStripMenuItem.Name = "deleteToolStripMenuItemToolStripMenuItem";
            this.deleteToolStripMenuItemToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.deleteToolStripMenuItemToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItemToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.modifyToolStripMenuItem.Text = "Modify";
            this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // SequenceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 605);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolStrip1);
            this.HideOnClose = true;
            this.Name = "SequenceSelector";
            this.Text = "测试序列库";
            this.Load += new System.EventHandler(this.SequenceSelector_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SequenceSelector_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1_Delete;
        private System.Windows.Forms.ToolStripButton toolStripButton1_NewSequence;
        private System.Windows.Forms.ToolStripButton toolStripButton1_NewFolder;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1_Modify;
        private System.Windows.Forms.ToolStripButton toolStripButton1_Edit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1_RefreshFolder;
        private System.Windows.Forms.ToolStripButton toolStripButton1_Browser;
        private TestManager.Utility.ExtendedControl.TreeViewEx treeView;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Splitter splitter1;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTestSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}