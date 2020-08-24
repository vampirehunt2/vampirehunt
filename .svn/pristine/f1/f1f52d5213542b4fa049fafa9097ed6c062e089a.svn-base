using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class VampireAi: HostileAi {

        public VampireAi() : base() { }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is AttackAction) {
                action = new SuckStrengthAction(Being, (action as AttackAction).Attackee);
            }
            return action;
        }

    }
}
