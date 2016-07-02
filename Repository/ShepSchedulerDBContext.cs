using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Models;
using ShepScheduler.Repository.Models;

namespace ShepScheduler.Repository
{
	public class ShepSchedulerDBContext : DbContext
	{
		public DbSet<ShepScheduler.Repository.Models.Clients> Clients { get; set; }
		public DbSet<ShepScheduler.Repository.Models.Visits> Visits { get; set; }
		public DbSet<ShepScheduler.Repository.Models.Treatments> Treatments { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Chinook Database does not pluralize table names
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public T Create<T>(T entity)
		where T : class
		{
			try
			{
				GetDbSetForType<T>().Add(entity);
				SaveChanges();
				Entry(entity).GetDatabaseValues();
				return entity;
			}
			catch (Exception ex)
			{
				return default(T);
			}
		}

		private DbSet<T> GetDbSetForType<T>()
			where T : class
		{
			var type = typeof(T);

			if (type == typeof(Clients)) return Clients as DbSet<T>;
			if (type == typeof(Treatments)) return Treatments as DbSet<T>;
			//...my other types
			throw new Exception("Type not found in db");
		}    
	}
}
