using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public class NotificationsController : BaseController
    {
        public IMessage _message;
        public IAppSettings _appSettings;
        IDao _dao;

        public NotificationsController(IMessage message, IAppSettings appSettings,IDao dao)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _dao = dao;
        }
        [HttpPost("GetTotalNotification")]
        public virtual IActionResult GetTotalNotification()
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];
            param["CountWatched"] = string.Empty;
            var result = _dao.ExecuteSP("SAFVIET_tblNotifications_Mobile_CountWatched", param);

            return new OkObjectResult(ReturnOk(result));
        }
        [HttpPost("GetTotalNotificationNew")]
        public virtual IActionResult GetTotalNotificationNew()
        {

            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];

            param["CountNotWatched"] = string.Empty;
            var result = _dao.ExecuteSP("SAFVIET_tblNotifications_Mobile_CountNotWatched", token);
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("GetListNotification")]
        public virtual IActionResult GetListNotification([FromBody] Notification body)
        {
            var token = HttpContextToKen.GetHttpContextToKen(this.User);
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = token["USERID"];
            param["BUID"] = token["BUID"];
            param["Lang"] = token["LANG"];
            param["BeginNum"] = body.BeginNum;
            param["EndNum"] = body.EndNum;
            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }
            var result = _dao.ExecuteSP("SAFVIET_tblNotifications_Mobile_Load", param);
  
         
           var listNotificationNew = result.AsEnumerable().Where(x=> x.Field<bool>("Watched") ==false).Select(x => 
            x.Field<Int64>("Id")).ToList();
            if(listNotificationNew.Count()>0)
            {
                var stringId = string.Join(",",listNotificationNew);
                object paraNotification = new
                {
                    BUID = token["BUID"].ToString(),
                    Lang = token["LANG"].ToString(),
                    UserId = token["USERID"].ToString(),
                    Id = body.Id
                };
                var result1 = _dao.ExecuteSP("SAFVIET_tblNotifications_Mobile_Update", paraNotification);
            }    
          
            return new OkObjectResult(ReturnOk(result));
        }

        [HttpPost("UpdateNotification")]
        public virtual IActionResult UpdateNotification([FromBody] Notification body)
        {
            var tokenCurrent = HttpContextToKen.GetHttpContextToKen(this.User);
            object paras = new
            {
                BUID = tokenCurrent["BUID"].ToString(),
                Lang = tokenCurrent["LANG"].ToString(),
                UserId = tokenCurrent["USERID"].ToString(),
                Id = body.Id
            };
            if (body is null)
            {
                return base.BadRequest("Have not body value");
            }
            var result = _dao.ExecuteSP("SAFVIET_tblNotifications_Mobile_Update", paras);
            return new OkObjectResult(ReturnOk(result));
        }
    }
}