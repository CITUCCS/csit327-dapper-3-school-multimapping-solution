using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperExer2.Context
{
    /// <summary>
    /// Wrapper class for database context.
    /// </summary>
    internal class DapperContext
    {
        private string _connectionString;

        /// <summary>
        /// Create new instance of DapperContext.
        /// </summary>
        /// <param name="connectionString">Connection string to the target database</param>
        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates a new connection to the database.
        /// </summary>
        /// <returns>The db connection</returns>
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
