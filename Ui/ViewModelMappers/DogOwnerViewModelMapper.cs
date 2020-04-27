using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Ui.Entities;
using Ui.Models;
using Ui.Services;

namespace Ui.ViewModelMappers
{
	public class DogOwnerViewModelMapper
	{
		public DogOwnerViewModelMapper(DogOwnerService dogOwnerService, DogService dogService)
		{
			_DogOwnerService = dogOwnerService;
			_DogService = dogService;
		}

		private DogOwnerService _DogOwnerService;
		private DogService _DogService;

		public DogOwnerListViewModel GetAllDogOwners()
		{
			var dogOwners = _DogOwnerService.GetAllDogOwners();
			var dogs = _DogService.GetAllDogs();
			var dogOwnerListViewModel = new DogOwnerListViewModel
			{
				DogOwnerViewModels = dogOwners.GroupJoin(dogs,
					owner => owner.OwnerId,
					dog => dog.OwnerId,
					(owner, dogCollection) => new DogOwnerViewModel() { OwnerName = owner.OwnerName, DogNames = dogCollection.Select(x => x.DogName).ToList() }
				).ToList()
			};

			return dogOwnerListViewModel;
		} 
	}
}