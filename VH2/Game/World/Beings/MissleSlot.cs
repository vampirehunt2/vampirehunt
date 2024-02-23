using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;
using VH.Game.World.Items.Weapons;

namespace VH.Game.World.Beings {
    public class MissleSlot: EquipmentSlot {

        public static readonly string ID = "missle-slot";

        public MissleSlot() : base() {
            id = ID;
        }

        public override bool IsItemCompatible(Item item) {
            return item is Ammo;
        }

    }
}
