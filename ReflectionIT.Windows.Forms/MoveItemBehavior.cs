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
        private IButtonControl _acceptButton;

        [DefaultValue(true)]
        public bool DoubleClickSupport { get; set; } = true;

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
                if (Source is not null) {
                    Source.DoubleClick -= new EventHandler(Source_DoubleClick);
                    Source.Enter -= new EventHandler(Source_Enter);
                    Source.Leave -= new EventHandler(Source_Leave);
                }
                _source = value;
                if (Source is not null) {
                    Source.DoubleClick += new EventHandler(Source_DoubleClick);
                    Source.Enter += new EventHandler(Source_Enter);
                    Source.Leave += new EventHandler(Source_Leave);
                }
            }
        }

        public ListBox Destination { get; set; }

        private void ActionButton_Click(object sender, EventArgs e) {
            ListBoxHelper.MoveItem(Source, Destination);
        }

        private void Source_DoubleClick(object sender, EventArgs e) {
            if (DoubleClickSupport) {
                ListBoxHelper.MoveItem(Source, Destination);
            }
        }

        private void Source_Enter(object sender, EventArgs e) {
            if (DoubleClickSupport) {
                Form currentForm = Source.FindForm();
                _acceptButton = currentForm.AcceptButton;
                currentForm.AcceptButton = ActionButton;
            }
        }

        private void Source_Leave(object sender, EventArgs e) {
            if (DoubleClickSupport) {
                Source.FindForm().AcceptButton = _acceptButton;
            }
        }
    }

}
