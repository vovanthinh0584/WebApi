using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    public class AccountController : BaseController
    {
        IMessage _message;
        IAccountService _accountService;
        IAppSettings _appSettings;
        public AccountController(IMessage Message, IAccountService accountService, IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _message = Message;
            _accountService = accountService;
        }
        [AllowAnonymous]
        [HttpGet("GetCaptionLanguage")]
        public virtual IActionResult GetListLanguages(string formName, string lang)
        {


            object paras = new
            {
                FormName = formName,
                Lang = lang
            };
            var result = _accountService.GetListCaptions("GetListCaptionOfForm", paras);
            return new ObjectResult(ReturnOk(result));
        }
        [AllowAnonymous]
        [HttpGet("GetListBussiness")]
        public virtual IActionResult GetListBussiness()
        {
            var result = _accountService.GetListBussiness("GetListBussiness");
            return new ObjectResult(ReturnOk(result));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public virtual IActionResult Login([FromBody] User user)
        {
            var keyIv = (user.UserID + "0000000000000000").Substring(0, 16);
            user.Password = Encrypting.AesDecrypt(user.Password, Encoding.UTF8.GetBytes(keyIv), Encoding.UTF8.GetBytes(keyIv), Encoding.UTF8);
            var result = _accountService.Login("GetUser", user);
            if (!result)
            {
                return new ObjectResult(ReturnInvalid(user.Message));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserID),
                    new Claim("Language", user.Language),
                    new Claim("BusinessUnitID",user.BusinessUnitID),
                }),
                Expires = DateTime.UtcNow.AddMinutes(MBLConstants.TOKEN_EXPIRATION),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            user.TOKEN = tokenString;
            user.SessionExpires = tokenDescriptor.Expires.Value;
            user.Password = null;
            return new OkObjectResult(ReturnOk(user));
        }
    }
}
