using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TestManager.Extern;
using xwx.TDP.Editor.Properties;

namespace xwx.TDP.Editor.Engine
{
    internal class TdpTestSequence
    {
        private TdpTestSequenceInfo _sequenceInfo = new TdpTestSequenceInfo();
        private List<TdpTestCase> _tdpTestCases = new List<TdpTestCase>();
        private string _errorMessage;
        private ExecutionStatus _executionStatus = new ExecutionStatus();
        private bool _isHasReadingParameterOrLimit2XmlWarning;
        public TdpTestSequenceInfo SequenceInfo
        {
            get
            {
                return this._sequenceInfo;
            }
            set
            {
                this._sequenceInfo = value;
            }
        }
        public List<TdpTestCase> TdpTestCases
        {
            get
            {
                return this._tdpTestCases;
            }
            set
            {
                this._tdpTestCases = value;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
        }
        public ExecutionStatus ExecutionStatus
        {
            get
            {
                return this._executionStatus;
            }
        }
        public bool IsHasReadingParameterOrLimit2XmlWarning
        {
            get
            {
                return this._isHasReadingParameterOrLimit2XmlWarning;
            }
        }
        public bool LoadFromFile(string xmlFileFullName)
        {
            this._isHasReadingParameterOrLimit2XmlWarning = false;
            XmlDocument xmlDocument = new XmlDocument();
            bool result;
            try
            {
                xmlDocument.Load(xmlFileFullName);
                result = this.LoadFromXmlDocument(xmlDocument);
            }
            catch (Exception ex)
            {
                this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TdpTestSequence_LoadSequenceFailed + "\n\n{0}", ex.Message);
                result = false;
            }
            return result;
        }
        private bool LoadFromXmlDocument(XmlDocument xmlDocument)
        {
            try
            {
                XmlNode documentElement = xmlDocument.DocumentElement;
                this._sequenceInfo.Author = documentElement.SelectSingleNode("descendant::Author").InnerText;
                this._sequenceInfo.DisplayName = documentElement.SelectSingleNode("descendant::Name").InnerText;
                this._sequenceInfo.Description = documentElement.SelectSingleNode("descendant::Description").InnerText;
                this._sequenceInfo.CreatedTime = DateTime.Parse(documentElement.SelectSingleNode("descendant::CreatedTime").InnerText);
                this._sequenceInfo.ModifiedTime = DateTime.Parse(documentElement.SelectSingleNode("descendant::ModifiedTime").InnerText);
                XmlNode xmlNode = documentElement.SelectSingleNode("descendant::GlobeEngineMode");
                this._sequenceInfo.ExecutTimes = uint.Parse(xmlNode.Attributes["executedTimes"].Value);
                xmlNode = documentElement.SelectSingleNode("descendant::GlobeEngineMode/ErrorMode");
                this._sequenceInfo.GlobeEngineMode.Error = (EngineMode_Error)Enum.Parse(typeof(EngineMode_Error), xmlNode.Attributes["mode"].Value, true);
                this._sequenceInfo.GlobeEngineMode.ErrorRetry = uint.Parse(xmlNode.Attributes["retry"].Value);
                xmlNode = documentElement.SelectSingleNode("descendant::GlobeEngineMode/FailMode");
                this._sequenceInfo.GlobeEngineMode.OK = (EngineMode_OK)Enum.Parse(typeof(EngineMode_OK), xmlNode.Attributes["mode"].Value, true);
                this._sequenceInfo.GlobeEngineMode.OkRetry = uint.Parse(xmlNode.Attributes["retry"].Value);
                XmlNode xmlNode2 = documentElement.SelectSingleNode("descendant::CaseSequence");
                this._tdpTestCases.Clear();
                foreach (object obj in xmlNode2.ChildNodes)
                {
                    XmlNode caseNode = (XmlNode)obj;
                    TdpTestCase item = null;
                    if (!this.XmlCaseNode2TdpTestCase(caseNode, out item))
                    {
                        return false;
                    }
                    this._tdpTestCases.Add(item);
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TdpTestSequence_LoadSequenceFailed + "\n\n{0}", ex.Message);
                return false;
            }
            return true;
        }
        private bool XmlCaseNode2TdpTestCase(XmlNode caseNode, out TdpTestCase tdpTestCase)
        {
            tdpTestCase = new TdpTestCase();
            try
            {
                TestCaseInfo testCaseInfo = new TestCaseInfo();
                testCaseInfo.AssemblyName = caseNode.Attributes["dllName"].Value;
                testCaseInfo.AssemblyVersion = new Version(caseNode.Attributes["dllVersion"].Value);
                testCaseInfo.TestCaseFullName = caseNode.Attributes["fullName"].Value;
                XmlNode xmlNode = caseNode.SelectSingleNode("descendant::EngineMode");
                testCaseInfo.CaseEngineMode.Type = (EngineType)Enum.Parse(typeof(EngineType), xmlNode.Attributes["type"].Value, true);
                xmlNode = caseNode.SelectSingleNode("descendant::EngineMode/ErrorMode");
                testCaseInfo.CaseEngineMode.Error = (EngineMode_Error)Enum.Parse(typeof(EngineMode_Error), xmlNode.Attributes["mode"].Value, true);
                testCaseInfo.CaseEngineMode.ErrorRetry = uint.Parse(xmlNode.Attributes["retry"].Value);
                xmlNode = caseNode.SelectSingleNode("descendant::EngineMode/FailMode");
                testCaseInfo.CaseEngineMode.OK = (EngineMode_OK)Enum.Parse(typeof(EngineMode_OK), xmlNode.Attributes["mode"].Value, true);
                testCaseInfo.CaseEngineMode.OkRetry = uint.Parse(xmlNode.Attributes["retry"].Value);
                xmlNode = caseNode.SelectSingleNode("descendant::Limits");
                foreach (object obj in xmlNode.ChildNodes)
                {
                    XmlNode xmlNode2 = (XmlNode)obj;
                    testCaseInfo.Limits.Add(xmlNode2.Attributes["name"].Value, xmlNode2.InnerText);
                }
                xmlNode = caseNode.SelectSingleNode("descendant::Parameters");
                foreach (object obj2 in xmlNode.ChildNodes)
                {
                    XmlNode xmlNode3 = (XmlNode)obj2;
                    testCaseInfo.Parameters.Add(xmlNode3.Attributes["name"].Value, xmlNode3.InnerText);
                }
                tdpTestCase.SelfTestCase = new TestCase(testCaseInfo);
                if (tdpTestCase.SelfTestCase.CaseInstance == null)
                {
                    this._errorMessage = tdpTestCase.SelfTestCase.ErrorMessage;
                    return false;
                }
                if (tdpTestCase.SelfTestCase.ErrorCode != TestCase.TestCaseErrorCode.OK)
                {
                    if (tdpTestCase.SelfTestCase.ErrorCode != TestCase.TestCaseErrorCode.SetCaseLimitsError)
                    {
                        if (tdpTestCase.SelfTestCase.ErrorCode != TestCase.TestCaseErrorCode.SetCaseParametersError)
                        {
                            this._errorMessage = tdpTestCase.SelfTestCase.ErrorMessage;
                            return false;
                        }
                    }
                    this._isHasReadingParameterOrLimit2XmlWarning = true;
                }
                xmlNode = caseNode.SelectSingleNode("descendant::ContainedCases");
                foreach (object obj3 in xmlNode.ChildNodes)
                {
                    XmlNode caseNode2 = (XmlNode)obj3;
                    TdpTestCase item = null;
                    if (!this.XmlCaseNode2TdpTestCase(caseNode2, out item))
                    {
                        return false;
                    }
                    tdpTestCase.ContainedTdpTestCase.Add(item);
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = string.Format("Load TDP test case failed.\n\n{0}", ex.Message);
                return false;
            }
            return true;
        }
        public bool WriteToFile(string xmlFileFullName)
        {
            bool result = false;
            XmlDocument xmlDocument = new XmlDocument();
            result = this.WriteToXmlDocument(ref xmlDocument);
            try
            {
                xmlDocument.Save(xmlFileFullName);
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private bool WriteToXmlDocument(ref XmlDocument xmlDocument)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
            try
            {
                XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "TSDP_TS", string.Empty);
                xmlNode.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Name", string.Empty)).InnerText = this._sequenceInfo.DisplayName;
                xmlNode.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Author", string.Empty)).InnerText = this._sequenceInfo.Author;
                xmlNode.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "CreatedTime", string.Empty)).InnerText = this._sequenceInfo.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                xmlNode.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "ModifiedTime", string.Empty)).InnerText = this._sequenceInfo.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss");
                xmlNode.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "Description", string.Empty)).InnerText = this._sequenceInfo.Description;
                XmlNode xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "GlobeEngineMode", string.Empty);
                XmlAttribute node;
                (node = xmlDocument.CreateAttribute("executedTimes")).Value = this._sequenceInfo.ExecutTimes.ToString();
                xmlNode2.Attributes.Append(node);
                XmlNode xmlNode3 = xmlNode2.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "ErrorMode", string.Empty));
                (node = xmlDocument.CreateAttribute("mode")).Value = this._sequenceInfo.GlobeEngineMode.Error.ToString();
                xmlNode3.Attributes.Append(node);
                (node = xmlDocument.CreateAttribute("retry")).Value = this._sequenceInfo.GlobeEngineMode.ErrorRetry.ToString();
                xmlNode3.Attributes.Append(node);
                xmlNode3 = xmlNode2.AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "FailMode", string.Empty));
                (node = xmlDocument.CreateAttribute("mode")).Value = this._sequenceInfo.GlobeEngineMode.OK.ToString();
                xmlNode3.Attributes.Append(node);
                (node = xmlDocument.CreateAttribute("retry")).Value = this._sequenceInfo.GlobeEngineMode.OkRetry.ToString();
                xmlNode3.Attributes.Append(node);
                xmlNode.AppendChild(xmlNode2);
                XmlNode xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "CaseSequence", string.Empty);
                foreach (TdpTestCase tdpTestCase in this._tdpTestCases)
                {
                    XmlNode newChild = xmlDocument.CreateElement("Case");
                    if (!this.TdpTestCase2XmlCaseNode(tdpTestCase, ref newChild))
                    {
                        return false;
                    }
                    xmlNode4.AppendChild(newChild);
                }
                xmlNode.AppendChild(xmlNode4);
                xmlDocument.AppendChild(xmlNode);
            }
            catch (Exception ex)
            {
                this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TdpTestSequence_SaveSequenceFailed + "\n\n{0}", ex.Message);
                return false;
            }
            return true;
        }
        private bool TdpTestCase2XmlCaseNode(TdpTestCase tdpTestCase, ref XmlNode caseNode)
        {
            try
            {
                NameValueCollection limits = new NameValueCollection();
                tdpTestCase.SelfTestCase.CaseInstance.CaseLimitSetting.GetPropertiesValue(out limits);
                tdpTestCase.SelfTestCase.CaseInfo.Limits = limits;
                NameValueCollection parameters = new NameValueCollection();
                tdpTestCase.SelfTestCase.CaseInstance.CaseParameterSetting.GetPropertiesValue(out parameters);
                tdpTestCase.SelfTestCase.CaseInfo.Parameters = parameters;
                XmlDocument ownerDocument = caseNode.OwnerDocument;
                XmlAttribute node;
                (node = ownerDocument.CreateAttribute("dllName")).Value = tdpTestCase.SelfTestCase.CaseInfo.AssemblyName;
                caseNode.Attributes.Append(node);
                (node = ownerDocument.CreateAttribute("dllVersion")).Value = tdpTestCase.SelfTestCase.CaseInfo.AssemblyVersion.ToString();
                caseNode.Attributes.Append(node);
                (node = ownerDocument.CreateAttribute("fullName")).Value = tdpTestCase.SelfTestCase.CaseInfo.TestCaseFullName;
                caseNode.Attributes.Append(node);
                XmlNode xmlNode = caseNode.OwnerDocument.CreateElement("EngineMode");
                (node = ownerDocument.CreateAttribute("type")).Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Type.ToString();
                xmlNode.Attributes.Append(node);
                XmlNode xmlNode2 = xmlNode.AppendChild(ownerDocument.CreateNode(XmlNodeType.Element, "ErrorMode", string.Empty));
                (node = ownerDocument.CreateAttribute("mode")).Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.Error.ToString();
                xmlNode2.Attributes.Append(node);
                (node = ownerDocument.CreateAttribute("retry")).Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.ErrorRetry.ToString();
                xmlNode2.Attributes.Append(node);
                xmlNode2 = xmlNode.AppendChild(ownerDocument.CreateNode(XmlNodeType.Element, "FailMode", string.Empty));
                (node = ownerDocument.CreateAttribute("mode")).Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OK.ToString();
                xmlNode2.Attributes.Append(node);
                (node = ownerDocument.CreateAttribute("retry")).Value = tdpTestCase.SelfTestCase.CaseInfo.CaseEngineMode.OkRetry.ToString();
                xmlNode2.Attributes.Append(node);
                caseNode.AppendChild(xmlNode);
                XmlNode xmlNode3 = caseNode.OwnerDocument.CreateElement("Limits");
                for (int i = 0; i < tdpTestCase.SelfTestCase.CaseInfo.Limits.Count; i++)
                {
                    xmlNode2 = xmlNode3.AppendChild(ownerDocument.CreateNode(XmlNodeType.Element, "Limit", string.Empty));
                    (node = ownerDocument.CreateAttribute("name")).Value = tdpTestCase.SelfTestCase.CaseInfo.Limits.Keys[i];
                    xmlNode2.Attributes.Append(node);
                    if (!string.IsNullOrEmpty(tdpTestCase.SelfTestCase.CaseInfo.Limits[i]))
                    {
                        xmlNode2.InnerText = tdpTestCase.SelfTestCase.CaseInfo.Limits[i];
                    }
                }
                caseNode.AppendChild(xmlNode3);
                XmlNode xmlNode4 = caseNode.OwnerDocument.CreateElement("Parameters");
                for (int j = 0; j < tdpTestCase.SelfTestCase.CaseInfo.Parameters.Count; j++)
                {
                    xmlNode2 = xmlNode4.AppendChild(ownerDocument.CreateNode(XmlNodeType.Element, "Parameter", string.Empty));
                    (node = ownerDocument.CreateAttribute("name")).Value = tdpTestCase.SelfTestCase.CaseInfo.Parameters.Keys[j];
                    xmlNode2.Attributes.Append(node);
                    if (!string.IsNullOrEmpty(tdpTestCase.SelfTestCase.CaseInfo.Parameters[j]))
                    {
                        xmlNode2.InnerText = tdpTestCase.SelfTestCase.CaseInfo.Parameters[j];
                    }
                }
                caseNode.AppendChild(xmlNode4);
                XmlNode xmlNode5 = caseNode.OwnerDocument.CreateElement("ContainedCases");
                foreach (TdpTestCase tdpTestCase2 in tdpTestCase.ContainedTdpTestCase)
                {
                    XmlNode newChild = ownerDocument.CreateElement("Case");
                    if (!this.TdpTestCase2XmlCaseNode(tdpTestCase2, ref newChild))
                    {
                        return false;
                    }
                    xmlNode5.AppendChild(newChild);
                }
                caseNode.AppendChild(xmlNode5);
            }
            catch (Exception ex)
            {
                this._errorMessage = string.Format(Resources.LNG_TDP_Engine_TdpTestSequence_SaveSequenceFailed + "\n\n{0}", ex.Message);
                return false;
            }
            return true;
        }
        public bool Reset(bool isResetPassFailRatio)
        {
            if (isResetPassFailRatio)
            {
                this._executionStatus.ResetPassFailRatio();
            }
            this._executionStatus.ResetTempVariable();
            XmlDocument xmlDocument = new XmlDocument();
            return this.WriteToXmlDocument(ref xmlDocument) && this.LoadFromXmlDocument(xmlDocument);
        }
        public List<CoreCase> GetCoreCaseCollection()
        {
            List<CoreCase> list = new List<CoreCase>();
            foreach (TdpTestCase tdpTestCase in this._tdpTestCases)
            {
                list.AddRange(this.GetCoreCaseCollection(tdpTestCase));
            }
            return list;
        }
        private List<CoreCase> GetCoreCaseCollection(TdpTestCase tdpTestCase)
        {
            List<CoreCase> list = new List<CoreCase>();
            list.Add(tdpTestCase.SelfTestCase.CaseInstance);
            foreach (TdpTestCase tdpTestCase2 in tdpTestCase.ContainedTdpTestCase)
            {
                list.AddRange(this.GetCoreCaseCollection(tdpTestCase2));
            }
            return list;
        }
        public TreeNode WriteToTreeNode()
        {
            TreeNode treeNode = new TreeNode(this._sequenceInfo.DisplayName);
            treeNode.Name = this._sequenceInfo.DisplayName;
            treeNode.Tag = new TdpTestSequence.TreeNodeTagInfo(this._sequenceInfo);
            foreach (TdpTestCase tdpTestCase in this._tdpTestCases)
            {
                TreeNode node = null;
                this.TdpTestCase2TreeNode(tdpTestCase, out node);
                treeNode.Nodes.Add(node);
            }
            return treeNode;
        }
        private void TdpTestCase2TreeNode(TdpTestCase tdpTestCase, out TreeNode treeNode)
        {
            treeNode = new TreeNode(tdpTestCase.SelfTestCase.DisplayName);
            treeNode.Name = tdpTestCase.SelfTestCase.DisplayName;
            treeNode.ForeColor = tdpTestCase.SelfTestCase.DisplayColor;
            treeNode.Tag = new TdpTestSequence.TreeNodeTagInfo(tdpTestCase, this._sequenceInfo);
            foreach (TdpTestCase tdpTestCase2 in tdpTestCase.ContainedTdpTestCase)
            {
                TreeNode node = null;
                this.TdpTestCase2TreeNode(tdpTestCase2, out node);
                treeNode.Nodes.Add(node);
            }
        }
        public void LoadFromTreeNode(TdpTestSequenceInfo sequenceInfo, TreeNodeCollection treeNodes)
        {
            this._sequenceInfo = sequenceInfo;
            this._tdpTestCases.Clear();
            foreach (object obj in treeNodes)
            {
                TreeNode treeNode = (TreeNode)obj;
                TdpTestCase item = new TdpTestCase();
                this.TreeNode2TdpTestCase(treeNode, out item);
                this._tdpTestCases.Add(item);
            }
        }
        private void TreeNode2TdpTestCase(TreeNode treeNode, out TdpTestCase tdpTestCase)
        {
            tdpTestCase = new TdpTestCase();
            tdpTestCase.SelfTestCase = (treeNode.Tag as TdpTestSequence.TreeNodeTagInfo).TdpTestCase.SelfTestCase;
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                TreeNode treeNode2 = treeNode.Nodes[i];
                TdpTestCase item = new TdpTestCase();
                this.TreeNode2TdpTestCase(treeNode2, out item);
                tdpTestCase.ContainedTdpTestCase.Add(item);
            }
        }
        public static bool GetTdpSequenceInfo(string xmlFileFullName, out TdpTestSequenceInfo sequenceInfo, out string errMessage)
        {
            sequenceInfo = new TdpTestSequenceInfo();
            errMessage = string.Empty;
            bool result;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlFileFullName);
                XmlNode documentElement = xmlDocument.DocumentElement;
                sequenceInfo.XmlFileFullName = xmlFileFullName;
                sequenceInfo.Author = documentElement.SelectSingleNode("descendant::Author").InnerText;
                sequenceInfo.DisplayName = documentElement.SelectSingleNode("descendant::Name").InnerText;
                sequenceInfo.Description = documentElement.SelectSingleNode("descendant::Description").InnerText;
                sequenceInfo.CreatedTime = DateTime.Parse(documentElement.SelectSingleNode("descendant::CreatedTime").InnerText);
                sequenceInfo.ModifiedTime = DateTime.Parse(documentElement.SelectSingleNode("descendant::ModifiedTime").InnerText);
                XmlNode xmlNode = documentElement.SelectSingleNode("descendant::GlobeEngineMode");
                sequenceInfo.ExecutTimes = ((uint.Parse(xmlNode.Attributes["executedTimes"].Value) < 1U) ? 1U : uint.Parse(xmlNode.Attributes["executedTimes"].Value));
                xmlNode = documentElement.SelectSingleNode("descendant::GlobeEngineMode/ErrorMode");
                sequenceInfo.GlobeEngineMode.Error = (EngineMode_Error)Enum.Parse(typeof(EngineMode_Error), xmlNode.Attributes["mode"].Value, true);
                sequenceInfo.GlobeEngineMode.ErrorRetry = uint.Parse(xmlNode.Attributes["retry"].Value);
                xmlNode = documentElement.SelectSingleNode("descendant::GlobeEngineMode/FailMode");
                sequenceInfo.GlobeEngineMode.OK = (EngineMode_OK)Enum.Parse(typeof(EngineMode_OK), xmlNode.Attributes["mode"].Value, true);
                sequenceInfo.GlobeEngineMode.OkRetry = uint.Parse(xmlNode.Attributes["retry"].Value);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = string.Format(Resources.LNG_TDP_Engine_TdpTestSequence_LoadSequenceFailed + "\n\n{0}", ex.Message);
                result = false;
            }
            return result;
        }
        public class TreeNodeTagInfo
        {
            public TdpTestCase TdpTestCase
            {
                get
                {
                    return this._tdpTestCase;
                }
                set
                {
                    this._tdpTestCase = value;
                }
            }
            public TdpTestSequenceInfo SequenceInfo
            {
                get
                {
                    return this._sequenceInfo;
                }
                set
                {
                    this._sequenceInfo = value;
                }
            }
            public TreeNodeTagInfo(TdpTestCase tdpTestCase) : this(tdpTestCase, null)
            {
            }

            // Token: 0x0600058F RID: 1423 RVA: 0x000058DC File Offset: 0x00003ADC
            public TreeNodeTagInfo(TdpTestSequenceInfo sequenceInfo) : this(null, sequenceInfo)
            {
            }

            // Token: 0x06000590 RID: 1424 RVA: 0x000058E6 File Offset: 0x00003AE6
            public TreeNodeTagInfo(TdpTestCase tdpTestCase, TdpTestSequenceInfo sequenceInfo)
            {
                this._tdpTestCase = tdpTestCase;
                this._sequenceInfo = sequenceInfo;
            }

            // Token: 0x04000297 RID: 663
            private TdpTestCase _tdpTestCase;

            // Token: 0x04000298 RID: 664
            private TdpTestSequenceInfo _sequenceInfo;
        }
    }
}
