using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services
{
	public interface IDogOwnerService
	{
		IEnumerable<DogOwner> GetAllDogOwners();
	}

	public class DogOwnerService : IDogOwnerService
	{
		private readonly IDogOwnerRepository dogOwnerRepository;

		public DogOwnerService(IDogOwnerRepository dogOwnerRepository)
		{
			this.dogOwnerRepository = dogOwnerRepository;
		}

		public IEnumerable<DogOwner> GetAllDogOwners()
		{
			try
			{
				return dogOwnerRepository.GetAllDogOwners();
			}
			catch (System.Exception)
			{
				return new List<DogOwner>();
			}
		}
	}
}