using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {
    class Room {

        #region

        private float DARK_ROOM_RATE = 0.05f;

        #endregion

        #region fields

        public int x1;
        public int y1;
        public int x2;
        public int y2;

        #endregion

        #region methods

        public void create(Map map) {
            char terrain = Terrain.Get("ground").Character;
            if (Rng.Random.NextFloat() < DARK_ROOM_RATE) terrain = Terrain.Get("dark-ground").Character;
            for (int i = x1 + 1; i < x2; ++i) {
                for (int j = y1 + 1; j < y2; ++j) {
                    map[i, j] = terrain;
                }
            }
        }

        #endregion
    }
}
