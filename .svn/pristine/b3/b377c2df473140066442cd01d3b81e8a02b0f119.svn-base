using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {

    public class ConsumeBirchDecoctionAction : Engine.World.Beings.Actions.AbstractAction {

        public ConsumeBirchDecoctionAction() : base(null) { }
        public ConsumeBirchDecoctionAction(Being performer) : base(performer) { }


        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["ill"]) {
                temps["ill"] = false;
                notify("cure");
            } else {
                notify("refreshing");
            }
            return true;
        }
    }

}
