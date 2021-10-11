using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.Translations;

namespace VH.Engine.Display {
    public class QuantityMenu : Menu {

        private MessageWindow window;
        private int quantity = 0;

        public QuantityMenu(MessageWindow window) {
            this.window = window;
        }

        public int Quantity {
            get { return quantity; }
        }

        public override MenuResult ShowMenu() {
            window.Clear();
            window.Console.ForegroundColor = ConsoleColor.Gray;
            string howMany = Translator.Instance["how-many"];
            window.ShowMessage(howMany);
            window.Console.Refresh();
            // ready user input
            char c;
            StringBuilder sb = new StringBuilder();
            int l = 0;
            do {
                c = window.ReadKey();
                if (c != '\r') sb.Append(c);
                if (c == ESC) return MenuResult.Cancel;
                window.Write(c, howMany.Length + 2 + l++, 0);
                window.Console.Refresh();
            } while (c != '\r');
            if (Int32.TryParse(sb.ToString(), out quantity)) return MenuResult.OK;
            else return MenuResult.Cancel;
        }
    }
}
