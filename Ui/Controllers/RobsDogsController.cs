using System.Web.Mvc;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {

        private readonly IDogOwnerViewModelMapper dogOwnerViewModelMapper;

        public RobsDogsController(IDogOwnerViewModelMapper dogOwnerViewModelMapper) {
            this.dogOwnerViewModelMapper = dogOwnerViewModelMapper;
        }

        // GET: RobsDogs
        public ActionResult Index()
        {
	        var dogOwnerListViewModel = dogOwnerViewModelMapper.GetAllDogOwners();

            return View(dogOwnerListViewModel);
        }
    }
}