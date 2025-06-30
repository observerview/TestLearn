using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern.Interface;
using TestManager.Utility.GenericForm;
using TestManager.Utility.Misc;
using TestManager.Utility.PropertyGridEx;
using WeifenLuo.WinFormsUI.Docking;
using xwx.TDP.Editor.Engine;
using xwx.TDP.Editor.Misc;
using xwx.TDP.Editor.Properties;

namespace xwx.TDP.Editor.LogViews
{
    internal partial class MainLogView : LogViewWindow, ITdpModuleAuthorizationAdmin, IInternalLogView, ILogView, ILogViewEx, IPlanExecutor
    {
        public EngineLogLogView EngineLogLogView
        {
            set
            {
                this._engineLogLogView = value;
            }
        }
        public bool IsEngineRunning
        {
            get
            {
                return this._isEngineRunning;
            }
        }
        public TdpEngine Engine
        {
            get
            {
                return this.engine;
            }
            set
            {
                this.engine = value;
            }
        }
        public Font SequenceInfoTextFont
        {
            set
            {
                this.label_SequenceInfo.Font = value;
            }
        }

        public Color SequenceInfoAresBackgroundColor
        {
            set
            {
                _setSequenceInfoAreaBackColor(value);
            }
        }
        private void _setSequenceInfoAreaBackColor(Color backColor)
        {
            if (!this.label_SequenceInfo.InvokeRequired)
            {
                this.label_SequenceInfo.BackColor = backColor;
                this.label_SequenceInfo.Update();
                return;
            }
            _setSequenceInfoAreaBackColorDelegate method = new _setSequenceInfoAreaBackColorDelegate(this._setSequenceInfoAreaBackColor);
            base.Invoke(method, new object[]{backColor});
        }
        public string TipText
        {
            get
            {
                return this.label_Tips.Text;
            }
            set
            {
                this._setTipText(value);
            }
        }
        public bool TipTextIsShow
        {
            set
            {
                this._isShowTipText(value);
            }
        }
        public void TipTextShow(string tipText)
        {
            this._setTipText(tipText);
            this._isShowTipText(!string.IsNullOrEmpty(tipText));
        }
        private void _setTipText(string tipText)
        {
            if (!this.label_Tips.InvokeRequired)
            {
                this.label_Tips.Text = tipText;
                this.label_Tips.Update();
                return;
            }
            _setTipTextDelegate method = new _setTipTextDelegate(this._setTipText);
            base.Invoke(method, new object[]
            {
                tipText
            });
        }
        private void _isShowTipText(bool isShow)
        {
            if (!this.label_Tips.InvokeRequired)
            {
                this.splitterH.Visible = isShow;
                this.label_Tips.Visible = isShow;
                this.label_Tips.Update();
                return;
            }
            _isShowTipTextDelegate method = new _isShowTipTextDelegate(this._isShowTipText);
            base.Invoke(method, new object[]
            {
                isShow
            });
        }

        public Color TipTextForeColor
        {
            set
            {
                this._setTipTextForeColor(value);
            }
        }
        public Color TipTextBackColor
        {
            set
            {
                this._setTipTextBackColor(value);
            }
        }
        public float TipTextFontSize
        {
            set
            {
                this._setTipTextFontSize(value);
            }
        }
        public ContentAlignment TipTextAlignment
        {
            set
            {
                this._setTipTextAlignment(value);
            }
        }

        private void _setTipTextForeColor(Color foreColor)
        {
            if (!this.label_Tips.InvokeRequired)
            {
                this.label_Tips.ForeColor = foreColor;
                this.label_Tips.Update();
                return;
            }
            _setTipTextForeColorDelegate method = new _setTipTextForeColorDelegate(_setTipTextForeColor);
            base.Invoke(method, new object[]
            {
                foreColor
            });
        }
        private void _setTipTextBackColor(Color backColor)
        {
            if (!this.label_Tips.InvokeRequired)
            {
                this.label_Tips.BackColor = backColor;
                this.label_Tips.Update();
                return;
            }
            _setTipTextBackColorDelegate method = new _setTipTextBackColorDelegate(_setTipTextBackColor);
            base.Invoke(method, new object[]
            {
                backColor
            });
        }
        private void _setTipTextFontSize(float fontSize)
        {
            if (!this.label_Tips.InvokeRequired)
            {
                this.label_Tips.Font = new Font(label_Tips.Font.FontFamily, fontSize);
                this.label_Tips.Update();
                return;
            }
            _setTipTextFontSizeDelegate method = new _setTipTextFontSizeDelegate(_setTipTextFontSize);
            base.Invoke(method, new object[]
            {
                fontSize
            });
        }
        private void _setTipTextAlignment(ContentAlignment alignment)
        {
            if (!this.label_Tips.InvokeRequired)
            {
                this.label_Tips.TextAlign = alignment;
                this.label_Tips.Update();
                return;
            }
            _setTipTextAlignmentDelegate method = new _setTipTextAlignmentDelegate(_setTipTextAlignment);
            base.Invoke(method, new object[]
            {
                alignment
            });
        }

        public void ListViewResultClear()
        {
            this._listViewResultClear();
        }
        public void ListViewResultAddItem(ListViewItem lvi)
        {
            this._listViewResultAddItem(lvi);
        }
        public void ListViewResultSetItemColor(int index, Color bgColor, Color foreColor)
        {
            this._listViewResultSetItemColor(index, bgColor, foreColor);
        }
        public void ListViewResultSetItemText(int index, int nColumn, string text)
        {
            this._listViewResultSetItemText(index, nColumn, text);
        }
        public void ListViewResultSetItemIcon(int index, string iconKey)
        {
            this._listViewResultSetItemIcon(index, iconKey);
        }
        private void _listViewResultSetItemIcon(int index, string iconKey)
        {
            if (!this.listView_Result.InvokeRequired)
            {
                this.listView_Result.Items[index].ImageKey = iconKey;
                this.listView_Result.Update();
                return;
            }
            _listViewResultSetItemIconDelegate method = new _listViewResultSetItemIconDelegate(_listViewResultSetItemIcon);
            base.Invoke(method, new object[]
            {
                index,
                iconKey
            });
        }
        private void _listViewResultClear()
        {
            if (!this.listView_Result.InvokeRequired)
            {
                this.listView_Result.Items.Clear();
                this.listView_Result.Update();
                return;
            }
            _listViewResultClearDelegate method = new _listViewResultClearDelegate(_listViewResultClear);
            base.Invoke(method, new object[0]);
        }
        private void _listViewResultAddItem(ListViewItem lvi)
        {
            if (!listView_Result.InvokeRequired)
            {
                listView_Result.Items.Add(lvi);
                if (Settings.Default.TDP_UI_LogView_MainLogViewEngineLogLogView_AutoSctroll)
                {
                    listView_Result.EnsureVisible(listView_Result.Items.Count - 1);
                }
                listView_Result.Update();
                return;
            }
            _listViewResultAddItemDelegate method = new _listViewResultAddItemDelegate(this._listViewResultAddItem);
            base.Invoke(method, new object[]
            {
                lvi
            });
        }
        private void _listViewResultSetItemColor(int index, Color bgColor, Color foreColor)
        {
            if (!this.listView_Result.InvokeRequired)
            {
                if (index < this.listView_Result.Items.Count)
                {
                    if (bgColor != Color.Transparent)
                    {
                        this.listView_Result.Items[index].BackColor = bgColor;
                    }
                    if (foreColor != Color.Transparent)
                    {
                        this.listView_Result.Items[index].ForeColor = foreColor;
                    }
                    this.listView_Result.Update();
                    return;
                }
            }
            else
            {
                _listViewResultSetItemColorDelegate method = new _listViewResultSetItemColorDelegate(this._listViewResultSetItemColor);
                base.Invoke(method, new object[]
                {
                    index,
                    bgColor,
                    foreColor
                });
            }
        }
        private void _listViewResultSetItemText(int index, int nColumn, string text)
        {
            if (!this.listView_Result.InvokeRequired)
            {
                if (index < this.listView_Result.Items.Count && nColumn < this.listView_Result.Columns.Count)
                {
                    this.listView_Result.Items[index].SubItems[nColumn].Text = text;
                    this.listView_Result.Update();
                    return;
                }
            }
            else
            {
                _listViewResultSetItemTextDelegate method = new _listViewResultSetItemTextDelegate(this._listViewResultSetItemText);
                base.Invoke(method, new object[]{
                    index,
                    nColumn,
                    text
                });
            }
        }

