using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class Company
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public string Telephone { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public int Employees { get; set; }
		public int Radius { get; set; }
		public string OpeningHour { get; set; }
		public string ClosingHour { get; set; }
		public string[] DaysOfWeek { get; set; }
		public decimal HourlyRate { get; set; }

		//Navigation
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Inventory> Inventorys { get; set; }
		public virtual User User { get; set; }
	}
}