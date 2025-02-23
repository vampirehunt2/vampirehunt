using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Random;
using VH.Engine.World.Beings.AI.Stimuli;

namespace VH.Engine.World.Beings.Actions {
    public abstract class AbstractAttackAction: AbstractAction {

        #region fields

        private GameController controller = GameController.Instance;
        protected Being attackee = null;
        protected int damage;

        #endregion

        #region constructors
        protected AbstractAttackAction(Being performer) : base(performer) {
        }

        #endregion

        #region properties

        protected abstract int Attack { get;}

        protected abstract int Defense { get; }

        public Being Attackee {
            get { return attackee; }
            set { attackee = value; }
        }

        #endregion

        #region public methods

        public override bool Perform() {
            damage = Attack - Defense;
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
