using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Data
{
    public interface IDogOwnerRepository
    {
        List<DogOwner> GetAllDogOwners();
    }
}