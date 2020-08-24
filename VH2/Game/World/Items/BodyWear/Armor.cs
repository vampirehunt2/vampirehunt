using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;

namespace VH.Game.World.Items.BodyWear {

    public class Armor: Item, IEquipableItem {

        #region constants

        private const string DEFENSE = "defense";

        #endregion

        #region fields

        private int defense;

        #endregion

        #region properties

        public int Defense {
            get { return defense; }
            set { defense = value; }
        }

        public int Attack {
            get { return 0; }
            set { /* do nothing */ }
        }

        public Stat StatModifier {
            get { return null; }
            set { /* do nothing */ }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            defense = GetIntAttribute(DEFENSE);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(DEFENSE, defense);
            return element;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            defense = int.Parse(prototype.Attributes[DEFENSE].Value);
        }

        public override string ToString() {
            string result = Name + " ";
            if (Defense >= 0) result += "+";
            result += Defense;
            return result;
        }

        public void OnEquip() {
            // do nothing
        }

        public void OnDeequip() {
            // do nothing
        }

        #endregion


    }
}
