using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VH.Engine.Levels {

    public class Terrain {

        #region constants

        private static readonly string XML_PATH;
        private const string KEY = "key";
        private const string CHARACTER = "character";
        private const string DISPLAY = "display";
        private const string COLOR = "color";
        private const string TRANSPARENT = "transparent";

        #endregion

        #region fields

        private string key;
        private char character;
        private ConsoleColor color;
        private char display;
        private bool transparent = true;

        private static List<Terrain> terrainKinds = new List<Terrain>();
        private static ConsoleColor defaultColor = ConsoleColor.Gray;

        #endregion

        #region constructors

        public Terrain(string key, string characterString) {
            this.key = key;
            character = getChar(characterString);
            display = character;
            color = defaultColor;
        }

        static Terrain() {
            XML_PATH = Application.StartupPath + @"\Data\Resources\terrain.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(XML_PATH);
            XmlNodeList nodes = doc.SelectNodes("//terrain");
            XmlAttribute defaultColorAttribute = (XmlAttribute)doc.SelectSingleNode("/terrain-kinds/@default-color");
            if (defaultColorAttribute != null) {
                defaultColor = getColor(defaultColorAttribute.Value);
            }
            foreach (XmlNode node in nodes) {
                terrainKinds.Add(createTerrain(node));
            }
        }

        #endregion

        #region properties

        public string Key {
            get { return key; }
        }

        public char Character {
            get { return character; }
        }

        public char Display {
            get { return display; }
        }

        public ConsoleColor Color {
            get { return color; }
        }

        public bool Transparent {
            get { return transparent; }
        }

        #endregion

        #region public methods

        public static Terrain Get(string key) {
            IEnumerable<Terrain> terrains = 
                from Terrain terrain in terrainKinds
                where terrain.Key == key
                select terrain;            
            if (terrains.Count() >= 1) return terrains.First();
            return new Terrain("UNKNOWN", "?");
        }

        public static Terrain Get(char character) {
            IEnumerable<Terrain> terrains =
                from Terrain terrain in terrainKinds
                where terrain.Character == character
                select terrain;
            if (terrains.Count() >= 1) return terrains.First();
            return new Terrain("UNKNOWN", "?");
        }

        #endregion

        #region private methods

        private static char getChar(string s) {
            if (s.Length == 1) {
                return s[0];
            } else {
                int i = int.Parse(s);
                return (char)i;
            }
        }

        private static ConsoleColor getColor(string s) {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), s); 
        }

        private static Terrain createTerrain(XmlNode node) {
            string key = node.Attributes[KEY].Value;
            string characterString = node.Attributes[CHARACTER].Value;
            Terrain terrain = new Terrain(key, characterString);
            if (node.Attributes[DISPLAY] != null) terrain.display = getChar(node.Attributes[DISPLAY].Value);
            if (node.Attributes[COLOR] != null) terrain.color = getColor(node.Attributes[COLOR].Value);
            if (node.Attributes[TRANSPARENT] != null) terrain.transparent = bool.Parse(node.Attributes[TRANSPARENT].Value);
            return terrain;
        }

        #endregion


    }
}
