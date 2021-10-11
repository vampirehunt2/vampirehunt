using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;

namespace VH.Game.World.Beings.Actions {
    public class CauseAmnesiaAction: Engine.World.Beings.Actions.AbstractAction {

        //private const float RESISTANCE = 0.5f;

        public CauseAmnesiaAction() : base(null) { }
        public CauseAmnesiaAction(Being performer) : base(performer) { }

        public override bool Perform() {
            Map map = VhGameController.Instance.Level.Map;
            for (int i = 0; i < map.Width; ++i) {
                for (int j = 0; j < map.Height; ++j) {
                    //commented out for test purposes, but turned out it's actually better this way
                    /*if (Rng.Random.NextFloat() > RESISTANCE)*/ map.Mem[i, j] = Map.UNKNOWN;
                }
            }
            notify("amnesia");
            return true;
        }
    }
}
