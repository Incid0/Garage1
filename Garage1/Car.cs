using System;

namespace Garage1
{
	class Car : Vehicle
	{
		public string FuelType { get; set; }

		public Car() : base()
		{
			Wheels = 4;
			FuelType = "";
		}

		public override void EnterData()
		{
			base.EnterData();
			Console.Write("FuelType: ");
			FuelType = Console.ReadLine();
		}

		public override string Stats()
		{
			return String.Format("{0}FuelType:         {1}\n", base.Stats(), FuelType);
		}

		public override bool Match(string pattern)
		{
			return base.Match(pattern) || (FuelType.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0);
		}
	}
}
