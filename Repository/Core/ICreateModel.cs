using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Repository.Core
{
	public interface ICreateModel<T>
		where T:IEntity
	{
		void FillNewEntity(T entity);
	}
}
