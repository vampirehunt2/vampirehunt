using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Random;

namespace VH.Engine.Levels {
    class Cell {

        #region constants

        private int MIN_CELL_SIZE = 10;
        private int MIN_ROOM_SIZE = 5;
        private int MAX_ROOMS = 100;
        private float HIDDEN_DOOR_RATE = 0.02f;
        private float OPEN_DOOR_RATE = 0.2f;

        #endregion

        #region constructors

        public Cell(Map map, Cell parent) {
            splitOrientation = SplitOrientation.None;
            this.map = map;
            this.parent = parent;
        }


        public Cell(Map map) : this(map, null) {
            x1 = 0;
            y1 = 0;
            x2 = map.Width - 1;
            y2 = map.Height - 1;
        }

        #endregion

        #region fields

        int x1;
        int y1;
        int x2;
        int y2;

        Cell child1;
        Cell child2;
        Cell parent;
        Room room;

        Map map;

        SplitOrientation splitOrientation;


        #endregion

        #region properties

        int height {
            get { return Math.Abs(y1 - y2); }
        }

        int width {
            get { return Math.Abs(x1 - x2); }
        }

        bool hasChildren {
            get { return child1 != null; }
        }

        bool canSplit {
            get { return (width > MIN_CELL_SIZE * 2) || (height > MIN_CELL_SIZE * 2); }
        }

        #endregion

        #region methods

        void split() {
            splitOrientation = chooseSplit();
            if (splitOrientation == SplitOrientation.Horizontal) {
                int r = Rng.Random.Next(width - 2 * MIN_CELL_SIZE);
                r = r + MIN_CELL_SIZE;
                child1 = new Cell(map, this);
                child1.x1 = x1;
                child1.y1 = y1;
                child1.x2 = x1 + r;
                child1.y2 = y2;
                child2 = new Cell(map, this);
                child2.x1 = x1 + r;
                child2.y1 = y1;
                child2.x2 = x2;
                child2.y2 = y2;
            }
            if (splitOrientation == SplitOrientation.Vertical) {
                int r = Rng.Random.Next(height - 2 * MIN_CELL_SIZE);
                r = r + MIN_CELL_SIZE;
                child1 = new Cell(map, this);
                child1.x1 = x1;
                child1.y1 = y1;
                child1.x2 = x2;
                child1.y2 = y1 + r;
                child2 = new Cell(map, this);
                child2.x1 = x1;
                child2.y1 = y1 + r;
                child2.x2 = x2;
                child2.y2 = y2;
            }

            if (child1.canSplit) child1.split();
            if (child2.canSplit) child2.split();
        }

        SplitOrientation chooseSplit() {
            if ((width > MIN_CELL_SIZE * 2) && !(height > MIN_CELL_SIZE * 2)) return SplitOrientation.Horizontal;
            if ((height > MIN_CELL_SIZE * 2) && !(width > MIN_CELL_SIZE * 2)) return SplitOrientation.Vertical;
            if ((width > MIN_CELL_SIZE * 2) && (height > MIN_CELL_SIZE * 2)) return (SplitOrientation)(Rng.Random.Next(2) + 1);
            return SplitOrientation.None;
        }

        Cell getSibling() {
            if (parent == null) return null;
            if (parent.child1 == this) return parent.child2;
            if (parent.child2 == this) return parent.child1;
            return null;
        }

        void makeRooms() {
            if (hasChildren) {
                child1.makeRooms();
                child2.makeRooms();
            } else {
                createRoom();
            }
        }

        void createRoom() {
            room = new Room();
            int rw = Rng.Random.Next(width);
            rw = Math.Max(rw, MIN_ROOM_SIZE);
            rw = Math.Min(rw, width - 4);
            room.x1 = x1 + Rng.Random.Next(width - rw - 2) + 2;
            room.x2 = room.x1 + rw;
            int rh = Rng.Random.Next(height);
            rh = Math.Max(rh, MIN_ROOM_SIZE);
            rh = Math.Min(rh, height - 4);
            room.y1 = y1 + Rng.Random.Next(height - rh - 2) + 2;
            room.y2 = room.y1 + rh;
            room.create(map);
        }

