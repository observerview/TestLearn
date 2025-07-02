namespace xwx.TDP.Editor
{
    partial class SequenceEditor
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new TestManager.Utility.ExtendedControl.TreeViewEx();
            this.panel_EngineMode = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_EngineMode_Type = new System.Windows.Forms.TableLayoutPanel();
            this.radioButton_EngineMode_Globe = new System.Windows.Forms.RadioButton();
            this.radioButton_EngineMode_Skip = new System.Windows.Forms.RadioButton();
            this.radioButton_EngineMode_Case = new System.Windows.Forms.RadioButton();
            this.groupBox_EngineMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_ExecutionMode = new System.Windows.Forms.TableLayoutPanel();
            this.label_ExecutionMode_Error = new System.Windows.Forms.Label();
            this.label_ExecutionMode_OK = new System.Windows.Forms.Label();
            this.label_ExecutionMode_Error_Retry = new System.Windows.Forms.Label();
            this.label_ExecutionMode_OK_Retry = new System.Windows.Forms.Label();
            this.comboBox_ExecutionMode_Error = new System.Windows.Forms.ComboBox();
            this.comboBox_ExecutionMode_OK = new System.Windows.Forms.ComboBox();
            this.numericUpDown_ExectionMode_Error = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ExectionMode_OK = new System.Windows.Forms.NumericUpDown();
            this.textBox_Tips = new System.Windows.Forms.TextBox();
            this.splitter = new System.Windows.Forms.Splitter();
            this.propertyGrid = new TestManager.Utility.PropertyGridEx.PropertyGridEx();
            this.toolStrip_PropertyGrid = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Parameter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Limit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_RestoreDefaultValue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_CaseName = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Discard = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_SequenceName = new System.Windows.Forms.ToolStripLabel();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel_EngineMode.SuspendLayout();
            this.tableLayoutPanel_EngineMode_Type.SuspendLayout();
            this.groupBox_EngineMode.SuspendLayout();
            this.tableLayoutPanel_ExecutionMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_Error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_OK)).BeginInit();
            this.toolStrip_PropertyGrid.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel1.Controls.Add(this.panel_EngineMode);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.textBox_Tips);
            this.splitContainer.Panel2.Controls.Add(this.splitter);
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer.Panel2.Controls.Add(this.toolStrip_PropertyGrid);
            this.splitContainer.Size = new System.Drawing.Size(646, 522);
            this.splitContainer.SplitterDistance = 287;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(2, 2);
            this.treeView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(283, 398);
            this.treeView.TabIndex = 2;
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView.DragLeave += new System.EventHandler(this.treeView_DragLeave);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // panel_EngineMode
            // 
            this.panel_EngineMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_EngineMode.Controls.Add(this.tableLayoutPanel_EngineMode_Type);
            this.panel_EngineMode.Controls.Add(this.groupBox_EngineMode);
            this.panel_EngineMode.Location = new System.Drawing.Point(2, 405);
            this.panel_EngineMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_EngineMode.Name = "panel_EngineMode";
            this.panel_EngineMode.Size = new System.Drawing.Size(283, 114);
            this.panel_EngineMode.TabIndex = 1;
            // 
            // tableLayoutPanel_EngineMode_Type
            // 
            this.tableLayoutPanel_EngineMode_Type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_EngineMode_Type.ColumnCount = 3;
            this.tableLayoutPanel_EngineMode_Type.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_EngineMode_Type.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_EngineMode_Type.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_EngineMode_Type.Controls.Add(this.radioButton_EngineMode_Globe, 0, 0);
            this.tableLayoutPanel_EngineMode_Type.Controls.Add(this.radioButton_EngineMode_Skip, 2, 0);
            this.tableLayoutPanel_EngineMode_Type.Controls.Add(this.radioButton_EngineMode_Case, 1, 0);
            this.tableLayoutPanel_EngineMode_Type.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel_EngineMode_Type.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel_EngineMode_Type.Name = "tableLayoutPanel_EngineMode_Type";
            this.tableLayoutPanel_EngineMode_Type.RowCount = 1;
            this.tableLayoutPanel_EngineMode_Type.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_EngineMode_Type.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel_EngineMode_Type.Size = new System.Drawing.Size(277, 22);
            this.tableLayoutPanel_EngineMode_Type.TabIndex = 1;
            // 
            // radioButton_EngineMode_Globe
            // 
            this.radioButton_EngineMode_Globe.AutoSize = true;
            this.radioButton_EngineMode_Globe.Location = new System.Drawing.Point(2, 2);
            this.radioButton_EngineMode_Globe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_EngineMode_Globe.Name = "radioButton_EngineMode_Globe";
            this.radioButton_EngineMode_Globe.Size = new System.Drawing.Size(53, 16);
            this.radioButton_EngineMode_Globe.TabIndex = 0;
            this.radioButton_EngineMode_Globe.TabStop = true;
            this.radioButton_EngineMode_Globe.Text = "Globe";
            this.radioButton_EngineMode_Globe.UseVisualStyleBackColor = true;
            this.radioButton_EngineMode_Globe.CheckedChanged += new System.EventHandler(this.radioButton_EngineMode_Globe_CheckedChanged);
            // 
            // radioButton_EngineMode_Skip
            // 
            this.radioButton_EngineMode_Skip.AutoSize = true;
            this.radioButton_EngineMode_Skip.Location = new System.Drawing.Point(186, 2);
            this.radioButton_EngineMode_Skip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_EngineMode_Skip.Name = "radioButton_EngineMode_Skip";
            this.radioButton_EngineMode_Skip.Size = new System.Drawing.Size(47, 16);
            this.radioButton_EngineMode_Skip.TabIndex = 2;
            this.radioButton_EngineMode_Skip.TabStop = true;
            this.radioButton_EngineMode_Skip.Text = "Skip";
            this.radioButton_EngineMode_Skip.UseVisualStyleBackColor = true;
            this.radioButton_EngineMode_Skip.CheckedChanged += new System.EventHandler(this.radioButton_EngineMode_Skip_CheckedChanged);
            // 
            // radioButton_EngineMode_Case
            // 
            this.radioButton_EngineMode_Case.AutoSize = true;
            this.radioButton_EngineMode_Case.Location = new System.Drawing.Point(94, 2);
            this.radioButton_EngineMode_Case.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_EngineMode_Case.Name = "radioButton_EngineMode_Case";
            this.radioButton_EngineMode_Case.Size = new System.Drawing.Size(47, 16);
            this.radioButton_EngineMode_Case.TabIndex = 1;
            this.radioButton_EngineMode_Case.TabStop = true;
            this.radioButton_EngineMode_Case.Text = "Case";
            this.radioButton_EngineMode_Case.UseVisualStyleBackColor = true;
            this.radioButton_EngineMode_Case.CheckedChanged += new System.EventHandler(this.radioButton_EngineMode_Case_CheckedChanged);
            // 
            // groupBox_EngineMode
            // 
            this.groupBox_EngineMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_EngineMode.Controls.Add(this.tableLayoutPanel_ExecutionMode);
            this.groupBox_EngineMode.Location = new System.Drawing.Point(2, 14);
            this.groupBox_EngineMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_EngineMode.Name = "groupBox_EngineMode";
            this.groupBox_EngineMode.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_EngineMode.Size = new System.Drawing.Size(275, 99);
            this.groupBox_EngineMode.TabIndex = 0;
            this.groupBox_EngineMode.TabStop = false;
            this.groupBox_EngineMode.Text = "Execution Mode";
            // 
            // tableLayoutPanel_ExecutionMode
            // 
            this.tableLayoutPanel_ExecutionMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_ExecutionMode.ColumnCount = 4;
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_ExecutionMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_Error, 0, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_OK, 0, 1);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_Error_Retry, 2, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.label_ExecutionMode_OK_Retry, 2, 1);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.comboBox_ExecutionMode_Error, 1, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.comboBox_ExecutionMode_OK, 1, 1);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.numericUpDown_ExectionMode_Error, 3, 0);
            this.tableLayoutPanel_ExecutionMode.Controls.Add(this.numericUpDown_ExectionMode_OK, 3, 1);
            this.tableLayoutPanel_ExecutionMode.Location = new System.Drawing.Point(-2, 43);
            this.tableLayoutPanel_ExecutionMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel_ExecutionMode.Name = "tableLayoutPanel_ExecutionMode";
            this.tableLayoutPanel_ExecutionMode.RowCount = 2;
            this.tableLayoutPanel_ExecutionMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ExecutionMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ExecutionMode.Size = new System.Drawing.Size(277, 50);
            this.tableLayoutPanel_ExecutionMode.TabIndex = 0;
            // 
            // label_ExecutionMode_Error
            // 
            this.label_ExecutionMode_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_Error.AutoSize = true;
            this.label_ExecutionMode_Error.Location = new System.Drawing.Point(2, 6);
            this.label_ExecutionMode_Error.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ExecutionMode_Error.Name = "label_ExecutionMode_Error";
            this.label_ExecutionMode_Error.Size = new System.Drawing.Size(37, 12);
            this.label_ExecutionMode_Error.TabIndex = 0;
            this.label_ExecutionMode_Error.Text = "Error";
            // 
            // label_ExecutionMode_OK
            // 
            this.label_ExecutionMode_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_OK.AutoSize = true;
            this.label_ExecutionMode_OK.Location = new System.Drawing.Point(2, 31);
            this.label_ExecutionMode_OK.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ExecutionMode_OK.Name = "label_ExecutionMode_OK";
            this.label_ExecutionMode_OK.Size = new System.Drawing.Size(37, 12);
            this.label_ExecutionMode_OK.TabIndex = 1;
            this.label_ExecutionMode_OK.Text = "OK";
            // 
            // label_ExecutionMode_Error_Retry
            // 
            this.label_ExecutionMode_Error_Retry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_Error_Retry.AutoSize = true;
            this.label_ExecutionMode_Error_Retry.Location = new System.Drawing.Point(167, 6);
            this.label_ExecutionMode_Error_Retry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ExecutionMode_Error_Retry.Name = "label_ExecutionMode_Error_Retry";
            this.label_ExecutionMode_Error_Retry.Size = new System.Drawing.Size(37, 12);
            this.label_ExecutionMode_Error_Retry.TabIndex = 2;
            this.label_ExecutionMode_Error_Retry.Text = "Retry";
            // 
            // label_ExecutionMode_OK_Retry
            // 
            this.label_ExecutionMode_OK_Retry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ExecutionMode_OK_Retry.AutoSize = true;
            this.label_ExecutionMode_OK_Retry.Location = new System.Drawing.Point(167, 31);
            this.label_ExecutionMode_OK_Retry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ExecutionMode_OK_Retry.Name = "label_ExecutionMode_OK_Retry";
            this.label_ExecutionMode_OK_Retry.Size = new System.Drawing.Size(37, 12);
            this.label_ExecutionMode_OK_Retry.TabIndex = 3;
            this.label_ExecutionMode_OK_Retry.Text = "Retry";
            // 
            // comboBox_ExecutionMode_Error
            // 
            this.comboBox_ExecutionMode_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_ExecutionMode_Error.FormattingEnabled = true;
            this.comboBox_ExecutionMode_Error.Location = new System.Drawing.Point(43, 2);
            this.comboBox_ExecutionMode_Error.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_ExecutionMode_Error.Name = "comboBox_ExecutionMode_Error";
            this.comboBox_ExecutionMode_Error.Size = new System.Drawing.Size(120, 20);
            this.comboBox_ExecutionMode_Error.TabIndex = 4;
            this.comboBox_ExecutionMode_Error.SelectionChangeCommitted += new System.EventHandler(this.comboBox_ExecutionMode_Error_SelectionChangeCommitted);
            // 
            // comboBox_ExecutionMode_OK
            // 
            this.comboBox_ExecutionMode_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_ExecutionMode_OK.FormattingEnabled = true;
            this.comboBox_ExecutionMode_OK.Location = new System.Drawing.Point(43, 27);
            this.comboBox_ExecutionMode_OK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_ExecutionMode_OK.Name = "comboBox_ExecutionMode_OK";
            this.comboBox_ExecutionMode_OK.Size = new System.Drawing.Size(120, 20);
            this.comboBox_ExecutionMode_OK.TabIndex = 5;
            this.comboBox_ExecutionMode_OK.SelectionChangeCommitted += new System.EventHandler(this.comboBox_ExecutionMode_OK_SelectionChangeCommitted);
            // 
            // numericUpDown_ExectionMode_Error
            // 
            this.numericUpDown_ExectionMode_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_ExectionMode_Error.Location = new System.Drawing.Point(208, 2);
            this.numericUpDown_ExectionMode_Error.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown_ExectionMode_Error.Name = "numericUpDown_ExectionMode_Error";
            this.numericUpDown_ExectionMode_Error.Size = new System.Drawing.Size(67, 21);
            this.numericUpDown_ExectionMode_Error.TabIndex = 6;
            this.numericUpDown_ExectionMode_Error.ValueChanged += new System.EventHandler(this.numericUpDown_ExectionMode_Error_ValueChanged);
            // 
            // numericUpDown_ExectionMode_OK
            // 
            this.numericUpDown_ExectionMode_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_ExectionMode_OK.Location = new System.Drawing.Point(208, 27);
            this.numericUpDown_ExectionMode_OK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown_ExectionMode_OK.Name = "numericUpDown_ExectionMode_OK";
            this.numericUpDown_ExectionMode_OK.Size = new System.Drawing.Size(67, 21);
            this.numericUpDown_ExectionMode_OK.TabIndex = 7;
            this.numericUpDown_ExectionMode_OK.ValueChanged += new System.EventHandler(this.numericUpDown_ExectionMode_OK_ValueChanged);
            // 
            // textBox_Tips
            // 
            this.textBox_Tips.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_Tips.Location = new System.Drawing.Point(0, 443);
            this.textBox_Tips.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Tips.Multiline = true;
            this.textBox_Tips.Name = "textBox_Tips";
            this.textBox_Tips.Size = new System.Drawing.Size(356, 85);
            this.textBox_Tips.TabIndex = 6;
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter.Location = new System.Drawing.Point(0, 441);
            this.splitter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(356, 2);
            this.splitter.TabIndex = 5;
            this.splitter.TabStop = false;
            this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SplitterMoved);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(0, 27);
            this.propertyGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(356, 414);
            this.propertyGrid.TabIndex = 4;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            this.propertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid_SelectedGridItemChanged);
            // 
            // toolStrip_PropertyGrid
            // 
            this.toolStrip_PropertyGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_PropertyGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Parameter,
            this.toolStripButton_Limit,
            this.toolStripButton_RestoreDefaultValue,
            this.toolStripSeparator1,
            this.toolStripLabel_CaseName});
            this.toolStrip_PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_PropertyGrid.Name = "toolStrip_PropertyGrid";
            this.toolStrip_PropertyGrid.Size = new System.Drawing.Size(356, 27);
            this.toolStrip_PropertyGrid.TabIndex = 3;
            this.toolStrip_PropertyGrid.Text = "toolStrip1";
            // 
            // toolStripButton_Parameter
            // 
            this.toolStripButton_Parameter.Image = global::xwx.TDP.Editor.Properties.Resources.parameter;
            this.toolStripButton_Parameter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Parameter.Name = "toolStripButton_Parameter";
            this.toolStripButton_Parameter.Size = new System.Drawing.Size(92, 24);
            this.toolStripButton_Parameter.Text = "Parameter";
            this.toolStripButton_Parameter.Click += new System.EventHandler(this.toolStripButton_Parameter_Click);
            // 
            // toolStripButton_Limit
            // 
            this.toolStripButton_Limit.Image = global::xwx.TDP.Editor.Properties.Resources.limits;
            this.toolStripButton_Limit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Limit.Name = "toolStripButton_Limit";
            this.toolStripButton_Limit.Size = new System.Drawing.Size(59, 24);
            this.toolStripButton_Limit.Text = "Limit";
            this.toolStripButton_Limit.Click += new System.EventHandler(this.toolStripButton_Limit_Click);
            // 
            // toolStripButton_RestoreDefaultValue
            // 
            this.toolStripButton_RestoreDefaultValue.Image = global::xwx.TDP.Editor.Properties.Resources.refresh;
            this.toolStripButton_RestoreDefaultValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_RestoreDefaultValue.Name = "toolStripButton_RestoreDefaultValue";
            this.toolStripButton_RestoreDefaultValue.Size = new System.Drawing.Size(105, 24);
            this.toolStripButton_RestoreDefaultValue.Text = "DefaultValue";
            this.toolStripButton_RestoreDefaultValue.Click += new System.EventHandler(this.toolStripButton_RestoreDefaultValue_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel_CaseName
            // 
            this.toolStripLabel_CaseName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_CaseName.Name = "toolStripLabel_CaseName";
            this.toolStripLabel_CaseName.Size = new System.Drawing.Size(34, 24);
            this.toolStripLabel_CaseName.Text = "case";
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Save,
            this.toolStripButton_Discard,
            this.toolStripLabel_SequenceName});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(646, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButton_Save
            // 
            this.toolStripButton_Save.Image = global::xwx.TDP.Editor.Properties.Resources.save;
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(59, 24);
            this.toolStripButton_Save.Text = "Save";
            this.toolStripButton_Save.Click += new System.EventHandler(this.toolStripButton_Save_Click);
            // 
            // toolStripButton_Discard
            // 
            this.toolStripButton_Discard.Image = global::xwx.TDP.Editor.Properties.Resources.discard;
            this.toolStripButton_Discard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Discard.Name = "toolStripButton_Discard";
            this.toolStripButton_Discard.Size = new System.Drawing.Size(76, 24);
            this.toolStripButton_Discard.Text = "Discard";
            this.toolStripButton_Discard.Click += new System.EventHandler(this.toolStripButton_Discard_Click);
            // 
            // toolStripLabel_SequenceName
            // 
            this.toolStripLabel_SequenceName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_SequenceName.Name = "toolStripLabel_SequenceName";
            this.toolStripLabel_SequenceName.Size = new System.Drawing.Size(99, 24);
            this.toolStripLabel_SequenceName.Text = "SequenceName";
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
            // SequenceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 547);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.splitContainer);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SequenceEditor";
            this.Text = "序列编辑器";
            this.Load += new System.EventHandler(this.SequenceEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SequenceEditor_KeyDown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel_EngineMode.ResumeLayout(false);
            this.tableLayoutPanel_EngineMode_Type.ResumeLayout(false);
            this.tableLayoutPanel_EngineMode_Type.PerformLayout();
            this.groupBox_EngineMode.ResumeLayout(false);
            this.tableLayoutPanel_ExecutionMode.ResumeLayout(false);
            this.tableLayoutPanel_ExecutionMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_Error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ExectionMode_OK)).EndInit();
            this.toolStrip_PropertyGrid.ResumeLayout(false);
            this.toolStrip_PropertyGrid.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panel_EngineMode;
        private TestManager.Utility.PropertyGridEx.PropertyGridEx propertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip_PropertyGrid;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.GroupBox groupBox_EngineMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_ExecutionMode;
        private System.Windows.Forms.Label label_ExecutionMode_Error;
        private System.Windows.Forms.Label label_ExecutionMode_OK;
        private System.Windows.Forms.Label label_ExecutionMode_Error_Retry;
        private System.Windows.Forms.Label label_ExecutionMode_OK_Retry;
        private System.Windows.Forms.ComboBox comboBox_ExecutionMode_Error;
        private System.Windows.Forms.ComboBox comboBox_ExecutionMode_OK;
        private System.Windows.Forms.NumericUpDown numericUpDown_ExectionMode_Error;
        private System.Windows.Forms.NumericUpDown numericUpDown_ExectionMode_OK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_EngineMode_Type;
        private System.Windows.Forms.RadioButton radioButton_EngineMode_Globe;
        private System.Windows.Forms.RadioButton radioButton_EngineMode_Case;
        private System.Windows.Forms.RadioButton radioButton_EngineMode_Skip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ToolStripButton toolStripButton_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton_Discard;
        private System.Windows.Forms.ToolStripButton toolStripButton_Parameter;
        private System.Windows.Forms.ToolStripButton toolStripButton_Limit;
        private System.Windows.Forms.ToolStripButton toolStripButton_RestoreDefaultValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_CaseName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_SequenceName;
        private TestManager.Utility.ExtendedControl.TreeViewEx treeView;
        private System.Windows.Forms.TextBox textBox_Tips;
    }
}