using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VH.Engine.Persistency;
using System.Xml;

namespace VH.Engine.World.Beings {

    /// <summary>
    /// Describes temporary conditions that affect a particular Being, 
    /// such as blindness, paralysis etc. 
    /// which are supposed to be stored directly as boolean values rather than computed.
    /// </summary>
    public class TempSet: AbstractPersistent {

        #region fields

        private Hashtable temps = new Hashtable();

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets a value indicating whether an entity attributed with this TempSet is
        /// currently affected by a temporary condition identified by the key.
        /// </summary>
        /// <param name="key">The key of the condition</param>
        /// <returns>True if the entity is affected</returns>
        public bool this[string key] {
            get {
                if (temps[key] == null) temps.Add(key, false);
                return (bool)temps[key];
            }
            set {
                if (temps[key] == null) temps.Add(key, value);
                else temps[key] = value;
            }
        }

        #endregion

        #region public methods

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element =  base.ToXml(name, doc);
            foreach (string key in temps.Keys) {
                AddAttribute(key, temps[key].ToString());
            }
            return element;
        }

        #endregion

    }
}
