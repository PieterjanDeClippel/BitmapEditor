using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public abstract class Layer
	{
		public abstract void Draw(Graphics gr, bool print);
	}
}
