using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Actions {

    public class ManageEquipmentAction: Engine.World.Beings.Actions.AbstractAction {

        public ManageEquipmentAction(Being performer) : base(performer) { } 

        public override bool Perform() {
            object[] slots = ((IEquipmentBeing)performer).Equipment.Slots.ToArray();
            EquipmentSlot slot = (EquipmentSlot)selectTarget(slots);
            if (slot == null) return false;
            if (slot.Item != null) {
                slot.Item.Position = Performer.Position;
                return putIntoBackPack(slot);
            } else {
                return equip(slot);
            }
        }

        private bool putIntoBackPack(EquipmentSlot slot) {
            ((IBackPackBeing)performer).BackPack.Add(slot.Item);
            notify("item-deequipped", slot.Item);
            slot.Item = null;
            return true;
        }

        private bool equip(EquipmentSlot slot) {
            return new EquipAction(performer, slot).Perform();
        }
    }
}
