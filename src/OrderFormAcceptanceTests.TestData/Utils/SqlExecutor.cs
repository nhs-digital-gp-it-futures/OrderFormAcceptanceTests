using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OrderFormAcceptanceTests.TestData.Utils
{
    public static class SqlExecutor
    {
        public static IEnumerable<T> Execute<T>(string connectionString, string query, object param)
        {
            IEnumerable<T> returnValue = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Policies.RetryPolicy().Execute(() => { returnValue = connection.Query<T>(query, param); });
            }

            return returnValue;
        }

        public static int ExecuteScalar(string connectionString, string query, object param)
        {
            var result = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Policies.RetryPolicy().Execute(() => { result = connection.ExecuteScalar<int>(query, param); });
            }

            return result;
        }
    }
}
