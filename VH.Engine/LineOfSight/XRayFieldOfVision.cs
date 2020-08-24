using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;

namespace VH.Engine.LineOfSight {

    public class XRayFieldOfVision: AbstractFieldOfVision {


        public override void ComputeFieldOfVision(Map map, Levels.Position observer, int visionRange) {
            if (visionRange > MAX_VISION_RANGE || visionRange < 0 ) throw new ArgumentOutOfRangeException("visionRange out of range");
            clear();
            for (int i = -MAX_VISION_RANGE; i <= MAX_VISION_RANGE; ++i) {
                for (int j = -MAX_VISION_RANGE; j <= MAX_VISION_RANGE; ++j) {
                    this[i, j] = true;  
                }
            }
        }

        public override int MaxVisionRange {
            get { return MAX_VISION_RANGE; }
        }

    }
}
