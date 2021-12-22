using System;

namespace ReflectionIT.Windows.Forms
{
	/// <summary>
	/// DualListActionEventArgs used for a DualList AfterAction event
	/// </summary>
	public class DualListActionEventArgs : System.EventArgs
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="fromIndex">Index of the moved/copied item in the ListBoxFrom.Items</param>
		/// <param name="toIndex">Index of the moved/copied item in the ListBoxTo.Items</param>
		public DualListActionEventArgs(int fromIndex, int toIndex) {
			FromIndex = FromIndex;
			ToIndex = toIndex;
		}

		/// <summary>
		/// Index of the moved/copied item in the ListBoxFrom.Items
		/// </summary>
		public int FromIndex { get; }

		/// <summary>
		/// Index of the moved/copied item in the ListBoxTo.Items
		/// </summary>
		/// <remarks>When the ListBoTo is Sorted it will always be -1</remarks>
		public int ToIndex { get; }

	}
}
