using System.Collections.Generic;
using System.Security.Policy;

namespace Ui.Entities
{
    public class PetOwner
    {
        public short DogOwnerId { get; set; }
        public Owner Owner { get; set; }
        public List<Pet> Pets { get; set; }
    }
    public class Owner
    {
        public short OwnerId { get; set; }
        public string Name { get; set; }
    }
}