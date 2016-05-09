using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Groorine
{
	public class DecibelConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 20 * Math.Log10((double)value);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Math.Pow(10, (double)value / 20);
	}
}
