using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {
    public class ScrollingVerticalMessageWindow : VerticalMessageWindow {

        List<string> messages = new List<string>();

        public ScrollingVerticalMessageWindow(int x, int y, int width, int height, IConsole console) : 
            base(x, y, width, height, console) {
        }

        public override void Clear() {
            base.Clear();
            messages.Clear();
        }

        protected override void showMessage(string message) {
            if (row >= height - 2) scroll();
            base.showMessage(message);
        }

        protected override void showSimpleMessage(string message) {
            base.showSimpleMessage(message);
            messages.Add(message);
        }

        private void scroll() {
            messages.RemoveAt(0);
            base.Clear();
            for (int i = 0; i < messages.Count; ++i) base.showSimpleMessage(messages[i]);
        }

    }
}
