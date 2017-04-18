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
    public class PaymentDetailsController : ApiController
    {
        private MoveMe123DataContext db = new MoveMe123DataContext();

		// GET: api/PaymentDetails
		public IHttpActionResult GetPaymentDetails()
		{
			var resultSet = db.PaymentDetails.Select(paymentDetail => new
			{
				paymentDetail.PaymentDetailId,
				paymentDetail.CustomerId,
				paymentDetail.CCNumber,
				paymentDetail.ExpDate,
				paymentDetail.CCV,
				paymentDetail.StreetAddress,
				paymentDetail.City,
				paymentDetail.State,
				paymentDetail.Zip
			});
			return Ok(resultSet);

		}

		// GET: api/PaymentDetails/5
		[ResponseType(typeof(PaymentDetail))]
		public IHttpActionResult GetPaymentDetail(int id)
		{
			PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
			if (paymentDetail == null)
			{
				return NotFound();
			}

			var resultSet =  new
			{
				paymentDetail.PaymentDetailId,
				paymentDetail.CustomerId,
				paymentDetail.CCNumber,
				paymentDetail.ExpDate,
				paymentDetail.CCV,
				paymentDetail.StreetAddress,
				paymentDetail.City,
				paymentDetail.State,
				paymentDetail.Zip	
			};
			return Ok(resultSet);
		}

		// PUT: api/PaymentDetails/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPaymentDetail(int id, PaymentDetail paymentDetail)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != paymentDetail.PaymentDetailId)
			{
				return BadRequest();
			}
			var dbPaymentDetail = db.PaymentDetails.Find(id);
			dbPaymentDetail.PaymentDetailId = paymentDetail.PaymentDetailId;
			dbPaymentDetail.CustomerId = paymentDetail.CustomerId;
			dbPaymentDetail.CCNumber = paymentDetail.CCNumber;
			dbPaymentDetail.ExpDate = paymentDetail.ExpDate;
			dbPaymentDetail.CCV = paymentDetail.CCV;
			dbPaymentDetail.StreetAddress = paymentDetail.StreetAddress;
			dbPaymentDetail.City = paymentDetail.City;
			dbPaymentDetail.State = paymentDetail.State;
			dbPaymentDetail.Zip = paymentDetail.Zip;
			db.Entry(dbPaymentDetail).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PaymentDetailExists(id))
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

		// POST: api/PaymentDetails
		[ResponseType(typeof(PaymentDetail))]
		public IHttpActionResult PostPaymentDetail(PaymentDetail paymentDetail)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.PaymentDetails.Add(paymentDetail);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = paymentDetail.PaymentDetailId }, paymentDetail);
		}

		// DELETE: api/PaymentDetails/5
		[ResponseType(typeof(PaymentDetail))]
		public IHttpActionResult DeletePaymentDetail(int id)
		{
			PaymentDetail p = db.PaymentDetails.Find(id);
			if (p == null)
			{
				return NotFound();
			}

			db.PaymentDetails.Remove(p);
			db.SaveChanges();

			var resultSet = db.PaymentDetails.Select(paymentDetail => new
			{
				paymentDetail.PaymentDetailId,
				paymentDetail.CustomerId,
				paymentDetail.CCNumber,
				paymentDetail.ExpDate,
				paymentDetail.CCV,
				paymentDetail.StreetAddress,
				paymentDetail.City,
				paymentDetail.State,
				paymentDetail.Zip
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

		private bool PaymentDetailExists(int id)
		{
			return db.PaymentDetails.Count(e => e.PaymentDetailId == id) > 0;
		}
	}
}