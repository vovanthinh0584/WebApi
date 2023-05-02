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
        public DataTable QueryFilterWorks(string statementSql, IDictionary<string, object> param)
        {
            var result = _dao.ExecuteSP(statementSql, param);
            return result;
        }
        public int CreateGetTask(string statementSql, CreateGetTaskBody param)
        {
            int rowAffected = _dao.ExecuteSP(statementSql, param);

            return rowAffected;
        }

        public int FinishTask(string statementSql, object body)
        {
            int rowAffected = _dao.ExecuteSP(statementSql, body);

            return rowAffected;
        }
        public int AssignWorks(string statementSql, object body)
        {
            int result = _dao.ExecuteSP(statementSql, body);
            return result;
        }
       
        public IEnumerable<TeamDTO> QueryTeams()
        {
            string sql = _dao.GetSqlStatement("QueryTeams");
            IEnumerable<TeamDTO> result = _dao.Query<TeamDTO>(sql, null);
            return result;
        }
        public IEnumerable<WorkerDTO> QueryWorkers()
        {
            string sql = _dao.GetSqlStatement("QueryWorkers");
            IEnumerable<WorkerDTO> result = _dao.Query<WorkerDTO>(sql, null);
            return result;
        }
    }
}