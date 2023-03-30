using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;
using VH.Engine.World.Items.Weapons;

namespace VH.Game.World.Beings {
    public class MissleWeaponSlot: EquipmentSlot {


        public static readonly string ID = "missle-weapon-slot";

        public MissleWeaponSlot() : base() {
            id = ID;
        }

        public override bool IsItemCompatible(Item item) {
            return item is MissleWeapon;
        }
    }
}
