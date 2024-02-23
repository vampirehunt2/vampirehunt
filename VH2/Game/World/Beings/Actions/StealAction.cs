using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Actions {

    public class StealAction : MeleeAttackAction {

        public StealAction(Being performer, Being attackee) : base(performer, attackee) {
        }

        public override bool Perform() {
            if (Performer is IBackPackBeing && Attackee is IBackPackBeing) {
                StackingBackPack performerBackPack = ((IBackPackBeing)Performer).BackPack as StackingBackPack;
                StackingBackPack attackeeBackPack = ((IBackPackBeing)Attackee).BackPack as StackingBackPack;
                if (!performerBackPack.Full) {
                    if (dxCheck()) {
                        notify("unsuccessful-steal");
                        return true;
                    }
                    int count = attackeeBackPack.Items.Count;
                    if (count > 0) {
                        int i = Rng.Random.Next(count);
                        Item item = attackeeBackPack.Items[i];
                        attackeeBackPack.Remove(item);
                        performerBackPack.Items.Add(item);
                        notify("steal", Attackee);
                    } else {
                        notify("empty-backpack-steal", Attackee);
                    }
                }
            }
            return true;
        }

        private bool dxCheck() {
            if (Attackee is IStatBeing) {
                IStatBeing statBeing = Attackee as IStatBeing;
                Stat dx = statBeing.Stats["Dx"];
                if (dx != null) {
                    int dxValue = dx.Value;
                    if (Rng.Random.Next(VhPc.MAX_STAT_VALUE) < dxValue) return true;
                }
            }
            return false;
        }
    }
}
