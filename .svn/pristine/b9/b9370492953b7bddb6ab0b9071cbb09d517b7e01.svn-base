using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Tools;

namespace VH.Engine.Persistency {

    public class PersistentFactory {

        public static IPersistent CreateObject(XmlDocument doc, XmlElement element) {
            String typeName = element.Attributes["type"].Value;
            String assemblyName = element.Attributes["assembly"].Value;
            IPersistent persistent = (IPersistent)AssemblyCache.CreateObject(typeName, assemblyName);
            persistent.FromXml(element);
            return persistent;
        }

       
    }
}
