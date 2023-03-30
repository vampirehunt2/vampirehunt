using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class InflictAmnesiaAction: MeleeAttackAction {

        #region constants

        private const float RESISTANCE = 0.5f;

        #endregion

        #region constructors
        public InflictAmnesiaAction(Being performer, Being attackee) : base(performer, attackee) { }

        #endregion

        #region public methods
        public override bool Perform() {
            Map map = VhGameController.Instance.Level.Map;
            for (int i = 0; i < map.Width; ++i) {
                for (int j = 0; j < map.Height; ++j) {
                    /*if (Rng.Random.NextFloat() > RESISTANCE)*/ map.Mem[i, j] = Map.UNKNOWN;
                }
            }
            VhGameController.Instance.ViewPort.Clear(); // WARNING! this is not PC-agnostic!
            notify("amnesia", Attackee);
            return true;
        }

        #endregion

    }
}
