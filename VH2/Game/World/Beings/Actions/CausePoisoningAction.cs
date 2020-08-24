using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class CausePoisoningAction: Engine.World.Beings.Actions.AbstractAction {

        private const float RESISTANCE = 0.3f;

        public CausePoisoningAction(): base(null) { }
        public CausePoisoningAction(Being performer) : base(performer) { }

        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (!temps["poisoned"] && (!temps["poison-resistance"] || Rng.Random.NextFloat() > RESISTANCE)) {
                temps["poisoned"] = true;
                notify("poisoned");
            } else {
                notify("not-poisoned");
            }
            return true;
        }

    }
}
