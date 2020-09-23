using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Ui.Entities;
using Ui.Models;

namespace Ui.Data
{

    public class DogOwnerRepository : IDogOwnerRepository
    {
        private IDbData _dbData;
        public DogOwnerRepository(IDbData dbData)
        {
            this._dbData = dbData;
            //data in new DB
        }
        public List<PetOwner> GetAllPetOwners()
        {
            var dogOwnerList = _dbData.PetOwners;
            return dogOwnerList;
        }
        public bool DeletePetOwner(int petOwnerId)
        {
            throw new NotImplementedException();
        }
        public bool AddPetOwner(PetOwner newPetOwner)
        {
            throw new NotImplementedException();
        }
    }
    public interface IDogOwnerRepository
    {
        List<PetOwner> GetAllPetOwners();
        bool DeletePetOwner(int petOwnerId);
        bool AddPetOwner(PetOwner newPetOwner);
    }
}