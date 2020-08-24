using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VH.Engine.Levels {

    public abstract class AbstractMapGenerator: Persistency.AbstractPersistent {

        protected Map map;

        public Map Map {
            get { return map; }
            set { map = value; }
        }

        public abstract Map Generate(int width, int height);

        public abstract Position GenerateFeature(char feature);

    }
}
