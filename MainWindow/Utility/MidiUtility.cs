using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groorine.Utility
{
	public static class MidiUtility
	{
		public static double TickToMilisecond(double bpm, int resolution, double tick) => 60000 / bpm / resolution * tick;

		public static double MilisecondToTick(double bpm, int resolution, double milisec) => bpm * resolution * milisec / 60000;

		

	}
}
