using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;

namespace Garage1 {
	class Program {
		static void Main(string[] args)
		{
			var handler = new GarageHandler();
			handler.SetupMenu();
		}
	}
}
