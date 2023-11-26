using UserApi.Model;

namespace UserApi.DB
{
    public interface IUserAccountDb
    {
        public List<T> GetList<T>() where T : class, IDbObject;
        public T Get<T, Key>(Key key) where T : class, IDbObject;
        public long Insert<T>(T obj) where T : class, IDbObject;
        public long Insert<T>(List<T> obj) where T : class, IDbObject;
        public bool Delete<T>(T obj) where T : class, IDbObject;
        public UserAccount? GetUserAccountByUserName(string userName);
    }
}
