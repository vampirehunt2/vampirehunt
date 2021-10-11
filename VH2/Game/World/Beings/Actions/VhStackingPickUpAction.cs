using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class VhStackingPickUpAction : PickUpAction {

        public VhStackingPickUpAction(): base(null) { }

        public VhStackingPickUpAction(Being performer) : base(performer) { }

        public override bool Perform() {
            if (SelectItem()) {
                new ExamineItemAction(performer, item).Perform();
                return PickUp();
            }
            return false;
        }

    }
}
