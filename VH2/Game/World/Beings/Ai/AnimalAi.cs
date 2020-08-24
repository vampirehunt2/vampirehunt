using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.AI;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Display;
using VH.Engine.World.Beings;
using VH.Engine.Pathfinding;
using VH.Engine.Game;
using VH.Game.World.Beings.Actions;
using VH.Engine.Levels;

namespace VH.Game.World.Beings.Ai {

    public class AnimalAi: HostileAi {

        #region constructors

        public AnimalAi() : base() { }

        public AnimalAi(Being being) : base(being) { }

        #endregion

    }
}
