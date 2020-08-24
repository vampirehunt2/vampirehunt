using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Translations;
using VH.Engine.Random;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Professions {

    public class Quack: AbstractProfession {

        public Quack(Being being): base(being) {
            name = Translator.Instance["quack"];
        }
        
        public Quack() { }

        public override void InitBeing() {
            StatSet stats = (being as IStatBeing).Stats;
            stats["In"].Value = Rng.Random.Next(2) + 5;
            stats["Dx"].Value = Rng.Random.Next(2) + 7;
            stats["St"].Value = Rng.Random.Next(2) + 5;
            stats["To"].Value = Rng.Random.Next(2) + 4;
            stats["Pe"].Value = Rng.Random.Next(2) + 5;

            TempSet temps = (being as ITempsBeing).Temps;
            temps["potion-identification"] = true;
            if (Rng.Random.NextFloat() > 0.5f) temps["illness-resistance"] = true;
            else temps["illness-resistance"] = true;

            Equipment equipment = ((IEquipmentBeing)being).Equipment;
            ItemFacade facade = new ItemFacade();
            equipment.Slots[0].Item = facade.CreateItemById("hat");
            equipment.Slots[2].Item = facade.CreateItemById("knife");
            equipment.Slots[3].Item = facade.CreateItemById("clothes");

            being.Color = equipment.Slots[3].Item.Color;
        }
    }
}
