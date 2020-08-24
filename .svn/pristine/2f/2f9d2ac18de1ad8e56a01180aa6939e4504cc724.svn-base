using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;

namespace VH.Engine.World.Items {

    public abstract class AbstractItemGenerator {

        protected ItemFacade facade;

        public ItemFacade ItemFacade {
            get { return facade; }
        }

        public AbstractItemGenerator(ItemFacade facade) {
            this.facade = facade;
        }

        public abstract void Generate(Level level);

    }
}
