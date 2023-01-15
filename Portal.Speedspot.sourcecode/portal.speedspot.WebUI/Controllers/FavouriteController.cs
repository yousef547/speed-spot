using Microsoft.AspNetCore.Mvc;
using portal.speedspot.WebUI.Controllers.Infrastructure;

namespace portal.speedspot.WebUI.Controllers
{
    public class FavouriteController : BaseController
    {
        public IActionResult ReloadViewComponent()
        {
            return ViewComponent("Favourite");
        }
    }
}
