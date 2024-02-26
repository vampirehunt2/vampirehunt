using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VH.Engine.Display {
    public class ColorVerticalMessageWindow : VerticalMessageWindow {
        public ColorVerticalMessageWindow(int x, int y, int width, int height, IConsole console): 
            base(x, y, width, height, console) {
        }

        public void SetColor(ConsoleColor color) {
            console.ForegroundColor = color;
        }

        public override void ShowMessage(string message) {
            message = message.Substring(0, 1).ToUpper() + message.Substring(1);
            string[] messages = message.Split(new char[] { '\n', '\r' });
            for (int i = 0; i < messages.Length; ++i) {
                showMessage(messages[i]);
            }
        }

        public virtual void ShowMessage(string message, ConsoleColor color) {
            SetColor(color);
            ShowMessage(message);   
        }

    }
}
