using System.Collections.Generic;

namespace Ui.Entities
{
	public class DogOwner
	{
		public DogOwner(string ownerName, string[] dogNames)
		{
			OwnerName = ownerName;
			DogNames = dogNames;
		}

		public string OwnerName { get; }
		public IEnumerable<string> DogNames { get; }
	}
}