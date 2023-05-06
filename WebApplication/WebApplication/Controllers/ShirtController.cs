using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.RequestBody.InputRequest;
using WebApplication.Services;
using WebApplication.Utils;

namespace WebApplication.Controllers
{

    public class ShiftController : BaseController
    {
        public IMessage _message;
        public IAppSettings _appSettings;
        public IInputRequestService _inputRequestService;
        IDao _dao;

        public ShiftController(IMessage message, IAppSettings appSettings, IDao dao)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _dao = dao;
        }
        [HttpPost("SearhShift")]
        public async Task<IActionResult> SearhShift([FromBody] ShiftsAttimeDTO body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];
            param["Date"] = body.Date;
            param["ShiftId"] = body.ShiftID;
            var result = this._dao.ExecuteSP("SAFVIET_frmTruyVanDSDica_Mobile_Filter", param);

            return new OkObjectResult(ReturnOk(result));
        }
        [HttpGet("GetShifts")]
        public async Task<IActionResult> GetShifts()
        {
            IEnumerable<object> result = this._dao.StatementQuery<object>("QueryShifts", null);
            return new OkObjectResult(ReturnOk(result));
        }
        
    }
}