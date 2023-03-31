using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Levels;

namespace VH.Engine.World.Beings.Actions {
    public class ShootAction : AbstractAction {

        #region fields

        int range;
        AbstractAttackAction attack = null;
        protected Position pos;

        #endregion

        #region constructors

        public ShootAction(Being performer, int range) : base(performer) {   
            this.range = range; 
        }

        #endregion

        #region properties

        public AbstractAttackAction Attack { 
            get { return attack; } 
            set { attack = value; }
        }   

        #endregion

        #region public methods
        public override bool Perform() {
            pos = performer.Position;
            Step step = (Step)performer.Ai.SelectTarget(null, this);
            bool hit = false;
            int currentRange = 0;
            while (!hit & currentRange < range) {
                currentRange++;
                pos = pos.AddStep(step);
                missleStep();
                Being attackee = GameController.Instance.GetBeingAt(pos);
                if (attackee != null) {
                    hit = true;
                    if (attack == null) {
                        attack = new AttackAction(performer, attackee);
                    } else {
                        attack.Attackee = attackee;
                    }
                    attack.Perform();
                }
            }
            return true;
        }

        #endregion

        #region protected methods

        protected virtual void missleStep() { }

        #endregion

    }
}
