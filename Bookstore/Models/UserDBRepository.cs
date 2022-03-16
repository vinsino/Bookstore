using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace Bookstore.Models
{
    public class UserDBRepository : IUserRepository, IDisposable
    {
        private string _bookConnection = "";
        private IDbConnection conn;

        public UserDBRepository()
        {
            _bookConnection = ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
            conn = new SQLiteConnection(_bookConnection);
        }

        public User Get(string UserId)
        {
            User ret;

            string sql = @"SELECT * FROM Users WHERE UserId=@Userid";
            ret = conn.Query<User>(sql, new { UserId }).SingleOrDefault();

            return ret;
        }

        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
            return;
        }
               
    }
}