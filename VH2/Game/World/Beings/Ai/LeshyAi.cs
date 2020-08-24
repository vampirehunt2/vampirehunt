using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {

    public class LeshyAi : HostileAi {

        #region constants

        private const string DORMANT = "dormant";
        private const int MAX_DISTANCE = 3;

        #endregion

        #region fields

        private bool dormant = true;

        #endregion

        #region constructors

        public LeshyAi(): base() {
        }

        public LeshyAi(Being being) : base(being) {
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            dormant = GetBoolAttribute(DORMANT);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(DORMANT, dormant);
            return element;
        }

        public override AbstractAction SelectAction() {
            if (dormant) {
                Being oponent = findOponent();
                if (oponent != null) {
                    wakeUp();
                }
                return new WaitAction(Being);
            } else {
                return base.SelectAction();
            }
        }

        #endregion

        #region private methods

        private Being findOponent() {
            foreach (Being oponent in GameController.Instance.Beings) {
                if (oponent.Race == "human"
                    && Being.Position.Distance(oponent.Position) <= MAX_DISTANCE) {
                    return oponent;
                }
            }
            return null;
        }

        private void wakeUp() {
            Being.Color = ConsoleColor.Green;
            dormant = false;
            Notify("wake-up");
        }

        #endregion

    }
}
