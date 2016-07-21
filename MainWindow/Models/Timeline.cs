using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Threading;
using System.Diagnostics;
using Groorine.Utility;

namespace Groorine.Models
{
	public class Timeline : NotifyPropertyChangedBase
	{
		
		private int resolution;
		public ConductorTrack Conductor { get; set; }
		public ObservableCollection<PlayerTrack> Players { get; } = new ObservableCollection<PlayerTrack>();

		private double bpm;
		private BeatEvent beat;

		public double CurrentBPM
		{
			get { return bpm; }
			set
			{
				bpm = value;
				NotifyPropertyChanged(nameof(CurrentBPM));
			}
		}
		public BeatEvent CurrentBeat
		{
			get { return beat; }
			set
			{
				beat = value;
				NotifyPropertyChanged(nameof(CurrentBeat));
			}
		}

		


		public MusicTime MusicTime
		{
			get
			{
				Queue<BeatEvent> qre = new Queue<BeatEvent>(Conductor.Events.OfType<BeatEvent>());
				MusicTime last = new MusicTime();
				last.Measure = 0;
				last.Beat = 1;

				BeatEvent tmp = new BeatEvent(4, 4);
				for (int i = 0; i < CurrentTick; i++)
				{
					if (last.Tick >= 4f / tmp.Note * Resolution - 1)
					{
						last.Beat++;
						last.Tick = -1;
					}
					if (last.Beat > tmp.Rhythm)
					{
						last.Measure++;
						last.Beat = 1;
					}

					if (qre.Count > 0 && qre.Peek().Tick <= i)
					{
						tmp = qre.Dequeue();
					}


					last.Tick++;
				}

				return last;
			}
			set
			{
				if (value.Measure < 0)
					value.Measure = 0;
				if (value.Beat < 1)
					value.Beat = 1;
				if (value.Tick < 0)
					value.Tick = 0;
				var qre = Conductor.Events.OfType<BeatEvent>();
				var source = MusicTime.InitialValue;
				var last = 0f;
				var tmp = new BeatEvent(4, 4);
				var tmp2 = tmp;
				for (source.Measure = 0; source.Measure <= value.Measure; source.Measure++)
				{
					tmp2 = (from be in qre
							where be.Tick <= last
							select be)
							.LastOrDefault();
					tmp = tmp2 ?? tmp;
					last += (4f / tmp.Note * Resolution) * tmp.Rhythm;
				}
				last += (4f / tmp.Note * Resolution) * value.Beat;
				last += value.Tick;
				if (last > EndOfProject)
					last = EndOfProject;
				currentTick = last;
				NotifyPropertyChanged(nameof(MusicTime));
				NotifyPropertyChanged(nameof(CurrentTick));
			}
		}

		public int Resolution
		{
			get
			{
				return resolution;
			}
			set
			{
				resolution = value;
				NotifyPropertyChanged(nameof(Resolution));
			}
		}

		int btime;

		private int endOfProject;
		private double currentTick;
		public int EndOfProject
		{
			get
			{
				return endOfProject;
			}
			set
			{
				endOfProject = value;
				NotifyPropertyChanged(nameof(EndOfProject));
			}
		}


		public double CurrentTick
		{
			get
			{
				return currentTick;
			}
			set
			{
				if (value > EndOfProject)
					value = EndOfProject;
				currentTick = value;
				NotifyPropertyChanged(nameof(CurrentTick));
				NotifyPropertyChanged(nameof(MusicTime));
			}
		}

		

		void UpdateTick()
		{
			int ntime = Environment.TickCount;
			if (IsRunning && ntime > btime)
			{
				CurrentTick += Resolution * ((ntime - btime) / 1000.0) * (CurrentBPM / 60.0);
				NotifyPropertyChanged(nameof(CurrentTick));
				btime = Environment.TickCount;
			}
		}

		//public int MiliSec => Environment.TickCount - startTime;

		
		public bool IsRunning => cts != null;

		private int startTime;

		private CancellationTokenSource cts;
		
		
		public async Task StartAsync()
		{

			Debug.WriteLine("Called" + nameof(StartAsync));
			btime = startTime = Environment.TickCount;
			if (IsRunning)
				return;
			cts = new CancellationTokenSource();

			NotifyPropertyChanged(nameof(IsRunning));
			try
			{
				await Task.Run(async () =>
				{
					Debug.WriteLine("Created Task and started it.");
					int lastTick = (int)CurrentTick;
					while (true)
					{
						UpdateTick();
						//Debug.WriteLine($"{CurrentTick} / {EndOfProject}");
						foreach (var bce in Conductor.GetEventRange(lastTick, (int)CurrentTick))
						{
							if (bce is BeatEvent)
								CurrentBeat = bce as BeatEvent;
							if (bce is TempoEvent)
								CurrentBPM = (bce as TempoEvent).Tempo;
						}
						NotifyPropertyChanged(nameof(MusicTime));
						lastTick = (int)CurrentTick;
						if ((int)CurrentTick >= EndOfProject)
							break;
						await Task.Delay(1);
					}
				}, cts.Token);
			}
			catch (OperationCanceledException)
			{
				Debug.WriteLine("Music has been canceled.");
			}
			finally
			{
				cts = null;

				NotifyPropertyChanged(nameof(IsRunning));
				Debug.WriteLine("Playing process has been finished.");
				Debug.WriteLine($"CurrentTick: {CurrentTick}");
			}

		}

		

		public void Rewind()
		{
			MusicTime -= new MusicTime(-1, 1, 0);
		}

		public void Next()
		{
			MusicTime = new MusicTime(MusicTime.Measure + 1, 1, 0);
		}

		public void Previous()
		{
			if (MusicTime.Beat == 1 && MusicTime.Tick == 0)
				Rewind();
			else
				MusicTime = new MusicTime(MusicTime.Measure, 1, 0);
		}

		public void Seek(int tick)
		{
			CurrentTick = tick;
			SetCurrent();
		}

		public void SetCurrent()
		{
			CurrentBPM = Conductor.Events.OfType<TempoEvent>().LastOrDefault(e => e.Tick <= CurrentTick)?.Tempo ?? 120;

			CurrentBeat = Conductor.Events.OfType<BeatEvent>().LastOrDefault(e => e.Tick <= CurrentTick);
		}

		public void Stop() => cts?.Cancel();

		public void Reset()
		{
			CurrentTick = 0;
			
		}

		public Timeline()
		{
			resolution = 480;
			Conductor = new ConductorTrack(this);
			bpm = 120;
			BindingOperations.EnableCollectionSynchronization(Players, new object());
		}

	}

	public struct MusicTime
	{
		public int Measure { get; set; }
		public int Beat { get; set; }
		public int Tick { get; set; }

		public MusicTime(int meas, int beat, int tick)
		{
			Measure = meas;
			Beat = beat;
			Tick = tick;
		}

		public static MusicTime InitialValue => new MusicTime(0, 1, 0);

		public static MusicTime operator +(MusicTime m1, MusicTime m2)
			=> new MusicTime(
				m1.Measure + m2.Measure,
				m1.Beat + m2.Beat,
				m1.Tick + m2.Tick
				);

		public static MusicTime operator -(MusicTime m1, MusicTime m2)
			=> new MusicTime(
				m1.Measure - m2.Measure,
				m1.Beat - m2.Beat,
				m1.Tick - m2.Tick
				);

	}

}