using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    public class VerticalMessageWindow: MessageWindow {

        #region constants

        private const string ENTER = "<ENTER>";

        #endregion

        #region fields

        protected int row = 0;

        #endregion

        #region constructors

        public VerticalMessageWindow(int x, int y, int width, int height, IConsole console) :
            base(x, y, width, height, console) { }

        #endregion

        #region public methods

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public override void ShowMessage(string message) {
            message = message.Substring(0, 1).ToUpper() + message.Substring(1);
            console.ForegroundColor = ConsoleColor.Gray;
            string[] messages = message.Split(new char[] {'\n', '\r'});
            for (int i = 0; i < messages.Length; ++i) {
                showMessage(messages[i]);
            }
        }

        public override void Clear() {
            Console.Clear(X, Y, Width, row);
            row = 0;
        }

        #endregion

        #region protected methods

        protected virtual void showMessage(string message) {
            if (message.Length <= Width) showSimpleMessage(message);
            else split(message);
        }

        protected virtual void showSimpleMessage(string message) {
            //if (row > height / 2) Clear();
            base.GoTo(0, row++);
            if (row == Height - 1) {
                base.Write(ENTER);
                console.ReadLine();
                Clear();
                row = 0;
                base.GoTo(0, row++);
            }
            base.Write(message);
        }

        #endregion

        #region private methods

        private void split(string message) {
            string submessage = message.Substring(0, Width);
            int i = submessage.LastIndexOf(' ');
            if (i == -1) i = Width;
            string message1 = message.Substring(0, i);
            string message2 = message.Substring(i);
            showMessage(message1);
            showMessage(message2);
        }

        #endregion

    }
}
