using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace ReflectionIT.Windows.Forms {

    /// <summary>
    /// BeforeActionHandler
    /// </summary>
    public delegate void BeforeActionHandler(object sender, DualListActionCancelEventArgs e);

    /// <summary>
    /// AfterActionHandler
    /// </summary>
    public delegate void AfterActionHandler(object sender, DualListActionEventArgs e);


    /// <summary>
    /// The DualList component handles the movement of items between 2 listboxes.
    /// 
    /// The DualList component takes care of the following features:
    /// - Actions: Move Selected, Copy Selected, Move All, Copy All 
    /// - DoubleClick support, inclusive (re)setting the Default button 
    /// - Select next item 
    /// - Enable and disable buttons 
    /// - MultiSelect support 
    /// 
    /// You can place it on the form as a sort of "invisible control". 
    /// Next you set its behaviour properties: Action, Button, ListBoxFrom and ListBoxTo. 
    /// Optionally you can set the DoubleClickSupport and AutoDisableButton properties. 
    /// 
    /// Created by: Fons Sonnemans, Reflection IT
    /// 
    /// For questions and comments: Fons.Sonnemans@reflectionit.nl
    /// </summary>
    [ToolboxBitmap(typeof(Bitmap))] // Set Projects Default Namespace to current NameSpace. Add 16x16x16 bitmap to the project, set its BuildAction to: Embedded Resource
    [DefaultProperty("Button")]
    [DefaultEvent("BeforeAction")]
#if NET48
    [Designer(typeof(ReflectionIT.Windows.Forms.Design.DualListDesigner))]
#endif
    public class DualList : System.ComponentModel.Component {
        // Controls
        private ListBox _from;
        private ListBox _to;
        private Button _button;

        // Properties
        private DualListAction _action = DualListAction.MoveSelected;
        private bool _autoDisableButton = false;
        private bool _busy = false;

        // helpers
        protected IButtonControl PreviousAcceptButton { get; set; }

        /// <summary>
        /// Before event
        /// </summary>
        [Category("Action")]
        [Description("Before event")]
        public event BeforeActionHandler BeforeAction;

        /// <summary>
        /// After event
        /// </summary>
        [Category("Action")]
        [Description("After event")]
        public event AfterActionHandler AfterAction;

        public DualList(System.ComponentModel.IContainer container) {
            container.Add(this);
        }

        public DualList() {
        }

        protected virtual void OnBeforeAction(DualListActionCancelEventArgs e) {
            BeforeAction?.Invoke(this, e);
        }

        protected virtual void OnAfterAction(DualListActionEventArgs e) {
            AfterAction?.Invoke(this, e);
        }

        /// <summary>
        /// Button Click EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ButtonClick(object sender, System.EventArgs e) {
            Click();
        }

        /// <summary>
        /// ListBoxFrom Leave EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void FromEnter(object sender, System.EventArgs e) {
            if (DoubleClickSupport) {
                // Set the AcceptButton of the Form
                Form f = _from.FindForm();
                PreviousAcceptButton = f.AcceptButton;
                f.AcceptButton = _button;
            }
        }

        /// <summary>
        /// ListBoxFrom Leave EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void FromLeave(object sender, System.EventArgs e) {
            if (DoubleClickSupport) {
                // Reset the AcceptButton of the Form
                Form f = _from.FindForm();
                f.AcceptButton = PreviousAcceptButton;
            }
        }

        /// <summary>
        /// ListBoxFrom DoubleClick EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void FromDoubleClick(object sender, System.EventArgs e) {
            if (DoubleClickSupport) {
                Click();
            }
        }

        /// <summary>
        /// ListBoxFrom SelectedIndexChanged EventHandler, Enables/Disables the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SelectedIndexChanged(object sender, System.EventArgs e) {
            EnableDisable();
        }

        /// <summary>
        /// Returns/sets the ListBox where the items are moved/copied from
        /// </summary>
        [Category("Behavior")]
        [Description("ListBox which the item(s) are moved/copied from")]
        public virtual ListBox From {
            get { return _from; }
            set {
                _from = value;
                if (_from is not null) {
                    _from.Enter += new System.EventHandler(this.FromEnter);
                    _from.Leave += new System.EventHandler(this.FromLeave);
                    _from.DoubleClick += new System.EventHandler(this.FromDoubleClick);
                    _from.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
                }
                EnableDisable();
            }
        }

        /// <summary>
        /// Returns/sets the ListBox where the items are moved/copied to
        /// </summary>
        [Category("Behavior")]
        [Description("ListBox where the item(s) are moved/copied to")]
        public virtual ListBox To {
            get { return _to; }
            set { _to = value; }
        }

        /// <summary>
        /// Returns/sets the Button which Click event is handled
        /// </summary>
        [Category("Behavior")]
        [Description("Button that activates the Action")]
        public virtual Button Button {
            get { return _button; }
            set {
                _button = value;
                if (_button is not null) {
                    _button.Click += new System.EventHandler(this.ButtonClick);
                    EnableDisable();
                }
            }
        }

        /// <summary>
        /// Returns/sets the DoubleClickSupport
        /// </summary>
        [Category("Behavior")]
        [Description("Returns/sets the DoubleClickSupport")]
        [DefaultValue(true)]
        public bool DoubleClickSupport { get; set; } = true;

        /// <summary>
        /// Returns/sets whether button should be disabled when no Item in the 'From' ListBox is selected.
        /// </summary>
        [Category("Behavior")]
        [Description("Disables the button when no Item in the 'From' ListBox is selected.")]
        [DefaultValue(false)]
        public virtual bool AutoDisableButton {
            get { return _autoDisableButton; }
            set {
                _autoDisableButton = value;
                if (value) {
                    EnableDisable();
                } else {
                    if (Button is not null) {
                        Button.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Returns/sets the Action which is executed when the Button is clicked
        /// </summary>
        [Category("Behavior")]
        [Description("Returns/sets the Action which is executed when the Button is clicked")]
        [DefaultValue(DualListAction.MoveSelected)]
        public virtual DualListAction Action {
            get { return _action; }
            set { _action = value; }
        }

        /// <summary>
        /// Move the selected Item from ListBoxFrom to ListBoxTo
        /// </summary>
        public virtual void MoveSelected() {
            _busy = true;
            DoSelected(DualListAction.MoveSelected);
            _busy = false;
            EnableDisable();
        }

        /// <summary>
        /// Copy the selected Item from ListBoxFrom to ListBoxTo
        /// </summary>
        public virtual void CopySelected() {
            _busy = true;
            DoSelected(DualListAction.CopySelected);
            _busy = false;
            EnableDisable();
        }

        /// <summary>
        /// Move all Items from ListBoxFrom to ListBoxTo
        /// </summary>
        public virtual void MoveAll() {
            _busy = true;
            for (int t = 0; t < _from.Items.Count; t++) {
                DoAction(false, t);
            }
            _from.Items.Clear();
            _busy = false;
            EnableDisable();
        }

        /// <summary>
        /// Copy all Items from ListBoxFrom to ListBoxTo
        /// </summary>
        public virtual void CopyAll() {
            _busy = true;
            for (int t = 0; t < _from.Items.Count; t++) {
                DoAction(false, t);
            }
            _busy = false;
            EnableDisable();
        }

        /// <summary>
        /// Decide what to do
        /// </summary>
        protected virtual void Click() {
            if (_button is not null && From is not null && To is not null) {
                switch (Action) {
                    case DualListAction.MoveSelected:
                        MoveSelected();
                        break;
                    case DualListAction.MoveAll:
                        MoveAll();
                        break;
                    case DualListAction.CopySelected:
                        CopySelected();
                        break;
                    case DualListAction.CopyAll:
                        CopyAll();
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        protected virtual void DoSelected(DualListAction action) {
            To.BeginUpdate();
            From.BeginUpdate();
            if (_from.SelectionMode == SelectionMode.One) {
                // Single Select
                if (_from.SelectedIndex > -1) {
                    int i = _from.SelectedIndex;
                    int newIndex = -1;
                    if (action == DualListAction.MoveSelected) {
                        newIndex = DoAction(true, i);
                    } else {
                        newIndex = DoAction(false, i);
                        i += 1;
                    }

                    // select next/previous item
                    if (i >= (_from.Items.Count)) {
                        i = _from.Items.Count - 1;
                    }

                    _from.SelectedIndex = i;

                    _to.SelectedIndex = newIndex;
                }
            } else {
                // MultiSelect

                // Add all selected items
                foreach (int x in _from.SelectedIndices) {
                    DoAction(false, x);
                }

                // Remove or de-select all selected items
                int i;
                if ((_from.SelectedIndices.Count) > 0) {
                    i = _from.SelectedIndices[_from.SelectedIndices.Count - 1] + 1;
                } else {
                    return;
                }

                for (int t = _from.Items.Count - 1; t >= 0; t--) {
                    if (action == DualListAction.MoveSelected) {
                        if (_from.SelectedIndices.Contains(t)) {
                            i = t;
                            _from.Items.RemoveAt(t);
                        }
                    } else {
                        // de-select
                        _from.SetSelected(t, false);
                    }
                }

                // select next/previous item
                if (i >= (_from.Items.Count)) {
                    i = _from.Items.Count - 1;
                }
                if (i > -1) {
                    _from.SetSelected(i, true);
                }

            }
            To.EndUpdate();
            From.EndUpdate();
        }

        /// <summary>
        /// Enable/Disable button
        /// </summary>
        protected virtual void EnableDisable() {
            if (AutoDisableButton && !_busy && Button is not null && From is not null) {
                if ((Action == DualListAction.MoveSelected) || (Action == DualListAction.CopySelected)) {
                    if (Button.Enabled != (From.SelectedIndex > -1)) {
                        Button.Enabled = (From.SelectedIndex > -1);
                    }
                } else {
                    if (Button.Enabled != (From.Items.Count > 0)) {
                        Button.Enabled = (From.Items.Count > 0);
                    }
                }
            }
        }

        /// <summary>
        /// Move/Copy the given Item and raise the Before and After events.
        /// </summary>
        /// <param name="remove">True is Remove item</param>
        /// <param name="index">Index of Item to Move/Copy</param>
        protected virtual int DoAction(bool remove, int index) {
            DualListActionCancelEventArgs e = new DualListActionCancelEventArgs(index);

            // Raise Before event
            OnBeforeAction(e);

            // If not canceled
            if (!e.Cancel) {
                // Add the item
                int newIndex = _to.Items.Add(_from.Items[index]);

                // Remove the item
                if (remove) {
                    _from.Items.RemoveAt(index);
                }

                // Raise After event
                OnAfterAction(new DualListActionEventArgs(index, newIndex));

                return newIndex;
            } else {
                return -1;
            }
        }

    }
}
