using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Interfaces
{
	public interface IPageViewModel
	{
		string Name { get; }
		bool IsActive { get; set; }
	}
}
