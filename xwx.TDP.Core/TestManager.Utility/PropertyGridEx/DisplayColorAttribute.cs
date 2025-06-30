using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestManager.Utility.PropertyGridEx
{
    [AttributeUsage(AttributeTargets.All)]
    public class DisplayColorAttribute : Attribute
    {
        private Color _displayColor = SystemColors.WindowText;

        public Color DisplayColor => _displayColor;

        public DisplayColorAttribute(string displayColor)
        {
            _displayColor = Color.FromName(displayColor);
        }
    }
}
