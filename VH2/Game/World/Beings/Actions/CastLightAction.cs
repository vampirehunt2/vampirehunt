using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Beings;
using VH.Engine.Levels;
using VH.Engine.Game;
using VH.Engine.World.Beings.Actions;

namespace VH.Game.World.Beings.Actions {

    public class CastLightAction: Engine.World.Beings.Actions.AbstractAction {

        private const int RANGE = 4;

        public CastLightAction(): base(null) { }
        public CastLightAction(Being performer) : base(performer) { }

        public override bool Perform() {
            if (((ISkillsBeing)performer).Skills["magick"].Roll()) {
                for (int i = -RANGE; i <= RANGE; ++i) {
                    for (int j = -RANGE; j <= RANGE; ++j) {
                        if (i * i + j * j <= RANGE * RANGE) {
                            int x = Performer.Position.X + i;
                            int y = Performer.Position.Y + j;
                            Terrain terrain = Terrain.Get(GameController.Instance.Map[x, y]);
                            GameController.Instance.Map[x, y] = getLitTerrain(terrain).Character;
                        }
                    }
                }
                notify("cast-light");
            } else {
                notify("spell-failed");
            }
            return true;
        }

        private Terrain getLitTerrain(Terrain terrain) {
            if (terrain.Key == "dark-floor") return Terrain.Get("floor");
            if (terrain.Key == "dark-ground") return Terrain.Get("ground");
            if (terrain.Key == "dark-corridor") return Terrain.Get("corridor");
            if (terrain.Key == "dark-grass") return Terrain.Get("grass");
            return terrain;
        }

    }
}
