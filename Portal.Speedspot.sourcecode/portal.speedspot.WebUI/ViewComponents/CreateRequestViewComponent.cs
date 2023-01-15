using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "CreateRequest")]
    public class CreateRequestViewComponent : ViewComponent
    {
        private readonly ApplicationUserManager _userManager;
        public readonly LocalizationService _localizationService;
        public readonly IMapper _mapper;

        public CreateRequestViewComponent(IMapper mapper,
            ApplicationUserManager usermanager,
            LocalizationService localizationService)
        {
            _mapper = mapper;
            _userManager = usermanager;
            _localizationService = localizationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(Request.HttpContext.User);
            var myEmployees = await _userManager.GetAllUsersExceptAsync(user.Id);
            ViewData["RequestFromId"] = myEmployees.Select(u => _mapper.Map<UserViewModel>(u)).ToList();

            return View( new GeneralRequestViewModel());
        }
    }
}
