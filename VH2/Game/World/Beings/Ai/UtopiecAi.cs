using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class UtopiecAi: HostileAi {

        #region constants

        private const float PULL_IN_RATE = 0.4f;

        #endregion

        #region constructors
        public UtopiecAi() { }

        public UtopiecAi(Being being) : base(being) { }

        #endregion

        public override AbstractAction SelectAction() {
            AbstractAction action = base.SelectAction();
            if (action is AttackAction && Rng.Random.NextFloat() < PULL_IN_RATE) {
                Being attackee = (action as AttackAction).Attackee;
                Map map = GameController.Instance.Map;
                if (
                    map[Being.Position] == Terrain.Get("water").Character &&
                    map[GetOppositePosition(Being, attackee)] == Terrain.Get("water").Character &&
                    map[attackee.Position] != Terrain.Get("water").Character &&
                    Being.Position.IsAdjacent(attackee.Position)
                /// TODO: is there no monster behind, on the opposite postion to the attackee
                ) {
                    action = new PullInAction(Being, attackee);
                }
            }
            return action;
        }

        #region public methods

        public static Position GetOppositePosition(Being performer, Being attackee) {
            Position pos = performer.Position.Clone();
            int x = pos.X - attackee.Position.X;
            int y = pos.Y - attackee.Position.Y;
            Step step = new Step(x, y);
            pos.AddStep(step);
            return pos;
        }

        #endregion
    }
}
