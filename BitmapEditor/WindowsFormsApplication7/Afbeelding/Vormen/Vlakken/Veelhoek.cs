using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Veelhoek : Vlak
	{
		public Veelhoek()
			: base(Vorm_type.Veelhoek)
		{
			punten.Changed += Punten_Changed;
		}

		#region Punten
		private Collectie<Punt> punten = new Collectie<Punt>();
		public Collectie<Punt> Punten
		{
			get { return punten; }
		}
		#endregion
		#region Glyphs
		private List<PuntGlyph> glyphs = new List<PuntGlyph>();
		public override Glyph[] Glyphs
		{
			get
			{
				return glyphs.ToArray();
			}
		}
		#endregion

		public override void Draw(Graphics gr, bool print)
		{
			gr.FillPolygon(GetBrush(gr, print), punten.Select(T => T.Coordinaat).ToArray());
			gr.DrawPolygon(GetPen(), punten.Select(T => T.Coordinaat).ToArray());
		}

		private void Punten_Changed(object sender, Collectie<Punt>.CollectieChangedEventArgs e)
		{
			glyphs.AddRange(e.ItemsToegevoegd.Select(T => new PuntGlyph(T)));
			glyphs.RemoveAll(T => e.ItemsVerwijderd.Contains(T.P));
		}

		public override Rectangle Bounds()
		{
			var x_min = punten.Min(T => T.Coordinaat.X);
			var y_min = punten.Min(T => T.Coordinaat.Y);
			var x_max = punten.Max(T => T.Coordinaat.X);
			var y_max = punten.Max(T => T.Coordinaat.Y);
			return new Rectangle(x_min, y_min, x_max - x_min, y_max - y_min);
		}
	}
}
