using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFashion_EF_Core.Models
{
    class User
    {
        public User()
        {
        }

        public User( string name, string passWord)
        {
          
            Name = name;
            PassWord = passWord;
        }

        public User(int id, string name, string passWord)
       : this(name, passWord)
        {
            Id = id;

        }

        public int Id { get;  set; }
        public string Name { get;  set; }
        public string PassWord { get;  set; }
    }
}
