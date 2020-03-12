using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Models
{
	public class DogOwnerListViewModel
	{
		public IEnumerable<DogOwnerViewModel> DogOwnerViewModels { get; set; }
	}
}