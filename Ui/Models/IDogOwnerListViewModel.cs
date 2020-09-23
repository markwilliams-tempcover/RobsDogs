using System.Collections.Generic;

namespace Ui.Models
{
    public interface IDogOwnerListViewModel
    {
        List<DogOwnerViewModel> DogOwnerViewModels { get; set; }
    }
}