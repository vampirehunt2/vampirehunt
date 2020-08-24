using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Random {

    public class SystemRng: Rng {

        private System.Random random = new System.Random();

        public override int Next(int a) {
            return random.Next(a);
        }

        public override float NextFloat() {
            return (float)random.NextDouble();
        }

        public override void Randomize() {
            random = new System.Random(DateTime.Now.Millisecond);
        }

    }
}
