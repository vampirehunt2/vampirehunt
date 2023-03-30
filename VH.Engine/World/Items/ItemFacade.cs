using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.World.Items {

    public class ItemFacade: EntityCreator {

        #region constructors

        public ItemFacade() : base("Data/Resources/items.xml") { }

        #endregion

        #region public methods

        public Item CreateRandomItem() {
            return (Item)Generate("//item");
        }

        public Item CreateRandomItem(string xpath) {
            return (Item)Generate("//item" + xpath);
        }

        public Item CreateItemByDanger(int danger) {
            return (Item)Generate("//item[@danger<=" + danger + "]");
        }

        public Item CreateWeapon() {
            return (Item)Generate("//item[@character='(']");
        }

        public Item CreateItemByName(string name) {
            return (Item)Generate("//item[@name='" + name + "']");
        }

        public Item CreateItemById(string id) {
            return (Item)Generate("//item[@id='" + id + "']");
        }

        #endregion
    }
}
