using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Translations;
using System.Collections;
using VH.Engine.Persistency;
using System.Xml;

namespace VH.Engine.World.Items {

    /// <summary>
    /// Represent set of Items that a Being has equipped
    /// </summary>
    public class Equipment: AbstractPersistent, IEnumerable {

        #region fields 

        private List<EquipmentSlot> slots = new List<EquipmentSlot>();
        private string title = "";

        #endregion

        #region constructors

        /// <summary>
        /// Creates an Equipment instance
        /// </summary>
        /// <param name="title">The string to be the header 
        /// of the ToString method result</param>
        /// <param name="slots">EquipmentSlots that 
        /// constitute this Equipment</param>
        public Equipment(string title, params EquipmentSlot[] slots) : this(slots) {
            this.title = title;
        }

        /// <summary>
        /// Creates an Equipment instance
        /// </summary>
        /// <param name="slots">EquipmentSlots that 
        /// constitute this Equipment</param>
        public Equipment(params EquipmentSlot[] slots) {
            for (int i = 0; i < slots.Length; ++i) {
                this.slots.Add(slots[i]);
            }
        }

        public Equipment() { }

        #endregion

        #region properties

        /// <summary>
        /// A list of all EquipmentSlots that constitute this Equipment
        /// </summary>
        public List<EquipmentSlot> Slots {
            get { return slots; }
        }

        public EquipmentSlot this[int i] {
            get { return slots[i]; } 
        }

        public EquipmentSlot this[string id] {
            get {
                foreach (EquipmentSlot slot in slots) {
                    if (slot.Id == id) return slot;
                }
                return null; 
            }
        }

        /// <summary>
        /// The combined attack bonus of all equipped Items
        /// </summary>
        public int Attack {
            get {
                int attack = 0;
                foreach (EquipmentSlot slot in Slots) {
                    if (slot.Item != null && slot.Item is IEquipableItem) {
                        attack += ((IEquipableItem)slot.Item).Attack;
                    }
                }
                return attack;
            }
        }

        /// <summary>
        /// The combined defense bonus of all equipped Items
        /// </summary>
        public int Defense {
            get {
                int defense = 0;
                foreach (EquipmentSlot slot in Slots) {
                    if (slot.Item != null && slot.Item is IEquipableItem) {
                        defense += ((IEquipableItem)slot.Item).Defense;
                    }
                }
                return defense;
            }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            slots = GetElements<EquipmentSlot>("slots");
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddElements("slots", slots);
            return element;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder(title + ":\n");
            for (int i = 0; i < slots.Count; ++i) {
                sb.Append(slots[i] + "\n");
            }
            return sb.ToString();
        }


        public IEnumerator GetEnumerator() {
            foreach (EquipmentSlot slot in slots) yield return slot;
        }

        #endregion


    }
}
