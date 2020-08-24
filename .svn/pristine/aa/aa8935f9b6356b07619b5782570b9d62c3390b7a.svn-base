using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VH.Engine.Game;
using VH.Engine.Display;
using VH.Engine.Translations;
using System.Diagnostics;

namespace VH.Game {

    public class SplashScreen {

        private static readonly string FILENAME = Application.StartupPath + @"\Data\Resources\splash.txt";

        private string splashText;
        private string pcName = "";
        IConsole console = GameController.Instance.Console;

        public SplashScreen() {
            StreamReader sr = new StreamReader(new FileStream(FILENAME, FileMode.Open));
            splashText = sr.ReadToEnd();
        }

        public string PcName {
            get { return pcName; }
        }

        public void Show() {
            console.ForegroundColor = ConsoleColor.Red;
            console.Write(splashText);
            showInfo();
            readPcName();
        }

        private void showInfo() {
            console.ForegroundColor = ConsoleColor.DarkYellow;
            StringBuilder sb = new StringBuilder();
            sb.Append("  " + Translator.Instance["version"] + Application.ProductVersion + "\n");
            sb.Append("  Engine: " + FileVersionInfo.GetVersionInfo(Application.StartupPath + @"\VH.Engine.dll").ProductVersion + "\n");
            //sb.Append("  www.vampirehunt.origo.ethz.ch\n");
            sb.Append("  vampirehunt@o2.pl\n");
            console.GoTo(0, 25);
            console.Write(sb.ToString());
        }

        private void readPcName() {
            console.ForegroundColor = ConsoleColor.Gray;
            string prompt = Translator.Instance["name-prompt"];
            console.GoTo(5, 35);
            console.Write(prompt);
            do {
                console.GoTo(5 + prompt.Length, 35);
                console.Write("                                ");
                console.GoTo(5 + prompt.Length, 35);
                console.Refresh();
                pcName = console.ReadLine();
            } while (pcName.Length == 0);
            if (pcName.Length > 8) pcName = pcName.Substring(0, 8);
        }

    }
}
