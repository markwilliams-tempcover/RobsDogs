using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services
{
	public class DogService
	{
		public DogService(DogRepository dogRepository)
		{
			_DogRepository = dogRepository;
		}

		private DogRepository _DogRepository;

		public virtual List<Dog> GetAllDogs()
		{
			var dogList = _DogRepository.GetAllDogs();

			return dogList;
		}
	}
}