using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;
using System.Xml;
using VH.Engine.Levels;

namespace VH.Game.World.Beings {

    public class VhMonster: Monster, ITempsBeing {

        #region constants

        private const string TEMPS = "temps";

        #endregion

        #region fields

        private TempSet temps = new TempSet();

        #endregion

        #region properties

        public TempSet Temps {
            get { return temps; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            temps = GetElement(TEMPS) as TempSet;
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element =  base.ToXml(name, doc);
            AddElement(TEMPS, temps);
            return element;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
        }

        #endregion


    }
}
