using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Items;
using VH.Game.World.Items.Potions;
using VH.Game.World.Items.Scrolls;

namespace VH.Game.World.Beings.Actions {

    public class UseItemAction : Engine.World.Beings.Actions.AbstractAction {

        public UseItemAction(Being performer) : base(performer) { }

        public override bool Perform() {
            object[] objects = ((IBackPackBeing)performer).BackPack.Items.ToArray();
            if (objects.Length == 0) return false;
            UsableItem item = (UsableItem)selectTarget(objects);
            if (item == null) return false;
            if (item is Potion) return new DrinkAction(performer, item).Perform();
            if (item is Scroll) return new ReadAction(performer, item).Perform();
            return true;
        }

    }
}