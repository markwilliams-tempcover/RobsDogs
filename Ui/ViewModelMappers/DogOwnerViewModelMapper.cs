using System.Collections.Generic;
using System.Linq;
using Ui.Models;
using Ui.Services;

namespace Ui.ViewModelMappers
{
    public class DogOwnerViewModelMapper : IDogOwnerViewModelMapper {

        private readonly IDogOwnerService dogOwnerService;

        public DogOwnerViewModelMapper(IDogOwnerService dogOwnerService) {
            this.dogOwnerService = dogOwnerService;
        }

        public DogOwnerListViewModel GetAllDogOwners() {
            var dogOwners = dogOwnerService.GetAllDogOwners();
            var dogOwnerListViewModel = new DogOwnerListViewModel {
                DogOwnerViewModels = dogOwners.Select(e => new DogOwnerViewModel {
                    OwnerName = e.OwnerName,
                    DogNames = e.DogNameList
                }).ToList()
            };

            return dogOwnerListViewModel;
        }
    }
}