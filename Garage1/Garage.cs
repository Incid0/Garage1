using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1
{
	class TypeList
	{
		public string Name { get; set; }
		public int Count { get; set; }
	}

	class Garage<T> : IEnumerable<T> where T : Vehicle
	{
		private int capacity;
		private int count = 0;
		private T[] arrVehicle;

		public int Capacity { get => capacity; }

		private int _IndexOf(string RegNr)
		{
			RegNr = RegNr.ToUpper();
			for (int i = 0; i < count; i++)
			{
				if (arrVehicle[i].RegNr.ToUpper() == RegNr)
				{
					return i;
				}
			}
			return -1;
		}

		public Garage(int cap = 0)
		{
			capacity = cap;
			arrVehicle = new T[cap];
		}

		public bool Add(T vehicle)
		{
			bool result = (count < capacity);
			if (result)
			{
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

		public bool Remove(string RegNr)
		{
			return Remove(_IndexOf(RegNr));
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < count; i++)
			{
				yield return (T)arrVehicle[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<T> Search(string words)
		{
			IEnumerable<T> result = this;
			foreach (string word in words.Split())
			{
				result = result.Where(x => x.Match(word));
			}
			return result;
		}

		public IEnumerable<TypeList> Types()
		{
			return this.GroupBy(vehicle => vehicle.GetType().Name)
					   .Select(types => new TypeList
					   {
						   Name = types.Key,
						   Count = types.Count()
					   })
						.OrderBy(t => t.Name);
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
