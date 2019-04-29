using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using ModelsApp.Models;


namespace ModelsApp.Repository
{
    public class MenuRepository : IRepository<Menu>
    {
        private string connectionString;

        public MenuRepository(IConfiguration configuration)
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

        public void Add(Menu item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO samplemenu (category, dish, description," +
                    "veg_comment, price, file_id) VALUES(@Category, @Dish, @Description, " +
                    "@Veg_Comment, @Price, @File_Id)", item);
            }
        }

        public IEnumerable<Menu> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Menu>("SELECT * FROM samplemenu WHERE id > 0");
            }
        }   


        public Menu FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Menu>("SELECT * FROM samplemenu WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }      


        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM samplemenu WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Menu item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE samplemenu SET category = @Category,  dish  = @Dish, description = @Description, " +
                    "veg_comment = @Veg_Comment, price = @Price, file_id = @File_Id WHERE id = @Id", item);
            }
        }  
        
        public int FindCurrentFile(Menu item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<int>("SELECT file_id FROM samplemenu WHERE id = @Id", new { Id = item.Id }).FirstOrDefault();
            }
        }

        public int FindCurrentFileByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<int>("SELECT file_id FROM samplemenu WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

    }
}
