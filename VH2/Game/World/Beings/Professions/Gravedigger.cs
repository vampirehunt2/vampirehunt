using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Translations;
using VH.Engine.Random;
using VH.Engine.World.Items;
using VH2.Game.World.Items;

namespace VH.Game.World.Beings.Professions {

    public class Gravedigger: AbstractProfession {

        public Gravedigger(Being being): base(being) {
            name = Translator.Instance["gravedigger"];
        }

        public Gravedigger() { }

        public override void InitBeing() {
            StatSet stats = (being as IStatBeing).Stats;
            stats["In"].Value = Rng.Random.Next(2) + 2;
            stats["Dx"].Value = Rng.Random.Next(2) + 2;
            stats["St"].Value = Rng.Random.Next(2) + 7;
            stats["To"].Value = Rng.Random.Next(2) + 7;
            stats["Pe"].Value = Rng.Random.Next(2) + 4;

            TempSet temps = (being as ITempsBeing).Temps;
            //temps["blindness-resistance"] = true;
            temps["confusion-resistance"] = true;
            temps["booze-identification"] = true;

            Equipment equipment = (being as IEquipmentBeing).Equipment;
            ItemFacade facade = new ItemFacade();
            equipment.Slots[0].Item = facade.CreateItemById("felt-beret");
            equipment.Slots[2].Item = facade.CreateItemById("spade");
            equipment.Slots[3].Item = facade.CreateItemById("felt-jacket");

            BackPack backpack = (being as IBackPackBeing).BackPack;
            backpack.Add(facade.CreateItemById("booze"));
            backpack.Add(facade.CreateItemById("booze"));
            backpack.Add(facade.CreateItemById("potion-of-amnesia"));

            foreach (Item item in backpack.Items) {
                if (item is MagicalItem) {
                    (item as MagicalItem).Identify();
                }
            }

            being.Color = equipment.Slots[3].Item.Color;

        }
    }
}
