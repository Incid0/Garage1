using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1
{
	class Vehicle
	{
		public string RegNr { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string Color { get; set; }
		public int Wheels { get; set; }

		public virtual void EnterData()
		{
			Console.Write("Brand: ");
			Brand = Console.ReadLine();
			Console.Write("Model: ");
			Model = Console.ReadLine();
			Console.Write("Color: ");
			Color = Console.ReadLine();
			Wheels = GarageHandler.InputNumeric("Wheels:");
		}

		public virtual string Stats()
		{
			return String.Format("\n{0}\n{1}\n{2}\nRegnr:            {3}\nBrand:            {4}\nModel:            {5}\nColor:            {6}\nWheels:           {7}\n",
				new String('=', 30), this.GetType().Name, new String('-', 30), RegNr, Brand, Model, Color, Wheels);
		}

		public virtual bool Match(string pattern)
		{
			return RegNr.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
					Brand.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
					Model.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
					Color.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0;
		}

		public virtual bool MatchNumeric(int value)
		{
			return (Wheels == value);
		}
	}
}
