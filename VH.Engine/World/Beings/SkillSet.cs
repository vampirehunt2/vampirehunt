using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VH.Engine.Persistency;
using System.Xml;

namespace VH.Engine.World.Beings {

    public class SkillSet: AbstractPersistent, IEnumerable {

        #region constants

        private const string TITLE = "title";
        private const string SKILLS = "skills";

        #endregion

        #region fields

        private string title;
        private List<Skill> skills = new List<Skill>();

        #endregion

        #region constructors

        public SkillSet(string title, params Skill[] skills) {
            this.title = title;
            foreach (Skill skill in skills) {
                this.skills.Add(skill);
            }
        }

        public SkillSet() { }

        #endregion

        #region properties

        public Skill this[string key] {
            get {
                return (
                    from Skill skill in skills
                    where skill.Id == key
                    select skill
                ).Single();
            }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            title = GetStringAttribute(TITLE);
            skills = GetElements<Skill>(SKILLS);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(TITLE, title);
            AddElements(SKILLS, skills);
            return element;
        }

        public IEnumerator GetEnumerator() {
            foreach (Skill skill in skills) yield return skill;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder(title + ":\n");
            foreach (Skill skill in skills) {
                sb.Append(skill.ToString() + "\n");
            }
            return sb.ToString();
        }

        #endregion

    }
}
