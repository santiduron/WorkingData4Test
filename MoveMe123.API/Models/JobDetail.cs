using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveMe123.API.Models
{
	public class JobDetail
	{
		public int JobDetailId { get; set; }
		public int CustomerId { get; set; }
		public string FromStreetAddress { get; set; }
		public string FromCity { get; set; }
		public string FromState { get; set; }
		public string FromZip { get; set; }
		public string ToStreetAddress { get; set; }
		public string ToCity { get; set; }
		public string ToState { get; set; }
		public string ToZip { get; set; }
		public DateTime MoveOut { get; set; }
		public DateTime MoveIn { get; set; }
		public int NumBedroom { get; set; }
		public int NumPooper { get; set; }
		public int SqFeet { get; set; }
		public int Stairs { get; set; }
		public bool Elevator { get; set; }
		public bool Over400 { get; set; }
		public bool PackingAssistance { get; set; }
		public bool ProtectiveMaterial { get; set; }
		public int NumMovers { get; set; }
		public int NumHours { get; set; }
		public int Distance { get; set; }

		// Navigation

		public virtual ICollection<RequiredEquipment> RequiredEquipments { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual Order Order { get; set; }
	}
}