using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Items;

namespace VH.Engine.World.Beings {

    public class ExtendedStatSet: StatSet {

        #region fields

        private Equipment equipment;

        #endregion

        #region constructors

        public ExtendedStatSet(Equipment equipment, string title, params Stat[] stats)
            : base(title, stats) {
                this.equipment = equipment;
        }

        #endregion

        #region properties

        // FIXME
        public virtual Stat this[string id] {
            get {
                Stat stat = (Stat)base[id].Clone();
                foreach (EquipmentSlot slot in equipment) {
                    if (slot.Item is IEquipableItem) {
                        Stat modifier = (slot.Item as IEquipableItem).StatModifier;
                        if (modifier.Id == id) stat.Value += modifier.Value;
                    }
                }
                return stat;
            }
        }

        #endregion

    }
}
