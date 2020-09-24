using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui;
using Ui.Data;
using Ui.Entities;
using FluentAssertions;
namespace Ui.Tests.Data
{
    [TestClass]
    public class OwnerRepositoryTests
    {
        public Mock<IDbData> mockData;
        public OwnerRepository OwnerRepo;
        [TestInitialize]
        public void Setup()
        {
            mockData = new Mock<IDbData>();
            OwnerRepo = new OwnerRepository(mockData.Object);
        }
        #region AddOwner
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "OwnerRepo")]
        public void AddOwnerThrowExceptionOnInvalidEntry()
        {
            OwnerRepo.AddOwner(null);
        }

        [TestMethod]
        public void AddOwnerThrowExceptionForExistigOwner()
        {
            var duplicateUserName = "DuplicateUserName";
            var existingOwner = new Owner { Name = duplicateUserName, OwnerId = 1 };
            var newOwner = new Owner { Name = duplicateUserName };
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner });
            Action act = () => OwnerRepo.AddOwner(newOwner);
            act.Should().Throw<ArgumentException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.Owner.DataAlreadyExists);
        }
        //happy path
        [TestMethod]
        public void SuccesfullyAddingNewOwner()
        {
            var existingOwner = new Owner { Name = "ExistingUser", OwnerId = 1 };
            var newOwner = new Owner { Name = "NewUser" };
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner });
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = OwnerRepo.AddOwner(newOwner);
            result.Should().BeTrue();
        }
        #endregion
        #region UpdateOwner
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "OwnerRepo")]
        public void UpdateOwnerThrowExceptionOnInvalidEntry()
        {
            OwnerRepo.UpdateOwner(null);
        }

        [TestMethod]
        public void UpdateOwnerThrowExceptionForNonExistigOwner()
        {
            var existingOwner = new Owner { Name = "ExsitingUser", OwnerId = 1 };
            var nonExistingOwner = new Owner { Name = "UpdateNonExistingUser", OwnerId = 2 };
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner });
            Action act = () => OwnerRepo.UpdateOwner(nonExistingOwner);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.Owner.DataNotFound);
        }
        [TestMethod]
        public void SuccessfullyUpdatingOwner()
        {
            var existingOwner = new Owner { Name = "ExsitingUser", OwnerId = 1 };
            var existingOwner2 = new Owner { Name = "ExsitingUser2", OwnerId = 2 };
            var updateUserName = "UpdatedUser";
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner, existingOwner2 });
            existingOwner2.Name = updateUserName;
            var result = OwnerRepo.UpdateOwner(existingOwner2);
            result.Name.Should().BeSameAs(updateUserName);
        }
        #endregion
        #region DeleteOwner

        [TestMethod]
        public void DeleteOwnerThrowExceptionForNonExistigOwner()
        {
            var existingOwner = new Owner { Name = "ExsitingUser", OwnerId = 1 };
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner });
            Action act = () => OwnerRepo.DeleteOwner(2);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(PetOwnerConstants.ErrorMessages.Owner.DataNotFound);
        }
        [TestMethod]
        public void SuccessfullyDeletingOwnerDeletesPetOwnerToo()
        {
            var existingOwner = new Owner { Name = "ExsitingUser", OwnerId = 1 };
            var existingOwner2 = new Owner { Name = "ExsitingUser2", OwnerId = 2 };
            var existingPet = new Pet { Name = "Pet2", Age = 1, PetId = 1, PetType = 1 };
            var existingPetOwner = new PetOwner { PetOwnerId = 1, OwnerId = existingOwner.OwnerId, PetId = existingPet.PetId};
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner, existingOwner2 });
            mockData.Setup(x => x.PetOwners).Returns(new List<PetOwner> { existingPetOwner }).Verifiable();
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = OwnerRepo.DeleteOwner(2);
            result.Should().BeTrue();
            mockData.Verify(x => x.PetOwners, Times.AtLeastOnce);
        }

        [TestMethod]
        public void SuccessfullyDeletingOwner()
        {
            var existingOwner = new Owner { Name = "ExsitingUser", OwnerId = 1 };
            var existingOwner2 = new Owner { Name = "ExsitingUser2", OwnerId = 2 };
            mockData.Setup(x => x.Owners).Returns(new List<Owner> { existingOwner, existingOwner2 });
            mockData.Setup(x => x.SaveChanges()).Returns(true);
            var result = OwnerRepo.DeleteOwner(2);
            result.Should().BeTrue();
        }
        #endregion
    }
}
