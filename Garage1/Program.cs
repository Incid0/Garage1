using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1 {
	class Program {
		static void Main(string[] args) {
			var MyGarage = new Garage<Vehicle>(6);
			MyGarage.Add(new Vehicle() { RegNr = "ABC123", Wheels = 4 });
			MyGarage.Add(new Vehicle() { RegNr = "XXX666", Wheels = 2 });
			MyGarage.Add(new Motorcycle() { RegNr = "ZOO123" });
			MyGarage.Add(new Vehicle() { RegNr = "TXT000", Wheels = 10 });

			foreach(var vehicle in MyGarage.Search(5)) {
				Console.WriteLine(vehicle.Stats());
			}

			MyGarage.Remove("XXX666");

			foreach (var vehicle in MyGarage)
			{
				Console.WriteLine(vehicle.Stats());
			}

			var query = from v in MyGarage
						orderby v.GetType().Name
						select v.GetType().Name;

			foreach (var type in MyGarage.Types()) {
				Console.WriteLine("{0} : {1}", type.Name, type.Count);
			}
			Console.ReadKey();
		}
	}
}
