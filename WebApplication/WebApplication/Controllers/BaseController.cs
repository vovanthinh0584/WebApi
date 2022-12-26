using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [LogoutToken]
    public class BaseController : ControllerBase
    {
       
        public BaseController()
        {
        }
        protected virtual ServiceResult ReturnOk(object data)
        {
            var _serviceResult = new ServiceResult(MBLConstants.SUCCESS_RESPONSE_CODE, data, null, null);
            return _serviceResult;
        }
        protected virtual ServiceResult ReturnInvalid(string errorMessage)
        {
            var _serviceResult = new ServiceResult(MBLConstants.INVALID_RESPONSE_CODE, null, errorMessage);
            return _serviceResult;
        }
    }
}
