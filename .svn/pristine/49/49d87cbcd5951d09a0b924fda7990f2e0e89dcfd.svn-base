using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Actions {

    public class IncreaseStrengthAction: Engine.World.Beings.Actions.AbstractAction {

        public IncreaseStrengthAction(): base(null) { }

        public IncreaseStrengthAction(Being performer) : base(performer) { }

        public override bool Perform() {
            if (performer is IStatBeing) {
                Stat strength = (Performer as IStatBeing).Stats["St"];
                if (Rng.Random.Next(VhPc.MAX_STAT_VALUE) > strength.Value) {
                    strength.Value++;
                    notify("strength-increased");
                } else {
                    notify("strength-not-increased");
                }
            }
            return true;
        }
    }
}
