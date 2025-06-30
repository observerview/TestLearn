using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern;
using TestManager.Extern.Interface;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.ThemeVS2015;
using xwx.TDP.Editor.Engine;
using xwx.TDP.Editor.LogViews;
using xwx.TDP.Editor.Misc;
using xwx.TDP.Editor.Properties;
using xwx.TDP.Library.BaseCase.Logger;

namespace xwx.TDP.Editor
{
    public partial class MainForm : Form
    {
        private static string _dockpanelConfigFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockManager.config");
        private SequenceEditor _sequenceEditor = new SequenceEditor();
        private SequenceSelector _sequenceSelector = new SequenceSelector();
        private CaseLibrary _caseLibrary = new CaseLibrary();
        private SplashWindow _splashWindow = new SplashWindow();
        private MainLogView _mainLogView = new MainLogView();
        private TxtLogView _txtLogView = new TxtLogView();
        private TxtResultView _txtResultView = new TxtResultView();
        private CsvResultView _csvResultView = new CsvResultView();
        private List<ILogViewEx> _userLogViewsAll = new List<ILogViewEx>();
        private List<ILogViewEx> _userLogViewsLoaded = new List<ILogViewEx>();
        public MainForm()
        {
            InitializeComponent();
            Init();

        }

        private void Init()
        {
            this.Text = Resources.TDP_MainForm_text;
            Init_UserLogView();
            Init_EngineSetting();
            Init_DockSetting();
            Init_ControlInit();
            _caseLibrary.ReloadCaseLibrary();
            _sequenceSelector.RefreshSequenceLibrary();
            _caseLibrary.CaseLibraryItemLoading += _caseLibrary_CaseLibraryItemLoading;

        }

