using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Ai {

    public class MothAi: FlyingAnimalAi {

        private const float CAST_DARKNESS_RATE = 0.2f;

        public MothAi() : base() { }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is AttackAction && Rng.Random.NextFloat() < CAST_DARKNESS_RATE) {
                action = new CastDarknessAction(Being);
            }
            return action;
        }

    }
}
