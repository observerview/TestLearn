using System;

namespace TestManager.Extern.Interface
{
	[Flags]
	public enum LogTipType
	{
		Normal = 1,
		Notify = 2,
		Warning = 4
	}
}
