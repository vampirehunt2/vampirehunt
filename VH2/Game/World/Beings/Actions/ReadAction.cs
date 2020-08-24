using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Game.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {

    public class ReadAction: AbstractAction{

         private UsableItem item;

         public ReadAction(Being performer, UsableItem item)
            : base(performer) {
            this.item = item;
        }

        public override bool Perform() {
            ((IBackPackBeing)performer).BackPack.Remove(item);
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
