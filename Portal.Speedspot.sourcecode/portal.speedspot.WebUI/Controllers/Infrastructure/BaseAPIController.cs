using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.IdentityModule;

namespace portal.speedspot.WebUI.Controllers.Infrastructure
{
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        protected IMapper _mapper;
        public readonly ApplicationUserManager _userManager;

        public BaseAPIController()
        {

        }

        public BaseAPIController(IMapper mapper, ApplicationUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
    }
}
