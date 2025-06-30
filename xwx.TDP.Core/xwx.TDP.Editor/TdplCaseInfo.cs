using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor
{
    internal class TdplCaseInfo
    {
        private string _displayName = "<NULL>";

        private string _description = "N/A";

        private string _category = "Misc";

        private Color _displayColor = SystemColors.WindowText;

        private Type _caseType;

        private Assembly _caseAssembly;
        public string DisplayName
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this._displayName = value;
            }
        }
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = value;
            }
        }
        public Color DisplayColor
        {
            get
            {
                return this._displayColor;
            }
            set
            {
                this._displayColor = value;
            }
        }
        public Type CaseType
        {
            get
            {
                return this._caseType;
            }
            set
            {
                this._caseType = value;
            }
        }
        public Assembly CaseAssembly
        {
            get
            {
                return this._caseAssembly;
            }
            set
            {
                this._caseAssembly = value;
            }
        }
    }
}
