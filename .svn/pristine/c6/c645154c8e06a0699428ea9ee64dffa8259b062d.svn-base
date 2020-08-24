using VH.Engine.World.Beings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.Levels;

namespace VH.Engine.World.Beings.Actions {

    public class TakeStairsAction : AbstractAction {


        public TakeStairsAction(Being performer): base(performer) { }

        public override bool Perform() {
            char feature = GameController.Instance.Map[performer.Position];
            if (feature != Terrain.Get("upstair").Character && feature != Terrain.Get("downstair").Character) {
                notify("no-stairs", performer);
                return false;
            }
            Level nextLevel = GameController.Instance.Level.GetNextLevel(performer.Position);
            if (feature == Terrain.Get("downstair").Character) notify("go-down", performer);
            if (feature == Terrain.Get("upstair").Character) notify("go-up", performer);
            GameController.Instance.Level = nextLevel;
            return true;
        }

    }
}
