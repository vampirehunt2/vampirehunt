using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class ConsumeToadVenomAction: Engine.World.Beings.Actions.AbstractAction {


        public ConsumeToadVenomAction() : base(null) { }
        public ConsumeToadVenomAction(Being performer) : base(performer) { }


        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["poison resistant"]) {
                notify("bitter");
            } else { 
                temps["poison resistant"] = true;
                temps["poisoned"] = true;
                notify("poisoned");
            }
            return true;
        }
    }
}
