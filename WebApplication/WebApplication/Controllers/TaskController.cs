using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Models.RequestBody.GetTask;
using WebApplication.Services;

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
        
            object param = new
            {
                UserId = "Worker_01",
                BUID = "SAFVIET",
                LANG = "vi-VN"
            };

            IEnumerable<QueryGetTaskSummary> GetTasks = getTaskService.QueryGetTask("FA_tblWorkMaintain_GetDateByUserID", param);
            return new OkObjectResult(ReturnOk(GetTasks));
        }
       
        [HttpPost]
        public virtual IActionResult CreateGetTask(CreateGetTaskBody body)
        {
            body.UserId = "Worker_01";
            body.BUID = "SAFVIET";
            body.LANG = "vi-VN";

            int result = getTaskService.CreateGetTask("FA_tblWorkMaintain_Mobile_Confirm", body);

            return new OkObjectResult(ReturnOk(result));
        }
    }
}
