using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;
using Groorine.Utility;

namespace Groorine.Models
{
	public class ConductorTrack : NotifyPropertyChangedBase
	{
		public ObservableCollection<BaseConductorEvent> Events { get; } = new ObservableCollection<BaseConductorEvent>();
		

		private Timeline parent;

		public ConductorTrack(Timeline parent)
		{
			BindingOperations.EnableCollectionSynchronization(Events, new object());

			Events.Add(new TempoEvent());
			Events.Add(new BeatEvent(4, 4));
			this.parent = parent;
		}

		public IEnumerable<BaseConductorEvent> GetEventRange(int starttick, int endtick) => from bce in Events
																							where starttick <= bce.Tick && bce.Tick <= endtick
																							select bce;

	}

	
}