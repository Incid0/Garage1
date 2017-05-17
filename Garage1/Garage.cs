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
		private T[] arrVehicle;

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
			arrVehicle = new T[cap];
		}

		public bool Add(T vehicle) {
			bool result = (count < capacity);
			if (result) {
				arrVehicle[count++] = vehicle;
			}
			return result;
		}

		public bool Remove(int index)
		{
			if (index < 0) return false;
			while (index < count && index < capacity)
			{
				arrVehicle[index] = arrVehicle[++index];
			}
			count--;
			return true;
		}

		public bool Remove(string RegNr) {
			return Remove(_IndexOf(RegNr));
		}

		public IEnumerator<T> GetEnumerator() {
			for(int i = 0; i < count; i++) {
				yield return (T)arrVehicle[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public IEnumerable<T> Search(int wheel = Int32.MaxValue)
		{
			return this.Where(x => x.Wheels < wheel);
		}

		public T Find(string RegNr)
		{
			T result = null;
			int index = _IndexOf(RegNr);
			if (index >= 0)
				result = arrVehicle[index];
			return result;
		}
	}
}
