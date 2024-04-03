using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Game;
using VH.Engine.Display;
using VH.Engine.Levels;
using VH.Engine.LineOfSight;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;
using VH.Engine.Random;
using VH.Game.World.Beings;
using VH.Engine.Configuration;
using VH.Game.World.Items;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH.Levels;
using VH.Game.World.Beings.Ai;
using VH.Engine.Persistency;
using System.Windows.Forms;
using VH.Engine.Translations;
using VH.Game.World.Beings.Professions;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VH.Engine.VhConsole;

namespace VH.Game {

    public class VhGameController: GameController {


        #region constants

        #endregion

        #region fields

        protected Keybindings keybindings = Keybindings.Instance;
        string command;

        #endregion

        #region constructor

        public VhGameController() {
            
        }

        #endregion

        #region properties

        public override Level Level {
            get {
                return base.Level;
            }
            set {
                base.Level = value;
                showLevelName();
            }
        }

        public Keybindings Keybindings {
            get { return keybindings; }
        }

        public string Command {
            get { return command; }
        }

        public IEnumerable<Being> AllBeings {
            get {
                List<Being> beings = new List<Being>();
                foreach (Monster monster in Level.Monsters) beings.Add(monster);
                beings.Add(pc);
                return beings;
            }
        }

        #endregion

        #region public methods

        #endregion

        #region protected methods

        protected override void loadGame(string filename) {
            initGenerators();
            try {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                initDisplay();
                FromXml(doc.DocumentElement.SelectSingleNode("game") as XmlElement);
            } catch (Exception ex) {
                Console.Clear();
                Console.Write("'" + filename + "' is corrupted or is not a valid save file\n");
                Console.Write(ex.StackTrace);
                Console.Write("press any key");
                Console.ReadKey();
                throw;
            }
            drawFrames();
            showLevelName();
            
            welcome();
        }

        protected override void setUpGame() {
            initDisplay();
            initPc();
            showSplash();
            drawFrames();
            initGenerators();
            initViewPort();
            setUpLevelStructure();
            initProfession();
            welcome();
        }

        protected override void tearDownGame() {
            saveInHof();
            Application.Exit();
        }

        protected override void runTurn() {
            base.runTurn();

            // display the whole game screen
            console.ForegroundColor = ConsoleColor.Gray;
            showStatus();
            fieldOfVision.ComputeFieldOfVision(Map, pc.Position, (Pc as VhPc).VisionRange);
            viewPort.RenderMap(fieldOfVision);
            foreach (Item item in Level.Items) viewPort.Display(item, fieldOfVision, pc.Position);
            foreach (Monster monster in Level.Monsters) viewPort.Display(monster, fieldOfVision, pc.Position);
            viewPort.Display(pc, fieldOfVision, pc.Position);
            viewPort.Refresh();

            // select next action
            GameController.Instance.Console.ClearBuffer();
            char c = GameController.Instance.Console.ReadKey();
            AbstractAction action = null; // resets the action on each turn
            command = keybindings[c];

            // in-game actions. 
            // these are actions performed by the pc.
            action = pc.Ai.SelectAction();
            //MessageWindow.Clear();

            // perform selected pc action, if there is one.
            if (action != null && action.Perform()) {
                gametimeTicks = (int)(action.TimeNeeded / pc.Speed);
                moveMonsters();
                Pc.Move();
                runBaseAction(Pc);
            } else {
                // out-of-game actions. 

                // these are actions performed by the player.
                // these actions do not take up gametime.
                if      (Command == "backpack") showPcInventory();
                else if (Command == "show-equipment") showEquipment();
                else if (Command == "show-stats") showAttributes();
                else if (Command == "show-help") showHelp();
                else if (Command == "message-log") showMessageLog();
                else if (Command == "show-plan") showPlan();
                else if (Command == "quit") quit(); 
                else if (Command == "save") saveGame();
            }
        }

        protected override void runBaseAction(Being performer) {
            new VhAction(performer).Perform();
        }

        #endregion

        #region IXmlSerializable

