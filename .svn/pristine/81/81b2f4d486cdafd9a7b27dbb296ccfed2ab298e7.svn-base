using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Display;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.World.Items {

    /// <summary>
    /// Represents an item.
    /// </summary>
    public class Item: AbstractEntity, IPersistent  {

        #region constants

        #endregion

        #region fields


        #endregion

        #region properties

        public override Person Person {
            get { return Person.Third; }
        }

        /// <summary>
        /// The name that this Being is referred by.
        /// </summary>
        public override string Identity {
            get { return Name; } 
        }

        #endregion

        #region public methods

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
        }

        public void FromXml(XmlDocument doc, XmlElement element) {
            throw new NotImplementedException();
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            return element;
        }

        #endregion

    }
}
