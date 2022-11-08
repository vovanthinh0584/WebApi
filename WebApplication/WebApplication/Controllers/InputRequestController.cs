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
    public sealed class InputRequestController : BaseController
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateInputRequestAsync([FromBody] CreateRequestInputBody body)
        {
            string lang = "vi-VN";
            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }    

            string m = await _inputRequestService.CreateInputRequestAsync(body);

            if (string.IsNullOrEmpty(m))
            {
                return new ObjectResult(ReturnOk(_message.GetMessage("MBL00002", lang)));
            }

            return base.BadRequest(m);
        }

        [AllowAnonymous]
        [HttpGet("QueryWorkShops")]
        public async Task<IActionResult> QueryWorkShopsAsync()
        {
            //string BUID = this.User.Claims.FirstOrDefault(c => c.Type == "BusinessUnitID").Value;
            string BUID = "SAFVIET";
            IEnumerable<WorkShopSummary> workShops = await this._inputRequestService.QueryWorkShopsAsync(BUID);

            return base.Ok(workShops);
        }

        [AllowAnonymous]
        [HttpGet("QueryLocations")]
        public async Task<IActionResult> QueryLocations()
        {
            //string BUID = this.User.Claims.FirstOrDefault(c => c.Type == "BusinessUnitID").Value;
            string BUID = "SAFVIET";
            IEnumerable<LocationSummary> locations = await this._inputRequestService.QueryLocationsAsync(BUID);

            return base.Ok(locations);
        }
    }
}
