using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using VH.Engine.Levels;

namespace VH.Engine.World.Items {

    public class ItemStack : Item {

        #region constants

        private const string ITEM = "item";

        #endregion

        #region fields

        private List<Item> items = new List<Item>();

        #endregion

        #region constructors

        public ItemStack() { }

        public ItemStack(Item item) {
            items.Add(item);
        }

        public ItemStack(Item item, int count) {
            for (int i = 0; i < count; i++) {
                items.Add(item.Clone());
            }
        }

        #endregion

        #region properties

        public Item Item {
            get {
                if (items.Count > 0) return items[0];
                else return null;
            }
        }

        public List<Item> Items {
            get {
                return items;
            }
        }

        public int Count {
            get {
                return items.Count;
            }
        }

        public override string Accusativ {
            get {
                return Item.Accusativ + countString;
            }
            set { }
        }

        public override char Character {
            get { return Item.Character; }
            set { }
        }

        public override ConsoleColor Color {
            get { return Item.Color; }
            set { }
        }

        public override int Danger {
            get { return Item.Danger; } //TODO if items in the stack just appear the same, 
                                        //as opposed to actually being the same, 
                                        //they may have a different danger level
            set { }
        }

        public override string Name {
            get { return Item.Name + countString; }
            set { }
        }

        public override string Plural {
            get { return Item.Plural + countString; }
            set { }
        }

        private string countString {
            get { return " (x" + Count + ")"; }
        }

        #endregion

        #region public methods

        public bool Matches(Item item) {
            return item.Equals(Item);
        }

        public void RemoveAll() {
            items.RemoveRange(0, items.Count);
        }

        public Item RemoveFirst() {
            Item item = items[0];
            items.RemoveAt(0);
            return item;
        }

        public bool IsEmpty() {
            return Count == 0;
        }

        public void Add(Item item) {
            if (Item != null && !item.Equals(Item)) throw new ArgumentException("Not possible to add " + item.ToString() + " to this stack");
            items.Add(item);
        }

        public ItemStack Split(int size) {
            if (size > Count) throw new ArgumentOutOfRangeException("Target Size is bigger than source Count");
            ItemStack newStack = new ItemStack();
            for (int i = 0; i < size; ++i) {
                Item item = items[0];
                newStack.Add(item);
                RemoveFirst();
            }
            return newStack;
        }

        public void Merge(ItemStack stack) {
            if (!Item.Equals(stack.Item)) throw new ArgumentException("The two stacks contain different items");
            for (int i = 0; i < stack.Count; ++i) items.Add(stack.Item);
            stack.RemoveAll();
        }

        public override string ToString() {
            return Item.Plural + countString;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            for (int i = 0; i < Items.Count; ++i) AddElement(ITEM, Items[i]);
            return element;
        }

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            List<Item> items = GetElements<Item>(ITEM);
            for (int i = 0; i < items.Count; ++i) this.Items.Add(items[i]);
        }

        #endregion

    }
}
