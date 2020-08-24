using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Display;
using System.Xml;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Game;
using VH.Engine.World.Items;
using VH.Engine.World.Beings.AI;
using VH.Engine.Levels;

namespace VH.Engine.World.Beings {

    public class Monster: 
        Being {

        #region constants

        private const string AI_TYPE_NAME = "ai-type-name";
        private const string AI_ASSEMBLY_NAME = "ai-assembly-name";
        private const string ATTACK = "attack";
        private const string DEFENSE = "defense";
        private const string HEALTH = "health";
        private const string MAX_HEALTH = "max-health";
        private const string DISTANCE_ATTACK = "distance-attack";
        private const string TICKS_AVAILABLE = "ticks-available";
        private const string CAN_OPEN_DOOR = "can-open-door";



        #endregion

        #region fields

        private int health;
        private int maxHealth;
        private int attack;
        private int defense;
        private int distanceAttack;
        private bool canOpenDoor = false;

        private int ticksAvailable;

        #endregion 

        #region properties

        public override Person Person {
            get { return Person.Third; }
        }

        public override string Identity {
            get { return Name; }
        }

        public override int Health {
            get { return health; }
            set { health = value; }
        }

        public override int MaxHealth {
            get { return maxHealth; }
        }

        public override int Attack {
            get { return attack; }
        }

        public override int Defense {
            get { return defense; }
        }

        public override int DistanceAttack {
            get { return distanceAttack; }
        }

        public bool CanOpenDoor {
            get { return canOpenDoor; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            health = GetIntAttribute(HEALTH);
            maxHealth = GetIntAttribute(MAX_HEALTH);
            attack = GetIntAttribute(ATTACK);
            defense = GetIntAttribute(DEFENSE);
            distanceAttack = GetIntAttribute(DISTANCE_ATTACK);
            canOpenDoor = GetBoolAttribute(CAN_OPEN_DOOR);
            ticksAvailable = GetIntAttribute(TICKS_AVAILABLE);
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(HEALTH, health);
            AddAttribute(MAX_HEALTH, maxHealth);
            AddAttribute(ATTACK, attack);
            AddAttribute(DEFENSE, defense);
            AddAttribute(DISTANCE_ATTACK, distanceAttack);
            AddAttribute(CAN_OPEN_DOOR, canOpenDoor);
            AddAttribute(TICKS_AVAILABLE, ticksAvailable);
            return element;
        }

        public virtual void Move(int gametimeTicks) {
            AbstractAction action;
            ticksAvailable += gametimeTicks;
            do {
                action = Ai.SelectAction();
                int ticksNeeded = (int)(action.TimeNeeded / Speed);
                if (ticksNeeded <= ticksAvailable) action.Perform();
                else break;
                // disregard whether the action actually succeeded.
                // it's better to consume ticks for a failed/invalid action
                // than to risk an infinite action loop.
                ticksAvailable -= ticksNeeded;
            } while (ticksAvailable > 0 && !GameController.Instance.QuitGame);
        }

        public override void Create(XmlElement prototype) {
            base.Create(prototype);
            //
            maxHealth = health = int.Parse(prototype.Attributes[HEALTH].Value);
            attack = int.Parse(prototype.Attributes[ATTACK].Value);
            defense = int.Parse(prototype.Attributes[DEFENSE].Value);
            if (prototype.Attributes[CAN_OPEN_DOOR] != null) {
                canOpenDoor = bool.Parse(prototype.Attributes[CAN_OPEN_DOOR].Value);
            }
            //
            string aiTypeName = prototype.Attributes[AI_TYPE_NAME].Value;
            string aiAssemblyName = prototype.Attributes[AI_ASSEMBLY_NAME].Value;
            Ai = AbstractAi.LoadAi(aiTypeName, aiAssemblyName);
            Ai.Being = this;
        }

        public override void Kill() {
            base.Kill();
            GameController.Instance.Level.Monsters.Remove(this);
        }

        public override bool CanWalkOn(char c) {
            return base.CanWalkOn(c) || canOpenDoor && c == Terrain.Get("closed-door").Character;
        }

        #endregion
    }

}
