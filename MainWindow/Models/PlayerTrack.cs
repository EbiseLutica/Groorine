using Groorine.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Groorine.Models
{
	public class PlayerTrack : NotifyPropertyChangedBase
	{
		public IInstrument Instrument { get; set; }
		public ObservableCollection<IMidiEffector> MidiEffectors { get; } = new ObservableCollection<IMidiEffector>();
		public ObservableCollection<IAudioEffector> AudioEffectors { get; } = new ObservableCollection<IAudioEffector>();
		public bool IsMidiTrack { get; set; }
		public ObservableCollection<Region> Regions { get; } = new ObservableCollection<Region>();

		private Region selectedRegion;

		public Region SelectedRegion
		{
			get
			{
				return selectedRegion;
			}
			set
			{
				selectedRegion = value;
				NotifyPropertyChanged(nameof(SelectedRegion));
			}
		}

		public int Panpot
		{
			get
			{
				return panpot;
			}
			set
			{
				panpot = value;
				NotifyPropertyChanged(nameof(Panpot));
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
		public double Volume
		{
			get
			{
				return volume;
			}
			set
			{
				volume = value;
				NotifyPropertyChanged(nameof(Volume));
			}
		}
		public bool IsMute
		{
			get
			{
				return ismute;
			}
			set
			{
				ismute = value;
				NotifyPropertyChanged(nameof(IsMute));
			}
		}
		public bool IsSolo
		{
			get
			{
				return issolo;
			}
			set
			{
				issolo = value;
				NotifyPropertyChanged(nameof(IsSolo));
			}
		}

		public Region CurrentRegion(int tick) => Regions.First((r) => r.Tick <= tick && tick < r.Tick + r.Length);

		public double MaxOutput { get; private set; }
		public double Output { get; set; }

		private int panpot;
		private string name;
		private double volume;
		private bool ismute;
		private bool issolo;

		public PlayerTrack()
		{
			BindingOperations.EnableCollectionSynchronization(MidiEffectors, new object());
			BindingOperations.EnableCollectionSynchronization(AudioEffectors, new object());
			BindingOperations.EnableCollectionSynchronization(Regions, new object());
		}

		public PlayerTrack(PlayerTrack t)
			:this()
		{
			Panpot = t.Panpot;
			IsMute = t.IsMute;
			IsSolo = t.IsSolo;
			Volume = t.Volume;
			Name = t.Name;
			IsMidiTrack = t.IsMidiTrack;
			Instrument = Activator.CreateInstance(t.Instrument.GetType()) as IInstrument;
			foreach (IAudioEffector ae in t.AudioEffectors)
				AudioEffectors.Add(Activator.CreateInstance(ae.GetType()) as IAudioEffector);
			foreach (IMidiEffector me in t.MidiEffectors)
				MidiEffectors.Add(Activator.CreateInstance(me.GetType()) as IMidiEffector);
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
