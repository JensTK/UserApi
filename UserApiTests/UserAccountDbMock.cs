using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.DB;
using UserApi.Model;

namespace UserApiTests
{
    public class UserAccountDbMock : IUserAccountDb
    {
        public List<UserAccount> userAccounts { get; set; } = new();

        bool IUserAccountDb.Delete<T>(T obj)
        {
            if (typeof(T) == typeof(UserAccount))
            {
                return userAccounts.Remove(obj as UserAccount);
            }
            return false;
        }

        T IUserAccountDb.Get<T, Key>(Key key)
        {
            if (typeof(T) == typeof (UserAccount) && typeof(Key) == typeof(string))
            {
                return userAccounts.First(x => x.UserName == key as string) as T;
            }
            return null;
        }

        List<T> IUserAccountDb.GetList<T>()
        {
            if (typeof(T) == typeof(UserAccount))
            {
                return userAccounts as List<T>;
            }
            return new List<T>();
        }

        long IUserAccountDb.Insert<T>(T obj)
        {
            if (typeof(T) == typeof(UserAccount))
            {
                userAccounts.Add(obj as UserAccount);
                return userAccounts.Count;
            }
            return -1;
        }
        long IUserAccountDb.Insert<T>(List<T> obj)
        {
            if (typeof(T) == typeof(UserAccount))
            {
                userAccounts.AddRange(obj as List<UserAccount>);
                return userAccounts.Count;
            }
            return -1;
        }
        UserAccount? IUserAccountDb.GetUserAccountByUserName(string userName)
        {
            return userAccounts.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
