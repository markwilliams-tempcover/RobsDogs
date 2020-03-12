using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Data
{
    public interface IDogOwnerRepository
    {
        IEnumerable<DogOwner> GetAllDogOwners();
    }

    public class DogOwnerRepository : IDogOwnerRepository
    {
        public IEnumerable<DogOwner> GetAllDogOwners()
        {
            var dogOwnerList = new List<DogOwner>
            {
                new DogOwner("Rob", new []{ "Willow","Nook","Sox" })
            };

            return dogOwnerList;
        }
    }
}