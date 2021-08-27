using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class ForestNymphAi : HostileAi {

        private const float CAUSE_AMNESIA_RATE = 0.05f;

        public ForestNymphAi() {
        }

        public ForestNymphAi(Being being) : base(being) {
        }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is AttackAction && Rng.Random.NextFloat() < CAUSE_AMNESIA_RATE) {
                Being attackee = (action as AttackAction).Attackee;
                Notify("sing", attackee);
                return new InflictAmnesiaAction(Being, attackee);
            }
            return action;
        }

    }
}
