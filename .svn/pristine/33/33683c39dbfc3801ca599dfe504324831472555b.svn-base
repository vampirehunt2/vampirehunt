using System;
using System.Collections.Generic;
using System.Linq;
using VH.Engine.Display;
using VH.Engine.Levels;
using VH.Game;
using VH.Engine.Game;
using System.Windows.Forms;

namespace VH2 {

    class Program {

        [STAThread]
        static void Main(string[] args) {
            GameController.Instance = new VhGameController();
//#if !DEBUG
            try {
                if (args.Length > 0 && !args[0].StartsWith("#")) {
                    GameController.Instance.Play(args[0]);
                } else {

                GameController.Instance.Play();

                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace);
                Console.ReadKey();
                string filename = Application.StartupPath + "\\Data\\_error.log";
                System.IO.File.WriteAllText(filename, ex.Message + "\n" + ex.StackTrace);
            }
//#else
//            GameController.Instance.Play();
//#endif
        }
    }
}
