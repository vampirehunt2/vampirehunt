using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.Game;

namespace VH.Game.World.Beings.Actions {

    public class DropAction: AbstractAction {

        public DropAction(Being performer): base(performer) { }

        public override bool Perform() {
            object[] objects = ((IBackPackBeing)performer).BackPack.Items.ToArray();
            if (objects.Length == 0) return false;
            Item item = (Item)selectTarget(objects);
            if (item == null) return false; 
            ((IBackPackBeing)performer).BackPack.Remove(item);
            item.Position = performer.Position.Clone();
            GameController.Instance.Level.Items.Add(item);
            notify("drop", item);
            return true;
        }
    }
}
