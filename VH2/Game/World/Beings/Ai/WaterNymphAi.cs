using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class WaterNymphAi : HostileAi {

        private const float CONFUSE_RATE = 0.05f;

        public WaterNymphAi() {
        }

        public WaterNymphAi(Being being) : base(being) {
        }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is MeleeAttackAction && Rng.Random.NextFloat() < CONFUSE_RATE) {
                Being attackee = (action as MeleeAttackAction).Attackee;
                Notify("sing", attackee);
                return new CauseConfusionAction(attackee);
            }
            return action;
        }

    }
}
