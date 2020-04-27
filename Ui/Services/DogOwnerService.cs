using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services
{
	public class DogOwnerService
	{
		public DogOwnerService(DogOwnerRepository dogOwnerRepository)
		{
			_DogOwnerRepository = dogOwnerRepository;
		}

		private DogOwnerRepository _DogOwnerRepository;

		public virtual List<DogOwner> GetAllDogOwners()
		{
			var dogOwnerList = _DogOwnerRepository.GetAllDogOwners();

			return dogOwnerList;
		}
	}
}