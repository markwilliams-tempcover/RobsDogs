using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Controllers;
using Ui.Services;

namespace Ui.Tests.Controllers
{
	[TestClass]
	public class RobsDogsControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			var mockDogService = new Mock<IDogOwnerService>();
			RobsDogsController controller = new RobsDogsController(mockDogService.Object);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			// Should be testing/verifying call to GetAllDogOwners and subsequent methods down the stack.
			// Moq is installed to help you.
		}
	}
}