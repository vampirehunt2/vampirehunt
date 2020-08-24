using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;

namespace VH.Game.World.Beings.Ai {
    public class GnomeAi: HumanAi {

        public GnomeAi(): base() { }
        public GnomeAi(Being being): base(being) { }

        public override bool InteractWithEnvironment(Position position) {
            if (new DigAction(Being, position).Perform()) return true;
            return base.InteractWithEnvironment(position);
        }

        public override AbstractAction SelectAction() {
            AbstractAction action = base.SelectAction();
            if (state == State.neutral && (action is MoveAction || action is WaitAction)) {
                Position wallPosition = getAdjacentWall();
                if (wallPosition != null) {
                    return new DigAction(Being, wallPosition);
                }
            }
            return action;
        }

        protected override bool isSuitableOponent(Being oponent) {
            return
                Math.Max(
                    Math.Abs(oponent.Position.X - Being.Position.X),
                    Math.Abs(oponent.Position.Y - Being.Position.Y)
                ) < 2 && (oponent.Race != Being.Race);
        }

        private Position getAdjacentWall() {
            for (int x = -1; x <= 1; ++x) {
                for (int y = -1; y <= 1; ++y) {
                    Step step = new Step(x, y);
                    Position position = Being.Position.AddStep(step);
                    if (GameController.Instance.Map[position] == Terrain.Get("wall").Character) {
                        return position;
                    }
                }
            }
            return null;
        }
    }
}
