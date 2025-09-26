using Microsoft.AspNetCore.Mvc;

namespace CRUD_Mega_Project_dotNET.Controllers
{
    public class PersonsController : Controller
    {
        [Route("/persons/index")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
