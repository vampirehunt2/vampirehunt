using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Levels;

namespace VH.Engine.World.Beings.Actions {

    public class WaitAction: AbstractAction {

        public WaitAction(Being performer) : base(performer) { }


        public override bool Perform() {
            // do nothing, just sit there and consume gamre ticks
            return new MoveAction(performer, Step.NONE).Perform();
        }


    }
}
