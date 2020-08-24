using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    /// <summary>
    /// Wraps default System.Console. 
    /// To use this console you need to run your app as console application.
    /// All instances of this class write to the same physical console.
    /// </summary>
    public class SystemConsole: IConsole {

        #region constructors

        public SystemConsole() {
            Console.SetWindowSize(80, 50);
        }

        #endregion

        #region properties

        public bool IsDoubleBuffered {
            get { return false; }
        }

        public bool IsColor {
            get { return true; }
        }

        public int Width {
            get { return Console.WindowWidth; }
            set { Console.WindowWidth = value; }
        }

        public int Height {
            get { return Console.WindowHeight; }
            set { Console.WindowHeight = value; }
        }

        public int MaximnumWidth {
            get { return Console.LargestWindowWidth; }
        }

        public int MaximumHeight {
            get { return Console.LargestWindowHeight; }
        }

        public bool CursorVisible {
            get { return Console.CursorVisible; }
            set { Console.CursorVisible = value; }
        }

        #endregion

        #region public methods

        public void Write(char c, int x, int y) {
            GoTo(x, y);
            Write(c);
        }

        public void Refresh() {
            // do nothing
        }

        public char ReadKey() {
            return Console.ReadKey(true).KeyChar;
        }

        public string ReadLine() {
            return Console.ReadLine();
        }

        public ConsoleColor ForegroundColor {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public ConsoleColor BackgroundColor {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        public void Clear() {
            Console.Clear();
        }

        public void Clear(int x, int y, int width, int height) {
            string clearer = "";
            for (int i = 0; i < width; ++i) clearer += " ";
            for (int i = 0; i < height; ++i) {
                GoTo(x, y + i);
                Write(clearer);
            }
            
        }

        public void Write(char c) {
            Console.Write(c);
        }

        public void Write(string s) {
            Console.Write(s);
        }

        public void GoTo(int x, int y) {
            Console.SetCursorPosition(x, y);
        }

        public void ClearBuffer() {
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        #endregion

    }
}
