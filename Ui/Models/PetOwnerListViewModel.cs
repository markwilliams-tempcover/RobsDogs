using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Models
{
    public class PetOwnerListViewModel : IPetOwnerListViewModel
    {
        public List<PetOwnerViewModel> PetOwnerViewModels { get; set; }
    }
}