using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.Levels {

    /// <summary>
    /// Represents a passage (such as a stairway) from this Level
    /// to another Level
    /// </summary>
    public class Passage: AbstractPersistent {

        #region constants

        private const string POSITION = "position";
        private const string TARGET_LEVEL_NAME = "target-level-name";

        #endregion

        #region fields

        private Level targetLevel;
        private Position position;
        private string targetLevelName;

        #endregion

        #region constructors

        public Passage(Level targetLevel) {
            this.targetLevel = targetLevel;
        }

        public Passage() { }

        #endregion

        #region properties

        public string TargetLevelName {
            get {
                if (TargetLevel != null) return TargetLevel.Name;
                return targetLevelName;
            }
        }

        public Level TargetLevel {
            get { return targetLevel; }
            set { targetLevel = value; }
        }

        public Position Position {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            position  = GetElement(POSITION) as Position;
            targetLevelName = GetStringAttribute(TARGET_LEVEL_NAME);
            // the target level is not getting loaded here in order to avoid infinite recursion
            // this class relies on LevelPersistencyHelper class to sort out target level.
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element =  base.ToXml(name, doc);
            AddElement(POSITION, position);
            AddAttribute(TARGET_LEVEL_NAME, targetLevel.Name);
            return element;
        }

        #endregion

    }
}
