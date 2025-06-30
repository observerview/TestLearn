using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestManager.Extern;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library
{
    public class TestPlanFileReader
    {
        public TDP_Plan _sequence = new TDP_Plan();

        public TDP_Plan Sequence 
        {  get { return _sequence; } 
            set { _sequence = value; }        
        }

        public void Load(Stream stream)
        {
            _sequence = new TDP_Plan();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            _sequence.DeSerialize(xmlDocument);
        }

        public void Save(Stream stream)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
            _sequence.Serialize(xmlDocument);
            xmlDocument.Save(stream);
        }

        public TestPlanFileReader ConvertFromCoreCaseList(CaseDllLoader dlls, CoreCase[] ccs)
        {
            TestPlanFileReader testPlanFileReader = new TestPlanFileReader();

            List<CaseNode> caseSequence = testPlanFileReader.Sequence.CaseSequence;
            foreach (CoreCase coreCase in ccs)
            {
                if (!(coreCase.GetType().FullName == "xwx.TDP.Library.BaseCases.CaseFolder"))
                {
                    ConvertCoreCaseIntoCaseNode(caseSequence, dlls, coreCase);
                }
            }
            return testPlanFileReader;
        }


        private static void ConvertCoreCaseIntoCaseNode(List<CaseNode> listCaseNode, CaseDllLoader dlls, CoreCase cc)
        {
            CaseNode caseNode = new CaseNode();
            caseNode.fullName = cc.GetType().FullName;
            caseNode.dllName = cc.GetType().Module.FullyQualifiedName.Substring(dlls.BaseDir.Length);
            caseNode.dllVersion = cc.GetType().Module.Assembly.ToString();
            caseNode.dllVersion = caseNode.dllVersion.Substring(caseNode.dllVersion.IndexOf("Version=") + "Version=".Length);
            caseNode.assembly = cc.GetType().Assembly;
            caseNode.DisplayName = caseNode.fullName;
            object[] customAttributes = cc.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), false);
            if (customAttributes.Length > 0)
            {
                caseNode.DisplayName = ((DisplayNameAttribute)customAttributes[0]).DisplayName;
            }
            customAttributes = cc.GetType().GetCustomAttributes(typeof(CategoryAttribute), false);
            if (customAttributes.Length > 0)
            {
                caseNode.Category = ((CategoryAttribute)customAttributes[0]).Category;
            }
            customAttributes = cc.GetType().GetCustomAttributes(typeof(DisplayColorAttribute), false);
            if (customAttributes.Length > 0)
            {
                caseNode.color = ((DisplayColorAttribute)customAttributes[0]).DisplayColor;
            }
            NameValueCollection names_values;
            cc.CaseParameterSetting.GetPropertiesValue(out names_values);
            string[] allKeys = names_values.AllKeys;
            foreach (string text in allKeys)
            {
                if (text == "DisplayName")
                {
                    caseNode.DisplayName = names_values[text];
                    break;
                }
            }
            caseNode.coreCase = cc;
            listCaseNode.Add(caseNode);
        }


    }
}
