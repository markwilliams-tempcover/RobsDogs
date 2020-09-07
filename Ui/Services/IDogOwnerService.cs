using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Services
{
    public interface IDogOwnerService
    {
        List<DogOwner> GetAllDogOwners();
    }
}