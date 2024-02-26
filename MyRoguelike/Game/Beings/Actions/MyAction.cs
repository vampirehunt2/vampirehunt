using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace MyRoguelike.Game.Beings.Actions {
    public class MyAction : AbstractAction {
        public MyAction(Being performer) : base(performer) {}

        public override bool Perform() {
            // here you can put code that should be executed on every game turn
            return true;
        }
    }
}
