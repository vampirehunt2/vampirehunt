using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.World.Beings {

    public abstract class AbstractProfession: AbstractPersistent {

        #region constants

        private const string NAME = "name";

        #endregion

        #region fields

        protected Being being;
        protected string name;

        #endregion

        #region constructors

        public AbstractProfession(Being being) {
            this.being = being;
        }

        public AbstractProfession() { }

        #endregion

        #region properties

        public string Name {
            get { return name; }
        }

        public Being Being {
            get { return being; }
            set { being = value; }
        }

        #endregion

        #region public methods

        public abstract void InitBeing();

        public override string ToString() {
            return Name;
        }

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            this.name = GetStringAttribute(NAME);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(NAME, this.name);
            return element;
        }

        #endregion

    }
}
