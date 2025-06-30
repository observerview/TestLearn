using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Utility.ExtendedControl;
using TestManager.Utility.Misc;
using xwx.TDP.Editor.Engine;
using xwx.TDP.Editor.Properties;

namespace xwx.TDP.Editor
{
    public partial class SequenceInfoEditor : Form
    {
        private bool _editMode;
        private TdpTestSequenceInfo _sequenceInfo = new TdpTestSequenceInfo();
        public bool EditMode
        {
            get
            {
                return this._editMode;
            }
            set
            {
                this._editMode = value;
                this.tableLayoutPanel_Info.Enabled = value;
                this.button_Cancel.Visible = value;
            }
        }
        public bool CanEditSequenceName
        {
            get
            {
                return this.textBox_DisplayName.Enabled;
            }
            set
            {
                this.textBox_DisplayName.Enabled = value;
            }
        }
        internal TdpTestSequenceInfo SequenceInfo
        {
            get
            {
                return this._sequenceInfo;
            }
            set
            {
                _sequenceInfo = value;
                textBox_Author.Text = this._sequenceInfo.Author;
                textBox_DisplayName.Text = this._sequenceInfo.DisplayName;
                textBox_CreatedTime.Text = this._sequenceInfo.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                textBox_ModifiedTime.Text = ((this._sequenceInfo.ModifiedTime == DateTime.MinValue) ? string.Empty : this._sequenceInfo.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"));
                textBox_Description.Text = this._sequenceInfo.Description;
                comboBox_ExecutionMode_Error.SelectedValue = this._sequenceInfo.GlobeEngineMode.Error;
                numericUpDown_ExectionMode_Error.Value = this._sequenceInfo.GlobeEngineMode.ErrorRetry;
                comboBox_ExecutionMode_OK.SelectedValue = this._sequenceInfo.GlobeEngineMode.OK;
                numericUpDown_ExectionMode_OK.Value = this._sequenceInfo.GlobeEngineMode.OkRetry;
            }
        }

        public SequenceInfoEditor()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            Utilities.SetControlFont(this, Settings.Default.UI_DefaultFont);
            this.comboBox_ExecutionMode_Error.DataSource = new EnumDataSourceWrapper<EngineMode_Error>();
            this.comboBox_ExecutionMode_Error.DisplayMember = "DisplayValue";
            this.comboBox_ExecutionMode_Error.ValueMember = "Value";
            this.comboBox_ExecutionMode_OK.DataSource = new EnumDataSourceWrapper<EngineMode_OK>();
            this.comboBox_ExecutionMode_OK.DisplayMember = "DisplayValue";
            this.comboBox_ExecutionMode_OK.ValueMember = "Value";
            base.DialogResult = DialogResult.Cancel;
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox_DisplayName.Text))
            {
                this.textBox_DisplayName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.textBox_Description.Text))
            {
                this.textBox_Description.Focus();
                return;
            }
            this._sequenceInfo.Author = this.textBox_Author.Text;
            this._sequenceInfo.DisplayName = this.textBox_DisplayName.Text;
            this._sequenceInfo.Description = this.textBox_Description.Text;
            this._sequenceInfo.GlobeEngineMode.Error = (EngineMode_Error)this.comboBox_ExecutionMode_Error.SelectedValue;
            this._sequenceInfo.GlobeEngineMode.ErrorRetry = (uint)this.numericUpDown_ExectionMode_Error.Value;
            this._sequenceInfo.GlobeEngineMode.OK = (EngineMode_OK)this.comboBox_ExecutionMode_OK.SelectedValue;
            this._sequenceInfo.GlobeEngineMode.OkRetry = (uint)this.numericUpDown_ExectionMode_OK.Value;
            base.DialogResult = DialogResult.OK;
            base.Close();
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void UpdateEngineModeUI()
        {
            if (this.comboBox_ExecutionMode_Error.SelectedValue is EngineMode_Error)
            {
                switch ((EngineMode_Error)comboBox_ExecutionMode_Error.SelectedValue)
                {
                    case EngineMode_Error.Abort:
                    case EngineMode_Error.Ignore:
                        this.numericUpDown_ExectionMode_Error.Enabled = false;
                        break;
                }
                numericUpDown_ExectionMode_Error.Enabled = true;
            }
            if (comboBox_ExecutionMode_OK.SelectedValue is EngineMode_OK)
            {
                switch ((EngineMode_OK)comboBox_ExecutionMode_OK.SelectedValue)
                {
                    case EngineMode_OK.IgnoreFail:
                    case EngineMode_OK.AbortOnFail:
                        numericUpDown_ExectionMode_OK.Enabled = false;
                        return;
                    case EngineMode_OK.RetestOnFail:
                    case EngineMode_OK.Repeat:
                    case EngineMode_OK.RepeatUntilFail:
                    case EngineMode_OK.RepeatUntilPass:
                        numericUpDown_ExectionMode_OK.Enabled = true;
                        break;
                    default:
                        return;
                }
            }
        }

        private void comboBox_ExecutionMode_OK_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateEngineModeUI();
        }

        private void comboBox_ExecutionMode_Error_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateEngineModeUI();
        }

        private void comboBox_ExecutionMode_OK_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateEngineModeUI();
        }

        private void comboBox_ExecutionMode_Error_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateEngineModeUI();
        }
    }
}
