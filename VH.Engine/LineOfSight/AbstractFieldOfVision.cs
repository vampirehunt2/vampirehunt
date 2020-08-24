using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.Persistency;

namespace VH.Engine.LineOfSight {

    public abstract class AbstractFieldOfVision: AbstractPersistent {

        #region constants

        public const int MAX_VISION_RANGE = 10;

        #endregion

        #region fields

        private bool[,] fov = new bool[MAX_VISION_RANGE * 2 + 1, MAX_VISION_RANGE * 2 + 1];
        private Position observer;

        #endregion

        #region properties

        public Position Observer {
            get { return observer; }
            set { observer = value; }
        }

        public bool this[int i, int j] {
            get {
                if (i < -MAX_VISION_RANGE || j < -MAX_VISION_RANGE ||
                    i > MAX_VISION_RANGE || j > MAX_VISION_RANGE) return false;
                return fov[i + MAX_VISION_RANGE, j + MAX_VISION_RANGE];
            }
            set { fov[i + MAX_VISION_RANGE, j + MAX_VISION_RANGE] = value; }
        }

        public abstract int MaxVisionRange { get; } 

        #endregion

        #region public methods
        
        /// <summary>
        /// Always call this.observer = observer when implementing this method in a subclass.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="observer"></param>
        /// <param name="visionRange"></param>
        public abstract void ComputeFieldOfVision(Map map, Position observer, int visionRange);

        public bool IsInFieldOfVision(Position position) {
            int x = position.X - observer.X;
            int y = position.Y - observer.Y;
            return this[x, y];
        }

        protected void clear() {
            for (int i = -MAX_VISION_RANGE; i <= MAX_VISION_RANGE; ++i) {
                for (int j = -MAX_VISION_RANGE; j <= MAX_VISION_RANGE; ++j) {
                    this[i, j] = false;
                }
            }
        }

        #endregion

    }
}
