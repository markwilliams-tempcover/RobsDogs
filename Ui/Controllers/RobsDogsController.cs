using System.Web.Mvc;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {
        IDogOwnerViewModelMapper _dogOwnerVMMapper;
        public RobsDogsController(IDogOwnerViewModelMapper dogOwnerVMMapper)
        {
            _dogOwnerVMMapper = dogOwnerVMMapper;
        }
        // GET: RobsDogs
        public ActionResult Index()
        { 
	        var dogOwnerListViewModel = _dogOwnerVMMapper.GetAllDogOwners();

            return View(dogOwnerListViewModel);
        }
    }
}