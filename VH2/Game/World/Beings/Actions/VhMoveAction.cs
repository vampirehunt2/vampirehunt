using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Display;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {
    public class VhMoveAction : MoveAction {

        #region constants

        private const float CONFUSION_RATE = 0.5f;

        #endregion

        #region constructors
        public VhMoveAction(Being performer, Step step) : base(performer, step) {
        }

        #endregion

        #region public methods

        public override bool Perform() {
            TempSet temps = (Performer as ITempsBeing).Temps;
            if (temps["confused"]) {
                // TODO confusion resistance may also be aquired through items
                if (!temps["confusion-resistance"] || Rng.Random.NextFloat() > CONFUSION_RATE) {
                    step = Step.CreateRandomStep();
                    notify("stagger");
                } else {
                    notify("no-stagger");
                }
            }
            // moving into a closed door results in an attempt to open them
            // if (performer.CanWalkOn(Terrain.Get("open-door").Character) && controller.Map[newPosition] == Terrain.Get("closed-door").Character) {
            //    return new OpenDoorAction(performer, step).Perform();
            //}

            if (!performer.CanWalkOn(Feature) && (Performer as ITempsBeing).Temps["blind"]) { 
                notify("boom");
                return false;
            }
            return base.Perform();
        }

        #endregion

        #region protected methods

        protected override void showItemNames() {
            string itemNames = getItemNames();
            if (itemNames.Length > 0 && performer.Person == Person.Second) {
                if (Performer is ITempsBeing && (Performer as ITempsBeing).Temps["blind"]) notify("trip");
                else controller.MessageManager.ShowDirectMessage(itemNames);
            }
        }

        #endregion

    }
}
