using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    /// <summary>
    /// Represents a character console.
    /// All coordinates are in characters, not pixels.
    /// </summary>
    public interface IConsole {

        /// <summary>
        /// Writes the specified character at given coordinates
        /// </summary>
        /// <param name="c">The character to write</param>
        /// <param name="x">The x corrdinate of the write position</param>
        /// <param name="y">The y coordinate of the write position</param>
        void Write(char c, int x, int y);

        /// <summary>
        /// Writes the specified character at current cursor position
        /// </summary>
        /// <param name="c">The character to write</param>
        void Write(char c);

        /// <summary>
        /// Writes the specified string at current cursor position
        /// </summary>
        /// <param name="c">The string to write</param>
        void Write(string s);

        /// <summary>
        /// Places the cursor at the specified position
        /// </summary>
        /// <param name="x">The x corrdinate of the cursor position</param>
        /// <param name="y">The y coordinate of the cursor position</param>
        void GoTo(int x, int y);

        /// <summary>
        /// Refreshes this IConsole, provided that it is double buffered
        /// Does nothing otherwise
        /// </summary>
        void Refresh();

        /// <summary>
        /// Indicates whether this IConsole implementation supports double buffering
        /// </summary>
        bool IsDoubleBuffered { get; }

        /// <summary>
        /// Indicates whether this IConsole implementation supports color display
        /// </summary>
        bool IsColor { get; }

        /// <summary>
        /// Reads a character from input
        /// </summary>
        /// <returns>The character read from input</returns>
        char ReadKey();

        /// <summary>
        /// Reads a line from input
        /// </summary>
        /// <returns>The line read from input</returns>
        string ReadLine();

        /// <summary>
        /// Gets or sets the number of columns of this IConsole
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the number of rows of this IConsole
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Indicates the maximum possible number of columns of this IConsole
        /// </summary>
        int MaximnumWidth { get; }

        /// <summary>
        /// Indicates the maximum possible number of rows of this IConsole
        /// </summary>
        int MaximumHeight { get; }

        /// <summary>
        /// Gets or sets the color with which the next write operation will use as the foreground color
        /// </summary>
        ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color with which the next write operation will use as the background color
        /// </summary>
        ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Clears the whole IConsole
        /// </summary>
        void Clear();

        void Clear(int x, int y, int width, int height);

        /// <summary>
        /// Gets or sets a value indicating whether the cursor is visible
        /// </summary>
        bool CursorVisible { get; set; }

        /// <summary>
        /// Clears the buffer of this IConsole.
        /// </summary>
        void ClearBuffer();
    }
}
