using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.Models.Constants;

namespace portal.speedspot.WebAPI.Infrastracture
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMapper _mapper;

        public BaseApiController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
