using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.RequestBody.GetTask;

namespace WebApplication.Services
{
    public interface IGetTaskService
    {
        IEnumerable<QueryGetTaskSummary> QueryGetTask(string statementSql, object param);
        int CreateGetTask(string statementSql, CreateGetTaskBody param);
        int FinishTask(string statementSql,object body);
    }
}
