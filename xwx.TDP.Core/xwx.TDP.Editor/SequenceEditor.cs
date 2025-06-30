using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern;
using TestManager.Utility.ExtendedControl;
using TestManager.Utility.GenericForm;
using TestManager.Utility.Misc;
using TestManager.Utility.PropertyGridEx;
using WeifenLuo.WinFormsUI.Docking;
using xwx.TDP.Editor.Engine;
using xwx.TDP.Editor.Misc;
using xwx.TDP.Editor.Properties;
using xwx.TDP.Library.BaseCase;

namespace xwx.TDP.Editor
{
    public partial class SequenceEditor : DockContent,ITdpModuleAuthorizationAdmin
    {
        public static string CurrentEditSequenceFile = string.Empty;

        private string _sequenceXmlFileName = string.Empty;

        private TdpTestSequence _tdpTestSequence;

        private bool _isParameterTabSeleted = true;

        private bool _isDiscardRequired;

        private TreeNode _copiedNode = null;

        private TdpEdition _tsdpEditionAuthorization = TdpEdition.Standard;
        public bool IsNeedSave
        {
            get
            {
                return this.toolStripButton_Save.Enabled;
            }
        }
        public string SequenceXmlFileName
        {
            set
            {
                if (this.toolStripButton_Save.Enabled && !this._isDiscardRequired && MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_SavePromptMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    this.toolStripButton_Save_Click(this.toolStripButton_Save, new EventArgs());
                }
                this._isDiscardRequired = false;
                this._sequenceXmlFileName = value;
                if (string.IsNullOrEmpty(this._sequenceXmlFileName))
                {
                    SequenceEditor.CurrentEditSequenceFile = string.Empty;
                    this.toolStripLabel_SequenceName.Text = Resources.LNG_TDP_SequenceEditor_NoSequenceLoaded;
                    this.toolStripLabel_SequenceName.ToolTipText = Resources.LNG_TDP_SequenceEditor_NoSequenceLoaded;
                    this.toolStripLabel_SequenceName.ForeColor = Color.Black;
                    this._tdpTestSequence = null;
                    this.treeView.Nodes.Clear();
                    this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
                    this.toolStripButton_Save.Enabled = false;
                    this.toolStripButton_Discard.Enabled = false;
                    return;
                }
                this._tdpTestSequence = new TdpTestSequence();
                if (!this._tdpTestSequence.LoadFromFile(value))
                {
                    string errorMessage = this._tdpTestSequence.ErrorMessage;
                    SequenceEditor.CurrentEditSequenceFile = string.Empty;
                    this.toolStripLabel_SequenceName.Text = string.Format(Resources.LNG_TDP_Common_Error + ": {0} ({1})", this._tdpTestSequence.SequenceInfo.DisplayName, value.Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1));
                    this.toolStripLabel_SequenceName.ToolTipText = errorMessage;
                    this.toolStripLabel_SequenceName.ForeColor = Color.Red;
                    this._tdpTestSequence = null;
                    this.treeView.Nodes.Clear();
                    this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
                    this.toolStripButton_Save.Enabled = false;
                    this.toolStripButton_Discard.Enabled = false;
                    MessageBoxEx.Show(base.MdiParent, errorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (this._tdpTestSequence.IsHasReadingParameterOrLimit2XmlWarning)
                {
                    MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceEditor_VersionNotMatchedWarning1, this._tdpTestSequence.SequenceInfo.DisplayName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                this.fileSystemWatcher.Path = Path.GetDirectoryName(value);
                SequenceEditor.CurrentEditSequenceFile = value;
                this.toolStripLabel_SequenceName.Text = string.Format("{0} ({1})", this._tdpTestSequence.SequenceInfo.DisplayName, value.Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1));
                this.toolStripLabel_SequenceName.ToolTipText = this.toolStripLabel_SequenceName.Text;
                this.toolStripLabel_SequenceName.ForeColor = Color.Black;
                this.BindConfigPropertyValueChangedEvent(this._tdpTestSequence);
                this.RestoreOriginalTestSequenceUI();
            }
        }
        public SequenceEditor()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            Utilities.SetControlFont(this, Settings.Default.UI_DefaultFont);
            propertyGrid.FirstColumnWidthPercent = 0.45;
            comboBox_ExecutionMode_Error.DataSource = new EnumDataSourceWrapper<EngineMode_Error>();
            comboBox_ExecutionMode_Error.DisplayMember = "DisplayValue";
            comboBox_ExecutionMode_Error.ValueMember = "Value";
            comboBox_ExecutionMode_OK.DataSource = new EnumDataSourceWrapper<EngineMode_OK>();
            comboBox_ExecutionMode_OK.DisplayMember = "DisplayValue";
            comboBox_ExecutionMode_OK.ValueMember = "Value";

            toolStripButton_Save.Enabled = false;
            toolStripButton_Discard.Enabled = false;
        }

