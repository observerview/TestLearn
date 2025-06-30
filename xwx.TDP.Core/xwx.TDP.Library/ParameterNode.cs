using System.Xml;

namespace xwx.TDP.Library
{
	public class ParameterNode
	{
		public string name = "";

		public string value = "";

		public void DeSerialize(XmlNode parentNode)
		{
			XmlNode namedItem = parentNode.Attributes.GetNamedItem("name");
			if (namedItem != null)
			{
				name = namedItem.Value;
			}
			value = parentNode.InnerText;
		}

		public void Serialize(XmlDocument writer, XmlNode parentNode)
		{
			XmlAttribute xmlAttribute = writer.CreateAttribute("name");
			xmlAttribute.Value = name;
			parentNode.Attributes.Append(xmlAttribute);
			parentNode.InnerText = value;
		}
	}
}
