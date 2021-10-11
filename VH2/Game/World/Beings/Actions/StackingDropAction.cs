using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.Display;

namespace VH.Game.World.Beings.Actions {

    public class StackingDropAction: AbstractAction {

        public StackingDropAction(Being performer): base(performer) { }

        public override bool Perform() {
            StackingBackPack backpack = ((IBackPackBeing)performer).BackPack;
            object[] objects = backpack.Items.ToArray();
            if (objects.Length == 0) return false;
            Item item = (Item)selectTarget(objects);
            if (item == null) return false;
            if (item is ItemStack) {
                ItemStack stack = (ItemStack)item;
                QuantityMenu menu = new QuantityMenu(GameController.Instance.MessageWindow);
                if (menu.ShowMenu() != MenuResult.OK) {
                    return false;
                } else {
                    if (menu.Quantity <= 0) return false;
                    if (menu.Quantity > stack.Count) return false;
                    if (menu.Quantity == 1) {
                        if (stack.Count > 2) {
                            Item toDrop = stack.Item;
                            stack.RemoveFirst();
                            putDown(toDrop);
                        } else {
                            backpack.Remove(stack);
                            Item toDrop = stack.Item;
                            stack.RemoveFirst();
                            Item toKeep = stack.Item;
                            stack.RemoveFirst();
                            putDown(toDrop);
                            backpack.Add(toKeep);
                        }
                    } else if (menu.Quantity == stack.Count - 1) {
                        Item toKeep = stack.Item;
                        stack.RemoveFirst();
                        stack.Position = performer.Position.Clone();
                        backpack.Remove(stack);
                        backpack.Add(toKeep);
                        putDown(stack);
                    } else if (menu.Quantity > 1 && menu.Quantity < stack.Count - 1) {
                        ItemStack toDrop = stack.Split(menu.Quantity);
                        putDown(toDrop);
                    }
                }
            } else {
                backpack.Remove(item);
                putDown(item);
            }
            return true;
        }

        private void putDown(Item item) {
            item.Position = performer.Position.Clone();
            GameController.Instance.Level.Items.Add(item); //TODO check if one or more of the same items 
                                                           //are not already on the floor in this spot
                                                           //if so, stack them up
            notify("drop", item);
        }
    }
}
