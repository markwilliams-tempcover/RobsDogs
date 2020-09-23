using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Data;

namespace Ui.Tests.Data
{
    [TestClass]
    public class OwnerRepositoryTests
    {
        public IMock<IDbData> mockData;
        public OwnerRepository OwnerRepo;
        [TestInitialize]
        public void Setup()
        {
            mockData = new Mock<IDbData>();
            OwnerRepo = new OwnerRepository(mockData.Object);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "OwnerRepo")]
        public void AddOwnerThrowExceptionOnInvalidEntry()
        {
            OwnerRepo.AddOwner(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "OwnerRepo")]
        public void UpdateOwnerThrowExceptionOnInvalidEntry()
        {
            OwnerRepo.UpdateOwner(null);
        }
    }
}
