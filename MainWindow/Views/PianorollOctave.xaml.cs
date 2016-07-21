using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Groorine.Views
{
	/// <summary>
	/// PianorollOctave.xaml の相互作用ロジック
	/// </summary>
	public partial class PianorollOctave : UserControl
	{
		public PianorollOctave()
		{
			InitializeComponent();
		}

		public int Octave
		{
			get { return (int)GetValue(OctaveProperty); }
			set { SetValue(OctaveProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Octave.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty OctaveProperty =
			DependencyProperty.Register(nameof(Octave), typeof(int), typeof(PianorollOctave), new PropertyMetadata(4));





	}
}
