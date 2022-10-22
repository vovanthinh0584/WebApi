using System.Collections.Generic;

namespace WebApplication.Utils
{
    public interface IContext
    {
        void SaveUserPermission(int userid, List<string> lstPhongBan, List<string> lstKhoDuoc);
        //void SaveLogoutToken(int userid, Dictionary<string, long> lstToken);
        List<string> GetUserKhoDuoc(int userid);
        List<string> GetUserPhongBan(int userid);
        //Dictionary<string, long> GetLogoutToken(int userid);
        bool IsTokenBlackList(string token);
        void SetTokenBlackList(string token, long timeExpire);
    }
}
