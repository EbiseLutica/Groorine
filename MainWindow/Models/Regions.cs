using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Data;
using Groorine.Utility;

namespace Groorine.Models
{
	public abstract class Region : NotifyPropertyChangedBase
	{
		private int tick;
		private int length;
		private int actualLength;
		private bool loop;
		private string name;

		public int Tick
		{
			get
			{
				return tick;
			}
			set
			{
				tick = value;
				NotifyPropertyChanged(nameof(Tick));
			}
		}
		public int Length
		{
			get
			{
				return length;
			}
			set
			{
				length = value;
				NotifyPropertyChanged(nameof(Length));
			}
		}

		public int ActualLength
		{
			get { return actualLength; }
			set
			{
				actualLength = value;
				NotifyPropertyChanged(nameof(ActualLength));
			}
		}

		public bool Loop
		{
			get
			{
				return loop;
			}
			set
			{
				loop = value;
				NotifyPropertyChanged(nameof(Loop));
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

	}

	public class MidiRegion : Region
	{
		public ObservableCollection<BasePlayerEvent> EventList { get; } = new ObservableCollection<BasePlayerEvent>();

		public MidiRegion()
		{
			BindingOperations.EnableCollectionSynchronization(EventList, new object());
		}
		/// <summary>
		/// このリージョンの特定の範囲にあるイベントを取得します。
		/// </summary>
		/// <param name="starttick">取得範囲の始点のTick。タイムラインの絶対時間を指定してください。</param>
		/// <param name="endtick">取得範囲の終点のTick。タイムラインの絶対時間を指定してください。</param>
		/// <returns></returns>
		public IEnumerable<BasePlayerEvent> GetEventRange(int starttick, int endtick) => from bpe in EventList
																						 where starttick - Tick <= bpe.Tick && bpe.Tick <= endtick - Tick
																						 select bpe;

	}

	public class AudioRegion : Region
	{

	}

}
