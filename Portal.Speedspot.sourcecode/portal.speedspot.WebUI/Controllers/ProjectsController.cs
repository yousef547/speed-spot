using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{

    public class ProjectsController : BaseController
    {
        private readonly ProjectsBiz _projectsBiz;
        public ProjectsController(IMapper mapper,
            ApplicationUserManager userManager,
            ProjectsBiz projectsBiz):base(mapper, userManager)
        {
            _projectsBiz = projectsBiz;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllProject()
        {
            var projects = _mapper.Map<List<ProjectViewModel>>(await _projectsBiz.GetAllAsync());
            return PartialView("_showProjectsPartial", projects);
        }
        public async Task<IActionResult> Details(int id)
        {
            var project = _mapper.Map<ProjectViewModel>(await _projectsBiz.GetByIdAsync(id));
            if(project is null)
            {
                return NotFound();
            }
            return View(project);
        }
    }
}
