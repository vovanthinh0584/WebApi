using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Models;
using WebApplication.Models.RequestBody.InputRequest;
using WebApplication.Utils;

namespace WebApplication.Services
{
    public class InputDeviceParameterService : IInputDeviceParameterService
    {
        IDao _dao;
        IMessage _message;
        public InputDeviceParameterService(IDao dao, IMessage message)
        {
            _dao = dao ?? throw new ArgumentException(nameof(dao));
            _message = message ?? throw new ArgumentException(nameof(message));
        }

        public string CreateInputDeviceParameter(string statementSql, InputDeviceParameterDTO body)
        {

            object paras = new
            {
                BUID = body.BUID,
                Lang = body.Lang,
                USERID = body.UserId,
                Device = body.Device,
                Zone = body.Zone,
                Shift = body.Shift,
                Date = body.Date,
                ChecklistID = body.ChecklistID,
                StandardValue = body.StandardValue,
                Value = body.Value,
                Confirm = body.Confirm,
                Time = body.Time,
                Id = body.Id,
                NonConfirm = body.NonConfirm
            };
            int result = _dao.ExecuteSP(statementSql, paras);
            if (result > 0)
            {
                return string.Empty;
            }

            return "Cannot create Voucher No";
        }

        public IEnumerable<object> GetListAsset(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<object> listCaptions = _dao.Query<object>(sql, param);
            return listCaptions;
        }
        public IEnumerable<object> GetListOperating(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<object> listCaptions = _dao.Query<object>(sql, param);
            return listCaptions;
        }
        public IEnumerable<object> GetListUM(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<object> listCaptions = _dao.Query<object>(sql, param);
            return listCaptions;
        }

        public IEnumerable<object> GetListZone()
        {
            string sql = _dao.GetSqlStatement("GetListZone");
            IEnumerable<object> zones = _dao.Query<object>(sql, null);
            return zones;
        }

        public IEnumerable<object> GetListShift()
        {
            string sql = _dao.GetSqlStatement("GetListShift");
            IEnumerable<object> zones = _dao.Query<object>(sql, null);
            return zones;
        }

        public IEnumerable<object> GetListDevice()
        {
            string sql = _dao.GetSqlStatement("GetListDevice");
            IEnumerable<object> zones = _dao.Query<object>(sql, null);
            return zones;
        }

        public IEnumerable<object> GetListTime()
        {
            string sql = _dao.GetSqlStatement("GetListTime");
            IEnumerable<object> zones = _dao.Query<object>(sql, null);
            return zones;
        }

        public DataTable GetParameter(string statementSql, IDictionary<string, object> param)
        {
            var parameter = _dao.ExecuteSP(statementSql, param);
            return parameter;
        }
    }
}
