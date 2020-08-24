using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;
using VH.Engine.World.Beings;
using VH2.Game.World.Items;
using VH.Game.World.Items.Potions;
using VH.Game.World.Items.Scrolls;

namespace VH.Game.World.Beings.Actions {

    public class ExamineItemAction: Engine.World.Beings.Actions.AbstractAction {

        #region fields

        private Item item = null;

        #endregion

        #region constructors

        public ExamineItemAction(Being performer) : base(performer) { }

        public ExamineItemAction(Being performer, Item item)
            : base(performer) {
                this.item = item;
        }

        #endregion

        #region properties

        public Item Item {
            get { return item; }
            set { item = value; }
        }

        #endregion

        #region public methods

        public override bool Perform() {
            if (Performer is ISkillsBeing && Item is MagicalItem) {
                if ((Performer as ITempsBeing).Temps["blind"]) {
                    notify("cant-examine");
                    return false;
                }
                Skill identification = (Performer as ISkillsBeing).Skills["identification"];
                bool unidentified = (Item as MagicalItem).HiddenName != item.Name;

                // automatic, class-dependant indentification
                if (unidentified) {
                    TempSet temps = (Performer as ITempsBeing).Temps;
                    if (Item is Potion && temps["potion-identification"]
                        || Item is Scroll && temps["scroll-identification"]
                        || Item.Id == "booze" && temps["booze-identification"]
                    ) {
                        (Item as MagicalItem).Identify();
                        notify("instant-recognition", item);
                        return true;
                    }
                }


                // TODO: store separate identify value for each being.
                // not relevant yet, as beings other than the PC are not able to use identification, but bogus anyway.
                if (unidentified) {
                    if (identification.Value > (Item as MagicalItem).IdentifyValue) {
                        if (identification.Roll(Item.Danger)) {
                            (Item as MagicalItem).Identify();
                            notify("identify", item);
                        } else {
                            notify("no-identify");
                        }
                    (Item as MagicalItem).IdentifyValue = identification.Value;
                    } else {
                        notify("next-no-identify", item);
                    }
                }
            }
            return true;
        }

        #endregion

    }
}
