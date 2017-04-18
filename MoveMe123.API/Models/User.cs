using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }

		// Navigation

		public virtual Customer Customer { get; set; }
		public virtual Company Company { get; set; }
	}
}