using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Models.RequestBody.GetTask;
using WebApplication.Services;
using WebApplication.Utils;

namespace WebApplication.Controllers
{

    public class TaskController : BaseController
    {
        IGetTaskService getTaskService;
        public TaskController(IGetTaskService getTaskService)
        {
            this.getTaskService = getTaskService ?? throw new ArgumentException(nameof(getTaskService));
        }


        [HttpPost("SearchListWork")]
        public virtual IActionResult SearchListWork(CreateGetTaskBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["ColField"] = "worktype";
            param["ValSearch"] = body.FilterType;
            var result = getTaskService.QueryFilterWorks("SAFVIET_tblWorks_procGetAll_Mobile", param);
            return new OkObjectResult(ReturnOk(result));

        }
        [HttpPost("GetTypeList")]
        public virtual IActionResult GetTypeList()
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            var result = getTaskService.GetTypeList();
            return new OkObjectResult(ReturnOk(result));

        }


        [HttpPost("AssignWork")]
        public virtual IActionResult AssignWork(CreateGetTaskBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new
            {
                UserId = token["USERID"],
                BUID = token["BUID"],
                LANG = token["LANG"],
                WorkNo = body.WorkNo,
                TeamId = body.Team,
                WorkerId = body.Worker,
                Level = body.Level,
                KindOfWork = body.KindOfWork,
                Classification = body.Classification
            };

            int result = getTaskService.AssignWorks("SAFVIET_AssignTask_Mobile_Apply", param);

            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("FinishedWork")]
        public virtual IActionResult FinishedWork([FromBody]CreateGetTaskBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);

            object paras = new
            {
                UserId = token["USERID"],
                BUID = token["BUID"],
                LANG = token["LANG"],
                WorkNo = body.WorkNo
            };

            int result = getTaskService.FinishTask("SAFVIET_tblWorks_Mobile_Finished", paras);

            return new OkObjectResult(ReturnOk(result));
        }
        [HttpGet("QueryWorkers")]
        public virtual IActionResult QueryWorkers()
        {
            IEnumerable<WorkerDTO> result = this.getTaskService.QueryWorkers();

            return new OkObjectResult(ReturnOk(result));
        }
        [HttpGet("QueryTeams")]
        public virtual IActionResult QueryTeams()
        {
            IEnumerable<TeamDTO> result = this.getTaskService.QueryTeams();

            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("GetWorkTypes")]
        public virtual IActionResult GetWorkTypes()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Field"] = "worktype";
            
            var result = getTaskService.GetWorkTypes("SAFVIET_frmWorks_proLoadComboxSearch", param);

            return new OkObjectResult(ReturnOk(result));
        }
    }

   
}
