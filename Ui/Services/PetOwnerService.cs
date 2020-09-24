using System.Collections.Generic;
using System.Linq;
using Ui.Data;
using Ui.Services;

namespace Ui.Services
{
    public class PetOwnerService : IPetOwnerService
    {
        IPetOwnerRepository _dogOwnerRepository;
        IPetRepository _petRepository;
        IOwnerRepository _ownerRepository;
        public PetOwnerService(IPetOwnerRepository dogOwnerRepository, IOwnerRepository ownerRepository, IPetRepository petRepository)
        {
            _dogOwnerRepository = dogOwnerRepository;
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
        }
        public List<PetOwnerModel> GetAllPetOwners(int? pageSize, int? pageNumber)
        {
            var petOwnersList = new List<PetOwnerModel>();
            var dbPetOwners = new List<Ui.Entities.PetOwner>();
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                dbPetOwners = _dogOwnerRepository.GetAllPetOwners(pageSize.Value, pageNumber.Value);
            }
            else
            {
                dbPetOwners = _dogOwnerRepository.GetAllPetOwners();
            }

            if (!dbPetOwners.Any())
            {
                return petOwnersList;
            }
            petOwnersList = _ownerRepository.GetAllOwner()
                .Where(owner => dbPetOwners.Any(petOwner => petOwner.OwnerId == owner.OwnerId))
                .Select(owner=>  new PetOwnerModel
                {
                    OwnerId = owner.OwnerId,
                    PetOwnerName = owner.Name
                }).ToList();

            foreach (var petOwner in petOwnersList)
            {
                var petOwnersPetIds = dbPetOwners.Where(x => x.OwnerId == petOwner.OwnerId).Select(x => x.PetId);
                petOwner.Pets = _petRepository.GetAllPets().Where(pet => petOwnersPetIds.Contains(pet.PetId)).Select(pet =>
                    new PetModel
                    {
                        Age = pet.Age,
                        Name = pet.Name,
                        PetType = (PetType)pet.PetType,
                        PetId = pet.PetId
                    }).ToList();
            }
            return petOwnersList;
        }
    }
}