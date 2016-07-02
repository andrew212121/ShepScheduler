using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Interfaces;
using ShepScheduler.Repository.Core;

namespace ShepScheduler.Repository.Models
{
	public class Clients : IEntity
	{
		//[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Comment { get; set; }

		public virtual ICollection<Visits> Visits { get; set; } 
	}
}
