using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Actions {

    public class EquipAction: AbstractAction {

        private EquipmentSlot slot;

        public EquipAction(Being performer, EquipmentSlot slot) : base(performer) {
            this.slot = slot;
        }

        public override bool Perform() {
            object[] items = (
                    from item in ((IBackPackBeing)performer).BackPack.Items
                    where slot.IsItemCompatible(item)
                    select item
                ).ToArray();
            if (items.Length == 0) {
                notify("no-suitable-items");
                return false;
            } else {
                Item item = (Item)selectTarget(items);
                if (item == null) return false;
                ((IBackPackBeing)performer).BackPack.Remove(item);
                slot.Item = item;
                item.Position = Performer.Position;
                notify("item-equipped", item);
                return true;
            }
        }
    }
}
