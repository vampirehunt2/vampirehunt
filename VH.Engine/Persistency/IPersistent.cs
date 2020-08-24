using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace VH.Engine.Persistency {
    public interface IPersistent {

        XmlElement ToXml(string name, XmlDocument doc);

        void FromXml(XmlElement element);

    }
}
