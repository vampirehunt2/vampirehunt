using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Random;

namespace VH.Engine.World.Beings.Actions {
    public class AttackAction : AbstractAttackAction {
        public AttackAction(Being performer) : base(performer) {
        }

        public AttackAction(Being performer, Being attackee) : base(performer) {
            this.attackee = attackee;
        }

        protected override int Attack {
            get { return Rng.Random.Next(performer.Attack); }
        }

        protected override int Defense {
            get { return Rng.Random.Next(attackee.Defense); }
        }
    }
}
