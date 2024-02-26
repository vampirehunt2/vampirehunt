using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Display;

namespace VH.Engine.World.Beings {

    public abstract class Pc: Being {

        protected string identity = "";

        public override string Identity {
            get { return identity; }  
        }

        public override Person Person {
            get { return Person.Second; }
        }

        public abstract void Move();

    }
}
