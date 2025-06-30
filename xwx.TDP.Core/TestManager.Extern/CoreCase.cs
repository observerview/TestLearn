using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using ZedGraph;
using System.Drawing;

namespace TestManager.Extern
{
    public abstract class CoreCase
    {
        public enum ExecOkError
        {
            OK,
            Error
        }
        private Form _onwerForm;
        private IRunTimeVarPool _userRunTimeVarPool;

        private IRunTimeVarPoolReadOnly _engineRunTimeVarPool;

        private ILogView _logView;

        public IRunTimeVarPool UserRunTimeVarPool 
        { 
            get { return _userRunTimeVarPool; }
            set { _userRunTimeVarPool = value; }
        }
        public IRunTimeVarPoolReadOnly EngineRunTimeVarPool
        {
            get { return _engineRunTimeVarPool; }
            set { _engineRunTimeVarPool = value; }
        }
        public ILogView Logger
        {
            get { return _logView; }
            set { _logView = value; }
        }

        public Form OnwerForm
        {
            get { return _onwerForm; }
            set { _onwerForm = value; }
        }
        public string ResultDirectory
        {
            get{ return _engineRunTimeVarPool["Plan.TestResultDir"] as string;}
        }

        public string AppDirectory
        {
            get{return _engineRunTimeVarPool["Application.RootDirectory"] as string;}
        }

        public string CurrentPlanName
        {
            get{ return _engineRunTimeVarPool["Plan.Name"] as string;}
        }
        public virtual ConfigBase CaseLimitSetting
        {
            get
            {
                return new ConfigBase();
            }
            set
            {
            }
        }

        public virtual ConfigBase CaseParameterSetting
        {
            get
            {
                return new ConfigBase();
            }
            set
            {
            }
        }

        protected void LogResult(string title, string resultString, string unit)
        {
            Logger.LogResult(LogResultType.TempResult, title, null, resultString, unit, null);
        }

        protected void LogResult(string title, object result, string resultString, string unit, ValueLimit limit)
        {
            Logger.LogResult(LogResultType.Normal, title, result, resultString, unit, limit);
        }

        protected void LogResult(string title, object result, string resultString, string unit, ValueLimitCollection limits)
        {
            Logger.LogResult(LogResultType.Normal, title, result, resultString, unit, limits);
        }

        protected void LogResult(string title, object result, string unit, ValueLimit limit)
        {
            Logger.LogResult(LogResultType.Normal, title, result, result.ToString(), unit, limit);
        }

        protected void LogResult(string title, object result, string unit, ValueLimitCollection limits)
        {
            Logger.LogResult(LogResultType.Normal, title, result, result.ToString(), unit, limits);
        }

        protected void LogResult(LogResultType logResultType, string title, object result, string resultString, string unit, ValueLimit limit)
        {
            Logger.LogResult(logResultType, title, result, resultString, unit, limit);
        }

        protected void LogResult(LogResultType logResultType, string title, object result, string resultString, string unit, ValueLimitCollection limits)
        {
            Logger.LogResult(logResultType, title, result, resultString, unit, limits);
        }

        protected void LogResult(LogResultType logResultType, string title, object result, string unit, ValueLimit limit)
        {
            Logger.LogResult(logResultType, title, result, result.ToString(), unit, limit);
        }

        protected void LogResult(LogResultType logResultType, string title, object result, string unit, ValueLimitCollection limits)
        {
            Logger.LogResult(logResultType, title, result, result.ToString(), unit, limits);
        }

        protected void LogInfo(LogInfoType logInfoType, string title, string value, string unit)
        {
            Logger.LogInfo(logInfoType, title, value, unit);
        }

        protected void LogInfo(LogInfoType logInfoType, string title, string value)
        {
            Logger.LogInfo(logInfoType, title, value, string.Empty);
        }

        protected void LogInfo(LogInfoType logInfoType, string title)
        {
            Logger.LogInfo(logInfoType, title, string.Empty, string.Empty);
        }

        protected void LogInfo()
        {
            Logger.LogInfo(LogInfoType.Normal, "--------------------------------", string.Empty, string.Empty);
        }

        protected void LogTip(LogTipType logTipType, string text)
        {
            Logger.LogTip(logTipType, text, float.NaN, true);
        }

        protected void LogTip(LogTipType logTipType, string text, float fontSize, bool isAlignCenter)
        {
            Logger.LogTip(logTipType, text, fontSize, isAlignCenter);
        }

