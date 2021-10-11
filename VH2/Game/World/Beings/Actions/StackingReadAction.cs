using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Game.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {

    public class StackingReadAction: AbstractAction{

         private UsableItem item;

         public StackingReadAction(Being performer, UsableItem item)
            : base(performer) {
            this.item = item;
        }

        public override bool Perform() {
            ((IBackPackBeing)performer).BackPack.RemoveSingleItem(item);
            item.Position = performer.Position.Clone();
            if (((ISkillsBeing)performer).Skills["reading"].Roll()) {
                notify(item.UseKind, item);
                item.Use(performer);
            }
            else {
                notify("read-failed", item);
            }
            return true;
        }
    }
}
