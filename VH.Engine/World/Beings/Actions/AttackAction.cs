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

namespace VH.Engine.World.Beings.Actions {
    
    public class AttackAction: AbstractAction {

        private Being attackee = null;
        private GameController controller = GameController.Instance;
        protected int damage;

        #region constructors

        public AttackAction(Being performer, Being attackee) : base(performer) {
            this.attackee = attackee;
        }

        public AttackAction(Being performer) : base(performer) { }

        #endregion

        #region properties

        public Being Attackee {
            get { return attackee; }
            set { attackee = value; }   
        }

        #endregion

        #region public methods

        public override bool Perform() {
            int attack = Rng.Random.Next(performer.Attack);
            int defense = Rng.Random.Next(attackee.Defense);
            damage = attack - defense;
            if (damage > 0) {
                attackee.DecreaseHealth(damage, performer.Accusativ);
                notify("attack", attackee);
            } else {
                notify("miss", attackee);
            }
            attackee.Ai.Stimulate(new AttackStimulus(performer));
            if (attackee.Health <= 0) {
                attackee.Ai.Notify("killed");
                attackee.Kill();
            }
            return true;
        }

        #endregion

    }
}
