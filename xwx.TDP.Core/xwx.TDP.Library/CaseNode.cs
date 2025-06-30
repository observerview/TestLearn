using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestManager.Extern;

namespace xwx.TDP.Library
{
    public class CaseNode
    {
        public string dllName = "";

        public string dllVersion = "";

        public string fullName = "";

        public string DisplayName = "";

        public Assembly assembly = null;

        public CoreCase coreCase = null;

        public Color color = Color.Black;

        public string Category = "Misc";

        public EngineMode EngineMode = new EngineMode();

        public List<LimitNode> Limits = new List<LimitNode>();

        public List<ParameterNode> Parameters = new List<ParameterNode>();

        public List<CaseNode> ContainedCases = new List<CaseNode>();

        public void DeSerialize(XmlNode parentNode)
        {
            XmlNode namedItem = parentNode.Attributes.GetNamedItem("dllName");
            if (namedItem != null)
            {
                dllName = namedItem.Value;
            }
            namedItem = parentNode.Attributes.GetNamedItem("dllVersion");
            if (namedItem != null)
            {
                dllVersion = namedItem.Value;
            }
            namedItem = parentNode.Attributes.GetNamedItem("fullName");
            if (namedItem != null)
            {
                fullName = namedItem.Value;
            }
            EngineMode = new EngineMode();
            namedItem = parentNode.SelectSingleNode("EngineMode");
            if (namedItem != null)
            {
                EngineMode.DeSerialize(namedItem);
            }
            Limits = new List<LimitNode>();
            namedItem = parentNode.SelectSingleNode("Limits");
            foreach (XmlNode childNode in namedItem.ChildNodes)
            {
                LimitNode limitNode = new LimitNode();
                limitNode.DeSerialize(childNode);
                Limits.Add(limitNode);
            }
            Parameters = new List<ParameterNode>();
            namedItem = parentNode.SelectSingleNode("Parameters");
            foreach (XmlNode childNode2 in namedItem.ChildNodes)
            {
                ParameterNode parameterNode = new ParameterNode();
                parameterNode.DeSerialize(childNode2);
                Parameters.Add(parameterNode);
            }
            namedItem = parentNode.SelectSingleNode("ContainedCases");
            ContainedCases = new List<CaseNode>();
            if (namedItem == null)
            {
                return;
            }
            foreach (XmlNode childNode3 in namedItem.ChildNodes)
            {
                CaseNode caseNode = new CaseNode();
                caseNode.DeSerialize(childNode3);
                ContainedCases.Add(caseNode);
            }
        }

        public void Serialize(XmlDocument writer, XmlNode parentNode)
        {
            XmlAttribute xmlAttribute = writer.CreateAttribute("dllName");
            xmlAttribute.Value = dllName;
            parentNode.Attributes.Append(xmlAttribute);
            xmlAttribute = writer.CreateAttribute("dllVersion");
            xmlAttribute.Value = dllVersion;
            parentNode.Attributes.Append(xmlAttribute);
            xmlAttribute = writer.CreateAttribute("fullName");
            xmlAttribute.Value = fullName;
            parentNode.Attributes.Append(xmlAttribute);
            XmlNode xmlNode = writer.CreateElement("EngineMode");
            EngineMode.Serialize(writer, xmlNode);
            parentNode.AppendChild(xmlNode);
            xmlNode = writer.CreateElement("Limits");
            parentNode.AppendChild(xmlNode);
            foreach (LimitNode limit in Limits)
            {
                XmlNode xmlNode2 = writer.CreateElement("Limit");
                limit.Serialize(writer, xmlNode2);
                xmlNode.AppendChild(xmlNode2);
            }
            xmlNode = writer.CreateElement("Parameters");
            parentNode.AppendChild(xmlNode);
            foreach (ParameterNode parameter in Parameters)
            {
                XmlNode xmlNode3 = writer.CreateElement("Parameter");
                parameter.Serialize(writer, xmlNode3);
                xmlNode.AppendChild(xmlNode3);
            }
            xmlNode = writer.CreateElement("ContainedCases");
            parentNode.AppendChild(xmlNode);
            foreach (CaseNode containedCase in ContainedCases)
            {
                XmlNode xmlNode4 = writer.CreateElement("Case");
                containedCase.Serialize(writer, xmlNode4);
                xmlNode.AppendChild(xmlNode4);
            }
        }
    }
}
