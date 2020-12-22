using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAccess.Models
{
    public class User
    {
        public string _name;
        public string Name
        {
            set
            {
                if(value.Contains("@"))
                {
                    throw new Exception("Not Allowed");
                }
                _name = value;
            }
            get
            {
                return _name;
            }
        }

        public int Age;

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
