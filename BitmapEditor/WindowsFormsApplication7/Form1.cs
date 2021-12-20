using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Afbeelding afb = new Afbeelding(new Size(128, 128)) { AntiAlias = checkBox1.Checked };
			ShapeLayer layer = new ShapeLayer();
			afb.Layers.Add(layer);

			Rechte r = new Rechte();
			r.Punt1.Coordinaat = new Point(10, 10);
			r.Punt2.Coordinaat = new Point(54, 54);
			layer.Vormen.Add(r);
			r.LijnStijl = System.Drawing.Drawing2D.DashStyle.DashDotDot;
			r.LijnDikte = 3;
			r.LijnKleur = Color.Blue;
			r.Geselecteerd = true;

			Cirkel c = new Cirkel();
			c.O.Coordinaat = new Point(10, 10);
			c.M.Coordinaat = new Point(54, 54);
			layer.Vormen.Add(c);
			c.LijnStijl = System.Drawing.Drawing2D.DashStyle.Dash;
			c.LijnDikte = 1;
			c.LijnKleur = Color.Red;
			c.OpvulType = Vlak.OpvulSoort.RadialGradient;
			c.Kleur1 = Color.Green;
			c.Kleur2 = Color.Yellow;
			c.Geselecteerd = true;

			GeslotenKromme k = new GeslotenKromme();
			k.OpvulType = Vlak.OpvulSoort.Solid;
			k.Kleur1 = Color.Blue;
			k.Punten.AddRange(new Punt[]
			{
				new Punt() {Coordinaat = new Point(20,20) },
				new Punt() {Coordinaat = new Point(20,40) },
				new Punt() {Coordinaat = new Point(40,50) },
				new Punt() {Coordinaat = new Point(60,60) },
				new Punt() {Coordinaat = new Point(20,80) }
			});
			layer.Vormen.Add(k);
			k.Geselecteerd = true;


			Kromme v = new Kromme();
			/*v.OpvulType = Vlak.OpvulSoort.Hatch;
			v.Kleur1 = Color.Purple;*/
			v.LijnKleur = Color.Green;
			v.LijnDikte = 3;
			v.LijnStijl = System.Drawing.Drawing2D.DashStyle.Dot;
			v.Punten.AddRange(new Punt[]
			{
				new Punt() {Coordinaat = new Point(20,20) },
				new Punt() {Coordinaat = new Point(20,40) },
				new Punt() {Coordinaat = new Point(40,50) },
				new Punt() {Coordinaat = new Point(60,60) },
				new Punt() {Coordinaat = new Point(20,80) }
			});
			layer.Vormen.Add(v);
			v.Geselecteerd = true;

			el = new Ellips();
			el.OpvulType = Vlak.OpvulSoort.Hatch;
			el.Kleur1 = Color.Purple;
			el.LijnKleur = Color.Green;
			el.LijnDikte = 3;
			el.LijnStijl = System.Drawing.Drawing2D.DashStyle.Dot;
			el.Location = new Point(30, 30);
			el.Size = new Size(100, 40);
			el.Geselecteerd = true;
			layer.Vormen.Add(el);


			afbeeldingEditor1.Afbeelding = afb;
			afbeeldingEditor1.Schaal = 2;
		}

		Ellips el;

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if (el != null)
				el.Angle = (int)numericUpDown1.Value;
			afbeeldingEditor1.Invalidate();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (afbeeldingEditor1.Afbeelding != null)
				afbeeldingEditor1.Afbeelding.AntiAlias = checkBox1.Checked;
			afbeeldingEditor1.Invalidate();
		}

        private void chkShowGlyphs_CheckedChanged(object sender, EventArgs e)
        {
			afbeeldingEditor1.ShowGlyphs = chkShowGlyphs.Checked;
        }
    }
}
