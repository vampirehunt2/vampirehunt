using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Items;
using VH.Engine.Translations;
using VH.Game.World.Items.BodyWear;

namespace VH.Game.World.Beings {

    public class ArmorSlot: EquipmentSlot {

        public ArmorSlot(): base() {
            id = "armor-slot";
        }

        public override bool IsItemCompatible(Item item) {
            return item is Armor && item.Character == '[';
        }
    }
}
