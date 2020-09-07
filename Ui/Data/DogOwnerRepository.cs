using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Data
{
    public class DogOwnerRepository : IDogOwnerRepository
	{
		public List<DogOwner> GetAllDogOwners()
		{
			var dogOwner = new List<DogOwner>
			{
				new DogOwner(){
				    Owner = new Owner
                    {
					    OwnerName = "Rob"
                    },
				    Dogs = new List<Dog>
                    {
					    new Dog{ DogName = "Willow"},
                        new Dog{ DogName = "Nook"},
                        new Dog{ DogName = "Sox"}
					}
				}
			};

			return dogOwner;
		} 
	}
}