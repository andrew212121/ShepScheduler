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
	public class Client : ClientBase, IEditModel<Clients>, ICreateModel<Clients>
	{
		public string Comment { get; set;}

		private List<Visit> _visits;
		public List<Visit> Visits { 
			get{
				if(_visits == null) _visits = new List<Visit>();
				return _visits;
			}
			set
			{
				_visits = value;
			}
		}

		private List<Photo> _photos;
		public List<Photo> Photos { 
			get
			{
				if(_photos == null)
				{
					_photos = new List<Photo>();
				}
				return _photos;
			}
			set
			{
				_photos = value;
			}
		}

		public void FillEntity(Clients entity)
		{
			entity.FirstName = FirstName;
			entity.LastName = LastName;
			entity.Phone = Phone;
			entity.Comment = Comment;
		}

		public void FillNewEntity(Clients entity)
		{
			FillEntity(entity);
		}
	}
}
