using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class RequiredEquipment
	{
		public int RequiredEquipmentId { get; set; }
		public int JobDetailId { get; set; }
		public int EquipmentId { get; set; }

		// Navigation 

		public virtual Equipment Equipment { get; set; }
		public virtual JobDetail JobDetail { get; set; }
	}
}