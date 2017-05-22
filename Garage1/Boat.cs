using System;

namespace Garage1
{
	class Boat : Vehicle
	{
		public int Length { get; set; }

		public Boat() : base()
		{
			Length = 0;
		}

		public override void EnterData()
		{
			base.EnterData();
			Length = GarageHandler.InputNumeric("Length:");
		}

		public override string Stats()
		{
			return String.Format("{0}Length:           {1}m\n", base.Stats(), Length);
		}

		public override bool Match(string pattern)
		{
			return base.Match(pattern) || (Length.ToString().IndexOf(pattern) >= 0);
		}
	}
}
