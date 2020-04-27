using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Controllers;
using Ui.Services;
using System.Collections.Generic;
using Ui.Entities;
using Ui.ViewModelMappers;
using System.Linq;
using Ui.Data;

namespace Ui.Tests.Controllers
{
	[TestClass]
	public class RobsDogsControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			RobsDogsController controller = new RobsDogsController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			// Should be testing/verifying call to GetAllDogOwners and subsequent methods down the stack.
			// Moq is installed to help you.
		}

		[TestMethod]
		public void TestDogOwnerViewModelMapper() {
			//test DogOwnerViewModelMapper
			var mockDogOwnerRepository = new Mock<DogOwnerRepository>();

			var mockDogOwnerService = new Mock<DogOwnerService>(mockDogOwnerRepository.Object);
			mockDogOwnerService.Setup(x => x.GetAllDogOwners()).Returns(
					new List<DogOwner>()
					{
						new DogOwner()
						{
							OwnerId = 1,
							OwnerName = "TestOwner"
						}
					}
				);

			var mockDogRepository = new Mock<DogRepository>();

			var mockDogService = new Mock<DogService>(mockDogRepository.Object);
			mockDogService.Setup(x => x.GetAllDogs()).Returns(
					new List<Dog>()
					{
						new Dog()
						{
							OwnerId = 1,
							DogName = "TestDog 1"
						},

						new Dog()
						{
							OwnerId = 1,
							DogName = "TestDog 2"
						}
					}
				);

			var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(mockDogOwnerService.Object, mockDogService.Object);

			var dogOwners = dogOwnerViewModelMapper.GetAllDogOwners();

			Assert.IsNotNull(dogOwners);
			Assert.AreEqual("TestOwner", dogOwners.DogOwnerViewModels.Single().OwnerName);
			Assert.AreEqual(2, dogOwners.DogOwnerViewModels.Single().DogNames.Count);
			Assert.AreEqual("TestDog 1", dogOwners.DogOwnerViewModels.Single().DogNames.First());
			Assert.AreEqual("TestDog 2", dogOwners.DogOwnerViewModels.Single().DogNames[1]);
		}

		[TestMethod]
		public void TestDogOwnerService()
		{
			var mockDogOwnerRepository = new Mock<DogOwnerRepository>();
			mockDogOwnerRepository.Setup(x => x.GetAllDogOwners()).Returns(
				new List<DogOwner>()
				{
					new DogOwner()
					{
						OwnerId = 1,
						OwnerName = "TestOwner"
					}
				}
				);

			var dogOwnerService = new DogOwnerService(mockDogOwnerRepository.Object);
			var dogOwners = dogOwnerService.GetAllDogOwners();

			Assert.IsNotNull(dogOwners);
			Assert.AreEqual(1, dogOwners.Count);
			Assert.AreEqual(1, dogOwners.Single().OwnerId);
			Assert.AreEqual("TestOwner", dogOwners.Single().OwnerName);

		}

		[TestMethod]
		public void TestDogService()
		{
			var mockDogRepository = new Mock<DogRepository>();
			mockDogRepository.Setup(x => x.GetAllDogs()).Returns(
				new List<Dog>()
				{
					new Dog()
					{
						OwnerId = 1,
						DogName = "TestDog"
					}
				}
				);

			var dogService = new DogService(mockDogRepository.Object);
			var dogs = dogService.GetAllDogs();

			Assert.IsNotNull(dogs);
			Assert.AreEqual(1, dogs.Count);
			Assert.AreEqual(1, dogs.Single().OwnerId);
			Assert.AreEqual("TestDog", dogs.Single().DogName);

		}
	}
}