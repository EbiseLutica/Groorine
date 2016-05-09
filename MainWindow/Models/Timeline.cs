using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Groorine.Models
{
	public class Timeline
	{
		public ConductorTrack Conductor { get; set; } = new ConductorTrack();
		public ObservableCollection<PlayerTrack> Players { get; } = new ObservableCollection<PlayerTrack>();

		public Timeline()
		{
			
			BindingOperations.EnableCollectionSynchronization(Players, new object());
		}

	}
}