using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;
using VH.Engine.Translations;

namespace VH.Engine.World.Items {

    /// <summary>
    /// Represents a set of items that a Being has with it.
    /// </summary>
    public class BackPack: AbstractPersistent {

        #region fields

        private int maxItems;
        private string title = "";
        protected List<Item> items = new List<Item>();

        #endregion

        #region constructors

        public BackPack(string title, int maxItems) : this(maxItems) {
            this.title = title;
        }

        public BackPack(int maxItems) {
            this.maxItems = maxItems;
        }

        public BackPack() { } 

        #endregion

        #region properties

        /// <summary>
        /// Gets a value indicating whether this BackPack is Full.
        /// </summary>
        public virtual bool Full {
            get { return items.Count() >= maxItems; }
        }

        /// <summary>
        /// List of Items contained in this BackPack
        /// </summary>
        public List<Item> Items {
            get { return items; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            maxItems = GetIntAttribute("max-items");
            title = GetStringAttribute("title");
            items = GetElements<Item>("items"); // TODO create constants
        }
    

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute("max-items", maxItems);
            AddAttribute("title", title);
            AddElements("items", items);
            return element;
        }

        /// <summary>
        /// Adds an Item to this BackPack
        /// </summary>
        /// <param name="item">An Item to add</param>
        public void Add(Item item) {
            if (!Full) items.Add(item);
        }

        /// <summary>
        /// Removes an Item from this BackPack
        /// </summary>
        /// <param name="item">An Item to remove</param>
        public void Remove(Item item) {
            items.Remove(item);
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder(title + "\n");
            for (int i = 0; i < items.Count; ++i) {
                sb.Append(items[i].ToString() + "\n");
            }
            return sb.ToString();
        }

        #endregion

    }
}