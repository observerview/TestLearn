namespace xwx.TDP.Editor.LogViews
{
    partial class MainLogView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainLogView));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Start = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Pause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_OpenTestResult = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_ResultFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton_Sequence = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton_ShowSequenceDetailInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_AutoScrollList = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_EditSequence = new System.Windows.Forms.ToolStripButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.label_SequenceInfo = new xwx.TDP.Editor.CustomControls.CustomBoardLabel();
            this.splitter_SequenceInfo = new System.Windows.Forms.Splitter();
            this.listView_Result = new System.Windows.Forms.ListView();
            this.columnHeader_Indicator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Unit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_PF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Limit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Extra = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip_SequenceInfo = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitterH = new System.Windows.Forms.Splitter();
            this.label_Tips = new xwx.TDP.Editor.CustomControls.CustomBoardLabel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Start,
            this.toolStripButton_Pause,
            this.toolStripButton_OpenTestResult,
            this.toolStripButton_ResultFolder,
            this.toolStripSeparator1,
            this.toolStripSplitButton_Sequence,
            this.toolStripButton_ShowSequenceDetailInfo,
            this.toolStripButton_AutoScrollList,
            this.toolStripButton_EditSequence});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(930, 31);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButton_Start
            // 
            this.toolStripButton_Start.Image = global::xwx.TDP.Editor.Properties.Resources.start;
            this.toolStripButton_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Start.Name = "toolStripButton_Start";
            this.toolStripButton_Start.Size = new System.Drawing.Size(68, 24);
            this.toolStripButton_Start.Text = "Start";
            this.toolStripButton_Start.Click += new System.EventHandler(this.toolStripButton_Start_Click);
            // 
            // toolStripButton_Pause
            // 
            this.toolStripButton_Pause.Image = global::xwx.TDP.Editor.Properties.Resources.pause;
            this.toolStripButton_Pause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Pause.Name = "toolStripButton_Pause";
            this.toolStripButton_Pause.Size = new System.Drawing.Size(75, 24);
            this.toolStripButton_Pause.Text = "Pause";
            this.toolStripButton_Pause.Click += new System.EventHandler(this.toolStripButton_Pause_Click);
            // 
            // toolStripButton_OpenTestResult
            // 
            this.toolStripButton_OpenTestResult.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_OpenTestResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_OpenTestResult.Image = global::xwx.TDP.Editor.Properties.Resources.FolderOpened;
            this.toolStripButton_OpenTestResult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OpenTestResult.Name = "toolStripButton_OpenTestResult";
            this.toolStripButton_OpenTestResult.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_OpenTestResult.Text = "OpenTestResult";
            // 
            // toolStripButton_ResultFolder
            // 
            this.toolStripButton_ResultFolder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_ResultFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ResultFolder.Image = global::xwx.TDP.Editor.Properties.Resources.newFolder;
            this.toolStripButton_ResultFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ResultFolder.Name = "toolStripButton_ResultFolder";
            this.toolStripButton_ResultFolder.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_ResultFolder.Text = "ResultFolder";
            this.toolStripButton_ResultFolder.Click += new System.EventHandler(this.toolStripButton_ResultFolder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSplitButton_Sequence
            // 
            this.toolStripSplitButton_Sequence.Image = global::xwx.TDP.Editor.Properties.Resources.parameter;
            this.toolStripSplitButton_Sequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton_Sequence.Name = "toolStripSplitButton_Sequence";
            this.toolStripSplitButton_Sequence.Size = new System.Drawing.Size(115, 24);
            this.toolStripSplitButton_Sequence.Text = "Sequence";
            // 
            // toolStripButton_ShowSequenceDetailInfo
            // 
            this.toolStripButton_ShowSequenceDetailInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_ShowSequenceDetailInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ShowSequenceDetailInfo.Image = global::xwx.TDP.Editor.Properties.Resources.showbelow;
            this.toolStripButton_ShowSequenceDetailInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ShowSequenceDetailInfo.Name = "toolStripButton_ShowSequenceDetailInfo";
            this.toolStripButton_ShowSequenceDetailInfo.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_ShowSequenceDetailInfo.Text = "ShowSequenceDetailInfo";
            this.toolStripButton_ShowSequenceDetailInfo.Click += new System.EventHandler(this.toolStripButton_ShowSequenceDetailInfo_Click);
            // 
            // toolStripButton_AutoScrollList
            // 
            this.toolStripButton_AutoScrollList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_AutoScrollList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_AutoScrollList.Image")));
            this.toolStripButton_AutoScrollList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AutoScrollList.Name = "toolStripButton_AutoScrollList";
            this.toolStripButton_AutoScrollList.Size = new System.Drawing.Size(63, 28);
            this.toolStripButton_AutoScrollList.Text = "滚动";
            this.toolStripButton_AutoScrollList.ToolTipText = "Auto Scroll";
            this.toolStripButton_AutoScrollList.Click += new System.EventHandler(this.toolStripButton_AutoScrollList_Click);
            // 
            // toolStripButton_EditSequence
            // 
            this.toolStripButton_EditSequence.Image = global::xwx.TDP.Editor.Properties.Resources.edit1;
            this.toolStripButton_EditSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_EditSequence.Name = "toolStripButton_EditSequence";
            this.toolStripButton_EditSequence.Size = new System.Drawing.Size(61, 24);
            this.toolStripButton_EditSequence.Text = "Edit";
            this.toolStripButton_EditSequence.Click += new System.EventHandler(this.toolStripButton_EditSequence_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "pause.png");
            this.imageList.Images.SetKeyName(1, "start.png");
            this.imageList.Images.SetKeyName(2, "ResultItemFail.png");
            this.imageList.Images.SetKeyName(3, "ResultItemPass.png");
            this.imageList.Images.SetKeyName(4, "ResultItemError.png");
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Created);
            this.fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Deleted);
            this.fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher_Renamed);
            // 
            // label_SequenceInfo
            // 
            this.label_SequenceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_SequenceInfo.Location = new System.Drawing.Point(0, 31);
            this.label_SequenceInfo.Name = "label_SequenceInfo";
            this.label_SequenceInfo.Size = new System.Drawing.Size(930, 41);
            this.label_SequenceInfo.TabIndex = 1;
            // 
            // splitter_SequenceInfo
            // 
            this.splitter_SequenceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter_SequenceInfo.Location = new System.Drawing.Point(0, 72);
            this.splitter_SequenceInfo.Name = "splitter_SequenceInfo";
            this.splitter_SequenceInfo.Size = new System.Drawing.Size(930, 3);
            this.splitter_SequenceInfo.TabIndex = 2;
            this.splitter_SequenceInfo.TabStop = false;
            this.splitter_SequenceInfo.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SequenceInfo_SplitterMoved);
            // 
            // listView_Result
            // 
            this.listView_Result.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Indicator,
            this.columnHeader_Time,
            this.columnHeader_Title,
            this.columnHeader_Result,
            this.columnHeader_Unit,
            this.columnHeader_PF,
            this.columnHeader_Limit,
            this.columnHeader_Extra});
            this.listView_Result.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView_Result.FullRowSelect = true;
            this.listView_Result.HideSelection = false;
            this.listView_Result.LargeImageList = this.imageList;
            this.listView_Result.Location = new System.Drawing.Point(0, 75);
            this.listView_Result.Name = "listView_Result";
            this.listView_Result.Size = new System.Drawing.Size(930, 486);
            this.listView_Result.SmallImageList = this.imageList;
            this.listView_Result.TabIndex = 3;
            this.listView_Result.UseCompatibleStateImageBehavior = false;
            this.listView_Result.View = System.Windows.Forms.View.Details;
            this.listView_Result.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listView_Result_ColumnWidthChanged);
            this.listView_Result.ItemActivate += new System.EventHandler(this.listView_Result_ItemActivate);
            // 
            // columnHeader_Indicator
            // 
            this.columnHeader_Indicator.Tag = "*";
            this.columnHeader_Indicator.Text = "*";
            this.columnHeader_Indicator.Width = 135;
            // 
            // columnHeader_Time
            // 
            this.columnHeader_Time.Tag = "Time";
            this.columnHeader_Time.Text = "Time";
            this.columnHeader_Time.Width = 143;
            // 
            // columnHeader_Title
            // 
            this.columnHeader_Title.Tag = "Title";
            this.columnHeader_Title.Text = "Title";
            this.columnHeader_Title.Width = 250;
            // 
            // columnHeader_Result
            // 
            this.columnHeader_Result.Tag = "Result";
            this.columnHeader_Result.Text = "Result";
            this.columnHeader_Result.Width = 84;
            // 
            // columnHeader_Unit
            // 
            this.columnHeader_Unit.Tag = "Unit";
            this.columnHeader_Unit.Text = "Unit";
            this.columnHeader_Unit.Width = 112;
            // 
            // columnHeader_PF
            // 
            this.columnHeader_PF.Tag = "Pass/Fail";
            this.columnHeader_PF.Text = "Pass/Fail";
            this.columnHeader_PF.Width = 107;
            // 
            // columnHeader_Limit
            // 
            this.columnHeader_Limit.Tag = "Limit";
            this.columnHeader_Limit.Text = "Limit";
            this.columnHeader_Limit.Width = 90;
            // 
            // columnHeader_Extra
            // 
            this.columnHeader_Extra.Tag = "Extra";
            this.columnHeader_Extra.Text = "Extra";
            // 
            // splitterH
            // 
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterH.Location = new System.Drawing.Point(0, 561);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(930, 3);
            this.splitterH.TabIndex = 4;
            this.splitterH.TabStop = false;
            this.splitterH.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spliterH_SplitterMoved);
            // 
            // label_Tips
            // 
            this.label_Tips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Tips.Location = new System.Drawing.Point(0, 564);
            this.label_Tips.Name = "label_Tips";
            this.label_Tips.Size = new System.Drawing.Size(930, 79);
            this.label_Tips.TabIndex = 5;
            // 
            // MainLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 643);
            this.Controls.Add(this.label_Tips);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.listView_Result);
            this.Controls.Add(this.splitter_SequenceInfo);
            this.Controls.Add(this.label_SequenceInfo);
            this.Controls.Add(this.toolStrip);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainLogView";
            this.Text = "序列执行器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainLogView_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainLogView_KeyDown);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton_Start;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton toolStripButton_Pause;
        private System.Windows.Forms.ToolStripButton toolStripButton_OpenTestResult;
        private System.Windows.Forms.ToolStripButton toolStripButton_ResultFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton_Sequence;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ToolStripButton toolStripButton_ShowSequenceDetailInfo;
        private System.Windows.Forms.ToolStripButton toolStripButton_AutoScrollList;
        private System.Windows.Forms.ToolStripButton toolStripButton_EditSequence;
        private CustomControls.CustomBoardLabel label_SequenceInfo;
        private System.Windows.Forms.Splitter splitter_SequenceInfo;
        private System.Windows.Forms.ListView listView_Result;
        private System.Windows.Forms.ColumnHeader columnHeader_Indicator;
        private System.Windows.Forms.ColumnHeader columnHeader_Time;
        private System.Windows.Forms.ColumnHeader columnHeader_Title;
        private System.Windows.Forms.ColumnHeader columnHeader_Result;
        private System.Windows.Forms.ColumnHeader columnHeader_Unit;
        private System.Windows.Forms.ColumnHeader columnHeader_PF;
        private System.Windows.Forms.ColumnHeader columnHeader_Limit;
        private System.Windows.Forms.ColumnHeader columnHeader_Extra;
        private System.Windows.Forms.ToolTip toolTip_SequenceInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private CustomControls.CustomBoardLabel label_Tips;
        private System.Windows.Forms.Splitter splitterH;
    }
}