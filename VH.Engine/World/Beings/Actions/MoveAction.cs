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

        #region fields

        protected Step step;
        protected bool canTryOpenDoors;
        protected Position newPosition;
        protected GameController controller = GameController.Instance;
        

        #endregion

        #region constructors

        public MoveAction(Being performer, Step step): base(performer) {
            this.step = step;
            newPosition = performer.Position.AddStep(step);
        }

        #endregion

        #region properties

        protected char Feature {
            get { return controller.ViewPort.GetDisplayCharacter(controller.Map[newPosition]); }
        }

        protected Being BeingAtNewPosition {
            get { return controller.GetBeingAt(newPosition); }
        }

        #endregion

        #region public methods

        public override bool Perform() {

            // check if new position is within level limits
            if (newPosition.X < 0 || newPosition.Y < 0 ||
                newPosition.X >= controller.Map.Width ||
                newPosition.Y >= controller.Map.Height) {
                return false;
            }
            // moving onto a wall results in an attept to dig through it

            // moving into a Being results in attacking it
            if (BeingAtNewPosition != null && BeingAtNewPosition != performer) {
                return new AttackAction(performer, BeingAtNewPosition).Perform();
            }

            // check if performer is able to walk through whatever is at the level at new position, otherwise
            // interact with the terrain at the new position
            // for example:moving onto a wall results in an attept to dig through it
            
            if (!performer.CanWalkOn(Feature)) {
                if (Performer.Ai.InteractWithEnvironment(newPosition)) return true;
                return false;
            }

            // perform move
            performer.Position = performer.Position.AddStep(step);
            showItemNames();
            return true;
        }

        #endregion

        #region protected methods

        protected virtual void showItemNames() {
            string itemNames = getItemNames();
            if (itemNames.Length > 0 && performer.Person == Person.Second) {
                controller.MessageManager.ShowDirectMessage(itemNames);
             }
        }

        protected string getItemNames() {
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
