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
    public class StackingBackPack: AbstractPersistent {

        #region fields

        private int maxItems;
        private string title = "";
        protected List<Item> items = new List<Item>();

        #endregion

        #region constructors

        public StackingBackPack(string title, int maxItems) : this(maxItems) {
            this.title = title;
        }

        public StackingBackPack(int maxItems) {
            this.maxItems = maxItems;
        }

        public StackingBackPack() { } 

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
        public void Add(Item newItem) {
            if (Full) return;
            Item existingItem = findExistingItem(newItem);
            if (existingItem == null) {
                items.Add(newItem);
            } else {
                if (!(existingItem is ItemStack) && !(newItem is ItemStack)) {
                    ItemStack stack = new ItemStack(existingItem);
                    stack.Add(newItem);
                    Remove(existingItem);
                    items.Add(stack);
                }
                if (!(existingItem is ItemStack) && newItem is ItemStack) {
                    ItemStack stack = (ItemStack)newItem;
                    stack.Add(existingItem);
                    Remove(existingItem);
                    items.Add(stack);
                }
                if (existingItem is ItemStack && !(newItem is ItemStack)) {
                    ((ItemStack)existingItem).Add(newItem);
                }
                if (existingItem is ItemStack && newItem is ItemStack) {
                    ((ItemStack)existingItem).Merge((ItemStack)newItem);
                }
            }
        }

        /// <summary>
        /// Removes an Item from this BackPack
        /// </summary>
        /// <param name="item">An Item to remove</param>
        public void Remove(Item item) {
            items.Remove(item);
        }

        public void  RemoveSingleItem(Item item) {
            Item existingItem = findExistingItem(item);
            if (existingItem == null) {
                throw new ArgumentException(item.ToString() + " does not exist in this BackPack");
            } else if (existingItem is ItemStack) {
                ItemStack stack = (ItemStack)existingItem;
                if (stack.Count > 2) {
                    stack.RemoveFirst();
                } else {
                    Remove(existingItem);
                    Add(stack.Item);
                }
            } else {
                Remove(item);
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder(title + "\n");
            for (int i = 0; i < items.Count; ++i) {
                sb.Append(items[i].ToString() + "\n");
            }
            return sb.ToString();
        }

        #endregion

        #region private methods

        private Item findExistingItem(Item item) {
            if (item is ItemStack) item = ((ItemStack)item).Item;
            foreach (Item existingItem in Items) {
                if (existingItem is ItemStack) {
                    if (((ItemStack)existingItem).Matches(item)) return existingItem;
                } else {
                    if (existingItem.Equals(item)) return existingItem;
                }
            }
            return null;
        }

        #endregion

    }
}
