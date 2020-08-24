using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.Levels {

    public class Map: AbstractPersistent {

        #region constants

        public const int MAX_WIDTH = 100;
        public const int MAX_HEIGHT = 100;

        public const string WIDTH = "width";
        public const string HEIGHT = "height";
        public const string MAP = "map";
        public const string MEM = "mem";

        /*public const char FLOOR = '.';
        public const char CORRIDOR = (char)129;
        public const char WALL = '#';
        public const char OPEN_DOOR = '/';
        public const char CLOSED_DOOR = '+';
        public const char WATER = '~';
        public const char DOWN_STAIR = '>';
        public const char UP_STAIR = '<';
        public const char KNOWN_TRAP = '_';
        public const char UNKNOW_TRAP = (char)130;*/
        public const char UNKNOWN = ' ';
        //public const char GRASS = (char)131;
        //public const char TREE = '&';

        #endregion

        #region fields

        private char[,] map;
        private char[,] mem;
        private int width;
        private int height;

        #endregion

        #region constructors

        public Map(int width, int height) {
            this.width = width;
            this.height = height;
            init();
            for (int i = 0; i < width; ++i) {
                for (int j = 0; j < height; ++j) {
                    Mem[i, j] = Map.UNKNOWN;
                }
            }
        }

        public Map() { }

        #endregion

        #region properties

        public int Width {
            get { return width; }
            set { width = value; }
        }

        public int Height {
            get { return height; }
            set { width = value; }
        }

        public char this[int i, int j] {
            get {
                if (i < 0 || j < 0 || i >= Width || j >= Height) return Map.UNKNOWN;
                return map[i, j]; 
            }
            set {
                if (i >= 0 && j >= 0 && i < Width && j < Height) map[i, j] = value; 
            }
        }

        public char this[Position position] {
            get { return this[position.X, position.Y]; }
            set { this[position.X, position.Y] = value; }
        }

        public char[,] Mem {
            get { return mem; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            width = GetIntAttribute(WIDTH);
            height = GetIntAttribute(HEIGHT);
            init();

            string mapStr = GetStringAttribute(MAP);
            for (int j = 0; j < height; ++j) {
                for (int i = 0; i < width; ++i) map[i, j] = mapStr[i + j * width];
            }

            string memStr = GetStringAttribute(MEM);
            for (int j = 0; j < height; ++j) {
                for (int i = 0; i < width; ++i) mem[i, j] = memStr[i + j * width];
            }
        }
    

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(HEIGHT, height);
            AddAttribute(WIDTH, width);

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < height; ++j) {
                for (int i = 0; i < width; ++i) sb.Append(map[i, j]);
            }
            AddAttribute(MAP, sb.ToString());

            sb = new StringBuilder();
            for (int j = 0; j < height; ++j) {
                for (int i = 0; i < width; ++i) sb.Append(mem[i, j]);
            }
            AddAttribute(MEM, sb.ToString());
            return element; 
        }

        /// <summary>
        /// Indicates, whether light can pass a specified terrain feature.
        /// Useful for computing FoV
        /// </summary>
        /// <param name="c">a character reoresenting the terrain feature</param>
        /// <returns>true, if light can pass the terrain feature</returns>
        public static bool IsTransparent(char c) {
            return Terrain.Get(c).Transparent;
        }

        public virtual void Save(string name) { 
            // just do nothing and let the data float around in the memory
        }

        public virtual void Load(string name) { 
            //do nothing, as the data is already floating around in memory
        }

        #endregion

        #region private methods

        private void init() {
            map = new char[width, height];
            mem = new char[width, height];
        }

        #endregion

    }
}
