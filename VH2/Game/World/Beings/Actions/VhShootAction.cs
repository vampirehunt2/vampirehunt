using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Items.Weapons;
using VH.Game.World.Items.Missles;

namespace VH.Game.World.Beings.Actions {
    public class VhShootAction: ShootAction {

        public VhShootAction(Being performer): base(performer, 10) {
        }

        public override bool Perform() {
            if (!(performer is IEquipmentBeing)) return false;
            EquipmentSlot slot = (performer as IEquipmentBeing).Equipment[WeaponSlot.ID];
            if (slot == null) return false;
            Item item = slot.Item;
            if (item == null || !(item is MissleWeapon)) {
                notify("no-missle-weapon");
                return false;
            }
            AbstractAttackAction attackAction = new MissleAttackAction(performer);
            Attack = attackAction;
            return base.Perform();
        }

        protected override void missleStep() {
            GameController controller = GameController.Instance;
            char character = controller.ViewPort.GetDisplayCharacter(controller.Map[pos]);
            ConsoleColor color = controller.ViewPort.GetDisplayColor(controller.Map[pos]);
            ItemFacade facade = new ItemFacade();
            Missle missleAnimation = (Missle)facade.CreateItemById("arrow");
            missleAnimation.Position = pos;
            controller.ViewPort.Display(missleAnimation, controller.FieldOfVision, performer.Position);
            controller.ViewPort.Refresh(pos); 
            Thread.Sleep(100);
            missleAnimation.Character = character;
            missleAnimation.Color = color;
            controller.ViewPort.Display(missleAnimation, controller.FieldOfVision, performer.Position);
            controller.ViewPort.Refresh(pos);
        }

    }
}
