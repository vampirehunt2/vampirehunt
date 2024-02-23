using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.Game;

namespace VH.Engine.World.Beings.Actions {

    public class PickUpAction: AbstractAction {

        #region fields

        protected Item item;

        #endregion

        #region constructors

        public PickUpAction(Being performer): base(performer) { }

        #endregion

        #region public methods

        public bool SelectItem() {
            // get the Item object to be picked up
            object[] objects = GameController.Instance.Level.GetItemsAt(performer.Position).ToArray();
            if (objects.Length == 0) {
                notify("no-items");
                return false;
            }
            if (objects.Length == 1) item = (Item)objects[0];
            else item = (Item)selectTarget(objects);
            // various ways in which adding items to the backpack may fail
            if (item == null) return false;
            if (!item.Position.Equals(performer.Position)) return false;
            return true;
        }

        public bool PickUp() {
            BackPack backpack = ((IBackPackBeing)performer).BackPack;
            if (backpack.Full) {
                notify("backpack-full", item);
                return false;
            }
            //
            backpack.Add(item);
            GameController.Instance.Level.Items.Remove(item);
            notify("pick-up", item);
            return true;
        }

        public override bool Perform() {
            if (SelectItem()) return PickUp();
            return false;
        }

        #endregion




    }
}
