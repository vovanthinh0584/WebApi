using System;
using System.Collections.Generic;
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
            string userId = "MOBILE_01";
            string buId = "BUID";
            string lang = "vi-VN";

            if (string.IsNullOrEmpty(buId))
            {
                throw new ArgumentNullException(nameof(buId));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrEmpty(lang))
            {
                throw new ArgumentNullException(nameof(lang));
            }

            if (body is null)
            {
                throw new ArgumentNullException(nameof(buId));
            }

            object paras = new
            {
                UserId = userId,
                Lang = lang,
                WorkshopId = body.WorkshopId,
                LocationId = body.LocationId,
                WorkerName = body.WorkerName,
                RequestedContent = body.RequestedContent,
                Reason = body.Reason,
                BUID = buId,
                ErrorMessage = string.Empty
            };

            int result = _dao.ExecuteSP(StoreProduceName.InputRequest.Insert, paras);

            if (result > 0)
            {
                return string.Empty;
            }

            return "Cannot create Voucher No";
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

        Task<string> IInputRequestService.CreateInputRequestAsync(CreateRequestInputBody body)
            => this.CreateInputRequestAsync(body);

        Task<IEnumerable<WorkShopSummary>> IInputRequestService.QueryWorkShopsAsync(string BuId)
        => this.QueryWorkShopsAsync(BuId);
        Task<IEnumerable<LocationSummary>> IInputRequestService.QueryLocationsAsync(string BuId)
       => this.QueryLocationsAsync(BuId);
    }
}
