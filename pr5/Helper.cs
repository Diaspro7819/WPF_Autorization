using pr5.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr5
{
	public class Helper
	{

		private static Lz_CompanyEntities s_firstDBEntities;

		public static Lz_CompanyEntities GetContext()
		{
			if (s_firstDBEntities == null)
			{
				s_firstDBEntities = new Lz_CompanyEntities();
			}
			return s_firstDBEntities;
		}
		public void CreateUsers(User user)
		{
			s_firstDBEntities.User.Add(user);
			s_firstDBEntities.SaveChanges();
		}
		public void CreateWorkers(Workers worker)
		{
			s_firstDBEntities.Workers.Add(worker);
			s_firstDBEntities.SaveChanges();
		}
		public void PrintU()
		{
			var users = s_firstDBEntities.User.ToList();
			foreach (var user in users)
			{
				Console.WriteLine($"ID: {user.User_id}, Login: {user.Login}, Password: {user.Password}");
			}
		}
		public void UpdateUsers(User user)
		{
			s_firstDBEntities.Entry(user).State = EntityState.Modified;
			s_firstDBEntities.SaveChanges();
		}
		public void FiltrWorkers()
		{
			var workers = s_firstDBEntities.Workers.Where(x => x.Name.StartsWith("А") || x.Name.StartsWith("М"));
		}
		public void SortWorkers()
		{
			var workers = s_firstDBEntities.Workers.OrderBy(x => x.Name);
		}
	}
}
