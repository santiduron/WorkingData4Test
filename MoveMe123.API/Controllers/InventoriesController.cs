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
    public class InventoriesController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/Inventories
		public IHttpActionResult GetInventories()
		{
			var resultSet = db.Inventorys.Select(inventory => new
			{
				inventory.InventoryId,
				inventory.EquipmentId,
				inventory.CompanyId,
				inventory.Price
			});
			return Ok(resultSet);
		}

		// GET: api/Inventories/5
		[ResponseType(typeof(Inventory))]
		public IHttpActionResult GetInventory(int id)
		{
			Inventory i = db.Inventorys.Find(id);
			if (i == null)
			{
				return NotFound();
			}

			var resultSet = db.Inventorys.Select(inventory => new
			{
				inventory.InventoryId,
				inventory.EquipmentId,
				inventory.CompanyId,
				inventory.Price
			});
			return Ok(resultSet);
		}

		// PUT: api/Inventories/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutInventory(int id, Inventory inventory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != inventory.InventoryId)
			{
				return BadRequest();
			}

			var dbInventory = db.Inventorys.Find(id);
			dbInventory.InventoryId = inventory.InventoryId;
			dbInventory.EquipmentId = inventory.EquipmentId;
			dbInventory.CompanyId = inventory.CompanyId;
			dbInventory.Price = inventory.Price;

			db.Entry(dbInventory).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!InventoryExists(id))
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

		// POST: api/Inventories
		[ResponseType(typeof(Inventory))]
		public IHttpActionResult PostInventory(Inventory inventory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Inventorys.Add(inventory);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = inventory.InventoryId }, inventory);
		}

		// DELETE: api/Inventories/5
		[ResponseType(typeof(Inventory))]
		public IHttpActionResult DeleteInventory(int id)
		{
			Inventory i = db.Inventorys.Find(id);
			if (i == null)
			{
				return NotFound();
			}

			db.Inventorys.Remove(i);
			db.SaveChanges();

			var resultSet = db.Inventorys.Select(inventory => new
			{
				inventory.InventoryId,
				inventory.EquipmentId,
				inventory.CompanyId,
				inventory.Price
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

		private bool InventoryExists(int id)
		{
			return db.Inventorys.Count(e => e.InventoryId == id) > 0;
		}
	}
}