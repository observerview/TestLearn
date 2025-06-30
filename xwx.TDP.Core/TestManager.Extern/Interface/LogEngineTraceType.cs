using System;

namespace TestManager.Extern.Interface
{
	[Flags]
	public enum LogEngineTraceType
	{
		Normal = 1,
		Notify = 2,
		Warning = 4,
		Error = 8,
		Fatal = 0x10,
		Debug = 0x20
	}
}
