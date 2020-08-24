using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {

    public class SuckLifeAction: AttackAction {

        public SuckLifeAction(Being performer, Being attackee) : base(performer, attackee) { }

        public override bool Perform() {
            bool result = base.Perform();
            if (Performer.Health + damage < Performer.MaxHealth) {
                Performer.Health += damage;
                notify("suck-life", Attackee);
            }
            return result;
        }
    }
}
