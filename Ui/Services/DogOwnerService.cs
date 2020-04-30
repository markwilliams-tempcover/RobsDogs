using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services {
    public class DogOwnerService : IDogOwnerService {

        private readonly IDogOwnerRepository dogOwnerRepository;

        public DogOwnerService(IDogOwnerRepository dogOwnerRepository) {
            this.dogOwnerRepository = dogOwnerRepository;
        }

        public List<DogOwner> GetAllDogOwners() {
            var dogOwnerList = dogOwnerRepository.GetAllDogOwners();

            return dogOwnerList;
        }
    }
}