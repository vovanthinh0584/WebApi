using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
           
            string m = _inputDeviceParameterService.CreateInputDeviceParameter("FA_tblInputAssetOperating_Mobile_Save", body);

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
        [AllowAnonymous]
        [HttpPost("GetParameter")]
        public virtual IActionResult GetParameter([FromBody] InputDeviceParameterDTO body)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = body.UserId;
            param["BUID"] = body.BUID;
            param["Lang"] = body.Lang;
            param["AssetId"] = body.AssetId;
            param["InputDate"] = body.InputDate;
            
            var dt = _inputDeviceParameterService.GetParameter("FA_tblInputAssetOperating_Mobile_GetParameter", param);

            IEnumerable<InputDeviceParameterDTO> result = dt.AsEnumerable().Select(x => new InputDeviceParameterDTO()
            {
                AssetId = x.Field<string>("AssetId"),
                InputDate = x.Field<DateTime>("InputDate"),
                OperatingId = x.Field<string>("OperatingId"),
                UMID = x.Field<string>("UMID"),
                Value = x.Field<decimal>("Value"),
                RecordID = x.Field<Guid>("RecordID")
            }).ToList();


            return new OkObjectResult(ReturnOk(result));
        }
    }
    
    
}
