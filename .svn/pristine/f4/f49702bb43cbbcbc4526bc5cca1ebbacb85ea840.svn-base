using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {
    public class RiverGenerator {

        private const int MIN_RIVER_WIDTH = 5;
        private const int MAX_RIVER_WIDTH = 8;

        public void GenerateRiver(Map map) {
            int startPosition = Rng.Random.Next(map.Width - MIN_RIVER_WIDTH);
            int currentRiverWidth = MIN_RIVER_WIDTH;
            for (int j = 0; j < map.Height; ++j) {
                int r = Rng.Random.Next(10);
                if (r < 3) startPosition++;
                if (r > 7) startPosition--;
                r = Rng.Random.Next(10);
                if (r < 3) currentRiverWidth++;
                if (r > 7) currentRiverWidth--;
                if (currentRiverWidth > MAX_RIVER_WIDTH) currentRiverWidth = MAX_RIVER_WIDTH;
                if (currentRiverWidth < MIN_RIVER_WIDTH) currentRiverWidth = MIN_RIVER_WIDTH;
                for (int i = startPosition; i <= startPosition + currentRiverWidth; ++i) {
                    if (i >= 0 && j >= 0 && i < map.Width && j < map.Height) map[i, j] = Terrain.Get("water").Character;
                }
            }
        }

    }
}
