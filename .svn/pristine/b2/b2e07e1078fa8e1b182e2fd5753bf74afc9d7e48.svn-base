using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    /// <summary>
    /// Represents a text menu in which the user can select a value by using character keys
    /// </summary>
    public class LetterMenu: Menu {

        #region fields

        private MessageWindow window;

        private string title;
        private bool canEscape = true;

        private readonly char[] chars = new char[] {'a', 'b', 'c', 'd', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A',
            'B', 'C', 'D', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
            'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        

        #endregion

        #region constructors

        /// <summary>
        /// Creates a new LetterMenu instance
        /// </summary>
        /// <param name="title">The title of the LetterMenu</param>
        /// <param name="objects">The objects to choose from</param>
        /// <param name="window">The window in which the LetterMenu will be displayed</param>
        public LetterMenu(string title, object[] objects, MessageWindow window) {
            this.title = title;
            this.objects = objects;
            this.window = window;
        }

        /// <summary>
        /// Creates a new LetterMenu instance
        /// </summary>
        /// <param name="title">The title of the LetterMenu</param>
        /// <param name="objects">The objects to choose from</param>
        /// <param name="window">The window in which the LetterMenu will be displayed</param>
        /// <param name="canEscape">A value that indicates whether the LetterMenu can be escaped</param>
        public LetterMenu(string title, object[] objects, MessageWindow window, bool canEscape): this(title, objects, window) {
            this.canEscape = canEscape;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets a value that indicated whether the LetterMenu can be escaped.
        /// If set to true, pressing ESC on this LetterMenu causes it to exit with MenuResult.Cancel.
        /// Defaults to true.
        /// </summary>
        public bool CanEscape {
            get { return canEscape; }
            set { canEscape = value; }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Displays the menu and lets the user select a value.
        /// </summary>
        /// <returns>MenuResult.OK if the user has selected a value
        /// MenuResult.Cancel if the user has escaped from this menu</returns>
        public override MenuResult ShowMenu() {
            if (objects == null) throw new ArgumentNullException("Menu objects list is null");
            StringBuilder sb = new StringBuilder();
            // build menu
            sb.Append(title + "\n");
            for (int i = 0; i < objects.Length; ++i) {
                sb.Append(
                    getCharForIndex(i) + 
                    " - " + 
                    objects[i].ToString() +
                    "\n"
                );
            }
            // display menu
            window.Clear();
            window.Console.ForegroundColor = ConsoleColor.Gray;
            window.ShowMessage(sb.ToString());
            window.Console.Refresh();
            // ready user input
            char c;
            do {
                c = window.ReadKey();
                if ( c == ESC && CanEscape) return MenuResult.Cancel;
            } while (!isValidChar(c));
            SelectedIndex = getIndexforChar(c);
            return MenuResult.OK;
        }

        #endregion

        #region private methods

        private char getCharForIndex(int i) {
            return chars[i];
        }

        private int getIndexforChar(char c) {
            for (int i = 0; i < chars.Length; ++i) {
                if (chars[i] == c) return i;
            }
            throw new ArgumentOutOfRangeException("character " + c + " is not a valid menu index");
        }

        private bool isValidChar(char c) {
            if (!chars.Contains(c)) return false;
            return getIndexforChar(c) < objects.Length;
        }

        #endregion

    }
}
