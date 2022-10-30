using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
            if(body is null)
            {
                return base.BadRequest("Have not body value");
            }    

            string m = await _inputRequestService.CreateInputRequestAsync(body);

            if (string.IsNullOrEmpty(m))
            {
                return base.Ok();
            }

            return base.BadRequest(m);
        }
    }
}
