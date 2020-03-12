using System.Collections.Generic;

namespace Ui.Models
{
	public class DogOwnerViewModel
	{
		public string OwnerName { get; set; }
		public IEnumerable<string> DogNames { get; set; }
	}
}