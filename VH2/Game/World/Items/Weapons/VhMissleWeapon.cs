using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace VH.Engine.World.Items.Weapons {
    public class VhMissleWeapon: MissleWeapon {

        public override string ToString() {
            return Name + " +" + Attack;
        }

        public override bool IsCompatibleMissle(Item item) {
            return true;
        }

    }

}
