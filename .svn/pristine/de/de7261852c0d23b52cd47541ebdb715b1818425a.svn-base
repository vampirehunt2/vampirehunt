using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VH.Engine.Display;

namespace VH.Engine.Display {

    // Fullscreen console with roguelike capabilities.
    public partial class VHConsole : Form, IConsole {

        #region constants

        private const int MAX_WIDTH = 100;
        private const int MAX_HEIGHT = 100;

        #endregion

        #region fields

        private Graphics graphics;
        private Font font = new Font("Courier", 16, FontStyle.Bold);
        private Brush brush = new SolidBrush(Color.LightGray);
        private List<Window> windows = new List<Window>();
        private int selectedWindowIndex = -1;
        private char[,] buffer = new char[MAX_WIDTH, MAX_HEIGHT];

        #endregion

        #region constructors

        public VHConsole() {
            InitializeComponent();
            for (int i = 0; i < MAX_WIDTH; ++i) {
                for (int j = 0; j < MAX_HEIGHT; ++j) {
                    buffer[i, j] = ' ';
                }
            }
        }

        #endregion

        #region public methods

        public void Write(char c, int x, int y) {
            buffer[baseX + x, baseY + y] = c;
        }

        public void Refresh() {
            for (int i = 0; i < MAX_WIDTH; ++i) {
                for (int j = 0; j < MAX_HEIGHT; ++j) {
                    graphics.DrawString("" + buffer[i, j], font, brush, i * 14, j * 18);
                }
            }
        }

        public void RegisterWindow(Window window) {
            if (!windows.Contains(window)) windows.Add(window);
        }

        public void SelectWindow(Window window) {
            for (int i = 0; i < windows.Count; ++i) {
                if (windows[i] == window) selectedWindowIndex = i;
            }
        }

        public void SelectWindow(string windowName) {
            for (int i = 0; i < windows.Count; ++i) {
                if (windows[i].Name == windowName) selectedWindowIndex = i;
            }
        }

        public void DeselectWindow() {
            selectedWindowIndex = -1;
        }

        #endregion

        #region private properties

        private int baseX {
            get {
                if (selectedWindowIndex == -1) {
                    return 0;
                } else {
                    return windows[selectedWindowIndex].X;
                }
            }
        }

        private int baseY {
            get {
                if (selectedWindowIndex == -1) {
                    return 0;
                } else {
                    return windows[selectedWindowIndex].Y;
                }
            }
        }

        #endregion

        #region event handling

        private void Console_Load(object sender, EventArgs e) {
            try {
                Size = SystemInformation.PrimaryMonitorSize;
                Top = 0;
                Left = 0;
                graphics = CreateGraphics();
            } catch (Exception ex) {
            }
        }

        private void VHConsole_Paint(object sender, PaintEventArgs e) {
            Refresh();
        }

        #endregion



    }
}
