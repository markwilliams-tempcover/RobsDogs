using System;
using System.Collections.Generic;
using System.Linq;
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
            var existingPetId = 1;
            short existingOwnerId = 1;
            short nonExistingOwnerId = 2;
            var existingPetOwner = new PetOwner { OwnerId = existingOwnerId, PetOwnerId = 1, PetId = existingPetId };
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner });
            Action act = () => petOwnerRepo.DeletePetOwner(existingPetId, nonExistingOwnerId);
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
            var result = petOwnerRepo.DeletePetOwner(2, 2);
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

        #region GetAllPetOwners
        [TestMethod]
        public void GetAllPetOwnersReturnsDefaultRows()
        {
            var existingPetOwners = GetExistingPetOwners();
            mockData.Setup(x => x.PetOwners).Returns(existingPetOwners);
            var result = petOwnerRepo.GetAllPetOwners();
            result.Count.Should().Be(PetOwnerConstants.DefaultPageSizeForGrid);
        }

        [TestMethod]
        public void GetAllPetOwnersReturnsRowsByPageNumber()
        {
            var existingPetOwners = GetExistingPetOwners();
            var pageSize = 10;
            var pageNumber = 3;
            var expectMinPetOwnerId = 21;
            var expectMaxPetOwnerId = 30;
            mockData.Setup(x => x.PetOwners).Returns(existingPetOwners);            
            var result = petOwnerRepo.GetAllPetOwners(pageSize, pageNumber);
            //exepected range 21-30
            result.Min(x=>x.PetOwnerId).Should().Be(expectMinPetOwnerId);
            result.Max(x => x.PetOwnerId).Should().Be(expectMaxPetOwnerId);
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


        //public List<PetOwner> GetAllPetOwners(int pageSize = PetOwnerConstants.DefaultPageSizeForGrid, int pageId = 1)
        //{
        //    if (_dbData.PetOwners == null)
        //    {
        //        return new List<PetOwner>();
        //    }
        //    return _dbData.PetOwners
        //        .OrderBy(x => x.OwnerId)
        //        .Skip((pageId - 1) * pageSize)
        //        .Take(pageSize).ToList();
        //}
        #endregion
    }
}
