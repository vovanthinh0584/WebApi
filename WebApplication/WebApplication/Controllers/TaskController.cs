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


        [HttpGet]
        public virtual IActionResult QueryGetTask()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            object param = new
            {
                UserId = token["USERID"],
                BUID = token["BUID"],
                LANG = token["LANG"]
            };

            IEnumerable<QueryGetTaskSummary> GetTasks = getTaskService.QueryGetTask("SAFVIET_tblWorks_Mobile_GetTaskByUserID", param);
            return new OkObjectResult(ReturnOk(GetTasks));
        }

        [HttpPost]
        public virtual IActionResult CreateGetTask(CreateGetTaskBody body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);

            body.UserId = token["USERID"].ToString();
            body.BUID = token["BUID"].ToString();
            body.LANG = token["LANG"].ToString();

            int result = getTaskService.CreateGetTask("FA_tblWorkMaintain_Mobile_Confirm", body);

            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("Finished/{WorkNo}")]
        public virtual IActionResult CreateGetTask(string WorkNo)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);

            object body = new
            {
                UserId = token["USERID"],
                BUID = token["BUID"],
                LANG = token["LANG"],
                WorkNo = WorkNo
            };

            int result = getTaskService.FinishTask("SAFVIET_tblWorks_Mobile_Finished", body);

            return new OkObjectResult(ReturnOk(result));
        }
    }
}
