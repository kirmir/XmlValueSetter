using System;
using System.IO;
using System.Xml;

namespace XmlValueSetter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length != 3 && args.Length != 4)
                    throw new ArgumentException(
                        "The number of arguments is incorrect. The usage:\n" +
                        "> XmlValueSetter.exe fileName xPath value [attributeName]");

                var fileName = args[0];
                var xPath    = args[1];
                var value    = args[2];
                var attrName = (args.Length == 4) ? args[3] : null;

                if (!File.Exists(fileName))
                    throw new FileNotFoundException("The input XML file isn't exists.", fileName);

                if (string.IsNullOrEmpty(xPath))
                    throw new ArgumentException("The XPath should be specified.");

                setAttributeValue(fileName, xPath, value, attrName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void setAttributeValue(string fileName, string xPath, string value, string attribute)
        {
            var xml = new XmlDocument();
            xml.Load(fileName);

            var node = xml.SelectSingleNode(xPath);
            if (node == null)
            {
                throw new ArgumentException("The XML file doesn't contain any node by the specified XPath.");
            }

            // ReSharper disable once PossibleNullReferenceException
            if (string.IsNullOrEmpty(attribute))
            {
                node.InnerText = value;
            }
            else
            {
                node.Attributes[attribute].InnerText = value;
            }

            xml.Save(fileName);
        }
    }
}