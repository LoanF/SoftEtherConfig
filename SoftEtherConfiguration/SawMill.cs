using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftEtherConfiguration
{
    public class SawMill
    {
        public string SawMillName { get; set; }
        public Dictionary<string,User> Users { get; set; }

        public SawMill()
        {
            Users = new Dictionary<string,User>();
        }

        public void AddUser(User user)
        {
            Users.Add(user.UserName, user);
        }
    }
}