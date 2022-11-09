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
        [AllowAnonymous]
        [HttpPost("GetParameter")]
        public virtual IActionResult GetParameter([FromBody] InputDeviceParameterDTO body)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = body.UserId;
            param["BUID"] = body.BUID;
            param["Lang"] = body.Lang;
            param["AssetId"] = "COMPRESSOR_0001";
            param["InputDate"] = body.InputDate;
            
            var dt = _inputDeviceParameterService.GetParameter("FA_tblInputAssetOperating_Mobile_GetParameter", param);

            IEnumerable<TestDemo> result = dt.AsEnumerable().Select(x => new TestDemo()
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
    public class TestDemo
    {
        [JsonProperty("AssetId")]
        public string AssetId { get; set; }

        [JsonProperty("InputDate")]
        public DateTime InputDate { get; set; }

        [JsonProperty("OperatingId")]
        public string OperatingId { get; set; }

        [JsonProperty("OperatingName")]
        public string OperatingName { get; set; } = null;

        [JsonProperty("UMID")]
        public string UMID { get; set; }

        [JsonProperty("Value")]
        public decimal Value { get; set; }

        [JsonProperty("Note")]
        public string Note { get; set; } = null;

        [JsonProperty("RecordID")]
        public Guid RecordID { get; set; }

        
        //[JsonProperty("CreatedOn")]
        //public string CreatedOn { get; set; }

        //[JsonProperty("CreatedBy")]
        //public string CreatedBy { get; set; }


        //[JsonProperty("ModifiedOn")]
        //public string ModifiedOn { get; set; }

        //[JsonProperty("ModifiedBy")]
        //public string ModifiedBy { get; set; }

    }
    
}
