using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VH.Engine.Persistency {

    public abstract class AbstractPersistent: IPersistent{

        #region constants

        private const string LIST_ITEM = "-item";
        private const string UUID = "uuid";

        #endregion

        #region fields

        private XmlDocument doc;
        private XmlElement element;
        private string uuid; 

        #endregion

        public string Uuid {
            get {
                if (uuid == null) uuid = Guid.NewGuid().ToString();
                return uuid;
            }
            set { uuid = value; }
        }

        #region public methods

        public virtual XmlElement ToXml(string name, XmlDocument doc) {
            this.doc = doc;
            XmlElement element = doc.CreateElement(name);
            this.element = element;
            AddAttribute("type", this.GetType().FullName);
            AddAttribute("assembly", Path.GetFileName(GetType().Assembly.Location));
            AddAttribute("uuid", Uuid);
            return element;
        }

        public virtual void FromXml(XmlElement element) {
            this.element = element;
            uuid = GetStringAttribute("uuid");
        }

        public bool HasAttribute(String name) {
            return element.Attributes[name] != null;
        }

        public string GetStringAttribute(String name) {
            return element.Attributes[name].Value;
        }

        public bool GetBoolAttribute(String name) {
            return Boolean.Parse(GetStringAttribute(name));
        }

        public int GetIntAttribute(String name) {
            return Int32.Parse(GetStringAttribute(name));
        }

        public void AddAttribute(String name, String value) {
            XmlAttribute attribute = doc.CreateAttribute(name);
            attribute.Value = value;
            element.Attributes.Append(attribute);
        }

        public void AddAttribute(String name, Object obj) {
            AddAttribute(name, obj.ToString());
        }

        public void AddAttribute(String name, bool value) {
            AddAttribute(name, "" + value);
        }

        public void AddAttribute(String name, int value) {
            AddAttribute(name, "" + value);
        }

        public IPersistent GetElement(string name) {
            XmlNode node = element.SelectSingleNode("./" + name);
            if (node == null) return null;
            return PersistentFactory.CreateObject(doc, (XmlElement)node);
        }

        public void AddElement(string name, IPersistent child) {
            if (child != null )
                element.AppendChild(child.ToXml(name, doc));
        }

        public string GetRawData(string name) {
            XmlNode data = element.SelectSingleNode("map");
            data = data.ChildNodes[0];
            if (!(data is XmlCDataSection)) throw new ArgumentException("Node '" + name + "' is not a CData node.");
            return (data as XmlCDataSection).Value;
        }

        public void AddRawData(string name, string data) {
            XmlElement dataElement = doc.CreateElement(name);
            element.AppendChild(dataElement);
            dataElement.AppendChild(doc.CreateCDataSection(data));
        }

        public List<T> GetElements<T>(string name) {
            XmlNodeList nodes = element.SelectNodes("./" + name + "/" + name + LIST_ITEM);
            List<T> list = new List<T>();
            foreach (XmlNode node in nodes) {
                list.Add((T)PersistentFactory.CreateObject(doc, (XmlElement)node));
            }
            return list;
        }

        public void AddElements(string name, IEnumerable<AbstractPersistent> elements) {
            XmlElement elementList = doc.CreateElement(name);
            foreach (AbstractPersistent e in elements) {
                elementList.AppendChild(e.ToXml(name + LIST_ITEM, doc));
            }
            element.AppendChild(elementList);
        }

        #endregion
    }
}
