using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginRepository : ILoginRepository
    {
       private MyCmsContext db;
        public LoginRepository(MyCmsContext context)
        {
            this.db = context;
        }
        public bool IsExistUser(string usernme, string password)
        {
            return db.AddminLogins.Any(u => u.UserName == usernme && u.Password == password);
        }
    }
}
