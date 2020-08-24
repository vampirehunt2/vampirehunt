using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;
using VH.Engine.Random;

namespace VH.Engine.World.Beings {

    public class Skill: AbstractPersistent {

        #region constants

        protected const int MAX_SKILL_VALUE = 100;

        private const string ID = "id";
        private const string NAME = "name";
        private const string SKILL_VALUE = "skill-value";
        private const string MAX_VALUE = "max-value";
        private const string TRAINING_POINTS = "training-points";

        #endregion

        #region fields

        private string id;
        private string name;
        private int skillValue;
        private int maxValue;

        protected int trainingPoints = 0;

        #endregion

        #region constructors

        public Skill() { }

        public Skill(string id, string name, int maxValue) : this(id, name, 0, maxValue) { }

        public Skill(string id, string name, int skillValue, int maxValue) {
            this.id = id;
            this.name = name;
            this.skillValue = skillValue;
            this.maxValue = maxValue;
        }

        #endregion

        #region properties

        public string Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        public int Value {
            get { return skillValue; }
            set {
                if (value < 0) skillValue = 0;
                else if (value > MaxValue) skillValue = MaxValue;
                else skillValue = value; 
            }
        }

        public virtual int MaxValue {
            get { return maxValue; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            id = GetStringAttribute(ID);
            name = GetStringAttribute(NAME);
            skillValue = GetIntAttribute(SKILL_VALUE);
            maxValue = GetIntAttribute(MAX_VALUE);
            trainingPoints = GetIntAttribute(TRAINING_POINTS);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(ID, id);
            AddAttribute(NAME, this.name);
            AddAttribute(SKILL_VALUE, skillValue);
            AddAttribute(MAX_VALUE, maxValue);
            AddAttribute(TRAINING_POINTS, trainingPoints);
            return element;
        }

        public bool Roll(int difficulty) {
            float valueToMatch = (float)(skillValue - difficulty) / MAX_SKILL_VALUE;
            bool success = Rng.Random.NextFloat() <= valueToMatch ;
            if (success) train();
            return success;
        }

        public bool Roll() {
            return Roll(0);
        }

        public override string ToString() {
            return Name + ": " + Value + "/" + MaxValue;
        }

        #endregion

        #region protected methods

        protected virtual void train() {
            trainingPoints++;
        }

        #endregion

    }
}
