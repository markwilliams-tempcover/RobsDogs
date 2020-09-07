using System.Web.Mvc;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {
        private readonly IDogOwnerViewModelMapper _dogOwnerViewModelMapper;

        public RobsDogsController(IDogOwnerViewModelMapper dogOwnerViewModelMapper )
        {
            _dogOwnerViewModelMapper = dogOwnerViewModelMapper;
        }

        // GET: RobsDogs
        public ActionResult Index()
        {
            var dogOwnerListViewModel = _dogOwnerViewModelMapper.GetAllDogOwners();

            return View(dogOwnerListViewModel);
        }
    }
}