using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using TestManager.Utility;
using TestManager.Utility.Misc;
using TestManager.Utility.GenericForm;
using System.IO;
using xwx.TDP.Editor.Engine;
using xwx.TDP.Editor.Properties;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Editor.LogViews;
using xwx.TDP.Editor.Misc;
using System.Diagnostics;

namespace xwx.TDP.Editor
{
    public partial class SequenceSelector : DockContent, ITdpModuleAuthorizationAdmin
    {
        public event SequenceSelectedToEditEventHandler SequenceSelectedToEdit;
        public event SequenceInfoChangingEventHandler SequenceInfoChanging;
        public event SequenceInfoChangedEventHandler SequenceInfoChanged;
        public delegate void SequenceLibraryItemLoadingEventHandler(object sender, string itemName);
        public event SequenceLibraryItemLoadingEventHandler SequenceLibraryItemLoading;
        private bool _canCreateDeleteNewFolderAndSequence = true;

        public TdpEdition TdpEditionAuthorization 
        {
            set
            {
                switch (value)
                {
                    case TdpEdition.SuperLite:
                        base.Enabled = false;
                        this._canCreateDeleteNewFolderAndSequence = false;
                        break;
                    case TdpEdition.Lite:
                        base.Enabled = true;
                        this._canCreateDeleteNewFolderAndSequence = false;
                        break;
                    case TdpEdition.Standard:
                    case TdpEdition.Premium:
                        base.Enabled = true;
                        this._canCreateDeleteNewFolderAndSequence = true;
                        break;
                }
                this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
            }
        }

        public SequenceSelector()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            Utilities.SetControlFont(this,Properties.Settings.Default.UI_DefaultFont);
            this.fileSystemWatcher.Filter = "*.*";
            this.fileSystemWatcher.Path = DefaultFolderInfo.SequenceLibrary_Folder;
        }

