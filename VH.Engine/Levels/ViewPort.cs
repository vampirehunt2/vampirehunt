using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Levels;
using VH.Engine.Display;
using VH.Engine.LineOfSight;
using System.Xml;

namespace VH.Engine.Levels {

    /// <summary>
    /// Manages displaying of Levels.
    /// Translates Level coordinates to Window coordinates.
    /// Translates terrain features to their display counterparts - characters to be displayed in the UI.
    /// Manages terrain feature colors.
    /// Enables Level memory.
    /// Be sure to always have a valid, non null Level set as the value of the Level property.
    /// </summary>
    public class ViewPort: BufferedWindow {

        #region constants

        private const string POSITION = "position";
        private const string SHADE = "shade";

        #endregion

        #region fields

        protected Position position;
        protected Map map;
        private bool shade = true;
        private ConsoleColor shadeColor = ConsoleColor.DarkGray;

        #endregion

        #region constructors

        public ViewPort() { }

        /// <summary>
        /// Creates a ViewPort.
        /// Note that after the ViewPort is created and before it is used, Level property has to be set.
        /// This has been left out of the constructor to allow greater flexibility (otherwise this is a bad practice...).
        /// This way the ViewPort can be created before the first level is set up.
        /// </summary>
        /// <param name="x">X coordinate of the upper left corner of this ViewPort in IConsole coordinates</param>
        /// <param name="y">Y coordinate of the upper left corner of this ViewPort in IConsole coordinates</param>
        /// <param name="width">Width of this ViewPort</param>
        /// <param name="height">Height of this ViewPort</param>
        /// <param name="console">The IConsole, to which this ViewPort writes</param>
        /// <param name="position">Position of the upper left corner of this ViewPort in Level coordinates</param>
        public ViewPort(int x, int y, int width, int height, IConsole console, Position position):
                base(x, y, width, height, console) {
            this.position = position;
        }

        

        #endregion

        #region properties

        /// <summary>
        /// Returns the display character of the corresponding Level square
        /// </summary>
        /// <param name="i">in VieWPort coorditanes</param>
        /// <param name="j">in ViewPort coordinates</param>
        /// <returns></returns>
        public char this[int i, int j] {
            get {
                i += position.X;
                j += position.Y;
                if ( i > map.Width || j > map.Height || i < 0 || j < 0) return Map.UNKNOWN;
                return GetDisplayCharacter(map[i, j]); 
            }
        }

