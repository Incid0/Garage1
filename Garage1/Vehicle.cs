using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1 {
	class Vehicle {
		private string regnr;
		private int wheels;

		public string RegNr { get => regnr; set => regnr = value; }
		public int Wheels { get => wheels; set => wheels = value; }
	}
}
