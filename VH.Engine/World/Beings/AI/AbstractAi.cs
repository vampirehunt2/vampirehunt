using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings;
using VH.Engine.Display;
using System.Reflection;
using System.Windows.Forms;
using VH.Engine.Tools;
using VH.Engine.Persistency;
using VH.Engine.Levels;

namespace VH.Engine.World.Beings.AI {

    /// <summary>
    /// Represents intelligence of a Being
    /// </summary>
    public abstract class AbstractAi: AbstractPersistent {

        #region fields

        private Being being;

        #endregion

        #region constructors

        /// <summary>
        /// Creates an Ai instance
        /// </summary>
        /// <param name="being">The Being that has this Ai</param>
        public AbstractAi(Being being) {
            this.being = being;
        }

        public AbstractAi() {
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the Being that has this Ai
        /// </summary>
        public virtual Being Being {
            get { return being; }
            set { being = value; }
        }

        #endregion

        #region public methods

        public virtual bool InteractWithEnvironment(Position position) { return false; }

        /// <summary>
        /// Selects the next AbstractAction 
        /// to be performed by the Being that has this Ai.
        /// </summary>
        /// <returns>The next action to be performed</returns>
        public abstract AbstractAction SelectAction();

        /// <summary>
        /// Selects an object, that is to be the target 
        /// of the most recently selected action
        /// </summary>
        /// <param name="objects">Array of possible action targets</param>
        /// <param name="action">The action that request selecting of the target</param>
        /// <returns>An object, that is to be the target 
        /// of the most recently selected action</returns>
        public abstract object SelectTarget(object[] objects, AbstractAction action);

        /// <summary>
        /// Notifys the Ai the something has happened 
        /// during performing of the action
        /// </summary>
        /// <param name="messageKey">An identifier of the message 
        /// that is to be sent to this Ai instance</param>
        public abstract void Notify(string messageKey);

        public abstract void Notify(string messageKey, bool force);

        /// <summary>
        /// Notifys the Ai the something has happened 
        /// during performing of the action
        /// </summary>
        /// <param name="messageKey">An identifier of the message 
        /// that is to be sent to this Ai instance</param>
        /// <param name="target">The target of the action</param>
        public abstract void Notify(string messageKey, AbstractEntity target);

        public abstract void Stimulate(Stimulus stimulus);

        public static AbstractAi LoadAi(string typeName, string assemblyName) {
            Assembly assembly = AssemblyCache.GetAssembly(Application.StartupPath + "/" + assemblyName);
            return (AbstractAi)assembly.CreateInstance(typeName);
        }

        #endregion

    }
}
