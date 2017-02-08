using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Rechte : Lijn
	{
		public Rechte()
			: base(Vorm_type.Rechte)
		{
			punt1 = new Punt();
			punt2 = new Punt();

			glyphs.Add(new PuntGlyph(punt1));
			glyphs.Add(new PuntGlyph(punt2));
		}

		#region Punt1
		private Punt punt1;
		public Punt Punt1
		{
			get { return punt1; }
		}
		#endregion
		#region Punt2
		private Punt punt2;
		public Punt Punt2
		{
			get { return punt2; }
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
			gr.DrawLine(GetPen(), punt1.Coordinaat, punt2.Coordinaat);
		}

		public override Rectangle Bounds()
		{
			var x_min = Math.Min(punt1.Coordinaat.X, punt2.Coordinaat.X);
			var y_min = Math.Min(punt1.Coordinaat.Y, punt2.Coordinaat.Y);
			var x_max = Math.Max(punt1.Coordinaat.X, punt2.Coordinaat.X);
			var y_max = Math.Max(punt1.Coordinaat.Y, punt2.Coordinaat.Y);
			return new Rectangle(x_min, y_min, x_max - x_min, y_max - y_min);
		}
	}
}
