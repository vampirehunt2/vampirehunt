using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace VH.Engine.World.Items.Weapons {
    public class MissleWeapon: Weapon {

        public override string ToString() {
            return Name + " +" + Attack;
        }

        public virtual bool IsCompatibleMissle(Item item) {
            return true;
        }

    }

}
