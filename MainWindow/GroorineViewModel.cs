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
	using Utility;
	public partial class GroorineViewModel : NotifyPropertyChangedBase
	{

		Project project;

		public Project CurrentProject => project;

		public PlayerTrack CurrentTrack { get; set; }

		public bool IsPlaying => project.Timeline.IsRunning;

		public bool IsNotPlaying => !IsPlaying;


		public GroorineViewModel()
		{
			project = new Project();
			project.Timeline.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(CurrentProject.Timeline.IsRunning))
				{
					NotifyPropertyChanged(nameof(IsPlaying));
					NotifyPropertyChanged(nameof(IsNotPlaying));
					
				}
				/*if (args.PropertyName == nameof(CurrentProject.Timeline.CurrentTick))
				{
					GoToFirst?.NotifyCanExecuteChanged(GoToFirst, EventArgs.Empty);
					GoToLast?.NotifyCanExecuteChanged(GoToLast, EventArgs.Empty);
				}*/
			};
			var pt = new PlayerTrack
			{
				Instrument = new TestInstrument()
			};
			pt.AudioEffectors.Add(new TestAudioEffect());
			pt.AudioEffectors.Add(new TestAudioEffect());
			pt.Name = "TestInstrument";
			pt.IsMidiTrack = true;
			var region = new MidiRegion
			{
				Name = "AAA",
				Length = 334
			};
			pt.Regions.Add(region);
			pt.SelectedRegion = region;
			project.Timeline.Players.Add(pt);
			CurrentTrack = pt;
			project.Timeline.EndOfProject = 1920;




		}


		int count = 1;
		private CommandHandler addTrack;
		private CommandHandler copyTrack;
		private CommandHandler play;
		private CommandHandler goToFirst;
		private CommandHandler goToLast;
		private CommandHandler stop;
		private CommandHandler previous;
		private CommandHandler next;
		private CommandHandler rewind;

		public CommandHandler AddTrack
			=> addTrack ?? (addTrack = new CommandHandler(() =>
			{
				var pt = new PlayerTrack
				{
					Instrument = new TestInstrument()
				};
				pt.AudioEffectors.Add(new TestAudioEffect());
				pt.AudioEffectors.Add(new TestAudioEffect());
				pt.Name = "TestInstrument" + count++;
				pt.IsMidiTrack = true;
				CurrentProject.Timeline.Players.Add(pt);
				CurrentTrack = pt;
			}, (x) => true));

		public CommandHandler CopyTrack
			=> copyTrack ?? (copyTrack = new CommandHandler(() =>
			{
				var pt = new PlayerTrack(CurrentTrack);
				CurrentProject.Timeline.Players.Add(pt);
				CurrentTrack = pt;
			}, (x) => CurrentTrack != null));

		public CommandHandler Play
			=> play ?? (play = new CommandHandler(async () => await CurrentProject.Timeline.StartAsync(), (x) => !CurrentProject.Timeline.IsRunning));

		public CommandHandler GoToFirst
			=> goToFirst ?? (goToFirst = new CommandHandler(() => CurrentProject.Timeline.Seek(0), (hage) => true));

		public CommandHandler GoToLast
			=> goToLast ?? (goToLast = new CommandHandler(() => CurrentProject.Timeline.Seek(CurrentProject.Timeline.EndOfProject - 1), (hage) => true));

		public CommandHandler Stop
			=> stop ?? (stop = new CommandHandler(() => CurrentProject.Timeline.Stop(), (x) => CurrentProject.Timeline.IsRunning));

		public CommandHandler Previous
			=> previous ?? (stop = new CommandHandler(() => CurrentProject.Timeline.Previous(), (x) => true));

		public CommandHandler Next
			=> next ?? (stop = new CommandHandler(() => CurrentProject.Timeline.Next(), (x) => true));

		public CommandHandler Rewind
			=> rewind ?? (stop = new CommandHandler(() => CurrentProject.Timeline.Rewind(), (x) => true));



		public class CommandHandler : ICommand
		{
			private Action action;
			private Func<object, bool> canExecute;
			public CommandHandler(Action action, Func<object, bool> canExecute)
			{
				this.action = action;
				this.canExecute = canExecute;
			}

			public bool CanExecute(object parameter) => canExecute(parameter);

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter) => action();

			public void NotifyCanExecuteChanged(object sender, EventArgs e) => CanExecuteChanged?.Invoke(sender, e);
		}

	}
}