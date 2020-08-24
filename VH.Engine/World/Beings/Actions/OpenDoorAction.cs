using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.Random;

namespace VH.Engine.World.Beings.Actions {

    public class OpenDoorAction: AbstractAction {

        Step direction;

        public OpenDoorAction(Being performer, Step direction): base(performer) {
            this.direction = direction;
        }

        public override bool Perform() {
            Position doorPosition = performer.Position.AddStep(direction);
            if (GameController.Instance.Map[doorPosition] != Terrain.Get("closed-door").Character) {
                return false;
            }
            // TODO move CanOpenDoor to Being class. Implement as return true in Pc class.
            if (performer is Monster && !((Monster)performer).CanOpenDoor) {
                notify("bash-door");
                return true;
            }
            if (Rng.Random.Next(10) > 7) {
                notify("stuck-door");
            } else {
                GameController.Instance.Map[doorPosition] = Terrain.Get("open-door").Character;
                notify("open-door");
            }
            return true;
        }
    }
}
