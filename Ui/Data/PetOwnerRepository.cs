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
            this._dbData = dbData;// throw new ArgumentNullException(nameof(dbData));
            //data in new DB
        }
        public List<PetOwner> GetAllPetOwners(int pageSize = PetOwnerConstants.DefaultPageSizeForGrid, int pageNumber = 1)
        {
            if (_dbData.PetOwners == null)
            {
                return new List<PetOwner>();
            }
            return _dbData.PetOwners
                .OrderBy(x => x.OwnerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
        }
        public bool AddPetOwner(PetOwner newPetOwner)
        {
            if (newPetOwner == null)
            {
                throw new ArgumentNullException(nameof(newPetOwner));
            }
            var ownerFound = _dbData.PetOwners.Exists(x => x.OwnerId == newPetOwner.OwnerId && x.PetId == newPetOwner.PetId);
            if (ownerFound)
            {
                throw new ArgumentException(PetOwnerConstants.ErrorMessages.PetOwner.DataAlreadyExists);
            }
            _dbData.PetOwners.Add(newPetOwner);
            return _dbData.SaveChanges();
        }
        public bool DeletePetOwner(int petId, short ownerId)
        {
            var petOwnerFound = _dbData.PetOwners.SingleOrDefault(x => x.OwnerId == ownerId && x.PetId == petId);
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
        List<PetOwner> GetAllPetOwners(int pageSize = PetOwnerConstants.DefaultPageSizeForGrid, int pageNumber = 1);
        bool DeletePetOwner(int petId, short ownerId);
        bool DeleteAllPetOwnerByOwnerId(short ownerId);
        bool DeleteAllPetOwnerByPetId(int petId);
        bool AddPetOwner(PetOwner newPetOwner);
    }
}