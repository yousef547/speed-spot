using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers.Infrastructure
{
    [Authorize]
    public class BaseController : Controller
    {
        public IWebHostEnvironment _webHostEnvironment;
        protected IMapper _mapper;
        public readonly ApplicationUserManager _userManager;
        public readonly LocalizationService _localizationService;
        public readonly StickyNotesBiz _stickyNotesBiz;
        public readonly UserActivitiesBiz _userActivitiesBiz;

        public BaseController()
        {

        }

        public BaseController(IMapper mapper, IWebHostEnvironment hostEnvironment, ApplicationUserManager userManager)
        {
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        public BaseController(IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            ApplicationUserManager userManager,
            LocalizationService localizationService)
        {
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _localizationService = localizationService;
        }

        public BaseController(IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            ApplicationUserManager userManager,
            LocalizationService localizationService,
            UserActivitiesBiz userActivitiesBiz)
        {
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _localizationService = localizationService;
            _userActivitiesBiz = userActivitiesBiz;
        }


        public BaseController(IMapper mapper, ApplicationUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public BaseController(IMapper mapper, ApplicationUserManager userManager, UserActivitiesBiz userActivitiesBiz)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userActivitiesBiz = userActivitiesBiz;
        }

        public BaseController(IMapper mapper, ApplicationUserManager userManager, LocalizationService localizationService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _localizationService = localizationService;
        }

        public BaseController(IMapper mapper, ApplicationUserManager userManager, LocalizationService localizationService, UserActivitiesBiz userActivitiesBiz)
        {
            _mapper = mapper;
            _userManager = userManager;
            _localizationService = localizationService;
            _userActivitiesBiz = userActivitiesBiz;
        }

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BaseController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public string UploadFile(IFormFile file, string folder)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", folder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public bool DeleteFile(string folder, string fileName)
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", folder, fileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public string BuildEmailTemplate(string templateFileName, params object[] args)
        {
            var pathToFile = _webHostEnvironment.WebRootPath
                          + Path.DirectorySeparatorChar.ToString()
                          + "Templates"
                          + Path.DirectorySeparatorChar.ToString()
                          + "EmailTemplates"
                          + Path.DirectorySeparatorChar.ToString()
                          + templateFileName;

            var builder = new BodyBuilder();

            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, args);
            return messageBody;
        }

        public IActionResult ReloadStickyNoteViewComponent(int offset = 0)
        {
            return ViewComponent("StickyNotes", new { offset });
        }

        public async Task SaveUserActivity(string userId, string Activity, string Description)
        {
            UserActivity userActivity = new()
            {
                UserId = userId,
                Activity = Activity,
                Description = Description,
                CreatedDateTime = DateTime.UtcNow
            };

            await _userActivitiesBiz.AddAsync(userActivity);
        }
    }
}
