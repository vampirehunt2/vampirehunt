using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Game.World.Items.Potions;
using VH.Game.World.Items.Scrolls;
using VH.Engine.World.Items;
using VH.Game.World.Items;

namespace VH.Game.World.Beings.Actions {

    public class StackingUseItemAction : Engine.World.Beings.Actions.AbstractAction {

        public StackingUseItemAction(Being performer) : base(performer) { }

        public override bool Perform() {
            object[] objects = ((IBackPackBeing)performer).BackPack.Items.ToArray();
            if (objects.Length == 0) return false;
            Item item = (Item)selectTarget(objects);
            //
            UsableItem usableItem = null;
            if (item is UsableItem) usableItem = (UsableItem)item;
            if (item is ItemStack) usableItem = (UsableItem)((ItemStack)item).Item;
            //
            if (usableItem == null) return false;
            if (usableItem is Potion) return new StackingDrinkAction(performer, usableItem).Perform();
            if (usableItem is Scroll) return new StackingReadAction(performer, usableItem).Perform();
            return true;
        }

    }
}