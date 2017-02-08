using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Vector
	{
		public Vector(double X, double Y)
		{
			x = X;
			y = Y;
		}
		public Vector(PointD P)
		{
			x = P.X;
			y = P.Y;
		}
		#region X
		private double x;
		public double X
		{
			get { return x; }
			set { x = value; }
		}
		#endregion
		#region Y
		private double y;
		public double Y
		{
			get { return y; }
			set { y = value; }
		}
		#endregion
		public void AddCarthesian(double dx, double dy)
		{
			x += dx;
			y += dy;
		}
		public void AddVector(double modulus, double argument_deg)
		{
			x += modulus * Math.Cos(argument_deg / 180 * Math.PI);
			y += modulus * Math.Sin(argument_deg / 180 * Math.PI);
		}
		

		public static implicit operator Point(Vector v)
		{
			return new Point((int)v.x, (int)v.y);
		}
	}
}
