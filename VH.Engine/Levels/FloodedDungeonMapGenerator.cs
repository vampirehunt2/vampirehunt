using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {

    public class FloodedDungeonMapGenerator: DungeonMapGenerator {

        public override Map Generate(int width, int height) {
            Map map = base.Generate(width, height);
            new RiverGenerator().GenerateRiver(map);
            return map;
        }

        public override Position GenerateFeature(char feature) {
            return base.GenerateFeature(feature);
        }

    }
}
