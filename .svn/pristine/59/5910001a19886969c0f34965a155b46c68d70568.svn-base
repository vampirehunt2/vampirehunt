using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {

    /// <summary>
    /// Represenst one step that a Being can take while walking on a Level.
    /// </summary>
    public class Step {

        #region constants

        public static Step NONE = new Step(0, 0);
        public static Step NORTH = new Step(0, -1);
        public static Step SOUTH = new Step(0, 1);
        public static Step EAST = new Step(1, 0);
        public static Step WEST = new Step(-1, 0);
        public static Step NORTH_EAST = new Step(1, -1);
        public static Step NORTH_WEST = new Step(-1, -1);
        public static Step SOUTH_EAST = new Step(1, 1);
        public static Step SOUTH_WEST = new Step(-1, 1);
        public static Step[] ALL = new Step[] {
            NORTH, 
            SOUTH, 
            EAST, 
            WEST, 
            NORTH_EAST,
            NORTH_WEST, 
            SOUTH_EAST, 
            SOUTH_WEST 
        };

        #endregion

        #region fields

        private int x;
        private int y;

        #endregion

        #region constructors

        public Step() { }

        public Step(int x, int y) {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region properties

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        #endregion

        #region public methods

        public static Step CreateRandomStep() {
            int r = Rng.Random.Next(8);
            switch (r) {
                case 0: return Step.EAST;
                case 1: return Step.WEST;
                case 2: return Step.NORTH;
                case 3: return Step.SOUTH;
                case 4: return Step.SOUTH_EAST;
                case 5: return Step.SOUTH_WEST;
                case 6: return Step.NORTH_EAST;
                case 7: return Step.NORTH_WEST;
                default: return Step.EAST;
            }
        }

        #endregion

    }
}