        void connectRooms() {
            if (hasChildren) {
                if (splitOrientation == SplitOrientation.Horizontal) {
                    int targetX = child1.x2;
                    int targetY = Rng.Random.Next(child1.y2 - child1.y1) + child1.y1;
                    Room room1 = child1.getClosestRoom(targetX, targetY);
                    Room room2 = child2.getClosestRoom(targetX, targetY);
                    int cx1 = room1.x2;
                    int cy1 = Rng.Random.Next(room1.y2 - room1.y1 - 2) + room1.y1 + 1;
                    map[cx1, cy1] = getDoor();
                    while (cx1 < child1.x2) map[++cx1, cy1] = Terrain.Get("corridor").Character;
                    //
                    int cx2 = room2.x1;
                    int cy2 = Rng.Random.Next(room2.y2 - room2.y1 - 2) + room2.y1 + 1;
                    map[cx2, cy2] = getDoor();
                    while (cx2 > child1.x2) map[--cx2, cy2] = Terrain.Get("corridor").Character;
                    //
                    while (cy1 != cy2) {
                        if (cy1 > cy2) cy1--;
                        else cy1++;
                        map[cx1, cy1] = Terrain.Get("corridor").Character;
                    }
                } else {
                    int targetX = Rng.Random.Next(child1.x2 - child1.x1) + child1.x1;
                    int targetY = child1.y2;
                    Room room1 = child1.getClosestRoom(targetX, targetY);
                    Room room2 = child2.getClosestRoom(targetX, targetY);
                    int cx1 = Rng.Random.Next(room1.x2 - room1.x1 - 2) + room1.x1 + 1;
                    int cy1 = room1.y2;
                    map[cx1, cy1] = getDoor();
                    while (cy1 < child1.y2) map[cx1, ++cy1] = Terrain.Get("corridor").Character;
                    //
                    int cx2 = Rng.Random.Next(room2.x2 - room2.x1 - 1) + room2.x1 + 1;
                    int cy2 = room2.y1;
                    map[cx2, cy2] = getDoor();
                    while (cy2 > child1.y2) map[cx2, --cy2] = Terrain.Get("corridor").Character;
                    //
                    while (cx1 != cx2) {
                        if (cx1 > cx2) cx1--;
                        else cx1++;
                        map[cx1, cy1] = Terrain.Get("corridor").Character;
                    }
                }
                child1.connectRooms();
                child2.connectRooms();
            }
        }

        void getRooms(List<Room> rooms) {
            if (hasChildren) {
                child1.getRooms(rooms);
                child2.getRooms(rooms);
            } else {
                rooms.Add(room);
            }
        }

        Room getClosestRoom(int targetX, int targetY) {
            int minDistance = int.MaxValue;
            Room closestRoom = null;
            List<Room> rooms = new List<Room>();
            getRooms(rooms);
            foreach (Room room in rooms) {
                int roomX = (room.x1 + room.x2) / 2;
                int roomY = (room.y1 + room.y2) / 2;
                int distance = Math.Abs(roomX - targetX) + Math.Abs(roomY - targetY);
                if (distance < minDistance) {
                    minDistance = distance;
                    closestRoom = room;
                }
            }
            return closestRoom;
        }

        private char getDoor() {
            if (Rng.Random.NextFloat() < HIDDEN_DOOR_RATE) return Terrain.Get("hidden-door").Character;
            else if (Rng.Random.NextFloat() < OPEN_DOOR_RATE) return Terrain.Get("open-door").Character;
            else return Terrain.Get("closed-door").Character;
        }

        public void GenerateDungeon() {
            for (int i = x1; i <= x2; ++i) {
                for (int j = y1; j <= y2; ++j) {
                    map[i, j] = Terrain.Get("wall").Character;
                }
            }
            split();
            makeRooms();
            connectRooms();
        }

        #endregion
    }

}
