using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    public enum MenuResult {
        None,
        OK,
        Cancel
    }

    /// <summary>
    /// Represents a user-selectable menu
    /// </summary>
    public abstract class Menu {

        #region constants

        protected char ESC = (char)27;


        #endregion

        #region fields

        protected object[] objects;
        private MenuResult result = MenuResult.None;
        private bool multiselect = false;
        private int selectedIndex;
        private List<object> selectedItems = new List<object>();

        #endregion

        #region properties

        /// <summary>
        /// Indicates the result of showing this menu
        /// </summary>
        public MenuResult Result {
            get { return result; }
        }

        /// <summary>
        /// Gets otr sets a value indicating whether this menu should be displayed as multi-select
        /// </summary>
        public bool Multiselect {
            get { return multiselect; }
            set { multiselect = value; }
        }

        /// <summary>
        /// Gets or sets an index of the object selected by the user
        /// </summary>
        public int SelectedIndex {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        /// <summary>
        /// Gets the object selected by the user
        /// </summary>
        public object SelectedItem {
            get { return objects[selectedIndex]; }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Displays the menu and lets the user select a value.
        /// </summary>
        /// <returns>MenuResult.OK if the user has selected a value
        /// MenuResult.Cancel if the user has escaped from this menu</returns>
        public abstract MenuResult ShowMenu();

        #endregion

    }
}
