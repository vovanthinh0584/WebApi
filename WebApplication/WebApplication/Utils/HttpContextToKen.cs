using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Utils
{
    public static class HttpContextToKen
    {
        public static IDictionary<string, object> GetHttpContextToKen(ClaimsPrincipal user)
        {
            
            IDictionary<string, object> result = new Dictionary<string, object>();
            result["BUID"] = user.Claims.ToList()[3].Value;
            result["LANG"] = user.Claims.ToList()[2].Value;
            result["USERID"] = user.Claims.ToList()[0].Value;

            return result;
        }
    }
}
