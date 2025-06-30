using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern;
using TestManager.Extern.Interface;
using xwx.TDP.Editor.Properties;
using xwx.TDP.Library.BaseCase;

namespace xwx.TDP.Editor.Engine
{
    internal class TdpEngine : IDisposable
    {
        public static readonly bool __DEMOSTRATION__ = false;
        private TdpTestSequence _testSequence;
        private TdpLogViews _logViews = new TdpLogViews();
        private RunTimeVarPool _engineRunTimeVarPool = new RunTimeVarPool();
        private RunTimeVarPool _userRunTimeVarPool = new RunTimeVarPool();
        private static TdpEngine _instance = null;
        private static readonly object padlock = new object();
        private Form _onwerForm;
        private TdpTestCase _currentExecutedTdpTestCase;
        private Thread _engineThread;
        public TdpTestSequence TestSequence 
        {
            get 
            {
                return _testSequence;
            } 
            set
            {
                _testSequence = value;
            }
        }
        public TdpLogViews LogViews
        {
            get
            {
                return _logViews;
            }
        }
        public RunTimeVarPool UserRunTimeVarPool
        {
            get
            {
                return _userRunTimeVarPool;
            }
        }
        public RunTimeVarPool EngineRunTimeVarPool
        {
            get
            {
                return _engineRunTimeVarPool;
            }
        }
        public static TdpEngine Instance
        {
            get
            {
                TdpEngine instance;
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new TdpEngine();
                    }
                    instance = _instance;
                }
                return instance;
            }
        }
        
        private TdpEngine()
        {
        }
        public void Initialize(Form ownerForm, IPlanExecutor planExecutorInstance)
        {
            this._onwerForm = ownerForm;
            this._logViews.PlanExecutorInstance = planExecutorInstance;
            this._logViews.EngineRunTimeVarPool = this._engineRunTimeVarPool;
            this._logViews.UserRunTimeVarPool = this._userRunTimeVarPool;
            this._logViews.LogResultLogged += this._logViews_LogResultLogged;
        }
        private void _logViews_LogResultLogged(object sender, TdpLogViews.LogResultLoggedEventArgs e)
        {
            switch (e.Type)
            {
                case LogResultType.Normal:
                    if (!e.IsResultPass)
                    {
                        this._currentExecutedTdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError = PassFailSkipError.Failed;
                        this._engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Failed;
                        return;
                    }
                    break;
                case LogResultType.TempResult:
                    switch (e.LogEvent)
                    {
                        case TdpLogViews.LogResultLoggedEventArgs.Event.None:
                            break;
                        case TdpLogViews.LogResultLoggedEventArgs.Event.ClearCurrentResultStatus:
                            this._currentExecutedTdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError = PassFailSkipError.Passed;
                            this._engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Passed;
                            break;
                        default:
                            return;
                    }
                    break;
                default:
                    return;
            }
        }

        public void Start()
        {
            _engineThread = null;
            ThreadStart start = new ThreadStart(ExecutionThread);
            
            _engineThread = new Thread(start);
            _engineThread.IsBackground = true;
            _engineThread.Start();
            //EventWaitHandle.Set();
        }
        public static EventWaitHandle EventWaitHandle = new AutoResetEvent(false);
        public static void SuspendThread()
        {
            EventWaitHandle.Reset();
        }
        public static void ResumeThread()
        {
            EventWaitHandle.Set();
        }
        public void Stop()
        {
            if (this._engineThread != null)
            {
                try
                {
                    _engineThread.Resume();//该方法已经过时

                }
                catch (Exception ex)
                {
                    this._logViews.LogEngineTrace(LogEngineTraceType.Debug, ex.Message);
                }
                try
                {
                    this._engineThread.Abort();
                    this._engineRunTimeVarPool["Engine.Status"] = "Aborted By Manual";
                    this._engineRunTimeVarPool["Plan.StopTime"] = DateTime.Now;
                    this._logViews.LogEngineStatus(LogEngineStatusType.AbortedByManual);
                    this._logViews.LogMisc("EngineStopped.ReportFileName", this._engineRunTimeVarPool["Engine.ReportFileFullName"]);
                }
                catch
                {
                }
            }
        }
        public void Pause()
        {
            if (this._engineThread != null && this._engineThread.IsAlive)
            {
                try
                {
                    _engineThread.Suspend();//该方法已经过时弃用,可能会导致线程死锁或其他不可预见的线程同步问题

                    this._engineRunTimeVarPool["Engine.Status"] = "Paused";
                    this._logViews.LogEngineStatus(LogEngineStatusType.Paused);
                }
                catch
                {
                }
            }
        }
        public void Resume()
        {
            if (this._engineThread != null && this._engineThread.IsAlive)
            {
                try
                {
                    this._engineThread.Resume();

                    this._engineRunTimeVarPool["Engine.Status"] = "Runing";
                    this._logViews.LogEngineStatus(LogEngineStatusType.Running);
                }
                catch
                {
                }
            }
        }

        private void ExecutionThread()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Default.TDP_UI_Language);
            if (_testSequence == null)
            {
                return;
            }
            uint num = 0;
            while (num < _testSequence.SequenceInfo.ExecutTimes)
            {
                _currentExecutedTdpTestCase = null;
                _engineRunTimeVarPool.Clear();
                _userRunTimeVarPool.Clear();
                _engineRunTimeVarPool["Internal.Current.TestSequence"] = _testSequence;
                _engineRunTimeVarPool["Internal.Current.TdpTestCase"] = _currentExecutedTdpTestCase;
                _engineRunTimeVarPool["Plan.Name"] = _testSequence.SequenceInfo.DisplayName;
                _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Passed;
                _engineRunTimeVarPool["Application.Name"] = string.Empty;
                _engineRunTimeVarPool["Application.Path"] = string.Empty;
                _engineRunTimeVarPool["Application.RootDirectory"] = string.Empty;
                _engineRunTimeVarPool["Application.Version"] = string.Empty;
                string text = _testSequence.SequenceInfo.DisplayName;
                char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
                foreach (char oldChar in invalidFileNameChars)
                {
                    text = text.Replace(oldChar, '_');
                }
                DateTime now = DateTime.Now;
                string text2 = string.Format("{0}TdpReports\\[{1}] [{2}]", Path.GetTempPath(), text, now.ToString("yyyyMMdd-HHmmss-ffff"));
                if (!Directory.Exists(text2))
                {
                    Directory.CreateDirectory(text2);
                }
                _engineRunTimeVarPool["Plan.TestResultDir"] = text2;
                _logViews.LogDirectory = text2;
                _engineRunTimeVarPool["Engine.Status"] = "Running";
                _engineRunTimeVarPool["Plan.StartTime"] = now;
                if (!_testSequence.Reset(false))
                {
                    _logViews.LogEngineTrace(LogEngineTraceType.Fatal, "Recreate the test sequence instance failed!");
                    return;
                }
                _engineRunTimeVarPool["Internal.Common.CoreCaseCollection"] = _testSequence.GetCoreCaseCollection();
                _logViews.LogReset();
                _logViews.LogEngineStatus(LogEngineStatusType.Started);
                bool flag = false;
                foreach (TdpTestCase tdpTestCase in _testSequence.TdpTestCases)
                {
                    if (!ExecutionTdpTestCase(tdpTestCase))
                    {
                        flag = true;
                        break;
                    }
                }
                switch ((PassFailSkipError)_engineRunTimeVarPool["Plan.FinalPassFail"])
                {
                    case PassFailSkipError.Failed:
                        _testSequence.ExecutionStatus.FailedTimes += 1U;
                        _testSequence.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Failed);
                        break;
                    case PassFailSkipError.Passed:
                        _testSequence.ExecutionStatus.PassedTimes += 1U;
                        _testSequence.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Passed);
                        break;
                    case PassFailSkipError.Error:
                        _testSequence.ExecutionStatus.ErrorTimes += 1U;
                        _testSequence.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Error);
                        break;
                }
                _engineRunTimeVarPool["Plan.StopTime"] = DateTime.Now;
                if (!flag)
                {
                    _engineRunTimeVarPool["Application.Name"] = string.Empty;
                    _engineRunTimeVarPool["Application.Path"] = string.Empty;
                    _engineRunTimeVarPool["Application.RootDirectory"] = string.Empty;
                    _engineRunTimeVarPool["Application.Version"] = string.Empty;
                    _engineRunTimeVarPool["Current.CaseName"] = null;
                    _engineRunTimeVarPool["Current.CasePassFailSkipError"] = null;
                    _engineRunTimeVarPool["Engine.Status"] = "Finished";
                    _logViews.LogEngineStatus(LogEngineStatusType.Finished);
                }
                _logViews.LogMisc("EngineStopped.ReportFileName", _engineRunTimeVarPool["Engine.ReportFileFullName"]);
                num++;
            }
        }

        private bool ExecutionTdpTestCase(TdpTestCase tdpTestCase)
        {
            
            if (tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type == EngineType.Skip)
            {
                tdpTestCase.SelfTestCase.Reset(false);
                _engineRunTimeVarPool["Current.CaseName"] = tdpTestCase.SelfTestCase.DisplayName;
                _engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Passed;
                Assembly assembly = tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly;
                _engineRunTimeVarPool["Application.Name"] = assembly.GetName().Name;
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] is AssemblyTitleAttribute)
                {
                    this._engineRunTimeVarPool["Application.Name"] = (customAttributes[0] as AssemblyTitleAttribute).Title;
                }
                _engineRunTimeVarPool["Application.Path"] = assembly.Location;
                _engineRunTimeVarPool["Application.RootDirectory"] = new FileInfo(assembly.Location).DirectoryName;
                _engineRunTimeVarPool["Application.Version"] = assembly.GetName().Version;
                tdpTestCase.SelfTestCase.CaseInstance.OnwerForm = this._onwerForm;
                tdpTestCase.SelfTestCase.CaseInstance.EngineRunTimeVarPool = this._engineRunTimeVarPool;
                tdpTestCase.SelfTestCase.CaseInstance.UserRunTimeVarPool = this._userRunTimeVarPool;
                tdpTestCase.SelfTestCase.LogView = this._logViews;
                _currentExecutedTdpTestCase = tdpTestCase;
                _engineRunTimeVarPool["Internal.Current.TdpTestCase"] = this._currentExecutedTdpTestCase;
                _engineRunTimeVarPool["Internal.Current.TestCase.StartTime"] = DateTime.Now;
                LogViews.LogEngineStatus(LogEngineStatusType.CaseBegin);
                tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError = PassFailSkipError.Skipped;
                tdpTestCase.SelfTestCase.ExecutionStatus.SkippedTimes += 1U;
                tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Skipped);
                _engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Skipped;
                _logViews.LogEngineTrace(LogEngineTraceType.Warning, string.Format("Case skipped: {0}", tdpTestCase.SelfTestCase.DisplayName));
                _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                return true;
            }
            if (tdpTestCase.SelfTestCase.CaseInstance is CaseFolder)
            {
                tdpTestCase.SelfTestCase.Reset(false);
                this._engineRunTimeVarPool["Current.CaseName"] = tdpTestCase.SelfTestCase.DisplayName;
                this._engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Passed;
                Assembly assembly2 = tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly;
                this._engineRunTimeVarPool["Application.Name"] = assembly2.GetName().Name;
                object[] customAttributes2 = assembly2.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes2 != null && customAttributes2.Length > 0 && customAttributes2[0] is AssemblyTitleAttribute)
                {
                    this._engineRunTimeVarPool["Application.Name"] = (customAttributes2[0] as AssemblyTitleAttribute).Title;
                }
                _engineRunTimeVarPool["Application.Path"] = assembly2.Location;
                _engineRunTimeVarPool["Application.RootDirectory"] = new FileInfo(assembly2.Location).DirectoryName;
                _engineRunTimeVarPool["Application.Version"] = assembly2.GetName().Version;
                tdpTestCase.SelfTestCase.CaseInstance.OnwerForm = this._onwerForm;
                tdpTestCase.SelfTestCase.CaseInstance.EngineRunTimeVarPool = this._engineRunTimeVarPool;
                tdpTestCase.SelfTestCase.CaseInstance.UserRunTimeVarPool = this._userRunTimeVarPool;
                tdpTestCase.SelfTestCase.LogView = this._logViews;
                _currentExecutedTdpTestCase = tdpTestCase;
                _engineRunTimeVarPool["Internal.Current.TdpTestCase"] = this._currentExecutedTdpTestCase;
                byte[] array = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes((tdpTestCase.SelfTestCase.CaseInstance as CaseFolder).DisplayName));
                byte[] array2 = new byte[]
                {
                    29,
                    79,
                    96,
                    17,
                    89,
                    157,
                    18,
                    68,
                    245,
                    139,
                    127,
                    205,
                    219,
                    69,
                    170,
                    124
                };
                byte[] bytes = new byte[]
                {
                    77,
                    121,
                    32,
                    109,
                    111,
                    116,
                    104,
                    101,
                    114,
                    44,
                    102,
                    97,
                    116,
                    104,
                    101,
                    114,
                    32,
                    97,
                    110,
                    100,
                    32,
                    115,
                    105,
                    115,
                    116,
                    101,
                    114,
                    44,
                    32,
                    73,
                    32,
                    108,
                    111,
                    118,
                    101,
                    32,
                    121,
                    111,
                    117,
                    33
                };
                byte[] bytes2 = new byte[]
                {
                    108,
                    105,
                    108,
                    101,
                    105,
                    49
                };
                if (array.Length == array2.Length)
                {
                    bool flag = true;
                    for (int i = 0; i < array.Length; i++)
                    {
                        flag &= (array[i] == array2[i]);
                    }
                    if (flag)
                    {
                        this._logViews.LogInfo(LogInfoType.Notify, Encoding.ASCII.GetString(bytes), Encoding.ASCII.GetString(bytes2), string.Empty);
                    }
                }
                tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError = PassFailSkipError.Passed;
                tdpTestCase.SelfTestCase.ExecutionStatus.PassedTimes += 1U;
                tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Passed);
                this._engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Passed;
                this._logViews.LogEngineTrace(LogEngineTraceType.Notify, string.Format("{0}", (tdpTestCase.SelfTestCase.CaseInstance as CaseFolder).DisplayName));
            }
            else
            {
                
                EngineMode engineMode;
                //for (; ; )
                while(true)
                {
                    // EventWaitHandle.WaitOne();

                    #region 初始化设置runtime pool, 并且打印一些测试case通用信息
                    tdpTestCase.SelfTestCase.Reset(false);
                    _engineRunTimeVarPool["Current.CaseName"] = tdpTestCase.SelfTestCase.DisplayName;
                    _engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Passed;
                    Assembly assembly3 = tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly;
                    _engineRunTimeVarPool["Application.Name"] = assembly3.GetName().Name;
                    object[] customAttributes3 = assembly3.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (customAttributes3 != null && customAttributes3.Length > 0 && customAttributes3[0] is AssemblyTitleAttribute)
                    {
                        _engineRunTimeVarPool["Application.Name"] = (customAttributes3[0] as AssemblyTitleAttribute).Title;
                    }
                    _engineRunTimeVarPool["Application.Path"] = assembly3.Location;
                    _engineRunTimeVarPool["Application.RootDirectory"] = new FileInfo(assembly3.Location).DirectoryName;
                    _engineRunTimeVarPool["Application.Version"] = assembly3.GetName().Version;
                    tdpTestCase.SelfTestCase.CaseInstance.OnwerForm = _onwerForm;
                    tdpTestCase.SelfTestCase.CaseInstance.EngineRunTimeVarPool = _engineRunTimeVarPool;
                    tdpTestCase.SelfTestCase.CaseInstance.UserRunTimeVarPool = _userRunTimeVarPool;
                    tdpTestCase.SelfTestCase.LogView = _logViews;
                    _currentExecutedTdpTestCase = tdpTestCase;
                    _engineRunTimeVarPool["Internal.Current.TdpTestCase"] = _currentExecutedTdpTestCase;
                    _engineRunTimeVarPool["Internal.Current.TestCase.StartTime"] = DateTime.Now;
                    _logViews.LogEngineStatus(LogEngineStatusType.CaseBegin);
                    _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case Start: {0}", tdpTestCase.SelfTestCase.DisplayName));
                    string arg = Path.GetFileNameWithoutExtension(tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly.Location);
                    object[] customAttributes4 = tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (customAttributes4 != null && customAttributes4.Length > 0 && customAttributes4[0] != null && customAttributes4[0] is AssemblyTitleAttribute)
                    {
                        arg = ((AssemblyTitleAttribute)customAttributes4[0]).Title;
                    }
                    _logViews.LogInfo(LogInfoType.Normal, "<-Parameter->TDPL Version", 
                        string.Format("{0} ({1})", arg, 
                        tdpTestCase.SelfTestCase.CaseInfo.AssemblyVersion.Equals(
                            tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly.GetName().Version) ? 
                            string.Format("{0}", tdpTestCase.SelfTestCase.CaseInfo.AssemblyVersion) : 
                            string.Format("{0} | {1}", tdpTestCase.SelfTestCase.CaseInfo.AssemblyVersion, 
                            tdpTestCase.SelfTestCase.CaseInstance.GetType().Assembly.GetName().Version)), 
                        string.Empty);
                    _logViews.LogInfo(LogInfoType.Normal, "<-Parameter->Case Name", tdpTestCase.SelfTestCase.CaseName, string.Empty);
                    NameValueCollection nameValueCollection = new NameValueCollection();
                    NameValueCollection nameValueCollection2 = new NameValueCollection();
                    NameValueCollection nameValueCollection3 = new NameValueCollection();
                    tdpTestCase.SelfTestCase.CaseInstance.CaseParameterSetting.GetPropertiesValue(out nameValueCollection, out nameValueCollection2);
                    foreach (string text in nameValueCollection2.AllKeys)
                    {
                        try
                        {
                            string name = text;
                            PropertyInfo property = tdpTestCase.SelfTestCase.CaseInstance.GetType().GetProperty(text);
                            if (property != null)
                            {
                                object[] customAttributes5 = property.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                                if (customAttributes5.Length > 0)
                                {
                                    name = (customAttributes5[0] as DisplayNameAttribute).DisplayName;
                                }
                            }
                            nameValueCollection3.Add(name, nameValueCollection2[text]);
                        }
                        catch
                        {
                            break;
                        }
                    }
                    string[] array3 = new string[nameValueCollection3.Count];
                    nameValueCollection3.AllKeys.CopyTo(array3, 0);
                    Array.Sort<string>(array3);
                    foreach (string text2 in array3)
                    {
                        _logViews.LogInfo(LogInfoType.Normal, "<-Parameter->" + text2, nameValueCollection2[text2], string.Empty);
                    }
                    #endregion

                    #region test case 内的skip方法预留。。正常流程不会用到。
                    MethodInfo method = tdpTestCase.SelfTestCase.CaseInstance.GetType().GetMethod("IsSkip", Type.EmptyTypes);
                    if (method != null)
                    {
                        object obj = method.Invoke(tdpTestCase.SelfTestCase.CaseInstance, null);
                        if (obj is bool && (bool)obj)
                        {
                            _logViews.LogInfo(LogInfoType.Warning, "Case skipped by code.", string.Empty, string.Empty);
                            tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError = PassFailSkipError.Skipped;
                            tdpTestCase.SelfTestCase.ExecutionStatus.SkippedTimes += 1U;
                            tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Skipped);
                            _engineRunTimeVarPool["Current.CasePassFailSkipError"] = PassFailSkipError.Skipped;
                            _logViews.LogEngineTrace(LogEngineTraceType.Warning, string.Format("Case skipped: {0}", tdpTestCase.SelfTestCase.DisplayName));
                            _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                            LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                            break;
                        }
                    }
                    #endregion

                    //执行
                    CoreCase.ExecOkError execOkError = tdpTestCase.SelfTestCase.ExecuteCase();

                    _engineRunTimeVarPool["Current.CasePassFailSkipError"] = tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError;
                    switch (tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError)
                    {
                        case PassFailSkipError.Failed:
                            tdpTestCase.SelfTestCase.ExecutionStatus.FailedTimes += 1U;
                            tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Failed);
                            break;
                        case PassFailSkipError.Passed:
                            tdpTestCase.SelfTestCase.ExecutionStatus.PassedTimes += 1U;
                            tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Passed);
                            break;
                        case PassFailSkipError.Error:
                            tdpTestCase.SelfTestCase.ExecutionStatus.ErrorTimes += 1U;
                            tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Add(PassFailSkipError.Error);
                            break;
                    }
                    //如果设置的Enginmode是globe，则选用globe配置，否则选用case内的配置
                    engineMode = ((tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type == EngineType.Globe) ? _testSequence.SequenceInfo.GlobeEngineMode : tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode);
                    //如果执行状态是error，就需要根据配置情况处理
                    if (execOkError == CoreCase.ExecOkError.Error)
                    {
                        _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Error;
                        //设置EngineMode_Error有4种情况：Abort，Ignore,RetryThenAbort,RetryThenContinue
                        // 先处理Abort，Ignore的情况，这两种情况没有参数
                        if (engineMode.Error != EngineMode_Error.RetryThenAbort && engineMode.Error != EngineMode_Error.RetryThenContinue)
                        {
                            //处理ignore，当前case结束，继续测试后续case
                            if (engineMode.Error == EngineMode_Error.Ignore)
                            {
                                _logViews.LogEngineTrace(LogEngineTraceType.Error, string.Format("Case error, ignore: {0}.", tdpTestCase.SelfTestCase.DisplayName));
                                _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                                LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                                break;
                            }
                            //处理Abort，直接结束
                            _engineRunTimeVarPool["Engine.Status"] = "Aborted By Engine";
                            _engineRunTimeVarPool["Plan.StopTime"] = DateTime.Now;
                            _logViews.LogEngineTrace(LogEngineTraceType.Error, string.Format("Case error, abort: {0}.", tdpTestCase.SelfTestCase.DisplayName));
                            _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                            LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                            _logViews.LogEngineStatus(LogEngineStatusType.AbortedByEngine);
                            return false;
                        }
                        //处理RetryThenAbort,RetryThenContinue情况
                        //如果当前的error次数，大于配置的次数，则返回false跳出循环
                        if (tdpTestCase.SelfTestCase.ExecutionStatus.ErrorTimes >= engineMode.ErrorRetry)
                        {
                            //对于RetryThenAbort，达到次数后，abort
                            if (engineMode.Error == EngineMode_Error.RetryThenAbort)
                            {
                                _engineRunTimeVarPool["Engine.Status"] = "Aborted By Engine";
                                _engineRunTimeVarPool["Plan.StopTime"] = DateTime.Now;
                                _logViews.LogEngineTrace(LogEngineTraceType.Error, string.Format("Case error #{1} times of {2}, abort: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.ErrorTimes, engineMode.ErrorRetry));
                                _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                                LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                                _logViews.LogEngineStatus(LogEngineStatusType.AbortedByEngine);
                                return false;
                            }
                            //对于RetryThenContinue，达到次数后继续后续测试
                        }
                        _logViews.LogEngineTrace(LogEngineTraceType.Error, string.Format("Case error #{1} of {2}, retry: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.ErrorTimes, engineMode.ErrorRetry));
                        _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                        LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                    }
                    else if (engineMode.OK == EngineMode_OK.Repeat)
                    {
                        if ((ulong)tdpTestCase.SelfTestCase.ExecutionStatus.TotalExecutedTimes >= (ulong)((long)engineMode.OkRetry))
                        {
                            if (tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Contains(PassFailSkipError.Failed))
                            {
                                _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Failed;
                            }
                        }
                        _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case executed #{1} of {2}, repeat: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.TotalExecutedTimes, engineMode.OkRetry));
                        _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                        LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                    }
                    else
                    {
                        if (tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError == PassFailSkipError.Failed && engineMode.OK == EngineMode_OK.AbortOnFail)
                        {
                            _engineRunTimeVarPool["Engine.Status"] = "Aborted By Engine";
                            _engineRunTimeVarPool["Plan.StopTime"] = DateTime.Now;
                            _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Failed;
                            _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case failed, abort: {0}.", tdpTestCase.SelfTestCase.DisplayName));
                            _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                            LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                            _logViews.LogEngineStatus(LogEngineStatusType.AbortedByEngine);
                            return false;
                        }
                        if (tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError == PassFailSkipError.Failed && engineMode.OK == EngineMode_OK.RetestOnFail)
                        {
                            if ((ulong)tdpTestCase.SelfTestCase.ExecutionStatus.FailedTimes >= (ulong)((long)engineMode.OkRetry))
                            {
                                if (tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Contains(PassFailSkipError.Failed))
                                {
                                    _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Failed;
                                }
                            }
                            _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case failed #{1} of {2}, retry: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.FailedTimes, engineMode.OkRetry));
                            _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                            LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                        }
                        else if (engineMode.OK == EngineMode_OK.RepeatUntilFail)
                        {
                            if ((ulong)tdpTestCase.SelfTestCase.ExecutionStatus.FailedTimes >= (ulong)((long)engineMode.OkRetry))
                            {
                                _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Failed;
                            }
                            _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case failed #{1} of {2}, retest: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.FailedTimes, engineMode.OkRetry));
                            _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                            LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                        }
                        else
                        {
                            if (engineMode.OK != EngineMode_OK.RepeatUntilPass)
                            {
                                if (tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Contains(PassFailSkipError.Failed))
                                {
                                    _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Failed;
                                }
                                _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case {1}: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.LastTimeFinalPassFailSkipError.ToString().ToLower()));
                                _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                                LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                            }
                            if (tdpTestCase.SelfTestCase.ExecutionStatus.PassedTimes >= engineMode.OkRetry)
                            {
                                if (tdpTestCase.SelfTestCase.ExecutionStatus.PassFailSkipErrorHistory.Contains(PassFailSkipError.Failed))
                                {
                                    _engineRunTimeVarPool["Plan.FinalPassFail"] = PassFailSkipError.Failed;
                                }
                            }
                            _logViews.LogEngineTrace(LogEngineTraceType.Normal, string.Format("Case passed #{1} of {2}, retest: {0}.", tdpTestCase.SelfTestCase.DisplayName, tdpTestCase.SelfTestCase.ExecutionStatus.PassedTimes, engineMode.OkRetry));
                            _engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] = DateTime.Now;
                            LogViews.LogEngineStatus(LogEngineStatusType.CaseEnd);
                            break;
                        }
                    }
                }
            }
            foreach (TdpTestCase tsdpTestCase2 in tdpTestCase.ContainedTdpTestCase)
            {
                if (!this.ExecutionTdpTestCase(tsdpTestCase2))
                {
                    return false;
                }
            }
            return true;
        }
        public void Dispose()
        {
            this._logViews.CloseView();
        }

        public class EngineRunTimeVar_Internal
        {
            // Token: 0x040001C6 RID: 454
            public const string CurrentSequence = "Internal.Current.TestSequence";

            // Token: 0x040001C7 RID: 455
            public const string CurrentTsdpTestCase = "Internal.Current.TdpTestCase";

            // Token: 0x040001C8 RID: 456
            public const string CurrentCaseStartTime = "Internal.Current.TestCase.StartTime";

            // Token: 0x040001C9 RID: 457
            public const string CurrentCaseStopTime = "Internal.Current.TestCase.StopTime";

            // Token: 0x040001CA RID: 458
            public const string DutSerialNumber = "DUT.SerialsNumber";

            // Token: 0x040001CB RID: 459
            public const string CommonCoreCaseCollection = "Internal.Common.CoreCaseCollection";
        }
    }
}
