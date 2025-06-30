using System;

namespace TestManager.Extern.Interface
{
	[Flags]
	public enum LogInfoType
	{
		Normal = 1,
		Warning = 2,
		Error = 4,
		Notify = 8,
		Log = 0x10
	}
}
