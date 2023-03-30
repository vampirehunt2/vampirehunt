using System;
using VH.Engine.Display;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.World.Items {

    /// <summary>
    /// Represents an item.
    /// </summary>
    public class Item: AbstractEntity, IPersistent  {

        #region constants

        #endregion

        #region fields

        private XmlElement prototype;

        #endregion

        #region properties

        public override Person Person {
            get { return Person.Third; }
        }

        /// <summary>
        /// The name that this Entity is referred by.
        /// </summary>
        public override string Identity {
            get { return Name; } 
        }

        #endregion

        #region public methods

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            this.prototype = prototype;
        }

        public Item Clone() {
            Item newItem = new Item();
            newItem.Create(prototype);
            return newItem;
        }

        public override bool Equals(object obj) {
            return Identity == ((Item)obj).Identity;
        }

        public void FromXml(XmlDocument doc, XmlElement element) {
            throw new NotImplementedException();
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            return element;
        }

        #endregion

    }
}
