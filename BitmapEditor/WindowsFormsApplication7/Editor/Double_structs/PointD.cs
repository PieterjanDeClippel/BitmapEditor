using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class PointD
	{
		public PointD(double X, double Y)
		{
			x = X;
			y = Y;
		}
		public PointD()
		{
			x = y = 0;
		}

		private double x;
		public double X
		{
			get { return x; }
		}

		private double y;
		public double Y
		{
			get { return y; }
		}

		public static implicit operator Point(PointD p)
		{
			return new Point((int)p.x, (int)p.y);
		}

		public static implicit operator PointD(Point p)
		{
			return new PointD(p.X, p.Y);
		}
	}
}
