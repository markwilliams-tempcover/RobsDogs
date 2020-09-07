using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Entities
{
	public class DogOwner
	{
		public Owner Owner { get; set; }
		public List<Dog> Dogs { get; set; }
	}
}