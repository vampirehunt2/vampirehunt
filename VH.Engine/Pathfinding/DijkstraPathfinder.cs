using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Levels;
using VH.Engine.Game;

namespace VH.Engine.Pathfinding {

    public class DijkstraPathfinder: Pathfinder {

        public override Path FindPath(Being being, Position target) {
            Map map = GameController.Instance.Map;
            int width = map.Width;
            int height = map.Height;
            byte[,] bytemap = new byte[width, height];
            bytemap[target.X, target.Y] = byte.MaxValue;
            return new Path();
        }

    }
}
