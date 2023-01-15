using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "StickyNotes")]
    public class StickyNotesViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly StickyNotesBiz _stickyNotesBiz;
        private readonly ApplicationUserManager _userManager;

        public StickyNotesViewComponent(IMapper mapper, StickyNotesBiz stickyNotesBiz, ApplicationUserManager userManager)
        {
             _mapper = mapper;
            _stickyNotesBiz = stickyNotesBiz;
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync(int offset = 0)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var stickyNotes =  _mapper.Map<List<StickyNoteViewModel>>(await _stickyNotesBiz.GetAllNotesByUserId(user.Id)).ToList();
            stickyNotes.ForEach(n => n.CreatedDate = n.CreatedDate.AddMinutes(-offset));
            return View(stickyNotes);
        }
    }
}
