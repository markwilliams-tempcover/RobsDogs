using System.Collections.Generic;
using System.Linq;
using Ui.Models;
using Ui.Services;

namespace Ui.ViewModelMappers
{
    public interface IDogOwnerViewModelMapper
    {
        DogOwnerListViewModel GetAllDogOwners();
    }

    public class DogOwnerViewModelMapper : IDogOwnerViewModelMapper
    {
        IDogOwnerService _dogOwnerService;
        public DogOwnerViewModelMapper(IDogOwnerService dogOwnerService)
        {
            _dogOwnerService = dogOwnerService;

        }
        public DogOwnerListViewModel GetAllDogOwners()
        { 
            var dogOwners = _dogOwnerService.GetAllDogOwners();
            var dogOwnerListViewModel = new DogOwnerListViewModel
            {
                DogOwnerViewModels = dogOwners.Select(e => new DogOwnerViewModel
                {
                    //OwnerName = e.Owner.Name,
                    Pets = new List<PetViewModel>
                    { }
                }).ToList()
            };

            return dogOwnerListViewModel;
        }
    }
}