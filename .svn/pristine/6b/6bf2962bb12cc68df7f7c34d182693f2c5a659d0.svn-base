using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Translations;
using VH.Engine.Random;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Professions {

    public class Custodian: AbstractProfession {

        public Custodian(Being being)
            : base(being) {
            name = Translator.Instance["custodian"];
        }

        public Custodian() { }

        public override void InitBeing() {
            StatSet stats = (being as IStatBeing).Stats;
            stats["In"].Value = Rng.Random.Next(2) + 9;
            stats["Dx"].Value = Rng.Random.Next(2) + 4;
            stats["St"].Value = Rng.Random.Next(2) + 2;
            stats["To"].Value = Rng.Random.Next(2) + 3;
            stats["Pe"].Value = Rng.Random.Next(2) + 8;

            TempSet temps = (being as ITempsBeing).Temps;
            temps["ring-identification"] = true;
            temps["neckwear-identification"] = true;
            temps["scroll-identification"] = true;

            Equipment equipment = ((IEquipmentBeing)being).Equipment;
            ItemFacade facade = new ItemFacade();
            equipment.Slots[0].Item = facade.CreateItemById("hat");
            equipment.Slots[2].Item = facade.CreateItemById("kluka");
            equipment.Slots[3].Item = facade.CreateItemById("clothes");

            being.Color = equipment.Slots[3].Item.Color;
        }
    }
}
