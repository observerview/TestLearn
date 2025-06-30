using System;

namespace TestManager.Extern.Interface
{
	[Flags]
	public enum LogMessageType
	{
		Normal = 1,
		Notify = 2,
		Debug = 4,
		Warning = 8,
		Error = 0x10
	}
}
