using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// Pianoroll.xaml の相互作用ロジック
	/// </summary>
	public partial class Pianoroll : UserControl, INotifyPropertyChanged
	{
		public Pianoroll()
		{
			InitializeComponent();
			
		}

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public int NoteHeight
		{
			get { return (int)GetValue(NoteHeightProperty); }
			set { SetValue(NoteHeightProperty, value); }
		}

		public int OctaveHeight => NoteHeight * 12;


		public Thickness TopMargin => new Thickness(0, -NoteHeight * 4, 0, 0);

		private readonly int[] octaves = {
			9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1
		};

		public int[] Octaves => octaves;

		// Using a DependencyProperty as the backing store for NoteHeight.  This enables animation, styling, binding, etc...
		
		public static readonly DependencyProperty NoteHeightProperty =
			DependencyProperty.Register(nameof(NoteHeight), typeof(int), typeof(Pianoroll), new PropertyMetadata(12, new PropertyChangedCallback(OnNoteHeightChanged)));

		private static void OnNoteHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Pianoroll)d).OnPropertyChanged(nameof(OctaveHeight));
			((Pianoroll)d).OnPropertyChanged(nameof(TopMargin));
		}

		public int NoteWidth
		{
			get { return (int)GetValue(NoteWidthProperty); }
			set { SetValue(NoteWidthProperty, value); }
		}

		// Using a DependencyProperty as the backing store for NoteWidth.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty NoteWidthProperty =
			DependencyProperty.Register(nameof(NoteWidth), typeof(int), typeof(Pianoroll), new PropertyMetadata(36, new PropertyChangedCallback(OnNoteWidthChanged)));

		public event PropertyChangedEventHandler PropertyChanged;

		private static void OnNoteWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			
		}



		

	}
}
