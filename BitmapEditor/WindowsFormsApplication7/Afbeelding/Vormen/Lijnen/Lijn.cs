using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public abstract class Lijn : Vorm
	{
		public Lijn(Vorm_type vorm_type)
			: base(vorm_type)
		{
		}

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
	}
}
