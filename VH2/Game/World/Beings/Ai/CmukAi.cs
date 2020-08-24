using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Ai {

    public class CmukAi: FlyingAnimalAi {

        private const float CAUSE_BLINDNESS_RATE = 0.15f;

        public CmukAi() : base() { }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is AttackAction && Rng.Random.NextFloat() < CAUSE_BLINDNESS_RATE) {
                action = new CauseBlindnessAction(getOponent());
            }
            return action;
        }

    }
}
