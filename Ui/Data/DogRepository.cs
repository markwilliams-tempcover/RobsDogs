using System.Collections.Generic;
using Ui.Entities;

namespace Ui.Data
{
	public class DogRepository
	{
		public virtual List<Dog> GetAllDogs()
		{
			var dogList = new List<Dog>
			{
				new Dog
				{
					OwnerId = 1,
					DogName = "Willow"
				},
				new Dog
				{
					OwnerId = 1,
					DogName = "Nook"
				},
				new Dog
				{
					OwnerId = 1,
					DogName = "Sox"
				}
			};
			return dogList;
		}
	}
}