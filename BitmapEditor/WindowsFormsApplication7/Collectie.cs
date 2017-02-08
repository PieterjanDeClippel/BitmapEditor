using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
	public class Collectie<T> : System.Collections.ObjectModel.Collection<T>
	{
		public class CollectieChangedEventArgs : EventArgs
		{
			private T[] itemsToegevoegd = new T[] { };
			public T[] ItemsToegevoegd
			{
				get { return itemsToegevoegd; }
			}

			private T[] itemsVerwijderd = new T[] { };
			public T[] ItemsVerwijderd
			{
				get { return itemsVerwijderd; }
			}

			public enum enActie
			{
				Toegevoegd,
				Verwijderd,
				Gewijzigd
			}
			private enActie actie = enActie.Toegevoegd;
			public enActie Actie
			{
				get { return actie; }
			}

			public CollectieChangedEventArgs(enActie _actie, T[] _toegevoegd, T[] _verwijderd)
			{
				actie = _actie;
				itemsToegevoegd = _toegevoegd;
				itemsVerwijderd = _verwijderd;
			}
		}
		public delegate void ChangedEventHandler(object sender, CollectieChangedEventArgs e);
		public event ChangedEventHandler Changed;
		protected virtual void OnChanged(CollectieChangedEventArgs e)
		{
			if (Changed != null & canRaiseEvents)
				Changed(this, e);
		}

		public new void Add(T item)
		{
			base.Add(item);
			CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Toegevoegd, new T[] { item }, new T[] { });
			OnChanged(ee);
		}
		public void AddRange(T[] items)
		{
			foreach (T item in items)
				base.Add(item);
			CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Toegevoegd, items, new T[] { });
			OnChanged(ee);
		}
		public void Insert(T item, int index)
		{
			base.Insert(index, item);
			CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Toegevoegd, new T[] { item }, new T[] { });
			OnChanged(ee);
		}
		public new void Remove(T item)
		{
			base.Remove(item);
			CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Verwijderd, new T[] { }, new T[] { item });
			OnChanged(ee);
		}
		public new void RemoveAt(int index)
		{
			T item = this[index];
			base.RemoveAt(index);
			CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Verwijderd, new T[] { }, new T[] { item });
			OnChanged(ee);
		}
		public new void Clear()
		{
			T[] items = this.ToArray();
			base.Clear();
			CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Verwijderd, new T[] { }, items);
			OnChanged(ee);
		}
		[DebuggerNonUserCode()]
		public new T this[int index]
		{
			[DebuggerNonUserCode()]
			get
			{
				return base[index];
			}
			[DebuggerNonUserCode()]
			set
			{
				CollectieChangedEventArgs ee = new CollectieChangedEventArgs(CollectieChangedEventArgs.enActie.Gewijzigd, new T[] { value }, new T[] { base[index] });
				base[index] = value;
				OnChanged(ee);
			}
		}

		public new int IndexOf(T item)
		{
			return base.IndexOf(item);
		}

		private bool canRaiseEvents = true;
		public bool CanRaiseEvents
		{
			get { return canRaiseEvents; }
			set { canRaiseEvents = value; }
		}
	}
}
