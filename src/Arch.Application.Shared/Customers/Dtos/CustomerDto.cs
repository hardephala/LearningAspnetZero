using System;
using Abp.Application.Services.Dto;

namespace Arch.Customers.Dtos
{
    public class CustomerDto : EntityDto<long>
    {
        public string customerrole { get; set; }

        public string customercode { get; set; }

        public string customername { get; set; }

        public string customergroupcode { get; set; }

        public string customergroupname { get; set; }

        public string primaryemail { get; set; }

        public string altemail { get; set; }

        public string phonenumber { get; set; }

        public string accounttype { get; set; }

        public string linkedcode { get; set; }

        public string password { get; set; }

        public string status { get; set; }

        public string notes { get; set; }

    }
}