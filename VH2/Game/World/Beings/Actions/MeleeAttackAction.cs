using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.World.Items;
using VH.Engine.World.Beings.AI.Stimuli;
using VH.Engine.Random;

namespace VH.Game.World.Beings.Actions {
    
    public class MeleeAttackAction: AbstractAttackAction {

        #region constructors

        public MeleeAttackAction(Being performer, Being attackee) : base(performer) {
            this.attackee = attackee;
        }

        public MeleeAttackAction(Being performer) : base(performer) { }

        #endregion

        #region properties

        protected override int Attack {
            get { return Rng.Random.Next(performer.Attack); }
        }

        protected override int Defense {
            get { return Rng.Random.Next(attackee.Defense);  }
        }

        #endregion

    }
}
