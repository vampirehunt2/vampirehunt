using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.World.Items;
using VH.Engine.Display;
using VH.Engine.Random;

namespace VH.Engine.World.Beings.Actions {

    public class MoveAction: AbstractAction {

        #region constants

        private const float CONFUSION_RATE = 0.5f;

        #endregion

        #region fields

        private Step step;
        bool canTryOpenDoors;
        private GameController controller = GameController.Instance;

        #endregion

        #region public methods

        public MoveAction(Being performer, Step step): base(performer) {
            this.step = step;
        }

        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["confused"]) {
                // TODO confusion resistance may also be aquired through items
                if (!temps["confusion-resistance"] || Rng.Random.NextFloat() > CONFUSION_RATE) {
                    step = Step.CreateRandomStep();
                    notify("stagger");
                } else {
                    notify("no-stagger");
                }
            }
            Position newPosition = performer.Position.AddStep(step);

            // check if new position is within level limits
            if (newPosition.X < 0 || newPosition.Y < 0 ||
                newPosition.X >= controller.Map.Width ||
                newPosition.Y >= controller.Map.Height) {
                return false;
            }

            // moving into a closed door results in an attempt to open them
            if (performer.CanWalkOn(Terrain.Get("open-door").Character) && controller.Map[newPosition] == Terrain.Get("closed-door").Character) {
                return new OpenDoorAction(performer, step).Perform();
            }

            // moving onto a wall results in an attept to dig through it

            // moving into a Being results in attacking it
            Being being = controller.GetBeingAt(newPosition);
            if (being != null && being != performer) return new MeleeAttackAction(performer, being).Perform();

            // check if performer is able to walk through whatever is at the level at new position, otherwise
            // interact with the terrain at the new position
            // for example:moving onto a wall results in an attept to dig through it
            char feature = controller.ViewPort.GetDisplayCharacter(controller.Map[newPosition]);
            if (!performer.CanWalkOn(feature)) {
                if (Performer.Ai.InteractWithEnvironment(newPosition)) return true;
            }
            if (!performer.CanWalkOn(feature)  || (being != null && being != performer)) {
                if ((Performer as ITempsBeing).Temps["blind"]) notify("boom");
                return false;
            }

            // perform move
            performer.Position = performer.Position.AddStep(step);
            showItemNames();
            return true;
        }

        #endregion

        #region private methods

        private void showItemNames() {
            string itemNames = getItemNames();
            if (itemNames.Length > 0 && performer.Person == Person.Second) {
                if ((Performer as ITempsBeing).Temps["blind"]) notify("trip");
                else controller.MessageManager.ShowDirectMessage(itemNames);
             }
        }

        private string getItemNames() {
            IEnumerable<Item> items = GameController.Instance.Level.GetItemsAt(performer.Position);
            StringBuilder sb = new StringBuilder();
            foreach (Item item in items) {
                sb.Append(item.ToString() + "\n");
            }
            return sb.ToString().Trim();
        }

        #endregion

    }
}
