using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using VH.Engine.Tools;

namespace VH.Engine.Levels {

    public class LevelGenerator {

        #region constants

        private readonly string XML_PATH;

        private const string BIDIRECTIONAL = "bidirectional";
        private const string PERSISTENT = "persistent";
        private const string DANGER = "danger";
        private const string PARENT = "parent";
        private const string CHILD = "child";
        private const string GENERATOR = "generator";
        private const string WIDTH = "width";
        private const string HEIGHT = "height";
        private const string DEFAULT_WIDTH = "default-width";
        private const string DEFAULT_HEIGHT = "default-height";
        private const string DEFAULT_GENERATOR = "default-generator";
        private const string ROOT = "root";

        private const string NAME = "name";
        private const string TYPE = "type";
        private const string ASSEMBLY = "assembly";

        #endregion

        #region fields

        XmlDocument doc;
        XmlNodeList generatorNodes;
        List<Level> levels = new List<Level>();
        string defaultGeneratorName;
        int defaultWidth;
        int defaultHeight;

        #endregion

        #region constructors

        public LevelGenerator() {
            XML_PATH = Application.StartupPath + @"\Data\Resources\levels.xml";
            doc = new XmlDocument();
            doc.Load(XML_PATH);
            generatorNodes = doc.SelectNodes("//generator");
            XmlElement main = doc.DocumentElement;
            defaultWidth = int.Parse(main.Attributes[DEFAULT_WIDTH].Value);
            defaultHeight = int.Parse(main.Attributes[DEFAULT_HEIGHT].Value);
            defaultGeneratorName = main.Attributes[DEFAULT_GENERATOR].Value;
        }

        #endregion

        #region public methods

        public Level CreateLevelStructure() {
            processLevels();
            processConnections();
            string rootLevelName = ((XmlAttribute)doc.SelectSingleNode("/levels/@root")).Value;
            return findLevel(rootLevelName);
        }

        #endregion

        #region private methods

        private void processChildren() { }

        private AbstractMapGenerator createMapGenerator(XmlNode generatorNode) {
            string typeName = generatorNode.Attributes[TYPE].Value;
            string assemblyName = generatorNode.Attributes[ASSEMBLY].Value;
            return (AbstractMapGenerator)AssemblyCache.CreateObject(typeName, assemblyName); 
        }

        private AbstractMapGenerator createMapGenerator(string name) {
            foreach (XmlNode node in generatorNodes) {
                if (node.Attributes[NAME].Value == name) {
                    return createMapGenerator(node);
                }
            }
            throw new ArgumentException("Invalid MapGenerator name: " + name);
        }

        private Level createLevel(XmlNode levelNode) {
            string name = levelNode.Attributes[NAME].Value;;
            AbstractMapGenerator generator = getMapGenerator(levelNode);
            int width;
            int height;
            if (levelNode.Attributes[WIDTH] != null) {
                width = int.Parse(levelNode.Attributes[WIDTH].Value);
            } else {
                width = defaultWidth;
            }
            if (levelNode.Attributes[HEIGHT] != null) {
                height = int.Parse(levelNode.Attributes[HEIGHT].Value);
            } else {
                height = defaultHeight;
            }
            Level level = new Level(name, generator, width, height);
            if (levelNode.Attributes[DANGER] != null) {
                level.Danger = int.Parse(levelNode.Attributes[DANGER].Value);
            }
            if (levelNode.Attributes[BIDIRECTIONAL] != null) {
                level.Bidirectional = bool.Parse(levelNode.Attributes[BIDIRECTIONAL].Value);
            } 
            if (levelNode.Attributes[PERSISTENT] != null) {
                level.Persistent = bool.Parse(levelNode.Attributes[PERSISTENT].Value);
            }
            return level;
        }

        private AbstractMapGenerator getMapGenerator(XmlNode levelNode) {
            string name;
            if (levelNode.Attributes[GENERATOR] != null) {
                name = levelNode.Attributes[GENERATOR].Value;
            } else {
                name = defaultGeneratorName;
            }
            return createMapGenerator(name);
        }

        private void processLevels() {
            XmlNodeList levelNodes = doc.SelectNodes("//level");
            foreach (XmlNode levelNode in levelNodes) {
                Level level = createLevel(levelNode);
                levels.Add(level);
            }
        }

        private void processConnections() {
            XmlNodeList connectionNodes = doc.SelectNodes("//connection");
            foreach (XmlNode connectionNode in connectionNodes) {
                Level parent = findLevel(connectionNode.Attributes[PARENT].Value);
                Level child = findLevel(connectionNode.Attributes[CHILD].Value);
                parent.AddLevelBelow(child);
            }
        }

        private Level findLevel(string name) {
            return (
                from Level level in levels 
                where level.Name == name
                select level
            ).Single();
        }

        #endregion

    }
}
