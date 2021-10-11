using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;
using VH.Engine.Translations;
using VH.Engine.Random;
using VH.Engine.Game;
using System.Windows.Forms;
using System.Xml;

namespace VH.Game.World.Beings {

    public class VhPc: Pc,
        IWeaponSkillsBeing,
        ITempsBeing,
        IBackPackBeing,
        IEquipmentBeing,
        IStatBeing,
        ISkillsBeing,
        IProfessionBeing
    {

        #region constants

        public const int MAX_STAT_VALUE = 20;

        private const int MAX_ITEMS = 50;
        private const int ARMOR_SLOT_INDEX = 3;

        private const string HEALTH = "health";
        private const string EQUIPMENT = "equipment";
        private const string SKILLS = "skills";
        private const string STATS = "stats";
        private const string BACKPACK = "backpack";
        private const string PROFESSION = "profession";

        #endregion

        #region fields

        int health;

        TempSet temps = new TempSet();

        WeaponSkillSet weaponSkills;

        StackingBackPack backpack = new StackingBackPack(Translator.Instance["backpack"], MAX_ITEMS);
        Equipment equipment;
        StatSet stats;
        SkillSet skills;
        AbstractProfession profession;

        #endregion

        #region constructors

        public VhPc() {
            equipment = new Equipment(Translator.Instance["equipment"],
                new HeadgearSlot(),
                new NeckwearSlot(),
                new WeaponSlot(), 
                new ArmorSlot(),
                new RingSlot(Translator.Instance["right-ring"]),
                new RingSlot(Translator.Instance["left-ring"])
            );

            stats = new StatSet(Translator.Instance["stats"],
                new Stat("St", Translator.Instance["St"], getInitialStatValue()),
                new Stat("To", Translator.Instance["To"], getInitialStatValue()),
                new Stat("Dx", Translator.Instance["Dx"], getInitialStatValue()),
                new Stat("In", Translator.Instance["In"], getInitialStatValue()),
                new Stat("Pe", Translator.Instance["Pe"], getInitialStatValue())
            );

            skills = new SkillSet(Translator.Instance["skills"],
                new VhSkill("swimming", Translator.Instance["swimming"], 0, Stats["Dx"], Stats["In"]),
                new VhSkill("digging", Translator.Instance["digging"], 0, Stats["St"], Stats["In"]),
                new VhSkill("searching", Translator.Instance["searching"], 0, Stats["Pe"], Stats["In"]),
                new VhSkill("identification", Translator.Instance["identification"], 0, Stats["In"], Stats["In"]),
                new VhSkill("magick", Translator.Instance["magick"], 0, Stats["In"], Stats["In"]),
                new VhSkill("reading", Translator.Instance["reading"], 0, Stats["In"], Stats["In"])
            );

            Accusativ = Translator.Instance["you"];
            race = "human";
        }

        #endregion

        #region properties

        public TempSet Temps {
            get { return temps; }
        }

        public WeaponSkillSet WeaponSkills {
            get { return weaponSkills; }
        }

        public SkillSet Skills {
            get { return skills; }
        }

        public StackingBackPack BackPack {
            get { return backpack; }
        }

        public Equipment Equipment {
            get { return equipment; }
        }

        public StatSet Stats {
            get { return stats;  }
        }

        public AbstractProfession Profession {
            get { return profession; }
            set { profession = value; }
        }

        public override int Attack {
            get {
                int attack = Equipment.Attack;
                attack += getStatAttack();
                return attack;
            }
        }

        public override int Defense {
            get {
                int defense = Equipment.Defense;
                defense += getStatDefense();
                return defense;
            }
        }

        public override int DistanceAttack {
            get { throw new NotImplementedException(); }
        }

        public override int Health {
            get { return health; }
            set { health = value; /*TODO*/ }
        }

        public override int MaxHealth {
            get {
                int maxHealth = (int)((getStatRate("To") * 0.8 + getStatRate("St") * 0.2) * 30); 
                if (Temps["poisoned"] ) maxHealth /= 2;
                return maxHealth;
            }
        }

        public int VisionRange {
            get {
                if (temps["blind"]) return 0;
                else return Stats["Pe"].Value / 2; 
            }
        }

        private float healRate {
            get {
                if (Temps["ill"]) return 0;
                else return 0.1f; 
            }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            health = GetIntAttribute(HEALTH);
            equipment = GetElement(EQUIPMENT) as Equipment;

            skills = GetElement(SKILLS) as SkillSet;
            stats = GetElement(STATS) as StatSet;
            foreach (Skill skill in skills) {
                VhSkill vhSkill = skill as VhSkill;
                vhSkill.Stat = Stats[vhSkill.StatId];
                vhSkill.LearningStat = Stats[vhSkill.LearningStatId];
            }

            backpack = GetElement(BACKPACK) as StackingBackPack;
            profession = GetElement(PROFESSION) as AbstractProfession;
            profession.Being = this;
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(HEALTH, health);
            AddElement(EQUIPMENT, equipment);
            AddElement(SKILLS, skills);
            AddElement(STATS, stats);
            AddElement(BACKPACK, backpack);
            AddElement(PROFESSION, profession);
            return element;
        }

        public override void Kill() {
            string endMessage = Translator.Instance["end-game"];
            GameController.Instance.MessageWindow.ShowMessage(endMessage);
            GameController.Instance.Console.ReadLine();
            GameController.Instance.QuitGame = true;
            Application.Exit();
        }

        public override void Move() {
            Item armor = Equipment[ARMOR_SLOT_INDEX].Item;
            if (armor != null) Color = armor.Color;
            else Color = ConsoleColor.DarkYellow;
            if (Health < MaxHealth && Rng.Random.NextFloat() < healRate) Health++;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(Stats.ToString() + "\n");
            sb.Append(Skills.ToString() + "\n");
            sb.Append(Translator.Instance["combat-stats"] + ":\n");
            sb.Append(Translator.Instance["attack"] + ": " + Attack + "\n");
            sb.Append(Translator.Instance["defense"] + ": " + Defense + "\n\n");
            sb.Append(Translator.Instance["health"] + ": " + Health + "/" + MaxHealth);
            return sb.ToString();
        }

        #endregion

        #region private methods

        private static int getInitialStatValue() {
            return 4 + Rng.Random.Next(3);
        }

        private int getStatAttack() {
            return (int)((getStatRate("St") * 0.5 + getStatRate("Dx") * 0.5) * 10);
        }

        private int getStatDefense() {
            return (int)((getStatRate("To") * 0.5 + getStatRate("Pe") * 0.5) * 10);
        }

        private float getStatRate(string statName) {
            return (float)Stats[statName].Value / MAX_STAT_VALUE;
        }

        #endregion

    }
}
