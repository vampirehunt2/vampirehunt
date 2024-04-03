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

namespace VH.Game.World.Beings.Actions {
    public class VhShootAction: ShootAction {

        Ammo missle; 

        public VhShootAction(Being performer): base(performer, 10) {
        }

        public override MissleWeapon MissleWeapon { 
            get {
                if (!(performer is IEquipmentBeing)) return null;
                EquipmentSlot missleWeaponSlot = (performer as IEquipmentBeing).Equipment[WeaponSlot.ID];
                if (missleWeaponSlot == null) return null;
                Item missleWeapon = missleWeaponSlot.Item;
                if (missleWeapon == null || !(missleWeapon is MissleWeapon)) {
                    return null;
                }
                return (MissleWeapon)missleWeapon;
            }
        }

        public override Item Missle {
            get {
                if (!(performer is IEquipmentBeing)) return null;
                MissleSlot missleSlot = (MissleSlot)(performer as IEquipmentBeing).Equipment[MissleSlot.ID];
                if (missleSlot == null) return null;
                missle = missleSlot.Item as Ammo;
                if (missle == null || missle.Number == 0) {
                    return null;
                }
                return missle;
            }
        }

        public override bool Perform() {
            if (MissleWeapon == null) {
                notify("no-missle-weapon");
                return false;
            }

            if (Missle == null) {
                notify("no-missle");
                return false;
            }

            AbstractAttackAction attackAction = new MissleAttackAction(performer);
            Attack = attackAction;
            
            if (base.Perform()) {
                if (missle.Spend(1)) return true;
            }
            return false;
        }

        protected override void missleStep() {
            GameController controller = GameController.Instance;
            char character = controller.ViewPort.GetDisplayCharacter(controller.Map[pos]);
            ConsoleColor color = controller.ViewPort.GetDisplayColor(controller.Map[pos]);
            ItemFacade facade = new ItemFacade();
            Ammo missleAnimation = new Ammo();
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
