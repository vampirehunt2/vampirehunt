using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Display;
using VH.Engine.World.Beings.AI;
using System.Globalization;
using VH.Engine.Tools;
using VH.Engine.Persistency;
using VH.Engine.Random;
using VH.Engine.Levels;
using VH.Engine.Game;

namespace VH.Engine.World.Beings {

    /// <summary>
    /// Represents a Being. Any lifeform in the game is derived from this class.
    /// </summary>
    public abstract class Being: AbstractEntity {

        #region constants

        private const string WALKABLE_TERRAIN = "walkable-terrain";
        private const string SPEED = "speed";
        private const string RACE = "race";
        private const string AI = "ai";

        #endregion

        #region fields

        protected string race;
        private TempSet temps = new TempSet();

        // Let's have a default value for each of the following fields. 
        // This way we will not have to define their value in subclasses if we are OK with just using the default one
        // (or the one from superclass)
        private string walkableTerrain = "., /~_><";
        private float speed = 1;
        private AbstractAi ai;
        private string killReason = "";

        #endregion

        #region events

        public event EventHandler Killed;

        #endregion

        #region properties

        public string WalkableTerrain {
            get { return walkableTerrain; }
        }

        /// <summary>
        /// Gets the speed of this Being.
        /// </summary>
        public float Speed {
            get { return speed; }
        }

        public virtual string Race {
            get { return race; }
        }

        public AbstractAi Ai {
            get { return ai; }
            set { ai = value; }
        }

        public string KillReason {
            get { return killReason; }
        }

        public abstract int Health { get; set; }

        public abstract int MaxHealth { get; }

        public abstract int Attack { get; }

        public abstract int Defense { get; }

        public abstract int DistanceAttack { get; }


        #endregion

        #region public methods

        /// <summary>
        /// Checks the ability of this Being to walk on a specified terrain feature.
        /// </summary>
        /// <param name="c">Represents the terrain feature. 
        /// Always use a value returned by ViewPort.GetDisplayCharacter </param>
        /// <returns>true if this Being can walk through the specified terrain feature</returns>
        public virtual bool CanWalkOn(char c) {
            return walkableTerrain.IndexOf(c) > -1;
        }

        /// <summary>
        /// Creates this Being from a prototype.
        /// Creating means setting all the field values based on values stored in the xml.
        /// </summary>
        /// <param name="prototype"></param>
        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            race = prototype.Attributes[RACE].Value;
            if (prototype.Attributes[WALKABLE_TERRAIN] != null) walkableTerrain = prototype.Attributes[WALKABLE_TERRAIN].Value;
            if (prototype.Attributes[SPEED] != null) speed = float.Parse(prototype.Attributes[SPEED].Value, NumberFormatter.NumberFormat);
            if (prototype.Attributes[WALKABLE_TERRAIN] != null) walkableTerrain = prototype.Attributes[WALKABLE_TERRAIN].Value;
        }

        public virtual void Kill() {
            if (Killed != null) Killed(this, new EventArgs());
        }

        public virtual void DecreaseHealth(int damage, string reason) {
            Health -= damage;
            killReason = reason;
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(WALKABLE_TERRAIN, walkableTerrain);
            AddAttribute(RACE, race);
            AddAttribute(SPEED, (int)(speed * 1000));
            AddElement(AI, ai);
            return element;
        }

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            walkableTerrain = GetStringAttribute(WALKABLE_TERRAIN);
            race = GetStringAttribute(RACE);
            speed = GetIntAttribute(SPEED) / 1000f;
            ai = GetElement(AI) as AbstractAi;
            ai.Being = this;
        }

        public bool ChoosePosition() {
            int c = 0;
            Level level = GameController.Instance.Level;
            do {
                Position.X = Rng.Random.Next(level.LevelWidth);
                Position.Y = Rng.Random.Next(level.LevelHeight);
                ++c;
            } while (!isValidPosition() && c <= 10);
            if (c > 10) {
                List<Position> positions = new List<Position>();
                for (int x = 0; x < level.LevelWidth; ++x) {
                    for (int y = 0; y < level.LevelHeight; ++y) {
                        if (CanWalkOn(level.Map[x, y])) positions.Add(new Position(x, y));
                    }
                }
                if (positions.Count == 0) return false;
                int i = Rng.Random.Next(positions.Count);
                this.Position = positions[i];
            }
            return true;
        }

        #endregion

        #region private methods

        private bool isValidPosition() {
            Level level = GameController.Instance.Level;
            char terrain = GameController.Instance.ViewPort.GetDisplayCharacter(level.Map[Position]);
            return WalkableTerrain.Contains(terrain);
        }

        #endregion

    }
}
