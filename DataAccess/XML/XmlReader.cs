using System.Collections.Generic;
using System.Xml;

namespace DataAccess.XML {
    public class XmlReader {

        public static List<Dictionary<string, object>> GetXmlData(XmlDocument loadedXmlDoc) {

            var records = new List<Dictionary<string, object>>();

            var parentNodes = loadedXmlDoc.GetElementsByTagName(XmlConstants.ParentElement);
            foreach(XmlNode parentNode in parentNodes) {

                var childNodes = parentNode.ChildNodes;
                var record = new Dictionary<string, object>();
                foreach(XmlNode childNode in childNodes) {
                    var key = childNode.Name;
                    var value = childNode.InnerText;
                    record[key] = value;
                }

                records.Add(record);

            }

            return records;

        }
    }
}
