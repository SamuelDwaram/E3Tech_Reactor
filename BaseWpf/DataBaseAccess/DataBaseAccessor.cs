using DataBaseAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAccess
{
    public class DataBaseAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User UpdateName(string nameUser)
        {
            User user = new User();

            user.SetName(nameUser);

            return user;
        }
    }
}
