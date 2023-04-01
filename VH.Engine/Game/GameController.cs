using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;
using VH.Game.World.Beings;
using VH.Engine.World.Items;
using VH.Engine.Levels;
using VH.Engine.Display;
using System.Windows.Forms;
using VH.Engine.World.Beings.Actions;
using VH.Engine.LineOfSight;
using VH.Engine.World.Beings;
using VH.Engine.Time;
using VH.Engine.Persistency;
using System.Xml;
using System.Drawing;

namespace VH.Engine.Game {

    /// <summary>
    /// Main game object that handles all the game flow.
    /// </summary>
    public abstract class GameController: AbstractPersistent {

        #region singleton

        private static GameController instance;

        public static GameController Instance {
            get { return instance; }
            set {
                if (instance == null) instance = value;
                else throw new InvalidOperationException("Game controller already initialized");
            }
        }

        protected GameController() {

        }

        #endregion

        #region fields

        protected Pc pc;
        protected Level level;

        protected IConsole console;
        protected AbstractFieldOfVision fieldOfVision;
        protected MessageWindow messageWindow;
        protected ViewPort viewPort;
        protected TimeManager timeManager;
        protected MessageManager messageManager;

        protected AbstractItemGenerator itemGenerator;
        protected MonsterGenerator monsterGenerator;

        private bool quitGame = false;
        protected int gametimeTicks = 0;

        

        #endregion

        #region properties

        public bool QuitGame {
            get { return quitGame; }
            set { quitGame = value; }
        }

        /// <summary>
        /// the Map that the game is currently taking place on
        /// </summary>
        public Map Map {
            get { return Level.Map; }
            set { Level.Map = value; }
        }

        /// <summary>
        /// Level that wraps current Map
        /// </summary>
        public virtual Level Level {
            get { return level; }
            set {
                beforeLevelChange();
                Level previousLevel = level;
                level = value;
                level.Enter();
                ViewPort.Map = Map;
                if ( previousLevel != null && previousLevel.Bidirectional ) {
                    PlacePC(level.GetPassagePosition(previousLevel));
                } else {
                    PlacePC();
                }
                afterLevelChange();
            }
        }

        /// <summary>
        /// The IConsole instance that this game uses
        /// </summary>
        public IConsole Console {
            get { return console; }
        }

        /// <summary>
        /// The ViewPort used to display the map
        /// </summary>
        public ViewPort ViewPort {
            get { return viewPort; }
        }

        public MessageManager MessageManager {
            get { return messageManager; } 
        }

        public AbstractItemGenerator ItemGenerator {
            get { return itemGenerator; }
        }

        public MonsterGenerator MonsterGenerator {
            get { return monsterGenerator; }
        }

        public Pc Pc {
            get { return pc; }
        }

        public AbstractFieldOfVision FieldOfVision {
            get { return fieldOfVision; } 
        }

        public MessageWindow MessageWindow {
            get { return messageWindow; }
        }

        public List<Being> Beings {
            get {
                List<Being> beings = new List<Being>();
                beings.Add(Pc);
                foreach (Monster monster in Level.Monsters) beings.Add(monster);
                return beings;
            }
        }

        #endregion

        #region public methods

        public virtual AbstractPersistent FindPersistent(string uuid) {
            foreach (Being being in Beings) {
                if (being.Uuid == uuid) return being;
            }
            foreach (Item item in Level.Items) {
                if (item.Uuid == uuid) return item; 
            }
            return null; 
        }

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            gametimeTicks = GetIntAttribute("gametime-ticks"); // TODO convert XML element and attribute names to constants
            pc = GetElement("pc") as Pc;
            LevelPersistencyHelper helper = new LevelPersistencyHelper();
            helper.FromXml(element.SelectSingleNode("levels") as XmlElement); // TODO probably wrong
            level = helper.StartingLevel;
            fieldOfVision = GetElement("field-of-vision") as AbstractFieldOfVision;
            viewPort = GetElement("viewport") as ViewPort;
            //console = new SystemConsole();
            viewPort.Console = console;
            viewPort.Map = Level.Map;
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute("gametime-ticks", gametimeTicks);
            AddElement("viewport", viewPort);
            AddElement("pc", pc);
            AddElement("levels", new LevelPersistencyHelper(level));
            AddElement("field-of-vision", fieldOfVision);
            return element;
        }

        public virtual void PlacePC() {
            int i;
            int j;
            do {
                i = Rng.Random.Next(Map.Width);
                j = Rng.Random.Next(Map.Height);
            } while (!pc.CanWalkOn(viewPort.GetDisplayCharacter(Map[i, j])));
            pc.Position.X = i;
            pc.Position.Y = j;
        }

        public virtual void PlacePC(Position position) {
            if (position == null) PlacePC();
            else pc.Position = position;
        }

        public void SetUpLevel() { }

        public void Play(string filename) {
            loadGame(filename);
            runGame();
        }

        public void Play() {
            setUpGame();
            runGame();
        }

        protected virtual void loadGame(string filename) {
            setUpGame(); // do not load, instead create a fresh game. Actual loading takes place in subclasses.
        }

        private void runGame() {
            // main gameloop
            while (!QuitGame) runTurn();
            tearDownGame();
            Application.Exit();
        }

        public Being GetBeingAt(Position position) {
            if (pc.Position.Equals(position)) return pc;
            else return Level.GetMonsterAt(position);
        }

        public bool IsFreeSpace(Position position, Being being) {
            return
                GetBeingAt(position) == null &&
                being.CanWalkOn(ViewPort.GetDisplayCharacter(Level.Map[position]));
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Reimplement in subclass to run a single game turn.
        /// Always call base.runTurn() when reimplementing 
        /// or handle viewPort scrolling yourself. 
        /// </summary>
        protected virtual void runTurn() {
            scroll();
        }

        /// <summary>
        /// Sets up the game.
        /// </summary>
        protected abstract void setUpGame();

        protected abstract void tearDownGame();

        protected virtual void moveMonsters() {
            for ( int i = 0; i < Level.Monsters.Count; ++i) {
                if (!QuitGame) { // <-- fix for the "PC being killed twice in one round by two separate Monsters" issue
                    Monster monster = Level.Monsters[i];
                    monster.Move(gametimeTicks);
                    runBaseAction(monster);
                }
            }
        }

        protected abstract void runBaseAction(Being performer);

        protected virtual void beforeLevelChange() { }

        protected virtual void afterLevelChange() { }

        #endregion

        #region private

        private void scroll() {
            if (Map.Width > viewPort.Width || Map.Height > viewPort.Height) {
                int x = pc.Position.X - viewPort.Width / 2;
                int y = pc.Position.Y - viewPort.Height / 2;
                if (x < 0) x = 0;
                if (y < 0) y = 0;
                if (x >= Map.Width - viewPort.Width) x = Map.Width - viewPort.Width;
                if (y >= Map.Height - viewPort.Height) y = Map.Height - viewPort.Height;
                viewPort.Position.X = x;
                viewPort.Position.Y = y;
                viewPort.PreventGlitches();
            } else {
                viewPort.Position.X = 0;
                viewPort.Position.Y = 0;
            }
        }

        #endregion

    }
}
