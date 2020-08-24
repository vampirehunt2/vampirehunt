using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.AI;
using VH.Engine.Display;
using System.Reflection;
using System.Windows.Forms;
using VH.Engine.Tools;

namespace VH.Engine.World.Beings.Actions {

    public abstract class AbstractAction {

        #region fields

        protected Being performer;

        #endregion

        #region constructors

        /// <summary>
        /// Always call super(performer) when implementing a constructor in subclass
        /// </summary>
        /// <param name="performer">The being that performs this action</param>
        public AbstractAction(Being performer) {
            this.performer = performer;
        }

        #endregion

        #region properties

        /// <summary>
        /// Returns number of gametime ticks (miliseconds) that a Being with average speed (speed of 1) needs to perform this Action.
        /// </summary>
        public virtual int TimeNeeded {
            get { return 1000; }
        }

        public Being Performer {
            get { return performer; }
            set { performer = value; }
        }

        #endregion

        #region public methods
        
        /// <summary>
        /// Performs this Action
        /// </summary>
        /// <returns>
        /// true if Action execution succeeded. 
        /// Check result to see if gametime has passed.
        /// </returns>
        public abstract bool Perform();

        public static AbstractAction LoadAction(string typeName, string assemblyName) {
            Assembly assembly = AssemblyCache.GetAssembly(Application.StartupPath + "/" + assemblyName);
            return (AbstractAction)assembly.CreateInstance(typeName);
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Selects an object, that is to be the target 
        /// of the current action
        /// </summary>
        /// <param name="objects">Array of possible action targets</param>
        /// <returns>An object, that is to be the target 
        /// of this action</returns>
        protected object selectTarget(object[] objects) {
            return performer.Ai.SelectTarget(objects, this);
        }

        /// <summary>
        /// Notifys the Ai the something has happened 
        /// during performing of the action
        /// </summary>
        /// <param name="messageKey">An identifier of the message 
        /// that is to be sent by this action</param>
        protected void notify(string messageKey) {
            performer.Ai.Notify(messageKey);
        }

        protected void notify(string messageKey, bool force) {
            performer.Ai.Notify(messageKey, force);
        }

        /// <summary>
        /// Notifys the Ai the something has happened 
        /// during performing of this action
        /// </summary>
        /// <param name="messageKey">An identifier of the message 
        /// that is to be sent to this Ai instance</param>
        /// <param name="target">The target of this action</param>
        protected void notify(string messageKey, AbstractEntity target) {
            performer.Ai.Notify(messageKey, target);
        }

        #endregion


    }
}
