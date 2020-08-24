using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;

namespace VH.Game.World.Beings.Ai {

    public class HauntBehavior: OponentDependantBehaviour {

        #region constants

        private const string CHASE = "chase";
        private const string FLEE = "flee";

        private const int MAX_DISTANCE = 5;
        private const int MIN_DISTANCE = 3;

        #endregion

        #region fields

        private AbstractAi chase;
        private AbstractAi flee;
        private AbstractAi wander;

        #endregion

        #region properties

        public override Being Being {
            get { return base.Being; }
            set {
                base.Being = value;
                chase.Being = wander.Being = flee.Being = value;
            }
        }

        #endregion


        #region constructors

        public HauntBehavior(): base() { }

        public HauntBehavior(Being being, Being oponent): base(being) {
            this.Oponent = oponent;
            chase = new ChaseBehavior(being, oponent);
            flee = new FleeBehavior(being, oponent);
            wander = new NeutralBehavior(being);
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            chase = GetElement(CHASE) as AbstractAi;
            flee = GetElement(FLEE) as AbstractAi;
            wander = new NeutralBehavior(Being);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddElement(CHASE, chase);
            AddElement(FLEE, flee);
            return element;
        }

        public override AbstractAction SelectAction() {
            float distance = getDistance(Being.Position, Oponent.Position);
            if (distance > MAX_DISTANCE) return chase.SelectAction();
            if (distance < MIN_DISTANCE) return flee.SelectAction();
            return wander.SelectAction();
        }

        #endregion




    }
}
