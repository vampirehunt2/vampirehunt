using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Actions {

    public class CauseBlindnessAction: Engine.World.Beings.Actions.AbstractAction {

        private const float RESISTANCE = 0.5f;

        public CauseBlindnessAction(): base(null) { }
        public CauseBlindnessAction(Being performer) : base(performer) { }

        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (!temps["blind"] && (!temps["blindness-resistance"] || Rng.Random.NextFloat() > RESISTANCE)) {
                temps["blind"] = true;
                notify("blinded");
            } else {
                notify("not-blinded");
            }
            return true;
        }
    }
}
