using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace Groorine
{
	using Models;
	using Modules;
	public class GroorineViewModel
	{
		
		Project project = new Project();

		public Project CurrentProject => project;

		public PlayerTrack CurrentTrack { get; private set; }

		public GroorineViewModel()
		{
			var pt = new PlayerTrack
			{
				Instrument = new TestInstrument()
			};
			pt.AudioEffectors.Add(new TestAudioEffect());
			pt.AudioEffectors.Add(new TestAudioEffect());
			pt.IsMidiTrack = true;
			project.Timeline.Players.Add(pt);
			CurrentTrack = pt;
		}

	}
}
