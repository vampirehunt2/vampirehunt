using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Engine.Levels;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;
using VH.Engine.Game;

namespace VH.Engine.World.Beings.AI {

    public class NeutralBehavior: BaseAi {

        private const float MOVE_RATE = 0.3f;

        public NeutralBehavior(): base() { }

        public NeutralBehavior(Being being) : base(being) { }

        public override AbstractAction SelectAction() {
            // pick up an item, if possible
            if (Being is IBackPackBeing) {
                IEnumerable<Item> items = GameController.Instance.Level.GetItemsAt(Being.Position);
                if (items.Count() > 0 && !((IBackPackBeing)Being).BackPack.Full) {
                   // TODO return new PickUpAction(Being);
                }
            }
            // try to move in a random direction
            Step step = Step.CreateRandomStep();
            Position position = Being.Position.AddStep(step);
            if (GameController.Instance.IsFreeSpace(position, Being) ||
                GameController.Instance.Level.Map[position] == Terrain.Get("closed-door").Character) {
                return new MoveAction(Being, step);
            }
            // finally, just hang around
            else return new WaitAction(Being);
        }
    }
}
