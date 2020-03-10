using System.Web.Mvc;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {
        // GET: RobsDogs
        public ActionResult Index()
        {
			var dogOwnerViewModelMapper = new DogOwnerViewModelMapper();
	        var dogOwnerListViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            return View(dogOwnerListViewModel);
        }
    }
}