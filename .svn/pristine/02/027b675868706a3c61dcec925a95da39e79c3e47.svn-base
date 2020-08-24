using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Game.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.Random;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {

    public class ZapAction: AbstractAction {

        private const float WAND_DESTROY_RATE = 0.2f;

        private UsableItem item;


        public ZapAction(Being performer, UsableItem item)
            : base(performer) {
            this.item = item;
        }

        public override bool Perform() {
            if (Rng.Random.NextFloat() < WAND_DESTROY_RATE) {
                ((IBackPackBeing)performer).BackPack.Remove(item);
                notify("wand-destroy");
            }
            item.Position = performer.Position.Clone();
            notify(item.UseKind, item);
            item.Use(performer);
            return true;
        }
    }
}
