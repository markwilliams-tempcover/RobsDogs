using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Data;
using Ui.Entities;

namespace Ui.Tests.Data
{
    [TestClass]
    public class PetRepositoryTests
    {
        public Mock<IDbContext> mockData;
        public PetRepository petRepo;
        [TestInitialize]
        public void Setup()
        {
            mockData = new Mock<IDbContext>();
            petRepo = new PetRepository(mockData.Object);
        } 

        #region GetAllOwners
        [TestMethod]
        public void GetAllPetsReturnEmptyListOfPetEntities()
        {
            mockData.Setup(x => x.Pets).Returns(new List<Pet>());
            var result = petRepo.GetAllPets();
            result.Should().BeOfType<List<Pet>>();
            result.Count.Should().Be(0);
        }
        [TestMethod]
        public void GetAllPetsReturnListOfPetEntities()
        {
            var existingPet = new Pet{ Name = "Pet1", PetId= 1 ,PetType=1, Age =1};
            var existingPet2 = new Pet { Name = "Pet2", PetId = 2, PetType = 1, Age = 2 };
            mockData.Setup(x => x.Pets).Returns(new List<Pet> { existingPet, existingPet2 });
            var result = petRepo.GetAllPets();
            result.Should().BeOfType<List<Pet>>();
            result.Count.Should().BeGreaterThan(0);
        }
        #endregion

    }
}
