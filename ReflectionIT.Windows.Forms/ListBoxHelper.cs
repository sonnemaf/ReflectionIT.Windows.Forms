using System.Windows.Forms;

namespace ReflectionIT.Windows.Forms {
    public static class ListBoxHelper {

		public static void MoveItem(ListBox source, ListBox destination) {
			int index = source.SelectedIndex;
			if (index > -1) {
				destination.SelectedIndex = destination.Items.Add(source.SelectedItem);
				source.Items.RemoveAt(index);
				source.SelectedIndex = (index > source.Items.Count - 1) ? index - 1 : index;
			}
		}

	}

}
