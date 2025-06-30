using System.Xml;

namespace xwx.TDP.Library
{
	public class EngineMode
	{
		public string type = "Globe";

		public TestMode ErrorMode = new TestMode();

		public TestMode FailMode = new TestMode();

		public EngineMode()
		{
			ErrorMode.mode = "Abort";
			ErrorMode.retry = 3;
			FailMode.mode = "IgnoreFail";
			FailMode.retry = 3;
		}

		public void DeSerialize(XmlNode parentNode)
		{
			XmlNode namedItem = parentNode.Attributes.GetNamedItem("type");
			if (namedItem != null)
			{
				type = namedItem.Value;
			}
			XmlNode xmlNode = parentNode.SelectSingleNode("ErrorMode");
			ErrorMode = new TestMode();
			if (xmlNode != null)
			{
				ErrorMode.DeSerialize(xmlNode);
			}
			xmlNode = parentNode.SelectSingleNode("FailMode");
			if (xmlNode != null)
			{
				FailMode.DeSerialize(xmlNode);
			}
		}

		public void Serialize(XmlDocument writer, XmlNode parentNode)
		{
			XmlAttribute xmlAttribute = writer.CreateAttribute("type");
			xmlAttribute.Value = type;
			parentNode.Attributes.Append(xmlAttribute);
			XmlNode xmlNode = writer.CreateNode(XmlNodeType.Element, "ErrorMode", string.Empty);
			ErrorMode.Serialize(writer, xmlNode);
			parentNode.AppendChild(xmlNode);
			xmlNode = writer.CreateElement("FailMode");
			FailMode.Serialize(writer, xmlNode);
			parentNode.AppendChild(xmlNode);
		}
	}
}
