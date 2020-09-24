﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ui.Entities;
namespace Ui.Data
{
    public class DbData : IDbData
    {
        public List<Owner> Owners { get; set; }
        public List<Pet> Pets { get; set; }
        public List<PetOwner> PetOwners { get; set; }
        public DbData()
        {
            Owners = new List<Owner> { new Owner { Name = "Rob", OwnerId = 1 }, new Owner { Name = "Steve", OwnerId = 2 } };
            Pets = new List<Pet> { new Pet { Name = "Willow", Age = 4, PetId = 1, PetType = 1 }, new Pet { Name = "Nook", Age = 1, PetId = 2, PetType = 1 } };
            PetOwners = new List<PetOwner> 
            { 
                new PetOwner { PetOwnerId = 1, OwnerId = 1, PetId = 1 },
                new PetOwner { PetOwnerId = 2, OwnerId = 1, PetId = 2 },
                new PetOwner { PetOwnerId = 3, OwnerId = 1, PetId = 3 }
            };
        }
        public bool SaveChanges() { return true; }
    }

    public interface IDbData
    {
        List<Owner> Owners { get; set; }
        List<Pet> Pets { get; set; }
        List<PetOwner> PetOwners { get; set; }
        bool SaveChanges();
    }
}