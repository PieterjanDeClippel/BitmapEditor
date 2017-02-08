using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Ellips : Vlak, IRectangular
	{
		public Ellips()
			: base(Vorm_type.Ellips)
		{
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Top | AnchorStyles.Left));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Top));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Top | AnchorStyles.Right));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Bottom | AnchorStyles.Left));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Bottom));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Bottom | AnchorStyles.Right));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Left));
			glyphs.Add(new RectangularGlyph(this, AnchorStyles.Right));
		}

		private int angle = 0;
		public int Angle
		{
			get
			{
				return angle;
			}
			set
			{
				angle = value;
			}
		}

		private List<Glyph> glyphs = new List<Glyph>();
		public override Glyph[] Glyphs
		{
			get
			{
				return glyphs.ToArray();
			}
		}

		private Point location = new Point();
		public Point Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		private Size size = new Size();
		public Size Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}

		public override Rectangle Bounds()
		{
			throw new NotImplementedException();
		}

		public override void Draw(Graphics gr, bool print)
		{
			Brush br = GetBrush(gr, print);
			gr.TranslateTransform(location.X, location.Y);
			gr.RotateTransform(angle);
			gr.FillEllipse(br, new Rectangle(new Point(), size));
			gr.DrawEllipse(GetPen(), new Rectangle(new Point(), size));
			gr.ResetTransform();
		}

		/*
public void SetSizeD(double width, double height)
{
	size = new SizeD(width, height);
}

public void SetLocationD(double X, double Y)
{
	location = new PointD(X, Y);
}*/
	}
}
