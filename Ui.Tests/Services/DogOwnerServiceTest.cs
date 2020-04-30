using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Ui.Data;
using Ui.Entities;
using Ui.Services;

namespace Ui.Tests.Services {
    [TestClass]
    public class DogOwnerServiceTest {

        [TestMethod]
        public void GetAllDogOwners_Should_returnDogOwnersList_When_repositoryIsNotEmpty() {

            //Arrange
            var dogOwnerRepository = new Mock<IDogOwnerRepository>();
            var dogOwner = new DogOwner() { OwnerName = "Qwe", DogNameList = new List<string>() { "q", "w", "e" } };
            var dogOwnerList = new List<DogOwner> { dogOwner };
            dogOwnerRepository.Setup(x => x.GetAllDogOwners()).Returns(dogOwnerList);
            var dogOwnerService = new DogOwnerService(dogOwnerRepository.Object);

            //Act
            var result = dogOwnerService.GetAllDogOwners();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(dogOwnerList, result);
        }

        [TestMethod]
        public void GetAllDogOwners_Should_returnEmptyDogOwnersList_When_repositoryIsEmpty() {

            //Arrange
            var dogOwnerRepository = new Mock<IDogOwnerRepository>();
            var dogOwnerList = new List<DogOwner>();
            dogOwnerRepository.Setup(x => x.GetAllDogOwners()).Returns(dogOwnerList);
            var dogOwnerService = new DogOwnerService(dogOwnerRepository.Object);

            //Act
            var result = dogOwnerService.GetAllDogOwners();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(dogOwnerList, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetAllDogOwners_Should_throwException_When_repositoryReturnsNull() {
            //Arrange
            var dogOwnerRepository = new Mock<IDogOwnerRepository>();
            dogOwnerRepository.Setup(x => x.GetAllDogOwners()).Throws(new NullReferenceException());
            var dogOwnerService = new DogOwnerService(dogOwnerRepository.Object);

            //Act
            var result = dogOwnerService.GetAllDogOwners();
        }


    }
}
