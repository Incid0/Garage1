using System;

namespace Garage1
{
	class Airplane : Vehicle
	{
		public int Wingspan { get; set; }

		public Airplane() : base()
		{
			Wingspan = 50;
		}

		public override void EnterData()
		{
			base.EnterData();
			Wingspan = GarageHandler.InputNumeric("Wingspan:");
		}

		public override string Stats()
		{
			return String.Format("{0}Wingspan:         {1}m\n", base.Stats(), Wingspan);
		}

		public override bool Match(string pattern)
		{
			return base.Match(pattern) || (Wingspan.ToString().IndexOf(pattern) >= 0);
		}
	}
}
