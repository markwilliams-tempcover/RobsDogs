using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Ui.Entities;
using Ui.Models;

namespace Ui.Data
{

    public class PetOwnerRepository : IPetOwnerRepository
    {
        private IDbData _dbData;
        public PetOwnerRepository(IDbData dbData)
        {
            this._dbData = dbData;
            //data in new DB
        }
        public List<PetOwner> GetAllPetOwners()
        {
            if (_dbData.PetOwners == null)
            {
                return new List<PetOwner>();
            }
            return _dbData.PetOwners.ToList();
        }
        public bool AddPetOwner(PetOwner newPetOwner)
        { 
            if (newPetOwner == null)
            {
                throw new ArgumentNullException(nameof(newPetOwner));
            }
            var ownerFound = _dbData.PetOwners.Exists(x => x.OwnerId == newPetOwner.OwnerId &&  x.PetId== newPetOwner.PetId);
            if (ownerFound)
            {
                throw new ArgumentException(PetOwnerConstants.ErrorMessages.PetOwner.DataAlreadyExists);
            }
            _dbData.PetOwners.Add(newPetOwner);
            return _dbData.SaveChanges();
        }
        public bool DeletePetOwner(int petOwnerId)
        {
            var petOwnerFound = _dbData.PetOwners.SingleOrDefault(x => x.PetOwnerId == petOwnerId);
            if (petOwnerFound == null)
            {
                throw new InvalidOperationException(PetOwnerConstants.ErrorMessages.PetOwner.DataNotFound);
            }
            return _dbData.PetOwners.Remove(petOwnerFound) && _dbData.SaveChanges();
        }
        public bool DeleteAllPetOwnerByPetId(int petId)
        {
            var petOwnersFound = _dbData.PetOwners.Where(x => x.PetId == petId);
            if (!petOwnersFound.Any())
            {
                throw new InvalidOperationException(PetOwnerConstants.ErrorMessages.Pet.DataNotFound);
            }
            _dbData.PetOwners.RemoveAll(x => x.PetId == petId);
            return _dbData.SaveChanges();
        }
        public bool DeleteAllPetOwnerByOwnerId(short ownerId)
        {
            var petOwnersFound = _dbData.PetOwners.Where(x => x.OwnerId == ownerId);
            if (!petOwnersFound.Any())
            {
                throw new InvalidOperationException(PetOwnerConstants.ErrorMessages.Owner.DataNotFound);
            }
            _dbData.PetOwners.RemoveAll(x => x.OwnerId == ownerId);
            return _dbData.SaveChanges();
        }
    }
    public interface IPetOwnerRepository
    {
        List<PetOwner> GetAllPetOwners();
        bool DeletePetOwner(int petOwnerId);
        bool DeleteAllPetOwnerByOwnerId(short ownerId);
        bool DeleteAllPetOwnerByPetId(int petId);
        bool AddPetOwner(PetOwner newPetOwner);
    }
}