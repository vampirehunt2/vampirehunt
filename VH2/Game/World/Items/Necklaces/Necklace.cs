using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace VH.Game.World.Items.Necklaces {

    public class Necklace : Item, IEquipableItem {

        public int Attack { 
            get { return 0; }
            set { /* do nothing */ }
        }

        public int Defense {
            get { return 0; }
            set { /* do nothing */ }
        }

        public Stat StatModifier {
            get { return null; }
            set { /* do nothing */ }
        }

        public void OnDeequip() {
            // do nothing for now, TODO implement temps
        }

        public void OnEquip() {
            // do nothing for now, TODO implement temps
        }

        public override string ToString() {
            return Name;
        }
    }
}
