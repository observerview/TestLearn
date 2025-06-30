using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using WeifenLuo.WinFormsUI.Docking;
using xwx.TDP.Editor.Properties;

namespace xwx.TDP.Editor.Engine
{
    internal class TdpLogViews : ILogView, ILogViewEx
    {
        public delegate void LogResultLoggedHandler(object sender, TdpLogViews.LogResultLoggedEventArgs e);
        public event LogResultLoggedHandler LogResultLogged;
        private List<ILogViewEx> _logViews = new List<ILogViewEx>();
        private IRunTimeVarPoolReadOnly _engineRunTimeVarPool;
        private IRunTimeVarPoolReadOnly _userRunTimeVarPool;
        private IPlanExecutor _planExecutorInstance;
        private object _mdiDockPanel;
        private Font _windowsFont;
        public List<ILogViewEx> LogViews
        {
            get
            {
                return _logViews;
            }
        }
        public IRunTimeVarPoolReadOnly EngineRunTimeVarPool
        {
            set
            {
                _engineRunTimeVarPool = value;
            }
        }
        public IRunTimeVarPoolReadOnly UserRunTimeVarPool
        {
            set
            {
                _userRunTimeVarPool = value;
            }
        }
        public IPlanExecutor PlanExecutorInstance
        {
            set
            {
                _planExecutorInstance = value;
            }
        }
        public bool IsDisposed
        {
            get
            {
                foreach (ILogViewEx logViewEx in _logViews)
                {
                    if (!logViewEx.IsDisposed)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool Disposing
        {
            get
            {
                foreach (ILogViewEx logViewEx in _logViews)
                {
                    if (logViewEx.Disposing)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public string LogDirectory
        {
            set
            {
                foreach (ILogViewEx logViewEx in _logViews)
                {
                    logViewEx.LogDirectory = value;
                }
            }
        }

        public void AddView(ILogViewEx logView)
        {
            if (!_logViews.Contains(logView) && !(logView is TdpLogViews))
            {
                logView.WindowsFont = _windowsFont;
                logView.MdiDockPanel = _mdiDockPanel;
                if (logView is DockContent)
                {
                    (logView as DockContent).HideOnClose = true;
                }
                logView.EngineRunTimeVarPool = _engineRunTimeVarPool;
                logView.UserRunTimeVarPool = _userRunTimeVarPool;
                logView.PlanExecutorInstance = _planExecutorInstance;
                _logViews.Add(logView);
            }
        }
        public void ShowView()
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing)
                {
                    logViewEx.ShowView();
                }
            }
        }
        public void HideView()
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing)
                {
                    logViewEx.HideView();
                }
            }
        }
        public void CloseView()
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing)
                {
                    logViewEx.CloseView();
                }
            }
        }
        public void LogMisc(string miscValue, object miscParameter)
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing)
                {
                    try
                    {
                        MethodInfo method = logViewEx.GetType().GetMethod("LogMisc");
                        method?.Invoke(logViewEx, new object[]{
                                miscValue,
                                miscParameter
                        });
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void LogReset()
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing)
                {
                    logViewEx.LogReset();
                }
            }
        }
        public void LogEngineStatus(LogEngineStatusType logEngineStatusType)
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    logViewEx.LogEngineStatus(logEngineStatusType);
                }
            }
        }
        public void LogEngineTrace(LogEngineTraceType logEngineTraceType, string message)
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    logViewEx.LogEngineTrace(logEngineTraceType, message);
                }
            }
        }
        public void LogResult(LogResultType logResultType, string title, object result, string resultString, string unit, object limit)
        {
            if (logResultType == LogResultType.TempResult && !string.IsNullOrEmpty(title) && title.StartsWith("<!-") && title.EndsWith("-!>"))
            {
                string value = title.Substring(3, title.Length - 6);
                if (Enum.IsDefined(typeof(LogResultLoggedEventArgs.Event), value))
                {
                    LogResultLoggedEventArgs.Event logEvent = (LogResultLoggedEventArgs.Event)Enum.Parse(typeof(LogResultLoggedEventArgs.Event), value);
                    this.LogResultLogged(this, new LogResultLoggedEventArgs(logEvent));
                    return;
                }
            }
            if (logResultType == LogResultType.Normal && this.LogResultLogged != null && result != null && limit != null)
            {
                bool isResultPass = false;
                if (result != null && limit != null && (limit is ValueLimit || limit is ValueLimitCollection))
                {
                    object obj = null;
                    if (limit is ValueLimit)
                    {
                        switch ((limit as ValueLimit).ValidateValue(result, out obj))
                        {
                            case ValueLimit.ValueValidResult.InLimit:
                                isResultPass = true;
                                break;
                        }
                    }
                    if (limit is ValueLimitCollection)
                    {
                        switch ((limit as ValueLimitCollection).ValidateValue(result, out obj))
                        {
                            case ValueLimitCollection.ValueValidResult.InLimit:
                                isResultPass = true;
                                break;
                        }
                    }
                }
                LogResultLogged(this, new LogResultLoggedEventArgs(isResultPass));
            }
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    logViewEx.LogResult(logResultType, title, result, resultString, unit, limit);
                }
            }
        }
        public void LogInfo(LogInfoType logInfoType, string title, string value, string unit)
        {
            if (logInfoType == LogInfoType.Error)
            {
                LogResultLogged(this, new LogResultLoggedEventArgs(false));
            }
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    logViewEx.LogInfo(logInfoType, title, value, unit);
                }
            }
        }
        public void LogTip(LogTipType logTipType, string text, float fontSize, bool isAlignCenter)
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    logViewEx.LogTip(logTipType, text, fontSize, isAlignCenter);
                }
            }
        }
        public void LogMessage(LogMessageType logMessageType, string text)
        {
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    logViewEx.LogMessage(logMessageType, text);
                }
            }
        }
        public void LogUserDefinedData(ILogData data, out List<object> retValues)
        {
            retValues = new List<object>();
            foreach (ILogViewEx logViewEx in _logViews)
            {
                if (logViewEx != null && !logViewEx.IsDisposed && !logViewEx.Disposing && !Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(logViewEx.GetType().FullName))
                {
                    List<object> collection = new List<object>();
                    logViewEx.LogUserDefinedData(data, out collection);
                    retValues.AddRange(collection);
                }
            }
        }
        public string LogViewName
        {
            get
            {
                return "Log Views";
            }
        }

        public object MdiDockPanel
        {
            set
            {
                this._mdiDockPanel = value;
            }
        }
        public Keys OpenViewShortcutKeys
        {
            get
            {
                return Keys.None;
            }
        }
        public Font WindowsFont
        {
            set
            {
                this._windowsFont = value;
            }
        }
        public class LogResultLoggedEventArgs : EventArgs
        {
            public LogResultType Type
            {
                get
                {
                    return this._type;
                }
                set
                {
                    this._type = value;
                }
            }
            public Event LogEvent
            {
                get
                {
                    return _logEvent;
                }
                set
                {
                    _logEvent = value;
                }
            }
            public bool IsResultPass
            {
                get
                {
                    return _isResultPass;
                }
                set
                {
                    _isResultPass = value;
                }
            }
            public LogResultLoggedEventArgs(bool isResultPass)
            {
                _type = LogResultType.Normal;
                _isResultPass = isResultPass;
            }
            public LogResultLoggedEventArgs(Event logEvent)
            {
                _type = LogResultType.TempResult;
                _logEvent = logEvent;
            }

            private LogResultType _type = LogResultType.Normal;
            private Event _logEvent;
            private bool _isResultPass;
            public enum Event
            {
                None,
                ClearCurrentResultStatus
            }
        }
    }
}
