using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Items;
using VH.Engine.Translations;
using VH.Game.World.Items.Weapons;

namespace VH.Game.World.Beings {

    public class WeaponSlot: EquipmentSlot {

        public static readonly string ID = "weapon-slot";

        public WeaponSlot() : base() {
            id = ID;
        }

        public override bool IsItemCompatible(Item item) {
            return item is Weapon;
        }
    }
}
