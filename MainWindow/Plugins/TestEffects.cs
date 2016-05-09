using Groorine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groorine.Plugins
{
	public class TestAudioEffect : IAudioEffector
	{
		public string Name => "AudioEffect";
	}

	public class TestMidiEffect : IMidiEffector
	{
		public string Name => "MidiEffect";
	}
}
