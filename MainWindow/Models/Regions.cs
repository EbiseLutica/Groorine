using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Groorine.Models
{
	public abstract class Region : ModelBase
	{
		private int tick;
		private int length;
		private bool loop;
		private string name;

		public int Tick
		{
			get
			{
				return tick;
			}
			set
			{
				tick = value;
				NotifyPropertyChanged(nameof(Tick));
			}
		}
		public int Length
		{
			get
			{
				return length;
			}
			set
			{
				length = value;
				NotifyPropertyChanged(nameof(Length));
			}
		}
		public bool Loop
		{
			get
			{
				return loop;
			}
			set
			{
				loop = value;
				NotifyPropertyChanged(nameof(Loop));
			}
		}
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}

	}

	public class MidiRegion : Region
	{
		public ObservableCollection<BasePlayerEvent> EventList { get; } = new ObservableCollection<BasePlayerEvent>();

		public MidiRegion()
		{
			BindingOperations.EnableCollectionSynchronization(EventList, new object());
		}

	}

	public class AudioRegion : Region
	{

	}

}
