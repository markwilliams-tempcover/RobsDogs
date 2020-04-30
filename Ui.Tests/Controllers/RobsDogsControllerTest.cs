using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Controllers;
using Ui.Entities;
using Ui.Models;
using Ui.ViewModelMappers;

namespace Ui.Tests.Controllers
{
	[TestClass]
	public class RobsDogsControllerTest
	{
		[TestMethod]
		public void Index()
		{
            var dogOwnerViewModelMapper = new Mock<IDogOwnerViewModelMapper>();
            var dogOwner = new DogOwner() { OwnerName = "Qwe", DogNameList = new List<string> { "q", "w", "e" } };
            var dogOwners = new List<DogOwner> { dogOwner };
            var dogOwnerListViewModel = new DogOwnerListViewModel {
                DogOwnerViewModels = dogOwners.Select(e => new DogOwnerViewModel {
                    OwnerName = e.OwnerName,
                    DogNames = e.DogNameList
                }).ToList()
            };

            dogOwnerViewModelMapper.Setup(x => x.GetAllDogOwners()).Returns(dogOwnerListViewModel);
            //Arrange
            RobsDogsController controller = new RobsDogsController(dogOwnerViewModelMapper.Object);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dogOwnerListViewModel, result.Model);
            //Should be testing / verifying call to GetAllDogOwners and subsequent methods down the stack.
            //Moq is installed to help you.
        }
	}
}