using System;
using pr3.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace pr3
{
	internal class AuthHelp
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
	}
}
