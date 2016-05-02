using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groorine.Models
{
	public class BasePlayerEvent : BaseEvent
	{

	}

	public class NoteEvent : BasePlayerEvent
	{
		public int Gate { get; set; }
		private int number;
		public int Number
		{
			get
			{
				return number;
			}
			set
			{
				if (value < 0 || 127 < value)
					throw new ArgumentOutOfRangeException(nameof(value));
				number = value;
				NotifyPropertyChanged(nameof(number));
			}
		}
		private int velocity;
		public int Velocity
		{
			get
			{
				return velocity;
			}
			set
			{
				if (value < 0 || 127 < value)
					throw new ArgumentOutOfRangeException(nameof(value));
				velocity = value;
				NotifyPropertyChanged(nameof(velocity));
			}

		}
	}

}
