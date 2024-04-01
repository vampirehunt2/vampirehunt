using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    public class YesNoMenu: Menu {

        #region fields

        string title;
        MessageWindow window;
        char yesChar;
        char noChar;

        #endregion

        #region constructors

        public YesNoMenu(string title, MessageWindow window, char yesChar, char noChar) {
            this.title = title;
            this.window = window;
            this.yesChar = yesChar;
            this.noChar = noChar;
        }

        #endregion

        #region public methods

        public override MenuResult ShowMenu() {
            window.Console.ForegroundColor = ConsoleColor.Gray;
            window.ShowMessage(title + " " + yesChar + "/" + noChar);
            window.Console.Refresh();
            char z;
            do {
                z = window.Console.ReadKey();
            } while (z != yesChar && z != noChar && z != ESC);
            if (z == yesChar) return MenuResult.OK;
            else if (z == noChar) return MenuResult.None;
            else if (z == ESC) return MenuResult.Cancel;
            // should never happen
            else return MenuResult.Cancel;
        }

        #endregion

    }
}
