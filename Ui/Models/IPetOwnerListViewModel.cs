using System.Collections.Generic;

namespace Ui.Models
{
    public interface IPetOwnerListViewModel
    {
        List<PetOwnerViewModel> PetOwnerViewModels { get; set; }
    }
}