        public XmlSchema GetSchema() {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader) {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteStartDocument();
            writer.WriteStartElement("vh-saved-game");


            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        #endregion

        #region private methods

        private void initDisplay() {
            console = new VhConsole();
            console.ForegroundColor = ConsoleColor.Gray;
            fieldOfVision = new RaycastingFieldOfVision();
            messageWindow = new ScrollingVerticalMessageWindow(50, 1, 28, 48, console);
            messageManager = new MessageManager(messageWindow);
            console.CursorVisible = false;
            console.Height = 50;
            console.Width = 80;
        }

        private void initPc() {
            pc = new VhPc();
            pc.Character = '@';
            pc.Ai = new PcAi(pc);
        }

        private void showSplash() {
            SplashScreen splash = new SplashScreen();
            splash.Show();
            GameController.Instance.Pc.Name = splash.PcName;
        }

        private void showLevelName() {
            console.Clear(5, 49, 10, 1);
            console.GoTo(5, 49);
            console.ForegroundColor = ConsoleColor.Gray;
            string levelString = Level.Name;
            while (levelString.Length < 8) levelString += " ";
            console.Write(CP437.LEFT_FRAME_LINK + levelString + CP437.RIGHT_FRAME_LINK);
        }

        private void initProfession() {
            VH.Engine.Display.Menu professionMenu = new LetterMenu(
                Translator.Instance["choose-profession"],
                new object[] { 
                    new Custodian(Pc),
                    new Quack(Pc),
                    new Gravedigger(Pc),
                    new Hunter(Pc)
                },
                MessageWindow,
                false
            );
            professionMenu.ShowMenu();
            ((VhPc)Pc).Profession = (AbstractProfession)professionMenu.SelectedItem;
            ((VhPc)Pc).Profession.InitBeing();
            foreach (Skill skill in ((VhPc)Pc).Skills) {
                skill.Value = Rng.Random.Next(skill.MaxValue) + 1;
            }
            Pc.Health = Pc.MaxHealth;
        }

        private void initGenerators() {
            itemGenerator = new VhItemGenerator(new ItemFacade());
            monsterGenerator = new MonsterGenerator();
        }

        private void initViewPort() {
            viewPort = new ViewPort(1, 1, 48, 48, console, new Position(0, 0));
            //viewPort.Shade = false;
        }

        private void welcome() {
            console.GoTo(16, 0);
            console.Write(Pc.Name + ", " + ((VhPc)Pc).Profession.Name);
            MessageWindow.Clear();
            MessageWindow.ShowMessage(Translator.Instance["welcome"] + Pc.Name + "!");
        }

        private void quit() {
            string msg = Translator.Instance["quit?"];
            if (new YesNoMenu(msg, messageWindow, 'T', 'N').ShowMenu() == MenuResult.OK) {
                QuitGame = true;
            }
            messageWindow.Clear();
        }

        private void setUpLevelStructure() {
            Level = new LevelGenerator().CreateLevelStructure();
            viewPort.Map = Map;
        }

        private void drawFrames() {
            // upper frame border
            console.Clear();
            console.GoTo(0, 0);
            console.Write(CP437.UPPER_LEFT_CORNER);
            for (int i = 1; i < 79; ++i) {
                if (i == 14) console.Write(CP437.LEFT_FRAME_LINK);
                else if (i == 35) console.Write(CP437.RIGHT_FRAME_LINK);
                else if (i > 14 && i < 35) console.Write(' ');
                else if (i == 49) console.Write(CP437.TOP_FRAME_LINK);
                else console.Write(CP437.HORIZONTAL_BORDER);
            }
            console.Write(CP437.UPPER_RIGHT_CORNER);

            // lower frame border
            console.GoTo(0, 49);
            console.Write(CP437.LOWER_LEFT_CORNER);
            for (int i = 1; i < 79; ++i) {
                if (i == 52) console.Write(CP437.LEFT_FRAME_LINK);
                else if (i == 76) console.Write(CP437.RIGHT_FRAME_LINK);
                else if (i > 52 && i < 76) console.Write(' ');
                else if (i == 49) console.Write(CP437.BOTTOM_FRAME_LINK);
                else console.Write(CP437.HORIZONTAL_BORDER);
            }
            console.Write(CP437.LOWER_RIGHT_CORNER);
            console.GoTo(55, 49);
            console.Write("Gł Os Sp Pa Z:     ");

            // vertical borders
            for (int i = 1; i < 49; ++i) {
                console.Write(CP437.VERTICAL_BORDER, 0, i);
                console.Write(CP437.VERTICAL_BORDER, 49, i);
                console.Write(CP437.VERTICAL_BORDER, 79, i);
            }
            console.GoTo(0, 0);
        }

        private void showPcInventory() {
            messageWindow.Clear();
            console.ForegroundColor = ConsoleColor.Gray;
            messageWindow.ShowMessage(((IBackPackBeing)pc).BackPack.ToString());
        }

        protected void showHelp() {
            show(Keybindings.ToString());
        }

        private void showMessageLog() {
            show(MessageManager.MessageLog);
        }

        private void showEquipment() {
            show(((IEquipmentBeing)Pc).Equipment.ToString());
        }

        private void showAttributes() {
            show(pc.ToString());
        }

        private void showPlan() {
            if (Level.Map.Width == 96) {
                LevelPlan plan = new LevelPlan(Level.Map, pc.Position);
                plan.Show(viewPort);
            }
        }

        private void show(string s) {
            messageWindow.Clear();
            console.ForegroundColor = ConsoleColor.Gray;
            messageWindow.ShowMessage(s);
        }

        private void showStatus() {
            console.Clear(55, 49, 21, 1);
            console.GoTo(55, 49);

            TempSet temps = (Pc as ITempsBeing).Temps;
            showStatusPart(temps["confused"], "Os "); 
            showStatusPart(temps["ill"], "Ch ");
            showStatusPart(temps["poisoned"], "Za ");
            showStatusPart(temps["blind"], "Śl ");   
            showStatusPart(pc.Health <= 3,  "" + pc.Health + "/" + ((VhPc)pc).MaxHealth);
        }

        private void showStatusPart(bool condition, string part) {
            if (condition) console.ForegroundColor = ConsoleColor.Red; else console.ForegroundColor = ConsoleColor.Gray;
            console.Write(part); 
        }

        private void saveGame() {
            string filename = Application.StartupPath + @"\" + Pc.Name + "_svg.xml";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <vh2-saved-game/>");
            doc.DocumentElement.AppendChild(ToXml("game", doc));
            doc.Save(filename);
            Environment.Exit(0);
         }

        private void saveInHof() {
            StreamWriter writer = File.AppendText(Application.StartupPath + @"\Data\cementary.txt");
            StringBuilder builder = new StringBuilder();
            builder.Append(Pc.Name + ", ");
            builder.Append(((IProfessionBeing)Pc).Profession.Name + " ");
            builder.Append(Translator.Instance["kill-reason"]);
            builder.Append(Pc.KillReason);
            builder.Append(Translator.Instance["kill-place"]);
            builder.Append(Level.Name);
            writer.WriteLine(builder.ToString());
            writer.Close();
        }


        #endregion

    }
}