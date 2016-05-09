using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.ComponentModel;

namespace Groorine.Controls
{
	public partial class Dial  : UserControl, INotifyPropertyChanged
	{
		private Point startPosition;
		private Point currentPosition;
		private int startValue;
		private bool isRotating;
		public static readonly DependencyProperty ValueProperty;

		public static readonly DependencyProperty MaxValueProperty;

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public double Angle => Math.Round(Value * (150.0 / MaxValue));

		public int MaxValue
		{
			get { return (int)this.GetValue(MaxValueProperty); }
			set
			{
				this.SetValue(MaxValueProperty, value);
				OnPropertyChanged(nameof(Angle));
			}
		}

		public int Value
		{
			get
			{
				return (int)this.GetValue(ValueProperty);
			}
			set
			{
				this.SetValue(ValueProperty, value);
				OnPropertyChanged(nameof(Angle));
			}
		}

		//---------------------------------------------------------------------------------------------
		static Dial()
		{
			
			MaxValueProperty = DependencyProperty.Register(nameof(MaxValue), typeof(int), typeof(Dial),
				new FrameworkPropertyMetadata(128, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (a, b) => ((Dial)a).OnPropertyChanged(nameof(Angle)), (d, baseValue) => baseValue));

			ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(Dial),
				new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (a, b) => ((Dial)a).OnPropertyChanged(nameof(Angle)), (d, baseValue) =>
				{
					var val = (int)baseValue;
					var max = (int)((Dial)d).MaxValue;
					if (val >= max || val < -max)
						throw new ArgumentOutOfRangeException();
					return val;
				}));

		}

		//---------------------------------------------------------------------------------------------
		public Dial()
		{
			InitializeComponent();
		}

		//---------------------------------------------------------------------------------------------
		private static object coerceValueCallback(DependencyObject d, object baseValue)
		{
			var angle = (double)baseValue;

			if (angle >= 150)
				return 149;
			if (angle < -150)
				return -150;
			return angle;
		}

		//---------------------------------------------------------------------------------------------
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			Console.WriteLine(nameof(OnMouseDown));
			startPosition = e.GetPosition(this);
			startValue = Value;
			this.Focus();
			CaptureMouse();
			isRotating = true;
		}

		//---------------------------------------------------------------------------------------------
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Console.WriteLine(nameof(OnMouseMove));
			if (isRotating)
			{
				currentPosition = e.GetPosition(this);
				int movex = (int)(currentPosition.X - startPosition.X);
				int movey = (int)(startPosition.Y - currentPosition.Y);
				int tmp = startValue + movex + movey;
				if (tmp >= MaxValue)
					tmp = MaxValue - 1;
				if (tmp < -MaxValue)
					tmp = -MaxValue;
				Value = tmp;
			}
		}

		//---------------------------------------------------------------------------------------------
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			Console.WriteLine(nameof(OnMouseUp));
			if (isRotating)
			{
				isRotating = false;
				ReleaseMouseCapture();
			}
		}

		//---------------------------------------------------------------------------------------------
		protected override void OnLostMouseCapture(MouseEventArgs e)
		{
			if (isRotating)
				CaptureMouse();
		}

		//---------------------------------------------------------------------------------------------
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			Console.WriteLine(nameof(OnRenderSizeChanged));
			base.OnRenderSizeChanged(sizeInfo);
		}

		//---------------------------------------------------------------------------------------------
	} // end of RotateAngleControl class
} // end of emanual.Wpf.Controls namespace
