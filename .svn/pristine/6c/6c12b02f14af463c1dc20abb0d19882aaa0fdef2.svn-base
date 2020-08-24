using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Game;

namespace VH.Engine.World.Beings.Actions {

    public class CloseDoorAction: AbstractAction {

        public CloseDoorAction(Being performer): base(performer) { }

        public override bool Perform() {
            Step direction = (Step)selectTarget(Step.ALL);
            if (direction == null) return false;
            Position doorPosition = performer.Position.AddStep(direction);
            if (GameController.Instance.Map[doorPosition] != Terrain.Get("open-door").Character) {
                notify("no-door");
                return false;
            }
            if (GameController.Instance.GetBeingAt(doorPosition) != null ||
                GameController.Instance.Level.GetItemsAt(doorPosition).Count() > 0) {
                    notify("blocked-door");
                    return false;
            }
            GameController.Instance.Map[doorPosition] = Terrain.Get("closed-door").Character;
            notify("close-door");
            return true;
        }
    }
}
