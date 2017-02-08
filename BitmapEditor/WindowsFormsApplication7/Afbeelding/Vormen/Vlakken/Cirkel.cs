using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Cirkel : Vlak
	{
		public Cirkel()
			: base(Vorm_type.Cirkel)
		{
			m = new Punt();
			o = new Punt();

			glyphs.Add(new PuntGlyph(m));
			glyphs.Add(new PuntGlyph(o));
		}

		#region M
		private Punt m;
		public Punt M
		{
			get { return m; }
		}
		#endregion
		#region O
		private Punt o;
		public Punt O
		{
			get { return o; }
		}
		#endregion
		#region Glyphs
		private List<PuntGlyph> glyphs = new List<PuntGlyph>();
		public override Glyph[] Glyphs
		{
			get { return glyphs.ToArray(); }
		}
		#endregion

		public override void Draw(Graphics gr, bool print)
		{
			int r = (int)Math.Sqrt(Math.Pow(m.Coordinaat.X - o.Coordinaat.X, 2) + Math.Pow(m.Coordinaat.Y - o.Coordinaat.Y, 2));
			gr.FillEllipse(GetBrush(gr, print), m.Coordinaat.X - r, m.Coordinaat.Y - r, 2 * r, 2 * r);
			gr.DrawEllipse(GetPen(), m.Coordinaat.X - r, m.Coordinaat.Y - r, 2 * r, 2 * r);
		}

		public override Rectangle Bounds()
		{
			int r = (int)Math.Sqrt(Math.Pow(m.Coordinaat.X - o.Coordinaat.X, 2) + Math.Pow(m.Coordinaat.Y - o.Coordinaat.Y, 2));
			Rectangle rct = new Rectangle(m.Coordinaat, new Size());
			rct.Inflate(r, r);
			return rct;
		}
	}
}
