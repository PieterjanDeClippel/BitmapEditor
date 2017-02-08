using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class PaintLayer : Layer
	{
		public PaintLayer(Size Size)
		{
			bmp = new Bitmap(Size.Width, Size.Height);
		}

		#region Bitmap
		Bitmap bmp;
		#endregion
		#region Size
		public Size Size
		{
			get { return bmp.Size; }
			set
			{
				Bitmap nieuw = new Bitmap(value.Width, value.Height);
				if (bmp != null)
				{
					Graphics gr = Graphics.FromImage(nieuw);
					gr.DrawImage(bmp, new Point());
					gr.Dispose();
				}
				bmp = nieuw;
			}
		}
		#endregion

		public override void Draw(Graphics gr, bool print)
		{
			if (bmp != null)
				gr.DrawImage(bmp, new Point());
		}
	}
}
