using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.Persistency;
using System.Xml;

namespace VH.Engine.Levels {

    /// <summary>
    /// Describes relationship between levels.
    /// Defines which level can be reached from a given level.
    /// </summary>
    public class Level: AbstractPersistent {

        #region constants

        private const string PERSISTENT = "persistent";
        private const string BIDIRECTIONAL = "bidirectional";
        private const string NAME = "name";
        private const string LEVEL_WIDTH = "level-width";
        private const string LEVEL_HEIGHT = "level-height";
        private const string DANGER = "danger";
        private const string MAP = "map";
        private const string MONSTERS = "monsters";
        private const string ITEMS = "items";
        private const string UP_PASSAGES = "up-passages";
        private const string DOWN_PASSAGES = "down-passages";
        private const string MAP_GENERATOR = "map-generator";

        #endregion

        #region fields

        private Map map = null;
        private bool persistent = true;
        private bool bidirectional = true;
        private string name;
        private AbstractMapGenerator mapGenerator;
        private int levelWidth;
        private int levelHeight;
        private int danger;

        private List<Passage> upPassages = new List<Passage>();
        private List<Passage> downPassages = new List<Passage>();

        protected List<Monster> monsters = new List<Monster>();
        protected List<Item> items = new List<Item>();

        #endregion

        #region constructors

        public Level(string name, AbstractMapGenerator mapGenerator,
                int levelWidth, int levelHeight) {
            this.name = name;
            this.mapGenerator = mapGenerator;
            this.levelWidth = levelWidth;
            this.levelHeight = levelHeight;
        }

        public Level(string name, AbstractMapGenerator levelGenerator,
                int levelWidth, int levelHeight, int danger)
            : this(name, levelGenerator, levelWidth, levelHeight) {
            this.danger = danger;
        }

        public Level() { } 

        #endregion

        #region properties

        public List<Passage> UpPassages {
            get { return upPassages; }
        }

        public List<Passage> DownPassages {
            get { return downPassages; }
        }

        public Map Map {
            get { return map; }
            set { map = value; }
        }

        public bool Persistent {
            get { return persistent; }
            set { persistent = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public AbstractMapGenerator MapGenerator {
            get { return mapGenerator; }
            set { mapGenerator = value; }
        }

        public int LevelWidth {
            get { return levelWidth; }
            set { levelWidth = value; }
        }

        public int LevelHeight {
            get { return levelHeight; }
            set { levelHeight = value; }
        }

        public bool Bidirectional {
            get { return bidirectional; }
            set { bidirectional = value; }
        }

        public int Danger {
            get { return danger; }
            set { danger = value; }
        }

        public List<Monster> Monsters {
            get { return monsters; }
        }

        public List<Item> Items {
            get { return items; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            persistent = GetBoolAttribute(PERSISTENT);
            bidirectional = GetBoolAttribute(BIDIRECTIONAL);
            name = GetStringAttribute(NAME);
            levelWidth = GetIntAttribute(LEVEL_WIDTH);
            levelHeight = GetIntAttribute(LEVEL_HEIGHT);
            danger = GetIntAttribute(DANGER);
            map = GetElement(MAP) as Map;
            mapGenerator = GetElement(MAP_GENERATOR) as AbstractMapGenerator;
            mapGenerator.Map = map;            
            monsters = GetElements<Monster>(MONSTERS);
            items = GetElements<Item>(ITEMS);
            upPassages = GetElements<Passage>(UP_PASSAGES);
            downPassages = GetElements<Passage>(DOWN_PASSAGES);
        }
    
        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element =  base.ToXml(name, doc);
            AddAttribute(PERSISTENT, persistent);
            AddAttribute(BIDIRECTIONAL, bidirectional);
            AddAttribute(NAME, this.name); // TODO terrible
            AddAttribute(LEVEL_WIDTH, levelWidth);
            AddAttribute(LEVEL_HEIGHT, levelHeight);
            AddAttribute(DANGER, danger);
            AddElement(MAP, map);
            AddElement(MAP_GENERATOR, mapGenerator);
            AddElements(MONSTERS, monsters.Cast<AbstractPersistent>());
            AddElements(ITEMS, items.Cast<AbstractPersistent>());
            AddElements(UP_PASSAGES, upPassages.Cast<AbstractPersistent>());
            AddElements(DOWN_PASSAGES, downPassages.Cast<AbstractPersistent>());
            return element;
        }

        /// <summary>
        /// Enters the LevelStructure. 
        /// Sets up or loads the Map.
        /// </summary>
        public void Enter() {
            if (Persistent && map != null) {
                map.Load(Name);
            } else {
                Map = mapGenerator.Generate(levelWidth, levelHeight);
                foreach (Passage passage in upPassages) {
                    Position position = mapGenerator.GenerateFeature(Terrain.Get("upstair").Character);
                    passage.Position = position;
                }
                foreach (Passage passage in downPassages) {
                    Position position = mapGenerator.GenerateFeature(Terrain.Get("downstair").Character);
                    passage.Position = position;
                }
                // TODO consider refactoring this to avoid circular reference to GameController
                GameController.Instance.ItemGenerator.Generate(this);
                GameController.Instance.MonsterGenerator.Generate(this);
            }
        }

        /// <summary>
        /// Leaves the Level.
        /// Saves the map if needed.
        /// </summary>
        public void Leave() {
            if (Persistent) map.Save(Name);
        }

        public void AddLevelBelow(Level child) {
            downPassages.Add(new Passage(child));
            if (child.Bidirectional) child.AddLevelAbove(this);
        }

        public void AddLevelAbove(Level parent) {
            upPassages.Add(new Passage(parent));
        }

        /// <summary>
        /// Returns a Level  
        /// to which a passage (e.g. stairway) exists at a given Position
        /// or null, if no passage is found at this Position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Level GetNextLevel(Position position) {
            foreach (Passage passage in upPassages) {
                if (passage.Position.Equals(position)) return passage.TargetLevel;
            }
            foreach (Passage passage in downPassages) {
                if (passage.Position.Equals(position)) return passage.TargetLevel;
            }
            return null;
        }

        public Position GetPassagePosition(Level level) {
            foreach (Passage passage in upPassages) {
                if (passage.TargetLevel == level) return passage.Position;
            }
            foreach (Passage passage in downPassages) {
                if (passage.TargetLevel == level) return passage.Position;
            }
            return null;
        }

        /// <summary>
        /// Returns a Being occupying the specified square of current level
        /// or null if no Being is present on that square.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Monster GetMonsterAt(Position position) {
            foreach (Monster monster in monsters) {
                if (monster.Position.Equals(position)) return monster;
            }
            return null;
        }

        /// <summary>
        /// Returns a list of Items lying on the ground on the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IEnumerable<Item> GetItemsAt(Position position) {
            return
                from item in items
                where item.Position.Equals(position)
                select item;
        }

        #endregion

    }
}
