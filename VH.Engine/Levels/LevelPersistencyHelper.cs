using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.Levels {
    public class LevelPersistencyHelper: AbstractPersistent {

        #region constants

        private const string LEVEL_STRUCTURE = "level-structure";
        private const string LEVELS = "levels";
        private const string STARTING_LEVEL = "starting-level";

        #endregion

        #region fields

        private Level startingLevel;

        #endregion

        #region constructors

        public LevelPersistencyHelper(Level startingLevel) {
            this.startingLevel = startingLevel;
        }

        public LevelPersistencyHelper() { }

        #endregion

        #region properties

        public Level StartingLevel {
            get { return startingLevel; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            List<Level> levels = GetElements<Level>(LEVEL_STRUCTURE);
            foreach (Level level in levels) {
                foreach (Passage passage in level.UpPassages) {
                    passage.TargetLevel = getLevelByName(passage.TargetLevelName, levels);
                }
                foreach (Passage passage in level.DownPassages) {
                    passage.TargetLevel = getLevelByName(passage.TargetLevelName, levels);
                }
            }
            startingLevel = getLevelByName(GetStringAttribute(STARTING_LEVEL), levels);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(STARTING_LEVEL, startingLevel.Name);
            HashSet<AbstractPersistent> traversed = new HashSet<AbstractPersistent>();
            traverse(startingLevel, traversed);
            List<AbstractPersistent> levels = new List<AbstractPersistent>();
            foreach (AbstractPersistent persistent in traversed) {
                if (persistent is Level) {
                    levels.Add(persistent);
                }
            }
            AddElements(LEVEL_STRUCTURE, levels);
            return element;
        }

        #endregion

        #region private methods

        private void traverse(Level level, HashSet<AbstractPersistent> traversed) {
            if (!traversed.Contains(level)) {
                traversed.Add(level);
                foreach (Passage passage in level.UpPassages) traverse(passage, traversed);
                foreach (Passage passage in level.DownPassages) traverse(passage, traversed);
            }

        }

        private void traverse(Passage passage, HashSet<AbstractPersistent> traversed) {
            if (!traversed.Contains(passage)) {
                traversed.Add(passage);
                traverse(passage.TargetLevel, traversed);
            }
        }

        private static Level getLevelByName(string name, List<Level> levels) {
            foreach (Level level in levels) {
                if (level.Name == name) return level;
            }
            return null;
        }

        #endregion

    }
}
