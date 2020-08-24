using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.AI;

namespace VH.Engine.World.Beings.AI {
    public abstract class OponentDependantBehaviour: BaseAi {

        private const string OPONENT_UUID = "oponent-uuid";


        private Being oponent;
        private string oponentUuid;

        #region properties

        public OponentDependantBehaviour(): base() { }

        public OponentDependantBehaviour(Being being): base(being) { }

        public OponentDependantBehaviour(Being being, Being oponent): base(being) {
            this.oponent = oponent;
        }

        public Being Oponent {
            get {
                if (oponent == null && oponentUuid != null) {
                    oponent = GameController.Instance.FindPersistent(oponentUuid) as Being;
                }
                return oponent;
            }
            set {
                oponent = value;
            }
        }

        #endregion

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            if (HasAttribute(OPONENT_UUID)) oponentUuid = GetStringAttribute(OPONENT_UUID);

        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            if (Oponent != null) AddAttribute(OPONENT_UUID, Oponent.Uuid);
            return element;
        }

    }
}
