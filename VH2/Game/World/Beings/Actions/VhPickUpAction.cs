using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class VhPickUpAction : PickUpAction {

        public VhPickUpAction(): base(null) { }

        public VhPickUpAction(Being performer) : base(performer) { }

        public override bool Perform() {
            bool result = base.Perform();
            if (result) new ExamineItemAction(performer, item).Perform();
            return result;
        }

    }
}
