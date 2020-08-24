using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World;
using VH.Engine.World.Beings;
using VH.Engine.Levels;
using System.Xml;
using System.Windows.Forms;
using VH.Engine.Random;
using VH.Engine.Game;
using System.Text.RegularExpressions;

namespace VH.Game.World.Beings {

    public class MonsterGenerator {

        #region constants

        private const string LEVEL = "level";
        private const string NAME = "name";
        private const string GENERATOR = "generator";
        private const string MINCOUNT = "mincount";
        private const string MAXCOUNT = "maxcount";
        private const string XPATH = "xpath";

        private readonly string XML_PATH;

        #endregion

        #region fields

        XmlDocument doc;
        XmlElement root;
        protected MonsterFacade facade = new MonsterFacade();

        #endregion

        #region constructors

        public MonsterGenerator() {
            XML_PATH = Application.StartupPath + @"\Data\Resources\monster-generation.xml";
            doc = new XmlDocument();
            doc.Load(XML_PATH);
            root = doc.DocumentElement;
        }

        #endregion

        #region public methods

        public void Generate(Level level) {
            XmlElement levelElement = matchLevel(level.Name);
            if (levelElement == null) return;
            level.Monsters.Clear();
            foreach (XmlNode node in levelElement.ChildNodes) {
                if (node is XmlElement) {
                    XmlElement generator = (XmlElement)node;
                    if (generator.Name == GENERATOR) {
                        int mincount = int.Parse(generator.Attributes[MINCOUNT].Value);
                        int maxcount = int.Parse(generator.Attributes[MAXCOUNT].Value);
                        int count = mincount + Rng.Random.Next(maxcount - mincount + 1);
                        string xpath = generator.Attributes[XPATH].Value;
                        xpath = xpath.Replace("$danger", "" + level.Danger);
                        for (int i = 0; i < count; ++i) {
                            Monster monster = facade.CreateMonster(xpath);
                            if (monster.ChoosePosition()) level.Monsters.Add(monster);
                        }
                    }
                }
            }
        }

        #endregion

        #region private methods

        private XmlElement matchLevel(string name) {
            XmlNodeList levelList = root.SelectNodes("//level");
            foreach (XmlElement levelElement in levelList) {
                Regex regex = new Regex(levelElement.Attributes[NAME].Value);
                if (regex.IsMatch(name)) return levelElement;
            }
            return null;
        }

        #endregion

    }
}
