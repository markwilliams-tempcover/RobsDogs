using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Data
{
	public class DogOwnerRepository
	{
		public virtual List<DogOwner> GetAllDogOwners()
		{
			var dogOwnerList = new List<DogOwner>
			{
				new DogOwner
				{
					OwnerName = "Rob",
					OwnerId = 1
				}
			};
			return dogOwnerList;
		} 
	}
}