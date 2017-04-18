using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class Inventory
	{
		public int InventoryId { get; set; }
		public decimal Price { get; set; }
		public int CompanyId { get; set; }
		public int EquipmentId { get; set; }

		//Navigation

		public virtual Equipment Equipment { get; set; }
		public virtual Company Company { get; set; }
	}
}