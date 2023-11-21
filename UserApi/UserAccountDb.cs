using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using UserApi.Model;

namespace UserApi
{
    public class UserAccountDb
    {
        private static readonly string connectionString = "Server=localhost\\SQLEXPRESS;Integrated Security=True;Database=userAccountDb;";
        public static List<T> GetList<T>() where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.GetAll<T>().AsList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public static T Get<T, Key>(Key key) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Get<T>(key);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public static long Insert<T>(T obj) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Insert<T>(obj);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public static bool Delete<T>(T obj) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Delete<T>(obj);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
    }
}
