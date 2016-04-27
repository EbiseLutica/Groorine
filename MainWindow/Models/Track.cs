using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groorine.Models
{
	class Track
	{
		public IInstrument Instrument { get; set; }
		public List<IMidiEffector> MidiEffectors { get; set; }
		public List<IAudioEffector> AudioEffectors { get; set; }
		
	}

	interface IInstrument : IModule
	{

	}

	interface IMidiEffector : IEffector	{


	}

	interface IAudioEffector : IEffector
	{

	}

	interface IEffector : IModule
	{

	}

	interface IModule
	{

	}

}
