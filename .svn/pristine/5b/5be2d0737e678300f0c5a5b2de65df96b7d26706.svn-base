using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class ConsumeRueAction : Engine.World.Beings.Actions.AbstractAction {

        public ConsumeRueAction() : base(null) { }
        public ConsumeRueAction(Being performer) : base(performer) { }


        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["poisoned"]) {
                temps["poisoned"] = false;
                notify("cure");
            }
            return true;
        }
    }
}
