using System.Collections.Generic;
using System.Security.Policy;

namespace Ui.Entities
{
    public class PetOwner
    {
        public int PetOwnerId { get; set; }
        public short OwnerId { get; set; }
        public int PetId { get; set; } 
    }
}