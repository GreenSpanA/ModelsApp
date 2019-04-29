using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using ModelsApp.Models;

namespace ModelsApp.Repository
{
    public class FileRepository : FRepository<File>
    {
        private string connectionString;

        public FileRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
        
        public IEnumerable<File> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<File>("SELECT * FROM file WHERE id > 0");
            }
        }

        public IEnumerable<int> FindMax()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<int>("SELECT max(id) FROM file");
            }
        }

        public void Update(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE file SET id = @Id WHERE id = @Id", new { Id = id });
            }
        }


    }
}