        public void RefreshSequenceLibrary()
        {
            TreeNode treeNode = null;
            this.RefreshSequenceLibrary(DefaultFolderInfo.SequenceLibrary_Folder, out treeNode);
            this.treeView.BeginUpdate();
            this.treeView.Nodes.Clear();
            treeNode.Expand();
            this.treeView.Nodes.Add(treeNode);
            this.treeView.SelectedNode = treeNode;
            this.treeView.EndUpdate();
        }
        private void RefreshSequenceLibrary(string folderPath, out TreeNode treeNode)
        {
            string text = new DirectoryInfo(folderPath).Name;
            text = text.Substring(0, 1).ToUpper() + text.Substring(1);
            treeNode = new TreeNode(text);
            treeNode.Name = folderPath;
            treeNode.ImageKey = "FolderClosed.png";
            treeNode.SelectedImageKey = "FolderClosed.png";
            treeNode.Tag = folderPath;
            string[] files = Directory.GetFiles(folderPath, "*.tdpts");
            string[] directories = Directory.GetDirectories(folderPath);
            foreach (string folderPath2 in directories)
            {
                TreeNode node = new TreeNode();
                this.RefreshSequenceLibrary(folderPath2, out node);
                treeNode.Nodes.Add(node);
            }
            foreach (string text2 in files)
            {
                TdpTestSequenceInfo tdpTestSequenceInfo = new TdpTestSequenceInfo();
                string empty = string.Empty;
                if (TdpTestSequence.GetTdpSequenceInfo(text2, out tdpTestSequenceInfo, out empty))
                {
                    if (SequenceLibraryItemLoading != null)
                    {
                        SequenceLibraryItemLoading(this, string.Format("{0}\\{1}", 
                            folderPath.Substring(DefaultFolderInfo.SequenceLibrary_Folder.Length), 
                            Path.GetFileName(text2)).Trim(new char[]{ '\\'}));
                    }
                    TreeNode treeNode2 = treeNode.Nodes.Add(tdpTestSequenceInfo.DisplayName);
                    treeNode2.Name = text2;
                    treeNode2.ImageKey = "Sequence.png";
                    treeNode2.SelectedImageKey = "Sequence.png";
                    treeNode2.Tag = tdpTestSequenceInfo;
                }
            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.TDP_SequenceLibrary_InfoWindowHeigh = textBox1.Height;
            Settings.Default.Save();
        }

        private void SequenceSelector_Load(object sender, EventArgs e)
        {
            textBox1.Height = Settings.Default.TDP_SequenceLibrary_InfoWindowHeigh;
        }

        private void SequenceSelector_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyData = e.KeyCode;
            if (keyData != Keys.Delete)
            {
                return;
            }
            SelectedNode_Delete();
        }
       

        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            TreeNode[] array = this.treeView.Nodes.Find(e.OldFullPath, true);
            foreach (TreeNode treeNode in array)
            {
                this.RefreshSubNodeInfo(treeNode, e.FullPath);
            }
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
        }
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            TreeNode[] array = this.treeView.Nodes.Find(e.FullPath, true);
            foreach (TreeNode treeNode in array)
            {
                this.RefreshSubNodeInfo(treeNode, e.FullPath);
            }
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
        }
        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            TreeNode[] array = this.treeView.Nodes.Find(e.FullPath, true);
            foreach (TreeNode treeNode in array)
            {
                if (treeNode.Parent != null && treeNode.Parent.Nodes.Count == 1)
                {
                    treeNode.Parent.ImageKey = "FolderClosed.png";
                    treeNode.Parent.SelectedImageKey = "FolderClosed.png";
                }
                treeNode.Remove();
            }
            this.treeView_AfterSelect(this.treeView, new TreeViewEventArgs(this.treeView.SelectedNode));
        }
        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {

        }

        private void toolStripButton1_NewFolder_Click(object sender, EventArgs e)
        {
            SelectedNode_NewFolder();
        }

        private void toolStripButton1_Edit_Click(object sender, EventArgs e)
        {
            SelectedNode_Edit();
        }
        private void toolStripButton1_NewSequence_Click(object sender, EventArgs e)
        {
            SelectedNode_NewSequence();
        }
        private void toolStripButton1_Delete_Click(object sender, EventArgs e)
        {
            SelectedNode_Delete();
        }
        private void toolStripButton1_Modify_Click(object sender, EventArgs e)
        {
            this.SelectedNode_Modify();
        }

        #region Functions
        private void SelectedNode_Modify()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null && this.treeView.SelectedNode.Level != 0)
            {
                if (this.treeView.SelectedNode.Tag is string && Directory.Exists(this.treeView.SelectedNode.Tag as string))
                {
                    if (SequenceEditor.CurrentEditSequenceFile.StartsWith(this.treeView.SelectedNode.Tag as string, true, null) && SequenceEditor.CurrentEditSequenceFile[(this.treeView.SelectedNode.Tag as string).Length] == '\\')
                    {
                        MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceSelector_FolderCanNotBeDeletedWhileSequenceInEdit + "\n\n" + SequenceEditor.CurrentEditSequenceFile.Substring(DefaultFolderInfo.SequenceLibrary_Folder.Length + 1), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    string text = this.treeView.SelectedNode.Text;
                    if (MessageBoxEx.ShowPrompt(base.MdiParent, Resources.LNG_TDP_SequenceSelector_RenamedFolderName, this.Text, ref text) != DialogResult.OK || string.Compare(text, this.treeView.SelectedNode.Text, true) == 0)
                    {
                        return;
                    }
                    try
                    {
                        string text2 = this.treeView.SelectedNode.Tag as string;
                        string text3 = string.Format("{0}\\{1}", Directory.GetParent(this.treeView.SelectedNode.Tag as string).FullName, text);
                        Directory.Move(this.treeView.SelectedNode.Tag as string, text3);
                        if (this.SequenceInfoChanging != null && SequenceEditor.CurrentEditSequenceFile.StartsWith(text2, true, null) && SequenceEditor.CurrentEditSequenceFile[text2.Length] == '\\')
                        {
                            SequenceEditor.CurrentEditSequenceFile = Path.Combine(text3, SequenceEditor.CurrentEditSequenceFile.Substring(text2.Length).TrimStart(new char[]
                            {
                                '\\'
                            }));
                            this.SequenceInfoChanging(this, new SequenceInfoChangingEventArgs(SequenceEditor.CurrentEditSequenceFile));
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_RenamedFolderFailed + " {0}\n\n{1}", (this.treeView.SelectedNode.Tag as string).Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1), ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (this.treeView.SelectedNode.Tag is TdpTestSequenceInfo)
                {
                    SequenceInfoEditor sequenceInfoEditor = new SequenceInfoEditor();
                    sequenceInfoEditor.EditMode = true;
                    //if (!string.IsNullOrEmpty(MainLogView.CurrentExecutingSequenceName) && string.Compare(MainLogView.CurrentExecutingSequenceName, (this.treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName, true) == 0)
                    //{
                    //    sequenceInfoEditor.CanEditSequenceName = false;
                    //}
                    sequenceInfoEditor.SequenceInfo = (this.treeView.SelectedNode.Tag as TdpTestSequenceInfo);
                    bool flag;
                    do
                    {
                        flag = false;
                        if (sequenceInfoEditor.ShowDialog(base.MdiParent) == DialogResult.OK)
                        {
                            if (this.SequenceInfoChanging != null)
                            {
                                this.SequenceInfoChanging(this, new SequenceInfoChangingEventArgs(sequenceInfoEditor.SequenceInfo.XmlFileFullName));
                            }
                            TdpTestSequence tsdpTestSequence = new TdpTestSequence();
                            tsdpTestSequence.LoadFromFile(sequenceInfoEditor.SequenceInfo.XmlFileFullName);
                            tsdpTestSequence.SequenceInfo = sequenceInfoEditor.SequenceInfo;
                            tsdpTestSequence.SequenceInfo.ModifiedTime = DateTime.Now;
                            if (!tsdpTestSequence.WriteToFile(tsdpTestSequence.SequenceInfo.XmlFileFullName))
                            {
                                MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_ModifySequenceFailed + "\n\n{0}", tsdpTestSequence.ErrorMessage), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                flag = true;
                            }
                            else
                            {
                                this.treeView.SelectedNode.Text = tsdpTestSequence.SequenceInfo.DisplayName;
                                this.treeView.SelectedNode.Tag = tsdpTestSequence.SequenceInfo;
                                if (this.SequenceInfoChanged != null)
                                {
                                    this.SequenceInfoChanged(this, new SequenceInfoChangedEventArgs(sequenceInfoEditor.SequenceInfo.XmlFileFullName));
                                }
                            }
                        }
                    }
                    while (flag);
                }
                return;
            }
        }
        private void SelectedNode_NewSequence()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                if (this.treeView.SelectedNode.Tag is string && Directory.Exists(this.treeView.SelectedNode.Tag as string))
                {
                    SequenceInfoEditor sequenceInfoEditor = new SequenceInfoEditor();
                    sequenceInfoEditor.EditMode = true;
                    bool flag;
                    do
                    {
                        flag = false;
                        if (sequenceInfoEditor.ShowDialog(base.MdiParent) == DialogResult.OK)
                        {
                            TdpTestSequence tdpTestSequence = new TdpTestSequence();
                            tdpTestSequence.SequenceInfo = sequenceInfoEditor.SequenceInfo;
                            tdpTestSequence.SequenceInfo.CreatedTime = DateTime.Now;
                            string text = string.Format("{0}\\{1}{2}", this.treeView.SelectedNode.Tag as string, tdpTestSequence.SequenceInfo.DisplayName, ".tdpts");
                            if (File.Exists(text))
                            {
                                MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_SequenceExistedWarning + "\n\n{0}", text.Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1)), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                flag = true;
                            }
                            else if (!tdpTestSequence.WriteToFile(text))
                            {
                                MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_CreateSequenceFailed + "\n\n{0}", tdpTestSequence.ErrorMessage), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                flag = true;
                            }
                            else
                            {
                                TreeNode treeNode = this.treeView.SelectedNode.Nodes.Add(tdpTestSequence.SequenceInfo.DisplayName);
                                treeNode.Name = text;
                                treeNode.ImageKey = "Sequence.png";
                                treeNode.SelectedImageKey = "Sequence.png";
                                treeNode.Tag = tdpTestSequence.SequenceInfo;
                                this.treeView.SelectedNode.Expand();
                            }
                        }
                    }
                    while (flag);
                }
                return;
            }
        }
        private void SelectedNode_Edit()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                if (treeView.SelectedNode.Tag is TdpTestSequenceInfo &&  SequenceSelectedToEdit != null)
                {
                    SequenceSelectedToEdit(this, new SequenceSelectedToEditEventArgs((treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName));
                }
                return;
            }
        }
        private void SelectedNode_NewFolder()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                if (this.treeView.SelectedNode.Tag is string && Directory.Exists(this.treeView.SelectedNode.Tag as string))
                {
                    string empty = string.Empty;
                    if (MessageBoxEx.ShowPrompt(base.MdiParent, Resources.LNG_TDP_SequenceSelector_CreatedFolderName, this.Text, ref empty) == DialogResult.OK)
                    {
                        string text = string.Format("{0}\\{1}", this.treeView.SelectedNode.Tag as string, empty);
                        if (!Directory.Exists(text))
                        {
                            try
                            {
                                Directory.CreateDirectory(text);
                                TreeNode treeNode = this.treeView.SelectedNode.Nodes.Insert(0, empty);
                                treeNode.Name = text;
                                treeNode.ImageKey = "FolderClosed.png";
                                treeNode.SelectedImageKey = "FolderClosed.png";
                                treeNode.Tag = text;
                                this.treeView.SelectedNode.Expand();
                            }
                            catch (Exception ex)
                            {
                                MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_CreateFolderFailed + "\n{0}\n\n{1}", text.Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1), ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
                return;
            }
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                if (e.Node.Tag is TdpTestSequenceInfo)
                {
                    TdpTestSequenceInfo tsdpTestSequenceInfo = e.Node.Tag as TdpTestSequenceInfo;
                    this.textBox1.Text = string.Format(Resources.LNG_TDP_SequenceSelector_SequenceInfo.Replace("/t", "\t"), new object[]
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
                    this.toolStripButton1_NewFolder.Enabled = false;
                    this.toolStripButton1_NewSequence.Enabled = false;
                    this.toolStripButton1_Delete.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.toolStripButton1_Modify.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.toolStripButton1_Edit.Enabled = true;
                    return;
                }
                if (e.Node.Tag is string && Directory.Exists(e.Node.Tag as string))
                {
                    this.textBox1.Text = string.Empty;
                    this.toolStripButton1_NewFolder.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.toolStripButton1_NewSequence.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.toolStripButton1_Delete.Enabled = (e.Node.Level != 0 && this._canCreateDeleteNewFolderAndSequence);
                    this.toolStripButton1_Modify.Enabled = (e.Node.Level != 0 && this._canCreateDeleteNewFolderAndSequence);
                    this.toolStripButton1_Edit.Enabled = false;
                    return;
                }
                this.toolStripButton1_NewFolder.Enabled = false;
                this.toolStripButton1_NewSequence.Enabled = false;
                this.toolStripButton1_Delete.Enabled = false;
                this.toolStripButton1_Modify.Enabled = false;
                this.toolStripButton1_Edit.Enabled = false;
            }
        }
        private void RefreshSubNodeInfo(TreeNode treeNode, string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                if (Directory.Exists(fullPath))
                {
                    treeNode.Text = new DirectoryInfo(fullPath).Name;
                    treeNode.Name = fullPath;
                    treeNode.Tag = fullPath;
                    foreach (object obj in treeNode.Nodes)
                    {
                        TreeNode treeNode2 = (TreeNode)obj;
                        string text = string.Empty;
                        if (treeNode2.Tag != null)
                        {
                            if (treeNode2.Tag is string)
                            {
                                text = Path.Combine(fullPath, new DirectoryInfo(treeNode2.Tag as string).Name);
                            }
                            else if (treeNode2.Tag is TdpTestSequenceInfo)
                            {
                                text = Path.Combine(fullPath, Path.GetFileName((treeNode2.Tag as TdpTestSequenceInfo).XmlFileFullName));
                            }
                            if (!string.IsNullOrEmpty(text))
                            {
                                this.RefreshSubNodeInfo(treeNode2, text);
                            }
                        }
                    }
                }
                return;
            }
            TdpTestSequenceInfo tsdpTestSequenceInfo = new TdpTestSequenceInfo();
            string empty = string.Empty;
            if (TdpTestSequence.GetTdpSequenceInfo(fullPath, out tsdpTestSequenceInfo, out empty))
            {
                treeNode.Text = tsdpTestSequenceInfo.DisplayName;
                treeNode.Name = fullPath;
                treeNode.ImageKey = "Sequence.png";
                treeNode.SelectedImageKey = "Sequence.png";
                treeNode.Tag = tsdpTestSequenceInfo;
                return;
            }
            treeNode.Remove();
        }
        private void SelectedNode_Delete()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                if (this.treeView.SelectedNode.Tag is TdpTestSequenceInfo)
                {
                    //if (!string.IsNullOrEmpty(MainLogView.CurrentExecutingSequenceName) && string.Compare(MainLogView.CurrentExecutingSequenceName, (this.treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName, true) == 0)
                    //{
                    //    MessageBoxEx.Show(base.MdiParent, Resources.LNG_TDP_SequenceSelector_SequenceCanNotDeleteWhileRunning, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}
                    if (MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_DeleteSequenceConfirm + "\n\n{0}\n{1}", (this.treeView.SelectedNode.Tag as TdpTestSequenceInfo).DisplayName, (this.treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName.Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1)), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        File.Delete((this.treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName);
                        return;
                    }
                }
                else if (this.treeView.SelectedNode.Tag is string 
                    && Directory.Exists(this.treeView.SelectedNode.Tag as string) 
                    && this.treeView.SelectedNode.Level != 0 
                    && MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_DeleteFolderConfirm + "\n{0}", (this.treeView.SelectedNode.Tag as string).Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1)), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes 
                    && MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_DeleteFolderConfirm + "\n{0}", (this.treeView.SelectedNode.Tag as string).Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1)), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        Directory.Delete(this.treeView.SelectedNode.Tag as string, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_DeleteFolderFailed + "\n{0}\n\n{1}", (this.treeView.SelectedNode.Tag as string).Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1), ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                return;
            }
        }

        #endregion

        private void toolStripButton1_RefreshFolder_Click(object sender, EventArgs e)
        {
            SelectedNode_RefreshFolder();
        }
        private void SelectedNode_RefreshFolder()
        {
            this.RefreshSequenceLibrary();
        }

        private void toolStripButton1_Browser_Click(object sender, EventArgs e)
        {
            if (this.treeView.SelectedNode != null)
            {
                string arg = DefaultFolderInfo.SequenceLibrary_Folder;
                if (this.treeView.SelectedNode.Level != 0 && this.treeView.SelectedNode.Tag != null)
                {
                    if (this.treeView.SelectedNode.Tag is string)
                    {
                        arg = (this.treeView.SelectedNode.Tag as string);
                    }
                    if (this.treeView.SelectedNode.Tag is TdpTestSequenceInfo)
                    {
                        arg = Path.GetDirectoryName((this.treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName);
                    }
                }
                Process.Start(string.Format("\"{0}\"", arg));
            }
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageKey = "FolderClosed.png";
            e.Node.SelectedImageKey = "FolderClosed.png";
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageKey = "FolderOpened.png";
            e.Node.SelectedImageKey = "FolderOpened.png";
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                return;
            }
            if (e.Node.Tag is TdpTestSequenceInfo)
            {
                e.CancelEdit = true;
                return;
            }
            if (e.Node.Tag is string && Directory.Exists(e.Node.Tag as string))
            {
                try
                {
                    Directory.Move(e.Node.Tag as string, string.Format("{0}\\{1}", Directory.GetParent(e.Node.Tag as string).FullName, e.Label));
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show(base.MdiParent, string.Format(Resources.LNG_TDP_SequenceSelector_RenamedFolderFailed + "\n{0}\n\n{1}", (e.Node.Tag as string).Remove(0, DefaultFolderInfo.SequenceLibrary_Folder.Length + 1), ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Node.Tag != null)
            {
                if (e.Node.Tag is TdpTestSequenceInfo)
                {
                    this.newFolderToolStripMenuItem.Enabled = false;
                    this.newTestSequenceToolStripMenuItem.Enabled = false;
                    this.newTestSequenceToolStripMenuItem.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.modifyToolStripMenuItem.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.editToolStripMenuItem.Enabled = true;
                    this.contextMenuStrip.Show(this.treeView, e.Location);
                }
                if (e.Node.Tag is string && Directory.Exists(e.Node.Tag as string))
                {
                    this.newFolderToolStripMenuItem.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.newTestSequenceToolStripMenuItem.Enabled = this._canCreateDeleteNewFolderAndSequence;
                    this.newTestSequenceToolStripMenuItem.Enabled = (e.Node.Level != 0 && this._canCreateDeleteNewFolderAndSequence);
                    this.modifyToolStripMenuItem.Enabled = (e.Node.Level != 0 && (!SequenceEditor.CurrentEditSequenceFile.StartsWith(e.Node.Tag as string, true, null) || SequenceEditor.CurrentEditSequenceFile[(e.Node.Tag as string).Length] != '\\') && this._canCreateDeleteNewFolderAndSequence);
                    this.editToolStripMenuItem.Enabled = false;
                    this.contextMenuStrip.Show(this.treeView, e.Location);
                }
            }
        }

        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = treeView.GetNodeAt(e.Location);
            if(e.Button == MouseButtons.Left && nodeAt.Tag != null && nodeAt.Tag is TdpTestSequenceInfo &&SequenceSelectedToEdit != null)
            {
                SequenceSelectedToEdit(this, new SequenceSelectedToEditEventArgs((nodeAt.Tag as TdpTestSequenceInfo).XmlFileFullName));
            }
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.treeView.GetNodeAt(e.Location);
            if (nodeAt != null)
            {
                treeView.SelectedNode = nodeAt;
            }
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode && (e.Item as TreeNode).Level != 0)
            {
                if ((e.Item as TreeNode).Tag is TdpTestSequenceInfo)
                {
                    TdpTestSequenceInfo data = (e.Item as TreeNode).Tag as TdpTestSequenceInfo;
                    base.DoDragDrop(data, DragDropEffects.Copy);
                    return;
                }
                if ((e.Item as TreeNode).Tag is string)
                {
                    Directory.Exists((e.Item as TreeNode).Tag as string);
                }
            }
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyData = e.KeyData;
            if (keyData != Keys.Return)
            {
                return;
            }
            if (treeView.SelectedNode != null && treeView.SelectedNode.Tag != null && treeView.SelectedNode.Tag is TdpTestSequenceInfo && this.SequenceSelectedToEdit != null)
            {
                SequenceSelectedToEdit(this, new SequenceSelectedToEditEventArgs((treeView.SelectedNode.Tag as TdpTestSequenceInfo).XmlFileFullName));
            }
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode_NewFolder();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode_Delete();
        }

        private void newTestSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode_NewSequence();
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode_Modify();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode_Edit();
        }
    }
}
