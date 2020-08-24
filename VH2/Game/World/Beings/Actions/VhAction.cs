using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.Translations;

namespace VH.Game.World.Beings.Actions {

    public class VhAction: AbstractAction {

        private const float BLINDNESS_RECOVERY_RATE = 0.05f;
        private const float CONFUSION_RECOVERY_RATE = 0.03f;
        private const float ILLNESS_RECOVERY_RATE = 0.05f;
        private const float POISONING_RECOVERY_RATE = 0.02f;

        public VhAction(Being performer): base(performer) { }

        public override bool Perform() {
            if (performer.MaxHealth < performer.Health) performer.Health = performer.MaxHealth;
            // check for water
            if (performer is ISkillsBeing && GameController.Instance.Map[performer.Position] == Terrain.Get("water").Character) {
                if (((ISkillsBeing)performer).Skills["swimming"].Roll()) {
                    notify("swim");
                } else {
                    notify("drown");
                    performer.DecreaseHealth(Rng.Random.Next(3), Translator.Instance["drowning"]);
                    if (performer.Health <= 0) {
                        notify("killed");
                        performer.Kill();
                    }
                }
            }

            // check for hidden stuff
            if (performer is ISkillsBeing) {
                for (int x = -1; x <= 1; ++x) {
                    for (int y = -1; y <= 1; ++y) {
                        Step step = new Step(x, y);
                        Position position = performer.Position.AddStep(step);
                        if (GameController.Instance.Map[position] == Terrain.Get("hidden-door").Character) {
                            if (((ISkillsBeing)performer).Skills["searching"].Roll()) {
                                GameController.Instance.Map[position] = Terrain.Get("closed-door").Character;
                                notify("spot");
                            }
                        }
                    }
                }
            }

            // check for temps
            TempSet temps = (performer as ITempsBeing).Temps;
            if (temps["blind"] && Rng.Random.NextFloat() < BLINDNESS_RECOVERY_RATE) {
                temps["blind"] = false;
                notify("unblinded", true);
            }
            if (temps["confused"] && Rng.Random.NextFloat() < CONFUSION_RECOVERY_RATE) {
                temps["confused"] = false;
                notify("unconfused");
            }
            float illnessRecoveryRate = ILLNESS_RECOVERY_RATE;
            if (temps["illness-resistance"]) illnessRecoveryRate *= 2;
            if (temps["ill"] && Rng.Random.NextFloat() < illnessRecoveryRate) {
                temps["ill"] = false;
                notify("cured");
            }
            float poisoningRecoveryRate = POISONING_RECOVERY_RATE;
            if (temps["poison-resistance"]) poisoningRecoveryRate *= 2;
            if (temps["poisoned"] && Rng.Random.NextFloat() < poisoningRecoveryRate) {
                temps["poisoned"] = false;
                notify("cured");
            }

            //
            return true;
        }

    }
}
