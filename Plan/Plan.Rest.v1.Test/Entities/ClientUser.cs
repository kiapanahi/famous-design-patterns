using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Rest.v1.Test
{
    public class ClientUser 
    {
        public ClientUser()
        {
            Tasks = new List<ClientTask>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<ClientTask> Tasks { get; set; }
    } 
}
