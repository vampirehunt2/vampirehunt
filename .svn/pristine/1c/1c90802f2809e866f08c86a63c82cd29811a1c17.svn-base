using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH2.Game.World.Items;
using System.Xml;
using VH.Engine.Translations;

namespace VH.Game.World.Items.Scrolls {

    public class Scroll: UsableItem, MagicalItem {

        #region constants

        private const string HIDDEN_NAME = "hidden-name";
        private const string HIDDEN_ACCUSATIV = "hidden-accusative";
        private const string HIDDEN_PLURAL = "hidden-plural";
        private const string IDENTIFY_VALUE = "identify-value";

        #endregion

        #region fields

        private string hiddenName;
        private string hiddenAccusativ;
        private string hiddenPlural;
        private int identifyValue;

        #endregion

        #region constructors

        public Scroll() {
            UseKind = "read";
        }

        #endregion

        #region properties

        public string HiddenName {
            get { return hiddenName; }
            set { hiddenName = value; }
        }

        public string HiddenAccusativ {
            get { return hiddenAccusativ; }
            set { hiddenAccusativ = value; }
        }

        public string HiddenPlural {
            get { return hiddenPlural; }
            set { hiddenPlural = value; }
        }

        public void Identify() {
            Name = HiddenName;
            Accusativ = HiddenAccusativ;
            Plural = HiddenPlural;
        }

        public int IdentifyValue {
            get { return identifyValue; }
            set { identifyValue = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            hiddenName = GetStringAttribute(HIDDEN_NAME);
            hiddenAccusativ = GetStringAttribute(HIDDEN_ACCUSATIV);
            hiddenPlural = GetStringAttribute(HIDDEN_PLURAL);
            identifyValue = GetIntAttribute(IDENTIFY_VALUE);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(HIDDEN_NAME, hiddenName);
            AddAttribute(HIDDEN_ACCUSATIV, hiddenAccusativ);
            AddAttribute(HIDDEN_PLURAL, hiddenPlural);
            AddAttribute(IDENTIFY_VALUE, identifyValue);
            return element;
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            HiddenName = Name;
            HiddenAccusativ = Accusativ;
            HiddenPlural = Plural;
            Name = Translator.Instance["scroll"];
            Accusativ = Translator.Instance["scroll-accusativ"];
            Plural = Translator.Instance["scroll-plural"];
        }

        public override string ToString() {
            return Name;
        }

        #endregion
    }
}
