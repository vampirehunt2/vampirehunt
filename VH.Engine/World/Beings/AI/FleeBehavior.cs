using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Engine.World.Beings.AI {

    public class FleeBehavior: OponentDependantBehaviour {

        public FleeBehavior(): base() { }

        public FleeBehavior(Being being, Being oponent)
            : base(being) {
                Oponent = oponent;
        }

        public override AbstractAction SelectAction() {
            return new MoveAction(Being, getStepAwayFrom(getPossibleSteps(Being, Oponent.Position)));
        }
    }
}
