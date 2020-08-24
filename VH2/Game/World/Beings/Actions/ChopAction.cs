using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Actions {
    public class ChopAction: Engine.World.Beings.Actions.AbstractAction {

        private Position position;

        public ChopAction(Being performer, Position position) : base(performer) {
            this.position = position;
        }

        public object ItemGenerator { get; private set; }

        public override bool Perform() {
            if (performer is IEquipmentBeing && performer is IStatBeing) {
                Equipment equipment = (performer as IEquipmentBeing).Equipment;
                EquipmentSlot slot = equipment["weapon-slot"];
                if (slot != null && slot.Item != null && slot.Item.HasTag("chopping") ) {
                    char terrain = GameController.Instance.Level.Map[position];
                    if (terrain == Terrain.Get("tree").Character) {
                        if (stCheck()) {
                            GameController.Instance.Level.Map[position] = Terrain.Get("grass").Character;
                            generateSticks();
                            notify("choping-succeeded");
                        } else {
                            notify("choping-failed");
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private void generateSticks() {
            int x = Rng.Random.Next(4);
            for (int i = 0; i < x; ++i) {
                Item stick = GameController.Instance.ItemGenerator.ItemFacade.CreateItemById("stick");
                stick.Position = position;
                GameController.Instance.Level.Items.Add(stick);
            }
        }

        private bool stCheck() {
            if (Performer is IStatBeing) {
                IStatBeing statBeing = Performer as IStatBeing;
                Stat st = statBeing.Stats["St"];
                if (st != null) {
                    int stValue = st.Value / 2;
                    if (Rng.Random.Next(VhPc.MAX_STAT_VALUE) < stValue) return true;
                }
            }
            return false;
        }
    }
}
