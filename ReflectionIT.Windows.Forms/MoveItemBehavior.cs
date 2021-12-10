using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ReflectionIT.Windows.Forms {

    /// <summary>
    /// Summary description for DualListComponent.
    /// </summary>
    public class MoveItemBehavior : Component {

		private Button _actionButton;
		private ListBox _source;
        private bool _doubleClickSupport = true;

		private IButtonControl _acceptButton;

		[DefaultValue(true)]
		public bool DoubleClickSupport {
            get => _doubleClickSupport;
            set {
                if (_doubleClickSupport && Source != null) {
                    Source.DoubleClick -= new EventHandler(Source_DoubleClick);
                    Source.Enter -= new EventHandler(Source_Enter);
                    Source.Leave -= new EventHandler(Source_Leave);
                }

                _doubleClickSupport = value;

                if (value && Source != null) {
                    Source.DoubleClick += new EventHandler(Source_DoubleClick);
                    Source.Enter += new EventHandler(Source_Enter);
                    Source.Leave += new EventHandler(Source_Leave);
                }
            }
        }

        private void Source_DoubleClick(object sender, EventArgs e) {
			ListBoxHelper.MoveItem(Source, Destination);
		}

		private void Source_Enter(object sender, EventArgs e) {
			Form currentForm = Source.FindForm();
			_acceptButton = currentForm.AcceptButton;
			currentForm.AcceptButton = ActionButton;
		}

		private void Source_Leave(object sender, EventArgs e) {
			Source.FindForm().AcceptButton = _acceptButton;
		}

		public Button ActionButton {
            get => _actionButton;
            set {
                if (_actionButton is not null) {
                    _actionButton.Click -= new EventHandler(ActionButton_Click);
                }
                _actionButton = value;
                if (_actionButton is not null) {
                    _actionButton.Click += new EventHandler(ActionButton_Click);
                }
            }
        }

        public ListBox Source {
            get => _source;
            set {
                _source = value;
                DoubleClickSupport = DoubleClickSupport;
            }
        }

        public ListBox Destination { get; set; }

        private void ActionButton_Click(object sender, EventArgs e) {
			ListBoxHelper.MoveItem(Source, Destination);
		}
	}

}
