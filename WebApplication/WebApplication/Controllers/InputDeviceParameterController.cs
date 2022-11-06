using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.RequestBody.InputRequest;
using WebApplication.Services;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    public  class InputDeviceParameterController : BaseController
    {
        IMessage _message;
        IAppSettings _appSettings;
        IInputDeviceParameterService _inputDeviceParameterService;

        public InputDeviceParameterController(IMessage message, IAppSettings appSettings, IInputDeviceParameterService inputDeviceParameterService)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _inputDeviceParameterService = inputDeviceParameterService ?? throw new ArgumentNullException(nameof(inputDeviceParameterService));
        }
        [AllowAnonymous]
        [HttpPost]
        public virtual IActionResult  CreateInputDeviceParameter([FromBody] InputDeviceParameterDTO body)
        {
           
            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }
            
            string m = _inputDeviceParameterService.CreateInputDeviceParameter("FA_tblInputAssetOperating_Mobile_Save",body);

            return new OkObjectResult(ReturnOk(m));
        }
        [AllowAnonymous]
        [HttpGet]
        public virtual IActionResult GetInputDeviceParameter()
        {
            var listAsset = _inputDeviceParameterService.GetListAsset("GetListAsset", null);
            var listOperating = _inputDeviceParameterService.GetListOperating("GetListOperating", null);
            var listUM = _inputDeviceParameterService.GetListUM("GetListUM", null);
            return new OkObjectResult(ReturnOk(new { listAsset, listOperating, listUM }));
        }
    }
}
