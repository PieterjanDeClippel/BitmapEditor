using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
	public class RectangularGlyph : Glyph
	{
		public RectangularGlyph(IRectangular vorm, AnchorStyles plaatsing)
		{
			this.vorm = vorm;
			this.plaatsing = plaatsing;
		}
		#region Vorm
		private IRectangular vorm;
		public IRectangular Vorm
		{
			get { return vorm; }
		}
		#endregion
		#region Plaatsing
		private AnchorStyles plaatsing;
		public AnchorStyles Plaatsing
		{
			get { return plaatsing; }
		}
		#endregion
		#region Locatie
		public override Point Locatie
		{
			get
			{
				Vector vector = new Vector(vorm.Location);
				if ((plaatsing & AnchorStyles.Left) == AnchorStyles.Left)
				{ }
				else if ((plaatsing & AnchorStyles.Right) == AnchorStyles.Right)
				{
					vector.AddVector(vorm.Size.Width, vorm.Angle);
				}
				else
				{
					vector.AddVector(vorm.Size.Width / 2, vorm.Angle);
				}

				if ((plaatsing & AnchorStyles.Top) == AnchorStyles.Top)
				{ }
				else if ((plaatsing & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				{
					vector.AddVector(vorm.Size.Height, vorm.Angle + 90);
				}
				else
				{
					vector.AddVector(vorm.Size.Height / 2, vorm.Angle + 90);
				}
				return vector;
				/*};

				return convert(plaatsing);*/
			}
			set
			{
				// keep bound values stored as doubles instead of ints
				// RectangularHelper helper = vorm.Helper;
				// helper.SetGlyphLocation(plaatsing, value);
			}
		}

		#endregion
		#region Helper
		private RectangularHelper helper;
		public void AssignNewHelper()
		{
			helper = new RectangularHelper(vorm);
		}
		public void ClearHelper()
		{
			helper = null;
		}
		public RectangularHelper Helper
		{
			get { return helper; }
		}
		#endregion

		public override void Draw(Graphics gr, Point pt)
		{
			Rectangle r = new Rectangle(pt, new Size());
			r.Inflate(5, 5);
			gr.FillRectangle(Brushes.LightGreen, r);
			gr.DrawRectangle(Pens.Black, r);
		}
	}
}
