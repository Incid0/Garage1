using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1
{
	class GarageHandler
	{
		private Garage<Vehicle> MyGarage;
		private List<Type> vehiclelist;

		public static string InputRegNr(string label)
		{
			string result = "";
			int oldRow = Console.CursorTop;

			return result;
		}

		public static int InputNumeric(string label)
		{
			int result = 0;
			int oldRow = Console.CursorTop;
			string input;
			do
			{
				Console.CursorTop = oldRow;
				Console.Write("\r{0}\r{1} ", new String(' ', Console.WindowWidth - 1), label);
				input = Console.ReadLine();
				if (int.TryParse(input, out result))
					break;
				Console.Write("Not a correct input! Try again.");
			}
			while (true);
			Console.Write("\r{0}\r", new String(' ', Console.WindowWidth - 1));
			return result;
		}

		private string ShowCapacity()
		{
			string captext = "Capacity: No Capacity";
			if (MyGarage.Capacity > 0)
				captext = String.Format("Capacity: {0} / {1}", MyGarage.Count(), MyGarage.Capacity);
			return String.Format("Garage v1.0{0}{1,30}", new string(' ', Console.WindowWidth - 42), captext);
		}

		private void SetupGarage(int notused)
		{
			int newcap = InputNumeric("Enter the capacity of the garage:");
			MyGarage = new Garage<Vehicle>(newcap);
		}

		private void ListParkedVehicles(int notused)
		{
			foreach (var vehicle in MyGarage)
			{
				Console.WriteLine(vehicle.Stats());
			}

			Console.ReadKey();
		}

		private void ListCountedCategories(int notused)
		{
			Console.WriteLine("{0,-15} : {1,6}", "Vehicletype", "Amount");
			Console.WriteLine(new String('=', 24));
			foreach (var type in MyGarage.Types())
			{
					Console.WriteLine("{0,-15} : {1,6}", type.Name, type.Count);
			}

			Console.ReadKey();
		}

		private void AddVehicles(int notused)
		{
			MyGarage?.Add(new Vehicle() { RegNr = "ABC123", Wheels = 4 });
			MyGarage?.Add(new Vehicle() { RegNr = "XXX666", Wheels = 2 });
			MyGarage?.Add(new Motorcycle() { RegNr = "ZOO123" });
			MyGarage?.Add(new Vehicle() { RegNr = "TXT000", Wheels = 10 });
		}

		public void SetupMenu()
		{
			MyGarage = new Garage<Vehicle>();

			// Build the list with vehicleclasses and a create a menu for them
			var baseType = typeof(Vehicle);
			var assembly = baseType.Assembly;
			vehiclelist = (List<Type>)assembly.GetTypes().Where(t => t == baseType || t.IsSubclassOf(baseType));
			Menu NewVehicleMenu = new Menu("Park a vehicle in the garage");
			foreach(var vehicle in vehiclelist)
			{
				NewVehicleMenu.Add(vehicle.Name);
			}

			foreach (var vehicle in MyGarage.Search(wheel: 5))
			{
				Console.WriteLine(vehicle.Stats());
			}

//			MyGarage.Remove("XXX666");


			Menu MyMenu;// = new Menu();
			(MyMenu = new Menu("Mainmenu", ShowCapacity))
							.Add("Create garage (old one will be replaced)", SetupGarage)
							.Add("Add some temporary vehicles to garage", AddVehicles)
							.Add(NewVehicleMenu)
							.Add("Show all currently parked vehicles", ListParkedVehicles)
							.Add("Show amount of different parked vehicles", ListCountedCategories)
							.Run();
							//.Add("Add some temporary vehicles to garage", (x) =>
							// {
							//	 Console.WriteLine("You picked {0}!", x);
							//	 Console.ReadLine();
							// })


			foreach (var veh in vehiclelist)
			{
				//Console.WriteLine(typ.Name);
				var instance = Activator.CreateInstance(veh);
				Console.WriteLine(((Vehicle)instance).Stats());
				//Console.WriteLine(instance.GetType().FullName);
			}
		}
	}
}
