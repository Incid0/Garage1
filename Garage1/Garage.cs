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
			for(int i = 0; i < count; i++) {
				if (arrVehicle[i].RegNr == RegNr) {
					return i;
				}
			}
			return -1;
		}

		public Garage(int cap = 3) {
			capacity = cap;
			arrVehicle = new Vehicle[cap];
		}

		public void Add(Vehicle vehicle) {
			if (count < capacity) {
				arrVehicle[count++] = vehicle;
			}
		}

		public bool Remove(string RegNr) {
			int index = _IndexOf(RegNr);
			if (index < 0) return false;
			while (index < count && index < capacity) {
				arrVehicle[index] = arrVehicle[++index];
			}
			count--;
			return true;
		}

		public IEnumerator<T> GetEnumerator() {
			for(int i = 0; i < count; i++) {
				yield return (T)arrVehicle[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
