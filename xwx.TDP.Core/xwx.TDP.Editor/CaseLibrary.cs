using System;
using System.Collections;
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
using WeifenLuo.WinFormsUI.Docking;
using xwx.TDP.Editor.Properties;
using TestManager.Utility;
using TestManager.Extern;
using TestManager.Utility.PropertyGridEx;
using System.Web;
using xwx.TDP.Editor.Misc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TestManager.Utility.Misc;

namespace xwx.TDP.Editor
{
    public partial class CaseLibrary : DockContent, ITdpModuleAuthorizationAdmin
    {
        private static readonly object padlock = new object();
        private static CaseLibrary _instance = null;
        private TdpEdition _tdpEditionAuthorization;
        private List<TreeNode> _rootTreeNodeApplications = new List<TreeNode>();
        public delegate void CaseLibraryItemLoadingEventHandler(object sender, string itemName);
        public event CaseLibraryItemLoadingEventHandler CaseLibraryItemLoading;
        private TdpEdition _tdpEeditionAuthorization;

        public static CaseLibrary Instance
        {
            get
            {
                CaseLibrary instance;
                lock (CaseLibrary.padlock)
                {
                    if (CaseLibrary._instance == null)
                    {
                        CaseLibrary._instance = new CaseLibrary();
                    }
                    instance = CaseLibrary._instance;
                }
                return instance;
            }
        }
        public CaseLibrary()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            Utilities.SetControlFont(this, Settings.Default.UI_DefaultFont);
        }

        public void RefreshCaseLibrary()
        {
            foreach (TreeNode treeNode in this._rootTreeNodeApplications)
            {
                if (treeNode.Tag != null && treeNode.Tag is CaseLibraryRootNodeInfo)
                {
                    CaseLibraryRootNodeInfo caseLibraryRootNodeInfo = treeNode.Tag as CaseLibraryRootNodeInfo;
                    if (Settings.Default.TDP_Engine_Libraries_DisabledLibrariesAbstractPath.Contains(caseLibraryRootNodeInfo.ApplicationAssembly.Location.Substring(DefaultFolderInfo.Applications_Folder.Length + 1).ToLower()))
                    {
                        if (this.treeView1.Nodes.Contains(treeNode))
                        {
                            treeNode.Remove();
                        }
                    }
                    else if (!this.treeView1.Nodes.Contains(treeNode))
                    {
                        this.treeView1.Nodes.Add(treeNode);
                    }
                }
            }
            this.treeView1_AfterSelect(this.treeView1, new TreeViewEventArgs(this.treeView1.SelectedNode));
        }

