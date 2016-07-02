using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Enums;
using ShepScheduler.Interfaces;
using ShepScheduler.Repository.Core;

namespace ShepScheduler.Repository.Models
{
	public class Treatments : IEntity
	{
		//[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public string Name { get; set; }
		public int Duration { get; set; }
		public decimal Price { get; set; }
		public TreatmentCategory Category { get; set; }
	}
}
