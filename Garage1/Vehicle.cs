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

		public virtual string Stats() {
			return String.Format("\n{0}\n==========\nRegnr:  {1}\nWheels: {2}\n", this.GetType().Name, regnr, wheels);
		}
	}

	class Motorcycle : Vehicle
	{
		public int CylVolume { get; set; }

		public Motorcycle() : base()
		{
			Wheels = 2;
			CylVolume = 50;
		}

		public override string Stats()
		{
			return String.Format("{0}Cylinder volume: {1}cc\n", base.Stats(), CylVolume);
		}

	}
}
