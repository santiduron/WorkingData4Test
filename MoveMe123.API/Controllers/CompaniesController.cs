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
    public class CompaniesController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/Companies
		public IHttpActionResult GetCompanies()
		{
			var resultSet = db.Companys.Select(company => new
			{
				company.CompanyId,
				company.CompanyName,
				company.Telephone,
				company.StreetAddress,
				company.City,
				company.State,
				company.Zip,
				company.Employees,
				company.Radius,
				company.OpeningHour,
				company.ClosingHour,
				company.HourlyRate
			});


			return Ok(resultSet);
		}

		// GET: api/Companies/5
		[ResponseType(typeof(Company))]
		public IHttpActionResult GetCompany(int id)
		{
			Company c = db.Companys.Find(id);
			if (c == null)
			{
				return NotFound();
			}

			var resultSet = db.Companys.Select(company => new
			{
				company.CompanyId,
				company.CompanyName,
				company.Telephone,
				company.StreetAddress,
				company.City,
				company.State,
				company.Zip,
				company.Employees,
				company.Radius,
				company.OpeningHour,
				company.ClosingHour,
				company.HourlyRate
			});


			return Ok(resultSet);
		}

		// PUT: api/Companies/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCompany(int id, Company company)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != company.CompanyId)
			{
				return BadRequest();
			}


			var dbCompany = db.Companys.Find(id);
			dbCompany.CompanyId = company.CompanyId;
			dbCompany.CompanyName = company.CompanyName;
			dbCompany.Telephone = company.Telephone;
			dbCompany.StreetAddress = company.StreetAddress;
			dbCompany.City = company.City;
			dbCompany.State = company.State;
			dbCompany.Zip = company.Zip;
			dbCompany.Employees = company.Employees;
			dbCompany.Radius = company.Radius;
			dbCompany.OpeningHour = company.OpeningHour;
			dbCompany.ClosingHour = company.ClosingHour;
			dbCompany.HourlyRate = company.HourlyRate;
			db.Entry(dbCompany).State = EntityState.Modified;


			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CompanyExists(id))
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

		// POST: api/Companies
		[ResponseType(typeof(Company))]
		public IHttpActionResult PostCompany(Company company)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Companys.Add(company);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);
		}

		// DELETE: api/Companies/5
		[ResponseType(typeof(Company))]
		public IHttpActionResult DeleteCompany(int id)
		{
			Company c = db.Companys.Find(id);
			if (c == null)
			{
				return NotFound();
			}

			db.Companys.Remove(c);
			db.SaveChanges();

			var resultSet = db.Companys.Select(company => new
			{
				company.CompanyId,
				company.CompanyName,
				company.Telephone,
				company.StreetAddress,
				company.City,
				company.State,
				company.Zip,
				company.Employees,
				company.Radius,
				company.OpeningHour,
				company.ClosingHour,
				company.HourlyRate
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

		private bool CompanyExists(int id)
		{
			return db.Companys.Count(e => e.CompanyId == id) > 0;
		}
	}
}