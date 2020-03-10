using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services
{
	public class DogOwnerService
	{
		public List<DogOwner> GetAllDogOwners()
		{
			var dogOwnerRepository = new DogOwnerRepository();
			var dogOwnerList = dogOwnerRepository.GetAllDogOwners();

			return dogOwnerList;
		}
	}
}