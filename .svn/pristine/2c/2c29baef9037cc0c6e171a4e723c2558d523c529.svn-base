using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using VH.Engine.Random;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using VH.Engine.Tools;

namespace VH.Engine.World {

    /// <summary>
    /// This class can be used to dynamically 
    /// create Creatable instances based on
    /// definitions contained in an xml file.
    /// </summary>
    public class EntityCreator {

        #region constants

        private const string TYPE_NAME = "type-name";
        private const string ASSEMBLY_NAME = "assembly-name";

        #endregion

        #region fields

        XmlDocument doc;
        string filename;

        #endregion

        #region constructors

        /// <summary>
        /// Creates a Creator instance
        /// </summary>
        /// <param name="filename">Full path to an xml file, 
        /// where Creatable definitions are stored</param>
        public EntityCreator(string filename) {
            this.filename = filename;
            doc = new XmlDocument();
            doc.Load(filename); 
        }

        #endregion

        #region public methods

        /// <summary>
        /// Generates a Creatable instance by using a definition 
        /// contained in a node specified by an xpath.
        /// If the specified xpath gives more than one result, 
        /// a Creatable is selected randomly.
        /// </summary>
        /// <param name="xpath">The xpath to the node 
        /// containing the Creatable definition</param>
        /// <returns>The generated Creatable instance</returns>
        public Creatable Generate(string xpath) {
            XmlNodeList nodes = doc.SelectNodes(xpath);
            if (nodes.Count == 0) throw new InvalidOperationException("No matching Creatable definition found in " + filename);
            XmlElement prototype = (XmlElement)nodes[Rng.Random.Next(nodes.Count)];
            string typeName = prototype.Attributes[TYPE_NAME].Value;
            string assemblyName = prototype.Attributes[ASSEMBLY_NAME].Value;
            Assembly assembly = AssemblyCache.GetAssembly(Application.StartupPath + "/" + assemblyName);
            Creatable creatable = (Creatable)assembly.CreateInstance(typeName);
            creatable.Create(prototype);
            return creatable;
        }

        #endregion


    }
}
