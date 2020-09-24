using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ui.Entities;
namespace Ui.Data
{
    public class PetRepository
    {
        private IDbData _dbData;
        public PetRepository(IDbData dbData)
        {
            this._dbData = dbData;// throw new ArgumentNullException(nameof(dbData));
            //data in new DB
        }
        public bool AddPet(Pet newPet)
        {
            if (newPet == null)
            {
                throw new ArgumentNullException(nameof(newPet));
            }
            throw new NotImplementedException();
        }

        public Pet UpdatePet(Pet updatePet)
        {
            if (updatePet == null)
            {
                throw new ArgumentNullException(nameof(updatePet));
            }
            throw new NotImplementedException();
        }

        public bool DeletePet(int PetId)
        {
            throw new NotImplementedException();
        }
        public List<Pet> GetAllPets()
        {
            throw new NotImplementedException();
        }
    }
    public interface IPetRepository
    {
        bool AddPet(Pet newPet);
        Pet UpdatePet(Pet updatePet);
        bool DeletePet(int PetId);
        List<Pet> GetAllPets();
    }
}