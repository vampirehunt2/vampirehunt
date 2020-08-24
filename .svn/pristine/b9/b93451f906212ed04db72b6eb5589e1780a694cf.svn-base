using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Engine.World.Beings.AI {

    public class ChaseBehavior: OponentDependantBehaviour {

        public ChaseBehavior() : base() { }

        public ChaseBehavior(Being being, Being oponent)
            : base(being) {
                Oponent = oponent;
        }

        public override AbstractAction SelectAction() {
            if (isAdjacentTo(Oponent)) return new AttackAction(Being, Oponent);
            else return new MoveAction(Being, getStepTowards(getPossibleSteps(Being, Oponent.Position)));
        }

        protected bool isAdjacentTo(Being oponent) {
            return Math.Max(Math.Abs(Being.Position.X - oponent.Position.X),
                Math.Abs(Being.Position.Y - oponent.Position.Y)) == 1;
        }


    }
}
