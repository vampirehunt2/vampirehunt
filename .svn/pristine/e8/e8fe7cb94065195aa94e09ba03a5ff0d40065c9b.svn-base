using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VH.Engine.Configuration {
    public class Options {

        private static readonly string FILE_PATH = Application.StartupPath + @"\Data\Conf\options.cfg";

        private Dictionary<string, string> optionDict = new Dictionary<string, string>();

        private static Options instance = null;

        private Options() {
            using (StreamReader file = new StreamReader(FILE_PATH)) {
                string line;
                while ((line = file.ReadLine()) != null) {
                    string[] tokens = line.Split('=');
                    optionDict.Add(tokens[0].Trim(), tokens[1].Trim());
                }
            }
        }

        public static Options Instance {
            get {
                if (instance == null) instance = new Options();
                return instance;
            }
        }

        public string this[string key] {
            get { return optionDict[key]; }
        }

    }
}
