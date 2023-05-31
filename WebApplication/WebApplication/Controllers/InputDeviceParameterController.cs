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

    public class InputDeviceParameterController : BaseController
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
        public virtual IActionResult CreateInputDeviceParameter([FromBody] InputDeviceParameterDTO body)
        {

            string m = _inputDeviceParameterService.CreateInputDeviceParameter("SAFVIET_frmDeviceParameter_Mobile_Save", body);

            return new OkObjectResult(ReturnOk(m));
        }
        [AllowAnonymous]
        [HttpGet]
        public virtual IActionResult GetInputDeviceParameter()
        {
            var listAsset = _inputDeviceParameterService.GetListAsset("GetListAsset", null);
            var listOperating = _inputDeviceParameterService.GetListOperating("GetListOperating", null);
            var listUM = _inputDeviceParameterService.GetListUM("GetListUM", null);

            var zones = _inputDeviceParameterService.GetListZone();
            var shifts = _inputDeviceParameterService.GetListShift();
            var devices = _inputDeviceParameterService.GetListDevice();
            var times = _inputDeviceParameterService.GetListTime();
            return new OkObjectResult(ReturnOk(new { listAsset, listOperating, listUM, zones, shifts, devices, times }));
        }

        [AllowAnonymous]
        [HttpPost("GetParameter")]
        public virtual IActionResult GetParameter([FromBody] InputDeviceParameterDTO body)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();

            param["BUID"] = body.BUID;
            param["LANG"] = body.Lang;
            param["USERID"] = body.UserId;
            //param["Zone"] = body.Zone;
            param["Zone"] = "12";
            //param["Device"] = body.Device;
            param["Device"] = "E13.31";
            //param["Date"] = body.InputDate;
            param["Date"] = new DateTime(2023, 5, 17);
            //param["Shift"] = body.Shift;
            param["Shift"] = "CA1";
            //param["Time"] = body.Time;
            param["Time"] = 9;

            var dt = _inputDeviceParameterService.GetParameter("SAFVIET_frmDeviceParameter_Mobile_GetCheckinglist", param);
            var dt1 = dt.AsEnumerable().ToList();
            IEnumerable<InputDeviceParameterDTO> result = dt.AsEnumerable().Select(x => new InputDeviceParameterDTO()
            {
                //AssetId = x.Field<string>("AssetId"),
                //InputDate = x.Field<DateTime>("InputDate"),
                //OperatingId = x.Field<string>("OperatingId"),
                //UMID = x.Field<string>("UMID"),
                //Value = x.Field<decimal>("Value"),
                //RecordID = x.Field<Guid>("RecordID")

                Id = x.Field<Int64>("Id"),
                Zone = x.Field<string>("ZoneId"),
                Device = x.Field<string>("DeviceId"),
                Date = x.Field<DateTime>("Date"),
                Shift = x.Field<string>("ShiftID"),
                Time = x.Field<Int64>("AttimeId"),
                ChecklistID = x.Field<string>("ChecklistID"),
                StandardValue = x.Field<string>("StandardValue"),
                Confirm = x.Field<bool>("Confirm"),
                NonConfirm = x.Field<bool>("NonConfirm"),
                Value = x.Field<string>("Value"),
            }).ToList();


            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("CloseInput")]
        public virtual IActionResult CloseInput([FromBody] InputDeviceParameterDTO body)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();

            param["BUID"] = body.BUID;
            param["LANG"] = body.Lang;
            param["USERID"] = body.UserId;
            param["Device"] = body.Device;

            var result = _inputDeviceParameterService.GetParameter("SAFVIET_frmDeviceParameter_Mobile_Close", param);

            return new OkObjectResult(ReturnOk(result));
        }
    }


}
