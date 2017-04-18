using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MoveMe123.API.Models;

namespace MoveMe123.API.Data
{
	public class MoveMe123DataContext : DbContext
	{
		public MoveMe123DataContext() : base("MoveMe123")
		{
			//Database.SetInitializer(
			//    new MigrateDatabaseToLatestVersion<MoveMeDataContext, Configuration>()
			//     );
		}

		public IDbSet<Company> Companys { get; set; }
		public IDbSet<Customer> Customers { get; set; }
		public IDbSet<Equipment> Equipments { get; set; }
		public IDbSet<Inventory> Inventorys { get; set; }
		public IDbSet<JobDetail> JobDetails { get; set; }
		public IDbSet<Order> Orders { get; set; }
		public IDbSet<PaymentDetail> PaymentDetails { get; set; }
		public IDbSet<RequiredEquipment> RequiredEquipments { get; set; }
		public IDbSet<User> Users { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Company has many inventories and many orders 

			modelBuilder.Entity<Company>()
				.HasMany(company => company.Inventorys)
				.WithRequired(inventory => inventory.Company)
				.HasForeignKey(inventory => inventory.CompanyId);

			modelBuilder.Entity<Company>()
				.HasMany(company => company.Orders)
				.WithRequired(order => order.Company)
				.HasForeignKey(order => order.CompanyId);

			// Customer has many orders, payments, and job details

			modelBuilder.Entity<Customer>()
				.HasMany(customer => customer.Orders)
				.WithRequired(order => order.Customer)
				.HasForeignKey(order => order.CustomerId);
			modelBuilder.Entity<Customer>()
				.HasMany(customer => customer.PaymentDetails)
				.WithRequired(paymentDetail => paymentDetail.Customer)
				.HasForeignKey(paymentDetail => paymentDetail.CustomerId);
			modelBuilder.Entity<Customer>()
				.HasMany(customer => customer.JobDetails)
				.WithRequired(jobDetail => jobDetail.Customer)
				.HasForeignKey(jobDetail => jobDetail.CustomerId);

			// Equipment has many inventories and many required equipment

			modelBuilder.Entity<Equipment>()
				.HasMany(equipment => equipment.Inventorys)
				.WithRequired(inventory => inventory.Equipment)
				.HasForeignKey(inventory => inventory.EquipmentId);
			modelBuilder.Entity<Equipment>()
				.HasMany(equipment => equipment.RequiredEquipments)
				.WithRequired(requiredEquipment => requiredEquipment.Equipment)
				.HasForeignKey(requiredEquipment => requiredEquipment.EquipmentId);

			// Job Detail has many required equipment

			modelBuilder.Entity<JobDetail>()
				.HasMany(jobDetail => jobDetail.RequiredEquipments)
				.WithRequired(requiredEquipment => requiredEquipment.JobDetail)
				.HasForeignKey(requiredEquipment => requiredEquipment.JobDetailId);

			// Orders

			modelBuilder.Entity<Order>()
				.HasRequired(order => order.JobDetail)
				.WithRequiredDependent(jobDetail => jobDetail.Order)
				.Map(m => m.MapKey("JobDetailId"));

			// Payment has many Orders

			modelBuilder.Entity<PaymentDetail>()
				.HasMany(paymentDetail => paymentDetail.Orders)
				.WithRequired(order => order.PaymentDetail)
				.HasForeignKey(order => order.PaymentDetailId)
				.WillCascadeOnDelete(false);

			// User

			modelBuilder.Entity<User>()
				.HasOptional(user => user.Company)
				.WithOptionalDependent(company => company.User)
				.Map(m => m.MapKey("CompanyId"));

			modelBuilder.Entity<User>()
				.HasOptional(user => user.Customer)
				.WithOptionalDependent(customer => customer.User)
				.Map(m => m.MapKey("CustomerId"));
		}

		//public System.Data.Entity.DbSet<MoveMe.API.Models.Company> Companies { get; set; }

		// public System.Data.Entity.DbSet<MoveMe.API.Models.Inventory> Inventories { get; set; }
	}
}