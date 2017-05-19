using System;

namespace Garage1
{
	class Motorcycle : Vehicle
	{
		public int CylVolume { get; set; }

		public Motorcycle() : base()
		{
			Wheels = 2;
			CylVolume = 50;
		}

		public override void EnterData()
		{
			base.EnterData();
			CylVolume = GarageHandler.InputNumeric("Cylinder volume:");
		}

		public override string Stats()
		{
			return String.Format("{0}Cylinder volume:  {1}cc\n", base.Stats(), CylVolume);
		}

		public override bool MatchNumeric(int value)
		{
			return base.MatchNumeric(value) || (CylVolume == value);
		}
	}
}
