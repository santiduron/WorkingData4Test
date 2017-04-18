using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class Equipment
	{
		public int EquipmentId { get; set; }
		public string Tool { get; set; }

		// Navigation

		public virtual ICollection<Inventory> Inventorys { get; set; }
		public virtual ICollection<RequiredEquipment> RequiredEquipments { get; set; }
	}
}