        /// <summary>
        /// Position of the upper left corner of this ViewPort in Level coordinates
        /// </summary>
        public Position Position {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// The Level at which this ViewPort is 'looking'
        /// </summary>
        public Map Map {
            get { return map; }
            set { 
                map = value;
                Clear();
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether level area outside of 
        /// field of vision should be shaded.
        /// </summary>
        public bool Shade {
            get { return shade; }
            set { shade = value; }
        }

        public ConsoleColor ShadeColor {
            get { return shadeColor; }
            set { shadeColor = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            shade = GetBoolAttribute(SHADE);
            position = GetElement(POSITION) as Position;
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute(SHADE, shade);
            AddElement(POSITION, position);
            return element;
        }

        /// <summary>
        /// Prevents glitches that occur after changing of the ViewPort position.
        /// Call each time Position is changed. If You have a scrolling map, call this in each round/turn.
        /// It has to be called before RenderMap.
        /// </summary>
        public void PreventGlitches() {
            for (int i = 0; i < Width; ++i) {
                for (int j = 0; j < Height; ++j) {
                    if (buffer[i, j] != Map.UNKNOWN) newBuffer[i, j] = Map.UNKNOWN;
                }
            }
        }
        
        /// <summary>
        /// Renders the parts of the level map that the character sees or remembers.
        /// </summary>
        /// <param name="fieldOfVision"></param>
        public virtual void RenderMap(AbstractFieldOfVision fieldOfVision) {
              for (int i = 0; i < Width; ++i) {
                  for (int j = 0; j < Height; ++j) {
                     if ( isInFieldOfVision(i, j, fieldOfVision) ) {
                          console.ForegroundColor = GetDisplayColor(i, j);
                          Write(this[i, j], i, j);
                          Memorize(i, j);
                     } else if (getMemAt(i, j) != Map.UNKNOWN){
                         if (Shade) console.ForegroundColor = ShadeColor;
                         else console.ForegroundColor = GetDisplayColor(i, j);
                         Write(getDisplayMemAt(i, j), i, j);
                     }
                  }
              }
          }
        /*
        public virtual void RenderMap(AbstractFieldOfVision fieldOfVision) {
            Position observer = fieldOfVision.Observer;
            int x0 = Math.Max(observer.X - Position.X - AbstractFieldOfVision.MAX_VISION_RANGE - 1, 0);
            int x1 = Math.Min(x0 + AbstractFieldOfVision.MAX_VISION_RANGE * 2 + 2, Width - 1);
            int y0 = Math.Max(observer.Y - Position.Y - AbstractFieldOfVision.MAX_VISION_RANGE - 1, 0);
            int y1 = Math.Min(y0 + AbstractFieldOfVision.MAX_VISION_RANGE * 2 + 2, Height - 1);
            for (int i = x0; i <= x1; ++i) {
                for (int j = y0; j <= y1; ++j) {
                    if (isInFieldOfVision(i, j, fieldOfVision)) {
                        console.ForegroundColor = GetDisplayColor(i, j);
                        Write(this[i, j], i, j);
                        Memorize(i, j);
                    } else if (Shade && getMemAt(i, j) != Map.DARKNESS) {
                        console.ForegroundColor = ShadeColor;
                        Write(getMemAt(i, j), i, j);
                    }
                }
            }
        }*/
         
          /*
          public virtual void RenderMap(AbstractFieldOfVision fieldOfVision) {
              for (int i = 0; i < Width; ++i) {
                  for (int j = 0; j < Height; ++j) {
                      console.ForegroundColor = GetDisplayColor(this[i, j]);
                      if (isInFieldOfVision(i, j, fieldOfVision)) {
                          console.BackgroundColor = ConsoleColor.DarkGray;
                          Write(this[i, j], i, j);
                          Memorize(i, j);
                      } else if (Shade && (getMemAt(i, j) != Map.DARKNESS || newBackcolorBuffer[i, j] != ConsoleColor.Black)) {
                          console.BackgroundColor = ConsoleColor.Black;
                          Write(getMemAt(i, j), i, j);
                      }
                  }
              }
          }*/

        public void Display(AbstractEntity entity) {
            int i = entity.Position.X - Position.X;
            int j = entity.Position.Y - Position.Y;
            console.ForegroundColor = entity.Color;
            Write(entity.Character, i, j);
        }

        public void Display(AbstractEntity entity, AbstractFieldOfVision fov, Position observer) {
            int x = entity.Position.X - observer.X;
            int y = entity.Position.Y - observer.Y;
            if (fov[x, y]) Display(entity);
        }


        public virtual char GetDisplayCharacter(char c) {
            return Terrain.Get(c).Display;
        }

        /// <summary>
        /// Reimplement in subclass to change display colors
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual ConsoleColor GetDisplayColor(char c) {
            return Terrain.Get(c).Color;
        }

        /// <summary>
        /// Reimplement in subclass to change display colors
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual ConsoleColor GetDisplayColor(int i, int j) {
            i += position.X;
            j += position.Y;
            if (i > map.Width || j > map.Height || i < 0 || j < 0) return ConsoleColor.Black;
            return GetDisplayColor(map[i, j]);
        }

        public void Clear() {
            for (int i = 0; i < Width; ++i) {
                for (int j = 0; j < Height; ++j) {
                    Write(' ', i, j);
                }
            }
            Refresh();
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Saves the level square in level memory
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        protected void Memorize(int i, int j) {
            i += position.X;
            j += position.Y;
            if ( i >= 0 && j >= 0 && i < map.Width && j < map.Height ) {
                map.Mem[i, j] = map[i, j];
            }
        }

        /// <summary>
        /// Gets the memorized level square.
        /// </summary>
        /// <param name="i">in ViewPort coordinates</param>
        /// <param name="j">in ViewPort coordinates</param>
        /// <returns>memorized level square.</returns>
        protected char getDisplayMemAt(int i, int j) {
            i += position.X;
            j += position.Y;
            if (i >= map.Width || j >= map.Height || i < 0 || j < 0) return Map.UNKNOWN;
            char mem = GetDisplayCharacter(map.Mem[i, j]);
            return mem;
        }

        protected char getMemAt(int i, int j) {
            i += position.X;
            j += position.Y;
            if (i >= map.Width || j >= map.Height || i < 0 || j < 0) return Map.UNKNOWN;
            return map.Mem[i, j];
        }

        #endregion

        #region private methods

        /// <summary>
        /// Indicates whether a square of coordinates (x, y) is inside of fov.
        /// x and y are in ViewPort coordinates (not IConsole ol Level coordinates)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="fieldOfVision"></param>
        /// <returns></returns>
        private bool isInFieldOfVision(int i, int j, AbstractFieldOfVision fieldOfVision) {
            i += Position.X;
            j += Position.Y;
            i -= fieldOfVision.Observer.X;
            j -= fieldOfVision.Observer.Y;
            return fieldOfVision[i, j];
        }

        #endregion

    }
}
