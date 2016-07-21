using Groorine.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Groorine
{
	public class PlayMonitorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var v = value as MusicTime?;
			return $"MEAS:{v?.Measure,-3} BEAT:{v?.Beat,-2} TICK:{v?.Tick,-4}";

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var v = value as string;
			MatchCollection mc = Regex.Matches(v, @"^MEAS:(\d{3,}) BEAT:(\d+) TICK:(\d{4,})$");
			
			foreach (Match m in mc)
			{
				if (m.Groups.Count != 4)
					continue;
				return new MusicTime
				{
					Measure = int.Parse(m.Groups[1].Value),
					Beat = int.Parse(m.Groups[2].Value),
					Tick = int.Parse(m.Groups[3].Value),
				};
			}
			return null;
		}
	}
}
