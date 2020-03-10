using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Data
{
	public class DogOwnerRepository
	{
		public List<DogOwner> GetAllDogOwners()
		{
			var dogOwnerList = new List<DogOwner>
			{
				new DogOwner
				{
					OwnerName = "Rob",
					DogName = "Willow"
				}
			};

			return dogOwnerList;
		} 
	}
}