using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
	public interface IRectangular
	{
		Point Location { get; set; }
		Size Size { get; set; }
		int Angle { get; set; }
	}

	public class RectangularHelper
	{
		public RectangularHelper(IRectangular rect)
		{
			this.rect = rect;
			x = rect.Location.X;
			y = rect.Location.Y;
			w = rect.Size.Width;
			h = rect.Size.Height;
			hoek = rect.Angle;
		}

		private IRectangular rect;
		private double x, y;
		private double w, h;
		private double hoek;

		public void SetGlyphLocation(AnchorStyles plaatsing, Point GlyphLocation)
		{
			if ((plaatsing & AnchorStyles.Left) == AnchorStyles.Left)
			{
				if ((plaatsing & AnchorStyles.Top) == AnchorStyles.Top)
				{
					#region Rechter-onderhoek berekenen -> v
					Vector v = new Vector(x, y);
					v.AddVector(w, this.hoek);
					v.AddVector(h, this.hoek + 90);
					#endregion
					#region Nieuwe breedte-hoogte berekenen -> dx,dy,hoek
					double dx = v.X - GlyphLocation.X;
					double dy = v.Y - GlyphLocation.Y;
					double hoek = this.hoek * Math.PI / 180;
					#endregion
					#region calculate internals
					w = dx * Math.Cos(hoek) + dy * Math.Sin(hoek);
					h = -dx * Math.Sin(hoek) + dy * Math.Cos(hoek);
					x = GlyphLocation.X;
					y = GlyphLocation.Y;
					#endregion
					#region Apply calculations
					rect.Location = new Point((int)(v.X - dx), (int)(v.Y - dy));
					rect.Size = new Size((int)w, (int)h);
					#endregion
				}
				else if ((plaatsing & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				{
					#region Rechter-bovenhoek berekenen -> v
					Vector v = new Vector(x, y);
					v.AddVector(w, this.hoek);
					#endregion
					#region Nieuwe breedte-hoogte berekenen -> dx,dy,hoek
					double dx = v.X - GlyphLocation.X;
					double dy = v.Y - GlyphLocation.Y;
					double hoek = this.hoek * Math.PI / 180;
					#endregion
					#region calculate internals
					w = dx * Math.Cos(hoek) + dy * Math.Sin(hoek);
					h = dx * Math.Sin(hoek) - dy * Math.Cos(hoek);
					x = GlyphLocation.X + h * Math.Sin(hoek);
					y = GlyphLocation.Y - h * Math.Cos(hoek);
					#endregion
					#region Apply calculations
					rect.Location = new Point((int)x, (int)y);
					rect.Size = new Size((int)w, (int)h);
					#endregion
				}
				else
				{
				}
			}
			else if ((plaatsing & AnchorStyles.Right) == AnchorStyles.Right)
			{
				if ((plaatsing & AnchorStyles.Top) == AnchorStyles.Top)
				{
					#region Linker-onderhoek berekenen -> v
					Vector v = new Vector(x, y);
					v.AddVector(h, this.hoek + 90);
					#endregion
					#region Nieuwe breedte-hoogte berekenen -> dx,dy,hoek
					double dx = v.X - GlyphLocation.X;
					double dy = v.Y - GlyphLocation.Y;
					double hoek = rect.Angle * Math.PI / 180;
					#endregion
					#region Calculate internals
					w = -dx * Math.Cos(hoek) - dy * Math.Sin(hoek);
					h = -dx * Math.Sin(hoek) + dy * Math.Cos(hoek);
					x = GlyphLocation.X - w * Math.Cos(hoek);
					y = GlyphLocation.Y - w * Math.Sin(hoek);
					#endregion
					#region Apply Calculations
					rect.Location = new Point((int)x, (int)y);
					rect.Size = new Size(Convert.ToInt32(w), Convert.ToInt32(h));
					#endregion
				}
				else if ((plaatsing & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				{
					#region Linker-bovenhoek berekenen -> v
					Vector v = new Vector(x, y);
					#endregion
					#region Nieuwe breedte-hoogte berekenen -> dx,dy,hoek
					double dx = v.X - GlyphLocation.X;
					double dy = v.Y - GlyphLocation.Y;
					double hoek = rect.Angle * Math.PI / 180;
					#endregion
					#region Calculate internals
					w = -dx * Math.Cos(hoek) - dy * Math.Sin(hoek);
					h = dx * Math.Sin(hoek) - dy * Math.Cos(hoek);
					#endregion
					#region Apply Calculations
					rect.Size = new Size(Convert.ToInt32(w), Convert.ToInt32(h));
					#endregion
				}
				else
				{
				}
			}
			else
			{
				if ((plaatsing & AnchorStyles.Top) == AnchorStyles.Top)
				{
				}
				else if ((plaatsing & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				{

				}
				else
				{
				}
			}
		}
	}
}
