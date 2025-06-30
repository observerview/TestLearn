using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Utility;
using TestManager.Extern;
using System.ComponentModel;
using TestManager.Utility.PropertyGridEx;
using System.Windows.Forms;

namespace xwx.TDP.Library.BaseCase
{
    [Category("Flow Control")]
    [DisplayName("Message Box")]
    [DisplayColor("Fuchsia")]
    [Description("Show a message box to user.")]
    public class MessageBox : CoreCase
    {
        private class ParameterSetting : ConfigBase
        {
            private string _displayText = "Message text to display.";

            private MessageBoxIcon _icon = MessageBoxIcon.Asterisk;


            [Category("A. Misc")]
            [DisplayName("A01. Message Text")]
            [DefaultValue("Message text to display.")]
            [Description("Set the message text to show.")]
            public string DisplayText
            {
                get
                {
                    return _displayText;
                }
                set
                {
                    _displayText = (string.IsNullOrEmpty(value) ? "Message text to display." : value);
                }
            }

            [Category("A. Misc")]
            [DisplayName("A02. Icon")]
            [DefaultValue(typeof(MessageBoxIcon), "Information")]
            [Description("Set the icon to show.")]
            public MessageBoxIcon Icon
            {
                get
                {
                    return _icon;
                }
                set
                {
                    _icon = value;
                }
            }
        }

        private ParameterSetting _parameterSetting = new ParameterSetting();

        public override ConfigBase CaseParameterSetting
        {
            get
            {
                return (ConfigBase)(object)_parameterSetting;
            }
            set
            {
                if (value is ParameterSetting)
                {
                    _parameterSetting = value as ParameterSetting;
                }
            }
        }
        public override ExecOkError Exec()
        {
           LogMessageBox(_parameterSetting.DisplayText,CurrentPlanName,MessageBoxButtons.OK,_parameterSetting.Icon);
           return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            return ExecOkError.OK;
        }

        public override ExecOkError PreExec()
        {
            return ExecOkError.OK;
        }
    }
}
