using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace HHLDotNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query)
        {
            IDbConnection db = new MySqlConnection(_connectionString);
            var lst = db.Query<T>(query).ToList();

            return lst;

        }

        public T QueryFirstOrDefault<T>(string query,object? param = null)
        {
            IDbConnection db = new MySqlConnection(_connectionString);
            var item = db.QueryFirstOrDefault<T>(query,param);

            return item;

        }

        public int Execute(string query,object? param = null)
        {
            IDbConnection db = new MySqlConnection(_connectionString);
            var result = db.Execute(query,param);

            return result;

        }
    }
}