using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Afbeelding
	{
		public Afbeelding(Size Size)
		{
			size = Size;
		}

		#region Size
		private Size size;
		public Size Size
		{
			get { return size; }
			set
			{
				size = value;
			}
		}
		#endregion
		#region Layers
		private List<Layer> layers = new List<Layer>();
		public List<Layer> Layers
		{
			get { return layers; }
		}
		#endregion
		#region AntiAlias
		private bool antiAlias = false;
		public bool AntiAlias
		{
			get { return antiAlias; }
			set { antiAlias = value; }
		}
		#endregion
		#region GetPreview
		public Bitmap GetPreview(bool print)
		{
			Bitmap result = new Bitmap(size.Width, size.Height);
			Graphics gr = Graphics.FromImage(result);
			gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			if (antiAlias) gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			foreach (Layer layer in layers)
			{
				layer.Draw(gr, print);
			}
			return result;
		}
		#endregion
	}
}
