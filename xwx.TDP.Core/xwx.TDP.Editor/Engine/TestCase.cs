using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern.Interface;
using TestManager.Extern;
using TestManager.Utility.PropertyGridEx;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Collections.Specialized;
using xwx.TDP.Editor.Properties;
using System.IO;
using System.Threading;

namespace xwx.TDP.Editor.Engine
{
    internal class TestCase
    {
        private CoreCase _caseInstance;

        private ExecutionStatus _executionStatus = new ExecutionStatus();

        private TestCaseInfo _caseInfo = new TestCaseInfo();

        private TestCase.TestCaseErrorCode _errorCode = TestCase.TestCaseErrorCode.OK;

        private string _errorMessage;

        private ILogViewEx _logView;

        public enum TestCaseErrorCode
        {
            [EnumDisplayName("Unknown")]
            Unknown,
            [EnumDisplayName("OK")]
            OK,
            [EnumDisplayName("Assembly File Not Found")]
            AssemblyFileNotFound,
            [EnumDisplayName("Load Assembly File Error")]
            AssemblyFileLoadError,
            [EnumDisplayName("Assembly Version Not Compatible")]
            AssemblyVersionNotCompatible,
            [EnumDisplayName("Creat Case Instance Failed")]
            CreateCaseInstanceFailed,
            [EnumDisplayName("Set Case Limits Error")]
            SetCaseLimitsError,
            [EnumDisplayName("Set Case Parameters Error")]
            SetCaseParametersError,
            [EnumDisplayName("Get Case Limits Error")]
            GetCaseLimitsError,
            [EnumDisplayName("Get Case Parameters Error")]
            GetCaseParametersError
        }
        public TestCaseInfo CaseInfo
        {
            get
            {
                return this._caseInfo;
            }
            set {  this._caseInfo = value; }    
        }
        public CoreCase CaseInstance
        {
            get
            {
                return this._caseInstance;
            }
        }
        public ExecutionStatus ExecutionStatus
        {
            get
            {
                return this._executionStatus;
            }
            set
            {
                this._executionStatus = value;
            }
        }
        public TestCase.TestCaseErrorCode ErrorCode
        {
            get
            {
                return this._errorCode;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
        }
        public string CaseName
        {
            get
            {
                Type type = this._caseInstance.GetType();
                string result = type.FullName;
                object[] customAttributes = type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DisplayNameAttribute)
                {
                    result = ((DisplayNameAttribute)customAttributes[0]).DisplayName;
                }
                return result;
            }
        }
        public string DisplayName
        {
            get
            {
                Type type = this._caseInstance.GetType();
                string result = type.FullName;
                object[] customAttributes = type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DisplayNameAttribute)
                {
                    result = ((DisplayNameAttribute)customAttributes[0]).DisplayName;
                }
                PropertyInfo property = this._caseInstance.CaseParameterSetting.GetType().GetProperty("DisplayName", typeof(string));
                if (property != null)
                {
                    string text = property.GetValue(this._caseInstance.CaseParameterSetting, null) as string;
                    if (!string.IsNullOrEmpty(text))
                    {
                        result = text;
                    }
                }
                return result;
            }
        }
        public string Category
        {
            get
            {
                Type type = this._caseInstance.GetType();
                string result = "Misc";//默认
                object[] customAttributes = type.GetCustomAttributes(typeof(CategoryAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is CategoryAttribute)
                {
                    result = ((CategoryAttribute)customAttributes[0]).Category;
                }
                return result;
            }
        }
        public Color DisplayColor
        {
            get
            {
                Type type = this._caseInstance.GetType();
                Color result = SystemColors.WindowText;
                object[] customAttributes = type.GetCustomAttributes(typeof(DisplayColorAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DisplayColorAttribute)
                {
                    result = ((DisplayColorAttribute)customAttributes[0]).DisplayColor;
                }
                if (this._caseInfo != null && this._caseInfo.CaseEngineMode != null && this._caseInfo.CaseEngineMode.Type == EngineType.Skip)
                {
                    result = SystemColors.GrayText;
                }
                return result;
            }
        }
        public string Description
        {
            get
            {
                Type type = this._caseInstance.GetType();
                string result = "N/A";
                object[] customAttributes = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DescriptionAttribute)
                {
                    result = ((DescriptionAttribute)customAttributes[0]).Description;
                }
                return result;
            }
        }
        public ILogViewEx LogView
        {
            get
            {
                return this._logView;
            }
            set
            {
                this._logView = value;
                this._caseInstance.Logger = value;
            }
        }
        public TestCase(TestCaseInfo testCaseInfo)
        {
            this.TestCaseInternal(testCaseInfo);
        }
        public TestCase(CoreCase coreCase, EngineMode engineMode)
        {
            this._errorCode = TestCase.TestCaseErrorCode.Unknown;
            this._caseInstance = coreCase;
            Assembly assembly = coreCase.GetType().Assembly;
            this._caseInfo.AssemblyName = assembly.Location.Remove(0, DefaultFolderInfo.Applications_Folder.Length + 1);
            this._caseInfo.AssemblyVersion = assembly.GetName().Version;
            this._caseInfo.TestCaseFullName = coreCase.GetType().FullName;
            this._caseInfo.CaseEngineMode = engineMode;
            NameValueCollection limits = new NameValueCollection();
            NameValueCollection parameters = new NameValueCollection();
            if (!coreCase.CaseLimitSetting.GetPropertiesValue(out limits))
            {
                this._errorCode = TestCase.TestCaseErrorCode.GetCaseLimitsError;
                this._errorMessage = Resources.LNG_TDP_Engine_TestCase_GetLimitsError;
                return;
            }
            if (!coreCase.CaseParameterSetting.GetPropertiesValue(out parameters))
            {
                this._errorCode = TestCase.TestCaseErrorCode.GetCaseParametersError;
                this._errorMessage = Resources.LNG_TDP_Engine_TestCase_GetParametersError;
                return;
            }
            this._caseInfo.Limits = limits;
            this._caseInfo.Parameters = parameters;
            this._errorCode = TestCase.TestCaseErrorCode.OK;
            this._errorMessage = string.Empty;
        }
        private void TestCaseInternal(TestCaseInfo testCaseInfo)
        {
            this._errorCode = TestCase.TestCaseErrorCode.Unknown;
            this._caseInfo = testCaseInfo;
            string text = string.Format("{0}\\{1}", DefaultFolderInfo.Applications_Folder, this._caseInfo.AssemblyName);
            if (!File.Exists(text))
            {
                this._errorCode = TestCase.TestCaseErrorCode.AssemblyFileNotFound;
                this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TestCase_TdplNotFound + " {0}", this._caseInfo.AssemblyName);
                return;
            }
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFile(text);
            }
            catch (Exception ex)
            {
                this._errorCode = TestCase.TestCaseErrorCode.AssemblyFileLoadError;
                this._errorMessage = string.Format("{0}\n{1}", ex.Message, text);
                return;
            }
            string text2 = assembly.GetName().Name;
            object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (customAttributes.Length > 0 && customAttributes[0] is AssemblyTitleAttribute)
            {
                text2 = (customAttributes[0] as AssemblyTitleAttribute).Title;
            }
            Version version = assembly.GetName().Version;
            if (version.Major == this._caseInfo.AssemblyVersion.Major)
            {
                if (version.Minor == this._caseInfo.AssemblyVersion.Minor)
                {
                    object obj = null;
                    try
                    {
                        obj = assembly.CreateInstance(this._caseInfo.TestCaseFullName, true);
                    }
                    catch (Exception ex2)
                    {
                        this._errorCode = TestCase.TestCaseErrorCode.CreateCaseInstanceFailed;
                        this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TestCase_CreateCaseInstanceFailed + " {0}\n{1}", this._caseInfo.TestCaseFullName, ex2.Message);
                        return;
                    }
                    if (obj == null || !(obj is CoreCase))
                    {
                        this._errorCode = TestCase.TestCaseErrorCode.CreateCaseInstanceFailed;
                        this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TestCase_CreateCaseInstanceFailed + " {0}.", this._caseInfo.TestCaseFullName);
                        return;
                    }
                    this._caseInstance = (obj as CoreCase);
                    if (!this._caseInstance.CaseLimitSetting.SetPropertiesValue(this._caseInfo.Limits))
                    {
                        this._errorCode = TestCase.TestCaseErrorCode.SetCaseLimitsError;
                        this._errorMessage = Resources.LNG_TDP_Engine_TestCase_SetCaseLimitsError;
                        return;
                    }
                    if (!this._caseInstance.CaseParameterSetting.SetPropertiesValue(this._caseInfo.Parameters))
                    {
                        this._errorCode = TestCase.TestCaseErrorCode.SetCaseParametersError;
                        this._errorMessage = Resources.LNG_TDP_Engine_TestCase_SetCaseParametersError;
                        return;
                    }
                    this._errorCode = TestCase.TestCaseErrorCode.OK;
                    this._errorMessage = string.Empty;
                    return;
                }
            }
            this._errorCode = TestCase.TestCaseErrorCode.AssemblyVersionNotCompatible;
            this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TestCase_VersionNotCompitiableWarning, new object[]
            {
                version,
                this._caseInfo.AssemblyVersion,
                text2,
                testCaseInfo.TestCaseFullName
            });
        }

        public void Reset(bool isResetPassFailRatio)
        {
            TestCaseInternal(_caseInfo);
            _executionStatus.ResetTempVariable();
            if (isResetPassFailRatio)
            {
                _executionStatus.ResetPassFailRatio();
            }
        }
        public CoreCase.ExecOkError ExecuteCase()
        {
            bool flag = true;
            try
            {
                flag = (_caseInstance.PreExec() == CoreCase.ExecOkError.Error || _caseInstance.Exec() == CoreCase.ExecOkError.Error || _caseInstance.PostExec() == CoreCase.ExecOkError.Error);
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                flag = true;
                if (_logView != null)
                {
                    _logView.LogEngineTrace(LogEngineTraceType.Error, string.Format("{0}{1}", ex.Message, (ex.InnerException == null) ? string.Empty : (" " + ex.InnerException.Message)));
                }
            }
            if (flag)
            {
                _executionStatus.LastTimeFinalPassFailSkipError = PassFailSkipError.Error;
            }
            switch (_executionStatus.LastTimeFinalPassFailSkipError)
            {
                case PassFailSkipError.Failed:
                    return CoreCase.ExecOkError.OK;
                case PassFailSkipError.Passed:
                    return CoreCase.ExecOkError.OK;
                case PassFailSkipError.Error:
                    return CoreCase.ExecOkError.Error;
            }
            return CoreCase.ExecOkError.OK;
        }
    }
}
