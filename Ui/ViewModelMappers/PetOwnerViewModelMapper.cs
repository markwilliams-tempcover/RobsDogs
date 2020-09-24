using System.Collections.Generic;
using System.Linq;
using Ui.Models;
using Ui.Services;

namespace Ui.ViewModelMappers
{
    public interface IPetOwnerViewModelMapper
    {
        PetOwnerListViewModel GetAllPetOwners(int? pageSize, int? pageNumber);
    }

    public class PetOwnerViewModelMapper : IPetOwnerViewModelMapper
    {
        IPetOwnerService _dogOwnerService;
        public PetOwnerViewModelMapper(IPetOwnerService dogOwnerService)
        {
            _dogOwnerService = dogOwnerService;
        }

        public PetOwnerListViewModel GetAllPetOwners(int? pageSize, int? pageNumber)
        {
            var dogOwners = _dogOwnerService.GetAllPetOwners(pageSize, pageNumber);
            if (!dogOwners.Any())
            {
                return new PetOwnerListViewModel
                {
                    PetOwnerViewModels = new List<PetOwnerViewModel>()
                };
            }
            var dogOwnerListViewModel = new PetOwnerListViewModel
            {
                PetOwnerViewModels = dogOwners.Select(e => new PetOwnerViewModel
                {
                    OwnerName = e.PetOwnerName,
                    Pets = e.Pets.Select(pet => new PetViewModel
                    {
                        AgeInYears = pet.Age,
                        Name = pet.Name,
                        PetId = pet.PetId,
                        PetType = pet.PetType
                    }).ToList()
                }).ToList()
            };
            return dogOwnerListViewModel;
        }
    }
}