        public void ReloadCaseLibrary()
        {
            this._rootTreeNodeApplications.Clear();
            string[] directories = Directory.GetDirectories(DefaultFolderInfo.Applications_Folder);
            foreach (string path in directories)
            {
                string[] files = Directory.GetFiles(path, string.Format("xwx.TDP.Library.*{0}", ".dll"));
                foreach (string text in files)
                {
                    if (Path.GetFileNameWithoutExtension(text).Split(new char[]
                    {
                        '.'
                    }).Length == 4 && !Settings.Default.Engine_Libraries_DisabledLibrariesAbstractPath.Contains(text.Substring(DefaultFolderInfo.Applications_Folder.Length + 1).ToLower()))
                    {
                        try
                        {
                            Assembly assembly = Assembly.LoadFile(text);
                            string text2 = Path.GetFileNameWithoutExtension(assembly.Location);
                            object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                            if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is AssemblyTitleAttribute)
                            {
                                text2 = ((AssemblyTitleAttribute)customAttributes[0]).Title;
                            } //名字

                            Type[] exportedTypes = assembly.GetExportedTypes();
                            IAppHooker appHooker = null;
                            foreach (Type type in exportedTypes)
                            {
                                if (type.GetInterface(typeof(IAppHooker).FullName) != null)
                                {
                                    object obj = assembly.CreateInstance(type.FullName, true);
                                    appHooker = (obj as IAppHooker);
                                    appHooker.OnwerForm = base.ParentForm;
                                    TreeNode treeNode = new TreeNode(text2);
                                    treeNode.ImageKey = "NotChoose.png";
                                    treeNode.SelectedImageKey = "NotChoose.png";
                                    treeNode.Tag = new CaseLibraryRootNodeInfo(assembly, appHooker);
                                    foreach (Type type2 in exportedTypes)
                                    {
                                        if (type2.IsSubclassOf(typeof(CoreCase)) && !type2.IsAbstract && type2.IsVisible)
                                        {
                                            bool flag = true;
                                            customAttributes = type2.GetCustomAttributes(typeof(BrowsableAttribute), false);
                                            if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is BrowsableAttribute)
                                            {
                                                flag = ((BrowsableAttribute)customAttributes[0]).Browsable;
                                            }
                                            if (flag)
                                            {
                                                string displayName = type2.FullName;
                                                string category = "Misc";
                                                string description = "N/A";
                                                Color displayColor = SystemColors.WindowText;
                                                customAttributes = type2.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                                                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DisplayNameAttribute)
                                                {
                                                    displayName = ((DisplayNameAttribute)customAttributes[0]).DisplayName;
                                                }
                                                customAttributes = type2.GetCustomAttributes(typeof(CategoryAttribute), false);
                                                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is CategoryAttribute)
                                                {
                                                    category = ((CategoryAttribute)customAttributes[0]).Category;
                                                }
                                                customAttributes = type2.GetCustomAttributes(typeof(DescriptionAttribute), false);
                                                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DescriptionAttribute)
                                                {
                                                    description = ((DescriptionAttribute)customAttributes[0]).Description;
                                                }
                                                customAttributes = type2.GetCustomAttributes(typeof(DisplayColorAttribute), false);
                                                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is DisplayColorAttribute)
                                                {
                                                    displayColor = ((DisplayColorAttribute)customAttributes[0]).DisplayColor;
                                                }
                                                TdplCaseInfo CaseInfo = new TdplCaseInfo();
                                                CaseInfo.Category = category;
                                                CaseInfo.Description = description;
                                                CaseInfo.DisplayColor = displayColor;
                                                CaseInfo.DisplayName = displayName;
                                                CaseInfo.CaseType = type2;
                                                CaseInfo.CaseAssembly = assembly;
                                                if (CaseLibraryItemLoading != null)
                                                {
                                                    CaseLibraryItemLoading(this, string.Format("[{0}] {1}", text2, CaseInfo.DisplayName));
                                                }
                                                TreeNode treeNode2 = null;
                                                foreach (object obj2 in treeNode.Nodes)
                                                {
                                                    TreeNode treeNode3 = (TreeNode)obj2;
                                                    if (string.Equals(treeNode3.Text, CaseInfo.Category))
                                                    {
                                                        treeNode2 = treeNode3;
                                                        break;
                                                    }
                                                }
                                                if (treeNode2 == null)
                                                {
                                                    treeNode2 = treeNode.Nodes.Add(CaseInfo.Category);
                                                    treeNode2.ImageKey = "FolderClosed.png";
                                                    treeNode2.SelectedImageKey = "FolderClosed.png";
                                                }
                                                TreeNode treeNode4 = treeNode2.Nodes.Add(CaseInfo.DisplayName);
                                                treeNode4.ForeColor = CaseInfo.DisplayColor;
                                                treeNode4.ImageKey = "TestCase.png";
                                                treeNode4.SelectedImageKey = "TestCase.png";
                                                treeNode4.Tag = CaseInfo;
                                            }
                                        }
                                    }
                                    foreach (object obj3 in treeNode.Nodes)
                                    {
                                        TreeNode treeNode5 = (TreeNode)obj3;
                                        TreeNode[] array5 = new TreeNode[treeNode5.Nodes.Count];
                                        treeNode5.Nodes.CopyTo(array5, 0);
                                        Array.Sort<TreeNode>(array5, (TreeNode x, TreeNode y) => string.Compare(x.Text, y.Text));
                                        treeNode5.Nodes.Clear();
                                        treeNode5.Nodes.AddRange(array5);
                                    }
                                    TreeNode[] array6 = new TreeNode[treeNode.Nodes.Count];
                                    treeNode.Nodes.CopyTo(array6, 0);
                                    Array.Sort<TreeNode>(array6, (TreeNode x, TreeNode y) => string.Compare(x.Text, y.Text));
                                    treeNode.Nodes.Clear();
                                    treeNode.Nodes.AddRange(array6);
                                    if (treeNode.Nodes.Count != 0 || (treeNode.Tag as CaseLibraryRootNodeInfo).AppHooker.ToolStripItems.Length != 0)
                                    {
                                        this._rootTreeNodeApplications.Add(treeNode);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error: " + ex.ToString());
                        }
                        break;
                    }
                }
            }
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            this._rootTreeNodeApplications.Sort((TreeNode x, TreeNode y) => string.Compare(x.Text, y.Text));
            this.treeView1.Nodes.AddRange(this._rootTreeNodeApplications.ToArray());
            this.treeView1.EndUpdate();
            if (this.treeView1.Nodes.Count > 0 && !string.IsNullOrEmpty(Settings.Default.CaseLibrary_TreeView_LastBrowsedFullPath))
            {
                string[] array7 = Settings.Default.CaseLibrary_TreeView_LastBrowsedFullPath.Split(new string[]
                {
                    this.treeView1.PathSeparator
                }, StringSplitOptions.RemoveEmptyEntries);
                IEnumerator enumerator3 = array7.GetEnumerator();
                TreeNodeCollection nodes = this.treeView1.Nodes;
                TreeNode treeNode6 = null;
                while (nodes.Count > 0 && enumerator3.MoveNext() && enumerator3.Current != null)
                {
                    foreach (object obj4 in nodes)
                    {
                        TreeNode treeNode7 = (TreeNode)obj4;
                        if (object.Equals(treeNode7.Text, enumerator3.Current))
                        {
                            nodes = treeNode7.Nodes;
                            treeNode6 = treeNode7;
                            break;
                        }
                    }
                }
                if (treeNode6 != null)
                {
                    this.treeView1.SelectedNode = treeNode6;
                    return;
                }
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            }
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Level !=0)
            {
                e.Node.ImageKey = "FolderClosed.png";
                e.Node.SelectedImageKey = "FolderClosed.png";
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                e.Node.ImageKey = "FolderOpened.png";
                e.Node.SelectedImageKey = "FolderOpened.png";
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null) {return; }
            Settings.Default.CaseLibrary_TreeView_LastBrowsedFullPath = e.Node.FullPath;
            TreeNode treeNode = e.Node;
            while (treeNode.Level > 0) { treeNode = treeNode.Parent; }
            IAppHooker apphooker = (treeNode.Tag as CaseLibraryRootNodeInfo).AppHooker;
            if (apphooker != null)
            {
                this.toolStrip1.SuspendLayout();
                this.toolStrip1.Items.Clear();
                this.toolStrip1.Tag = null;
                foreach(ToolStripItem toolStripItem in apphooker.ToolStripItems)
                {
                    toolStripItem.DisplayStyle = (Settings.Default.CaseLibrary_ApplicationButton_IsShowText ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image);
                    if(toolStripItem.Tag !=null && toolStripItem.Tag is string)
                    {
                        string[] array = (toolStripItem.Tag as string).Split(new char[] { '|' },StringSplitOptions.RemoveEmptyEntries);
                        //string value = Array.Find<string>(array,(string s)=>string.Compare(s, this._tdpEeditionAuthorization.ToString(), true) == 0);
                        //toolStripItem.Visible = !string.IsNullOrEmpty(value);
                        //sx：根据权限设置来判断显示的内容。先注销了，让全部显示，后面再实现。
                        toolStripItem.Visible = true;
                    }
                    
                    this.toolStrip1.Items.Add(toolStripItem);
                }
                toolStrip1.ResumeLayout(true);
                toolStrip1.Tag = apphooker;
            }
            else
            {
                toolStrip1.Items.Clear();
            }
            toolStrip1.Enabled = (e.Node.Level == 0);
            if (e.Node.Level == 0)
            {
                Assembly applicationAssembly = (e.Node.Tag as CaseLibraryRootNodeInfo).ApplicationAssembly;
                string text = Path.GetFileNameWithoutExtension(applicationAssembly.Location);
                object[] customAttributes = applicationAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is AssemblyTitleAttribute)
                {
                    text = ((AssemblyTitleAttribute)customAttributes[0]).Title;
                }
                string text2 = string.Empty;
                customAttributes = applicationAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is AssemblyDescriptionAttribute)
                {
                    text2 = ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
                }
                string text3 = string.Empty;
                customAttributes = applicationAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] != null && customAttributes[0] is AssemblyCopyrightAttribute)
                {
                    text3 = ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
                }
                string text4 = applicationAssembly.GetName().Version.ToString();
                this.textBox1.Text = string.Format("{0} (V{1})\r\n\r\n{2}\r\n\r\n{3}", new object[]
                {
                    text,
                    text4,
                    text3.Replace("\n", "\r\n"),
                    text2.Replace("\n", "\r\n")
                });
                return;
            }
            if (e.Node.Tag != null && e.Node.Tag is TdplCaseInfo)
            {
                this.textBox1.Text = (e.Node.Tag as TdplCaseInfo).Description.Replace("\n", "\r\n");
                return;
            }
            this.textBox1.Text = string.Empty;
        }

        public TdpEdition TdpEditionAuthorization
        {
            set
            {
                this._tdpEditionAuthorization = value;
                switch (value)
                {
                    case TdpEdition.SuperLite:
                        for (int i = 0; i < this.treeView1.Nodes.Count; i++)
                        {
                            this.treeView1.Nodes[i].Nodes.Clear();
                        }
                        break;
                }
                this.treeView1_AfterSelect(this.treeView1, new TreeViewEventArgs(this.treeView1.SelectedNode));
            }
        }

        private void CaseLibrary_Load(object sender, EventArgs e)
        {
            this.textBox1.Height = Settings.Default.TDP_CaseLibrary_InfoWindowHeigh;
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.TDP_CaseLibrary_InfoWindowHeigh = this.textBox1.Height;
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode && (e.Item as TreeNode).Tag is TdplCaseInfo)
            {
                TdplCaseInfo data = (e.Item as TreeNode).Tag as TdplCaseInfo;
                base.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.treeView1.GetNodeAt(e.Location);
            if (nodeAt != null)
            {
                this.treeView1.SelectedNode = nodeAt;
            }
        }
    }
}
