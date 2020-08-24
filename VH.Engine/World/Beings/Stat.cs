using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.World.Beings {

    public class Stat: AbstractPersistent, ICloneable {

        #region constants

        private const string ID = "id";
        private const string NAME = "name";
        private const string ATTRIBUTE_VALUE = "attribute-value";

        #endregion

        #region fields

        private string id;
        private string name;
        private int attributeValue;

        #endregion

        #region constructors

        public Stat() { }

        public Stat(string id, string name): this(id, name, 0) { }

        public Stat(string id, string name, int attributeValue) {
            this.id = id;
            this.name = name;
            this.attributeValue = attributeValue;
        }

        #endregion

        #region properties

        public string Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        public int Value {
            get { return attributeValue; }
            set { attributeValue = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            id = GetStringAttribute(ID);
            name = GetStringAttribute(NAME);
            attributeValue = GetIntAttribute(ATTRIBUTE_VALUE);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(ID, id);
            AddAttribute(NAME, this.name);
            AddAttribute(ATTRIBUTE_VALUE, attributeValue);
            return element;
        }

        public override string ToString() {
            return Name + ": " + Value;
        }

        public object Clone() {
            return new Stat(Id, Name, Value);
        }

        #endregion


    }
}
