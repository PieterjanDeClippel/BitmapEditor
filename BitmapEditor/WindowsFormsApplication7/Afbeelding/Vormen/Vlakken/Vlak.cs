using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public abstract class Vlak : Vorm
	{
		public Vlak(Vorm_type vorm_type)
			: base(vorm_type)
		{
		}

		#region Omtrek
		private Pen pen = new Pen(Color.Black);
		public Pen GetPen()
		{
			return pen;
		}

		public Color LijnKleur
		{
			get { return pen.Color; }
			set
			{
				if (pen.Color == value) return;
				pen.Color = value;
			}
		}
		public DashStyle LijnStijl
		{
			get { return pen.DashStyle; }
			set
			{
				if (pen.DashStyle == value) return;
				pen = new Pen(pen.Color, pen.Width) { DashStyle = value };
			}
		}
		public float[] DashPattern
		{
			get
			{
				switch (pen.DashStyle)
				{
					case DashStyle.Solid:
						return new float[] { 1.0f };
					default:
						return pen.DashPattern;
				}
			}
			set
			{
				if (pen.DashPattern == value) return;
				pen.DashPattern = value;
			}
		}
		public float LijnDikte
		{
			get { return pen.Width; }
			set
			{
				if (pen.Width == value) return;
				pen.Width = value;
			}
		}
		#endregion
		
		#region Opvulling
		#region Brush-type
		public enum OpvulSoort
		{
			Geen,
			Solid,
			Hatch,
			LinearGradient,
			RadialGradient
		}
		private OpvulSoort opvulsoort = OpvulSoort.Solid;
		public OpvulSoort OpvulType
		{
			get { return opvulsoort; }
			set
			{
				if (opvulsoort == value) return;
				opvulsoort = value;
			}
		}
		#endregion
		#region HatchBrush -> Stijl
		private HatchStyle vulstijl = HatchStyle.SolidDiamond;
		public HatchStyle VulStijl
		{
			get { return vulstijl; }
			set
			{
				if (vulstijl == value) return;
				vulstijl = value;
			}
		}
		#endregion
		#region LinearGradient -> Hoek
		private int loophoek = 45;
		public int LoopHoek
		{
			get { return loophoek; }
			set
			{
				if (loophoek == value) return;
				loophoek = value;
			}
		}
		#endregion
		#region Kleur1
		private Color kleur1 = Color.White;
		public Color Kleur1
		{
			get { return kleur1; }
			set
			{
				if (kleur1 == value) return;
				kleur1 = value;
			}
		}
		#endregion
		#region Kleur2
		private Color kleur2 = Color.Gray;
		public Color Kleur2
		{
			get { return kleur2; }
			set
			{
				if (kleur2 == value) return;
				kleur2 = value;
			}
		}
		#endregion
		#region GetBrush
		public Brush GetBrush(Graphics gr, bool print)
		{
			Color k1 = Color.FromArgb(Convert.ToInt32(2.55f * Zichtbaarheid), kleur1);
			Color k2 = Color.FromArgb(Convert.ToInt32(2.55f * Zichtbaarheid), kleur2);
			switch (opvulsoort)
			{
				case OpvulSoort.Geen:
					return Brushes.Transparent;
				case OpvulSoort.Solid:
					return new SolidBrush(k1);
				case OpvulSoort.Hatch:
					return new HatchBrush(vulstijl, k1, k2);
				case OpvulSoort.LinearGradient:
					Rectangle b = Bounds();
					return new LinearGradientBrush(b, k1, k2, loophoek);
				case OpvulSoort.RadialGradient:
					Rectangle rct = Bounds();
					GraphicsPath path = new GraphicsPath();
					switch (Vorm_Type)
					{
						case Vorm_type.Cirkel:
							path.AddEllipse(rct);
							break;
						case Vorm_type.Ellips:
							path.AddEllipse(rct);
							break;
						case Vorm_type.Veelhoek:
							Veelhoek v = (Veelhoek)this;
							path.AddPolygon(v.Punten.Select(T => T.Coordinaat).ToArray());
							break;
						case Vorm_type.GeslotenKromme:
							GeslotenKromme k = (GeslotenKromme)this;
							path.AddClosedCurve(k.Punten.Select(T => T.Coordinaat).ToArray());
							break;
						case Vorm_type.CirkelSector:
							path.AddEllipse(rct);
							break;
						case Vorm_type.CirkelSegment:
							path.AddEllipse(rct);
							break;
						default:
							break;
					}
					PathGradientBrush br = new PathGradientBrush(path);
					br.CenterPoint = new PointF((rct.Left + rct.Right) / 2, (rct.Top + rct.Bottom) / 2);
					br.CenterColor = k1;
					br.SurroundColors = new Color[] { k2 };
					return br;
				default:
					return new SolidBrush(k1);
			}
		}
		#endregion
		#endregion


	}
}
