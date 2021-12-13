using System;
using System.Windows.Forms;

namespace ReflectionIT.Windows.Forms {

    /// <summary>
    /// Make the current Cursor a WaitCursor and restore it on Dispose
    /// <code>
    /// using (new WaitCursor()) {
    ///     // long running operation, for example:
    ///     System.Threading.Thread.Sleep(1000);
    /// }
    /// </code>
    /// </summary>
    public sealed class WaitCursor : IDisposable {

        private readonly Cursor _prev;


        /// <summary>
        /// Save the current cursor and then set it to WaitCursor
        /// </summary>
        public WaitCursor() {
            _prev = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Restore the previous cursor
        /// </summary>
        public void Dispose() {
            Cursor.Current = _prev;
        }
    }

}
