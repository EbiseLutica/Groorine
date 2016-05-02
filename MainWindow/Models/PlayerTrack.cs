using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Groorine.Models
{
	public class PlayerTrack : ModelBase
	{
		public IInstrument Instrument { get; set; }
		public ObservableCollection<IMidiEffector> MidiEffectors { get; } = new ObservableCollection<IMidiEffector>();
		public ObservableCollection<IAudioEffector> AudioEffectors { get; } = new ObservableCollection<IAudioEffector>();
		public bool IsMidiTrack { get; set; }
		public ObservableCollection<Region> Regions { get; } = new ObservableCollection<Region>();
		public int Panpot { get; set; }
		public string Name { get; set; }
		public double Volume { get; set; }
		public bool IsMute { get; set; }
		public bool IsSolo { get; set; }

		public PlayerTrack()
		{
			BindingOperations.EnableCollectionSynchronization(MidiEffectors, new object());
			BindingOperations.EnableCollectionSynchronization(AudioEffectors, new object());
			BindingOperations.EnableCollectionSynchronization(Regions, new object());
		}

	}

	public interface IInstrument : IModule
	{

	}

	public interface IMidiEffector : IEffector	{

	}

	public interface IAudioEffector : IEffector
	{

	}

	public interface IEffector : IModule
	{

	}

	public interface IModule
	{
		string Name { get; }		
	}

}
