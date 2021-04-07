namespace OrderFormAcceptanceTests.TestData.Utils
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Dapper;

    public static class SqlExecutor
    {
        public static async Task<IEnumerable<T>> ExecuteAsync<T>(string connectionString, string query, object param)
        {
            IEnumerable<T> returnValue = null;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            await Policies.RetryPolicyAsync().ExecuteAsync(async () => { returnValue = await connection.QueryAsync<T>(query, param); });
            return returnValue;
        }

        internal static async Task<int> ExecuteScalarAsync(string connectionString, string query, object param)
        {
            int returnValue = 0;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            await Policies.RetryPolicyAsync().ExecuteAsync(async () => { returnValue = await connection.ExecuteScalarAsync<int>(query, param); });
            return returnValue;
        }
    }
}
