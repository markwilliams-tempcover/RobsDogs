using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Controllers;
using Ui.ViewModelMappers;

namespace Ui.Tests.Controllers
{
	[TestClass]
	public class RobsDogsControllerTest
	{
		public IMock<IPetOwnerViewModelMapper> mockPetOwnerVMMapper;
		[TestInitialize]
		public void Setup()
		{
			mockPetOwnerVMMapper = new Mock<IPetOwnerViewModelMapper>();
	}
		[TestMethod]
		public void Index()
		{
			// Arrange
			RobsDogsController controller = new RobsDogsController(mockPetOwnerVMMapper.Object);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			// Should be testing/verifying call to GetAllDogOwners and subsequent methods down the stack.
			// Moq is installed to help you.
		}
	}
}