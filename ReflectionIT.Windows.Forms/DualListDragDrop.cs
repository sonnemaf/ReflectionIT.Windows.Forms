using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace ReflectionIT.Windows.Forms {

	/// <summary>
	/// Action enum for a DualListDragDrop component
	/// </summary>
	public enum DualListDragDropAction {
		Move, 
		Copy, 
		MoveAndCopy
	}


	/// <summary>
	/// The DualListDragDrop component handles the Drag and Drop of the items 
	/// between two listboxes.
	/// 
	/// You can place it on the form as a sort of "invisible control". 
	/// Next you set its behaviour properties: ListBoxFrom and ListBoxTo. 
	/// 
	/// Created by: Fons Sonnemans, Reflection IT
	/// 
	/// For questions and comments: Fons.Sonnemans@reflectionit.nl
	/// </summary>
	[ToolboxBitmap(typeof(Bitmap))] // Set Projects Default Namespace to current NameSpace. Add 16x16x16 bitmap to the project, set its BuildAction to: Embedded Resource
	[DefaultProperty("ListBoxFrom")]
#if NET48
    [Designer(typeof(ReflectionIT.Windows.Forms.Design.DualListDragDropDesigner))]
#endif
    public class DualListDragDrop : System.ComponentModel.Component {

		private DropIndicator _dropper;
		private DateTime _nextScroll = DateTime.Now;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
		public DualListDragDrop(System.ComponentModel.IContainer container) {
			// Required for Windows.Forms Class Composition Designer support
			container.Add(this);
			_dropper = new DropIndicator();
			_dropper.Visible = false;
		}

        /// <summary>
        /// Constructor
        /// </summary>
		public DualListDragDrop() {
			_dropper = new DropIndicator();
			_dropper.Visible = false;
		}

		// Controls
		private ListBox _from;
        private ListBox _to;

        // Extra Fields
        private bool _mouseDown = false;
        private int _indexOfItemUnderMouseToDrop;
        private int _indexOfItemUnderMouseToDrag;
        private Point _screenOffset;
        private Rectangle _dragBoxFromMouseDown;

		/// <summary>
		/// Returns/sets the ListBox where the items are moved/copied from
		/// </summary>
		[Category("Behavior")]
		[Description("ListBox which the item(s) are dragged from")]
		public virtual ListBox From {
			get { return _from; }
			set { 
				if (_from != value) {

					_from = value;
					if (_from is not null) {
						_from.MouseDown += new MouseEventHandler(From_MouseDown);
						_from.MouseUp += new MouseEventHandler(From_MouseUp);
						_from.MouseMove += new MouseEventHandler(From_MouseMove);
						_from.QueryContinueDrag += new QueryContinueDragEventHandler(From_QueryContinueDrag);
						_from.DragOver +=new DragEventHandler(From_DragOver);
					}
				}
			}
		}

		/// <summary>
		/// Returns/sets the ListBox where the items are moved/copied to
		/// </summary>
		[Category("Behavior")]
		[Description("ListBox where the item(s) are dropped to")]
		public virtual ListBox To {
			get { return _to; }
			set { 
				if (_to != value) {
					_to = value;
					if (_to is not null) {
						_to.AllowDrop = true;
						_to.DragDrop += new DragEventHandler(To_DragDrop);
						_to.DragEnter += new DragEventHandler(To_DragEnter);
						_to.DragLeave += new EventHandler(To_DragLeave);
						_to.DragOver += new DragEventHandler(To_DragOver);
					}
				}
			}
		}

        /// <summary>
        /// Returns/sets the action that is executed by the Drag and Drop
        /// </summary>
        [Category("Behavior")]
        [Description("The action that is executed by the Drag and Drop")]
        [DefaultValue(DualListDragDropAction.Move)]
        public DualListDragDropAction Action { get; set; } = DualListDragDropAction.Move;

        /// <summary>
        /// Returns/sets whether a drop indicator line is shown
        /// </summary>
        [Category("Behavior")]
        [Description("Determines whether a drop indicator line is shown.")]
        [DefaultValue(true)]
        public bool ShowDropIndicator { get; set; } = true;

        /// <summary>
        /// Returns/sets the IndicatorColor used to display the indicator line in the 'To' ListBox.
        /// </summary>
        [Category("Appearance")]
        [Description("The IndicatorColor is used to display the indicator line in the 'To' ListBox.")]
        public Color IndicatorColor { get; set; } = Color.Red;

        protected bool ShouldSerializeIndicatorColor() {
			return IndicatorColor != Color.Red;
		}

		protected void ResetIndicatorColor() {
			IndicatorColor = Color.Red;
		}

		/// <summary>
		/// Detect mouse down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void From_MouseDown(object sender, MouseEventArgs e) {
			try {
				_indexOfItemUnderMouseToDrag = From.IndexFromPoint(e.X, e.Y);
				_mouseDown = _indexOfItemUnderMouseToDrag > -1;
				Size dragSize = SystemInformation.DragSize;

				// Create a rectangle using the DragSize, with the mouse position being
				// at the center of the rectangle.
				_dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
					e.Y - (dragSize.Height / 2)), dragSize);
			} catch {
				_mouseDown = false;
			}
		}

		/// <summary>
		/// Start Drag and Drop, remove Item when finished
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void From_MouseMove(object sender, MouseEventArgs e) {
			if (_mouseDown && (e.Button & MouseButtons.Left) == MouseButtons.Left) {

				if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y)) {

					// Start D&D
					_screenOffset = SystemInformation.WorkingArea.Location;
					DragDropEffects dropEffect = DragDropEffects.None;
					try {
						dropEffect = From.DoDragDrop(_indexOfItemUnderMouseToDrag, DragDropEffects.All | DragDropEffects.Link);
					} 
					catch (System.Exception) {
						// ignore errors
					}

					_mouseDown = false;
				}
			}
		}

		/// <summary>
		/// Set the Cursor, show the 'dropper' and scroll the listbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void To_DragOver(object sender, DragEventArgs e) {
			if (_mouseDown) {

				// Cursor
				SetCursor(e);

				// Drop Indicator
				if (ShowDropIndicator) {
					_dropper.Visible = true;
					_indexOfItemUnderMouseToDrop = To.IndexFromPoint(To.PointToClient(new Point(e.X, e.Y + 4)));
					int y = 1;
					if (_indexOfItemUnderMouseToDrop > -1) {
						Rectangle rect = To.GetItemRectangle(_indexOfItemUnderMouseToDrop);
						y = rect.Top + 1;
						if (y == 1 && To.TopIndex > 0) {
							// Scroll Up
							if (DateTime.Now > _nextScroll) {
								To.TopIndex -= 1;
								_nextScroll = DateTime.Now.AddMilliseconds(100);
							} 
							return; // Exit!
						} else {
							if (rect.Bottom + 1 > To.DisplayRectangle.Height) {
								// Scroll Down
								if (DateTime.Now > _nextScroll) {
									To.TopIndex += 1;
									_nextScroll = DateTime.Now.AddMilliseconds(100);
								}
							}
						}
					} else {
						if (To.Items.Count > 0) {
							// Scrolled to last item, select botom
							Rectangle rect = To.GetItemRectangle(To.Items.Count - 1);
							y = rect.Bottom + 1;
						} else {
							if (!To.DisplayRectangle.Contains(To.PointToClient(new Point(e.X, e.Y)))) {
								return;
							}
						}
					}

					// Set the Top of the dropper
					_dropper.Top = To.Top + y + 1;
				}
			}
		}

		/// <summary>
		/// Hide the drop indicator
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void To_DragLeave(object sender, EventArgs e) {
			_dropper.Visible = false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void To_DragDrop(object sender, DragEventArgs e) {
			if (_mouseDown) {
				_dropper.Visible = false;
				object item = From.Items[_indexOfItemUnderMouseToDrag];
				
				// Insert the item.
				if (ShowDropIndicator && _indexOfItemUnderMouseToDrop != ListBox.NoMatches) {
					To.Items.Insert(_indexOfItemUnderMouseToDrop, item);
					To.SelectedIndex = _indexOfItemUnderMouseToDrop;
				} else {
					To.SelectedIndex = To.Items.Add(item);
				}

				// Move position, not from listbox
				if (From == To) {
					if (_indexOfItemUnderMouseToDrop != ListBox.NoMatches && 
						_indexOfItemUnderMouseToDrag > _indexOfItemUnderMouseToDrop) {
						_indexOfItemUnderMouseToDrag++;
					}
					From.Items.RemoveAt(_indexOfItemUnderMouseToDrag);
					e.Effect = DragDropEffects.None; // Cancels RemoveAt() in MouseMove eventhandler
				} else {
					// Finished
					if (e.Effect == DragDropEffects.Move) {
						From.Items.RemoveAt(_indexOfItemUnderMouseToDrag);
						From.SelectedIndex = From.Items.Count > _indexOfItemUnderMouseToDrag ? _indexOfItemUnderMouseToDrag : From.Items.Count - 1;
                    }
				}
				_mouseDown = false;
			}
		}

		/// <summary>
		/// Cancel the drag if the mouse moves off the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void From_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) {
			ListBox lb = sender as ListBox;
	
			if (lb is not null) {

				Form f = lb.FindForm();

				// Cancel the drag if the mouse moves off the form. The screenOffset
				// takes into account any desktop bands that may be at the top or left
				// side of the screen.
				if (((Control.MousePosition.X - _screenOffset.X) < f.DesktopBounds.Left) || 
					((Control.MousePosition.X - _screenOffset.X) > f.DesktopBounds.Right) || 
					((Control.MousePosition.Y - _screenOffset.Y) < f.DesktopBounds.Top) || 
					((Control.MousePosition.Y - _screenOffset.Y) > f.DesktopBounds.Bottom)) {

					// Cancel D&D
					_mouseDown = false;
					_dropper.Visible = false;
					e.Action = DragAction.Cancel;
				} else {
					//e.Action = DragAction.Continue;
				}
			}
		}

		/// <summary>
		/// Stop Drag and Drop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void From_MouseUp(object sender, MouseEventArgs e) {
			_mouseDown = false;
			_dropper.Visible = false;
		}

		/// <summary>
		/// Add the 'dropper' to the parent of ListBoxTo
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void To_DragEnter(object sender, DragEventArgs e) {
			if (_mouseDown) {
				if (ShowDropIndicator) {
					Control.ControlCollection controls = To.Parent.Controls;
					if (!controls.Contains(_dropper)) {
						_dropper.Left = To.Left - 5;
						_dropper.Height = 1;
						_dropper.Width = To.Width + 10;
						_dropper.Enabled = false;
						_dropper.Top = -500;
						_dropper.Anchor = To.Anchor;
						_dropper.ForeColor = IndicatorColor;
						controls.Add(_dropper);
						controls.SetChildIndex(_dropper, 0);
					}
				}
			}
		}

		/// <summary>
		/// Set the cursor for the From ListBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void From_DragOver(object sender, DragEventArgs e) {
			if (_mouseDown) {
				SetCursor(e);
			}

		}

		/// <summary>
		/// Set the Cursor depending on the Action and the KeyState
		/// </summary>
		/// <param name="e"></param>
		private void SetCursor(DragEventArgs e) {
			// Cursor
			if (Action == DualListDragDropAction.Move) {
				e.Effect = DragDropEffects.Move;
			} else {
				if (Action == DualListDragDropAction.Copy) {
					e.Effect = DragDropEffects.Copy;
				} else {
					// MoveAndCopy
					if ((e.KeyState & 8) == 8) {
						// 8 = Ctrl
						e.Effect = DragDropEffects.Copy;
					} else {
						e.Effect = DragDropEffects.Move;
					}
				}
			}
		}

	}
}