        private void CopySelectedNode()
        {//复制粘贴有bug，新复制出来的node，参数会和原node共享，暂时没空研究。先注销

            //if (treeView.SelectedNode != null)
            //{
            //    _copiedNode = (TreeNode)this.treeView.SelectedNode.Clone();
            //}
        }

        private void PasteNode()
        {
            //if (_copiedNode != null && this.treeView.SelectedNode != null)
            //{
            //    TreeNode newNode = (TreeNode)_copiedNode.Clone();
            //    TreeNode parentNode = this.treeView.SelectedNode.Parent;
            //    if (parentNode != null)
            //    {
            //        int index = parentNode.Nodes.IndexOf(this.treeView.SelectedNode);
            //        parentNode.Nodes.Insert(index + 1, newNode);
            //    }
            //    else
            //    {
            //        int index = this.treeView.Nodes.IndexOf(this.treeView.SelectedNode);
            //        this.treeView.Nodes.Insert(index + 1, newNode);
            //    }
            //    this.treeView.SelectedNode.Expand();
            //}
            //// 设置保存和丢弃按钮的状态
            //toolStripButton_Save.Enabled = true;
            //toolStripButton_Discard.Enabled = true;
        }


        private void toolStripButton_RestoreDefaultValue_Click(object sender, EventArgs e)
        {
            if (propertyGrid.SelectedObject != null)
            {
                object value = propertyGrid.SelectedGridItem.Value;
                propertyGrid.ResetSelectedProperty();
                propertyGrid_PropertyValueChanged(propertyGrid, new PropertyValueChangedEventArgs(propertyGrid.SelectedGridItem, value));
                toolStripButton_Save.Enabled = true;
                toolStripButton_Discard.Enabled = true;
                toolStripButton_RestoreDefaultValue.Enabled = false;
            }
        }

        private void toolStripButton_Limit_Click(object sender, EventArgs e)
        {
            toolStripButton_Limit.Checked = true;
            toolStripButton_Parameter.Checked = false;
            _isParameterTabSeleted = false;
            textBox_Tips.Text = string.Empty;
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
        }

        private void toolStripButton_Parameter_Click(object sender, EventArgs e)
        {
            this.toolStripButton_Limit.Checked = false;
            this.toolStripButton_Parameter.Checked = true;
            this._isParameterTabSeleted = true;
            this.textBox_Tips.Text = string.Empty;
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this._sequenceXmlFileName))
            {
                return;
            }
            TdpTestSequence tdpTestSequence = new TdpTestSequence();
            this._tdpTestSequence.SequenceInfo.ModifiedTime = DateTime.Now;
            tdpTestSequence.LoadFromTreeNode(this._tdpTestSequence.SequenceInfo, this.treeView.Nodes);
            if (!tdpTestSequence.WriteToFile(this._sequenceXmlFileName))
            {
                MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_SaveFailed + "\n\n" + tdpTestSequence.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_SaveSuccessfully, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.toolStripButton_Save.Enabled = false;
            this.toolStripButton_Discard.Enabled = false;
        }

