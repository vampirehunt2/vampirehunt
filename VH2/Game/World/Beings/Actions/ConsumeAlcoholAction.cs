using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Random;
using VH.Game.World.Beings.Professions;

namespace VH.Game.World.Beings.Actions {

    public class ConsumeAlcoholAction: Engine.World.Beings.Actions.AbstractAction {

        private const float CURE_RATE = 0.2f;
        private const float DRUNK_RATE = 0.5f;

        public ConsumeAlcoholAction() : base(null) { }
        public ConsumeAlcoholAction(Being performer) : base(performer) { }


        public override bool Perform() {
            //
            if (Performer.Health <= Performer.MaxHealth - 2) {
                Performer.Health += Rng.Random.Next(2) + 1;
                notify("warmth");
            }
            //
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["ill"] && Rng.Random.NextFloat() < CURE_RATE) {
                temps["ill"] = false;
                notify("cure");
            }
            //
            if (Rng.Random.NextFloat() < DRUNK_RATE) new CauseConfusionAction(Performer).Perform();
            return true;
        }
    }
}
