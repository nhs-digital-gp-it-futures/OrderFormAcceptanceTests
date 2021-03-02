namespace OrderFormAcceptanceTests.TestData.Utils
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Dapper;

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

        public static async Task<IEnumerable<T>> ExecuteAsync<T>(string connectionString, string query, object param)
        {
            IEnumerable<T> returnValue = null;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            await Policies.RetryPolicyAsync().ExecuteAsync(async () => { returnValue = await connection.QueryAsync<T>(query, param); });
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
