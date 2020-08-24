using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.AI;
using VH.Engine.Random;
using VH.Game.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Display;
using VH.Engine.Levels;

namespace VH.Game.World.Beings.Ai {

    public class FlyingAnimalAi: AnimalAi {

        public FlyingAnimalAi() {
        }

        public override AbstractAction SelectAction() {
            if (Rng.Random.NextFloat() < 0.5) return base.SelectAction();
            else return new MoveAction(Being, Step.CreateRandomStep());
        }

    }
}
