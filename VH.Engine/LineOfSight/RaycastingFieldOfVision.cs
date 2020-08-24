using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;

namespace VH.Engine.LineOfSight {

    public class RaycastingFieldOfVision: AbstractFieldOfVision {

        public override void ComputeFieldOfVision(Map map, Levels.Position observer, int visionRange) {
            this.Observer = observer;
            clear();
            if ( visionRange > 0) this[0, 0] = true;
            for ( int i = -visionRange; i <= visionRange; ++i ) {
	            int x = 0;
	            int y = 0;
	            int it = 0;
                int adjacency = 0;
	            while (x * x + y * y < visionRange * visionRange) {
		            it++;
		            x--; 
		            y = (i * it) / visionRange;
		            this[x, y] = true;
                    if (y > 0) adjacency = -1;
                    if (y < 0) adjacency = 1;
		            if ( !Map.IsTransparent(map[observer.X + x, observer.Y + y]) ) {
                        int q = 0;
                        while (Map.IsTransparent(map[observer.X + x - q, observer.Y + y + adjacency]) &&
                            (x - 1 - q) * (x - 1 - q) + y * y < visionRange * visionRange) {
                            this[x - 1 - q, y] = true;
                            q++;
                        }
                        break;
                    }
                }
            }
            for ( int i = -visionRange; i <= visionRange; ++i ) {
	            int x = 0;
	            int y = 0;
	            int it = 0;
                int adjacency = 0;
	            while (x * x + y * y < visionRange * visionRange) {
		            it++;
		            x++; 
		            y = (i * it) / visionRange;
		            this[x, y] = true;
                    if (y > 0) adjacency = -1;
                    if (y < 0) adjacency = 1;
                    if (!Map.IsTransparent(map[observer.X + x, observer.Y + y]) ) {
                        int q = 0;
                        while (Map.IsTransparent(map[observer.X + x + q, observer.Y + y + adjacency]) &&
                            (x + 1 + q) * (x + 1 + q) + y * y < visionRange * visionRange) {
                            this[x + 1 + q, y] = true;
                            q++;
                        }
                        break;
                    }
                }
            }
            for ( int i = -visionRange; i <= visionRange; ++i ) {
	            int x = 0;
	            int y = 0;
	            int it = 0;
                int adjacency = 0;
	            while (x * x + y * y < visionRange * visionRange) {
		            it++;
		            y--; 
		            x = (i * it) / visionRange;
		            this[x, y] = true;
                    if (x > 0) adjacency = -1;
                    if (x < 0) adjacency = 1;
                    if (!Map.IsTransparent(map[observer.X + x, observer.Y + y]) ) {
                        int q = 0;
                        while (Map.IsTransparent(map[observer.X + x + adjacency, observer.Y + y - q]) &&
                            x * x + (y - q) * (y - q) < visionRange * visionRange) {
                            this[x, y - 1 - q] = true;
                            q++;
                        }
                        break;
                    }
                }
            }
            for ( int i = -visionRange; i <= visionRange; ++i ) {
	            int x = 0;
	            int y = 0;
	            int it = 0;
                int adjacency = 0;
	            while (x * x + y * y < visionRange * visionRange) {
		            it++;
		            y++; 
		            x = (i * it) / visionRange;
		            this[x, y] = true;
                    if (x > 0) adjacency = -1;
                    if (x < 0) adjacency = 1;
                    if (!Map.IsTransparent(map[observer.X + x, observer.Y + y]) ) {
                        int q = 0;
                        while (Map.IsTransparent(map[observer.X + x + adjacency, observer.Y + y + q]) &&
                            x * x + (y + q) * (y + q) < visionRange * visionRange) {
                            this[x, y + 1 + q] = true;
                            q++;
                        }
                        break;
                    }
                }
            } 
        }

        public override int MaxVisionRange {
            get { return 7; }
        }

    }
}
