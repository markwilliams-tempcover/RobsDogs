using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Data;
using Ui.Entities;
using Ui.Services;

namespace Ui.Tests.Services
{
    [TestClass]
    public class DogOwnerServiceTests
    {
        [TestMethod]
        public void GivenTheRepositoryHasData_WhenGettingAllDogs_ThenAListOfDogsOwnersIsReturned()
        {
            // Assemble
            var dogOwner = new List<DogOwner> { new DogOwner("Homer", new[] { "Santa's Little Helper" }) };

            var dogOwnerRepository = new Mock<IDogOwnerRepository>();
            dogOwnerRepository.Setup(s => s.GetAllDogOwners()).Returns(dogOwner);
            var dogOwnerService = new DogOwnerService(dogOwnerRepository.Object);

            // Act
            var allDogOwners = dogOwnerService.GetAllDogOwners();

            // Assert
            Assert.AreEqual(1, allDogOwners.Count());
            Assert.AreEqual("Homer", allDogOwners.Single().OwnerName);
            Assert.AreEqual(1, allDogOwners.Single().DogNames.Count());
            Assert.AreEqual("Santa's Little Helper", allDogOwners.Single().DogNames.Single());
        }

        [TestMethod]
        public void GivenTheRepositoryReturnsAnException_WhenGettingAllDogs_ThenTheExceptionIsHandles()
        {
            // Assemble
            var dogOwnerRepository = new Mock<IDogOwnerRepository>();
            dogOwnerRepository.Setup(s => s.GetAllDogOwners()).Throws(new Exception());
            var dogOwnerService = new DogOwnerService(dogOwnerRepository.Object);

            // Act
            var allDogOwners = dogOwnerService.GetAllDogOwners();

            // Assert
            Assert.AreEqual(0, allDogOwners.Count());
        }
    }
}
