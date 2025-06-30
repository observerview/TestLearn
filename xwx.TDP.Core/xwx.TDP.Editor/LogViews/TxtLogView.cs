using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern.Interface;
using TestManager.Utility;
using TestManager.Utility.Misc;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Editor.Properties;

namespace xwx.TDP.Editor.LogViews
{
    internal class TxtLogView : LogViewWindow, IInternalLogView, ILogView, ILogViewEx, IPlanExecutor
    {
        private IRunTimeVarPoolReadOnly _engineRunTimeVarPool;
        private IRunTimeVarPoolReadOnly _userRunTimeVarPool;
        private IPlanExecutor _planExecutorInstance;
        private static object objLogFileLock = new object();
        private static Encoding TxtLogFileEncoding = Encoding.UTF8;
        private string _logFileName = string.Empty;
        private string _logDirectory = string.Empty;

        private string GetLogString(NameValueCollection nv)
        {
            string LogString = string.Empty;
            foreach(string key in nv.Keys)
            {
                if (key == "LogType") continue;
                LogString += "\t\t" + nv[key];   
            }

            return LogString.Trim();
        }
        private void AppendLogItem(NameValueCollection nv)
        {
            lock (objLogFileLock)
            {
                File.AppendAllText(_logFileName, this.GetLogString(nv) + "\r\n", TxtLogFileEncoding);
            }
        }
        public string LogDirectory
        {
            set
            {
                this._logDirectory = value;
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
        public void ShowView()
        {
        }
        public void HideView()
        {
        }
        public void CloseView()
        {
        }
        public void LogReset()
        {
            _logFileName = string.Format("{0}\\{1}.log", _logDirectory, new DirectoryInfo(_logDirectory).Name);
            lock (objLogFileLock)
            {
                File.Create(_logFileName).Close();
                File.AppendAllText(_logFileName, "Start Test--------------------------------------- \n\r", TxtLogFileEncoding);
            }
        }
        public void LogEngineStatus(LogEngineStatusType logEngineStatusType)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            if (logEngineStatusType != LogEngineStatusType.Running)
            {
                if (logEngineStatusType != LogEngineStatusType.Paused)
                {
                    if (logEngineStatusType != LogEngineStatusType.CaseBegin)
                    {
                        if (logEngineStatusType != LogEngineStatusType.CaseEnd)
                        {
                            if (logEngineStatusType != LogEngineStatusType.Started)
                            {
                                if (logEngineStatusType != LogEngineStatusType.Finished)
                                {
                                    if (logEngineStatusType == LogEngineStatusType.AbortedByEngine || logEngineStatusType == LogEngineStatusType.AbortedByManual)
                                    {
                                        nameValueCollection.Add("*", string.Empty);
                                        nameValueCollection.Add("[Time]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
                                        nameValueCollection.Add("[Case]", this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
                                        nameValueCollection.Add("[Title]", string.Format("Sequence aborted by {0}.", (logEngineStatusType == LogEngineStatusType.AbortedByEngine) ? "engine" : "manual", this._engineRunTimeVarPool.ContainKey("Plan.Name") ? (this._engineRunTimeVarPool["Plan.Name"] as string) : string.Empty));
                                        nameValueCollection.Add("[Result]", string.Empty);
                                        nameValueCollection.Add("[Unit]", string.Empty);
                                        nameValueCollection.Add("[Pass/Fail]", "Aborted");
                                        nameValueCollection.Add("[Limit]", string.Empty);
                                        nameValueCollection.Add("[Extra]", string.Empty);
                                    }
                                    nameValueCollection.Add("LogType", string.Format("{0}:{1}", typeof(LogEngineStatusType).Name, logEngineStatusType.ToString()));
                                    this.AppendLogItem(nameValueCollection);
                                    if (logEngineStatusType == LogEngineStatusType.AbortedByEngine || logEngineStatusType == LogEngineStatusType.AbortedByManual || logEngineStatusType == LogEngineStatusType.Finished)
                                    {
                                        lock (objLogFileLock)
                                        {
                                            File.AppendAllText(_logFileName, "\n\rEnd---------------------------------------", TxtLogFileEncoding);
                                        }
                                    }
                                    return;
                                }
                            }
                            nameValueCollection.Add("*", string.Empty);
                            nameValueCollection.Add("[Time]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
                            nameValueCollection.Add("[Case]", this._engineRunTimeVarPool.ContainKey("Plan.Name") ? (this._engineRunTimeVarPool["Plan.Name"] as string) : string.Empty);
                            nameValueCollection.Add("[Title]", string.Format("Sequence {0}.", (logEngineStatusType == LogEngineStatusType.Started) ? "started" : "finished", this._engineRunTimeVarPool.ContainKey("Plan.Name") ? (this._engineRunTimeVarPool["Plan.Name"] as string) : string.Empty));
                            nameValueCollection.Add("[Result]", string.Empty);
                            nameValueCollection.Add("[Unit]", string.Empty);
                            switch (logEngineStatusType)
                            {
                                case LogEngineStatusType.Started:
                                    nameValueCollection.Add("[Pass/Fail]", string.Empty);
                                    break;
                                case LogEngineStatusType.Finished:
                                    nameValueCollection.Add("[Pass/Fail]", this._engineRunTimeVarPool["Plan.FinalPassFail"].ToString());
                                    break;
                            }
                            nameValueCollection.Add("[Limit]", string.Empty);
                            nameValueCollection.Add("[Extra]", string.Empty);
                            nameValueCollection.Add("LogType", string.Format("{0}:{1}", typeof(LogEngineStatusType).Name, logEngineStatusType.ToString()));
                            this.AppendLogItem(nameValueCollection);
                            if (logEngineStatusType == LogEngineStatusType.AbortedByEngine || logEngineStatusType == LogEngineStatusType.AbortedByManual || logEngineStatusType == LogEngineStatusType.Finished)
                            {
                                lock (objLogFileLock)
                                {
                                    File.AppendAllText(_logFileName, "End-----------------", TxtLogFileEncoding);
                                }
                            }
                            return;
                        }
                    }
                    nameValueCollection.Add("*", string.Empty);
                    nameValueCollection.Add("[Time]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
                    nameValueCollection.Add("[Case]", this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
                    nameValueCollection.Add("[Title]", this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
                    nameValueCollection.Add("[Result]", string.Empty);
                    nameValueCollection.Add("[Unit]", string.Empty);
                    string value = string.Empty;
                    if (logEngineStatusType == LogEngineStatusType.CaseEnd && this._engineRunTimeVarPool["Current.CasePassFailSkipError"] != null && this._engineRunTimeVarPool["Current.CasePassFailSkipError"] is PassFailSkipError)
                    {
                        value = ((PassFailSkipError)this._engineRunTimeVarPool["Current.CasePassFailSkipError"]).ToString();
                    }
                    nameValueCollection.Add("[Pass/Fail]", value);
                    nameValueCollection.Add("[Limit]", string.Empty);
                    nameValueCollection.Add("[Extra]", string.Empty);
                    nameValueCollection.Add("LogType", string.Format("{0}:{1}", typeof(LogEngineStatusType).Name, logEngineStatusType.ToString()));
                    this.AppendLogItem(nameValueCollection);
                    if (logEngineStatusType == LogEngineStatusType.AbortedByEngine || logEngineStatusType == LogEngineStatusType.AbortedByManual || logEngineStatusType == LogEngineStatusType.Finished)
                    {
                        lock (objLogFileLock)
                        {
                            File.AppendAllText(_logFileName, "End-----------------", TxtLogFileEncoding);
                        }
                    }
                    return;
                }
            }
            nameValueCollection.Add("*", string.Empty);
            nameValueCollection.Add("[Time]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
            nameValueCollection.Add("[Case]", this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
            nameValueCollection.Add("[Title]", string.Format("Sequence {0} by manual.", (logEngineStatusType == LogEngineStatusType.Running) ? "resumed" : "paused", this._engineRunTimeVarPool.ContainKey("Plan.Name") ? (this._engineRunTimeVarPool["Plan.Name"] as string) : string.Empty));
            nameValueCollection.Add("[Result]", string.Empty);
            nameValueCollection.Add("[Unit]", string.Empty);
            nameValueCollection.Add("[Pass/Fail]", string.Empty);
            nameValueCollection.Add("[Limit]", string.Empty);
            nameValueCollection.Add("[Extra]", string.Empty);
        }

        public void LogEngineTrace(LogEngineTraceType logEngineTraceType, string message)
        {
            this.AppendLogItem(new NameValueCollection
            {
                {
                    "*",
                    string.Empty
                },
                {
                    "[Time]",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                },
                {
                    "[Case]",
                    this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty
                },
                {
                    "[Title]",
                    "*" + message
                },
                {
                    "[Result]",
                    string.Empty
                },
                {
                    "[Unit]",
                    string.Empty
                },
                {
                    "[Pass/Fail]",
                    string.Empty
                },
                {
                    "[Limit]",
                    string.Empty
                },
                {
                    "[Extra]",
                    string.Empty
                },
                {
                    "LogType",
                    string.Format("{0}:{1}", typeof(LogEngineTraceType).Name, logEngineTraceType.ToString())
                }
            });
        }


        public void LogResult(LogResultType logResultType, string title, object result, string resultString, string unit, object limit)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            string text = string.Empty;
            object obj = null;
            if (result != null && limit != null && (limit is ValueLimit || limit is ValueLimitCollection))
            {
                if (limit is ValueLimit)
                {
                    switch ((limit as ValueLimit).ValidateValue(result, out obj))
                    {
                        case ValueLimit.ValueValidResult.InLimit:
                            text = PassFail.Pass.ToString();
                            if (limit is ValueLimitCollection)
                            {
                                switch ((limit as ValueLimitCollection).ValidateValue(result, out obj))
                                {
                                    case ValueLimitCollection.ValueValidResult.InLimit:
                                        text = PassFail.Pass.ToString();
                                        if(logResultType == LogResultType.TempResult)
                                        {
                                            text += "*";
                                        }
                                        break;
                                    default:
                                        text = PassFail.Fail.ToString();
                                        break;
                                }
                            }
                            break;
                        default:
                            text = PassFail.Fail.ToString();
                            break;
                    }
                    
                }
            }
            nameValueCollection.Add("*", string.Empty);
            nameValueCollection.Add("[Time]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
            nameValueCollection.Add("[Case]", this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
            nameValueCollection.Add("[Title]", title);
            nameValueCollection.Add("[Result]", resultString);
            nameValueCollection.Add("[Unit]", unit);
            nameValueCollection.Add("[Pass/Fail]", text);
            nameValueCollection.Add("[Limit]", (limit == null) ? string.Empty : limit.ToString());
            nameValueCollection.Add("[Extra]", string.Empty);
            nameValueCollection.Add("LogType", string.Format("{0}:{1}", typeof(LogResultType).Name, logResultType.ToString()));
            this.AppendLogItem(nameValueCollection);
        }

        public void LogInfo(LogInfoType logInfoType, string title, string value, string unit)
        {
            this.AppendLogItem(new NameValueCollection
            {
                {
                    "*",
                    string.Empty
                },
                {
                    "[Time]",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                },
                {
                    "[Case]",
                    this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty
                },
                {
                    "[Title]",
                    title
                },
                {
                    "[Result]",
                    value
                },
                {
                    "[Unit]",
                    unit
                },
                {
                    "[Pass/Fail]",
                    (logInfoType == LogInfoType.Error || logInfoType == LogInfoType.Warning) ? logInfoType.ToString() : string.Empty
                },
                {
                    "[Limit]",
                    string.Empty
                },
                {
                    "[Extra]",
                    string.Empty
                },
                {
                    "LogType",
                    string.Format("{0}:{1}", typeof(LogInfoType).Name, logInfoType.ToString())
                }
            });
        }

        public void LogTip(LogTipType logTipType, string text, float fontSize, bool isAlignCenter)
        {
            this.AppendLogItem(new NameValueCollection
            {
                {
                    "*",
                    string.Empty
                },
                {
                    "[Time]",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                },
                {
                    "[Case]",
                    this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty
                },
                {
                    "[Title]",
                    text
                },
                {
                    "[Result]",
                    string.Empty
                },
                {
                    "[Unit]",
                    string.Empty
                },
                {
                    "[Pass/Fail]",
                    string.Empty
                },
                {
                    "[Limit]",
                    string.Empty
                },
                {
                    "[Extra]",
                    string.Empty
                },
                {
                    "LogType",
                    string.Format("{0}:{1}", typeof(LogTipType).Name, logTipType.ToString())
                }
            });
        }

        public void LogMessage(LogMessageType logMessageType, string text)
        {
            this.AppendLogItem(new NameValueCollection
            {
                {
                    "*",
                    string.Empty
                },
                {
                    "[Time]",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                },
                {
                    "[Case]",
                    this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty
                },
                {
                    "[Title]",
                    text
                },
                {
                    "[Result]",
                    string.Empty
                },
                {
                    "[Unit]",
                    string.Empty
                },
                {
                    "[Pass/Fail]",
                    string.Empty
                },
                {
                    "[Limit]",
                    string.Empty
                },
                {
                    "[Extra]",
                    string.Empty
                },
                {
                    "LogType",
                    string.Format("{0}:{1}", typeof(LogMessageType).Name, logMessageType.ToString())
                }
            });
        }

        public void LogUserDefinedData(ILogData data, out List<object> retValues)
        {
            retValues = new List<object>();
            if (!data.IsSaveLog)
            {
                return;
            }
            this.AppendLogItem(new NameValueCollection
            {
                {
                    "*",
                    string.Format("[{0}]##{1}", data.GetType().FullName, data.Data)
                },
                {
                    "[Time]",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                },
                {
                    "[Case]",
                    this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty
                },
                {
                    "[Title]",
                    data.Message
                },
                {
                    "[Result]",
                    string.Empty
                },
                {
                    "[Unit]",
                    string.Empty
                },
                {
                    "[Pass/Fail]",
                    string.Empty
                },
                {
                    "[Limit]",
                    string.Empty
                },
                {
                    "[Extra]",
                    string.Empty
                },
                {
                    "LogType",
                    string.Format("{0}:{1}", typeof(ILogData).Name, data.GetType().FullName)
                }
            });
        }
        public bool CanBeDisabled
        {
            get
            {
                return false;
            }
        }

        public bool IsAdvancedView
        {
            get
            {
                return true;
            }
        }

        public object MdiDockPanel 
        {
            set
            {
            }
        }

        public string LogViewName
        {
            get
            {
                return "Txt log";
            }
        }

        public Keys OpenViewShortcutKeys
        {
            get
            {
                return Keys.None;
            }
        }

        public Font WindowsFont {set{ } }
    }
}
