using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using Dapper.Contrib.Extensions;
using UserApi.Model;

namespace UserApi.DB
{
    public class UserAccountDb : IUserAccountDb
    {
        private string connectionString;

        public UserAccountDb(string connectionString) {
            this.connectionString = connectionString;
        }

        public List<T> GetList<T>() where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.GetAll<T>().AsList();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public T Get<T, Key>(Key key) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Get<T>(key);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public UserAccount? GetUserAccountByUserName(string userName)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.QueryFirstOrDefault<UserAccount>("SELECT * FROM UserAccounts WHERE UserName = @userName", new { userName });
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public long Insert<T>(T obj) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Insert(obj);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public long Insert<T>(List<T> obj) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Insert(obj);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
        public bool Delete<T>(T obj) where T : class, IDbObject
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                return connection.Delete(obj);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("DB request error: " + ex.Message);
                throw;
            }
        }
    }
}
