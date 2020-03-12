using System.Web.Mvc;
using Ui.Services;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {
        private readonly IDogOwnerService dogOwnerService;

        public RobsDogsController(IDogOwnerService dogOwnerService)
        {
            this.dogOwnerService = dogOwnerService;
        }

        // GET: RobsDogs
        public ActionResult Index()
        {
			var dogOwnerViewModelMapper = new DogOwnerViewModelMapper(dogOwnerService);
	        var dogOwnerListViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            return View(dogOwnerListViewModel);
        }
    }
}