using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Translations;
using VH.Engine.World.Beings;

namespace MyRoguelike.Game.Beings {
    public class MyPc : Pc {

        public MyPc(): base() {
            identity = Accusativ = Translator.Instance["you"];
        }

        public override int Health {
            get { return 10; }      // replace with actual logic
            set { }                 // replace with actual logic
        }

        public override int MaxHealth {
            get { return 10; }      // replace with actual logic
        }

        public override int Attack {
            get { return 10; }      // replace with actual logic
        }

        public override int Defense {
            get { return 10; }      // replace with actual logic
        }

        public override int DistanceAttack {
            get { return 10; }                 // replace with actual logic
        }

        public override void Move() {
        }

        public override string Name { get => base.Name; set => base.Name = value; }
    }
}
