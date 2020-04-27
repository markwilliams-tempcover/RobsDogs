using System.Web.Mvc;
using Ui.Data;
using Ui.Entities;
using Ui.Services;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {
        // GET: RobsDogs
        public ActionResult Index()
        {
            var dogOwnerRepository = new DogOwnerRepository();
            var dogRepository = new DogRepository();

            var dogOwnerService = new DogOwnerService(dogOwnerRepository);
            var dogService = new DogService(dogRepository);

			var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(dogOwnerService, dogService);
	        var dogOwnerListViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            return View(dogOwnerListViewModel);
        }
    }
}