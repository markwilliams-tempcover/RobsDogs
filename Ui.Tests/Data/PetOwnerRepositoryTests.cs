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
    public class PetOwnerRepositoryTests
    {
        public Mock<IDbData> mockData;
        public PetOwnerRepository petOwnerRepo;
        [TestInitialize]
        public void Setup()
        {
            mockData = new Mock<IDbData>();
            petOwnerRepo = new PetOwnerRepository(mockData.Object);
        }

        #region AddPetOwner
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "PetOwnerRepo")]
        public void AddPetOwnerThrowExceptionOnInvalidEntry()
        {
            petOwnerRepo.AddPetOwner(null);
        }

        [TestMethod]
        public void AdPetdOwnerThrowExceptionForExistigPetOwner()
        {
            var existingPetOwner = new PetOwner
            {
                OwnerId = 1,
                PetId = 1,
                PetOwnerId = 1
            };
            var newPetOwner = new PetOwner
            {
                OwnerId = 1,
                PetId = 1
            };
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner });
            Action act = () => petOwnerRepo.AddPetOwner(newPetOwner);
            //Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.PetOwner.DataAlreadyExists);
        }
        //happy path
        [TestMethod]
        public void SuccesfullyAddingNewOwner()
        {
            var existingPetOwner = new PetOwner
            {
                OwnerId = 1,
                PetId = 1,
                PetOwnerId = 1
            };
            var newPetOwner = new PetOwner
            {
                OwnerId = 1,
                PetId = 2
            };
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner });
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = petOwnerRepo.AddPetOwner(newPetOwner);
            result.Should().BeTrue();
        }
        #endregion

        #region DeletePetOwner
        [TestMethod]
        public void DeletePetOwnerThrowExceptionForNonExistigPetOwner()
        {
            var existingPetOwner = new PetOwner { OwnerId = 1, PetOwnerId = 1, PetId = 1 };
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner });
            Action act = () => petOwnerRepo.DeletePetOwner(2);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.PetOwner.DataNotFound);
        } 

        [TestMethod]
        public void SuccessfullyDeletingPetOwner()
        {
            var existingPetOwner = new PetOwner { OwnerId = 1, PetOwnerId = 1, PetId = 1 };
            var existingPetOwner2 = new PetOwner { OwnerId = 1, PetOwnerId = 2, PetId = 2 };
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner, existingPetOwner2 });
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = petOwnerRepo.DeletePetOwner(2);
            result.Should().BeTrue();
        }
        #endregion


        #region DeleteAllPetOwnerByOwnerId
        [TestMethod]
        public void DeletePetOwnerByOwnerIdThrowExceptionForNonExistigOwner()
        { 
            var existingPetOwner = new PetOwner { OwnerId = 1, PetOwnerId = 1, PetId = 1 };
            short nonExistingOwnerId = 2;
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner }); 
            Action act = () => petOwnerRepo.DeleteAllPetOwnerByOwnerId(nonExistingOwnerId);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.Owner.DataNotFound);
        }

        [TestMethod]
        public void SuccessfullyDeletingPetOwnerByOwnerId()
        {
            var existingPetOwner = new PetOwner { OwnerId = 1, PetOwnerId = 1, PetId = 1 };
            var existingPetOwner2 = new PetOwner { OwnerId = 2, PetOwnerId = 1, PetId = 2 };
            short existingOwnerId = 2;
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner, existingPetOwner2 });
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = petOwnerRepo.DeleteAllPetOwnerByOwnerId(existingOwnerId);
            result.Should().BeTrue();
        }
        #endregion


        #region DeleteAllPetOwnerByOwnerId
        [TestMethod]
        public void DeletePetOwnerByOwnerIdThrowExceptionForNonExistigPet()
        {
            var existingPetOwner = new PetOwner { OwnerId = 1, PetOwnerId = 1, PetId = 1 };
            short nonExistingPetId = 2;
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner });
            Action act = () => petOwnerRepo.DeleteAllPetOwnerByPetId(nonExistingPetId);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.Pet.DataNotFound);
        }

        [TestMethod]
        public void SuccessfullyDeletingPetOwnerByPetId()
        {
            var existingPetOwner = new PetOwner { OwnerId = 1, PetOwnerId = 1, PetId = 1 };
            var existingPetOwner2 = new PetOwner { OwnerId = 2, PetOwnerId = 1, PetId = 2 };
            short existingPetId = 2;
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner, existingPetOwner2 });
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = petOwnerRepo.DeleteAllPetOwnerByPetId(existingPetId);
            result.Should().BeTrue();
        }
        #endregion
    }
}
