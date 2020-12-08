using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.Levels {

    /// <summary>
    /// Represents location of an entity (Item or Being) in Level coorditates.
    /// </summary>
    public class Position: AbstractPersistent {

        #region fields

        private int x;
        private int y;

        #endregion

        #region constructors

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position() { }

        #endregion

        #region properties

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            x = GetIntAttribute("x");
            y = GetIntAttribute("y");
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute("x", x);
            AddAttribute("y", y);
            return element;
        }

        public Position AddStep(Step step) {
            Position position = new Position(x, y);
            position.x += step.X;
            position.y += step.Y;
            return position;
        }

        public int Distance(Position position) {
            return Math.Max(Math.Abs(position.X - X), Math.Abs(position.Y - Y));
        }

        public bool Equals(Position otherPosition) {
            return X == otherPosition.X && Y == otherPosition.Y;
        }

        public bool IsAdjacent(Position position) {
            return Distance(position) == 1;
        }


        public Position Clone() {
            return new Position(x, y);
        }

        #endregion

    }
}
