using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class PuntGlyph : Glyph
	{
		public PuntGlyph(Punt p)
		{
			this.p = p;
		}
		#region Punt
		private Punt p;
		public Punt P
		{
			get { return p; }
		}
		#endregion
		#region Locatie
		public override Point Locatie
		{
			get { return p.Coordinaat; }
			set { p.Coordinaat = value; }
		}
		#endregion
		
		public override void Draw(Graphics gr, Point pt)
		{
			Rectangle r = new Rectangle(pt, new Size());
			r.Inflate(5, 5);
			gr.FillEllipse(Brushes.LightGreen, r);
			gr.DrawEllipse(Pens.Black, r);
		}
	}
}
