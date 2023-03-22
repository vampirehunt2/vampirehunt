using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.AI;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Configuration;
using VH.Engine.Game;
using VH.Game.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Levels;
using VH.Engine.Display;
using VH.Engine.World.Items;
using VH.Engine.Translations;
using VH.Game.World.Items;

namespace VH.Game.World.Beings.Ai {

    public class PcAi: AbstractAi {

        #region fields

        Keybindings keybindings = Keybindings.Instance;
        MessageWindow messageWindow = ((VhGameController)GameController.Instance).MessageWindow;
        MessageManager messageManager = GameController.Instance.MessageManager;

        #endregion

        #region constructors

        public PcAi() { }

        public PcAi(Pc pc) : base(pc) { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            Pc pc = (Pc)Being;
            string command = ((VhGameController)GameController.Instance).Command;
            Engine.World.Beings.Actions.AbstractAction action = null;
            //
            if (command == "wait") action = new WaitAction(pc);
            //
            else if (command == "north") action = new MoveAction(pc, Step.NORTH);
            else if (command == "south") action = new MoveAction(pc, Step.SOUTH);
            else if (command == "east") action = new MoveAction(pc, Step.EAST);
            else if (command == "west") action = new MoveAction(pc, Step.WEST);
            else if (command == "north-east") action = new MoveAction(pc, Step.NORTH_EAST);
            else if (command == "north-west") action = new MoveAction(pc, Step.NORTH_WEST);
            else if (command == "south-east") action = new MoveAction(pc, Step.SOUTH_EAST);
            else if (command == "south-west") action = new MoveAction(pc, Step.SOUTH_WEST);
            else if (command == "take-stairs") action = new TakeStairsAction(pc);
            else if (command == "close-door") action = new CloseDoorAction(pc);
            else if (command == "pick-up") action = new VhStackingPickUpAction(pc);
            else if (command == "drop") action = new StackingDropAction(pc);
            else if (command == "use") action = new StackingUseItemAction(pc);
            else if (command == "manage-equipment") action = new ManageEquipmentAction(pc);
            else if (command == "shoot") action = new ShootAction(pc, 10);
            //
            return action;
        }
        
        public override bool InteractWithEnvironment(Position position) {
            if (new DigAction(Being, position).Perform()) return true;
            if (new ChopAction(Being, position).Perform()) return true;
            return base.InteractWithEnvironment(position);
        }

        public override object SelectTarget(object[] objects, Engine.World.Beings.Actions.AbstractAction action) {
            if (action is StackingDropAction) return selectDropTarget(objects);
            if (action is PickUpAction) return selectPickUpTarget(objects);
            if (action is CloseDoorAction) return selectDirection(objects);
            if (action is ShootAction) return selectDirection(objects);
            if (action is ManageEquipmentAction) return selectDeequipTarget(objects);
            if (action is EquipAction) return selectEquipTarget(objects);
            if (action is StackingUseItemAction) return selectUseTarget(objects);
            return null;
        }

        public override void Notify(string messageKey) {
            messageManager.ShowMessage(messageKey, Being);
        }

        public override void Notify(string messageKey, bool force) {
            messageManager.ShowMessage(messageKey, Being, force);
        }

        public override void Notify(string messageKey, AbstractEntity target) {
            messageManager.ShowMessage(messageKey, Being, (AbstractEntity)target);
        }

        public override void Stimulate(Stimulus stimulus) {
            // ignore for now
        }

        #endregion

        #region private methods

        private object selectDropTarget(object[] objects) {
            List<Item> items = ((IBackPackBeing)Being).BackPack.Items;
            Menu menu = createMenu(Translator.Instance["drop"], items.ToArray());
            if (menu.ShowMenu() == MenuResult.OK) {
                GameController.Instance.MessageWindow.Clear();
                return menu.SelectedItem;
            } else {
                GameController.Instance.MessageWindow.Clear();
                return null;
            }
        }

        private object selectPickUpTarget(object[] objects) {
            Menu menu = new LetterMenu(Translator.Instance["pick-up"], objects, messageWindow);
            if (menu.ShowMenu() == MenuResult.OK) {
                GameController.Instance.MessageWindow.Clear();
                return menu.SelectedItem;
            } else {
                GameController.Instance.MessageWindow.Clear();
                return null;
            }
        }

        private object selectDirection(object[] objects) {
            GameController.Instance.MessageManager.ShowMessage("which-direction", Being);
            char key = GameController.Instance.Console.ReadKey();
            Step direction = ((VhGameController)GameController.Instance).Keybindings.GetStepForKey(key);
            return direction;
        }

        private object selectDeequipTarget(object[] objects) {
            return getMenuSelection("choose-slot", objects);
        }

        private object selectEquipTarget(object[] objects) {
            return getMenuSelection("select-item", objects);
        }

        private object selectUseTarget(object[] objects) {
            List<Item> usableItems = new List<Item>();
            foreach (Item item in objects) {
                if (item is UsableItem ||
                   (item is ItemStack && ((ItemStack)item).Item is UsableItem)) {
                    usableItems.Add(item);
                }
            }
            return getMenuSelection("select-item", usableItems.ToArray());
        }


        // helper methods below

        private object getMenuSelection(string titleKey, object[] objects) {
            string title = Translator.Instance[titleKey];
            Menu menu = createMenu(title, objects);
            if (menu.ShowMenu() == MenuResult.OK) {
                GameController.Instance.MessageWindow.Clear();
                return menu.SelectedItem;
            } else {
                GameController.Instance.MessageWindow.Clear();
                return null;
            }
        }

        private Menu createMenu(string title, object[] objects) {
            return new LetterMenu(title, objects, messageWindow);
        }

        #endregion
    }
}
