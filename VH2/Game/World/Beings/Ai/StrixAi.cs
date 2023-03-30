using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class StrixAi: HostileAi {

        public StrixAi() : base() { }

        public override AbstractAction SelectAction() {
            Engine.World.Beings.Actions.AbstractAction action = base.SelectAction();
            if (action is MeleeAttackAction) {
                action = new SuckLifeAction(Being, (action as MeleeAttackAction).Attackee);
            }
            return action;
        }
    }
}
