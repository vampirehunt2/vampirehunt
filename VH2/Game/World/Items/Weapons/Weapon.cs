using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;

namespace VH.Game.World.Items.Weapons {

    /// <summary>
    /// Represents a mellee weapon.
    /// </summary>
    public class Weapon: Item, IEquipableItem {

        #region constants

        private const string ATTACK = "attack";
        private const string DEFENSE = "defense";

        #endregion

        #region fields

        private int attack;
        private int defense;

        #endregion

        #region properties

        public int Attack {
            get { return attack; }
            set { attack = value; }
        }

        public int Defense {
            get { return defense; }
            set { defense = value; }
        }

        public Stat StatModifier {
            get { return null; }
            set { /* do nothing */ }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            attack = GetIntAttribute(ATTACK);
            defense = GetIntAttribute(DEFENSE);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(ATTACK, attack);
            AddAttribute(DEFENSE, defense);
            return element;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            attack = int.Parse(prototype.Attributes[ATTACK].Value);
            defense = int.Parse(prototype.Attributes[DEFENSE].Value);
        }

        public override string ToString() {
            return Name + " " + Attack + ", " + Defense;
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
