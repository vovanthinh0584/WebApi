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
    public  class Sheet033BoilerController : BaseController
    {
        IMessage _message;
        IAppSettings _appSettings;
        ISheet033BoilerService _sheet033BoilerService;

        public Sheet033BoilerController(IMessage message, IAppSettings appSettings, ISheet033BoilerService sheet033BoilerService)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _sheet033BoilerService = sheet033BoilerService ?? throw new ArgumentNullException(nameof(sheet033BoilerService));
        }
       

        [AllowAnonymous]
        [HttpPost("GetWorks")]
        public virtual IActionResult GetWorks([FromBody] WorkDTO body)
        {
            object param = new
            {
                BUID = body.BUID
            };
           
            var dt = _sheet033BoilerService.GetListWorks("QueryWorks", param);
            return new OkObjectResult(ReturnOk(dt));
        }

        [AllowAnonymous]
        [HttpPost("GetShiftsAttime")]
        public virtual IActionResult GetListShiftsAttime([FromBody] ShiftsAttimeDTO body)
        {
            object param = new
            {
                BUID = body.BUID,
                ShiftID = body.ShiftID
            };

            var dt = _sheet033BoilerService.GetListShiftsAttime("QueryShiftsAttime", param);
            return new OkObjectResult(ReturnOk(dt));

           
        }
        [AllowAnonymous]
        [HttpPost("GetCheckinglist")]
        public virtual IActionResult GetCheckinglist([FromBody] CheckinglistDTO body)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = body.UserId;
            param["BUID"] = body.BUID;
            param["WorkId"] = body.WorkId;
            param["ShiftId"] = body.ShiftId;
            param["Time"] = body.Time;
            param["Lang"] = body.Lang;
            var dt = _sheet033BoilerService.GetCheckinglist("SAFVIET_tblWorks_Mobile_GetCheckinglist", param);
            return new OkObjectResult(ReturnOk(dt));


        }

        [AllowAnonymous]
        [HttpPost("UpdateCheckinglist")]
        public virtual IActionResult UpdateCheckinglist([FromBody] List<CheckinglistDTO> body)
        {
            _sheet033BoilerService.UpdateCheckinglist("UpdateCheckingList", body);
            return new OkObjectResult(ReturnOk(null));


        }


    }
    
    
}
