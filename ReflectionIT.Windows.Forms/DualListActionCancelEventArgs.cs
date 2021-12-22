using System;

namespace ReflectionIT.Windows.Forms {
    /// <summary>
    /// DualListActionCancelEventArgs used to Cancel or Change a DualList BeforeAction event
    /// </summary>
    public class DualListActionCancelEventArgs : System.ComponentModel.CancelEventArgs {

        public DualListActionCancelEventArgs(int index) : base() {
            Index = index;
        }

        /// <summary>
        /// Returns the index of the Item 
        /// </summary>
        public int Index { get; }
    }
}
