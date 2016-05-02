using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Groorine.Models
{
	public abstract class Region
	{
		public int Tick { get; set; }
		public int Length { get; set; } 
		public bool Loop { get; set; }
		public string Name { get; set; }
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
