using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xwx.TDP.Common.Instruments;
using static xwx.TDP.Common.Instruments.MessageInstruments;

namespace xwx.TDP.Library.Common.Forms
{
    public partial class SystemConfig : Form
    {
        public SystemConfig()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = Configurations.Instance;
            propertyGrid2.SelectedObject = Settings.Instance;
            propertyGrid1.ExpandAllGridItems();
            propertyGrid2.ExpandAllGridItems();

            //隐藏一些现在没用，但是以后可能用的功能
            groupBox1.Hide();   
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Configurations.Instance.Save();
            Settings.Instance.Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            //this.textBox2.Text = CoreCaseBase.ConvertSnToIp(this.textBox1.Text);
            this.textBox1.Focus();
            this.textBox1.SelectAll();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //this.textBox2.Text = CoreCaseCommon.ConvertSnToIp(this.textBox1.Text);
                //this.textBox1.SelectAll();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            this.textBox1.SelectAll();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Name == this.tabPage3.Name)
            {
                this.textBox1.Focus();
                this.textBox1.SelectAll();
            }
        }

        private void buttonInstTest_Click(object sender, EventArgs e)
        {
            this.listView_Instrument_Info.Clear();
            bool isOk = true;
            string errMessage = string.Empty;
            string idn = string.Empty;

            isOk = true;
            foreach (PropertyInfo pi in Settings.Instance.GetType().GetProperties())
            {
                if (pi.PropertyType != typeof(DevInfo) && pi.PropertyType != typeof(DevInfo[]))
                    continue;
                isOk = true;

                DevInfo[] devInfos;
                if (pi.PropertyType == typeof(DevInfo[]))
                {
                    devInfos = (DevInfo[])pi.GetValue(Settings.Instance, null);
                }
                else
                {
                    DevInfo dev = (DevInfo)pi.GetValue(Settings.Instance, null);
                    devInfos = new DevInfo[1] { dev };
                }

                foreach (DevInfo info in devInfos)
                {
                    isOk = true;
                    try
                    {
                        MessageInstruments dev = InstrumentFactory.CreateInstanceFromDisplayName<MessageInstruments>(
                            info.DevType);
                        if (info.Global)
                        {
                            if (Settings.Instance.IsRemoteDriverType)
                            {
                                if (!dev.Initialize(info.DevAddr, Settings.Instance.InstrumentsServerIp))
                                {
                                    isOk = false;
                                    errMessage = dev.ErrString;
                                }
                            }
                            else
                            {
                                if (!dev.Initialize(info.DevAddr))
                                {
                                    isOk = false;
                                    errMessage = dev.ErrString;
                                }
                            }
                        }
                        else
                        {
                            if (info.IsRemote)
                            {
                                if (!dev.Initialize(info.DevAddr, info.SvrAddr))
                                {
                                    isOk = false;
                                    errMessage = dev.ErrString;
                                }
                            }
                            else
                            {
                                if (!dev.Initialize(info.DevAddr))
                                {
                                    isOk = false;
                                    errMessage = dev.ErrString;
                                }
                            }
                        }
                        MethodInfo mi = dev.GetType().GetMethod("GetIdn");
                        if (mi != null)
                        {
                            object[] prams = new object[] { null };
                            bool result;
                            result = (bool)mi.Invoke(dev, prams);
                            if (!result)
                            {
                                isOk = false;
                                errMessage = dev.ErrString;
                            }
                            idn = prams[0].ToString();
                        }
                        //if (!dev.QueryString("*IDN?\n",out idn))
                        //{
                        //    isOk = false;
                        //    errMessage = dev.ErrString;
                        //}
                    }

                    catch (Exception ex)
                    {
                        errMessage = ex.Message;
                        isOk = false;
                    }
                    this.listView_Instrument_Info.Items.Add(string.Format("{0}： {1}", info.DevType, isOk ? idn : errMessage)).ForeColor =
                        isOk ? SystemColors.WindowText : Color.Red;
                }
            }
        }

        private string _tcbRetString;
        private void OnTcbReceived(object sender, EventArgs e)
        {
            _tcbRetString += e.ToString();
        }

        //private SwitchComponent _switchComponent;
        //private MultifunctionSwitch _sw;
    }
}
