using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;

namespace VH.Engine.LineOfSight {

    public class HardcodedShadowcastingFieldOfVision: AbstractFieldOfVision {

        public override void ComputeFieldOfVision(Map map, Levels.Position observer, int visionRange) {
            this.Observer = observer;
            clear();
            if (visionRange > 0) {
                for (int i = -1; i <= 1; ++i) {
                    for (int j = -1; j <= 1; ++j) {
                        this[i, j] = true;
                    }
                }
            }
            if (visionRange > 1) {
                this[-2, -2] = Map.IsTransparent(map[observer.X - 1, observer.Y - 1]);
                this[-2, 2] = Map.IsTransparent(map[observer.X - 1, observer.Y + 1]);
                this[2, -2] = Map.IsTransparent(map[observer.X + 1, observer.Y - 1]);
                this[2, 2] = Map.IsTransparent(map[observer.X + 1, observer.Y + 1]);
          
                this[-2, 0] = this[-2, -1] = this[-2, 1] = Map.IsTransparent(map[observer.X - 1, observer.Y]);
                this[2, 0] = this[2, -1] = this[2, 1] = Map.IsTransparent(map[observer.X + 1, observer.Y]);
                this[0, -2] = this[1, -2] = this[-1, -2] = Map.IsTransparent(map[observer.X, observer.Y - 1]);
                this[0, 2] = this[1, 2] = this[-1, 2] = Map.IsTransparent(map[observer.X, observer.Y + 1]);
            }
        }

        public override int MaxVisionRange {
            get { return 2; }
        }
    }
}
