using System.Collections.Generic;
using System.Linq;
using Ui.Models;
using Ui.Services;

namespace Ui.ViewModelMappers
{
	public class DogOwnerViewModelMapper
	{
		public DogOwnerListViewModel GetAllDogOwners()
		{
			var dogOwnerService = new DogOwnerService();
			var dogOwners = dogOwnerService.GetAllDogOwners();
			var dogOwnerListViewModel = new DogOwnerListViewModel
			{
				DogOwnerViewModels = dogOwners.Select(e => new DogOwnerViewModel
				{
					OwnerName = e.OwnerName,
					DogNames = new List<string>
					{
						e.DogName
					}
				}).ToList()
			};

			return dogOwnerListViewModel;
		} 
	}
}