        public MainLogView()
        {
            InitializeComponent();
            Disposed += this.MainLogView_Disposed;
            Init();
        }
        private void MainLogView_Disposed(object sender, EventArgs e)
        {
            if (this.engine != null)
            {
                try
                {
                    this.engine.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }
        
        private void Init()
        {
            Utilities.SetControlFont(this, Settings.Default.TDP_UI_DefaultFont);
            this.Init_Ctrl();
            
        }
        private void Init_Ctrl()
        {
            this.fileSystemWatcher.Filter = "*.*";
            this.fileSystemWatcher.Path = DefaultFolderInfo.SequenceLibrary_Folder;
            this.toolStripButton_ResultFolder.ToolTipText = Settings.Default.TDP_TestResultFolder;
            this.RefreshSequenceList();
            if (!string.IsNullOrEmpty(Settings.Default.TDP_SequenceExecutor_HeaderWidth))
            {
                try
                {
                    int[] array = DefaultArrayConverter.ConvertFromString(Settings.Default.TDP_SequenceExecutor_HeaderWidth, typeof(int[])) as int[];
                    int num = (array.Length < this.listView_Result.Columns.Count) ? array.Length : this.listView_Result.Columns.Count;
                    for (int i = 0; i < num; i++)
                    {
                        if (this.listView_Result.Columns[i].Text != "*")
                        {
                            this.listView_Result.Columns[i].Width = array[i];
                        }
                    }
                }
                catch
                {
                }
            }
            Settings.Default.TDP_UI_LogView_MainLogViewEngineLogLogView_AutoSctroll = true;
            toolStripButton_AutoScrollList.Checked = Settings.Default.TDP_UI_LogView_MainLogViewEngineLogLogView_AutoSctroll;
            toolStripButton_ShowSequenceDetailInfo.Checked = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
            label_SequenceInfo.Height = Settings.Default.TDP_MainLogView_SequenceInfoAreaHeight;
            splitter_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
            label_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
            listView_Result.Height = Settings.Default.TDP_MainLogView_ListViewResultAreaHeight;
        }
        public void LogTip(LogTipType logTipType, string text, float fontSize, bool isAlignCenter)
        {
            switch (logTipType)
            {
                case LogTipType.Normal:
                    this.TipTextForeColor = Color.Black;
                    this.TipTextBackColor = Color.White;
                    break;
                case LogTipType.Notify:
                    this.TipTextForeColor = Color.Black;
                    this.TipTextBackColor = Color.LightYellow;
                    break;
                case LogTipType.Warning:
                    this.TipTextForeColor = Color.Black;
                    this.TipTextBackColor = Color.LightPink;
                    break;
            }
            this.TipTextShow(text);
            if (float.IsNaN(fontSize))
            {
                fontSize = 24f;
            }
            this.TipTextFontSize = fontSize;
            this.TipTextAlignment = (isAlignCenter ? ContentAlignment.MiddleCenter : ContentAlignment.TopLeft);
        }
        public IRunTimeVarPoolReadOnly EngineRunTimeVarPool
        {
            set
            {
                this._engineRunTimeVarPool = value;
            }
        }
        public IPlanExecutor PlanExecutorInstance
        {
            set
            {
                this._planExecutorInstance = value;
            }
        }
        public IRunTimeVarPoolReadOnly UserRunTimeVarPool
        {
            set
            {
                this._userRunTimeVarPool = value;
            }
        }
        public string LogDirectory
        {
            set
            {
                this._logDirectory = value;
            }
        }
        public void CloseView()
        {
            base.Close();
        }
        public void HideView()
        {
            base.Hide();
        }
        public void ShowView()
        {
            if (this._mdiDockPanel != null && this._mdiDockPanel is DockPanel)
            {
                base.Show(this._mdiDockPanel as DockPanel);
                return;
            }
            base.Show();
        }

        public void LogEngineStatus_debug(LogEngineStatusType logEngineStatusType)
        {
            if (!Settings.Default.TDP_Engine_MainLogView_LogEngineStatus)
            {
                return;
            }
            if (logEngineStatusType != LogEngineStatusType.AbortedByEngine)
            {
                if (logEngineStatusType != LogEngineStatusType.AbortedByManual)
                {
                    if (logEngineStatusType == LogEngineStatusType.Finished)
                    {
                        EnableToolButton(toolStripButton_OpenTestResult, true);
                        EnableToolButton(toolStripButton_ResultFolder, true);
                        EnableToolButton(toolStripSplitButton_Sequence, true);
                        _isEngineRunning = false;
                        EnableToolButton(toolStripButton_Start, true);
                        EnableToolButton(toolStripButton_Pause, false);
                        SetToolButton(toolStripButton_Start, Resources.LNG_TDP_Engine_Start, Resources.LNG_TDP_Engine_Start + " (F5)", Resources.start, "Start");
                        SetToolButton(toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                        ListViewItem listViewItem = new ListViewItem();
                        foreach (object obj in listView_Result.Columns)
                        {
                            ColumnHeader columnHeader = (ColumnHeader)obj;
                            string a;
                            if ((a = (columnHeader.Tag as string)) != null)
                            {
                                if (a == "*")
                                {
                                    continue;
                                }
                                if (a == "Title")
                                {
                                    listViewItem.SubItems.Add(string.Format("Finished: {0}", this._engineRunTimeVarPool["Plan.Name"]));
                                    continue;
                                }
                                if (a == "Time")
                                {
                                    listViewItem.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                                    continue;
                                }
                                if (a == "Result")
                                {
                                    TimeSpan timeSpan = (DateTime)this._engineRunTimeVarPool["Plan.StopTime"] - (DateTime)this._engineRunTimeVarPool["Plan.StartTime"];
                                    listViewItem.SubItems.Add(string.Format("{0:d2}:{1:d2}'{2:d2}''", (int)Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds));
                                    continue;
                                }
                                if (a == "Pass/Fail")
                                {
                                    switch ((PassFailSkipError)this._engineRunTimeVarPool["Plan.FinalPassFail"])
                                    {
                                        case PassFailSkipError.Failed:
                                            listViewItem.BackColor = Color.Red;
                                            break;
                                        case PassFailSkipError.Passed:
                                            listViewItem.BackColor = Color.Green;
                                            break;
                                        case PassFailSkipError.Skipped:
                                        case PassFailSkipError.Error:
                                            listViewItem.BackColor = Color.Red;
                                            break;
                                    }
                                    listViewItem.SubItems.Add(this._engineRunTimeVarPool["Plan.FinalPassFail"].ToString());
                                    continue;
                                }
                            }
                            listViewItem.SubItems.Add(string.Empty);
                        }
                        listViewItem.ForeColor = Color.White;
                        this.ListViewResultAddItem(listViewItem);
                    }
                    if (logEngineStatusType == LogEngineStatusType.Started)
                    {
                        this.EnableToolButton(this.toolStripButton_OpenTestResult, false);
                        this.EnableToolButton(this.toolStripButton_ResultFolder, false);
                        this.EnableToolButton(this.toolStripSplitButton_Sequence, false);
                        this._isEngineRunning = true;
                        this.EnableToolButton(this.toolStripButton_Start, true);
                        this.EnableToolButton(this.toolStripButton_Pause, true);
                        this.SetToolButton(this.toolStripButton_Start, Resources.LNG_TDP_Engine_Stop, Resources.LNG_TDP_Engine_Stop + " (F5)", Resources.stop, "Stop");
                        this.SetToolButton(this.toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                    }
                    if (logEngineStatusType == LogEngineStatusType.CaseBegin)
                    {
                        ListViewItem listViewItem2 = new ListViewItem();
                        ListViewItem listViewItem3 = new ListViewItem();
                        ListViewItem listViewItem4 = new ListViewItem();
                        ListViewItem listViewItem5 = new ListViewItem();
                        foreach (object obj2 in this.listView_Result.Columns)
                        {
                            ColumnHeader columnHeader2 = (ColumnHeader)obj2;
                            string a2;
                            if ((a2 = (columnHeader2.Tag as string)) != null)
                            {
                                if (a2 == "*")
                                {
                                    continue;
                                }
                                if (a2 == "Title")
                                {
                                    listViewItem2.SubItems.Add(this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
                                    listViewItem3.SubItems.Add("TDP For Demostration Only.");
                                    listViewItem4.SubItems.Add("Ensure You Have The Authorization.");
                                    listViewItem5.SubItems.Add("Please Contact AIT Team.");
                                    continue;
                                }
                                if (a2 == "Time")
                                {
                                    listViewItem2.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                                    listViewItem3.SubItems.Add(string.Empty);
                                    listViewItem4.SubItems.Add(string.Empty);
                                    listViewItem5.SubItems.Add(string.Empty);
                                    continue;
                                }
                            }
                            listViewItem2.SubItems.Add(string.Empty);
                            listViewItem3.SubItems.Add(string.Empty);
                            listViewItem4.SubItems.Add(string.Empty);
                            listViewItem5.SubItems.Add(string.Empty);
                        }
                        listViewItem2.BackColor = Color.Gray;
                        listViewItem2.ForeColor = Color.White;
                        this.ListViewResultAddItem(listViewItem2);
                        this._lastCastTitleIndexInListViewResult = this.listView_Result.Items.Count - 1;
                        if (TdpEngine.__DEMOSTRATION__)
                        {
                            listViewItem3.BackColor = Color.LightPink;
                            listViewItem3.ForeColor = Color.Black;
                            listViewItem4.BackColor = Color.LightPink;
                            listViewItem4.ForeColor = Color.Black;
                            listViewItem5.BackColor = Color.LightPink;
                            listViewItem5.ForeColor = Color.Black;
                            this.ListViewResultAddItem(listViewItem3);
                            this.ListViewResultAddItem(listViewItem4);
                            this.ListViewResultAddItem(listViewItem5);
                        }
                    }
                    else
                    {
                        if (logEngineStatusType == LogEngineStatusType.CaseEnd)
                        {
                            if (this._engineRunTimeVarPool["Current.CasePassFailSkipError"] != null && this._engineRunTimeVarPool["Current.CasePassFailSkipError"] is PassFailSkipError)
                            {
                                PassFailSkipError passFailSkipError = (PassFailSkipError)this._engineRunTimeVarPool["Current.CasePassFailSkipError"];
                                foreach (object obj3 in this.listView_Result.Columns)
                                {
                                    ColumnHeader columnHeader3 = (ColumnHeader)obj3;
                                    string a3;
                                    if ((a3 = (columnHeader3.Tag as string)) != null)
                                    {
                                        if (!(a3 == "Pass/Fail"))
                                        {
                                            if (a3 == "Result")
                                            {
                                                TimeSpan timeSpan2 = (DateTime)this._engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] - (DateTime)this._engineRunTimeVarPool["Internal.Current.TestCase.StartTime"];
                                                this.ListViewResultSetItemText(this._lastCastTitleIndexInListViewResult, columnHeader3.Index, string.Format("{0:d2}'{1:d2}''", (int)Math.Floor(timeSpan2.TotalMinutes), timeSpan2.Seconds));
                                            }
                                        }
                                        else
                                        {
                                            this.ListViewResultSetItemText(this._lastCastTitleIndexInListViewResult, columnHeader3.Index, passFailSkipError.ToString());
                                        }
                                    }
                                }
                                switch (passFailSkipError)
                                {
                                    case PassFailSkipError.Error:
                                        this.ListViewResultSetItemIcon(this._lastCastTitleIndexInListViewResult, "ResultItemError.png");
                                        break;
                                }
                            }
                            this.ListViewResultAddItem(new ListViewItem());
                        }
                        if (logEngineStatusType != LogEngineStatusType.Paused)
                        {
                        }
                    }
                }
            }
            else
            {
                this.EnableToolButton(this.toolStripButton_OpenTestResult, true);
                this.EnableToolButton(this.toolStripButton_ResultFolder, true);
                this.EnableToolButton(this.toolStripSplitButton_Sequence, true);
                this._isEngineRunning = false;
                this.EnableToolButton(this.toolStripButton_Start, true);
                this.EnableToolButton(this.toolStripButton_Pause, false);
                this.SetToolButton(this.toolStripButton_Start, Resources.LNG_TDP_Engine_Start, Resources.LNG_TDP_Engine_Start + " (F5)", Resources.start, "Start");
                this.SetToolButton(this.toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                ListViewItem listViewItem6 = new ListViewItem();
                foreach (object obj4 in this.listView_Result.Columns)
                {
                    ColumnHeader columnHeader4 = (ColumnHeader)obj4;
                    string a4;
                    if ((a4 = (columnHeader4.Tag as string)) != null)
                    {
                        if (a4 == "*")
                        {
                            continue;
                        }
                        if (a4 == "Title")
                        {
                            listViewItem6.SubItems.Add(string.Format("{0} Aborted", (logEngineStatusType == LogEngineStatusType.AbortedByEngine) ? "Engine" : "Manual"));
                            continue;
                        }
                        if (a4 == "Result")
                        {
                            TimeSpan timeSpan3 = (DateTime)this._engineRunTimeVarPool["Plan.StopTime"] - (DateTime)this._engineRunTimeVarPool["Plan.StartTime"];
                            listViewItem6.SubItems.Add(string.Format("{0:d2}:{1:d2}'{2:d2}''", (int)Math.Floor(timeSpan3.TotalHours), timeSpan3.Minutes, timeSpan3.Seconds));
                            continue;
                        }
                        if (a4 == "Time")
                        {
                            listViewItem6.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                            continue;
                        }
                        if (a4 == "Pass/Fail")
                        {
                            listViewItem6.SubItems.Add("Aborted");
                            continue;
                        }
                    }
                    listViewItem6.SubItems.Add(string.Empty);
                }
                listViewItem6.BackColor = Color.LightPink;
                listViewItem6.ForeColor = Color.Black;
                this.ListViewResultAddItem(listViewItem6);

            }
            //switch (logEngineStatusType)
            //{
            //    case LogEngineStatusType.Finished:
            //    case LogEngineStatusType.AbortedByEngine:
            //        break;
            //    case LogEngineStatusType.Started | LogEngineStatusType.Finished:
            //        goto IL_1138;
            //    default:
            //        if (logEngineStatusType != LogEngineStatusType.AbortedByManual)
            //        {
            //            goto IL_1138;
            //        }
            //        break;
            //}
            if(logEngineStatusType == LogEngineStatusType.Finished || logEngineStatusType == LogEngineStatusType.AbortedByEngine || logEngineStatusType == LogEngineStatusType.AbortedByManual)
            {
                string text = string.Empty;
                TimeSpan timeSpan4 = (DateTime)this._engineRunTimeVarPool["Plan.StopTime"] - (DateTime)this._engineRunTimeVarPool["Plan.StartTime"];
                string str = string.Format("{0:d2}H{1:d2}M{2:d2}S", (int)Math.Floor(timeSpan4.TotalHours), timeSpan4.Minutes, timeSpan4.Seconds);
                string text2 = string.Empty;
                if (this._userRunTimeVarPool.ContainKey("DUT.SerialsNumber"))
                {
                    if (this._userRunTimeVarPool["DUT.SerialsNumber"] is string)
                    {
                        text2 = (this._userRunTimeVarPool["DUT.SerialsNumber"] as string);
                    }
                    else if (this._userRunTimeVarPool["DUT.SerialsNumber"] is List<string>)
                    {
                        text2 = DefaultArrayConverter.ConvertToString((this._userRunTimeVarPool["DUT.SerialsNumber"] as List<string>).ToArray());
                    }
                    else
                    {
                        text2 = this._userRunTimeVarPool["DUT.SerialsNumber"].ToString();
                    }
                    text2 = text2.Trim(new char[]
                    {
                    '{',
                    '}'
                    });
                    File.WriteAllText(string.Format("{0}\\{1}.dut_no", this._logDirectory, text2), string.Empty);
                }
                string text3 = Settings.Default.TDP_RawDataLogView_ReportFileNamePattern.Replace("%d", string.IsNullOrEmpty(text2) ? string.Empty : ("[" + text2 + "]")).Replace("%t", "[" + this._engineRunTimeVarPool["Plan.Name"].ToString() + "]").Replace("%s", "[" + ((DateTime)this._engineRunTimeVarPool["Plan.StartTime"]).ToString("yyyyMMdd-HHmmss-ffff") + "]").Replace("%e", "[" + str + "]").Replace("%p", "[" + (this._engineRunTimeVarPool["Engine.Status"].ToString().Contains("Aborted") ? this._engineRunTimeVarPool["Engine.Status"].ToString() : (this._engineRunTimeVarPool["Engine.Status"].ToString() + " - " + this._engineRunTimeVarPool["Plan.FinalPassFail"].ToString())) + "]");
                char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
                foreach (char oldChar in invalidFileNameChars)
                {
                    text3 = text3.Replace(oldChar, '_');
                }
                string text4 = string.Format("{0}\\{1}", Settings.Default.TDP_TestResultFolder, ((DateTime)this._engineRunTimeVarPool["Plan.StartTime"]).ToString("yyyy-MM-dd"));
                if (!Directory.Exists(text4))
                {
                    Directory.CreateDirectory(text4);
                }
                //text = string.Format("{0}\\{1}.tdpr", text4, text3.Trim());
                text = string.Format("{0}\\{1}.zip", text4, text3.Trim());
                this._planExecutorInstance.LogTip(LogTipType.Normal, "Now Compressing Report, Please Wait...", float.NaN, true);
                string empty = string.Empty;
                bool flag;
                if (!(flag = FolderCompresserZip.Compress(this._logDirectory, text, out empty)))
                {
                    this._planExecutorInstance.LogMessage(LogMessageType.Warning, string.Format("Compressing report files failed: {0}\n\n{1}", empty, this._logDirectory));
                    if (Settings.Default.TDP_Engine_DeleteRawReportFile)
                    {
                        Settings.Default.TDP_Engine_DeleteRawReportFile = false;
                        this._planExecutorInstance.LogMessage(LogMessageType.Warning, "The setting of \"delete raw report files to save disk space\" has been un-checked to avoid deleting raw report files.");
                    }
                }
                (_engineRunTimeVarPool as IRunTimeVarPool).Add("Engine.ReportFileFullName", flag ? text : string.Empty);

                CurrentExecutingSequenceName = string.Empty;
            }

                ListViewItem listViewItem7 = new ListViewItem();
                foreach (object obj5 in this.listView_Result.Columns)
                {
                    ColumnHeader columnHeader5 = (ColumnHeader)obj5;
                    string a4;
                    if ((a4 = (columnHeader5.Tag as string)) != null)
                    {
                        if (a4 == "*")
                        {
                            continue;
                        }
                        if (a4 == "Title")
                        {
                            listViewItem7.SubItems.Add("End Of The Execution.");
                            continue;
                        }
                        if (a4 == "Time")
                        {
                            listViewItem7.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                            continue;
                        }
                    }
                    listViewItem7.SubItems.Add(string.Empty);
                }
                if (logEngineStatusType <= LogEngineStatusType.AbortedByManual)
                {
                    switch (logEngineStatusType)
                    {
                        case LogEngineStatusType.Finished:
                            switch ((PassFailSkipError)_engineRunTimeVarPool["Plan.FinalPassFail"])
                            {
                                case PassFailSkipError.Failed:
                                    TipTextForeColor = Color.White;
                                    TipTextBackColor = Color.Red;
                                    TipTextShow(PassFailSkipError.Failed.ToString());
                                    TipTextFontSize = 36f;
                                    TipTextAlignment = ContentAlignment.MiddleCenter;
                                    listViewItem7.BackColor = Color.Red;
                                    listViewItem7.ForeColor = Color.White;
                                    ListViewResultAddItem(listViewItem7);
                                    return;
                                case PassFailSkipError.Passed:
                                    TipTextForeColor = Color.White;
                                    TipTextBackColor = Color.Green;
                                    TipTextShow(PassFailSkipError.Passed.ToString());
                                    TipTextFontSize = 36f;
                                    TipTextAlignment = ContentAlignment.MiddleCenter;
                                    listViewItem7.BackColor = Color.Green;
                                    listViewItem7.ForeColor = Color.White;
                                    ListViewResultAddItem(listViewItem7);
                                    return;
                                case PassFailSkipError.Skipped:
                                case PassFailSkipError.Error:
                                    return;
                                default:
                                    return;
                            }
                        case LogEngineStatusType.Started | LogEngineStatusType.Finished:
                            return;
                        case LogEngineStatusType.AbortedByEngine:
                            break;
                        default:
                            if (logEngineStatusType != LogEngineStatusType.AbortedByManual)
                            {
                                return;
                            }
                            break;
                    }
                    TipTextForeColor = Color.Black;
                    TipTextBackColor = Color.LightPink;
                    TipTextShow("Aborted");
                    TipTextFontSize = 36f;
                    TipTextAlignment = ContentAlignment.MiddleCenter;
                    listViewItem7.BackColor = Color.LightPink;
                    listViewItem7.ForeColor = Color.Black;
                    ListViewResultAddItem(listViewItem7);
                    return;
                }
                if (logEngineStatusType != LogEngineStatusType.Paused && logEngineStatusType != LogEngineStatusType.Running)
                {
                    return;
                }
            
        }


        public void LogEngineStatus(LogEngineStatusType logEngineStatusType)
        {
            if (!Settings.Default.TDP_Engine_MainLogView_LogEngineStatus)
            {
                return;
            }
            if (logEngineStatusType != LogEngineStatusType.AbortedByEngine)
            {
                if (logEngineStatusType != LogEngineStatusType.AbortedByManual)
                {
                    if (logEngineStatusType == LogEngineStatusType.Finished)
                    {
                        EnableToolButton(toolStripButton_OpenTestResult, true);
                        EnableToolButton(toolStripButton_ResultFolder, true);
                        EnableToolButton(toolStripSplitButton_Sequence, true);
                        _isEngineRunning = false;
                        EnableToolButton(toolStripButton_Start, true);
                        EnableToolButton(toolStripButton_Pause, false);
                        SetToolButton(toolStripButton_Start, Resources.LNG_TDP_Engine_Start, Resources.LNG_TDP_Engine_Start + " (F5)", Resources.start, "Start");
                        SetToolButton(toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                        ListViewItem listViewItem = new ListViewItem();
                        foreach (object obj in listView_Result.Columns)
                        {
                            ColumnHeader columnHeader = (ColumnHeader)obj;
                            string a;
                            if ((a = (columnHeader.Tag as string)) != null)
                            {
                                if (a == "*")
                                {
                                    continue;
                                }
                                if (a == "Title")
                                {
                                    listViewItem.SubItems.Add(string.Format("Finished: {0}", this._engineRunTimeVarPool["Plan.Name"]));
                                    continue;
                                }
                                if (a == "Time")
                                {
                                    listViewItem.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                                    continue;
                                }
                                if (a == "Result")
                                {
                                    TimeSpan timeSpan = (DateTime)this._engineRunTimeVarPool["Plan.StopTime"] - (DateTime)this._engineRunTimeVarPool["Plan.StartTime"];
                                    listViewItem.SubItems.Add(string.Format("{0:d2}:{1:d2}'{2:d2}''", (int)Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds));
                                    continue;
                                }
                                if (a == "Pass/Fail")
                                {
                                    switch ((PassFailSkipError)this._engineRunTimeVarPool["Plan.FinalPassFail"])
                                    {
                                        case PassFailSkipError.Failed:
                                            listViewItem.BackColor = Color.Red;
                                            break;
                                        case PassFailSkipError.Passed:
                                            listViewItem.BackColor = Color.Green;
                                            break;
                                        case PassFailSkipError.Skipped:
                                        case PassFailSkipError.Error:
                                            listViewItem.BackColor = Color.Red;
                                            break;
                                    }
                                    listViewItem.SubItems.Add(this._engineRunTimeVarPool["Plan.FinalPassFail"].ToString());
                                    continue;
                                }
                            }
                            listViewItem.SubItems.Add(string.Empty);
                        }
                        listViewItem.ForeColor = Color.White;
                        this.ListViewResultAddItem(listViewItem);
                        goto IL_9C2;
                    }
                    if (logEngineStatusType == LogEngineStatusType.Started)
                    {
                        this.EnableToolButton(this.toolStripButton_OpenTestResult, false);
                        this.EnableToolButton(this.toolStripButton_ResultFolder, false);
                        this.EnableToolButton(this.toolStripSplitButton_Sequence, false);
                        this._isEngineRunning = true;
                        this.EnableToolButton(this.toolStripButton_Start, true);
                        this.EnableToolButton(this.toolStripButton_Pause, true);
                        this.SetToolButton(this.toolStripButton_Start, Resources.LNG_TDP_Engine_Stop, Resources.LNG_TDP_Engine_Stop + " (F5)", Resources.stop, "Stop");
                        this.SetToolButton(this.toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                        goto IL_9C2;
                    }
                    if (logEngineStatusType == LogEngineStatusType.CaseBegin)
                    {
                        ListViewItem listViewItem2 = new ListViewItem();
                        ListViewItem listViewItem3 = new ListViewItem();
                        ListViewItem listViewItem4 = new ListViewItem();
                        ListViewItem listViewItem5 = new ListViewItem();
                        foreach (object obj2 in this.listView_Result.Columns)
                        {
                            ColumnHeader columnHeader2 = (ColumnHeader)obj2;
                            string a2;
                            if ((a2 = (columnHeader2.Tag as string)) != null)
                            {
                                if (a2 == "*")
                                {
                                    continue;
                                }
                                if (a2 == "Title")
                                {
                                    listViewItem2.SubItems.Add(this._engineRunTimeVarPool.ContainKey("Current.CaseName") ? (this._engineRunTimeVarPool["Current.CaseName"] as string) : string.Empty);
                                    listViewItem3.SubItems.Add("TDP For Demostration Only.");
                                    listViewItem4.SubItems.Add("Ensure You Have The Authorization.");
                                    listViewItem5.SubItems.Add("Please Contact AIT Team.");
                                    continue;
                                }
                                if (a2 == "Time")
                                {
                                    listViewItem2.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                                    listViewItem3.SubItems.Add(string.Empty);
                                    listViewItem4.SubItems.Add(string.Empty);
                                    listViewItem5.SubItems.Add(string.Empty);
                                    continue;
                                }
                            }
                            listViewItem2.SubItems.Add(string.Empty);
                            listViewItem3.SubItems.Add(string.Empty);
                            listViewItem4.SubItems.Add(string.Empty);
                            listViewItem5.SubItems.Add(string.Empty);
                        }
                        listViewItem2.BackColor = Color.Gray;
                        listViewItem2.ForeColor = Color.White;
                        this.ListViewResultAddItem(listViewItem2);
                        this._lastCastTitleIndexInListViewResult = this.listView_Result.Items.Count - 1;
                        if (TdpEngine.__DEMOSTRATION__)
                        {
                            listViewItem3.BackColor = Color.LightPink;
                            listViewItem3.ForeColor = Color.Black;
                            listViewItem4.BackColor = Color.LightPink;
                            listViewItem4.ForeColor = Color.Black;
                            listViewItem5.BackColor = Color.LightPink;
                            listViewItem5.ForeColor = Color.Black;
                            this.ListViewResultAddItem(listViewItem3);
                            this.ListViewResultAddItem(listViewItem4);
                            this.ListViewResultAddItem(listViewItem5);
                            goto IL_9C2;
                        }
                        goto IL_9C2;
                    }
                    else
                    {
                        if (logEngineStatusType == LogEngineStatusType.CaseEnd)
                        {
                            if (this._engineRunTimeVarPool["Current.CasePassFailSkipError"] != null && this._engineRunTimeVarPool["Current.CasePassFailSkipError"] is PassFailSkipError)
                            {
                                PassFailSkipError passFailSkipError = (PassFailSkipError)this._engineRunTimeVarPool["Current.CasePassFailSkipError"];
                                foreach (object obj3 in this.listView_Result.Columns)
                                {
                                    ColumnHeader columnHeader3 = (ColumnHeader)obj3;
                                    string a3;
                                    if ((a3 = (columnHeader3.Tag as string)) != null)
                                    {
                                        if (!(a3 == "Pass/Fail"))
                                        {
                                            if (a3 == "Result")
                                            {
                                                TimeSpan timeSpan2 = (DateTime)this._engineRunTimeVarPool["Internal.Current.TestCase.StopTime"] - (DateTime)this._engineRunTimeVarPool["Internal.Current.TestCase.StartTime"];
                                                this.ListViewResultSetItemText(this._lastCastTitleIndexInListViewResult, columnHeader3.Index, string.Format("{0:d2}'{1:d2}''", (int)Math.Floor(timeSpan2.TotalMinutes), timeSpan2.Seconds));
                                            }
                                        }
                                        else
                                        {
                                            this.ListViewResultSetItemText(this._lastCastTitleIndexInListViewResult, columnHeader3.Index, passFailSkipError.ToString());
                                        }
                                    }
                                }
                                switch (passFailSkipError)
                                {
                                    case PassFailSkipError.Error:
                                        this.ListViewResultSetItemIcon(this._lastCastTitleIndexInListViewResult, "ResultItemError.png");
                                        break;
                                }
                            }
                            this.ListViewResultAddItem(new ListViewItem());
                            goto IL_9C2;
                        }
                        if (logEngineStatusType != LogEngineStatusType.Paused)
                        {
                            goto IL_9C2;
                        }
                        goto IL_9C2;
                    }
                }
            }
            this.EnableToolButton(this.toolStripButton_OpenTestResult, true);
            this.EnableToolButton(this.toolStripButton_ResultFolder, true);
            this.EnableToolButton(this.toolStripSplitButton_Sequence, true);
            this._isEngineRunning = false;
            this.EnableToolButton(this.toolStripButton_Start, true);
            this.EnableToolButton(this.toolStripButton_Pause, false);
            this.SetToolButton(this.toolStripButton_Start, Resources.LNG_TDP_Engine_Start, Resources.LNG_TDP_Engine_Start + " (F5)", Resources.start, "Start");
            this.SetToolButton(this.toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
            ListViewItem listViewItem6 = new ListViewItem();
            foreach (object obj4 in this.listView_Result.Columns)
            {
                ColumnHeader columnHeader4 = (ColumnHeader)obj4;
                string a4;
                if ((a4 = (columnHeader4.Tag as string)) != null)
                {
                    if (a4 == "*")
                    {
                        continue;
                    }
                    if (a4 == "Title")
                    {
                        listViewItem6.SubItems.Add(string.Format("{0} Aborted", (logEngineStatusType == LogEngineStatusType.AbortedByEngine) ? "Engine" : "Manual"));
                        continue;
                    }
                    if (a4 == "Result")
                    {
                        TimeSpan timeSpan3 = (DateTime)this._engineRunTimeVarPool["Plan.StopTime"] - (DateTime)this._engineRunTimeVarPool["Plan.StartTime"];
                        listViewItem6.SubItems.Add(string.Format("{0:d2}:{1:d2}'{2:d2}''", (int)Math.Floor(timeSpan3.TotalHours), timeSpan3.Minutes, timeSpan3.Seconds));
                        continue;
                    }
                    if (a4 == "Time")
                    {
                        listViewItem6.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                        continue;
                    }
                    if (a4 == "Pass/Fail")
                    {
                        listViewItem6.SubItems.Add("Aborted");
                        continue;
                    }
                }
                listViewItem6.SubItems.Add(string.Empty);
            }
            listViewItem6.BackColor = Color.LightPink;
            listViewItem6.ForeColor = Color.Black;
            this.ListViewResultAddItem(listViewItem6);
        IL_9C2:
            switch (logEngineStatusType)
            {
                case LogEngineStatusType.Finished:
                case LogEngineStatusType.AbortedByEngine:
                    break;
                case LogEngineStatusType.Started | LogEngineStatusType.Finished:
                    goto IL_1138;
                default:
                    if (logEngineStatusType != LogEngineStatusType.AbortedByManual)
                    {
                        goto IL_1138;
                    }
                    break;
            }
            string text = string.Empty;
            TimeSpan timeSpan4 = (DateTime)this._engineRunTimeVarPool["Plan.StopTime"] - (DateTime)this._engineRunTimeVarPool["Plan.StartTime"];
            string str = string.Format("{0:d2}H{1:d2}M{2:d2}S", (int)Math.Floor(timeSpan4.TotalHours), timeSpan4.Minutes, timeSpan4.Seconds);
            string text2 = string.Empty;
            if (this._userRunTimeVarPool.ContainKey("DUT.SerialsNumber"))
            {
                if (this._userRunTimeVarPool["DUT.SerialsNumber"] is string)
                {
                    text2 = (this._userRunTimeVarPool["DUT.SerialsNumber"] as string);
                }
                else if (this._userRunTimeVarPool["DUT.SerialsNumber"] is List<string>)
                {
                    text2 = DefaultArrayConverter.ConvertToString((this._userRunTimeVarPool["DUT.SerialsNumber"] as List<string>).ToArray());
                }
                else
                {
                    text2 = this._userRunTimeVarPool["DUT.SerialsNumber"].ToString();
                }
                text2 = text2.Trim(new char[]
                {
                    '{',
                    '}'
                });
                File.WriteAllText(string.Format("{0}\\{1}.dut_no", this._logDirectory, text2), string.Empty);
            }
            string text3 = Settings.Default.TDP_RawDataLogView_ReportFileNamePattern.Replace("%d", string.IsNullOrEmpty(text2) ? string.Empty : ("[" + text2 + "]")).Replace("%t", "[" + this._engineRunTimeVarPool["Plan.Name"].ToString() + "]").Replace("%s", "[" + ((DateTime)this._engineRunTimeVarPool["Plan.StartTime"]).ToString("yyyyMMdd-HHmmss-ffff") + "]").Replace("%e", "[" + str + "]").Replace("%p", "[" + (this._engineRunTimeVarPool["Engine.Status"].ToString().Contains("Aborted") ? this._engineRunTimeVarPool["Engine.Status"].ToString() : (this._engineRunTimeVarPool["Engine.Status"].ToString() + " - " + this._engineRunTimeVarPool["Plan.FinalPassFail"].ToString())) + "]");
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            foreach (char oldChar in invalidFileNameChars)
            {
                text3 = text3.Replace(oldChar, '_');
            }
            string text4 = string.Format("{0}\\{1}", Settings.Default.TDP_TestResultFolder, ((DateTime)this._engineRunTimeVarPool["Plan.StartTime"]).ToString("yyyy-MM-dd"));
            if (!Directory.Exists(text4))
            {
                Directory.CreateDirectory(text4);
            }
            //text = string.Format("{0}\\{1}.tdpr", text4, text3.Trim());
            text = string.Format("{0}\\{1}.zip", text4, text3.Trim());
            this._planExecutorInstance.LogTip(LogTipType.Normal, "Now Compressing Report, Please Wait...", float.NaN, true);
            string empty = string.Empty;
            bool flag;
            if (!(flag = FolderCompresserZip.Compress(this._logDirectory, text, out empty)))
            {
                this._planExecutorInstance.LogMessage(LogMessageType.Warning, string.Format("Compressing report files failed: {0}\n\n{1}", empty, this._logDirectory));
                if (Settings.Default.TDP_Engine_DeleteRawReportFile)
                {
                    Settings.Default.TDP_Engine_DeleteRawReportFile = false;
                    this._planExecutorInstance.LogMessage(LogMessageType.Warning, "The setting of \"delete raw report files to save disk space\" has been un-checked to avoid deleting raw report files.");
                }
            }
            (this._engineRunTimeVarPool as IRunTimeVarPool).Add("Engine.ReportFileFullName", flag ? text : string.Empty);

            CurrentExecutingSequenceName = string.Empty;
        IL_1138:
            ListViewItem listViewItem7 = new ListViewItem();
            foreach (object obj5 in this.listView_Result.Columns)
            {
                ColumnHeader columnHeader5 = (ColumnHeader)obj5;
                string a4;
                if ((a4 = (columnHeader5.Tag as string)) != null)
                {
                    if (a4 == "*")
                    {
                        continue;
                    }
                    if (a4 == "Title")
                    {
                        listViewItem7.SubItems.Add("End Of The Execution.");
                        continue;
                    }
                    if (a4 == "Time")
                    {
                        listViewItem7.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                        continue;
                    }
                }
                listViewItem7.SubItems.Add(string.Empty);
            }
            if (logEngineStatusType <= LogEngineStatusType.AbortedByManual)
            {
                switch (logEngineStatusType)
                {
                    case LogEngineStatusType.Finished:
                        switch ((PassFailSkipError)_engineRunTimeVarPool["Plan.FinalPassFail"])
                        {
                            case PassFailSkipError.Failed:
                                TipTextForeColor = Color.White;
                                TipTextBackColor = Color.Red;
                                TipTextShow(PassFailSkipError.Failed.ToString());
                                TipTextFontSize = 36f;
                                TipTextAlignment = ContentAlignment.MiddleCenter;
                                listViewItem7.BackColor = Color.Red;
                                listViewItem7.ForeColor = Color.White;
                                ListViewResultAddItem(listViewItem7);
                                return;
                            case PassFailSkipError.Passed:
                                TipTextForeColor = Color.White;
                                TipTextBackColor = Color.Green;
                                TipTextShow(PassFailSkipError.Passed.ToString());
                                TipTextFontSize = 36f;
                                TipTextAlignment = ContentAlignment.MiddleCenter;
                                listViewItem7.BackColor = Color.Green;
                                listViewItem7.ForeColor = Color.White;
                                ListViewResultAddItem(listViewItem7);
                                return;
                            case PassFailSkipError.Skipped:
                            case PassFailSkipError.Error:
                                return;
                            default:
                                return;
                        }
                    case LogEngineStatusType.Started | LogEngineStatusType.Finished:
                        return;
                    case LogEngineStatusType.AbortedByEngine:
                        break;
                    default:
                        if (logEngineStatusType != LogEngineStatusType.AbortedByManual)
                        {
                            return;
                        }
                        break;
                }
                TipTextForeColor = Color.Black;
                TipTextBackColor = Color.LightPink;
                TipTextShow("Aborted");
                TipTextFontSize = 36f;
                TipTextAlignment = ContentAlignment.MiddleCenter;
                listViewItem7.BackColor = Color.LightPink;
                listViewItem7.ForeColor = Color.Black;
                ListViewResultAddItem(listViewItem7);
                return;
            }
            if (logEngineStatusType != LogEngineStatusType.Paused && logEngineStatusType != LogEngineStatusType.Running)
            {
                return;
            }
        }

        public void LogReset()
        {
            this.TipText = string.Empty;
            this.TipTextIsShow = false;
            this.ListViewResultClear();
        }
        public void LogInfo(LogInfoType logInfoType, string title, string value, string unit)
        {
            if (!Settings.Default.TDP_Engine_MainLogView_LogInfo)
            {
                return;
            }
            switch (logInfoType)
            {
                case LogInfoType.Normal:
                    if (!Settings.Default.TDP_Engine_MainLogView_LogInfo_Normal)
                    {
                        return;
                    }
                    break;
                case LogInfoType.Warning:
                    if (!Settings.Default.TDP_Engine_MainLogView_LogInfo_Warning)
                    {
                        return;
                    }
                    break;
                case LogInfoType.Normal | LogInfoType.Warning:
                    break;
                case LogInfoType.Error:
                    if (!Settings.Default.TDP_Engine_MainLogView_LogInfo_Error)
                    {
                        return;
                    }
                    break;
                default:
                    if (logInfoType != LogInfoType.Notify)
                    {
                        if (logInfoType == LogInfoType.Log)
                        {
                            if (!Settings.Default.TDP_Engine_MainLogView_LogInfo_Log)
                            {
                                return;
                            }
                        }
                    }
                    else if (!Settings.Default.TDP_Engine_MainLogView_LogInfo_Notify)
                    {
                        return;
                    }
                    break;
            }
            ListViewItem listViewItem = new ListViewItem();
            foreach (object obj in this.listView_Result.Columns)
            {
                ColumnHeader columnHeader = (ColumnHeader)obj;
                string key;
                switch (key = (columnHeader.Tag as string))
                {
                    case "*":
                        continue;
                    case "Result":
                        listViewItem.SubItems.Add(value);
                        continue;
                    case "Unit":
                        listViewItem.SubItems.Add(unit);
                        continue;
                    case "Title":
                        if (title.StartsWith("<-Parameter->"))
                        {
                            title = title.Substring("<-Parameter->".Length);
                            listViewItem.ForeColor = Color.Gray;
                        }
                        listViewItem.SubItems.Add(title);
                        continue;
                    case "Time":
                        listViewItem.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                        continue;
                    case "Pass/Fail":
                        listViewItem.SubItems.Add((logInfoType == LogInfoType.Error || logInfoType == LogInfoType.Warning) ? logInfoType.ToString() : string.Empty);
                        continue;
                }
                listViewItem.SubItems.Add(string.Empty);
            }
            switch (logInfoType)
            {
                case LogInfoType.Normal:
                case LogInfoType.Normal | LogInfoType.Warning:
                    break;
                case LogInfoType.Warning:
                    listViewItem.ImageKey = "ResultItemInfo_Warning.png";
                    break;
                case LogInfoType.Error:
                    listViewItem.ImageKey = "ResultItemInfo_Error.png";
                    break;
                default:
                    if (logInfoType != LogInfoType.Notify)
                    {
                        if (logInfoType == LogInfoType.Log)
                        {
                            listViewItem.ForeColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        listViewItem.ImageKey = "ResultItemInfo_Emphasize.png";
                    }
                    break;
            }
            this.ListViewResultAddItem(listViewItem);
        }
        public void LogResult(LogResultType logResultType, string title, object result, string resultString, string unit, object limit)
        {
            if (!Settings.Default.TDP_Engine_MainLogView_LogResult)
            {
                return;
            }
            switch (logResultType)
            {
                case LogResultType.Normal:
                    if (!Settings.Default.TDP_Engine_MainLogView_LogResult_Normal)
                    {
                        return;
                    }
                    break;
                case LogResultType.TempResult:
                    if (!Settings.Default.TDP_Engine_MainLogView_LogResult_Temp)
                    {
                        return;
                    }
                    break;
            }
            Color black = Color.Black;
            string text = string.Empty;
            object obj = null;
            string imageKey = string.Empty;
            if (result != null && limit != null && (limit is ValueLimit || limit is ValueLimitCollection))
            {
                if (limit is ValueLimit)
                {
                    switch ((limit as ValueLimit).ValidateValue(result, out obj))
                    {
                        case ValueLimit.ValueValidResult.InLimit:
                            {
                                Color green = Color.Green;
                                imageKey = "ResultItemPass.png";
                                text = PassFail.Pass.ToString();
                                goto IL_EF;
                            }
                    }
                    if (logResultType != LogResultType.Normal)
                    {
                        Color fuchsia = Color.Fuchsia;
                    }
                    else
                    {
                        Color red = Color.Red;
                    }
                    imageKey = ((logResultType == LogResultType.Normal) ? "ResultItemFail.png" : "ResultItemFailTemp.png");
                    text = PassFail.Fail.ToString();
                }
            IL_EF:
                if (limit is ValueLimitCollection)
                {
                    switch ((limit as ValueLimitCollection).ValidateValue(result, out obj))
                    {
                        case ValueLimitCollection.ValueValidResult.InLimit:
                            {
                                Color green2 = Color.Green;
                                imageKey = "ResultItemPass.png";
                                text = PassFail.Pass.ToString();
                                goto IL_167;
                            }
                    }
                    if (logResultType != LogResultType.Normal)
                    {
                        Color fuchsia2 = Color.Fuchsia;
                    }
                    else
                    {
                        Color red2 = Color.Red;
                    }
                    imageKey = ((logResultType == LogResultType.Normal) ? "ResultItemFail.png" : "ResultItemFailTemp.png");
                    text = PassFail.Fail.ToString();
                }
            IL_167:
                switch (logResultType)
                {
                    case LogResultType.TempResult:
                        text += "*";
                        break;
                }
            }
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.ImageKey = imageKey;
            foreach (object obj2 in this.listView_Result.Columns)
            {
                ColumnHeader columnHeader = (ColumnHeader)obj2;
                string key;
                switch (key = (columnHeader.Tag as string))
                {
                    case "*":
                        continue;
                    case "Limit":
                        listViewItem.SubItems.Add((limit == null) ? string.Empty : limit.ToString());
                        continue;
                    case "Result":
                        listViewItem.SubItems.Add(resultString);
                        continue;
                    case "Unit":
                        listViewItem.SubItems.Add(unit);
                        continue;
                    case "Title":
                        listViewItem.SubItems.Add(title);
                        continue;
                    case "Time":
                        listViewItem.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                        continue;
                    case "Pass/Fail":
                        listViewItem.SubItems.Add(text);
                        continue;
                }
                listViewItem.SubItems.Add(this._userRunTimeVarPool.ContainKey(columnHeader.Tag as string) ? this._userRunTimeVarPool[columnHeader.Tag as string].ToString() : string.Empty);
            }
            ListViewResultAddItem(listViewItem);
            if (Settings.Default.TDP_Engine_MainLogView_PromptForFailResult && text.Contains(PassFail.Fail.ToString()))
            {
                MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_Engine_MainLogView_PromptForFailTestMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void LogUserDefinedData(ILogData data, out List<object> retValues)
        {
            retValues = new List<object>();
            if (data == null)
            {
                return;
            }
            if (data is UserFormLogData)
            {
                return;
            }
            if (data is UserDialogLogData)
            {
                MessageBoxEx.ShowDialog((data as UserDialogLogData).LoggedDialog, (data as UserDialogLogData).OnwerForm);
                return;
            }
            if (data is UserMessageBoxData)
            {
                retValues.Add(MessageBoxEx.Show(base.MdiParent, (data as UserMessageBoxData).MessageBoxText, (data as UserMessageBoxData).MessageBoxCaption, (data as UserMessageBoxData).MessageBoxButtons, (data as UserMessageBoxData).MessageBoxIcon));
                return;
            }
            if (Settings.Default.TDP_Engine_MainLogView_LogUserDefinedData_General)
            {
                if (data is GraphLogData && !(data as GraphLogData).IsSaveToFile)
                {
                    return;
                }
                if (data.IsSaveLog)
                {
                    Image image = (data is GraphLogData) ? Resources.Graph : data.Icon;
                    if (!this.listView_Result.SmallImageList.Images.ContainsKey(data.GetType().FullName))
                    {
                        this.listView_Result.SmallImageList.Images.Add(data.GetType().FullName, image);
                    }
                    ListViewItem listViewItem = new ListViewItem();
                    foreach (object obj in this.listView_Result.Columns)
                    {
                        ColumnHeader columnHeader = (ColumnHeader)obj;
                        string a;
                        if ((a = (columnHeader.Tag as string)) != null)
                        {
                            if (a == "Title")
                            {
                                listViewItem.SubItems.Add(data.Message);
                                continue;
                            }
                            if (a == "Time")
                            {
                                listViewItem.SubItems.Add(DateTime.Now.ToString("MM-dd HH:mm:ss"));
                                continue;
                            }
                            if (a == "*")
                            {
                                listViewItem.Text = string.Format("[{0}]##{1}", data.GetType().FullName, data.Data);
                                continue;
                            }
                        }
                        listViewItem.SubItems.Add(string.Empty);
                    }
                    listViewItem.ImageKey = data.GetType().FullName;
                    this.ListViewResultAddItem(listViewItem);
                }
            }
        }
        private void toolStripButton_ResultFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = Settings.Default.TDP_TestResultFolder;
            folderBrowserDialog.Description = Resources.LNG_TDP_MainLogView_ResultFolderNotifier;
            if (folderBrowserDialog.ShowDialog(base.MdiParent) == DialogResult.OK)
            {
                Settings.Default.TDP_TestResultFolder = folderBrowserDialog.SelectedPath;
                this.toolStripButton_ResultFolder.ToolTipText = folderBrowserDialog.SelectedPath;
            }
        }

        private void toolStripButton_Start_Click(object sender, EventArgs e)
        {
            if (toolStripButton_Start.Tag == null || !(toolStripButton_Start.Tag is string) || !(toolStripButton_Start.Tag as string == "Stop"))
            {
                if (toolStripSplitButton_Sequence.Tag != null && toolStripSplitButton_Sequence.Tag is TdpTestSequenceInfo)
                {
                    EnableToolButton(toolStripButton_OpenTestResult, false);
                    EnableToolButton(toolStripButton_ResultFolder, false);
                    EnableToolButton(toolStripSplitButton_Sequence, false);
                    _isEngineRunning = true;
                    EnableToolButton(toolStripButton_Start, true);
                    EnableToolButton(toolStripButton_Pause, true);
                    SetToolButton(toolStripButton_Start, Resources.LNG_TDP_Engine_Stop, Resources.LNG_TDP_Engine_Stop + " (F5)", Resources.stop, "Stop");
                    SetToolButton(toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                    CurrentExecutingSequenceName = (this.toolStripSplitButton_Sequence.Tag as TdpTestSequenceInfo).XmlFileFullName;
                    engine.TestSequence = new TdpTestSequence();
                    bool flag = true;
                    if (!engine.TestSequence.LoadFromFile(CurrentExecutingSequenceName))
                    {
                        MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_MainLogView_LoadSequenceFailed + "\n\n" + engine.TestSequence.ErrorMessage, base.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        flag = false;
                    }
                    if (engine.TestSequence.IsHasReadingParameterOrLimit2XmlWarning)
                    {
                        MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_MainLogView_SequenceParameterLimitChangedWarning, base.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        flag = false;
                    }
                    if (!flag)
                    {
                        EnableToolButton(toolStripButton_OpenTestResult, true);
                        EnableToolButton(toolStripButton_ResultFolder, true);
                        EnableToolButton(toolStripSplitButton_Sequence, true);
                        _isEngineRunning = false;
                        EnableToolButton(toolStripButton_Start, true);
                        EnableToolButton(toolStripButton_Pause, false);
                        SetToolButton(toolStripButton_Start, Resources.LNG_TDP_Engine_Start, Resources.LNG_TDP_Engine_Start + " (F5)", Resources.start, "Start");
                        SetToolButton(toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                        CurrentExecutingSequenceName = string.Empty;
                        return;
                    }
                    engine.Start();
                }
                return;
            }
            engine.Pause();
            if (MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_MainLogView_StopConfirm, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                engine.Stop();
                return;
            }
            engine.Resume();
        }
        private void toolStripButton_Pause_Click(object sender, EventArgs e)
        {
            if (this.toolStripButton_Pause.Tag != null && this.toolStripButton_Pause.Tag is string && this.toolStripButton_Pause.Tag as string == "Resume")
            {
                this.engine.Resume();
                this.SetToolButton(this.toolStripButton_Pause, Resources.LNG_TDP_Engine_Pause, Resources.LNG_TDP_Engine_Pause, Resources.pause, "Pause");
                return;
            }
            this.engine.Pause();
            this.SetToolButton(this.toolStripButton_Pause, Resources.LNG_TDP_Engine_Resume, Resources.LNG_TDP_Engine_Resume, Resources.resume, "Resume");
        }
        private void RefreshSequenceList()
        {
            this.toolStripSplitButton_Sequence.DropDownItems.Clear();
            List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
            this.GetSequenceListMenu(DefaultFolderInfo.SequenceLibrary_Folder, out list);
            this.toolStripSplitButton_Sequence.DropDownItems.AddRange(list.ToArray());
            bool flag = false;
            if (!string.IsNullOrEmpty(Settings.Default.TDP_SequenceExecutor_LastExecutedSequence) && File.Exists(Settings.Default.TDP_SequenceExecutor_LastExecutedSequence))
            {
                TdpTestSequenceInfo tag = new TdpTestSequenceInfo();
                string text;
                if (TdpTestSequence.GetTdpSequenceInfo(Settings.Default.TDP_SequenceExecutor_LastExecutedSequence, out tag, out text))
                {
                    this.SequenceListItem_Click(new ToolStripMenuItem
                    {
                        Tag = tag
                    }, new EventArgs());
                    flag = true;
                    this.toolStripButton_Pause.Enabled = !this.toolStripSplitButton_Sequence.Enabled;
                }
            }
            if (!flag)
            {
                this.toolStripSplitButton_Sequence.Text = Resources.LNG_TDP_MainLogView_SelectSequence;
                this.toolStripSplitButton_Sequence.ToolTipText = Resources.LNG_TDP_MainLogView_SelectSequence;
                this.toolStripSplitButton_Sequence.Tag = null;
                this.toolStripButton_Start.Enabled = false;
                this.toolStripButton_Pause.Enabled = false;
                this.toolStripButton_EditSequence.Enabled = false;
                this.label_SequenceInfo.Text = "N/A";
                Settings.Default.TDP_SequenceExecutor_LastExecutedSequence = string.Empty;
                Settings.Default.Save();
            }
        }
        private void GetSequenceListMenu(string folder, out List<ToolStripMenuItem> toolStripMenuItems)
        {
            toolStripMenuItems = new List<ToolStripMenuItem>();
            string[] directories = Directory.GetDirectories(folder);
            Array.Sort<string>(directories);
            foreach (string text in directories)
            {
                List<ToolStripMenuItem> list;
                this.GetSequenceListMenu(text, out list);
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(new DirectoryInfo(text).Name, Resources.FolderClosed);
                toolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                toolStripMenuItems.Add(toolStripMenuItem);
            }
            string[] files = Directory.GetFiles(folder, "*.tdpts");
            Array.Sort<string>(files);
            foreach (string xmlFileFullName in files)
            {
                TdpTestSequenceInfo tsdpTestSequenceInfo = new TdpTestSequenceInfo();
                string empty = string.Empty;
                if (TdpTestSequence.GetTdpSequenceInfo(xmlFileFullName, out tsdpTestSequenceInfo, out empty))
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(tsdpTestSequenceInfo.DisplayName, Resources.Sequence);
                    toolStripMenuItem2.Tag = tsdpTestSequenceInfo;
                    toolStripMenuItem2.ToolTipText = string.Format(Resources.LNG_TDP_SequenceSelector_SequenceInfo_MenuItem.Replace("/t", "\t"), new object[]
                    {
                        Path.GetFileName(tsdpTestSequenceInfo.XmlFileFullName),
                        tsdpTestSequenceInfo.DisplayName,
                        tsdpTestSequenceInfo.Author,
                        tsdpTestSequenceInfo.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        (tsdpTestSequenceInfo.ModifiedTime == DateTime.MinValue) ? string.Empty : tsdpTestSequenceInfo.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        EnumDisplayNameAttribute.GetEnumerationDisplayName(tsdpTestSequenceInfo.GlobeEngineMode.Error),
                        tsdpTestSequenceInfo.GlobeEngineMode.ErrorRetry,
                        EnumDisplayNameAttribute.GetEnumerationDisplayName(tsdpTestSequenceInfo.GlobeEngineMode.OK),
                        tsdpTestSequenceInfo.GlobeEngineMode.OkRetry,
                        tsdpTestSequenceInfo.Description
                    });
                    toolStripMenuItem2.Click += this.SequenceListItem_Click;
                    toolStripMenuItems.Add(toolStripMenuItem2);
                }
            }
        }
        private void SequenceListItem_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is ToolStripMenuItem && (sender as ToolStripMenuItem).Tag is TdpTestSequenceInfo)
            {
                TdpTestSequenceInfo tsdpTestSequenceInfo = (sender as ToolStripMenuItem).Tag as TdpTestSequenceInfo;
                this.toolStripSplitButton_Sequence.Text = tsdpTestSequenceInfo.DisplayName;
                this.toolStripSplitButton_Sequence.Tag = tsdpTestSequenceInfo;
                this.toolStripSplitButton_Sequence.ToolTipText = string.Format(Resources.LNG_TDP_SequenceSelector_SequenceInfo_MenuItem.Replace("/t", "\t"), new object[]
                {
                    Path.GetFileName(tsdpTestSequenceInfo.XmlFileFullName),
                    tsdpTestSequenceInfo.DisplayName,
                    tsdpTestSequenceInfo.Author,
                    tsdpTestSequenceInfo.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    (tsdpTestSequenceInfo.ModifiedTime == DateTime.MinValue) ? string.Empty : tsdpTestSequenceInfo.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EnumDisplayNameAttribute.GetEnumerationDisplayName(tsdpTestSequenceInfo.GlobeEngineMode.Error),
                    tsdpTestSequenceInfo.GlobeEngineMode.ErrorRetry,
                    EnumDisplayNameAttribute.GetEnumerationDisplayName(tsdpTestSequenceInfo.GlobeEngineMode.OK),
                    tsdpTestSequenceInfo.GlobeEngineMode.OkRetry,
                    tsdpTestSequenceInfo.Description
                });
                if (Enum.IsDefined(typeof(SequenceInfoDisplayStyle), Settings.Default.TDP_MainLogView_SequenceInfoDisplayStyle))
                {
                    switch ((SequenceInfoDisplayStyle)Enum.Parse(typeof(SequenceInfoDisplayStyle), Settings.Default.TDP_MainLogView_SequenceInfoDisplayStyle))
                    {
                        case SequenceInfoDisplayStyle.Name:
                            this.label_SequenceInfo.Text = tsdpTestSequenceInfo.DisplayName;
                            break;
                        case SequenceInfoDisplayStyle.Description:
                            this.label_SequenceInfo.Text = tsdpTestSequenceInfo.Description;
                            break;
                        case SequenceInfoDisplayStyle.NameDescription:
                            this.label_SequenceInfo.Text = tsdpTestSequenceInfo.DisplayName + "\n" + tsdpTestSequenceInfo.Description;
                            break;
                    }
                }
                this.toolStripButton_Start.Enabled = true;
                this.toolStripButton_Pause.Enabled = false;
                this.toolStripButton_EditSequence.Enabled = true;
                Settings.Default.TDP_SequenceExecutor_LastExecutedSequence = tsdpTestSequenceInfo.XmlFileFullName;
                Settings.Default.Save();
            }
        }
        private void EnableToolButton(ToolStripItem tsi, bool isEnable)
        {
            if (!this.toolStrip.InvokeRequired)
            {
                tsi.Enabled = isEnable;
                this.toolStrip.Update();
                return;
            }
            EnableToolButtonDelegate method = new EnableToolButtonDelegate(this.EnableToolButton);
            this.toolStrip.Invoke(method, new object[]
            {
                tsi,
                isEnable
            });
        }
        private void SetToolButton(ToolStripItem tsi, string text, string toolTip, Image image, string tagString)
        {
            if (!this.toolStrip.InvokeRequired)
            {
                tsi.Text = text;
                tsi.ToolTipText = toolTip;
                tsi.Image = image;
                tsi.Tag = tagString;
                toolStrip.Update();
                return;
            }
            SetToolButtonDelegate method = new SetToolButtonDelegate(SetToolButton);
            toolStrip.Invoke(method, new object[]
            {
                tsi,
                text,
                toolTip,
                image,
                tagString
            });
        }
        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            this.RefreshSequenceList();
        }
        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.RefreshSequenceList();
        }
        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (Settings.Default.TDP_SequenceExecutor_LastExecutedSequence.StartsWith(e.OldFullPath, true, null) && Settings.Default.TDP_SequenceExecutor_LastExecutedSequence[e.OldFullPath.Length] == '\\')
            {
                Settings.Default.TDP_SequenceExecutor_LastExecutedSequence = e.FullPath + Settings.Default.TDP_SequenceExecutor_LastExecutedSequence.Substring(e.OldFullPath.Length);
            }
            string tdp_SequenceExecutor_LastExecutedSequence = Settings.Default.TDP_SequenceExecutor_LastExecutedSequence;
            this.RefreshSequenceList();
        }
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.RefreshSequenceList();
        }
        public string LogViewName
        {
            get
            {
                return this.Text;
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
                Utilities.SetControlFont(this, value);
            }
        }
        private void MainLogView_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<int> list = new List<int>();
            foreach (object obj in this.listView_Result.Columns)
            {
                ColumnHeader columnHeader = (ColumnHeader)obj;
                list.Add(columnHeader.Width);
            }
            Settings.Default.TDP_SequenceExecutor_HeaderWidth = DefaultArrayConverter.ConvertToString(list.ToArray());
            Settings.Default.ToString();
        }
        private void MainLogView_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyData = e.KeyData;
            if (keyData == Keys.F5)
            {
                this.toolStripButton_Start_Click(toolStripButton_Start, EventArgs.Empty);
                return;
            }
            if (keyData != (Keys)131159)
            {
                return;
            }
        }
        private void listView_Result_ItemActivate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.listView_Result.FocusedItem.Text))
            {
                return;
            }
            Regex regex = new Regex("\\[.+\\]##");
            Match match = regex.Match(this.listView_Result.FocusedItem.Text);
            if (!match.Success)
            {
                return;
            }
            string strA = match.Value.Substring(1, match.Value.Length - 4);
            string text = this.listView_Result.FocusedItem.Text.Substring(match.Value.Length);

        }
        private void toolStripButton_AutoScrollList_Click(object sender, EventArgs e)
        {
            this.toolStripButton_AutoScrollList.Checked = !this.toolStripButton_AutoScrollList.Checked;
            Settings.Default.TDP_UI_LogView_MainLogViewEngineLogLogView_AutoSctroll = this.toolStripButton_AutoScrollList.Checked;
        }
        private void toolStripButton_ShowSequenceDetailInfo_Click(object sender, EventArgs e)
        {
            this.toolStripButton_ShowSequenceDetailInfo.Checked = !this.toolStripButton_ShowSequenceDetailInfo.Checked;
            Settings.Default.TDP_MainLogView_ShowSequenceInfo = this.toolStripButton_ShowSequenceDetailInfo.Checked;
            this.splitter_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
            this.label_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
            this.toolStrip.SendToBack();
        }
        private void toolStripButton_EditSequence_Click(object sender, EventArgs e)
        {
            if (toolStripSplitButton_Sequence.Tag != null && toolStripSplitButton_Sequence.Tag is TdpTestSequenceInfo && SequenceSelectedToEdit != null)
            {
                SequenceSelectedToEdit(this, new SequenceSelectedToEditEventArgs((toolStripSplitButton_Sequence.Tag as TdpTestSequenceInfo).XmlFileFullName));
            }
        }

