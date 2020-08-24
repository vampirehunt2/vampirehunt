using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {

    public class TownMapGenerator: AbstractMapGenerator {

        #region public methods

        public override Map Generate(int width, int height) {
            if (width > Map.MAX_WIDTH || height > Map.MAX_HEIGHT) throw new ArgumentOutOfRangeException("Map too big");
            Map map = new Map(width, height);
            TownCell cell = new TownCell(map);
            cell.GenerateTown();
            this.map = map;
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

        #endregion

    }


    #region utility classes

    class House {

        #region fields

        public int x1;
        public int y1; 
        public int x2; 
        public int y2;

        #endregion

        #region properties

        private int width {
            get { return Math.Abs(x1 - x2) + 1; }
        }

        private int height {
            get { return Math.Abs(y1 - y2) + 1; }
        }

        #endregion

        #region methods

        public void create(Map map) {
            for (int i = x1; i <= x2; ++i) {
                for (int j = y1; j <= y2; ++j) {
                    if (i == x1 || i == x2 || j == y1 || j == y2) {
                        map[i, j] = Terrain.Get("wall").Character;
                    } else {
                        map[i, j] = Terrain.Get("floor").Character;
                    }
                }
            }
            int wall = Rng.Random.Next(4);
            int x = 0;
            int y = 0;
            switch (wall) {
                case 0:
                    x = Rng.Random.Next(width - 2) + 1 + x1;
                    y = y1;
                    break;
                case 1:
                    x = Rng.Random.Next(width - 2) + 1 + x1;
                    y = y2;
                    break;
                case 2:
                    y = Rng.Random.Next(height - 2) + 1 + y1;
                    x = x1;
                    break;
                case 3:
                    y = Rng.Random.Next(height - 2) + 1 + y1;
                    x = x2;
                    break;
                default: break;
                    //does not happen
            }
            map[x, y] = Terrain.Get("closed-door").Character;
        }

        #endregion
    }


    class TownCell {

        #region constants

        private int MIN_CELL_SIZE = 10;
        private int MIN_ROOM_SIZE = 5;

        #endregion

        #region fields

        int x1;
        int y1;
        int x2;
        int y2;

        TownCell child1;
        TownCell child2;
        TownCell parent;
        House house;

        Map map;

        SplitOrientation splitOrientation;


        #endregion

        #region constructors

        public TownCell(Map map, TownCell parent) {
            splitOrientation = SplitOrientation.None;
            this.map = map;
            this.parent = parent;
        }


        public TownCell(Map map) : this(map, null) {
            x1 = 0;
            y1 = 0;
            x2 = map.Width - 1;
            y2 = map.Height - 1;
        }

        #endregion

        #region properties

        int height {
            get { return Math.Abs(y1 - y2); }
        }

        int width {
            get { return Math.Abs(x1 - x2); }
        }

        bool hasChildren {
            get { return child1 != null; }
        }

        bool canSplit {
            get { return (width > MIN_CELL_SIZE * 2) || (height > MIN_CELL_SIZE * 2); }
        }

        #endregion

        #region methods

        void split() {
            splitOrientation = chooseSplit();
            if (splitOrientation == SplitOrientation.Horizontal) {
                int r = Rng.Random.Next(width - 2 * MIN_CELL_SIZE);
                r = r + MIN_CELL_SIZE;
                child1 = new TownCell(map, this);
                child1.x1 = x1;
                child1.y1 = y1;
                child1.x2 = x1 + r;
                child1.y2 = y2;
                child2 = new TownCell(map, this);
                child2.x1 = x1 + r;
                child2.y1 = y1;
                child2.x2 = x2;
                child2.y2 = y2;
            }
            if (splitOrientation == SplitOrientation.Vertical) {
                int r = Rng.Random.Next(height - 2 * MIN_CELL_SIZE);
                r = r + MIN_CELL_SIZE;
                child1 = new TownCell(map, this);
                child1.x1 = x1;
                child1.y1 = y1;
                child1.x2 = x2;
                child1.y2 = y1 + r;
                child2 = new TownCell(map, this);
                child2.x1 = x1;
                child2.y1 = y1 + r;
                child2.x2 = x2;
                child2.y2 = y2;
            }
            if (child1.canSplit) child1.split();
            if (child2.canSplit) child2.split();
        }

        SplitOrientation chooseSplit() {
            if ((width > MIN_CELL_SIZE * 2) && !(height > MIN_CELL_SIZE * 2)) return SplitOrientation.Horizontal;
            if ((height > MIN_CELL_SIZE * 2) && !(width > MIN_CELL_SIZE * 2)) return SplitOrientation.Vertical;
            if ((width > MIN_CELL_SIZE * 2) && (height > MIN_CELL_SIZE * 2)) return (SplitOrientation)(Rng.Random.Next(2) + 1);
            return SplitOrientation.None;
        }

        TownCell getSibling() {
            if (parent == null) return null;
            if (parent.child1 == this) return parent.child2;
            if (parent.child2 == this) return parent.child1;
            return null;
        }

        void makeHouses() { 
            if ( hasChildren ) {
                child1.makeHouses();
                child2.makeHouses();
            } else {
                if (Rng.Random.NextFloat() < 0.2) createPark();
                else if (Rng.Random.NextFloat() < 0.2) createPond();
                else createHouse();
            }
        }

        private void createPark() {
            for (int x = x1 + 1; x < x2; ++x) {
                for (int y = y1 + 1; y < y2; ++y) {
                    if (Rng.Random.NextFloat() < 0.4) map[x, y] = Terrain.Get("tree").Character;
                }
            }
        }

        private void createPond() {
            int cx = x1 + width / 2;
            int cy = y1 + height / 2;
            for (int x = x1 + 1; x < x2; ++x) {
                for (int y = y1 + 1; y < y2; ++y) {
                    int distance = (x - cx) * (x - cx) + (y - cy) * (y - cy);
                    int maxDistance = Math.Min(width, height) / 2;
                    maxDistance *= maxDistance;
                    if (distance < maxDistance) map[x, y] = Terrain.Get("water").Character;
                }
            }
        }

        void createHouse() {
            house = new House();
            int rw = Rng.Random.Next(width);
            rw = Math.Max(rw, MIN_ROOM_SIZE);
            rw = Math.Min(rw, width - 4);
            house.x1 = x1 + Rng.Random.Next(width - rw - 2) + 2;
            house.x2 = house.x1 + rw;
            int rh = Rng.Random.Next(height);
            rh = Math.Max(rh, MIN_ROOM_SIZE);
            rh = Math.Min(rh, height - 4);
            house.y1 = y1 + Rng.Random.Next(height - rh - 2) + 2;
            house.y2 = house.y1 + rh;
            house.create(map);
        }

        public void GenerateTown() {
            for (int i = x1; i <= x2; ++i) {
                for (int j = y1; j <= y2; ++j) {
                    map[i, j] = Terrain.Get("grass").Character;
                }
            }
            split();
            makeHouses();
        }

        #endregion
    }

    #endregion
}
