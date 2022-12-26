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
    public class Sheet033BoilerService : ISheet033BoilerService
    {
        IDao _dao;
        IMessage _message;
        public Sheet033BoilerService(IDao dao, IMessage message)
        {
            _dao = dao ?? throw new ArgumentException(nameof(dao));
            _message = message ?? throw new ArgumentException(nameof(message));
        }
        public IEnumerable<WorkDTO> GetListWorks(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<WorkDTO> result = _dao.Query<WorkDTO>(sql, param);
            return result;
        }
        public IEnumerable<ShiftsAttimeDTO> GetListShiftsAttime(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<ShiftsAttimeDTO> result = _dao.Query<ShiftsAttimeDTO>(sql, param);
            return result;
        }

        public IEnumerable<ShiftsAttimeDTO> UpdateCheckinglist(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<ShiftsAttimeDTO> result = _dao.Query<ShiftsAttimeDTO>(sql, param);
            return result;
        }
        

        public DataTable GetCheckinglist(string statementSql, IDictionary<string, object> param)
        {
            var parameter = _dao.ExecuteSP(statementSql, param);
            return parameter;
        }
        public void  UpdateCheckinglist(string statementSql, List<CheckinglistDTO> checkinglist)
        {
            foreach(var item in checkinglist)
            {
                var param = new {Id=item.Id,Value=Convert.ToBoolean(item.Value), BUID = item.BUID};
                var result = _dao.StatementExecute(statementSql, param);
            }
            
        }

    }

    public interface ISheet033BoilerService
    {
        IEnumerable<WorkDTO> GetListWorks(string statementSql, object param);
        IEnumerable<ShiftsAttimeDTO> GetListShiftsAttime(string statementSql, object param);
        DataTable GetCheckinglist(string statementSql, IDictionary<string, object> param);
        void UpdateCheckinglist(string statementSql, List<CheckinglistDTO> checkinglist);

    }


}
