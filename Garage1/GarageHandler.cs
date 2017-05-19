using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage1
{
	class GarageHandler
	{
		private Garage<Vehicle> MyGarage;
		private List<Type> vehiclelist;

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

		public static void WaitForKey()
		{
			Console.Write("\nPress any key to continue...");
			Console.ReadKey(false);
			Console.WriteLine();
		}

		public string InputRegNr()
		{
			string result = "";
			int oldRow = Console.CursorTop;
			Regex pattern = new Regex(@"^[a-zA-Z]{3}\d{3}$");
			do
			{
				Console.CursorTop = oldRow;
				Console.Write("\r{0}\rRegNr (XXX###): ", new String(' ', Console.WindowWidth - 1));
				result = Console.ReadLine();
				if (pattern.IsMatch(result))
				{
					if (MyGarage.Find(result) == null)
						break;
					Console.Write("This regnr is already parked in the garage! Try again.");
				}
				else
				{
					Console.Write("Wrong format for a regnr! Try again.");
				}
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
			return String.Format("Garage v1.0{0}{1,30}", new string(' ', Console.WindowWidth - 41), captext);
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
			WaitForKey();
		}

		private void ListCountedCategories(int notused)
		{
			Console.WriteLine("{0,-15} : {1,6}", "Vehicletype", "Amount");
			Console.WriteLine(new String('=', 24));
			foreach (var type in MyGarage.Types())
			{
				Console.WriteLine("{0,-15} : {1,6}", type.Name, type.Count);
			}
			WaitForKey();
		}

		private void AddRandomVehicles(int notused)
		{
			MyGarage?.Add(new Vehicle() { RegNr = "ABC123", Brand = "Generic", Model = "Oddball", Color = "Pink", Wheels = 4 });
			MyGarage?.Add(new Airplane() { RegNr = "XXX666", Brand = "Boing", Model = "747", Color = "White", Wheels = 12 });
			MyGarage?.Add(new Motorcycle() { RegNr = "ZOO123", Brand = "Yamaha", Model = "CR80", Color = "Red", CylVolume = 80 });
			MyGarage?.Add(new Boat() { RegNr = "TXT000", Brand = "Otter", Model = "T500", Color = "Orange", Wheels = 0, Length = 5 });
			MyGarage?.Add(new Car() { RegNr = "ZOO567", Brand = "Volvo", Model = "V70", Color = "Silver", FuelType = "Gasoline" });
		}

		private void ParkVehicle(int index)
		{
			if (MyGarage.Count() >= MyGarage.Capacity)
			{
				Console.WriteLine("\nThere's no capacity in the garage for more vehicles at the moment!\n");
				WaitForKey();
			}
			else if (index < vehiclelist.Count)
			{
				var vehicle = (Vehicle)Activator.CreateInstance(vehiclelist[index]);
				vehicle.RegNr = InputRegNr();
				vehicle.EnterData();
				MyGarage?.Add(vehicle);
			}
		}

		private void DepartVehicle(int notused)
		{
			Console.Write("Enter Regnr that is departing: ");
			string regnr = Console.ReadLine().Trim();
			if (!MyGarage.Remove(regnr))
			{
				Console.WriteLine("\nThat vehicle was not found!\n");
			}
			else
			{
				Console.WriteLine("\nVehicle {0} has left the garage!\n", regnr);
			}
			WaitForKey();
		}

		private void SearchVehicles(int notused)
		{
			Console.Write("Enter keywords to search for: ");
			string words = Console.ReadLine().Trim();
			var result = MyGarage.Search(words).ToList();
			if (result.Count == 0)
			{
				Console.WriteLine("\nNo vehicles found!");
			}
			else
			{
				Console.WriteLine("\nFollowing vehicles matched:");
				foreach (var vehicle in result)
				{
					Console.WriteLine(vehicle.Stats());
				}
			}
			WaitForKey();
		}

		private void FindVehicle(int notused)
		{
			Console.Write("Enter Regnr to search for: ");
			string regnr = Console.ReadLine().Trim();
			var vehicle = MyGarage.Find(regnr);
			if (vehicle == null)
			{
				Console.WriteLine("\nThat vehicle isn't parked here!");
			}
			else
			{
				Console.WriteLine("\nVehicle found:");
				Console.WriteLine(vehicle.Stats());
			}
			WaitForKey();
		}

		public void SetupMenu()
		{
			MyGarage = new Garage<Vehicle>();

			// Build the list with vehicleclasses and a create a menu for them
			var baseType = typeof(Vehicle);
			var assembly = baseType.Assembly;
			vehiclelist = assembly.GetTypes().Where(t => t == baseType || t.IsSubclassOf(baseType)).ToList();
			Menu NewVehicleMenu = new Menu("Park a vehicle in the garage");
			foreach (var vehicle in vehiclelist)
			{
				NewVehicleMenu.Add(vehicle.Name, ParkVehicle);
			}

			Menu MyMenu;
			(MyMenu = new Menu("Mainmenu", ShowCapacity))
							.Add("Create garage (old one will be replaced)", SetupGarage)
							.Add(NewVehicleMenu)
							.Add("Depart a vehicle from the garage", DepartVehicle)
							.Add("Show all currently parked vehicles", ListParkedVehicles)
							.Add("Show amount of different parked vehicles", ListCountedCategories)
							.Add("Search for vehicles", SearchVehicles)
							.Add("Find a specific vehicle", FindVehicle)
							.Add("Park some temporary vehicles in garage", AddRandomVehicles)
							.Run();
		}
	}
}
