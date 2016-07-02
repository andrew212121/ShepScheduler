using ShepScheduler.Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Repository.Models
{
	public class Visits : IEntity
	{
		//[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public int TreatmentId { get; set; }
		public string TreatmentName { get; set; }
		public virtual Treatments Treatment { get; set; }
		public virtual Clients Client { get; set; }
		public int ClientId { get; set; }
		public string Description { get; set; }
		public string Comment { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
