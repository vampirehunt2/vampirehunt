using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.Game;
using VH.Engine.World.Items.Weapons;

namespace VH.Engine.World.Items {

    public  class Ammo: Weapon {

        #region constants

        const string NUMBER = "number";
        private static readonly int MAX_NUMBER = 25;

        #endregion

        #region fields

        private int number = Random.Rng.Random.Next(MAX_NUMBER);

        #endregion

        #region properties

        public int Number { 
            get { return number; } 
            set {  number = value; } 
        }

        #endregion

        #region public methods

        public bool Spend(int number) {
            if (Number >= number) {
                Number -= number;
                return true;
            } else {
                return false;
            }
        }

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            number = GetIntAttribute(NUMBER);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(NUMBER, number);
            return element;
        }

        public override string ToString() {
            if (number == 1) { return Name; }
            return Plural + " (x" + number + ")";
        }

        #endregion

    }
}
