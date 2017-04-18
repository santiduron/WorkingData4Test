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
    public class JobDetailsController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/JobDetails
		public IHttpActionResult GetJobDetails()
		{
			var resultSet = db.JobDetails.Select(jobDetail => new
			{
				jobDetail.JobDetailId,
				jobDetail.CustomerId,
				jobDetail.FromStreetAddress,
				jobDetail.FromCity,
				jobDetail.FromState,
				jobDetail.FromZip,
				jobDetail.ToStreetAddress,
				jobDetail.ToCity,
				jobDetail.ToState,
				jobDetail.ToZip,
				jobDetail.MoveOut,
				jobDetail.MoveIn,
				jobDetail.NumBedroom,
				jobDetail.NumPooper,
				jobDetail.SqFeet,
				jobDetail.Stairs,
				jobDetail.Elevator,
				jobDetail.Over400,
				jobDetail.PackingAssistance,
				jobDetail.ProtectiveMaterial,
				jobDetail.NumMovers,
				jobDetail.NumHours,
				jobDetail.Distance
			});
			return Ok(resultSet);
		}

		// GET: api/JobDetails/5
		[ResponseType(typeof(JobDetail))]
		public IHttpActionResult GetJobDetail(int id)
		{
			JobDetail j = db.JobDetails.Find(id);
			if (j == null)
			{
				return NotFound();
			}

			var resultSet = db.JobDetails.Select(jobDetail => new
			{
				jobDetail.JobDetailId,
				jobDetail.CustomerId,
				jobDetail.FromStreetAddress,
				jobDetail.FromCity,
				jobDetail.FromState,
				jobDetail.FromZip,
				jobDetail.ToStreetAddress,
				jobDetail.ToCity,
				jobDetail.ToState,
				jobDetail.ToZip,
				jobDetail.MoveOut,
				jobDetail.MoveIn,
				jobDetail.NumBedroom,
				jobDetail.NumPooper,
				jobDetail.SqFeet,
				jobDetail.Stairs,
				jobDetail.Elevator,
				jobDetail.Over400,
				jobDetail.PackingAssistance,
				jobDetail.ProtectiveMaterial,
				jobDetail.NumMovers,
				jobDetail.NumHours,
				jobDetail.Distance
			});
			return Ok(resultSet);
		}

		// PUT: api/JobDetails/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutJobDetail(int id, JobDetail jobDetail)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != jobDetail.JobDetailId)
			{
				return BadRequest();
			}

			var dbJobDetail = db.JobDetails.Find(id);
			dbJobDetail.JobDetailId = jobDetail.JobDetailId;
			dbJobDetail.CustomerId = jobDetail.CustomerId;
			dbJobDetail.FromStreetAddress = jobDetail.FromStreetAddress;
			dbJobDetail.FromCity = jobDetail.FromCity;
			dbJobDetail.FromState = jobDetail.FromState;
			dbJobDetail.FromZip = jobDetail.FromZip;
			dbJobDetail.ToStreetAddress = jobDetail.ToStreetAddress;
			dbJobDetail.ToCity = jobDetail.ToCity;
			dbJobDetail.ToState = jobDetail.ToState;
			dbJobDetail.ToZip = jobDetail.ToZip;
			dbJobDetail.MoveOut = jobDetail.MoveOut;
			dbJobDetail.MoveIn = jobDetail.MoveIn;
			dbJobDetail.NumBedroom = jobDetail.NumBedroom;
			dbJobDetail.NumPooper = jobDetail.NumPooper;
			dbJobDetail.SqFeet = jobDetail.SqFeet;
			dbJobDetail.Stairs = jobDetail.Stairs;
			dbJobDetail.Elevator = jobDetail.Elevator;
			dbJobDetail.Over400 = jobDetail.Over400;
			dbJobDetail.PackingAssistance = jobDetail.PackingAssistance;
			dbJobDetail.ProtectiveMaterial = jobDetail.ProtectiveMaterial;
			dbJobDetail.NumMovers = jobDetail.NumMovers;
			dbJobDetail.NumHours = jobDetail.NumHours;
			dbJobDetail.Distance = jobDetail.Distance;

			db.Entry(dbJobDetail).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!JobDetailExists(id))
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

		// POST: api/JobDetails
		[ResponseType(typeof(JobDetail))]
		public IHttpActionResult PostJobDetail(JobDetail jobDetail)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.JobDetails.Add(jobDetail);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = jobDetail.JobDetailId }, jobDetail);
		}

		// DELETE: api/JobDetails/5
		[ResponseType(typeof(JobDetail))]
		public IHttpActionResult DeleteJobDetail(int id)
		{
			JobDetail j = db.JobDetails.Find(id);
			if (j == null)
			{
				return NotFound();
			}

			db.JobDetails.Remove(j);
			db.SaveChanges();

			var resultSet = db.JobDetails.Select(jobDetail => new
			{
				jobDetail.JobDetailId,
				jobDetail.CustomerId,
				jobDetail.FromStreetAddress,
				jobDetail.FromCity,
				jobDetail.FromState,
				jobDetail.FromZip,
				jobDetail.ToStreetAddress,
				jobDetail.ToCity,
				jobDetail.ToState,
				jobDetail.ToZip,
				jobDetail.MoveOut,
				jobDetail.MoveIn,
				jobDetail.NumBedroom,
				jobDetail.NumPooper,
				jobDetail.SqFeet,
				jobDetail.Stairs,
				jobDetail.Elevator,
				jobDetail.Over400,
				jobDetail.PackingAssistance,
				jobDetail.ProtectiveMaterial,
				jobDetail.NumMovers,
				jobDetail.NumHours,
				jobDetail.Distance
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

		private bool JobDetailExists(int id)
		{
			return db.JobDetails.Count(e => e.JobDetailId == id) > 0;
		}
	}
}