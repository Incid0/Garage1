using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1 {
	class Garage<T> : IEnumerable<T> where T : Vehicle {
		private int capacity;
		private int count = 0;
		private Vehicle[] arrVehicle;

		private int _IndexOf(string RegNr) {
			return 0;
		}

		public Garage(int cap = 3) {
			capacity = cap;
			arrVehicle = new Vehicle[cap];
		}

		public void Add(Vehicle vehicle) {
			if (count < capacity) {
				arrVehicle[count++] = vehicle;
			}
			var x = new List<string>();
		}

		public bool Remove(string RegNr) {
			var index = _IndexOf(RegNr);
			if (index < 0) {
				
			}
			return true;
		}

		public IEnumerator<T> GetEnumerator() {
			for(var i = 0; i < count; i++) {
				yield return (T)arrVehicle[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
