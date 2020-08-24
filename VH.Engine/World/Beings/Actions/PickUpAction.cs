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

        protected Item item;

        public PickUpAction(Being performer): base(performer) { }

        public override bool Perform() {
            object[] objects = GameController.Instance.Level.GetItemsAt(performer.Position).ToArray();
            if (objects.Length == 0) {
                notify("no-items");
                return false;
            }
            if (objects.Length == 1) item = (Item)objects[0];
            else  item = (Item)selectTarget(objects);
            if (item == null) return false;
            if (!item.Position.Equals(performer.Position)) return false;
            if (!((IBackPackBeing)performer).BackPack.Full) {
                ((IBackPackBeing)performer).BackPack.Add(item);
                GameController.Instance.Level.Items.Remove(item);
                notify("pick-up", item);
                return true;
            } else {
                notify("backpack-full", item);
                return false;
            }
        }

    }
}
