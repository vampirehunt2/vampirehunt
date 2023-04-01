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

        Missle missle; 

        public VhShootAction(Being performer): base(performer, 10) {
        }

        public override bool Perform() {
            if (!(performer is IEquipmentBeing)) return false;
            EquipmentSlot missleWeaponSlot = (performer as IEquipmentBeing).Equipment[WeaponSlot.ID];
            if (missleWeaponSlot == null) return false;
            Item missleWeapon = missleWeaponSlot.Item;
            if (missleWeapon == null || !(missleWeapon is MissleWeapon)) {
                notify("no-missle-weapon");
                return false;
            }
            MissleSlot missleSlot = (MissleSlot)(performer as IEquipmentBeing).Equipment[MissleSlot.ID];
            if (missleSlot == null) return false;
            Item item = missleSlot.Item;
            if (item is ItemStack) item = (item as ItemStack).Item;
            if (item == null) {
                notify("no-missle");
                return false;
            }
            AbstractAttackAction attackAction = new MissleAttackAction(performer);
            Attack = attackAction;
            missle = missleSlot.NextMissle();
            if (base.Perform()) {
                return true;
            }
            return false;
        }

        protected override void missleStep() {
            GameController controller = GameController.Instance;
            char character = controller.ViewPort.GetDisplayCharacter(controller.Map[pos]);
            ConsoleColor color = controller.ViewPort.GetDisplayColor(controller.Map[pos]);
            ItemFacade facade = new ItemFacade();
            Missle missleAnimation = new Missle();
            missleAnimation.Character = missle.Character;
            missleAnimation.Color = missle.Color;
            missleAnimation.Position = pos;
            controller.ViewPort.Display(missleAnimation, controller.FieldOfVision, performer.Position);
            controller.ViewPort.Refresh(pos); 
            Thread.Sleep(100);
            missleAnimation.Character = character;
            missleAnimation.Color = color;
            controller.ViewPort.Display(missleAnimation, controller.FieldOfVision, performer.Position);
            controller.ViewPort.Refresh(pos);
        }

        protected override bool isShootable(Position position) {
            GameController gc = GameController.Instance;
            char c = gc.ViewPort.GetDisplayCharacter(gc.Level.Map[position]);
            return
                c == Terrain.Get("upstair").Character ||
                c == Terrain.Get("downstair").Character ||
                c == Terrain.Get("ground").Character ||
                c == Terrain.Get("floor").Character ||
                c == Terrain.Get("open-door").Character;
        }

    }
}
