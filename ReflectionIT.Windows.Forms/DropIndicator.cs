using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace ReflectionIT.Windows.Forms
{
	/// <summary>
	/// Summary description for DropIndicator.
	/// </summary>
	[ToolboxItem(false)]
	public class DropIndicator : Control 
	{

		public DropIndicator() {
			this.SetStyle(ControlStyles.UserPaint
						| ControlStyles.SupportsTransparentBackColor
						| ControlStyles.AllPaintingInWmPaint
						| ControlStyles.DoubleBuffer
						| ControlStyles.ResizeRedraw, true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			Graphics g = e.Graphics;
			using (Pen p = new Pen(this.ForeColor)) {
				p.Width = 1;
				p.DashStyle = DashStyle.Dash;
				g.DrawLine(p, new Point(0, 0), new Point(this.Width, 0));
			}

			base.OnPaint (e);
		}

	}
}
