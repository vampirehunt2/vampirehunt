using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace VH.Engine.Persistency {

    public class XmlHandler {

        public static void Save(object obj, string filename) {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlWriter writer = XmlWriter.Create(filename);
            serializer.Serialize(writer, obj);
        }

        public static object Load(string filename, Type type) {
            XmlSerializer serializer = new XmlSerializer(type);
            XmlReader reader = XmlReader.Create(filename);
            return serializer.Deserialize(reader);
        }

    }
}
