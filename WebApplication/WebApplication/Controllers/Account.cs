using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    public class Account : BaseController
    {
        IMessage _message;
        ICaptionService _captionService;
        public  Account(IMessage Message,ICaptionService captionService)
        {
            _message = Message;
            _captionService = captionService;
        }
        [AllowAnonymous]
        [HttpGet]
        public virtual IActionResult GetListLanguages(string formName,string lang)
        {
            object paras = new
            {
                FormName = formName,
                Lang = lang
            };
       
            var result = _captionService.GetListCaptions("GetListCaptionOfForm", paras);
           
          
            return new ObjectResult(ReturnOk(result));
        }
    }
}
