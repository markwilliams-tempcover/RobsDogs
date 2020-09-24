using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Data;
using Ui.Entities;
using Ui.Services;

namespace Ui.Tests.Services
{
    [TestClass]
    public class PetOwnerServiceTests
    {
        Mock<IPetOwnerRepository> mockPetOwnerRepo;
        Mock<IPetRepository> mockPetRepo;
        Mock<IOwnerRepository> mockOwnerRepo;
        IPetOwnerService petOwnerService;
        [TestInitialize]
        public void Setup()
        {
            mockPetOwnerRepo = new Mock<IPetOwnerRepository>();
            mockPetRepo = new Mock<IPetRepository>();
            mockOwnerRepo = new Mock<IOwnerRepository>();
            petOwnerService = new PetOwnerService(mockPetOwnerRepo.Object, mockOwnerRepo.Object, mockPetRepo.Object);
        }
        [TestMethod]
        public void DataReturnedWithOptionalPageSizeandPageNumbers()
        {
            //arrange
            var petOwners = GetExistingPetOwners();
            var owners = petOwners.Select(x => new Owner { Name = x.OwnerId.ToString(), OwnerId = x.OwnerId }).Distinct().ToList();
            var pets = petOwners.Select(x => new Pet { Age = (short)x.PetId, Name = x.PetId.ToString(), PetId = x.PetId, PetType = 1 }).Distinct().ToList();
            int checkpageSize = 0;
            int checkPageNumber = 0;
            mockPetOwnerRepo.Setup(x => x.GetAllPetOwners(It.IsAny<int>(), It.IsAny<int>()))
                            .Callback<int, int>((s, n) =>
                            {
                                checkpageSize = s;
                                checkPageNumber = n;
                            })
                            .Returns(petOwners)
                            .Verifiable();
            mockOwnerRepo.Setup(x => x.GetAllOwner()).Returns(owners);
            mockPetRepo.Setup(x => x.GetAllPets()).Returns(pets);
            //act
            var result = petOwnerService.GetAllPetOwners(null, null);
            //assert
            result.Count.Should().BeGreaterThan(0);
            mockPetOwnerRepo.Verify(x => x.GetAllPetOwners(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
            checkpageSize.Should().Equals(PetOwnerConstants.DefaultPageSizeForGrid);
            checkPageNumber.Should().Equals(1);
        }

        [TestMethod]
        public void OptionalPageSizeandPageNumbersValuesPassed()
        {
            //arrange
            var petOwners = GetExistingPetOwners();
            var owners = petOwners.Select(x => new Owner { Name = x.OwnerId.ToString(), OwnerId = x.OwnerId }).Distinct().ToList();
            var pets = petOwners.Select(x => new Pet { Age = (short)x.PetId, Name = x.PetId.ToString(), PetId = x.PetId, PetType = 1 }).Distinct().ToList();
            int checkpageSize = 0;
            int checkPageNumber = 0;
            int requestedPageSize = 0;
            int requestedPageNumber = 0;
            mockPetOwnerRepo.Setup(x => x.GetAllPetOwners(It.IsAny<int>(), It.IsAny<int>()))
                            .Callback<int, int>((s, n) =>
                            {
                                checkpageSize = s;
                                checkPageNumber = n;
                            })
                            .Returns(petOwners)
                            .Verifiable();
            mockOwnerRepo.Setup(x => x.GetAllOwner()).Returns(owners);
            mockPetRepo.Setup(x => x.GetAllPets()).Returns(pets);
            //act
            var result = petOwnerService.GetAllPetOwners(requestedPageSize, requestedPageNumber);
            //assert
            result.Count.Should().BeGreaterThan(0);
            mockPetOwnerRepo.Verify(x => x.GetAllPetOwners(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
            checkpageSize.Should().Equals(requestedPageSize);
            checkPageNumber.Should().Equals(requestedPageNumber);
        }
        public void ReturnedPetOwnerModelHasAllFieldPopulated()
        {
            //arrange
            var petOwners = GetExistingPetOwners();
            var owners = petOwners.Select(x => new Owner { Name = x.OwnerId.ToString(), OwnerId = x.OwnerId }).Distinct().ToList();
            var pets = petOwners.Select(x => new Pet { Age = (short)x.PetId, Name = x.PetId.ToString(), PetId = x.PetId, PetType = 1 }).Distinct().ToList();            
            int requestedPageSize = 0;
            int requestedPageNumber = 0;
            mockPetOwnerRepo.Setup(x => x.GetAllPetOwners(It.IsAny<int>(), It.IsAny<int>())).Returns(petOwners);
            mockOwnerRepo.Setup(x => x.GetAllOwner()).Returns(owners);
            mockPetRepo.Setup(x => x.GetAllPets()).Returns(pets);
            //act
            var result = petOwnerService.GetAllPetOwners(requestedPageSize, requestedPageNumber);
            //assert
            result.Count.Should().BeGreaterThan(0);
            mockPetOwnerRepo.Verify(x => x.GetAllPetOwners(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
            result[0].OwnerId.Should().BeGreaterThan(0);
            result[0].PetOwnerName.Should().NotBe(string.Empty);
            result[0].Pets.Count.Should().BeGreaterThan(0);
            result[0].Pets[0].Name.Should().NotBe(string.Empty);
            result[0].Pets[0].PetId.Should().BeGreaterThan(0);
            result[0].Pets[0].Age.Should().BeGreaterThan(0);
            result[0].Pets[0].PetType.Should().BeOfType<PetType>();
        }

        private static List<PetOwner> GetExistingPetOwners()
        {
            var petOwners = new List<PetOwner>();
            for (int petOwnerId = 1; petOwnerId < PetOwnerConstants.DefaultPageSizeForGrid + 5; petOwnerId++)
            {
                petOwners.Add(new PetOwner
                {
                    PetOwnerId = petOwnerId,
                    PetId = petOwnerId,
                    OwnerId = (short)(petOwnerId < 10 ? 1 : petOwnerId / 10)
                });
            }
            return petOwners;
        }

    }
}
