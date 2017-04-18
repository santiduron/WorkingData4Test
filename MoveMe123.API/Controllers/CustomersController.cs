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
    public class CustomersController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/Customers
		public IHttpActionResult GetCustomers()
		{
			var resultSet = db.Customers.Select(customer => new
			{
				customer.FirstName,
				customer.LastName,
				customer.Telephone,
				customer.CustomerId
			});
			return Ok(resultSet);
		}

		// GET: api/Customers/5
		[ResponseType(typeof(Customer))]
		public IHttpActionResult GetCustomer(int id)
		{
			Customer c = db.Customers.Find(id);
			if (c == null)
			{
				return NotFound();
			}

			var resultSet = db.Customers.Select(customer => new
			{
				customer.FirstName,
				customer.LastName,
				customer.Telephone,
				customer.CustomerId
			});
			return Ok(resultSet);
		}

		// PUT: api/Customers/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCustomer(int id, Customer customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != customer.CustomerId)
			{
				return BadRequest();
			}

			var dbCustomer = db.Customers.Find(id);
			dbCustomer.CustomerId = customer.CustomerId;
			dbCustomer.FirstName = customer.FirstName;
			dbCustomer.LastName = customer.LastName;
			dbCustomer.Telephone = customer.Telephone;


			db.Entry(dbCustomer).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CustomerExists(id))
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

		// POST: api/Customers
		[ResponseType(typeof(Customer))]
		public IHttpActionResult PostCustomer(Customer customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Customers.Add(customer);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
		}

		// DELETE: api/Customers/5
		[ResponseType(typeof(Customer))]
		public IHttpActionResult DeleteCustomer(int id)
		{
			Customer c = db.Customers.Find(id);
			if (c == null)
			{
				return NotFound();
			}

			db.Customers.Remove(c);
			db.SaveChanges();

			var resultSet = db.Customers.Select(customer => new
			{
				customer.FirstName,
				customer.LastName,
				customer.Telephone,
				customer.CustomerId
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

		private bool CustomerExists(int id)
		{
			return db.Customers.Count(e => e.CustomerId == id) > 0;
		}
	}
}