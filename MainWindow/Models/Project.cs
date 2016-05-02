using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groorine.Models
{
	public class Project
	{
		public MetaData MetaData { get; } = new MetaData();
		public Timeline Timeline { get; } = new Timeline();
	}
}
