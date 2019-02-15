using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // Generated 07/24/2013 16:13:17

    // Add custom code inside partial class

    public partial class User : Entity<User> 
	{
        public string FullName { get { return FirstName + " " + LastName; } }

        protected override void OnInserting(ref string sql)
        {
            if (SignupDate == null) SignupDate = DateTime.Now;
        }

        protected override void Validate()
        {
            // examples

            //ValidateLength(this.FirstName, "FirstName is too short");
            //ValidateExistence(this.LastName);
            //ValidateEmail(Email);
        }
	} 
}	
