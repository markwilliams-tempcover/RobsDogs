using System.Web.Mvc;
using Ui.ViewModelMappers;

namespace Ui.Controllers
{
    public class RobsDogsController : Controller
    {
        IPetOwnerViewModelMapper _dogOwnerVMMapper;
        public RobsDogsController(IPetOwnerViewModelMapper dogOwnerVMMapper)
        {
            _dogOwnerVMMapper = dogOwnerVMMapper;
        }
        // GET: RobsDogs
        public ActionResult Index()
        { 
	        var dogOwnerListViewModel = _dogOwnerVMMapper.GetAllPetOwners(null,null);

            return View(dogOwnerListViewModel);
        }
    }
}