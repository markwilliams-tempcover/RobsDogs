using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Models;
using Ui.Services;
using Ui.ViewModelMappers;

namespace Ui.Tests.ViewModelMappers
{
    [TestClass]
    public class PetOwnerViewModelMapperTests
    {
        private Mock<IPetOwnerService> mockPetOwnerService;
        private PetOwnerViewModelMapper petOwnerViewModelMapper;

        [TestInitialize]
        public void Setup()
        {
            mockPetOwnerService = new Mock<IPetOwnerService>();
            petOwnerViewModelMapper = new PetOwnerViewModelMapper(mockPetOwnerService.Object);
        }
        [TestMethod]
        public void WhenNoPetOwmersFoundGetAllPetOwnersReturnEmptyListOfPetOwnerListViewModel()
        {

            mockPetOwnerService.Setup(x => x.GetAllPetOwners(It.IsAny<int?>(),It.IsAny<int?>())).Returns(new List<PetOwnerModel>());
            var result = petOwnerViewModelMapper.GetAllPetOwners(null,null);
            result.Should().BeOfType<PetOwnerListViewModel>();
            result.PetOwnerViewModels.Count.Should().Be(0);
        }
        [TestMethod]
        public void ForExistingPetOwnersGetAllPetOwnersReturnFullyMappedPetOwnerListViewModel()
        {
            var petOwnerModelList = new List<PetOwnerModel> 
            {
                new PetOwnerModel{ OwnerId =1, PetOwnerName = "1", Pets=new List<PetModel>{ new PetModel{ Age =1, Name="ptet", PetId =1, PetType= PetType.Fish} } }
            };
            mockPetOwnerService.Setup(x => x.GetAllPetOwners(It.IsAny<int?>(), It.IsAny<int?>())).Returns(petOwnerModelList);
            var result = petOwnerViewModelMapper.GetAllPetOwners(null, null);
            result.Should().BeOfType<PetOwnerListViewModel>();
            result.PetOwnerViewModels.Count.Should().BeGreaterThan(0); 
            result.PetOwnerViewModels[0].OwnerName.Should().NotBe(string.Empty);
            result.PetOwnerViewModels[0].Pets.Count.Should().BeGreaterThan(0);
            result.PetOwnerViewModels[0].Pets[0].Name.Should().NotBe(string.Empty);
            result.PetOwnerViewModels[0].Pets[0].PetId.Should().BeGreaterThan(0);
            result.PetOwnerViewModels[0].Pets[0].AgeInYears.Should().BeGreaterThan(0);
            result.PetOwnerViewModels[0].Pets[0].PetType.Should().BeOfType<PetType>();
        }
    }
}
