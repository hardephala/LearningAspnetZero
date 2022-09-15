using Abp.Application.Services.Dto;
using System;

namespace Arch.Customers.Dtos
{
    public class GetAllCustomersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string customerroleFilter { get; set; }

        public string customercodeFilter { get; set; }

        public string customernameFilter { get; set; }

        public string customergroupcodeFilter { get; set; }

        public string customergroupnameFilter { get; set; }

        public string primaryemailFilter { get; set; }

        public string altemailFilter { get; set; }

        public string phonenumberFilter { get; set; }

        public string accounttypeFilter { get; set; }

        public string linkedcodeFilter { get; set; }

        public string passwordFilter { get; set; }

        public string statusFilter { get; set; }

        public string notesFilter { get; set; }

    }
}