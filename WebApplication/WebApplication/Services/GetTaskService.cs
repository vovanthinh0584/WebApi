using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.RequestBody.GetTask;
using WebApplication.Utils;

namespace WebApplication.Services
{
    public class GetTaskService : IGetTaskService
    {
        IDao _dao;
        IMessage _message;
        public GetTaskService(IDao dao, IMessage message)
        {
            _dao = dao ?? throw new ArgumentException(nameof(dao));
            _message = message ?? throw new ArgumentException(nameof(message));
        }
        public IEnumerable<QueryGetTaskSummary> QueryGetTask(string statementSql, object param)
        {
            //var json = JsonConvert.SerializeObject(param);
            //Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            //DataTable getTasks = _dao.ExecuteSP(statementSql, dictionary);
            IEnumerable<QueryGetTaskSummary> rows;
            using (IDbConnection db = _dao.CreateConnection())
            {
                rows = db.Query<QueryGetTaskSummary>(statementSql, param, null, true, null, CommandType.StoredProcedure);
            }
            return rows;
        }

        public int CreateGetTask(string statementSql, CreateGetTaskBody param)
        {
            int rowAffected = _dao.ExecuteSP(statementSql, param);

            return rowAffected;
        }
    }
}