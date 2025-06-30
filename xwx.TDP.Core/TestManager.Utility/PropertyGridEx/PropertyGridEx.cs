using System;
using System.Reflection;
using System.Windows.Forms;

namespace TestManager.Utility.PropertyGridEx
{
	public class PropertyGridEx : PropertyGrid
	{
		private object _selectedObject;

		private double _firstColumnWidthPercent = 0.6;

		public new object SelectedObject
		{
			get
			{
				return _selectedObject;
			}
			set
			{
				_selectedObject = value;
				base.SelectedObject = new TypeDescriptorEx(value);
			}
		}

		public double FirstColumnWidthPercent
		{
			set
			{
				value = ((value > 0.8) ? 0.8 : value);
				value = ((value < 0.2) ? 0.2 : value);
				_firstColumnWidthPercent = value;
			}
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			Control control = base.Controls[2];
			Type type = control.GetType();
			type.InvokeMember("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, control, new object[1] { (int)((double)base.Width * _firstColumnWidthPercent) });
			base.OnLayout(e);
		}
	}
}
