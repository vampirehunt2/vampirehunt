using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Random;
using VH.Engine.World.Items;
using VH.Engine.World.Items.Weapons;
using VH.Game.World.Items.Weapons;

namespace VH.Engine.World.Beings.Actions {
    public class MissleAttackAction : AbstractAttackAction {

        private const int DEFAULT_ATTACK = 1;

        public MissleAttackAction(Being performer) : base(performer) {
        }

        protected override int Attack {
            get {
                int attack = DEFAULT_ATTACK;
                IEquipmentBeing eb = performer as IEquipmentBeing;
                if (eb != null) {
                    EquipmentSlot missleWeaponSlot = eb.Equipment["missle-weapon-slot"];
                    MissleWeapon weapon = (MissleWeapon)missleWeaponSlot.Item;
                    if (weapon != null) attack += weapon.Attack;
                }
                ISkillsBeing sb = performer as ISkillsBeing;   
                if (sb != null) {
                    Skill skill = sb.Skills["missle-weapons"];
                    if (skill != null) attack += skill.Value / 10;
                }
                return Rng.Random.Next(performer.Attack); 
            }
        }

        protected override int Defense {
            get { return Rng.Random.Next(attackee.Defense); }
        }

    }
}
