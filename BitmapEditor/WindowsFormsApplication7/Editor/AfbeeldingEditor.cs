using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
	public class AfbeeldingEditor : ScrollableControl
	{
		public AfbeeldingEditor()
		{
			DoubleBuffered = true;
			AutoScroll = true;
			MouseDown += AfbeeldingEditor_MouseDown;
			MouseMove += AfbeeldingEditor_MouseMove;
			MouseUp += AfbeeldingEditor_MouseUp;
			MouseWheel += AfbeeldingEditor_MouseWheel;
			Resize += delegate { Invalidate(); };
		}
		
		Glyph MouseDownGlyph;
		bool isMouseDown = false;
		private void AfbeeldingEditor_MouseDown(object sender, MouseEventArgs e)
		{
			MouseDownGlyph = GetGlyphBelowCursor();
			if (MouseDownGlyph != null)
				if (MouseDownGlyph.GetType() == typeof(RectangularGlyph))
					((RectangularGlyph)MouseDownGlyph).AssignNewHelper();
			isMouseDown = true;
		}

		Mutex mut = new Mutex(false, "DrawIt_AfbeeldingEditor_Moving");
		private void AfbeeldingEditor_MouseMove(object sender, MouseEventArgs e)
		{
			if (mut.WaitOne(TimeSpan.Zero, true))
			{
				if (!isMouseDown)
					Cursor = GetGlyphBelowCursor() == null ? Cursors.Default : Cursors.SizeAll;
				else if (MouseDownGlyph != null)
				{
					if (MouseDownGlyph.GetType() == typeof(RectangularGlyph))
					{
						RectangularGlyph gl = (RectangularGlyph)MouseDownGlyph;
						gl.Helper.SetGlyphLocation(gl.Plaatsing, pt_co(e.Location));
					}
					else
					{
						MouseDownGlyph.Locatie = pt_co(e.Location);
					}
				}
				Invalidate();
				mut.ReleaseMutex();
			}
		}
		Point pt_co(Point pt)
		{
			return new Point((int)((pt.X - AutoScrollPosition.X - 5) / schaal), (int)((pt.Y - AutoScrollPosition.Y - 5) / schaal));
		}
		private void AfbeeldingEditor_MouseUp(object sender, MouseEventArgs e)
		{
			isMouseDown = false;
			if (MouseDownGlyph != null)
				if (MouseDownGlyph.GetType() == typeof(RectangularGlyph))
					((RectangularGlyph)MouseDownGlyph).ClearHelper();
			MouseDownGlyph = null;
		}
		private void AfbeeldingEditor_MouseWheel(object sender, MouseEventArgs e)
		{
			if ((ModifierKeys & Keys.Control) == Keys.Control)
			{
				((HandledMouseEventArgs)e).Handled = true;
				if (e.Delta > 0)
					Schaal *= 1 << (e.Delta / 120);
				else
					Schaal /= 1 << (-e.Delta / 120);
			}
		}


		private void CalcAutoScrollMinSize()
		{
			if (afbeelding == null)
				AutoScrollMinSize = new Size();
			else
				AutoScrollMinSize = new Size((int)(afbeelding.Size.Width * schaal + 5), (int)(afbeelding.Size.Height * schaal + 5));
		}

		#region Afbeelding
		private Afbeelding afbeelding;
		public Afbeelding Afbeelding
		{
			get { return afbeelding; }
			set
			{
				afbeelding = value;
				CalcAutoScrollMinSize();
				Invalidate();
			}
		}
		#endregion
		#region Schaal
		private double schaal = 1;
		public double Schaal
		{
			get { return schaal; }
			set
			{
				if (value == 0) return;
				schaal = value;
				CalcAutoScrollMinSize();
				Invalidate();
			}
		}
		#endregion
		#region Paint
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			LinearGradientBrush br = new LinearGradientBrush(new Point(), new Point(Width, Height), Color.FromArgb(197, 207, 223), Color.FromArgb(220, 229, 242));
			e.Graphics.FillRectangle(br, new Rectangle(0, 0, Width, Height));
			if (afbeelding != null)
			{
				HatchBrush br2 = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray);
				e.Graphics.FillRectangle(br2, 5 + AutoScrollPosition.X, 5 + AutoScrollPosition.Y, (int)(schaal * afbeelding.Size.Width), (int)(schaal * afbeelding.Size.Height));
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (afbeelding != null)
			{
				Bitmap bmp = afbeelding.GetPreview(false);
				Rectangle r = new Rectangle(5 + AutoScrollPosition.X, 5 + AutoScrollPosition.Y, (int)(schaal * bmp.Width), (int)(schaal * bmp.Height));
				e.Graphics.DrawImage(bmp, new PointF[] {
					new PointF(r.X, r.Y),
					new PointF(r.X + r.Width, r.Y),
					new PointF(r.X, r.Y + r.Height)
				});

				foreach (ShapeLayer l in afbeelding.Layers.Where(T => T.GetType() == typeof(ShapeLayer)).Select(T => (ShapeLayer)T))
					foreach (Vorm v in l.Vormen.Where(T => T.Geselecteerd))
						foreach (Glyph gl in v.Glyphs)
						{
							gl.Draw(e.Graphics, new Point(5 + AutoScrollPosition.X + (int)(gl.Locatie.X * schaal), 5 + AutoScrollPosition.Y + (int)(gl.Locatie.Y * schaal)));
						}
			}
		}
		#endregion
		#region Glyphs
		private Glyph GetGlyphBelowCursor()
		{
			if (afbeelding == null)
			{
				return null;
			}
			else
			{
				Point p = PointToClient(MousePosition);
				foreach (ShapeLayer l in afbeelding.Layers.Where(T => T.GetType() == typeof(ShapeLayer)).Select(T => (ShapeLayer)T))
				{
					foreach (Vorm v in l.Vormen.Where(T => T.Geselecteerd).Reverse<Vorm>())
					{
						foreach (Glyph gl in v.Glyphs)
						{
							// 5 + dX + x * schaal
							// dX + x * schaal <= p.X <= dX + x * schaal + 10
							if ((AutoScrollPosition.X + gl.Locatie.X * schaal <= p.X) & (p.X <= AutoScrollPosition.X + gl.Locatie.X * schaal + 10)
							 & (AutoScrollPosition.Y + gl.Locatie.Y * schaal <= p.Y) & (p.Y <= AutoScrollPosition.Y + gl.Locatie.Y * schaal + 10))
								return gl;
						}
					}
				}
				return null;
			}
		}
		#endregion
	}
}