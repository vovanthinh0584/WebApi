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
    public class InputDeviceParameterService : IInputDeviceParameterService
    {
        IDao _dao;
        IMessage _message;
        public InputDeviceParameterService(IDao dao, IMessage message)
        {
            _dao = dao ?? throw new ArgumentException(nameof(dao));
            _message = message ?? throw new ArgumentException(nameof(message));
        }

        public string CreateInputDeviceParameter(string statementSql,InputDeviceParameterDTO body)
        {
            
            object paras = new
            {
                BUID = body.BUID,
                Lang = body.Lang,
                UserId = body.UserId,
                InputDate = body.InputDate,
                AssetId = body.AssetId,
                OperatingId = body.OperatingID,
                UMID = body.UMID,
                Value = body.Value,
                Note=body.Note,
                RecordID =body.RecordID
                
            };
            int result = _dao.ExecuteSP(statementSql, paras);
            if(result > 0)
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
        public DataTable GetParameter(string statementSql,IDictionary<string, object> param)
        {
            var parameter = _dao.ExecuteSP(statementSql, param);
            return parameter;
        }
    }
}
