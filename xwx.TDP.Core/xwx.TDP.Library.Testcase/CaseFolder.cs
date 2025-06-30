using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library.BaseCase
{
    [Category("Flow Control")]
    [DisplayName("Case Folder")]
    [DisplayColor("Fuchsia")]
    [Description("A folder to contain cases.")]
    public class CaseFolder : CoreCase
    {
        private class LimitSetting : ConfigBase
        {
        }
        private class ParameterSetting : ConfigBase
        {
            private string _displayName = "Case Folder";

            [DisplayName("A01. Display Name")]
            [Category("A. Misc")]
            [DefaultValue("Case Folder")]
            [Description("The name of this case folder.")]
            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
                set
                {
                    _displayName = (string.IsNullOrEmpty(value) ? "Case Folder" : value);
                }
            }
        }
        public const string DefaultDisplayName = "Case Folder";

        private LimitSetting _limitSetting = new LimitSetting();

        private ParameterSetting _parameterSetting = new ParameterSetting();

        public override ConfigBase CaseLimitSetting
        {
            get
            {
                return (ConfigBase)(object)_limitSetting;
            }
            set
            {
                if (value is LimitSetting)
                {
                    _limitSetting = value as LimitSetting;
                }
            }
        }

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
        public string DisplayName
        {
            get
            {
                return _parameterSetting.DisplayName;
            }
            set
            {
                _parameterSetting.DisplayName = value;
            }
        }

        public CaseFolder(): this("Case Folder")
        {
        }

        public CaseFolder(string displayName)
        {
            DisplayName = displayName;
        }

        public override ExecOkError PreExec()
        {
            return (ExecOkError)0;
        }

        public override ExecOkError Exec()
        {
            return (ExecOkError)0;
        }

        public override ExecOkError PostExec()
        {
            return (ExecOkError)0;
        }
    }
}
