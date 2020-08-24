using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class BobokAi : HostileAi {

        #region constants

        private const float STEAL_RATE = 0.3f;
        private const float JUMP_RATE = 0.02f;

        #endregion

        #region constructors

        public BobokAi() {
        }

        public BobokAi(Being being) : base(being) {
        }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();

            if (Rng.Random.NextFloat() < JUMP_RATE) return new JumpAction(Being);

            if (action is AttackAction) {
                AttackAction attackAction = action as AttackAction;
                Being attackee = attackAction.Attackee;
                if (Rng.Random.NextFloat() < STEAL_RATE && attackee is IBackPackBeing) {
                    return new StealAction(Being, attackee);
                } else {
                    return attackAction;
                }
            }

            return action;
        }

        #endregion

    }
}
