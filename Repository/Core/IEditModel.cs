using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Repository.Core
{
	public interface IEditModel<T>
		where T:IEntity
	{
		int Id { get; set; }
		void FillEntity(T entity);
	}
}
