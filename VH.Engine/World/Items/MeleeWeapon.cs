using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.World.Items.Weapons;

namespace VH.Game.World.Items.Weapons {

    /// <summary>
    /// Represents a mellee weapon.
    /// </summary>
    public class MeleeWeapon: Weapon {

        public override string ToString() {
            return Name + " +" + Attack + ", +" + Defense;
        }

    }
}
