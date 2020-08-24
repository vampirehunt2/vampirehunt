using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using System.Xml;
using System.Windows.Forms;

namespace VH.Engine.Configuration {

    /// <summary>
    /// Manages Keybindings.
    /// Expects the keybindings file to be in Data/Conf/keybindings.xml
    /// </summary>
    public class Keybindings {

        #region fields

        private XmlDocument doc = new XmlDocument();

        #endregion

        #region singleton

        private static Keybindings instance = new Keybindings();

        /// <summary>
        /// The single instance of Keybindings
        /// </summary>
        public static Keybindings Instance { get { return instance; } }

        private Keybindings() {
            doc.Load(Application.StartupPath + @"/Data/Conf/keybindings.xml");
        }

        #endregion

        #region properties

        /// <summary>
        /// Returns a command name that is bound to the specified key
        /// </summary>
        /// <param name="key">the keyboard key</param>
        /// <returns>Command name that is bound to the specified key</returns>
        public string this[char key] {
            get {
                string xpath = "/keybindings/binding[@key='" + key + "']/@command";
                XmlNodeList nodes = doc.SelectNodes(xpath);
                if (nodes.Count == 0) return "";
                XmlAttribute attr = (XmlAttribute)nodes[0];
                return attr.Value;
            }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Return a step bound to a key
        /// </summary>
        /// <param name="key">the key pressed on an IConsole</param>
        /// <returns>A relevant Step or null if the key is not bound to any movement command</returns>
        public virtual Step GetStepForKey(char key) {
            string command = this[key];
            if (command == "north") return Step.NORTH;
            if (command == "south") return Step.SOUTH;
            if (command == "east") return Step.EAST;
            if (command == "west") return Step.WEST;
            if (command == "north-east") return Step.NORTH_EAST;
            if (command == "north-west") return Step.NORTH_WEST;
            if (command == "south-east") return Step.SOUTH_EAST;
            if (command == "south-west") return Step.SOUTH_WEST;
            return null;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            XmlNodeList nodes = doc.SelectNodes("//binding");
            for (int i = 0; i < nodes.Count; ++i) {
                XmlElement element = (XmlElement)nodes[i];
                string key = element.Attributes["key"].Value;
                string name = element.Attributes["name"].Value;
                sb.Append(key + " - " + name + "\n");
            }
            return sb.ToString();
        }

        #endregion
    }
}
