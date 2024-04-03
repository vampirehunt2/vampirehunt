using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Items;
using VH.Engine.World.Items.Weapons;

namespace VH.Engine.World.Beings.Actions {
    public class ShootAction : AbstractAction {

        #region fields

        int range;
        AbstractAttackAction attack = null;
        protected Position pos;
        protected Step step;

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

        public virtual MissleWeapon MissleWeapon { 
            get { return null; } 
        }

        public virtual Item Missle {
            get { return null; }
        }

        #endregion

        #region public methods
        public override bool Perform() {
            pos = performer.Position;
            step = (Step)performer.Ai.SelectTarget(null, this);
            bool hit = false;
            int currentRange = 0;
            while (!hit & currentRange < range) {
                currentRange++;
                pos = pos.AddStep(step);
                missleStep();
                if (!isShootable(pos)) {
                    notify("hit-wall");
                    break;
                }
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

        protected virtual bool isShootable(Position position) {
            GameController gc = GameController.Instance;
            char c = gc.ViewPort.GetDisplayCharacter(gc.Level.Map[position]);
            return
                c == Terrain.Get("upstair").Character ||
                c == Terrain.Get("downstair").Character ||
                c == Terrain.Get("ground").Character ||
                c == Terrain.Get("open-door").Character;
        }

        #endregion

    }
}
