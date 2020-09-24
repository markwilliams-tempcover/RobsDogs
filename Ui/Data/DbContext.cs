using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ui.Entities;
namespace Ui.Data
{
    public class DbContext : IDbContext
    {
        public List<Owner> Owners { get; set; }
        public List<Pet> Pets { get; set; }
        public List<PetOwner> PetOwners { get; set; }
        public DbContext()
        {
            Owners = new List<Owner> { new Owner { Name = "Rob", OwnerId = 1 }, new Owner { Name = "Steve", OwnerId = 2 } };
            Pets = new List<Pet> { new Pet { Name = "Willow", Age = 4, PetId = 1, PetType = 1 }, new Pet { Name = "Nook", Age = 1, PetId = 2, PetType = 1 },new Pet{ Name = "Sox", Age = 2, PetId = 3, PetType = 1 }, new Pet { Name = "Milo", Age = 2, PetId = 4, PetType = 2 } };
            PetOwners = new List<PetOwner>
            {
                new PetOwner { PetOwnerId = 1, OwnerId = 1, PetId = 1 },
                new PetOwner { PetOwnerId = 2, OwnerId = 1, PetId = 2 },
                new PetOwner { PetOwnerId = 3, OwnerId = 1, PetId = 3 },
                new PetOwner { PetOwnerId = 4, OwnerId = 2, PetId = 4 }
            };
        }
        public bool SaveChanges() { return true; }
    }

    public interface IDbContext
    {
        List<Owner> Owners { get; set; }
        List<Pet> Pets { get; set; }
        List<PetOwner> PetOwners { get; set; }
        bool SaveChanges();
    }
}