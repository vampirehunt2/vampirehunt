using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Game;
using VH.Engine.Levels;
using VH.Engine.World.Beings;
using VH.Engine.World.Beings.Actions;
using VH.Engine.World.Beings.AI;

namespace MyRoguelike.Game.Beings.Ai {
    public class MyPcAi : BaseAi {

        #region constructors
        public MyPcAi(Pc pc) : base(pc) { }

        #endregion

        #region public methods

        public override AbstractAction SelectAction() {
            Pc pc = (Pc)Being;
            string command = ((MyGameController)GameController.Instance).Command;
            AbstractAction action = null;
            //
            if (command == "wait") action = new WaitAction(pc);
            //
            else if (command == "north") action = new MoveAction(pc, Step.NORTH);
            else if (command == "south") action = new MoveAction(pc, Step.SOUTH);
            else if (command == "east") action = new MoveAction(pc, Step.EAST);
            else if (command == "west") action = new MoveAction(pc, Step.WEST);
            else if (command == "north-east") action = new MoveAction(pc, Step.NORTH_EAST);
            else if (command == "north-west") action = new MoveAction(pc, Step.NORTH_WEST);
            else if (command == "south-east") action = new MoveAction(pc, Step.SOUTH_EAST);
            else if (command == "south-west") action = new MoveAction(pc, Step.SOUTH_WEST);
            else if (command == "take-stairs") action = new TakeStairsAction(pc);
            else if (command == "close-door") action = new CloseDoorAction(pc);

            // add other actions below

            //

            return action;
        }

        public override bool InteractWithEnvironment(Position position) {
            if (new OpenDoorAction(Being, position).Perform()) return true;
            return base.InteractWithEnvironment(position);
        }

        #endregion



    }
}
