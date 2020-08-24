using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Actions {

    public class CauseConfusionAction: Engine.World.Beings.Actions.AbstractAction {

        private const float RESISTANCE = 0.5f;

        public CauseConfusionAction(): base(null) { }
        public CauseConfusionAction(Being performer) : base(performer) { }

        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (!temps["confusion-resistance"] || Rng.Random.NextFloat() > RESISTANCE) {
                temps["confused"] = true;
                notify("confused");
            } else {
                notify("not-confused");
            }
            return true;
        }
    }
}
