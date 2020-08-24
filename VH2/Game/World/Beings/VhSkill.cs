using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Game;
using VH.Engine.Translations;
using VH.Engine.Random;
using System.Xml;

namespace VH.Game.World.Beings {

    public class VhSkill: Skill {

        #region constants

        private const string STAT = "stat";
        private const string LEARNING_STAT = "learning-stat";

        #endregion

        #region fields

        private string statId;
        private string learningStatId;

        private Stat stat;
        private Stat learningStat;

        #endregion

        #region constructors

        public VhSkill() { }

        public VhSkill(string key, string name, int skillValue, Stat stat, Stat learningStat)
            : base(key, name, skillValue, 0) {
                this.stat = stat;
                this.learningStat = learningStat;
        }

        #endregion

        #region properties

        public string StatId {
            get {
                if (stat != null) return stat.Id;
                return statId;
            }
        }

        public string LearningStatId {
            get {
                if (learningStat != null) return learningStat.Id;
                return learningStatId;
            }
        }

        public Stat Stat {
            get { return stat; }
            set { stat = value; }
        }

        public Stat LearningStat {
            get { return learningStat; }
            set { learningStat = value; }
        }

        public override int MaxValue {
            get {
                return (int)(((float)Stat.Value / VhPc.MAX_STAT_VALUE) * MAX_SKILL_VALUE);
            }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            statId = GetStringAttribute(STAT);
            learningStatId = GetStringAttribute(LEARNING_STAT);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element =  base.ToXml(name, doc);
            AddAttribute(STAT, StatId);
            AddAttribute(LEARNING_STAT, LearningStatId);
            return element;
        }

        public override string ToString() {
            return base.ToString() + " (" + Stat.Name.Substring(0, 3) + ")";
        }

        #endregion

        #region protected methods

        protected override void train() {
            base.train();
            int skillUpgradeLevel = (Value / 5) * (25 - learningStat.Value);
            if (trainingPoints >= skillUpgradeLevel && Value < MaxValue) {
                Value++;
                trainingPoints = 0;
                string message = Name + Translator.Instance["skill-upgrade"] + Value;
                GameController.Instance.MessageManager.ShowDirectMessage(message);
            }
        }

        #endregion

    }
}
