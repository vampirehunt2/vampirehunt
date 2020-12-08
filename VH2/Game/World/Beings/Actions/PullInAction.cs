using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Ai;

namespace VH.Game.World.Beings.Actions {
    public class PullInAction : AttackAction {

        #region constructors

        private const int MAX_STRENGTH = 10;
        private const int REFERENCE_STRENGTH = 12;
        private const float TEST_THRESHOLD = 0.5f;

        #endregion

        #region constructors
        public PullInAction(Being performer, Being attackee) : base(performer, attackee) { }

        #endregion

        #region public methods

        public override bool Perform() {
            bool test = false;
            if (Attackee is IStatBeing) {
                IStatBeing statBeing = Attackee as IStatBeing;
                int strength = Math.Max(statBeing.Stats["St"].Value, MAX_STRENGTH);
                if (Rng.Random.NextFloat() > MAX_STRENGTH / (float)REFERENCE_STRENGTH) test = true;
            } else {
                if (Rng.Random.NextFloat() > TEST_THRESHOLD) test = true;
            }
            if (test) {
                Position pos = performer.Position;
                performer.Position = UtopiecAi.GetOppositePosition(performer, Attackee);
                Attackee.Position = pos;
                notify("pull-in-water", Attackee);
            } else {

            }
            return true;
        }

        #endregion



    }
}
