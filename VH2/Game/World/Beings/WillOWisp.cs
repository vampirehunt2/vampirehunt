using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.AI;
using VH.Game.World.Beings.Ai;

namespace VH.Game.World.Beings {
    public class WillOWisp: VhMonster {

        public override void Move(int gametimeTicks) {
            base.Move(gametimeTicks);
            if (Color == ConsoleColor.Red) Color = ConsoleColor.Yellow;
            else Color = ConsoleColor.Red;
        }

    }
}
