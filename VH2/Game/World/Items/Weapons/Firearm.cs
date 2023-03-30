using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;
using VH.Engine.World.Items.Weapons;

namespace VH.Game.World.Items.Weapons {
    public class Firearm: MissleWeapon {

        public override bool IsCompatibleMissle(Item item) {
            return item.Id == "round";
        }

    }
}
