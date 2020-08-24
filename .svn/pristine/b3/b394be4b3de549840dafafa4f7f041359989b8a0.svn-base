using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Items;

namespace VH.Game.World.Beings.Actions {
    public class DigAction : AbstractAction {

        private Position position;
        private Skill diggingSkill = new Skill("digging", "digging", 10, 10);

        public DigAction(Being performer, Position position) : base(performer) {
            this.position = position;
        }

        public override bool Perform() {
            char terrain = GameController.Instance.Level.Map[position];
            if (terrain == Terrain.Get("wall").Character) {
                if (performer is IEquipmentBeing && performer is ISkillsBeing) {
                    Equipment equipment = (performer as IEquipmentBeing).Equipment;
                    EquipmentSlot slot = equipment["weapon-slot"];
                    SkillSet skills = (performer as ISkillsBeing).Skills;
                    Skill diggingSkill = skills["digging"];
                    if (slot != null && slot.Item !=null && slot.Item.HasTag("digging") && diggingSkill != null) {
                        return doDig();
                    }
                } else {
                    return doDig();
                }
            }
            return false;
        }

        private bool doDig() {
            if (diggingSkill.Roll(GameController.Instance.Level.Danger)) {
                GameController.Instance.Level.Map[position] = Terrain.Get("ground").Character;
                notify("digging-succeeded");
            } else {
                notify("digging-failed");
            }
            return true;
        }
    }
}
