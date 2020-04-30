using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Ui.Entities;
using Ui.Models;
using Ui.Services;
using Ui.ViewModelMappers;

namespace Ui.Tests.ViewModelMapper {
    [TestClass]
    public class DogOwnerViewModelMapperTest {

        [TestMethod]
        public void GetAllDogOwners_Should_returnDogOwnerListVM_When_serviceReturnDogOwnersList() {

            //Arrange
            var dogOwnerService = new Mock<IDogOwnerService>();
            var dogOwner = new DogOwner() { OwnerName = "Qwe", DogNameList = new List<string> { "q", "w", "e" } };
            var dogOwners = new List<DogOwner> { dogOwner };
            dogOwnerService.Setup(x => x.GetAllDogOwners()).Returns(dogOwners);

            var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(dogOwnerService.Object);

            //Act
            var result = dogOwnerViewModelMapper.GetAllDogOwners();

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
