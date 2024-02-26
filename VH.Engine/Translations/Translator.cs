using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VH.Engine.Translations {

    /// <summary>
    /// Manages all the user viewable strings in the game.
    /// Expects the translations to be found in Application.StartupPath + @"\data\resources\translations.xml"
    /// </summary>
    public class Translator {

        #region constants

        public static readonly string TRANSLATIONS_FILE_NAME = Application.StartupPath + @"/Data/Resources/translations.xml";

        #endregion

        #region fields

        private XmlNode root;

        #endregion

        #region singleton

        private static Translator instance = new Translator();

        public static Translator Instance { get { return instance; } }

        private Translator() {
            XmlDocument doc = new XmlDocument();
            doc.Load(TRANSLATIONS_FILE_NAME);
            root = doc.DocumentElement;
        }

        #endregion

        #region properties

        /// <summary>
        /// A translation of the given key.
        /// </summary>
        /// <param name="key">The key of the translation</param>
        /// <returns>The translated text.</returns>
        public string this[string key] {
            get { 
                string xpath = "/translations/entry[@key='" + key + "']/@value";
                XmlNodeList nodes = root.SelectNodes(xpath);
                if (nodes.Count == 0) return "[[[" + key + "]]]";
                return ((XmlAttribute)nodes[0]).Value;
            }
        }

        #endregion


    }

}