        /// <summary>
        /// 添加用户定义的Logview，todo。目前没有写。
        /// </summary>
        private void Init_UserLogView()
        {
            int count = this.ToolStripMenuItem_View.DropDown.Items.Count;
            bool flag = false;
            string[] directories = Directory.GetDirectories(DefaultFolderInfo.Applications_Folder);
            foreach (string path in directories)
            {
                string[] files = Directory.GetFiles(path, string.Format("xwx.TDP.Library.*{0}", ".dll"));
                foreach (string path2 in files)
                {
                    if (Path.GetFileNameWithoutExtension(path2).Split(new char[]{'.'}).Length == 4)
                    {
                        try
                        {
                            this._splashWindow.SplashText = string.Format("Now loading library: {0}", Path.GetFileNameWithoutExtension(path2));
                            Assembly assembly = Assembly.LoadFile(path2);
                            this._splashWindow.SplashText = string.Format("Now analyzing library: {0}", Path.GetFileNameWithoutExtension(path2));
                            Type[] exportedTypes = assembly.GetExportedTypes();
                            IAppHooker appHooker = null;
                            foreach (Type type in exportedTypes)
                            {
                                if (type.GetInterface(typeof(IAppHooker).FullName) != null)
                                {
                                    object obj = assembly.CreateInstance(type.FullName, true);
                                    appHooker = (obj as IAppHooker);
                                    appHooker.OnwerForm = base.ParentForm;
                                    if (appHooker != null)
                                    {
                                        foreach (Type type2 in appHooker.UserDefinedLogViewsType)
                                        {
                                            object obj2 = type2.Assembly.CreateInstance(type2.ToString());
                                            if (obj2 is ILogViewEx)
                                            {
                                                this._userLogViewsAll.Add(obj2 as ILogViewEx);
                                                if (!Settings.Default.TDP_Engine_DisabledLogViewTypeFullName.Contains(type2.FullName))
                                                {
                                                    this._splashWindow.SplashText = string.Format("Now loading views: {0}", (obj2 as ILogViewEx).LogViewName);
                                                    if (obj2 is DockContent)
                                                    {
                                                        (obj2 as DockContent).HideOnClose = true;
                                                        if (obj2 is INLogger)
                                                        {
                                                            (obj2 as DockContent).CloseButton = false;
                                                            (obj2 as DockContent).CloseButtonVisible = false;
                                                        }
                                                        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem((obj2 as ILogViewEx).LogViewName, (obj2 as DockContent).Icon.ToBitmap());
                                                        toolStripMenuItem.ShortcutKeys = (obj2 as ILogViewEx).OpenViewShortcutKeys;
                                                        toolStripMenuItem.Tag = obj2;
                                                        toolStripMenuItem.Click += this.ShowUserLogViewFormMenu_Click;
                                                        if (obj2 is INLogger)
                                                        {
                                                            this.ToolStripMenuItem_Log.DropDown.Items.Add(toolStripMenuItem);
                                                            this.ToolStripMenuItem_Log.Visible = true;
                                                        }
                                                        else
                                                        {
                                                            this.ToolStripMenuItem_View.DropDown.Items.Add(toolStripMenuItem);
                                                            flag = true;
                                                        }
                                                    }
                                                    this._userLogViewsLoaded.Add(obj2 as ILogViewEx);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            //this._initWarningMessages.Add(string.Format("Load \"{0}\" failed.\n\n{1}{2}", string.Concat(new string[]
                            //{
                            //    Settings.Default.TDP_Internal_ApplicationFolder,
                            //    "\\",
                            //    new DirectoryInfo(path).Name,
                            //    "\\",
                            //    Path.GetFileName(path2)
                            //}), ex.Message, (ex.InnerException == null) ? string.Empty : ("\n" + ex.InnerException.Message)));
                        }
                        break;
                    }
                }
            }
            if (flag)
            {
                this.ToolStripMenuItem_View.DropDown.Items.Insert(count, new ToolStripSeparator());
            }
        }
        private void ShowUserLogViewFormMenu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem && (sender as ToolStripMenuItem).Tag is ILogViewEx && (sender as ToolStripMenuItem).Tag is DockContent)
            {
                ((sender as ToolStripMenuItem).Tag as ILogViewEx).ShowView();
            }
        }
        private void Init_EngineSetting()
        {
            TdpEngine.Instance.Initialize(this, _mainLogView);
            this._mainLogView.Engine = TdpEngine.Instance;
            this._mainLogView.Engine.LogViews.WindowsFont = Settings.Default.TDP_UI_DefaultFont;
            this._mainLogView.Engine.LogViews.MdiDockPanel = this.dockPanel1;
            //this._mainLogView.Engine.LogViews.AddView(this._preprocessingLogView);
            //this._mainLogView.Engine.LogViews.AddView(this._engineLogLogView);
            //this._mainLogView.Engine.LogViews.AddView(this._engineStatusLogView);
            //this._mainLogView.Engine.LogViews.AddView(this._consoleLogView);
            //this._mainLogView.Engine.LogViews.AddView(this._graphLogView);
            //foreach (ILogViewEx logViewEx in this._userLogViewsLoaded)
            //{
            //    this._splashWindow.SplashText = string.Format("Now initializing views: {0}", logViewEx.LogViewName);
            //    this._mainLogView.Engine.LogViews.AddView(logViewEx);
            //}
            //this._mainLogView.Engine.LogViews.AddView(this._rawDataLogView);
            this._mainLogView.Engine.LogViews.AddView(_mainLogView);
            this._mainLogView.Engine.LogViews.AddView(_txtLogView);
            this._mainLogView.Engine.LogViews.AddView(_txtResultView);
            this._mainLogView.Engine.LogViews.AddView(_csvResultView);
            //this._mainLogView.EngineLogLogView = this._engineLogLogView;
            //this._engineStatusLogView.EngineLogLogView = this._engineLogLogView;
        }

        private void Init_DockSetting()
        {
            this.dockPanel1.ShowDocumentIcon = true;
            this.dockPanel1.Theme = this.vS2015LightTheme1;
            if (File.Exists(_dockpanelConfigFile))
            {
                dockPanel1.LoadFromXml(_dockpanelConfigFile, new DeserializeDockContent(GetDeserializeDockContent));
            }
            else
            {
                _sequenceEditor.Show(dockPanel1, DockState.Document);
                _mainLogView.Show(dockPanel1,DockState.Document);
                _caseLibrary.Show(dockPanel1, DockState.DockRight);
                _sequenceSelector.Show(dockPanel1, DockState.DockRight);
            }
        }

        private void Init_ControlInit()
        {
            _sequenceSelector.SequenceSelectedToEdit += SequenceSelector_SequenceSelectedToEdit;
            _sequenceSelector.SequenceInfoChanging += SequenceSelector_SequenceInfoChanging;
            _sequenceSelector.SequenceInfoChanged += SequenceSelector_SequenceInfoChanged;
            _mainLogView.SequenceInfoTextFont = Settings.Default.TDP_MainLogView_SequenceInfoTextFont;
            _mainLogView.SequenceInfoAresBackgroundColor = Color.LightYellow;
            _mainLogView.SequenceSelectedToEdit += this.SequenceSelector_SequenceSelectedToEdit;
            //base.Location = Settings.Default.TDP_MainForm_Location;
            //base.Size = Settings.Default.TDP_MainForm_Size;
            //MainForm.FinderDialoger.Owner = this;
            //MainForm.FinderDialoger.BeginFind += delegate (object sender, FinderBeginFindEventArgs e)
            //{
            //    if (this._lastActivedFinderServer != null && this._lastActivedFinderServer != null)
            //    {
            //        this._lastActivedFinderServer.OnFinderBeginFind(this.dockPanel.ActiveContent, e);
            //    }
            //};
            //this.dockPanel1.ActiveContentChanged += delegate (object sender, EventArgs e)
            //{
            //    if (this.dockPanel1.ActiveContent != null)
            //    {
            //        if (this.dockPanel1.ActiveContent is IFinderServer)
            //        {
            //            MainForm.FinderDialoger.EnableFind = true;
            //            MainForm.FinderDialoger.SupportMatchWholeWord = (this.dockPanel1.ActiveContent as IFinderServer).FinderSupportMatchWholeWord;
            //            this._lastActivedFinderServer = (this.dockPanel1.ActiveContent as IFinderServer);
            //            return;
            //        }
            //        if (this.dockPanel1.ActiveContent != MainForm.FinderDialoger)
            //        {
            //            MainForm.FinderDialoger.EnableFind = false;
            //        }
            //    }
            //};
        }
        public IDockContent GetDeserializeDockContent(string persistString)
        {
            if (persistString == typeof(SequenceEditor).ToString())
                return _sequenceEditor;
            if (persistString == typeof(CaseLibrary).ToString())
                return _caseLibrary;
            if (persistString == typeof(SequenceSelector).ToString())
                return _sequenceSelector;
            if(persistString == typeof(MainLogView).ToString()) 
                return _mainLogView;
            return null;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel1.SaveAsXml(_dockpanelConfigFile);
        }

        private void _caseLibrary_CaseLibraryItemLoading(object sender, string itemName)
        {
            this._splashWindow.SplashText = string.Format("Now loading case library: {0}", itemName);
        }

        private void SequenceSelector_SequenceInfoChanged(object sender, SequenceInfoChangedEventArgs e)
        {
            if (string.Compare(e.SequenceXmlFileName, SequenceEditor.CurrentEditSequenceFile, true) == 0)
            {
                this._sequenceEditor.SequenceXmlFileName = e.SequenceXmlFileName;
            }
        }
        private void SequenceSelector_SequenceInfoChanging(object sender, SequenceInfoChangingEventArgs e)
        {
            if (string.Compare(e.SequenceXmlFileName, SequenceEditor.CurrentEditSequenceFile, true) == 0 && this._sequenceEditor.IsNeedSave)
            {
                this._sequenceEditor.SaveSequence();
            }
        }
        private void SequenceSelector_SequenceSelectedToEdit(object sender, SequenceSelectedToEditEventArgs e)
        {
            _sequenceEditor.SequenceXmlFileName = e.SequenceXmlFileName;
            _sequenceEditor.Show(dockPanel1);
        }

        private void sequenceExecutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sequenceEditor.Show();
        }

        private void 序列执行器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mainLogView.Show();
        }

        private void 测试CaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _caseLibrary.Show();
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sequenceSelector.Show();
        }
    }
}
