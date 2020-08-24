using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    /// <summary>
    /// This class represents a message window, that is 
    /// a portion of the screen on which messages are presented to the user.
    /// </summary>
    public abstract class MessageWindow: ExtendedWindow {

        /// <summary>
        /// Shows a message in this MessageWindow
        /// </summary>
        /// <param name="message">The message to show.</param>
        public abstract void ShowMessage(string message);

        /// <summary>
        /// Reads a key from this message window.
        /// </summary>
        /// <returns>The key pressed by the user.</returns>
        public char ReadKey() {
            return console.ReadKey();
        }

        /// <summary>
        /// Creates a new MessageWindow
        /// </summary>
        /// <param name="x">The x coordinate of the upper-left corner of the MessageWindow.</param>
        /// <param name="y">The y coordinate of the upper-left corner of the MessageWindow.</param>
        /// <param name="width">The width of the MessageWindow</param>
        /// <param name="height">The height of the MessageWindow</param>
        /// <param name="console">The console on which the MessageWindow is located</param>
        public MessageWindow(int x, int y, int width, int height, IConsole console) 
         : base(x, y, width, height, console) {
        }
    }
}
