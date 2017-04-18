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
    public class RequiredEquipmentsController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/RequiredEquipments
		public IHttpActionResult GetRequiredEquipments()
		{
			var resultSet = db.RequiredEquipments.Select(requiredEquipment => new
			{
				requiredEquipment.RequiredEquipmentId,
				requiredEquipment.JobDetailId,
				requiredEquipment.EquipmentId,
			});
			return Ok(resultSet);
		}

		// GET: api/RequiredEquipments/5
		[ResponseType(typeof(RequiredEquipment))]
		public IHttpActionResult GetRequiredEquipment(int id)
		{
			RequiredEquipment r = db.RequiredEquipments.Find(id);
			if (r == null)
			{
				return NotFound();
			}

			var resultSet = db.RequiredEquipments.Select(requiredEquipment => new
			{
				requiredEquipment.RequiredEquipmentId,
				requiredEquipment.JobDetailId,
				requiredEquipment.EquipmentId,
			});
			return Ok(resultSet);
		}

		// PUT: api/RequiredEquipments/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRequiredEquipment(int id, RequiredEquipment requiredEquipment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != requiredEquipment.RequiredEquipmentId)
			{
				return BadRequest();
			}
			var dbRequiredEquipment = db.RequiredEquipments.Find(id);
			dbRequiredEquipment.RequiredEquipmentId = requiredEquipment.RequiredEquipmentId;
			dbRequiredEquipment.JobDetailId = requiredEquipment.JobDetailId;
			dbRequiredEquipment.EquipmentId = requiredEquipment.EquipmentId;
			db.Entry(dbRequiredEquipment).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RequiredEquipmentExists(id))
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

		// POST: api/RequiredEquipments
		[ResponseType(typeof(RequiredEquipment))]
		public IHttpActionResult PostRequiredEquipment(RequiredEquipment requiredEquipment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.RequiredEquipments.Add(requiredEquipment);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = requiredEquipment.RequiredEquipmentId }, requiredEquipment);
		}

		// DELETE: api/RequiredEquipments/5
		[ResponseType(typeof(RequiredEquipment))]
		public IHttpActionResult DeleteRequiredEquipment(int id)
		{
			RequiredEquipment r = db.RequiredEquipments.Find(id);
			if (r == null)
			{
				return NotFound();
			}

			db.RequiredEquipments.Remove(r);
			db.SaveChanges();

			var resultSet = db.RequiredEquipments.Select(requiredEquipment => new
			{
				requiredEquipment.RequiredEquipmentId,
				requiredEquipment.JobDetailId,
				requiredEquipment.EquipmentId,
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

		private bool RequiredEquipmentExists(int id)
		{
			return db.RequiredEquipments.Count(e => e.RequiredEquipmentId == id) > 0;
		}
	}
}