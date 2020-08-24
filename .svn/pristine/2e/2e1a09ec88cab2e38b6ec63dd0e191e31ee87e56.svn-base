using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VH.Engine.Persistency;

namespace VH.Engine.Display {

    /// <summary>
    /// A rectangular region on the an IConsole
    /// </summary>
    public class Window: AbstractPersistent {

        #region fields 

        protected int x;
        protected int y;
        protected int width;
        protected int height;
        protected IConsole console;

        #endregion

        #region constructors

        public Window() { }

        public Window(int x, int y, int width, int height, IConsole console) {

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.console = console;
        }

        #endregion

        #region properties

        public int X
        {
            get { return x; }
        }

        public int Y {
            get { return y; }
        }

        public int Width {
            get { return width; }
        }

        public int Height {
            get { return height; }
        }

        public IConsole Console {
            get { return console; }
            set { console = value; }
        }

        #endregion

        #region public methods

        public override void FromXml(XmlElement element) {
            base.FromXml(element);
            x = GetIntAttribute("x");
            y = GetIntAttribute("y");
            width = GetIntAttribute("width");
            height = GetIntAttribute("height");
        }

        public override XmlElement ToXml(string name, XmlDocument doc) {
            XmlElement element = base.ToXml(name, doc);
            AddAttribute("x", x);
            AddAttribute("y", y);
            AddAttribute("width", width);
            AddAttribute("height", height);
            return element;
        }

        public virtual void Write(char c, int x, int y) {
            console.Write(c, x + this.x, y + this.y);
        }

        public virtual void Write(char c) {
            console.Write(c);
        }

        public virtual void GoTo(int x, int y) {
            console.GoTo(x + this.x, y + this.y);
        }        

        #endregion

    }
}
