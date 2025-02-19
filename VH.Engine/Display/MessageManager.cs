using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.LineOfSight;
using VH.Engine.Game;
using System.Xml;
using System.Windows.Forms;

namespace VH.Engine.Display {

    public class MessageManager {

        #region constants

        private const int MESSAGES_IN_LOG = 20;
        private const string UNKNOWN_MSG = "?????";

        #endregion

        #region fields

        protected MessageWindow window;
        protected XmlDocument doc = new XmlDocument();
        List<string> messageLog = new List<string>();

        #endregion

        #region constructors

        public MessageManager(MessageWindow window) {
            this.window = window;
            doc.Load(Application.StartupPath + @"/Data/Resources/messages.xml");
        }

        #endregion

        #region properties

        public virtual string this[string key, Person person] {
            get {
                string personAttr;
                if (person == Person.Second) personAttr = "person2";
                else personAttr = "person3";
                string xpath = "/messages/message[@key='" + key + "']/@" + personAttr;
                XmlNodeList nodes = doc.SelectNodes(xpath);
                if (nodes.Count > 0) return ((XmlAttribute)nodes[0]).Value;
                return "[[[" + key + "]]]";
            }
        }

        public virtual string MessageLog {
            get {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < messageLog.Count; ++i) sb.Append(messageLog[i] + "\n");
                return sb.ToString();
            }
        }

        protected XmlDocument Doc { get { return doc; } }  

        #endregion

        #region public methods

        public virtual void ShowMessage(string key, AbstractEntity performer, AbstractEntity target) {
            ShowMessage(key, performer, target, false);
        }

        public virtual void ShowMessage(string key, AbstractEntity performer, AbstractEntity target, bool force) {
            AbstractFieldOfVision fieldOfVision = GameController.Instance.FieldOfVision;
            if (fieldOfVision.IsInFieldOfVision(performer.Position) || force || performer.Person == Person.Second) {
                string targetString = getTargetString(target, performer.Person == Person.Second);
                string message = getMessage(performer.Person, key, performer.Identity,
                    target != null ? targetString : "");
                window.ShowMessage(message);
                logMessage(message);
            }
            if (!fieldOfVision.IsInFieldOfVision(performer.Position)
                    && performer.Person == Person.Third && target != null && target.Person == Person.Second) {
                string message = getMessage(performer.Person, key, Translations.Translator.Instance["something"], target.Accusativ);
                window.ShowMessage(message);
                logMessage(message);
            }
        }

        public virtual void ShowMessage(string key, AbstractEntity performer, bool force) {
            ShowMessage(key, performer, null, force);
        }

        public virtual void ShowMessage(string key, AbstractEntity performer) {
            ShowMessage(key, performer, null);
            GameController.Instance.Console.Refresh();
        }

        public virtual void ShowDirectMessage(string message) {
            window.ShowMessage(message);
            logMessage(message);
        }

        #endregion

        #region private methods

        private string getMessage(Person person, string key, params string[] parameters) {
            string s = this[key, person];
            for (int i = 0; i < parameters.Length; ++i) {
                if (parameters[i] != null) s = replace(s, parameters[i]);
            }
            return s;
        }

        private string replace(string s, string rep) {
            for (int i = 0; i < s.Length; ++i) {
                if (s[i] == '%') {
                    return s.Substring(0, i) + rep + s.Substring(i + 1);
                }
            }
            return s;
        }

        private void logMessage(string message) {
            messageLog.Add(message);
            while (messageLog.Count > MESSAGES_IN_LOG) messageLog.RemoveAt(0);
        }

        private string getTargetString(AbstractEntity target, bool secondPerson) {
            AbstractFieldOfVision fieldOfVision = GameController.Instance.FieldOfVision;
            if (target != null && (fieldOfVision.IsInFieldOfVision(target.Position) || target.Person == Person.Second || secondPerson)) {
                return target.Accusativ;
            } else {
                return Translations.Translator.Instance["something"];
            }
        }

        #endregion

    }
}
