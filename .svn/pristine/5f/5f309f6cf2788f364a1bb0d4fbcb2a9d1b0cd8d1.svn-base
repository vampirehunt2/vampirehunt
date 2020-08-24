using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    public class YesNoMenu: Menu {

        string title;
        MessageWindow window;
        char yesChar;
        char noChar;

        public YesNoMenu(string title, MessageWindow window, char yesChar, char noChar) {
            this.title = title;
            this.window = window;
            this.yesChar = yesChar;
            this.noChar = noChar;
        }

        public override MenuResult ShowMenu() {
            window.ShowMessage(title + " " + yesChar + "/" + noChar);
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

    }
}
