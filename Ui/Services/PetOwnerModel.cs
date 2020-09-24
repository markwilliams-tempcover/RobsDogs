using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ui.Services
{
    public class PetOwnerModel
    { 
        public int OwnerId { get; set; }
        public string PetOwnerName { get; set; }
        public List<PetModel> Pets { get; set; }
    }
    public class PetModel
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public short Age { get; set; }
        public PetType PetType { get; set; }
    }
    public enum PetType
    {
        Dog = 1,
        Cat = 2,
        Fish = 3,
        Hamster = 4
    }
}