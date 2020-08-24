using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;

namespace VH.Engine.World.Items {

    public interface IEquipableItem {

        void OnEquip();

        void OnDeequip();

        int Attack { get; set; }

        int Defense { get; set; }

        Stat StatModifier { get; set; }

    }
}
