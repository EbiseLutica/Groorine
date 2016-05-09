using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Groorine
{
	using Models;
	using Plugins;
	public partial class GroorineViewModel
	{
		class AddTrackCommand : ICommand
		{
			public event EventHandler CanExecuteChanged;
			private GroorineViewModel parent;
			public bool CanExecute(object parameter) => true;
			int count = 1;
			public void Execute(object parameter)
			{
				var pt = new PlayerTrack
				{
					Instrument = new TestInstrument()
				};
				pt.AudioEffectors.Add(new TestAudioEffect());
				pt.AudioEffectors.Add(new TestAudioEffect());
				pt.Name = "TestInstrument" + count++;
				pt.IsMidiTrack = true;
				parent.CurrentProject.Timeline.Players.Add(pt);
				parent.CurrentTrack = pt;
				
			}

			public AddTrackCommand(GroorineViewModel gvm)
			{
				parent = gvm;
			}
		}

		class CopyTrackCommand : ICommand
		{
			public event EventHandler CanExecuteChanged;
			private GroorineViewModel parent;
			public bool CanExecute(object parameter) => parent?.CurrentTrack != null;

			public void Execute(object parameter)
			{
				var pt = new PlayerTrack(parent.CurrentTrack);
				parent.CurrentProject.Timeline.Players.Add(pt);
				parent.CurrentTrack = pt;
			}

			public CopyTrackCommand(GroorineViewModel gvm)
			{
				parent = gvm;
			}

		}
		
		public ICommand CopyTrack { get; private set; }
		public ICommand AddTrack { get; private set; }

	}
}
