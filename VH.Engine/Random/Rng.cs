using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Random {

    public abstract class Rng {

        public static Rng Random = new SystemRng();

        public abstract int Next(int a);

        public abstract float NextFloat();

        public abstract void Randomize();

    }
}
