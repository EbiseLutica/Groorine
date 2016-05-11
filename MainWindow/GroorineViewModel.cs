using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace Groorine
{
	using Models;
	using Plugins;
	using System.Windows.Data;
	using System.Windows.Input;
	public partial class GroorineViewModel
	{
		
		Project project = new Project();

		public Project CurrentProject => project;

		public PlayerTrack CurrentTrack { get; set; }
		
		public GroorineViewModel()
		{
			var pt = new PlayerTrack
			{
				Instrument = new TestInstrument()
			};
			pt.AudioEffectors.Add(new TestAudioEffect());
			pt.AudioEffectors.Add(new TestAudioEffect());
			pt.Name = "TestInstrument";
			pt.IsMidiTrack = true;
			pt.Regions.Add(new MidiRegion
			{
				Name = "AAA",
				Length = 334
			});
			project.Timeline.Players.Add(pt);
			CurrentTrack = pt;

			AddTrack = new AddTrackCommand(this);
			CopyTrack = new CopyTrackCommand(this);


		}


	}
}
