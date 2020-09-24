using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ui.Entities;
using Ui;

namespace Ui.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        private IDbData _dbData;
        public OwnerRepository(IDbData dbData)
        {
            this._dbData = dbData;//?? throw new ArgumentNullException(nameof(dbData));
            //data in new DB
        }

        public bool AddOwner(Ui.Entities.Owner newOwner)
        {
            if (newOwner == null)
            {
                throw new ArgumentNullException(nameof(newOwner));
            }
            var ownerFound = _dbData.Owners.Exists(x => x.Name.Equals(newOwner.Name, StringComparison.InvariantCultureIgnoreCase));
            if (ownerFound)
            {
                throw new ArgumentException(PetOwnerConstants.ErrorMessages.Owner.DataAlreadyExists);
            }
            _dbData.Owners.Add(newOwner);
            return _dbData.SaveChanges();
        }

        public Owner UpdateOwner(Owner updateOwner)
        {
            if (updateOwner == null)
            {
                throw new ArgumentNullException(nameof(updateOwner));
            }
            var ownerFound = _dbData.Owners.Exists(x => x.OwnerId == updateOwner.OwnerId);
            if (!ownerFound)
            {
                throw new InvalidOperationException(PetOwnerConstants.ErrorMessages.Owner.DataNotFound);
            }
            _dbData.Owners.First(x => x.OwnerId == updateOwner.OwnerId).Name = updateOwner.Name;
            return _dbData.Owners.Single(x => x.OwnerId == updateOwner.OwnerId);
        }

        public bool DeleteOwner(short ownerId)
        {
            var ownerFound = _dbData.Owners.SingleOrDefault(x => x.OwnerId == ownerId);
            if (ownerFound == null)
            {
                throw new InvalidOperationException(PetOwnerConstants.ErrorMessages.Owner.DataNotFound);
            }
            //delete call from service layer too
            if (_dbData.PetOwners != null && _dbData.PetOwners.Exists(x => x.OwnerId == ownerId))
            {
                _dbData.PetOwners.RemoveAll(x => x.OwnerId == ownerId);
            }
            return _dbData.Owners.Remove(ownerFound) && _dbData.SaveChanges();

        }
        public List<Owner> GetAllOwner()
        {
            if (_dbData.Owners == null)
            {
                return new List<Owner>();
            }
            return _dbData.Owners.ToList();
        }
    }
    public interface IOwnerRepository
    {
        bool AddOwner(Ui.Entities.Owner newOwner);
        Owner UpdateOwner(Ui.Entities.Owner updateOwner);
        bool DeleteOwner(short ownerId);
        List<Owner> GetAllOwner();
    }
}
