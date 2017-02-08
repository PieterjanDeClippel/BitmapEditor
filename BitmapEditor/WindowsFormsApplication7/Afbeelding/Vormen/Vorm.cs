using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public abstract class Vorm
	{
		public Vorm(Vorm_type type)
		{
			vorm_type = type;
			switch (type)
			{
				case Vorm_type.Rechte:
				case Vorm_type.Kromme:
				case Vorm_type.CirkelBoog:
					vorm_hoofdtype = Vorm_hoofdtype.Lijn;
					break;
				case Vorm_type.Cirkel:
				case Vorm_type.Ellips:
				case Vorm_type.Veelhoek:
				case Vorm_type.GeslotenKromme:
				case Vorm_type.CirkelSector:
				case Vorm_type.CirkelSegment:
					vorm_hoofdtype = Vorm_hoofdtype.Vlak;
					break;
				case Vorm_type.Tekst:
					vorm_hoofdtype = Vorm_hoofdtype.Tekst;
					break;
			}
		}

		public abstract void Draw(Graphics gr, bool print);
		public abstract Glyph[] Glyphs { get; }
		public abstract Rectangle Bounds();
		#region Geselecteerd
		private bool geselecteerd = false;
		public bool Geselecteerd
		{
			get { return geselecteerd; }
			set { geselecteerd = value; }
		}
		#endregion
		#region Zichtbaar
		private int zichtbaarheid = 100;
		public int Zichtbaarheid
		{
			get { return zichtbaarheid; }
			set
			{
				if (zichtbaarheid == value) return;
				zichtbaarheid = value;
			}
		}
		#endregion
		#region HoofdType
		private Vorm_hoofdtype vorm_hoofdtype;
		public Vorm_hoofdtype Vorm_Hoofdtype
		{
			get { return vorm_hoofdtype; }
		}
		#endregion
		#region Type
		private Vorm_type vorm_type;
		public Vorm_type Vorm_Type
		{
			get { return vorm_type; }
		}
		#endregion
		#region Enums
		public enum Vorm_type
		{
			// nieuwe vorm
			Rechte,			//OK
			Kromme,			//OK
			Cirkel,			//OK
			CirkelBoog,
			Ellips,
			Veelhoek,		//OK
			GeslotenKromme,	//OK
			CirkelSector,
			CirkelSegment,
			Tekst
		}
		public enum Vorm_hoofdtype
		{
			Lijn,
			Tekst,
			Vlak
		}
		#endregion
	}
}
