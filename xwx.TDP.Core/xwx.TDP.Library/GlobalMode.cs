using System.Xml;

namespace xwx.TDP.Library
{
	public class GlobalMode
	{
		public int executedTimes = 1;

		public TestMode ErrorMode = new TestMode();

		public TestMode FailMode = new TestMode();

		public GlobalMode()
		{
			ErrorMode.mode = "Abort";
			ErrorMode.retry = 3;
			FailMode.mode = "IgnoreFail";
			FailMode.retry = 3;
		}

		public void DeSerialize(XmlNode parentNode)
		{
			foreach (XmlAttribute attribute in parentNode.Attributes)
			{
				if (attribute.Name == "executedTimes")
				{
					executedTimes = int.Parse(attribute.Value);
					break;
				}
			}
			XmlNode xmlNode = parentNode.SelectSingleNode("ErrorMode");
			if (xmlNode != null)
			{
				ErrorMode = new TestMode();
				ErrorMode.DeSerialize(xmlNode);
			}
			xmlNode = parentNode.SelectSingleNode("FailMode");
			if (xmlNode != null)
			{
				FailMode = new TestMode();
				FailMode.DeSerialize(xmlNode);
			}
		}

		public void Serialize(XmlDocument writer, XmlNode parentNode)
		{
			XmlAttribute xmlAttribute = writer.CreateAttribute("executedTimes");
			xmlAttribute.Value = executedTimes.ToString();
			parentNode.Attributes.Append(xmlAttribute);
			XmlNode xmlNode = writer.CreateNode(XmlNodeType.Element, "ErrorMode", string.Empty);
			ErrorMode.Serialize(writer, xmlNode);
			parentNode.AppendChild(xmlNode);
			xmlNode = writer.CreateNode(XmlNodeType.Element, "FailMode", string.Empty);
			FailMode.Serialize(writer, xmlNode);
			parentNode.AppendChild(xmlNode);
		}
	}
}
