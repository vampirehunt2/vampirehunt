using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;
using VH.Game.World.Items.Weapons;

namespace VH.Game.World.Items.MissleWeapons {
    public class MissleWeapon: Weapon {
        public bool IsCompatibleMissle(Item item) {
            return true;
        }

    }

}
