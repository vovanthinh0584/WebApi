using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace WebApplication
{
	public class Dao : IDao
	{
		private IDictionary<string, string> _xmlDictionary;
		private string _connectionString;

		public Dao(string ConnectionString, string[] sqlFiles) : base()
		{
			_connectionString = ConnectionString;
			LoadStatements(sqlFiles);
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}

		public IEnumerable<T> StatementQuery<T>(string statement, object param)
		{
			string sql = string.Empty;

			try
			{
				sql = GetSqlStatement(statement);
			}
			catch (KeyNotFoundException)
			{
				throw new KeyNotFoundException($"'{statement}' was not found!");
			}

			if (string.IsNullOrEmpty(sql))
				throw new InvalidDataException($"Content of '{statement}' is empty!");

			return Query<T>(sql, param);
		}
		public DataTable StatementQuery(string statement, IDictionary<string, object> _param)
		{
			string sql = string.Empty;

			try
			{
				sql = GetSqlStatement(statement);
			}
			catch (KeyNotFoundException)
			{
				throw new KeyNotFoundException($"'{statement}' was not found!");
			}

			if (string.IsNullOrEmpty(sql))
				throw new InvalidDataException($"Content of '{statement}' is empty!");

			return Query(sql, _param, CommandType.Text);
		}

		public DataTable QuerySP(string storeName, IDictionary<string, object> _param)
		{
			return Query(storeName, _param, CommandType.StoredProcedure);
		}

		public int StatementExecute(string statement, object param)
		{
			string sql = string.Empty;

			try
			{
				sql = GetSqlStatement(statement);
			}
			catch (KeyNotFoundException)
			{
				throw new KeyNotFoundException($"'{statement}' was not found!");
			}

			if (string.IsNullOrEmpty(sql))
				throw new InvalidDataException($"Content of '{statement}'is empty!");

			return Execute(sql, param);
		}

		public IEnumerable<T> Query<T>(string sql, object param)
		{
			IEnumerable<T> lst = null;

			using (IDbConnection db = CreateConnection())
			{
				lst = db.Query<T>(sql, param);
			}
			return lst;
		}
		public int Execute(string sql, object param)
		{
			int rowAffected = 0;
			using (IDbConnection db = CreateConnection())
			{
				rowAffected = db.Execute(sql, param);
			}
			return rowAffected;
		}

		public int ExecuteSP(string sql, object param)
		{
			int rowAffected = 0;
			using (IDbConnection db = CreateConnection())
			{
				rowAffected = db.Execute(sql, param, null, null, CommandType.StoredProcedure);
			}
			return rowAffected;
		}
		public DataTable ExecuteSP(string ProcName, IDictionary<string, object> _param)
		{
			return Query(ProcName, _param, CommandType.StoredProcedure);
		}

		public string GetSqlStatement(string name)
		{
			return GetSQL(name);
		}
		public DataTable Query(string sql, IDictionary<string, object> param, CommandType commandType)
		{
			DataTable dt = new DataTable();
			using (var db = CreateConnection())
			{
				using (IDbCommand cmd = db.CreateCommand())
				{
					cmd.CommandType = commandType;
					cmd.CommandText = sql;
					SqlCommand command = cmd as SqlCommand;
					//if (commandType != CommandType.StoredProcedure)
					//{
					foreach (var key in param)
					{
						command.Parameters.AddWithValue("@" + key.Key, key.Value ?? DBNull.Value);
					}
					//}
					SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd as SqlCommand);
					dataAdapter.Fill(dt);
				}
			}
			return dt;
		}
		private DbDataAdapter CreateAdapter(DbCommand cmd)
		{
			return new SqlDataAdapter(cmd as SqlCommand);
		}
		private void LoadStatements(string[] files)
		{
			_xmlDictionary = new Dictionary<string, string>();

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(StatementCollection));

			foreach (string file in files)
			{
				string f = file?.Trim();

				if (!string.IsNullOrEmpty(f))
				{
					StatementCollection statements = null;
					using (StreamReader reader = new StreamReader(f))
					{
						statements = (StatementCollection)serializer.Deserialize(reader);
					}

					foreach (var item in statements.Statements)
					{
						if (_xmlDictionary.ContainsKey(item.Id.Trim()))
						{
							_xmlDictionary[item.Id.Trim()] = item.Text?.Trim();
						} else
						{
							_xmlDictionary.Add(item.Id?.Trim(), item.Text?.Trim());
						}
					}
				}
			}
		}

		protected virtual string GetSQL(string id)
		{

			return _xmlDictionary[id];
		}
	}

	[System.Diagnostics.DebuggerDisplay("{Id}")]
	public class SqlStatement
	{
		[System.Xml.Serialization.XmlAttribute("id")]
		public string Id { get; set; }

		[System.Xml.Serialization.XmlText]
		public string Text { get; set; }
	}


	[System.Xml.Serialization.XmlRoot("Statements")]
	public class StatementCollection
	{
		[System.Xml.Serialization.XmlElement("Statement", typeof(SqlStatement))]
		public SqlStatement[] Statements { get; set; }
	}

	public class His1
	{

	}

	public class His2
	{

	}

	public class Core
	{

	}
}