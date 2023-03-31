using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Items;
using VH.Game.World.Items.Missles;
using VH.Game.World.Items.Weapons;

namespace VH.Game.World.Beings {
    public class MissleSlot: EquipmentSlot {

        public static readonly string ID = "missle-slot";

        public MissleSlot() : base() {
            id = ID;
        }

        public override bool IsItemCompatible(Item item) {
            return item is Missle ||
                item is ItemStack && (item as ItemStack).Item is Missle;
        }

        public Missle NextMissle() {
            if (Item == null) return null;
            if (Item is Missle) {
                Missle missle = (Missle)Item;
                Item = null;
                return missle;
            }
            if (Item is ItemStack) {
                ItemStack stack = (ItemStack)Item;
                if (stack.Count == 0) return null;
                Item stackedItem = stack.Item;
                if (stackedItem is Missle) {
                    stackedItem = stack.RemoveFirst();
                    if (stack.Count == 0) Item = null;
                    return (Missle)stackedItem;
                }
            }
            return null;
        }
    }
}
