using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // Generated 07/24/2013 16:13:17

    // Add custom code inside partial class

    public partial class Users : Repository<User>
    {
        public virtual User ByEmail(string email)
        {
            return Single(where: "Email = @0", parms: email);
        }
	}
}	
