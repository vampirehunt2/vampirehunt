using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    public enum OverflowBehavior {
        Ignore,
        Wrap,
        Truncate,
        ThrowException
    }

    /// <summary>
    /// Window with string manipulation capabilities
    /// </summary>
    public class ExtendedWindow: Window {

        #region fields

        protected string clearer;

        #endregion

        #region constructors

        /// <summary>
        /// Creates an ExtendedWindow
        /// </summary>
        /// <param name="x">The x position of this ExtendedWindow with respect to console</param>
        /// <param name="y">The y position of this ExtendedWindow with respect to console</param>
        /// <param name="width">The width of this ExtendedWindow</param>
        /// <param name="height">The height of this ExtendedWindow</param>
        /// <param name="console">The console, on which this ExtendedWindow is writing</param>
        public ExtendedWindow(int x, int y, int width, int height, IConsole console): 
            base(x, y, width, height, console) {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < width; ++i) builder.Append(' ');
            clearer = builder.ToString();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Writes the specified string at given coordinates
        /// </summary>
        /// <param name="s">The string to write</param>
        /// <param name="x">The x corrdinate of the write position</param>
        /// <param name="y">The y coordinate of the write position</param>
        public void Write(string s, int x, int y) {
            // TODO handle window width overflow
            console.GoTo(x + this.x, y + this.y);
            Write(s);
        }

        /// <summary>
        /// Writes the specified string at current cursor position
        /// </summary>
        /// <param name="s">The string to write</param>
        public void Write(string s) {
            console.Write(s);
        }

        /// <summary>
        /// Clears this ExtendedWindow
        /// </summary>
        public virtual void Clear() {
            for (int i = 0; i < height; ++i) {
                Write(clearer, 0, i);
            }
        }

        #endregion
    }
}
