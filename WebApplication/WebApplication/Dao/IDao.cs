using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication
{
    public interface IDao
    {
        /// <summary>
        /// Get SQL command text 
        /// </summary>
        /// <param name="name">Name of statement</param>
        /// <returns>Command text</returns>
        string GetSqlStatement(string name);
        /// <summary>
        /// Execute the sql command with parameters
        /// </summary>
        /// <typeparam name="T">type of return object</typeparam>
        /// <param name="sql">SQL command text</param>
        /// <param name="param">Command's parameters</param>
        /// <returns>Enumerable of T</returns>
        IEnumerable<T> Query<T>(string sql, object param);
        /// <summary>
        /// Execute the sql command with parameters
        /// </summary>
        /// <param name="sql">SQL command text</param>
        /// <param name="param">Command's parameters</param>
        /// <returns>Return's value of command</returns>
        int Execute(string sql, object param);
        /// <summary>
        /// Execute the sql statement with parameters
        /// </summary>
        /// <typeparam name="T">type of return object</typeparam>
        /// <param name="statement">Statement's name</param>
        /// <param name="param">Command's parameters</param>
        /// <returns>Enumerable of T</returns>
        IEnumerable<T> StatementQuery<T>(string statement, object param);
		DataTable StatementQuery(string statement, IDictionary<string, object> param);
        DataTable QuerySP(string storeName, IDictionary<string, object> param);

        /// <summary>
        /// Execute the sql statement with parameters
        /// </summary>
        /// <param name="statement">Statement's name</param>
        /// <param name="param">Command's parameters</param>
        /// <returns>Return's value of statement</returns>
        int StatementExecute(string statement, object param);
		int ExecuteSP(string sql, object param);
		DataTable ExecuteSP(string ProcName, IDictionary<string, object> param);
		IDbConnection CreateConnection();
	}
}
