using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xwx.TDP.Common.Instruments;
using static xwx.TDP.Common.Instruments.MessageInstruments;

namespace xwx.TDP.Library.Common.Forms
{
    public partial class DevSettings<T> : Form where T: MessageInstruments
    {
        DevInfo devInfo;
        public DevSettings(DevInfo dev)
        {
            InitializeComponent();
            this.devInfo = dev;
            List<InstrumentInfomation<T>> instList = new List<InstrumentInfomation<T>>();
            InstrumentFactory.GetSupportedInstruments<T>(out instList);
            this.comboBoxInsType.Items.AddRange(instList.ToArray());
            this.comboBoxInsType.Text = dev.DevType;
            this.comboBoxInsAddr.Text = dev.DevAddr;
            this.checkBoxGlobal.Checked = dev.Global;
            if (dev.Global)
            {
                this.checkBoxRemote.Checked = Settings.Instance.IsRemoteDriverType;
                this.textBoxSerAddr.Text = Settings.Instance.InstrumentsServerIp;
                this.checkBoxRemote.Enabled = false;
                this.textBoxSerAddr.Enabled = false;
                this.groupBoxFileSelect.Enabled = false;
            }
            else
            {
                this.checkBoxRemote.Checked = dev.IsRemote;
                this.textBoxSerAddr.Text = dev.SvrAddr;
                this.checkBoxRemote.Enabled = true;
                this.textBoxSerAddr.Enabled = true;
                this.groupBoxFileSelect.Enabled = true;
            }
            this.checkBoxGlobal.CheckedChanged += new EventHandler(checkBoxGlobal_CheckedChanged);

            //if (typeof(T) == typeof(MultifunctionSwitch))
            //{
            //    this.groupBoxFileSelect.Visible = true;
            //}
            //this.textBoxSwitchFile.Text = devInfo.SwitchFile;
        }
        void checkBoxGlobal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxGlobal.Checked)
            {
                this.checkBoxRemote.Checked = Settings.Instance.IsRemoteDriverType;
                this.textBoxSerAddr.Text = Settings.Instance.InstrumentsServerIp;
                this.checkBoxRemote.Enabled = false;
                this.textBoxSerAddr.Enabled = false;
                this.groupBoxFileSelect.Enabled = false;
            }
            else
            {
                this.checkBoxRemote.Checked = devInfo.IsRemote;
                this.textBoxSerAddr.Text = devInfo.SvrAddr;
                this.checkBoxRemote.Enabled = true;
                this.textBoxSerAddr.Enabled = true;
                this.groupBoxFileSelect.Enabled = true;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            devInfo.DevType = this.comboBoxInsType.Text;
            devInfo.DevAddr = this.comboBoxInsAddr.Text;
            devInfo.Global = this.checkBoxGlobal.Checked;
            if (!this.checkBoxGlobal.Checked)
            {
                devInfo.IsRemote = this.checkBoxRemote.Checked;
                devInfo.SvrAddr = this.textBoxSerAddr.Text;
                //devInfo.SwitchFile = this.textBoxSwitchFile.Text;
            }
            this.Close();
        }
        private void buttonFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "开关文件(*.xml)|*.xml|所有文件(*.*)|*.*";
            ofd.DefaultExt = "*.xml";
            ofd.CheckPathExists = true;
            ofd.FileName = this.textBoxSwitchFile.Text;
            ofd.InitialDirectory = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location) + "\\config";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxSwitchFile.Text = Path.GetFileName(ofd.SafeFileName);
            }
        }
    }
}
