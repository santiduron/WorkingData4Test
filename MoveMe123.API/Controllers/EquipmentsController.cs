using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MoveMe123.API.Data;
using MoveMe123.API.Models;

namespace MoveMe123.API.Controllers
{
    public class EquipmentsController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/Equipments
		public IHttpActionResult GetEquipments()
		{
			var resultSet = db.Equipments.Select(equipment => new
			{
				equipment.EquipmentId,
				equipment.Tool
			});
			return Ok(resultSet);
		}

		// GET: api/Equipments/5
		[ResponseType(typeof(Equipment))]
		public IHttpActionResult GetEquipment(int id)
		{
			Equipment e = db.Equipments.Find(id);
			if (e == null)
			{
				return NotFound();
			}

			var resultSet = db.Equipments.Select(equipment => new
			{
				equipment.EquipmentId,
				equipment.Tool
			});
			return Ok(resultSet);
		}

		// PUT: api/Equipments/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutEquipment(int id, Equipment equipment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != equipment.EquipmentId)
			{
				return BadRequest();
			}

			var dbEquipment = db.Equipments.Find(id);
			dbEquipment.EquipmentId = equipment.EquipmentId;
			dbEquipment.Tool = equipment.Tool;

			db.Entry(dbEquipment).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EquipmentExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Equipments
		[ResponseType(typeof(Equipment))]
		public IHttpActionResult PostEquipment(Equipment equipment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Equipments.Add(equipment);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = equipment.EquipmentId }, equipment);
		}

		// DELETE: api/Equipments/5
		[ResponseType(typeof(Equipment))]
		public IHttpActionResult DeleteEquipment(int id)
		{
			Equipment e = db.Equipments.Find(id);
			if (e == null)
			{
				return NotFound();
			}

			db.Equipments.Remove(e);
			db.SaveChanges();

			var resultSet = db.Equipments.Select(equipment => new
			{
				equipment.EquipmentId,
				equipment.Tool
			});
			return Ok(resultSet);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool EquipmentExists(int id)
		{
			return db.Equipments.Count(e => e.EquipmentId == id) > 0;
		}
	}
}