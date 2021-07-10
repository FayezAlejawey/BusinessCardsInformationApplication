using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using DataAccess.LogFile;

namespace DataAccess.XML {
    public class XmlReader {

        public static List<Dictionary<string, object>> GetXmlData(string xmlAbsolutePath) {

            var xmlExtenstion = Path.GetExtension(xmlAbsolutePath);
            if (!string.Equals(xmlExtenstion, "xml", StringComparison.OrdinalIgnoreCase)) {
                var msg = $"The specified absolute path [{xmlAbsolutePath}] isn't for XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            var isFileExists = File.Exists(xmlAbsolutePath);
            if (!isFileExists) {
                var msg = $"The specified absolute path [{xmlAbsolutePath}] for XML file doesn't exists.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            var records = new List<Dictionary<string, object>>();

            var doc = new XmlDocument();
            doc.Load(xmlAbsolutePath);

            var parentNodes = doc.GetElementsByTagName(XmlConstants.ParentElement);
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
