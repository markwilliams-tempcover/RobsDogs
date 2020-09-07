using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ui.Controllers;
using Ui.Data;
using Ui.Entities;
using Ui.Models;
using Ui.Services;
using Ui.Tests.Helpers;
using Ui.ViewModelMappers;

namespace Ui.Tests.Controllers
{
	[TestClass]
	public class RobsDogsControllerTest
	{
        public TestContext TestContext
        {
            get;
            set;
        }

        [TestMethod]
        public void EnsureModelReturnedIsAsExpectedTests()
        {
            foreach (var scenario in TestScenarios())
            {
                // write the scenario we are running to the test context (so it appears in the test result viewer)
                TestContext.WriteLine($"Scenario: {scenario.Item1}");

                // create new mock for each scenario
                var mock = new Mock<IDogOwnerRepository>();
                mock.Setup(mthd => mthd.GetAllDogOwners())
                    .Returns(scenario.Item2);

                // Arrange
                var controller =
                    new RobsDogsController(new DogOwnerViewModelMapper(new DogOwnerService(mock.Object)));

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(result);

                // Should be testing/verifying call to GetAllDogOwners and subsequent methods down the stack.
                // Moq is installed to help you.
                mock.Verify(mthd => mthd.GetAllDogOwners(), Times.Once());

                var model = result.Model as DogOwnerListViewModel;
                Assert.IsNotNull(model);

                // assert that the owner counts tally
                Assert.IsTrue(model.DogOwnerViewModels.Count == scenario.Item2.Count);

                // assert that the view model matches expected 
                CollectionAssert.AreEqual(model.DogOwnerViewModels, scenario.Item2, new TestComparer() );

            }
        }

        // [DMB] probably would suggest using NUnit at this point
        IEnumerable<Tuple<string, List<DogOwner>>> TestScenarios()
        {
            yield return new Tuple<string, List<DogOwner>>("One owner, one dog", new List<DogOwner>
            {
                new DogOwner
                {
                    Owner = new Owner()
                    {
                        OwnerName = "Rob"
                    },
                    Dogs = new List<Dog>
                    {
                        new Dog {DogName = "Willow"}
                    }
                }
            });


            yield return new Tuple<string, List<DogOwner>>("One owner, three dogs",new List<DogOwner>
            {
                new DogOwner
                {
                    Owner = new Owner()
                    {
                        OwnerName = "Rob"
                    },
                    Dogs = new List<Dog>
                    {
                        new Dog {DogName = "Willow"},
                        new Dog {DogName = "Nook"},
                        new Dog {DogName = "Sox"}
                    }
                }
            });

            yield return new Tuple<string, List<DogOwner>>("Two owners, first has 3 dogs, second has 1", new List<DogOwner>
            {
                new DogOwner
                {
                    Owner = new Owner()
                    {
                        OwnerName = "Rob"
                    },
                    Dogs = new List<Dog>
                    {
                        new Dog {DogName = "Willow"},
                        new Dog {DogName = "Nook"},
                        new Dog {DogName = "Sox"}
                    }
                },
                new DogOwner
                {
                    Owner = new Owner()
                    {
                        OwnerName = "Sue"
                    },
                    Dogs = new List<Dog>
                    {
                        new Dog{ DogName = "Trixie"}
                    }
                }
            });

        }
    }
}