        public event SequenceSelectedToEditEventHandler SequenceSelectedToEdit;
        private void listView_Result_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (e.ColumnIndex == 0 && this.listView_Result.Columns[0].Width != 20)
            {
                this.listView_Result.Columns[0].Width = 20;
            }
        }
        private void splitter_SequenceInfo_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.TDP_MainLogView_SequenceInfoAreaHeight = this.label_SequenceInfo.Height;
        }
        private void spliterH_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.TDP_MainLogView_ListViewResultAreaHeight = this.listView_Result.Height;
        }
        public void LogMessage(LogMessageType logMessageType, string text)
        {
        }

        public void LogEngineTrace(LogEngineTraceType logEngineTraceType, string message)
        {
            
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
                return false;
            }
        }
        public TdpEdition TdpEditionAuthorization
        {
            set
            {
                switch (value)
                {
                    case TdpEdition.SuperLite:
                    case TdpEdition.Lite:
                        this.toolStripButton_ResultFolder.Enabled = (value == TdpEdition.Lite);
                        Settings.Default.TDP_MainLogView_ShowSequenceInfo = true;
                        this.toolStripButton_ShowSequenceDetailInfo.Checked = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
                        this.splitter_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
                        this.label_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
                        this.toolStripButton_ShowSequenceDetailInfo.Enabled = false;
                        this.toolStripButton_EditSequence.Visible = (value == TdpEdition.Lite);
                        return;
                    case TdpEdition.Standard:
                    case TdpEdition.Premium:
                        this.toolStripButton_ResultFolder.Enabled = true;
                        this.toolStripButton_ShowSequenceDetailInfo.Checked = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
                        this.splitter_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
                        this.label_SequenceInfo.Visible = Settings.Default.TDP_MainLogView_ShowSequenceInfo;
                        this.toolStripButton_ShowSequenceDetailInfo.Enabled = true;
                        this.toolStripButton_EditSequence.Visible = true;
                        return;
                    default:
                        return;
                }
            }
        }


        private TdpEngine engine;
        private IRunTimeVarPoolReadOnly _engineRunTimeVarPool;
        private string _logDirectory;
        private IPlanExecutor _planExecutorInstance;
        private IRunTimeVarPoolReadOnly _userRunTimeVarPool;
        private object _mdiDockPanel;
        private int _lastCastTitleIndexInListViewResult;
        public static string CurrentExecutingSequenceName = string.Empty;
        private EngineLogLogView _engineLogLogView;
        private bool _isEngineRunning;

       // private List<GraphViewEx> _userClickedOpenGraphView = new List<GraphViewEx>();

      //  private List<TableViewer> _userClickedOpenTableViewer = new List<TableViewer>();

       // private List<TreeViewer> _userClickedOpenTreeViewer = new List<TreeViewer>();

        private delegate void _setSequenceInfoAreaBackColorDelegate(Color backColor);

        private delegate void _setTipTextDelegate(string tipText);

        private delegate void _isShowTipTextDelegate(bool isShow);

        private delegate void _setTipTextForeColorDelegate(Color foreColor);

        private delegate void _setTipTextBackColorDelegate(Color backColor);

        private delegate void _setTipTextFontSizeDelegate(float fontSize);

        private delegate void _setTipTextAlignmentDelegate(ContentAlignment alignment);

        private delegate void _listViewResultSetItemIconDelegate(int index, string iconKey);

        private delegate void _listViewResultClearDelegate();

        private delegate void _listViewResultAddItemDelegate(ListViewItem lvi);

        private delegate void _listViewResultSetItemColorDelegate(int index, Color bgColor, Color foreColor);

        private delegate void _listViewResultSetItemTextDelegate(int index, int nColumn, string text);

        private delegate void EnableToolButtonDelegate(ToolStripItem tsi, bool isEnable);

        private delegate void SetToolButtonDelegate(ToolStripItem tsi, string text, string toolTip, Image image, string tagString);

        private enum SequenceInfoDisplayStyle
        {
            [EnumDisplayName("Sequence Name")]
            Name = 1,
            [EnumDisplayName("Sequence Description")]
            Description,
            [EnumDisplayName("Sequence Name & Description")]
            NameDescription
        }


    }
}
