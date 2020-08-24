using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class SuckStrengthAction : AttackAction {

        private const float SUCK_RATE = 0.2f;

        public SuckStrengthAction(Being performer, Being attackee) : base(performer, attackee) { }

        public override bool Perform() {
            bool result = base.Perform();
            if (Rng.Random.NextFloat() < SUCK_RATE) {
                if (Attackee is IStatBeing) {
                    StatSet stats = (Attackee as IStatBeing).Stats;
                    Stat st = stats["St"];
                    if (st != null) {
                        if (st.Value > 1) {
                            st.Value--;
                            notify("suck-strength", Attackee);
                            return true;
                        }
                    }
                }
            }
            return result;
        }
    }
}
