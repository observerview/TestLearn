using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xwx.TDP.Library
{
    public class TDP_Plan
    {
        public string Name = "";

        public string Author = "";

        public DateTime CreatedTime = DateTime.Now;

        public DateTime ModifiedTime = DateTime.Now;

        public string Description = "";

        public GlobalMode GlobalEngineMode = new GlobalMode();

        public List<CaseNode> CaseSequence = new List<CaseNode>();

        public void DeSerialize(XmlDocument doc)
        {
            XmlNode xmlNode = doc.SelectSingleNode("TSP_Plan");
            XmlNode xmlNode2 = xmlNode.SelectSingleNode("Name");
            if (xmlNode2 != null)
            {
                Name = xmlNode2.InnerText;
            }
            xmlNode2 = xmlNode.SelectSingleNode("Author");
            if (xmlNode2 != null)
            {
                Author = xmlNode2.InnerText;
            }
            xmlNode2 = xmlNode.SelectSingleNode("CreatedTime");
            if (xmlNode2 != null)
            {
                CreatedTime = DateTime.Parse(xmlNode2.InnerText);
            }
            xmlNode2 = xmlNode.SelectSingleNode("ModifiedTime");
            if (xmlNode2 != null)
            {
                ModifiedTime = DateTime.Parse(xmlNode2.InnerText);
            }
            xmlNode2 = xmlNode.SelectSingleNode("Description");
            if (xmlNode2 != null)
            {
                Description = xmlNode2.InnerText;
            }
            xmlNode2 = xmlNode.SelectSingleNode("GlobeEngineMode");
            if (xmlNode2 != null)
            {
                GlobalEngineMode = new GlobalMode();
                GlobalEngineMode.DeSerialize(xmlNode2);
            }
            xmlNode2 = xmlNode.SelectSingleNode("CaseSequence");
            CaseSequence.Clear();
            if (xmlNode2 == null)
            {
                return;
            }
            foreach (XmlNode childNode in xmlNode2.ChildNodes)
            {
                CaseNode caseNode = new CaseNode();
                caseNode.DeSerialize(childNode);
                CaseSequence.Add(caseNode);
            }
        }

        public void Serialize(XmlDocument writer)
        {
            XmlNode xmlNode = writer.CreateNode(XmlNodeType.Element, "TSP_Plan", string.Empty);
            writer.AppendChild(xmlNode);
            XmlNode xmlNode2 = writer.CreateNode(XmlNodeType.Element, "Name", string.Empty);
            xmlNode2.InnerText = Name;
            xmlNode.AppendChild(xmlNode2);
            xmlNode2 = writer.CreateNode(XmlNodeType.Element, "Author", string.Empty);
            xmlNode2.InnerText = Author;
            xmlNode.AppendChild(xmlNode2);
            xmlNode2 = writer.CreateNode(XmlNodeType.Element, "CreatedTime", string.Empty);
            xmlNode2.InnerText = CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
            xmlNode.AppendChild(xmlNode2);
            xmlNode2 = writer.CreateNode(XmlNodeType.Element, "ModifiedTime", string.Empty);
            xmlNode2.InnerText = ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss");
            xmlNode.AppendChild(xmlNode2);
            xmlNode2 = writer.CreateNode(XmlNodeType.Element, "Description", string.Empty);
            xmlNode2.InnerText = Description;
            xmlNode.AppendChild(xmlNode2);
            xmlNode2 = writer.CreateNode(XmlNodeType.Element, "GlobeEngineMode", string.Empty);
            xmlNode.AppendChild(xmlNode2);
            if (GlobalEngineMode != null)
            {
                GlobalEngineMode.Serialize(writer, xmlNode2);
            }
            xmlNode2 = writer.CreateNode(XmlNodeType.Element, "CaseSequence", string.Empty);
            xmlNode.AppendChild(xmlNode2);
            if (CaseSequence != null)
            {
                for (int i = 0; i < CaseSequence.Count; i++)
                {
                    XmlNode xmlNode3 = writer.CreateElement("Case");
                    CaseSequence[i].Serialize(writer, xmlNode3);
                    xmlNode2.AppendChild(xmlNode3);
                }
            }
        }
    }
}
