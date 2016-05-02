using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;

namespace Groorine.Models
{
	public class ConductorTrack
	{
		public ObservableCollection<BaseConductorEvent> Events { get; } = new ObservableCollection<BaseConductorEvent>();

		public ConductorTrack()
		{
			BindingOperations.EnableCollectionSynchronization(Events, new object());
		}

	}
}