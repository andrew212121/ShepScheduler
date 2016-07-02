using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Models;
using ShepScheduler.Repository.Models;

namespace ShepScheduler.Repository.Core
{
	public class BaseFacade
	{
		protected Result Create<TEntity>(ICreateModel<TEntity> model)
			where TEntity : class, IEntity, new()
		{
			Result result = new Result();
			try
			{
				using (var context = new ShepSchedulerDBContext())
				{
					TEntity entity = new TEntity();
					model.FillNewEntity(entity);
					GetDbSetForType<TEntity>(context).Add(entity);
					context.SaveChanges();
					context.Entry(entity).GetDatabaseValues();
					result.NewIndex = entity.Id;
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}
			return result;
		}

		protected Result Edit<TEntity>(IEditModel<TEntity> model)
			where TEntity : class, IEntity
		{
			Result result = new Result();
			try
			{
				using (var context = new ShepSchedulerDBContext())
				{
					TEntity entity = GetDbSetForType<TEntity>(context).First(m => m.Id == model.Id);
					model.FillEntity(entity);
					int recordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = result.Message;
			}
			return result;
		}

		protected Result Remove<T>(int Id)
			where T : class, IEntity
		{
			Result result = new Result();
			try
			{
				using (var context = new ShepSchedulerDBContext())
				{
					DbSet<T> set = GetDbSetForType<T>(context);
					T entityToRemove = set.First(m => m.Id == Id);
					set.Remove(entityToRemove);
					int recordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = result.Message;
			}
			return result;
		}

		protected Result Remove<T>(List<int> Ids)
			where T : class, IEntity
		{
			Result result = new Result();
			try
			{
				using (var context = new ShepSchedulerDBContext())
				{
					DbSet<T> set = GetDbSetForType<T>(context);
					var entitiesToRemove = set.Where(m => Ids.Contains(m.Id));
					foreach(var entity in entitiesToRemove)
					{
						set.Remove((T)entity);
					}
					int recordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = result.Message;
			}
			return result;
		}

		private DbSet<T> GetDbSetForType<T>(ShepSchedulerDBContext context)
			where T : class
		{
			var type = typeof(T);

			if (type == typeof(Clients)) return context.Clients as DbSet<T>;
			if (type == typeof(Treatments)) return context.Treatments as DbSet<T>;
			if (type == typeof(Visits)) return context.Visits as DbSet<T>;
			//...my other types
			throw new Exception("Type not found in db");
		} 
	}
}
