using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;
using VH.Engine.Translations;

namespace VH.Engine.World.Items {

    /// <summary>
    /// Represents an equipment slot.
    /// </summary>
    public abstract class EquipmentSlot: AbstractPersistent {

        #region fields

        protected Item item;
        protected string id;

        #endregion

        #region constructors

        /// <summary>
        /// Creates an instance of EquipmentSlot
        /// </summary>
        public EquipmentSlot() { }

        #endregion

        #region properties

        /// <summary>
        /// Gets the name ot this equipment slot
        /// </summary>
        public string Id {
            get { return id; }
        }

        public string Name {
            get { return Translator.Instance[id]; }
        }

        /// <summary>
        /// Gets otr sets an Item that is contained in this EquipmentSlot
        /// </summary>
        public Item Item {
            get { return item; }
            set { item = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            item = GetElement("item") as Item;
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddElement("item", item);
            return element;
        }

        /// <summary>
        /// Indicates whether a given Item can be contained in this EquipmentSlot
        /// </summary>
        /// <param name="item">An item to check</param>
        /// <returns>true if the item can be contained in this equipment slot</returns>
        public abstract bool IsItemCompatible(Item item);

        public override string ToString() {
            string result = Name + ": ";
            if (item != null) result += item.ToString();
            return result;
        }

        #endregion


    }
}
