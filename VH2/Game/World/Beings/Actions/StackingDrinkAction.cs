using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Game.World.Items;

namespace VH.Game.World.Beings.Actions {

    public class StackingDrinkAction: AbstractAction{

        private UsableItem item;

        public StackingDrinkAction(Being performer, UsableItem item)
            : base(performer) {
            this.item = item;
        }

        public override bool Perform() {
            (((IBackPackBeing)performer).BackPack as StackingBackPack).RemoveSingleItem(item);
            item.Position = performer.Position.Clone();
            notify(item.UseKind, item);
            item.Use(performer);
            return true;
        }
    }
}
