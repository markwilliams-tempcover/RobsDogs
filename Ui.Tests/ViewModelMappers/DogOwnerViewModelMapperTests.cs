using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Entities;
using Ui.Services;
using Ui.ViewModelMappers;

namespace Ui.Tests.ViewModelMappers
{
    [TestClass]
    public class DogOwnerViewModelMapperTests
    {
        [TestMethod]
        public void GivenADogOwner_WithASingleDog_WhenLoadingTheList_ThenTheViewModelIsPopulated()
        {
            // Assemble
            var dogOwner = new List<DogOwner> { new DogOwner("Homer", new[] { "Santa's Little Helper" }) };

            var dogOwnerService = new Mock<IDogOwnerService>();
            dogOwnerService.Setup(t => t.GetAllDogOwners()).Returns(dogOwner);
            var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(dogOwnerService.Object);

            // Act
            var listViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            // Assert
            Assert.AreEqual(1, listViewModel.DogOwnerViewModels.Count());
            Assert.AreEqual("Homer", listViewModel.DogOwnerViewModels.Single().OwnerName);
            Assert.AreEqual(1, listViewModel.DogOwnerViewModels.Single().DogNames.Count());
            Assert.AreEqual("Santa's Little Helper", listViewModel.DogOwnerViewModels.Single().DogNames.Single());
        }

        [TestMethod]
        public void GivenNoDogOwners_WhenLoadingTheList_ThenTheViewModelIsPopulated()
        {
            // Assemble
            var dogOwner = new List<DogOwner>();
            var dogOwnerService = new Mock<IDogOwnerService>();
            dogOwnerService.Setup(t => t.GetAllDogOwners()).Returns(dogOwner);
            var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(dogOwnerService.Object);

            // Act
            var listViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            // Assert
            Assert.AreEqual(0, listViewModel.DogOwnerViewModels.Count());
        }

        [TestMethod]
        public void GivenADogOwner_WithoutADog_WhenLoadingTheList_ThenTheViewModelIsPopulated()
        {
            // Assemble
            var dogOwner = new List<DogOwner> { new DogOwner("Homer", new string[0]) };

            var dogOwnerService = new Mock<IDogOwnerService>();
            dogOwnerService.Setup(t => t.GetAllDogOwners()).Returns(dogOwner);
            var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(dogOwnerService.Object);

            // Act
            var listViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            // Assert
            Assert.AreEqual(1, listViewModel.DogOwnerViewModels.Count());
            Assert.AreEqual("Homer", listViewModel.DogOwnerViewModels.Single().OwnerName);
            Assert.AreEqual(0, listViewModel.DogOwnerViewModels.Single().DogNames.Count());
        }
    }
}
