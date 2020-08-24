using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Engine.Display;
using VH.Engine.Levels;
using VH.Engine.Game;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.AI;
using VH.Engine.World.Items;
using VH.Engine.Pathfinding;
using System.Collections;

namespace VH.Engine.World.Beings.AI {

    public abstract class BaseAi: AbstractAi {

        #region fields

        MessageWindow messageWindow = GameController.Instance.MessageWindow;
        MessageManager messageManager = GameController.Instance.MessageManager;
        protected Pathfinder pathfinder = new SimplePathfinder();

        #endregion

        #region constructors

        public BaseAi() : base() { }

        public BaseAi(Being being) : base(being) { }

        #endregion

        #region public

        public override object SelectTarget(object[] objects, Engine.World.Beings.Actions.AbstractAction action) {
            return objects[0];
        }

        public override void Notify(string messageKey) {
            messageManager.ShowMessage(messageKey, Being);
        }

        public override void Notify(string messageKey, bool force) {
            messageManager.ShowMessage(messageKey, Being, force);
        }

        public override void Notify(string messageKey, AbstractEntity target) {
            messageManager.ShowMessage(messageKey, Being, (AbstractEntity)target);
        }

        public override void Stimulate(Stimulus stimulus) {
            // do nothing
        }

        #endregion

        #region protected methods

        protected Being getOponent() {
           foreach (Being oponent in GameController.Instance.Beings) {
               if (isSuitableOponent(oponent)) return oponent;
           }
           return null;
        }

        protected virtual bool isSuitableOponent(Being oponent) {
            return 
                Math.Max(
                    Math.Abs(oponent.Position.X - Being.Position.X), 
                    Math.Abs(oponent.Position.Y - Being.Position.Y)
                ) < 10 && (oponent.Race != Being.Race);
        }

        protected bool isAdjacentTo(Being oponent) {
            return Math.Max(Math.Abs(Being.Position.X - oponent.Position.X),
                Math.Abs(Being.Position.Y - oponent.Position.Y)) == 1;
        }

        protected float getDistance(Position source, Position target) {
            int x2 = source.X - target.X;
            x2 *= x2;
            int y2 = source.Y - target.Y;
            y2 *= y2;
            return (float)Math.Sqrt(x2 + y2);
        }

        protected Step getStepTowards(Hashtable steps) {
            Step result = null;
            float minDistance = float.MaxValue;
            foreach (Step step in steps.Keys) {
                float currentDistance = (float)steps[step];
                if (currentDistance < minDistance) {
                    minDistance = currentDistance;
                    result = step;
                }
            }
            if (result != null) return result;
            else return Step.CreateRandomStep();
        }

        protected Step getStepAwayFrom(Hashtable steps) {
            Step result = null;
            float maxDistance = 0;
            foreach (Step step in steps.Keys) {
                float currentDistance = (float)steps[step];
                if (currentDistance > maxDistance) {
                    maxDistance = currentDistance;
                    result = step;
                }
            }
            if (result != null) return result;
            else return Step.CreateRandomStep();
        }

        protected Hashtable getPossibleSteps(Being being, Position target) {
            Hashtable steps = new Hashtable();
            for (int x = -1; x <= 1; ++x) {
                for (int y = -1; y <= 1; ++y) {
                    if (x != 0 || y != 0) {
                        Step step = new Step(x, y);
                        Position newPosition = being.Position.AddStep(step);
                        if (GameController.Instance.IsFreeSpace(newPosition, being)) {
                            float distance = getDistance(newPosition, target);
                            steps.Add(step, distance);
                        }
                    }
                }
            }
            return steps;
        }

        #endregion


    }
}
