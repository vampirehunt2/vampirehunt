using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Random;
using VH.Engine.Translations;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;
using VH2.Game.World.Items;

namespace VH.Game.World.Beings.Professions {
    public class Hunter : AbstractProfession {

        private const int MIN_STARTING_MISSLES = 10;
        private const int MAX_STARTING_MISSLES = 15;

        public Hunter(Being being) : base(being) {
            name = Translator.Instance["hunter"];
        }

        public Hunter() { }

        public override void InitBeing() {
            StatSet stats = (being as IStatBeing).Stats;
            stats["In"].Value = Rng.Random.Next(2) + 5;
            stats["Dx"].Value = Rng.Random.Next(2) + 7;
            stats["St"].Value = Rng.Random.Next(2) + 5;
            stats["To"].Value = Rng.Random.Next(2) + 4;
            stats["Pe"].Value = Rng.Random.Next(2) + 4;

            TempSet temps = (being as ITempsBeing).Temps;

            Equipment equipment = (being as IEquipmentBeing).Equipment;
            ItemFacade facade = new ItemFacade();
            equipment.Slots[1].Item = facade.CreateItemById("garlic");
            equipment.Slots[2].Item = facade.CreateItemById("xbow");
            equipment.Slots[3].Item = facade.CreateItemById("leather-jacket");

            BackPack backpack = (being as IBackPackBeing).BackPack;
            backpack.Add(facade.CreateItemById("pole"));
            backpack.Add(facade.CreateRandomItem("[@type-name='VH.Game.World.Items.Potions.Potion']"));

            equipment.Slots[4].Item = facade.CreateItemById("quarrel");

            foreach (Item item in backpack.Items) {
                if (item is MagicalItem) (item as MagicalItem).Identify();
            }
            being.Color = equipment.Slots[3].Item.Color;
        }
    
    }
}
