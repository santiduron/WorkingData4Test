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
    public class UsersController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/Users
		public IHttpActionResult GetUsers()
		{
			var resultSet = db.Users.Select(user => new
			{
				user.UserId,
				user.EmailAddress,
				user.Password,
				user.Company,
				user.Customer
			});
			return Ok(resultSet);
		}

		// GET: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult GetUser(int id)
		{
			User u = db.Users.Find(id);
			if (u == null)
			{
				return NotFound();
			}

			var resultSet = db.Users.Select(user => new
			{
				user.UserId,
				user.EmailAddress,
				user.Password,
				user.Company,
				user.Customer
			});
			return Ok(resultSet);
		}

		// PUT: api/Users/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutUser(int id, User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != user.UserId)
			{
				return BadRequest();
			}

			var dbUser = db.Users.Find(id);
			dbUser.UserId = user.UserId;
			dbUser.EmailAddress = user.EmailAddress;
			dbUser.Password = user.Password;
			dbUser.Company = user.Company;
			dbUser.Customer = user.Customer;

			db.Entry(dbUser).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
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

		// POST: api/Users
		[ResponseType(typeof(User))]
		public IHttpActionResult PostUser(User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Users.Add(user);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
		}

		// DELETE: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult DeleteUser(int id)
		{
			User u = db.Users.Find(id);
			if (u == null)
			{
				return NotFound();
			}

			db.Users.Remove(u);
			db.SaveChanges();

			var resultSet = db.Users.Select(user => new
			{
				user.UserId,
				user.EmailAddress,
				user.Password,
				user.Company,
				user.Customer
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

		private bool UserExists(int id)
		{
			return db.Users.Count(e => e.UserId == id) > 0;
		}
	}
}