using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2.Game.World.Items {

    public interface MagicalItem {

        string HiddenName { get; set; }

        string HiddenAccusativ { get; set; }

        string HiddenPlural { get; set; }

        int IdentifyValue { get; set; }

        void Identify();

    }
}
