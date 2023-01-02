using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.RequestBody.InputRequest;

namespace WebApplication.Services
{
    public interface IInputRequestService
    {
        Task<string> CreateInputRequestAsync(CreateRequestInputBody body);
        Task<IEnumerable<WorkShopSummary>> QueryWorkShopsAsync(string BuId);
        Task<IEnumerable<LocationSummary>> QueryLocationsAsync(string BuId);

        DataTable GetListRequest(string statementSql, IDictionary<string, object> param);

        IEnumerable<ZoneList> QueryListZone();


        int ComfirmRequest(string sqlString, object param);

        object GetAdminMTN();
    }
}
