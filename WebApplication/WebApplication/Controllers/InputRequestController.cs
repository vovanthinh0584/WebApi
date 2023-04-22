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

    public class InputRequestController : BaseController
    {
        public IMessage _message;
        public IAppSettings _appSettings;
        public IInputRequestService _inputRequestService;

        public InputRequestController(IMessage message, IAppSettings appSettings, IInputRequestService inputRequestService)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _inputRequestService = inputRequestService ?? throw new ArgumentNullException(nameof(inputRequestService));
        }


        [HttpPost]
        public async Task<IActionResult> CreateInputRequestAsync([FromBody] CreateRequestInputBody body)
        {
            var tokenCurrent = HttpContextToKen.GetHttpContextToKen(this.User);
            body.BUID = tokenCurrent["BUID"].ToString();
            body.Lang = tokenCurrent["LANG"].ToString();
            body.UserId = tokenCurrent["USERID"].ToString();



            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }
           
            string m = await _inputRequestService.CreateInputRequestAsync(body);

            if (string.IsNullOrEmpty(m))
            {
                return new ObjectResult(ReturnOk(_message.GetMessage("MBL00003", body.Lang)));
            }

            return base.BadRequest(m);
        }


        [HttpGet("QueryWorkShops")]
        public async Task<IActionResult> QueryWorkShopsAsync()
        {
            //string BUID = this.User.Claims.FirstOrDefault(c => c.Type == "BusinessUnitID").Value;
            string BUID = "SAFVIET";
            IEnumerable<WorkShopSummary> workShops = await this._inputRequestService.QueryWorkShopsAsync(BUID);

            return base.Ok(workShops);
        }

        [HttpGet("GetRequests")]
        public async Task<IActionResult> GetRequests()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];
            var result = this._inputRequestService.GetListRequest("SAFVIET_frmMTNRequest_Mobile_Load", param);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpGet("QueryLocations")]
        public async Task<IActionResult> QueryLocations()
        {
            //string BUID = this.User.Claims.FirstOrDefault(c => c.Type == "BusinessUnitID").Value;
            string BUID = "SAFVIET";
            IEnumerable<LocationSummary> locations = await this._inputRequestService.QueryLocationsAsync(BUID);

            return base.Ok(locations);
        }
        [HttpGet("QueryListZone")]
        public async Task<IActionResult> QueryListZone()
        {
            IEnumerable<ZoneList> zoneList = this._inputRequestService.QueryListZone();
            return new OkObjectResult(ReturnOk(zoneList));
        }
        [HttpPost("ComFirmRequest")]
        public async Task<IActionResult> ComFirmRequest(string MTNRequestNum)
        {
            object param = new { MTNRequestNum = MTNRequestNum };
            var result = this._inputRequestService.ComfirmRequest("ComFirmRequest", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("VisibleRequest")]
        public async Task<IActionResult> VisibleRequest([FromBody] CreateRequestInputBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang = token["LANG"], MTNRequestNum = body.MTNRequestNum, @UserId = token["USERID"] };
            var result = this._inputRequestService.VisibleRequest("SAFVIET_frmMTNRequest_Mobile_Visible", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("ApprovalRequest")]
        public async Task<IActionResult> ApprovalRequest([FromBody] CreateRequestInputBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang= token["LANG"], MTNRequestNum = body.MTNRequestNum, @UserId= token["USERID"] };
            var result = this._inputRequestService.ApprovalRequest("SAFVIET_frmMTNRequest_Mobile_Approval", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("NoApprovalRequest")]
        public async Task<IActionResult> NoApprovalRequest([FromBody] CreateRequestInputBody body)
        {
      
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang = token["LANG"], MTNRequestNum = body.MTNRequestNum, @UserId = token["USERID"], NotApprovalDescription=body.NotApprovalDescription };
            var result = this._inputRequestService.NoApprovalRequest("SAFVIET_frmMTNRequest_Mobile_NotApproval", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpGet("AdminMTN")]
        public async Task<IActionResult> GetAdminMTN()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object para = new { UserId = token["USERID"], Lang = token["LANG"], BUID = token["BUID"], ReceiveName =string.Empty };
            var adminMTN = this._inputRequestService.GetAdminMTN(para);
            return new OkObjectResult(ReturnOk(adminMTN));
        }
        [HttpGet("GetListManagement")]
        public async Task<IActionResult> GetListManagement()
        {
            IEnumerable<Management> result =  this._inputRequestService.QueryListManament();

            return new OkObjectResult(ReturnOk(result));
        }

    }
}