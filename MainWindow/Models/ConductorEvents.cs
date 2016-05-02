using System;

namespace Groorine.Models
{
	/// <summary>
	/// すべての Groorine MIDIイベントの基底クラスです．
	/// </summary>
	public abstract class BaseEvent : ModelBase
	{
		private int tick;

		/// <summary>
		/// このイベントのタイムライン上の時間です．
		/// </summary>
		public int Tick
		{
			get
			{
				return tick;
			}
			set
			{
				tick = value;
				NotifyPropertyChanged(nameof(tick));
			}
		}
	}

	/// <summary>
	/// Groorine 制御イベントの基底クラスです．
	/// </summary>
	public abstract class BaseConductorEvent : BaseEvent
	{

	}

	/// <summary>
	/// テンポを変更するイベントです．
	/// </summary>
	public class TempoEvent : BaseConductorEvent
	{
		private double tempo;

		/// <summary>
		/// テンポです．
		/// </summary>
		public double Tempo
		{
			get
			{
				return tempo;
			}
			set
			{
				tempo = value;
				NotifyPropertyChanged(nameof(tempo));
			}
		}
	}

	/// <summary>
	/// 拍子を変更するイベントです．
	/// </summary>
	public class BeatEvent : BaseConductorEvent
	{
		private int numerator;
		/// <summary>
		/// 分子です．
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int Numerator
		{
			get
			{
				return numerator;
			}
			set
			{
				if (value < 1 || value > 32)
					throw new ArgumentOutOfRangeException(nameof(value));
				numerator = value;
				NotifyPropertyChanged(nameof(numerator));
			}
		}
		private int denominator;
		/// <summary>
		/// 分母です．
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int Denominator
		{
			get
			{
				return denominator;
			}
			set
			{
				if ((value & value - 1) != 0 && value < 1 || value > 32)
					throw new ArgumentOutOfRangeException(nameof(value));
				denominator = value;
				NotifyPropertyChanged(nameof(denominator));
			}
		}



	}

}