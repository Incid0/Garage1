using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1 {
	class Program {
		static void Main(string[] args) {
			var GarageBas = new Garage<Vehicle>(6);
			GarageBas.AddVehicle(new Vehicle() { RegNr = "ABC123", Wheels = 4 });
			GarageBas.AddVehicle(new Vehicle() { RegNr = "XXX666", Wheels = 2 });
			GarageBas.AddVehicle(new Vehicle() { RegNr = "TXT000", Wheels = 10 });

			foreach(var vehicle in GarageBas) {
				Console.WriteLine(vehicle.RegNr);
				break;
			}
		}
	}
}
