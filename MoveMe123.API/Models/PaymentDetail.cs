using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class PaymentDetail
	{
		public int PaymentDetailId { get; set; }
		public int CustomerId { get; set; }
		public string CCNumber { get; set; }
		public string ExpDate { get; set; }
		public string CCV { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		// Navigation

		public virtual Customer Customer { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}