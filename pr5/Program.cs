using HashPassword;
using pr5.Model;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using pr5;

namespace pr5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var db = Helper.GetContext();
			Helper helper = new Helper();
			var user = new User();
			var worker = new Workers();
			Console.WriteLine("Введите логин: ");
			string login = Console.ReadLine();
			Console.WriteLine("Введите пароль: ");
			string password = Console.ReadLine();
			Console.WriteLine("Введите имя: ");
			string name = Console.ReadLine();
			Console.WriteLine("Введите фамилию: ");
			string sname = Console.ReadLine();
			user.Login = login;
			user.Password = Hash.Pass(password);
			helper.CreateUsers(user);
			 
			worker.Name = name;
			worker.Surname = sname;
			worker.User_id = user.User_id;
			helper.CreateWorkers(worker);

			Console.WriteLine($"{worker.Name} {worker.Surname} - {worker.User.Login} {worker.User.Password}");

			var users = db.User.FirstOrDefault(u => u.Login == "sus");
			Console.ReadKey();

		}
	}
}


