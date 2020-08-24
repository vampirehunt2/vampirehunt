using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.Display;

namespace VH.Levels {

    public class LevelPlan {

        private int width = 48;
        private int height = 48;
        private Map map;
        private Position position;
        private char[,] plan;
        

        public LevelPlan(Map map, Position position) {
            this.map = map;
            this.position = position;
            plan = new char[width, height]; 
            compute();
        }

        public char this[int x, int y] {
            get { return plan[x, y]; }
        }

        public void Show(ViewPort viewPort) {
            viewPort.Clear();
            viewPort.Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < width; ++i) {
                for (int j = 0; j < height; ++j) {
                    if (plan[i, j] != Map.UNKNOWN) viewPort.Write(plan[i, j], i, j);
                }
            }
            viewPort.Refresh();
            viewPort.Console.ReadKey();
        }

        private void compute() {
            for (int i = 0; i < width; ++i) {
                for (int j = 0; j < height; ++j) {
                    plan[i, j] = getDominant(i, j);
                }
            }
        }

        private char getDominant(int x, int y) {
            char dom = Map.UNKNOWN;
            char c1 = map.Mem[x * 2, y * 2];
            char c2 = map.Mem[x * 2 + 1, y * 2];
            char c3 = map.Mem[x * 2, y * 2 + 1];
            char c4 = map.Mem[x * 2 + 1, y * 2 + 1];
            //
            if (c1 == Terrain.Get("ground").Character || c2 == Terrain.Get("ground").Character || c3 == Terrain.Get("ground").Character || c4 == Terrain.Get("ground").Character) dom = CP437.SOLID_BRICK;
            if (c1 == Terrain.Get("corridor").Character || c2 == Terrain.Get("corridor").Character || c3 == Terrain.Get("corridor").Character || c4 == Terrain.Get("corridor").Character) dom = Terrain.Get("ground").Character;
            if (c1 == Terrain.Get("closed-door").Character || c2 == Terrain.Get("closed-door").Character || c3 == Terrain.Get("closed-door").Character || c4 == Terrain.Get("closed-door").Character) dom = Terrain.Get("closed-door").Character;
            if (c1 == Terrain.Get("upstair").Character || c2 == Terrain.Get("upstair").Character || c3 == Terrain.Get("upstair").Character || c4 == Terrain.Get("upstair").Character) dom = Terrain.Get("upstair").Character;
            if (c1 == Terrain.Get("downstair").Character || c2 == Terrain.Get("downstair").Character || c3 == Terrain.Get("downstair").Character || c4 == Terrain.Get("downstair").Character) dom = Terrain.Get("downstair").Character;
            if (x == position.X / 2 && y == position.Y / 2) dom = '@';   
            return dom;
        }

    }
}
