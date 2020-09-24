using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services
{
    public class DogOwnerService : IDogOwnerService
    {
        IPetOwnerRepository _dogOwnerRepository;
        public DogOwnerService(IPetOwnerRepository dogOwnerRepository)
        {
            _dogOwnerRepository = dogOwnerRepository;
        }
        public List<PetOwner> GetAllDogOwners()
        {
            var dogOwnerList = _dogOwnerRepository.GetAllPetOwners();

            return dogOwnerList;
        }
    }
}