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

    public class WorkPermitController : BaseController
    {
        public IMessage _message;
        public IAppSettings _appSettings;
        public IInputRequestService _inputRequestService;
        IDao _dao;

        public WorkPermitController(IMessage message, IAppSettings appSettings, IInputRequestService inputRequestService, IDao dao)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _inputRequestService = inputRequestService ?? throw new ArgumentNullException(nameof(inputRequestService));
            _dao = dao;
        }

        [HttpPost]
        public virtual IActionResult SaveWorkPermitAsync([FromBody] WorkPermitBody body)
        {
            var tokenCurrent = HttpContextToKen.GetHttpContextToKen(this.User);

            object paras = new
            {
                BUID = tokenCurrent["BUID"].ToString(),
                Lang = tokenCurrent["LANG"].ToString(),
                UserId = tokenCurrent["USERID"].ToString(),
                Contractor = body.Contractor,
                DeviceDescription = body.DeviceDescription,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                VendorManager = body.VendorManager,
                WorkDescription = body.WorkDescription,
                numofEmployees = body.numofEmployees,
                OtherSafe = body.OtherSafe,
                LaborProtection = body.LaborProtection,
                Helmet = body.Helmet,
                Goggles = body.Goggles,
                Safetyshoes = body.Safetyshoes,
                Soldermask = body.Soldermask,
                Facemask = body.Facemask,
                Cutmask = body.Cutmask,
                Gloves = body.Gloves,
                Apron = body.Apron,
                Chemicalresistantsuit = body.Chemicalresistantsuit,
                Gasmasks = body.Gasmasks,
                Antinoisedevice = body.Antinoisedevice,
                Hazard = body.Hazard,
                controlmeasures = body.controlmeasures,
            };
            try
            {
                var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Save", paras);
                return new OkObjectResult(ReturnOk(result));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return null;

        }


        [HttpPost("SendWorkPermit")]
        public virtual IActionResult SendWorkPermit([FromBody] WorkPermitBody body)
        {
            var tokenCurrent = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new
            {
                BUID = tokenCurrent["BUID"].ToString(),
                Lang = tokenCurrent["LANG"].ToString(),
                UserId = tokenCurrent["USERID"].ToString(),
                WorkPermitNo = body.WorkPermitNo,
                ProjectManager = body.ProjectManager,
                ZoneManager = body.ZoneManager,
                SafeManager = body.SafeManager,
                @ErrorMessage = string.Empty
            };
            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }
            var result = _dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Send", paras);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("SaveImageWorkPermit")]
        public virtual IActionResult SaveImage([FromBody] WorkPermitBody body)
        {
            var tokenCurrent = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new
            {
                BUID = tokenCurrent["BUID"].ToString(),
                Lang = tokenCurrent["LANG"].ToString(),
                UserId = tokenCurrent["USERID"].ToString(),
                WorkPermitNo = body.WorkPermitNo,
                FileImage = body.FileImage,
                FileName = body.FileName,
                FileSize = 2000,
                FileType = "png"
            };
            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }
            var result = _dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_SaveImage", paras);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpGet("GetWorkPermits")]
        [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> GetWorkPermits()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Load", param);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpGet("GetVerdorManagements")]
        public async Task<IActionResult> GetVerdorManagements()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new { UserID = token["USERID"] };
            IEnumerable<object> result = this._dao.StatementQuery<object>("QueryVendorManagers", paras);

            return new OkObjectResult(ReturnOk(result));
        }

        [HttpGet("QueryZoneManagers")]
        public async Task<IActionResult> QueryZoneManagers()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new { UserID = token["USERID"] };
            IEnumerable<object> result = this._dao.StatementQuery<object>("QueryZoneManagers", paras);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpGet("QuerySaleManagers")]
        public async Task<IActionResult> QuerySaleManagers()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new { UserID = token["USERID"] };
            IEnumerable<object> result = this._dao.StatementQuery<object>("QuerySaleManagers", paras);

            return new OkObjectResult(ReturnOk(result));
        }
        [HttpGet("GetProjectManagers")]
        public async Task<IActionResult> GetProjectManagers()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new { UserID = token["USERID"] };
            IEnumerable<object> result = this._dao.StatementQuery<object>("QueryProjectManagers", paras);

            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("GetWorkPermitImages")]
        public async Task<IActionResult> GetWorkPermitImages([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];
            param["WorkPermitNo"] = body.WorkPermitNo;
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_View", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("DeleteWorkPermitImage")]
        public async Task<IActionResult> DeleteWorkPermitImage([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["RecID"] = body.RecID;
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Delete_AttachmentFile", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("EditWorkPermitImage")]
        public async Task<IActionResult> EditWorkPermitImage([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new
            {
                RecID = body.RecID,
                FileImage = body.FileImage,
                FileName = body.FileName,
                FileSize = 2000,
                FileType = "png"
            };
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Update_AttachmentFile", paras);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("Approval")]
        public async Task<IActionResult> Approval([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang = token["LANG"], Status = body.Status, WorkPermitNo = body.WorkPermitNo, @UserId = token["USERID"], UserApproval = body.UserApproval };
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Approval", param);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("NoApproval")]
        public async Task<IActionResult> NoApproval([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang = token["LANG"], Status = body.Status, WorkPermitNo = body.WorkPermitNo, @UserId = token["USERID"], UserApproval = body.UserApproval };
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_NotApproval", param);
            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("CloseWorkerPermit")]
        public async Task<IActionResult> CloseWorkerPermit([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang = token["LANG"], WorkPermitNo = body.WorkPermitNo, @UserId = token["USERID"] };
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_Close", param);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("ExtendWork")]
        public async Task<IActionResult> ExtendWork([FromBody] WorkPermitBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new { BUID = token["BUID"], Lang = token["LANG"], WorkPermitNo = body.WorkPermitNo, @UserId = token["USERID"], ErrorMessage = string.Empty };
            var result = this._dao.ExecuteSP("SAFVIET_frmWorkPermit_Mobile_GiaHan", param);
            return new OkObjectResult(ReturnOk(param));
        }
    }
}