using System.Collections.Generic;
using Ui.Data; 
namespace Ui.Services
{
    public interface IPetOwnerService
    {
        List<PetOwnerModel> GetAllPetOwners(int? pageSize, int? pageNumber);
    }
}