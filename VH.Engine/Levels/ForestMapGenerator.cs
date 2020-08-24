using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {
    public class ForestMapGenerator : AbstractMapGenerator {

        public override Map Generate(int width, int height) {
            Map map = new Map(width, height);
            for (int x = 0; x < width; ++x) {
                for (int y = 0; y < height; ++y) {
                    if (Rng.Random.NextFloat() < 0.3) map[x, y] = Terrain.Get("tree").Character;
                    else map[x, y] = Terrain.Get("grass").Character;
                }
            }
            this.map = map;
            new RiverGenerator().GenerateRiver(map);
            return map;
        }

        public override Position GenerateFeature(char feature) {
            int x;
            int y;
            do {
                x = Rng.Random.Next(map.Width);
                y = Rng.Random.Next(map.Height);
            } while (map[x, y] != Terrain.Get("grass").Character);
            map[x, y] = feature;
            return new Position(x, y);
        }
    }
}
