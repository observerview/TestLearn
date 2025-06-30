using System;
using System.Runtime.InteropServices;

namespace TestManager.Utility.ExtendedControl
{
	public class Win32Api
	{
		public class User32
		{
			[DllImport("User32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);
        }
	}
}