        private void toolStripButton_Discard_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this._sequenceXmlFileName))
            {
                return;
            }
            if (MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_DiscardWarning, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                this._isDiscardRequired = true;
                this.SequenceXmlFileName = this._sequenceXmlFileName;
                this.RestoreOriginalTestSequenceUI();
            }
        }

        #region event
        private void CaseSetting_ConfigPropertyValueChanged(object sender, ConfigPropertyValueChangedEventArgs e)
        {
            object selectedObject = this.propertyGrid.SelectedObject;
            this.propertyGrid.SelectedObject = new object();
            this.propertyGrid.SelectedObject = selectedObject;
            this.propertyGrid.Update();
            if (this.propertyGrid.SelectedGridItem != null && this.propertyGrid.SelectedGridItem.PropertyDescriptor != null)
            {
                this.toolStripButton_RestoreDefaultValue.Enabled = this.propertyGrid.SelectedGridItem.PropertyDescriptor.CanResetValue(this.propertyGrid.SelectedObject);
            }
            else
            {
                this.toolStripButton_RestoreDefaultValue.Enabled = false;
            }
            this.toolStripButton_Save.Enabled = true;
            this.toolStripButton_Discard.Enabled = true;
        }
        private void BindConfigPropertyValueChangedEvent(TdpTestSequence tdpTestSequence)
        {
            if (tdpTestSequence != null)
            {
                for (int i = 0; i < tdpTestSequence.TdpTestCases.Count; i++)
                {
                    this.BindConfigPropertyValueChangedEvent(tdpTestSequence.TdpTestCases[i]);
                }
            }
        }
        private void BindConfigPropertyValueChangedEvent(TdpTestCase tdpTestCase)
        {
            tdpTestCase.SelfTestCase.CaseInstance.CaseLimitSetting.ConfigPropertyValueChanged += this.CaseSetting_ConfigPropertyValueChanged;
            tdpTestCase.SelfTestCase.CaseInstance.CaseParameterSetting.ConfigPropertyValueChanged += this.CaseSetting_ConfigPropertyValueChanged;
            for (int i = 0; i < tdpTestCase.ContainedTdpTestCase.Count; i++)
            {
                this.BindConfigPropertyValueChangedEvent(tdpTestCase.ContainedTdpTestCase[i]);
            }
        }

        #endregion

        private void RestoreOriginalTestSequenceUI()
        {
            this.treeView.BeginUpdate();
            this.treeView.Nodes.Clear();
            if (this._tdpTestSequence != null)
            {
                TreeNode treeNode = this._tdpTestSequence.WriteToTreeNode();
                if (treeNode == null)
                {
                    SequenceEditor.CurrentEditSequenceFile = string.Empty;
                    this.toolStripLabel_SequenceName.Text = this._tdpTestSequence.ErrorMessage;
                    this.toolStripLabel_SequenceName.ToolTipText = this.toolStripLabel_SequenceName.Text;
                    return;
                }
                foreach (object obj in treeNode.Nodes)
                {
                    TreeNode treeNode2 = (TreeNode)obj;
                    treeNode2.ExpandAll();
                    this.treeView.Nodes.Add(treeNode2);
                }
                if (this.treeView.Nodes.Count > 0)
                {
                    this.treeView.SelectedNode = this.treeView.Nodes[0];
                }
            }
            this.treeView.EndUpdate();
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
            this.toolStripButton_Save.Enabled = false;
            this.toolStripButton_Discard.Enabled = false;
            this.treeView.Focus();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.UnRegistEngineModeInputControlEvent();
            this.textBox_Tips.Text = string.Empty;
            if (e.Node != null)
            {
                TdpTestCase tdpTestCase = (e.Node.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase;
                this.propertyGrid.SelectedObject = (this._isParameterTabSeleted ? tdpTestCase.SelfTestCase.CaseInstance.CaseParameterSetting : tdpTestCase.SelfTestCase.CaseInstance.CaseLimitSetting);
                this.toolStripLabel_CaseName.Text = tdpTestCase.SelfTestCase.CaseName;
                switch (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type)
                {
                    case EngineType.Globe:
                        this.radioButton_EngineMode_Globe.Checked = true;
                        break;
                    case EngineType.CaseSpecified:
                        this.radioButton_EngineMode_Case.Checked = true;
                        break;
                    case EngineType.Skip:
                        this.radioButton_EngineMode_Skip.Checked = true;
                        break;
                }
                this.comboBox_ExecutionMode_Error.SelectedValue = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Error;
                this.numericUpDown_ExectionMode_Error.Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.ErrorRetry;
                switch (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Error)
                {
                    case EngineMode_Error.Abort:
                    case EngineMode_Error.Ignore:
                        this.numericUpDown_ExectionMode_Error.Enabled = false;
                        this.comboBox_ExecutionMode_OK.SelectedValue = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OK;
                        this.numericUpDown_ExectionMode_OK.Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OkRetry;
                        break;
                }
                this.numericUpDown_ExectionMode_Error.Enabled = true;

                 switch (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OK)
                {
                    case EngineMode_OK.IgnoreFail:
                    case EngineMode_OK.AbortOnFail:
                        this.numericUpDown_ExectionMode_OK.Enabled = false;
                        break;
                    case EngineMode_OK.RetestOnFail:
                    case EngineMode_OK.Repeat:
                    case EngineMode_OK.RepeatUntilFail:
                    case EngineMode_OK.RepeatUntilPass:
                        this.numericUpDown_ExectionMode_OK.Enabled = true;
                        break;
                }
                this.panel_EngineMode.Visible = !((e.Node.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.CaseInstance is CaseFolder);
                this.tableLayoutPanel_ExecutionMode.Visible = (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type == EngineType.CaseSpecified);
                this.RegistEngineModeInputControlEvent();
            }
            else
            {
                this.panel_EngineMode.Visible = false;
                this.propertyGrid.SelectedObject = Type.Missing;
                this.toolStripLabel_CaseName.Text = string.Empty;
            }
            this.treeView.Focus();
        }

        private void propertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            if (e.NewSelection != null && e.NewSelection.PropertyDescriptor != null)
            {
                this.toolStripButton_RestoreDefaultValue.Enabled = e.NewSelection.PropertyDescriptor.CanResetValue(this.propertyGrid.SelectedObject);
            }
            else
            {
                this.toolStripButton_RestoreDefaultValue.Enabled = false;
            }
            if (e.NewSelection != null && e.NewSelection.Value != null)
            {
                e.NewSelection.Value.GetType();
                string displayName = e.NewSelection.PropertyDescriptor.DisplayName;
                string text = "N/A";
                string text2 = "N/A";
                string description = e.NewSelection.PropertyDescriptor.Description;
                AttributeCollection attributes = e.NewSelection.PropertyDescriptor.Attributes;
                foreach (object obj in attributes)
                {
                    Attribute attribute = (Attribute)obj;
                    if (attribute is ValueLimitsAttribute)
                    {
                        text = (attribute as ValueLimitsAttribute).ToString();
                    }
                    if (attribute is DefaultValueAttribute && (attribute as DefaultValueAttribute).Value != null)
                    {
                        text2 = (attribute as DefaultValueAttribute).Value.ToString();
                        if ((attribute as DefaultValueAttribute).Value.GetType().IsEnum)
                        {
                            text2 = EnumDisplayNameAttribute.GetEnumerationDisplayName((attribute as DefaultValueAttribute).Value.GetType(), text2);
                        }
                    }
                }
                this.textBox_Tips.Text = string.Format(string.Concat(new string[]
                {
                    "{0}\r\n\r\n",
                    Resources.LNG_TDP_SequenceEditor_DefaultValue,
                    ":\t{1}\r\n",
                    Resources.LNG_TDP_SequenceEditor_ValueRange,
                    ":\t{2}\r\n\r\n{3}"
                }), new object[]
                {
                    displayName,
                    text2,
                    text,
                    description.Replace("\n", "\r\n")
                });
                return;
            }
            this.textBox_Tips.Text = string.Empty;
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.TDP_SequenceEditor_InfoWindowHeigh = this.textBox_Tips.Height;
            Settings.Default.Save();
        }

        private void SequenceEditor_Load(object sender, EventArgs e)
        {
            textBox_Tips.Height = Settings.Default.TDP_SequenceEditor_InfoWindowHeigh;
        }

        private void radioButton_EngineMode_Globe_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel_ExecutionMode.Visible = radioButton_EngineMode_Case.Checked;
            UpdateSelectedNodeCaseEngineMode();
        }

        private void radioButton_EngineMode_Case_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel_ExecutionMode.Visible = radioButton_EngineMode_Case.Checked;
            UpdateSelectedNodeCaseEngineMode();
        }

        private void radioButton_EngineMode_Skip_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel_ExecutionMode.Visible = radioButton_EngineMode_Case.Checked;
            UpdateSelectedNodeCaseEngineMode();
        }

        private void comboBox_ExecutionMode_Error_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateSelectedNodeCaseEngineMode();
        }

        private void comboBox_ExecutionMode_OK_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateSelectedNodeCaseEngineMode();
        }

        private void numericUpDown_ExectionMode_Error_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedNodeCaseEngineMode();
        }

        private void numericUpDown_ExectionMode_OK_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedNodeCaseEngineMode();
        }
        private void UpdateSelectedNodeCaseEngineMode()
        {
            if (this.treeView.SelectedNode == null)
            {
                return;
            }
            TdpTestCase tdpTestCase = (this.treeView.SelectedNode.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase;
            if (this.radioButton_EngineMode_Globe.Checked)
            {
                tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type = EngineType.Globe;
            }
            else if (this.radioButton_EngineMode_Case.Checked)
            {
                tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type = EngineType.CaseSpecified;
            }
            else if (this.radioButton_EngineMode_Skip.Checked)
            {
                tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type = EngineType.Skip;
            }
            tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Error = (EngineMode_Error)this.comboBox_ExecutionMode_Error.SelectedValue;
            tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.ErrorRetry = (uint)this.numericUpDown_ExectionMode_Error.Value;
            switch (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Error)
            {
                case EngineMode_Error.Abort:
                case EngineMode_Error.Ignore:
                    this.numericUpDown_ExectionMode_Error.Enabled = false;
                    tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OK = (EngineMode_OK)this.comboBox_ExecutionMode_OK.SelectedValue;
                    tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OkRetry = (uint)this.numericUpDown_ExectionMode_OK.Value;
                    break;
            }
            this.numericUpDown_ExectionMode_Error.Enabled = true;

             switch (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OK)
            {
                case EngineMode_OK.IgnoreFail:
                case EngineMode_OK.AbortOnFail:
                    this.numericUpDown_ExectionMode_OK.Enabled = false;
                    break;
                case EngineMode_OK.RetestOnFail:
                case EngineMode_OK.Repeat:
                case EngineMode_OK.RepeatUntilFail:
                case EngineMode_OK.RepeatUntilPass:
                    this.numericUpDown_ExectionMode_OK.Enabled = true;
                    break;
            }
            this.treeView.SelectedNode.ForeColor = tdpTestCase.SelfTestCase.DisplayColor;
            this.toolStripButton_Save.Enabled = true;
            this.toolStripButton_Discard.Enabled = true;
            this.treeView.Focus();
        }

        private void RegistEngineModeInputControlEvent()
        {
            this.radioButton_EngineMode_Case.CheckedChanged += this.radioButton_EngineMode_Case_CheckedChanged;
            this.radioButton_EngineMode_Globe.CheckedChanged += this.radioButton_EngineMode_Globe_CheckedChanged;
            this.radioButton_EngineMode_Skip.CheckedChanged += this.radioButton_EngineMode_Skip_CheckedChanged;
            this.comboBox_ExecutionMode_Error.SelectionChangeCommitted += this.comboBox_ExecutionMode_Error_SelectionChangeCommitted;
            this.comboBox_ExecutionMode_OK.SelectionChangeCommitted += this.comboBox_ExecutionMode_OK_SelectionChangeCommitted;
            this.numericUpDown_ExectionMode_Error.ValueChanged += this.numericUpDown_ExectionMode_Error_ValueChanged;
            this.numericUpDown_ExectionMode_OK.ValueChanged += this.numericUpDown_ExectionMode_OK_ValueChanged;
        }

        private void UnRegistEngineModeInputControlEvent()
        {
            this.radioButton_EngineMode_Case.CheckedChanged -= this.radioButton_EngineMode_Case_CheckedChanged;
            this.radioButton_EngineMode_Globe.CheckedChanged -= this.radioButton_EngineMode_Globe_CheckedChanged;
            this.radioButton_EngineMode_Skip.CheckedChanged -= this.radioButton_EngineMode_Skip_CheckedChanged;
            this.comboBox_ExecutionMode_Error.SelectionChangeCommitted -= this.comboBox_ExecutionMode_Error_SelectionChangeCommitted;
            this.comboBox_ExecutionMode_OK.SelectionChangeCommitted -= this.comboBox_ExecutionMode_OK_SelectionChangeCommitted;
            this.numericUpDown_ExectionMode_Error.ValueChanged -= this.numericUpDown_ExectionMode_Error_ValueChanged;
            this.numericUpDown_ExectionMode_OK.ValueChanged -= this.numericUpDown_ExectionMode_OK_ValueChanged;
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if(e.Item != null && e.Item is TreeNode)
            {
                treeView.DoDragDrop(e.Item as TreeNode, DragDropEffects.Move);
            }
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            if (this._tdpTestSequence == null)
            {
                return;
            }
            object data = e.Data.GetData(typeof(TdplCaseInfo));
            object data2 = e.Data.GetData(typeof(TdpTestSequenceInfo));
            object data3 = e.Data.GetData(typeof(TreeNode));
            TreeNode nodeAt = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
            if (nodeAt != null)
            {
                nodeAt.Expand();
            }
            if ((data != null && data is TdplCaseInfo) || (data2 != null && data2 is TdpTestSequenceInfo))
            {
                e.Effect = DragDropEffects.Copy;
                if (this.treeView.Nodes.Count == 0)
                {
                    return;
                }
                if (nodeAt == null)
                {
                    this.treeView.SetInsertMarkerAfterNode(this.treeView.Nodes[this.treeView.Nodes.Count - 1], true);
                    return;
                }
                if ((nodeAt.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.CaseInstance is CaseFolder && nodeAt.Nodes.Count == 0)
                {
                    this.treeView.SetInsertMarkerAfterNode(null, true);
                    this.treeView.SelectedNode = nodeAt;
                    return;
                }
                this.treeView.SetInsertMarkerAfterNode(nodeAt, false);
                return;
            }
            else
            {
                if (data3 == null || !(data3 is TreeNode) || !((data3 as TreeNode).Tag is TdpTestSequence.TreeNodeTagInfo))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                if (this.treeView.Nodes.Count == 0)
                {
                    return;
                }
                if (nodeAt == null)
                {
                    this.treeView.SetInsertMarkerAfterNode(this.treeView.Nodes[this.treeView.Nodes.Count - 1], true);
                    e.Effect = DragDropEffects.Move;
                    return;
                }
                bool flag = false;
                TreeNode treeNode = nodeAt;
                while (!flag && treeNode.Level > 0)
                {
                    if (object.Equals(data3 as TreeNode, treeNode.Parent))
                    {
                        flag = true;
                        break;
                    }
                    treeNode = treeNode.Parent;
                }
                if (flag)
                {
                    e.Effect = DragDropEffects.None;
                    this.treeView.SetInsertMarkerAfterNode(null, true);
                    return;
                }
                e.Effect = DragDropEffects.Move;
                if ((nodeAt.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.CaseInstance is CaseFolder && nodeAt.Nodes.Count == 0)
                {
                    this.treeView.SetInsertMarkerAfterNode(null, true);
                    this.treeView.SelectedNode = nodeAt;
                    return;
                }
                this.treeView.SetInsertMarkerAfterNode(nodeAt, false);
                return;
            }
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            if (this._tdpTestSequence == null)
            {
                this.treeView.SetInsertMarkerAfterNode(null, true);
                return;
            }
            object data = e.Data.GetData(typeof(TdplCaseInfo));
            object data2 = e.Data.GetData(typeof(TdpTestSequenceInfo));
            object data3 = e.Data.GetData(typeof(TreeNode));
            if (data != null && data is TdplCaseInfo)
            {
                TdplCaseInfo tsdplCaseInfo = data as TdplCaseInfo;
                TreeNode nodeAt = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
                TdpTestCase tsdpTestCase = new TdpTestCase();
                CoreCase coreCase = tsdplCaseInfo.CaseAssembly.CreateInstance(tsdplCaseInfo.CaseType.FullName) as CoreCase;
                tsdpTestCase.SelfTestCase = new TestCase(coreCase, new EngineMode());
                TreeNode treeNode = new TreeNode(tsdpTestCase.SelfTestCase.DisplayName);
                treeNode.Name = tsdpTestCase.SelfTestCase.DisplayName;
                treeNode.ForeColor = tsdpTestCase.SelfTestCase.DisplayColor;
                treeNode.Tag = new TdpTestSequence.TreeNodeTagInfo(tsdpTestCase);
                this.BindConfigPropertyValueChangedEvent(tsdpTestCase);
                if (nodeAt == null)
                {
                    this.treeView.Nodes.Add(treeNode);
                }
                else if ((nodeAt.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.CaseInstance is CaseFolder && nodeAt.Nodes.Count == 0)
                {
                    nodeAt.Nodes.Add(treeNode);
                    nodeAt.Expand();
                }
                else if (nodeAt.Level == 0)
                {
                    this.treeView.Nodes.Insert(nodeAt.Index, treeNode);
                }
                else
                {
                    nodeAt.Parent.Nodes.Insert(nodeAt.Index, treeNode);
                }
                this.toolStripButton_Save.Enabled = true;
                this.toolStripButton_Discard.Enabled = true;
            }
            else if (data2 != null && data2 is TdpTestSequenceInfo)
            {
                TdpTestSequenceInfo tsdpTestSequenceInfo = data2 as TdpTestSequenceInfo;
                TreeNode nodeAt2 = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
                TdpTestSequence tsdpTestSequence = new TdpTestSequence();
                if (!tsdpTestSequence.LoadFromFile(tsdpTestSequenceInfo.XmlFileFullName))
                {
                    MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_InsertSequenceFailed + "\n\n" + tsdpTestSequence.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (tsdpTestSequence.IsHasReadingParameterOrLimit2XmlWarning)
                    {
                        MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceEditor_VersionNotMatchedWarning2, tsdpTestSequence.SequenceInfo.DisplayName, this._tdpTestSequence.SequenceInfo.DisplayName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    TreeNode treeNode2 = tsdpTestSequence.WriteToTreeNode();
                    if (treeNode2 == null)
                    {
                        MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_InsertSequenceFailed + "\n\n" + tsdpTestSequence.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        TdpTestCase tsdpTestCase2 = new TdpTestCase();
                        CaseFolder coreCase2 = new CaseFolder(tsdpTestSequenceInfo.DisplayName);
                        tsdpTestCase2.SelfTestCase = new TestCase(coreCase2, new EngineMode());
                        this.BindConfigPropertyValueChangedEvent(tsdpTestCase2);
                        treeNode2.Name = tsdpTestCase2.SelfTestCase.DisplayName;
                        treeNode2.ForeColor = tsdpTestCase2.SelfTestCase.DisplayColor;
                        treeNode2.Tag = new TdpTestSequence.TreeNodeTagInfo(tsdpTestCase2);
                        this.BindConfigPropertyValueChangedEvent(tsdpTestSequence);
                        if (nodeAt2 == null)
                        {
                            this.treeView.Nodes.Add(treeNode2);
                        }
                        else if ((nodeAt2.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.CaseInstance is CaseFolder && nodeAt2.Nodes.Count == 0)
                        {
                            nodeAt2.Nodes.Add(treeNode2);
                            nodeAt2.Expand();
                        }
                        else if (nodeAt2.Level == 0)
                        {
                            this.treeView.Nodes.Insert(nodeAt2.Index, treeNode2);
                        }
                        else
                        {
                            nodeAt2.Parent.Nodes.Insert(nodeAt2.Index, treeNode2);
                        }
                    }
                    this.toolStripButton_Save.Enabled = true;
                    this.toolStripButton_Discard.Enabled = true;
                }
            }
            else if (data3 != null && data3 is TreeNode && (data3 as TreeNode).Tag is TdpTestSequence.TreeNodeTagInfo)
            {
                TreeNode nodeAt3 = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
                if (!object.Equals(data3 as TreeNode, nodeAt3))
                {
                    if (nodeAt3 == null)
                    {
                        (data3 as TreeNode).Remove();
                        this.treeView.Nodes.Add(data3 as TreeNode);
                    }
                    else if ((nodeAt3.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.CaseInstance is CaseFolder && nodeAt3.Nodes.Count == 0)
                    {
                        (data3 as TreeNode).Remove();
                        nodeAt3.Nodes.Add(data3 as TreeNode);
                        nodeAt3.Expand();
                    }
                    else if (nodeAt3.Level == 0)
                    {
                        (data3 as TreeNode).Remove();
                        this.treeView.Nodes.Insert(nodeAt3.Index, data3 as TreeNode);
                    }
                    else
                    {
                        (data3 as TreeNode).Remove();
                        nodeAt3.Parent.Nodes.Insert(nodeAt3.Index, data3 as TreeNode);
                    }
                    this.toolStripButton_Save.Enabled = true;
                    this.toolStripButton_Discard.Enabled = true;
                }
            }
            this.treeView.SetInsertMarkerAfterNode(null, true);
        }

        private void treeView_DragLeave(object sender, EventArgs e)
        {
            treeView.SetInsertMarkerAfterNode(null, true);
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.treeView.SelectedNode != null)
            {
                this.treeView.SelectedNode.Text = (this.treeView.SelectedNode.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase.DisplayName;
                this.treeView.SelectedNode.Name = this.treeView.SelectedNode.Text;
            }
            if (this.propertyGrid.SelectedGridItem != null && this.propertyGrid.SelectedGridItem.PropertyDescriptor != null)
            {
                this.toolStripButton_RestoreDefaultValue.Enabled = this.propertyGrid.SelectedGridItem.PropertyDescriptor.CanResetValue(this.propertyGrid.SelectedObject);
            }
            else
            {
                this.toolStripButton_RestoreDefaultValue.Enabled = false;
            }
            this.toolStripButton_Save.Enabled = true;
            this.toolStripButton_Discard.Enabled = true;
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyData = e.KeyData;
            if (keyData == (Keys.Control | Keys.C)) // Ctrl + C
            {
                CopySelectedNode();
                e.Handled = true;
            }
            else if (keyData == (Keys.Control | Keys.V)) // Ctrl + V
            {
                PasteNode();
                e.Handled = true;
            }
            if (keyData != Keys.Delete)
            {
                switch (keyData)
                {
                    case Keys.C:
                        this.radioButton_EngineMode_Case.PerformClick();
                        return;
                    case Keys.D:
                        break;
                    case Keys.E:
                        this.radioButton_EngineMode_Globe.PerformClick();
                        return;
                    default:
                        if (keyData != Keys.I)
                        {
                            return;
                        }
                        this.radioButton_EngineMode_Skip.PerformClick();
                        break;
                }
            }
            else
            {
                switch (this._tsdpEditionAuthorization)
                {
                    case TdpEdition.SuperLite:
                    case TdpEdition.Lite:
                        break;
                    default:
                        if (this.treeView.SelectedNode != null)
                        {
                            bool flag = true;
                            if (this.treeView.SelectedNode.Nodes.Count > 0 && MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceEditor_DeleteWarning, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                TreeNode selectedNode = (this.treeView.SelectedNode.NextNode != null) ? this.treeView.SelectedNode.NextNode : this.treeView.SelectedNode.PrevNode;
                                this.toolStripButton_Save.Enabled = true;
                                this.toolStripButton_Discard.Enabled = true;
                                this.treeView.SelectedNode.Remove();
                                this.treeView.SelectedNode = selectedNode;
                                this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
                                return;
                            }
                        }
                        break;
                }
            }
        }

        private void SequenceEditor_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyData = e.KeyData;
            if (keyData != Keys.F3)
            {
                if (keyData != (Keys)131155)
                {
                    return;
                }
                if (this.toolStripButton_Save.Enabled)
                {
                    this.toolStripButton_Save_Click(this.toolStripButton_Save, new EventArgs());
                    return;
                }
            }
            else
            {
                if (this._isParameterTabSeleted)
                {
                    this.toolStripButton_Limit_Click(this.toolStripButton_Limit, new EventArgs());
                    return;
                }
                this.toolStripButton_Parameter_Click(this.toolStripButton_Parameter, new EventArgs());
            }
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.treeView.GetNodeAt(e.Location);
            if (nodeAt != null)
            {
                this.treeView.SelectedNode = nodeAt;
            }
        }
        public void SaveSequence()
        {
            this.toolStripButton_Save_Click(this.toolStripButton_Save, new EventArgs());
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {

        }

        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (string.Compare(this._sequenceXmlFileName, e.FullPath, true) == 0)
            {
                this.SequenceXmlFileName = string.Empty;
            }
        }

        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {

        }
        public void RefreshPropertyGridDisplay()
        {
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
        }
        private void SequenceEditor_Paint(object sender, PaintEventArgs e)
        {
            this.propertyGrid.ResumeLayout();
        }
        public new bool Enabled
        {
            get
            {
                return this.splitContainer.Panel1.Enabled && this.splitContainer.Panel2.Enabled && this.toolStrip.Enabled;
            }
            set
            {
                this.splitContainer.Panel1.Enabled = value;
                this.splitContainer.Panel2.Enabled = value;
                this.toolStrip.Enabled = value;
            }
        }
        public TdpEdition TdpEditionAuthorization
        {
            set
            {
                this._tsdpEditionAuthorization = value;
                switch (value)
                {
                    case TdpEdition.SuperLite:
                        this.Enabled = false;
                        return;
                    case TdpEdition.Lite:
                        this.Enabled = true;
                        this.toolStripButton_Limit.Visible = false;
                        this.groupBox_EngineMode.Enabled = false;
                        this.treeView.AllowDrop = false;
                        return;
                    case TdpEdition.Standard:
                    case TdpEdition.Premium:
                        this.Enabled = true;
                        this.toolStripButton_Limit.Visible = true;
                        this.groupBox_EngineMode.Enabled = true;
                        this.treeView.AllowDrop = true;
                        return;
                    default:
                        return;
                }
            }
        }
    }
}
