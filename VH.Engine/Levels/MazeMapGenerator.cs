using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {

    public class MazeMapGenerator: AbstractMapGenerator {

        #region constructors
        #endregion

        #region public methods

        public override Map Generate(int width, int height) {
            initMap(width, height);
            MazeCell cell = new MazeCell(map);
            cell.GenerateMaze();
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

        #region private methods

        private void initMap(int width, int height) {
            map = new Map(width, height);
            for (int i = 0; i < width; ++i) {
                for (int j = 0; j < height; ++j) {
                    map[i, j] = Terrain.Get("wall").Character;
                }
            }
        }

        #endregion

    }

    #region utility classes

    class MazeCell {

        #region fields

        int x1;
        int y1;
        int x2;
        int y2;

        MazeCell child1;
        MazeCell child2;
        MazeCell parent;

        Map map;

        SplitOrientation splitOrientation;
        int connection;

        #endregion

        #region constructors

        public MazeCell(Map map, MazeCell parent) {
            splitOrientation = SplitOrientation.None;
            this.map = map;
            this.parent = parent;
        }


        public MazeCell(Map map) : this(map, null) {
            x1 = 1;
            y1 = 1;
            x2 = map.Width - 2;
            y2 = map.Height - 2;
        }

        #endregion

        #region properties

        int height {
            get { return Math.Abs(y1 - y2) + 1; }
        }

        int width {
            get { return Math.Abs(x1 - x2) + 1; }
        }

        bool hasChildren {
            get { return child1 != null; }
        }

        bool canSplit {
            get { 
                return 
                    (width >= 3) && (height >= 2)
                 || (width >= 2) && (height >= 3); 
            }
        }

        #endregion

        #region methods

        void split() {
            splitOrientation = chooseSplit();
            if (splitOrientation == SplitOrientation.Horizontal) {
                int r = Rng.Random.Next(width - 2) + 1;
                child1 = new MazeCell(map, this);
                child1.x1 = x1;
                child1.y1 = y1;
                child1.x2 = x1 + r - 1;
                child1.y2 = y2;
                child2 = new MazeCell(map, this);
                child2.x1 = x1 + r + 1;
                child2.y1 = y1;
                child2.x2 = x2;
                child2.y2 = y2;
                for (int y = y1; y <= y2; ++y) map[x1 + r, y] = Terrain.Get("wall").Character;
                if (parent == null || splitOrientation == parent.splitOrientation) {
                    
                } else {
                    connection = Rng.Random.Next(height - 1);
                    if (connection >= parent.connection) connection++;
                }
                connection = Rng.Random.Next(height);
                map[x1 + r, y1 + connection] = Terrain.Get("ground").Character;
            }
            if (splitOrientation == SplitOrientation.Vertical) {
                int r = Rng.Random.Next(height - 2) + 1;               
                child1 = new MazeCell(map, this);
                child1.x1 = x1;
                child1.y1 = y1;
                child1.x2 = x2;
                child1.y2 = y1 + r - 1;
                child2 = new MazeCell(map, this);
                child2.x1 = x1;
                child2.y1 = y1 + r + 1;
                child2.x2 = x2;
                child2.y2 = y2;
                for (int x = x1; x <= x2; ++x) map[x, y1 + r] = Terrain.Get("wall").Character;
                connection = Rng.Random.Next(width);
                map[x1 + connection, y1 + r] = Terrain.Get("ground").Character;
            }
            if (child1.canSplit) child1.split();
            if (child2.canSplit) child2.split();
        }

        SplitOrientation chooseSplit() {
            if ((width >= 3) && !(height >= 3)) return SplitOrientation.Horizontal;
            if ((height >= 3) && !(width >= 3)) return SplitOrientation.Vertical;
            if ((width >= 3) && (height >= 3)) {
                if (width > height + 3) return SplitOrientation.Horizontal;
                if (height > width + 3) return SplitOrientation.Vertical;
                return (SplitOrientation)(Rng.Random.Next(2) + 1);
            }
            return SplitOrientation.None;
        }

        MazeCell getSibling() {
            if (parent == null) return null;
            if (parent.child1 == this) return parent.child2;
            if (parent.child2 == this) return parent.child1;
            return null;
        }

        public void GenerateMaze() {
            for (int i = x1; i <= x2; ++i) {
                for (int j = y1; j <= y2; ++j) {
                    map[i, j] = Terrain.Get("ground").Character;
                }
            }
            split();
        }

        #endregion
    }

    #endregion
}
