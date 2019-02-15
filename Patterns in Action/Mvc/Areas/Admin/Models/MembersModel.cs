using Mvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Areas.Admin.Models
{
    // members ViewModel

    // ** Data Transfer Object (DTO) pattern

    public class MembersModel
    {
        public string Message { get; set; }

        // sortable list of members

        public SortedList<MemberModel> Members { get; set; }
    }
}