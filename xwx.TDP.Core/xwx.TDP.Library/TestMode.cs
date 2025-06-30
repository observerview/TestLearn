using System.Xml;

namespace xwx.TDP.Library
{
	public class TestMode
	{
		public string mode = "Abort";

		public int retry = 3;

		public void DeSerialize(XmlNode parentNode)
		{
			foreach (XmlAttribute attribute in parentNode.Attributes)
			{
				if (attribute.Name == "mode")
				{
					mode = attribute.Value;
				}
				else if (attribute.Name == "retry")
				{
					retry = int.Parse(attribute.Value);
				}
			}
		}

		public void Serialize(XmlDocument writer, XmlNode parentNode)
		{
			XmlAttribute xmlAttribute = writer.CreateAttribute("mode");
			xmlAttribute.Value = mode;
			parentNode.Attributes.Append(xmlAttribute);
			xmlAttribute = writer.CreateAttribute("retry");
			xmlAttribute.Value = retry.ToString();
			parentNode.Attributes.Append(xmlAttribute);
		}
	}
}
