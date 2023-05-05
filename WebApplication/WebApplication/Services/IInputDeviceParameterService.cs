using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.RequestBody.InputRequest;

namespace WebApplication.Services
{
    public interface IInputDeviceParameterService
    {
        string CreateInputDeviceParameter(string statementSql, InputDeviceParameterDTO body);
        IEnumerable<object> GetListOperating(string statementSql, object param);
        IEnumerable<object> GetListAsset(string statementSql, object param);

        IEnumerable<object> GetListUM(string statementSql, object param);
        //-------------------------------------------------------------------
        IEnumerable<object> GetListZone();
        IEnumerable<object> GetListShift();
        IEnumerable<object> GetListDevice();
        IEnumerable<object> GetListTime();
        //-------------------------------------------------------------------
        DataTable GetParameter(string statementSql, IDictionary<string, object> param);
    }
}
