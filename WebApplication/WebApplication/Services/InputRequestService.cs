using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Models;
using WebApplication.Models.RequestBody.InputRequest;
using WebApplication.Utils;

namespace WebApplication.Services
{
    public class InputRequestService : IInputRequestService
    {
        IDao _dao;
        IMessage _message;
        public InputRequestService(IDao dao, IMessage message)
        {
            _dao = dao ?? throw new ArgumentException(nameof(dao));
            _message = message ?? throw new ArgumentException(nameof(message));
        }

        private async Task<string> CreateInputRequestAsync(CreateRequestInputBody body)
        {
          
            object paras = new
            {
                UserId = body.UserId,
                BUID = body.BUID,
                Lang = body.Lang,
                ZoneId = body.ZoneId,
                Requester = body.Requester,
                ReceiveName = body.ReceiveName,
                Equipment = body.Equipment,
                Descriptionrequest = body.Descriptionrequest,
                Repair = body.Repair,
                Projectsupporting = body.Projectsupporting,
                Housekeeping = body.Housekeeping,
                Others = body.Others,
                ErrorMessage = string.Empty
            };
            try
            {
                int result = _dao.ExecuteSP("FA_tblMTNRequest_Mobile_Send", paras);
                if (result > 0)
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

           

            return "Cannot create Voucher No";
        }

        public SystemAdmin GetAdminMTN(object para)
        {

            var result = _dao.SingeOrDeFault<SystemAdmin>("GetSysAdmin", para);

            return result;
        }

        private async Task<IEnumerable<WorkShopSummary>> QueryWorkShopsAsync(string buiId)
        {
            object param = new
            {
                BUID = buiId
            };

            string sql = _dao.GetSqlStatement("QueryWorkShops");
            IEnumerable<WorkShopSummary> workShops = _dao.Query<WorkShopSummary>(sql, param);
            return workShops;
        }

        private async Task<IEnumerable<LocationSummary>> QueryLocationsAsync(string buiId)
        {
            object param = new
            {
                BUID = buiId
            };

            string sql = _dao.GetSqlStatement("QueryLocations");
            IEnumerable<LocationSummary> locations = _dao.Query<LocationSummary>(sql, param);
            return locations;
        }
        public DataTable GetListRequest(string statementSql, IDictionary<string, object> param)
        {
            var result = _dao.ExecuteSP(statementSql, param);
            return result;
        }
        public IEnumerable<ZoneList> QueryListZone()
        {
            string sql = _dao.GetSqlStatement("QueryListZone");
            IEnumerable<ZoneList> zoneList = _dao.Query<ZoneList>(sql, null);
            return zoneList;
        }
        public int ComfirmRequest(string sqlString,object param)
        {
            var result = _dao.StatementExecute(sqlString, param);
            return result;
        }
        Task<string> IInputRequestService.CreateInputRequestAsync(CreateRequestInputBody body)
            => this.CreateInputRequestAsync(body);

        Task<IEnumerable<WorkShopSummary>> IInputRequestService.QueryWorkShopsAsync(string BuId)
        => this.QueryWorkShopsAsync(BuId);
        Task<IEnumerable<LocationSummary>> IInputRequestService.QueryLocationsAsync(string BuId)
       => this.QueryLocationsAsync(BuId);
    }
}
