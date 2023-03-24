using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Levels;

namespace VH.Engine.Display {

    /// <summary>
    /// Minimizes Write operations by remembering what is currently displayed, 
    /// and only updating the screen if something has changed. 
    /// Bypassing this mechanism by Write'ing directly to the underlying IConsole
    /// or having another Window overlaping with this BufferedWindow causes the
    /// buffering to fail and will result in glitches. 
    /// It's up to the programmer to avoid that. 
    /// </summary>
    public class BufferedWindow : Window {

        #region fields

        protected char[,] buffer;
        protected char[,] newBuffer;
        protected ConsoleColor[,] colorBuffer;
        protected ConsoleColor[,] newColorBuffer;
        protected ConsoleColor[,] backcolorBuffer;
        protected ConsoleColor[,] newBackcolorBuffer;
        private int cursorX;
        private int cursorY;

        #endregion

        #region constructors

        public BufferedWindow() { }

        /// <summary>
        /// Creates a BufferedWindow
        /// </summary>
        /// <param name="x">The x position of this BufferedWindow with respect to console</param>
        /// <param name="y">The y position of this BufferedWindow with respect to console</param>
        /// <param name="width">The width of this BufferedWindow</param>
        /// <param name="height">The height of this BufferedWindow</param>
        /// <param name="console">The console, on which this BufferedWindow is writing</param>
        public BufferedWindow(int x, int y, int width, int height, IConsole console) :
            base(x, y, width, height, console) {
            initBuffers();
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            initBuffers();
        }

        /// <summary>
        /// Writes the specified character at given coordinates
        /// </summary>
        /// <param name="c">The character to write</param>
        /// <param name="x">The x corrdinate of the write position</param>
        /// <param name="y">The y coordinate of the write position</param>
        public override void Write(char c, int x, int y) {
            GoTo(x, y);
            Write(c);
        }

        /// <summary>
        /// Writes the specified character at current cursor position
        /// </summary>
        /// <param name="c">The character to write</param>
        public override void Write(char c) {
            if (cursorX < 0 || cursorY < 0 ||
                cursorX > newBuffer.GetUpperBound(0) ||
                cursorY > newBuffer.GetUpperBound(1)) return;
            newBuffer[cursorX, cursorY] = c;
            newColorBuffer[cursorX, cursorY] = console.ForegroundColor;
            newBackcolorBuffer[cursorX, cursorY] = console.BackgroundColor;
        }

        /// <summary>
        /// Places the cursor at the specified position
        /// </summary>
        /// <param name="x">The x corrdinate of the cursor position</param>
        /// <param name="y">The y coordinate of the cursor position</param>
        public override void GoTo(int x, int y) {
            cursorX = x;
            cursorY = y;
            /*
            if (cursorX < 0) cursorX = 0;
            if (cursorY < 0) cursorY = 0;
            if (cursorX >= Width) cursorX = Width - 1;
            if (cursorY >= Height) cursorY = Height - 1;
            */
            base.GoTo(x, y);
        }

        /// <summary>
        /// Refreshes this BufferedWindow from the buffers.
        /// </summary>
        public void Refresh() {
            for (int i = 0; i < Width; ++i) {
                for (int j = 0; j < Height; ++j) {
                    if (newBuffer[i, j] != buffer[i, j]
                        || newColorBuffer[i, j] != colorBuffer[i, j]
                        || newBackcolorBuffer[i, j] != backcolorBuffer[i, j]
                    ) {
                        colorBuffer[i, j] = newColorBuffer[i, j];
                        backcolorBuffer[i, j] = newBackcolorBuffer[i, j];
                        buffer[i, j] = newBuffer[i, j];
                        //newBuffer[i, j] = ' ';
                        console.ForegroundColor = colorBuffer[i, j];
                        console.BackgroundColor = backcolorBuffer[i, j];
                        base.Write(buffer[i, j], i, j);
                    }
                }
            }
            if (console.IsDoubleBuffered) console.Refresh();
        }

        /// <summary>
        /// Refreshes this BufferedWindow from the buffers.
        /// </summary>
        public void Refresh(int x, int y) {
            if (x < 0 || y < 0 ||
                x > newBuffer.GetUpperBound(0) ||
                y > newBuffer.GetUpperBound(1)) return;
            if (newBuffer[x, y] != buffer[x, y]
                        || newColorBuffer[x, y] != colorBuffer[x, y]
                        || newBackcolorBuffer[x, y] != backcolorBuffer[x, y]
                    ) {
                colorBuffer[x, y] = newColorBuffer[x, y];
                backcolorBuffer[x, y] = newBackcolorBuffer[x, y];
                buffer[x, y] = newBuffer[x, y];
                //newBuffer[i, j] = ' ';
                console.ForegroundColor = colorBuffer[x, y];
                console.BackgroundColor = backcolorBuffer[x, y];
                base.Write(buffer[x, y], x, y);
            }
            if (console.IsDoubleBuffered) console.Refresh();
        }

        public void Refresh(Position pos) {
            Refresh(pos.X, pos.Y);
        }

        #endregion

        #region private methods

        private void initBuffers() {
            buffer = new char[width, height];
            colorBuffer = new ConsoleColor[width, height];
            backcolorBuffer = new ConsoleColor[width, height];
            newBuffer = new char[width, height];
            newColorBuffer = new ConsoleColor[width, height];
            newBackcolorBuffer = new ConsoleColor[width, height];
        }

        #endregion

    }
}
