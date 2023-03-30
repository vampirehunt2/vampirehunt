using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class ViperAi: AnimalAi {

        private const float POISONING_ATACK_RATE = 0.20f;


        public ViperAi() : base() { }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is MeleeAttackAction && Rng.Random.NextFloat() < POISONING_ATACK_RATE) {
                action = new CausePoisoningAction((action as MeleeAttackAction).Attackee);
            }
            return action;
        }
    }
}
