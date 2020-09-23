using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ui.Entities;

namespace Ui.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        private IDbData _dbData;
        public OwnerRepository(IDbData dbData)
        {
            this._dbData = dbData;
            //data in new DB
        }
        public bool AddOwner(Ui.Entities.Owner newOwner)
        {
            if (newOwner == null)
            {
                throw new ArgumentNullException(nameof(newOwner));
            }
            throw new NotImplementedException();
        }

        public Owner UpdateOwner(Ui.Entities.Owner updateOwner)
        {
            if (updateOwner == null)
            {
                throw new ArgumentNullException(nameof(updateOwner));
            }
            throw new NotImplementedException();
        }

        public bool DeleteOwner(int ownerId)
        {
            throw new NotImplementedException();
        }
        public List<Owner> GetAllOwner()
        {
            throw new NotImplementedException();
        }
    }
    public interface IOwnerRepository
    {
        bool AddOwner(Ui.Entities.Owner newOwner);
        Owner UpdateOwner(Ui.Entities.Owner updateOwner);
        bool DeleteOwner(int ownerId);
        List<Owner> GetAllOwner();
    }
}