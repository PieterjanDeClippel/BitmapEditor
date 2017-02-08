using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class ShapeLayer : Layer
	{
		public ShapeLayer()
		{

		}

		#region Vormen
		private List<Vorm> vormen = new List<Vorm>();
		public List<Vorm> Vormen
		{
			get { return vormen; }
		}
		#endregion

		public override void Draw(Graphics gr, bool print)
		{
			foreach (Vorm vorm in vormen)
			{
				vorm.Draw(gr, print);
			}
		}
	}
}
