using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;

namespace Ui.Services
{
    public interface IDogOwnerService
    {
        List<PetOwner> GetAllDogOwners();
    }
}