        protected void LogMessage(LogMessageType logMessageType, string text)
        {
            Logger.LogMessage(logMessageType, text);
        }

        protected DialogResult LogMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            UserMessageBoxData data = new UserMessageBoxData(text, caption, buttons, icon);
            List<object> retValues = new List<object>();
            Logger.LogUserDefinedData(data, out retValues);
            DialogResult result = DialogResult.None;
            foreach (object item in retValues)
            {
                if (item is DialogResult)
                {
                    result = (DialogResult)item;
                }
            }
            return result;
        }

        protected DialogResult LogMessageBox(string text, string caption, MessageBoxButtons buttons)
        {
            return LogMessageBox(text, caption, buttons, MessageBoxIcon.Asterisk);
        }

        protected DialogResult LogMessageBox(string text, string caption)
        {
            return LogMessageBox(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        protected DialogResult LogMessageBox(string text)
        {
            return LogMessageBox(text, CurrentPlanName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        protected void LogNonModalDialog(Form userDialog)
        {
            userDialog.Text = string.Format("{0} - {1}", _engineRunTimeVarPool["Plan.Name"] as string, userDialog.Text);
            UserDialogLogData data = new UserDialogLogData(userDialog, null);
            List<object> retValues = new List<object>();
            Logger.LogUserDefinedData(data, out retValues);
        }

        protected void LogModalDialog(Form userDialog)
        {
            userDialog.Text = string.Format("{0} - {1}", _engineRunTimeVarPool["Plan.Name"] as string, userDialog.Text);
            LogModalDialog(userDialog, false);
        }

        protected void LogModalDialog(Form userDialog, bool resizable)
        {
            userDialog.ShowInTaskbar = false;
            userDialog.MinimizeBox = false;
            userDialog.MaximizeBox = resizable;
            userDialog.FormBorderStyle = (resizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog);
            UserDialogLogData data = new UserDialogLogData(userDialog, OnwerForm);
            List<object> retValues = new List<object>();
            Logger.LogUserDefinedData(data, out retValues);
        }

        protected void LogGraph()
        {
            GraphLogData data = new GraphLogData();
            List<object> retValues = new List<object>();
            Logger.LogUserDefinedData(data, out retValues);
        }

        protected void LogGraph(MasterPane masterPane, string graphTitle)
        {
            LogGraph(masterPane, graphTitle, false);
        }

        protected void LogGraph(GraphPane graphPane, string graphTitle)
        {
            LogGraph(graphPane, graphTitle, false);
        }

        protected void LogGraph(MasterPane masterPane, string graphTitle, bool isSaveToFile)
        {
            string fileName = string.Empty;
            if (isSaveToFile)
            {
                char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
                char[] array = invalidFileNameChars;
                foreach (char oldChar in array)
                {
                    graphTitle = graphTitle.Replace(oldChar, '_');
                }
                fileName = string.Format("[{0}]@@{1}{2}", graphTitle, DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff"), ".tmg");
            }
            GraphLogData data = new GraphLogData(masterPane, graphTitle, fileName, isSaveToFile);
            List<object> retValues = new List<object>();
            Logger.LogUserDefinedData(data, out retValues);
        }

        protected void LogGraph(GraphPane graphPane, string graphTitle, bool isSaveToFile)
        {
            MasterPane masterPane = new MasterPane();
            masterPane.Add(graphPane);
            LogGraph(masterPane, graphTitle, isSaveToFile);
        }

        protected void LogGraph(System.Drawing.Image image, string graphTitle)
        {
            LogGraph(image, graphTitle, false);
        }

        protected void LogGraph(System.Drawing.Image image, string graphTitle, bool isSaveToFile)
        {
            string fileName = string.Empty;
            if (isSaveToFile)
            {
                char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
                char[] array = invalidFileNameChars;
                foreach (char oldChar in array)
                {
                    graphTitle = graphTitle.Replace(oldChar, '_');
                }
                fileName = string.Format("[{0}]@@{1}{2}", graphTitle, DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff"), ".img");
            }
            GraphLogData data = new GraphLogData(image, graphTitle, fileName, isSaveToFile);
            List<object> retValues = new List<object>();
            Logger.LogUserDefinedData(data, out retValues);
        }

        public abstract ExecOkError PreExec();

        public abstract ExecOkError Exec();

        public abstract ExecOkError PostExec();
    }
}
