using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class SizeD
	{

		public SizeD(double Width, double Height)
		{
			width = Width;
			height = Height;
		}
		public SizeD()
		{
			width = height = 0;
		}

		private double width;
		public double Width
		{
			get { return width; }
		}

		private double height;
		public double Height
		{
			get { return height; }
		}

		public static implicit operator Size(SizeD s)
		{
			return new Size((int)s.width, (int)s.height);
		}

		public static implicit operator SizeD(Size s)
		{
			return new SizeD(s.Width, s.Height);
		}

		public Size ToSize()
		{
			return new Size((int)width, (int)height);
		}
	}
}
