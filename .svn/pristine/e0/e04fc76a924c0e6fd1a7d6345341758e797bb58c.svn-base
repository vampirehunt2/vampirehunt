using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Actions {

    public class CauseIllnessAction: Engine.World.Beings.Actions.AbstractAction {

        private const float RESISTANCE = 0.5f;

        public CauseIllnessAction(): base(null) { }
        public CauseIllnessAction(Being performer) : base(performer) { }

        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (!temps["ill"] && (!temps["illness-resistance"] || Rng.Random.NextFloat() > RESISTANCE)) {
                temps["ill"] = true;
                notify("ill");
            } else {
                notify("not-ill");
            }
            return true;
        }
    }
}
