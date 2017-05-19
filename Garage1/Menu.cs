using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage1
{
	class MenuItem
	{
		private string _name;

		internal Action<int> MenuAction { get; set; }
		internal Menu SubMenu { get; set; }
		internal string Name { get => (SubMenu != null) ? SubMenu.Title : _name; set => _name = value; }

		public void Call(int index)
		{
			if (SubMenu != null)
				SubMenu.Run();
			else
				MenuAction?.Invoke(index);
		}
	}

	class Menu
	{
		private List<MenuItem> _items;
		internal Menu Parent { get; set; }
		public string Title { get; set; }
		internal Func<string> StatusCallback;

		public Menu()
		{
			_items = new List<MenuItem>();
		}

		public Menu(string title) : this()
		{
			Title = title;
		}

		public Menu(string title, Func<string> callback) : this(title)
		{
			StatusCallback = callback;
		}

		static char GetChoice(int max)
		{
			char result;
			do
				result = Console.ReadKey(true).KeyChar;
			while (result < '0' || result > ('0' + max));
			return result;
		}

		public Menu Add(string choice)
		{
			_items.Add(new MenuItem() { Name = choice });
			return this;
		}

		public Menu Add(string choice, Action<int> menuaction)
		{
			_items.Add(new MenuItem() { Name = choice, MenuAction = menuaction });
			return this;
		}

		public Menu Add(Menu submenu)
		{
			_items.Add(new MenuItem() { SubMenu = submenu });
			submenu.Parent = this;
			submenu.StatusCallback = this.StatusCallback;
			return this;
		}

		public void Run()
		{
			int choice;
			do
			{
				Console.Clear();
				Console.WriteLine(StatusCallback?.Invoke());
				Console.WriteLine();
				Console.WriteLine(Title);
				Console.WriteLine(new String('=', 40));
				for (int i = 0; i < _items.Count(); i++)
				{
					Console.WriteLine("{0} - {1}", i + 1, _items[i].Name);
				}
				Console.WriteLine(Parent != null?"\n0 - To go back\n": "\n0 - To exit program\n");
				choice = (int)GetChoice(_items.Count()) - 48;
				if (choice > 0)
				{
					_items[choice - 1].Call(choice - 1);
				}
			} while (choice != 0);
		}
	}
}
