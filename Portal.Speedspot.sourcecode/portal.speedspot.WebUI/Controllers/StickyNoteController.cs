using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class StickyNoteController : BaseController
    {
        public readonly StickyNotesBiz _StickyNotesBiz;
        public StickyNoteController(ApplicationUserManager userManager, IMapper mapper, StickyNotesBiz stickyNotesBiz) : base(mapper, userManager)
        {
            _StickyNotesBiz = stickyNotesBiz;
        }


        public async Task<IActionResult> EditNote(int id)
        {
            ViewBag.isEdit = true;
            var note = _mapper.Map<StickyNoteViewModel>(await _StickyNotesBiz.GetByIdAsync(id));
            return PartialView("_CreateStickyNotePartial", note);
        }

        public async Task<IActionResult> DeleteStickyNote(int Id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _StickyNotesBiz.DeleteAsync(Id, userId);
            return Ok(new
            {
                result = result,
            });
        }
    }
}
