using ShepScheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Interfaces;
using ShepScheduler.Repository.Core;
using ShepScheduler.Repository.Models;

namespace ShepScheduler.Models
{
	public class Treatment : IEditModel<Treatments>, ICreateModel<Treatments>
	{
		public int Id { get; set;}
		public string Name { get; set; }
		public int Duration { get; set; }
		public decimal Price { get; set; }
		public TreatmentCategory Category { get; set;}

		public void FillEntity(Treatments entity)
		{
			entity.Name = Name;
			entity.Duration = Duration;
			entity.Price = Price;
			entity.Category = Category;
		}

		public void FillNewEntity(Treatments entity)
		{
			FillEntity(entity);
		}
	}
}
