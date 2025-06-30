using System;

namespace TestManager.Extern.Interface
{
	[Flags]
	public enum LogEngineStatusType
	{
		Started = 1,
		Finished = 2,
		AbortedByEngine = 4,
		AbortedByManual = 8,
		Paused = 0x10,
		Running = 0x20,
		CaseBegin = 0x40,
		CaseEnd = 0x80
	}
}
