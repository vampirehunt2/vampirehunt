using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {


    public class DungeonMapGenerator: AbstractMapGenerator {

        #region public methods

        public override Map Generate(int width, int height) {
            if (width > Map.MAX_WIDTH || height > Map.MAX_HEIGHT) throw new ArgumentOutOfRangeException("Map too big");
            Map map = new Map(width, height);
            Cell cell = new Cell(map);
            cell.GenerateDungeon();
            this.map = map;
            return map;
        }

        public override Position GenerateFeature(char feature) {
            int x;
            int y;
            do {
                x = Rng.Random.Next(map.Width);
                y = Rng.Random.Next(map.Height);
            } while (map[x, y] != Terrain.Get("ground").Character);
            map[x, y] = feature;
            return new Position(x, y);
        }

        #endregion

    }


    #region utility classes

    


    

    #endregion

}
