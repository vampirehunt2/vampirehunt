using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class ConsumeWormwoodAction : Engine.World.Beings.Actions.AbstractAction {

        public ConsumeWormwoodAction() : base(null) { }
        public ConsumeWormwoodAction(Being performer) : base(performer) { }


        public override bool Perform() {
            notify("bitter");
            notify("warmth");
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["ill"]) {
                temps["ill"] = false;
                notify("cure");
            }
            //
            new CauseConfusionAction(Performer).Perform();
            return true;
        }
    }
}
