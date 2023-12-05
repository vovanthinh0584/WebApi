using System.Collections.Generic;
using System.Data;
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

        DataTable QueryFilterWorks(string statementSql, IDictionary<string, object> param);

        int AssignWorks(string statementSql, object body);

        IEnumerable<TeamDTO> QueryTeams();
        IEnumerable<WorkerDTO> QueryWorkers();

        IEnumerable<TypeListDTO> GetTypeList();

        DataTable GetWorkTypes(string statementSql, IDictionary<string, object> param);
    }
}
