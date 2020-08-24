using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;

namespace VH.Engine.World.Beings {

    public class MonsterFacade: EntityCreator {
                
        #region constructors

        public MonsterFacade() : base("Data/Resources/monsters.xml") { }

        #endregion

        #region public methods

        public Monster CreateMonster(string xpath) {
            return (Monster)Generate(xpath);
        }

        public Monster CreateRandomMonster() {
            return (Monster)Generate("//monster");
        }

        public Monster CreateMonsterByDanger(int danger) {
            return (Monster)Generate("//monster[@danger<=" + danger + "]");
        }

        #endregion


    }
}
