using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Levels;
using VH.Game.World.Beings.Actions;
using VH.Engine.Game;
using VH.Engine.World.Beings.AI;

namespace VH.Game.World.Beings.Ai {

    public class HostileAi: BaseAi {

        #region constructors

        public HostileAi() : base() { }

        public HostileAi(Being being) : base(being) { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            // try to find oponent
            Being oponent = getOponent();
            if (oponent != null) {
                if (isAdjacentTo(oponent)) return new MeleeAttackAction(Being, oponent);
                else return new MoveAction(Being, getStepTowards(getPossibleSteps(Being, oponent.Position)));
            }
            // try to move in a random direction
            Step step = Step.CreateRandomStep();
            Position position = Being.Position.AddStep(step);
            if (GameController.Instance.IsFreeSpace(position, Being) ||
                GameController.Instance.Level.Map[position] == Terrain.Get("closed-door").Character) {
                return new MoveAction(Being, step);
            }
            // finally, just hang around
            else return new WaitAction(Being);
        }

        #endregion
    }
}
