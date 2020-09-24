using System.Collections.Generic;

namespace Ui.Models
{
    public class PetOwnerViewModel
	{
		public string OwnerName { get; set; }
		public List<PetViewModel> Pets { get; set; }
	}
}