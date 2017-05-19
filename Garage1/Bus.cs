using System;

namespace Garage1
{
	class Bus : Vehicle
	{
		public int Seats { get; set; }

		public Bus() : base()
		{
			Seats = 1;
		}

		public override void EnterData()
		{
			base.EnterData();
			Seats = GarageHandler.InputNumeric("Number of seats:");
		}

		public override string Stats()
		{
			return String.Format("{0}Number of seats:  {1}\n", base.Stats(), Seats);
		}

		public override bool MatchNumeric(int value)
		{
			return base.MatchNumeric(value) || (Seats == value);
		}
	}
}
