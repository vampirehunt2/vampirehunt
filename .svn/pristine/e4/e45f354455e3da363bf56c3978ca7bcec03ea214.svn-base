using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings.Actions;
using VH.Game.World.Beings.Actions;
using VH.Engine.World.Beings;
using VH.Engine.Levels;
using VH.Engine.Game;
using VH.Engine.World.Items;
using VH.Engine.World.Beings.AI;
using VH.Engine.World.Beings.AI.Stimuli;

namespace VH.Game.World.Beings.Ai {

    public class HumanAi: BaseAi {

        #region types

        protected enum State {
            neutral,
            angry,
            afraid
        }

        #endregion

        #region constants

        private const float LOW_HEALTH = 0.4f;
        private const float HIGH_HEALTH = 0.8f;

        #endregion

        #region fields

        protected State state = State.neutral;
        private Being oponent = null;
        private List<Being> hostileBeings = new List<Being>();

        #endregion

        #region constructors

        public HumanAi(Being being)
            : base(being) {}

        public HumanAi() : base() { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            performStateTransition();
            switch (state) {
                case State.neutral: return new NeutralBehavior(Being).SelectAction();
                case State.angry: return new ChaseBehavior(Being, oponent).SelectAction();
                case State.afraid: return new FleeBehavior(Being, oponent).SelectAction();
                default: return new WaitAction(Being);
            }
        }

        public override void Stimulate(Stimulus stimulus) {
            if (stimulus.Id == AttackStimulus.ID) reactToAttack((AttackStimulus)stimulus);
        }

        #endregion

        #region protected methods

        protected override bool isSuitableOponent(Being oponent) {
            return
                Math.Max(
                    Math.Abs(oponent.Position.X - Being.Position.X),
                    Math.Abs(oponent.Position.Y - Being.Position.Y)
                ) < 10 && (oponent.Race != Being.Race || hostileBeings.Contains(oponent));
        }

        #endregion

        #region private methods

        private void performStateTransition() {
            // prepare data to base decisions on
            oponent = getOponent();
            if (oponent == null) {
                state = State.neutral;
                return;
            }

            // state transitions
            switch (state) {
                case State.neutral:
                    if (oponent != null) {
                        state = State.angry;
                        Notify("become-hostile");
                    }
                    break;
                case State.angry:
                    if (Being.Health / (float)Being.MaxHealth < LOW_HEALTH) {
                        state = State.afraid;
                        Notify("become-afraid");
                    }
                    break;
                case State.afraid:
                    if (Being.Health / (float)Being.MaxHealth > HIGH_HEALTH) state = State.angry;
                    break;
            }
        }

        private void reactToAttack(AttackStimulus stimulus) {
            Being attacker = stimulus.Attacker;
            if (!hostileBeings.Contains(attacker)) {
                Notify("become-hostile");
                hostileBeings.Add(attacker);
                attacker.Killed += new EventHandler(attacker_Killed);
            }
        }

        private void attacker_Killed(object sender, EventArgs e) {
            Being being = (Being)sender;
            if (hostileBeings.Contains(being)) {
                hostileBeings.Remove(being);
                being.Killed -= new EventHandler(attacker_Killed);
            }
        }


        #endregion
    }
}
