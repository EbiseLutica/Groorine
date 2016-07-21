using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groorine.Utility
{
	public class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string info)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
		}
	}
}
