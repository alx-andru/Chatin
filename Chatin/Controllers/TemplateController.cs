
using System.Web.Mvc;


namespace Chatin.Controllers
{
    public class TemplateController : Controller
    {
        public ActionResult Message()
        {
            return View();
        }
    }
}
