﻿using System;
using Abp.Authorization;
using Abp.Runtime.Validation;
using Arch.Dto;

namespace Arch.Authorization.Users.Dto
{
    public class GetLoginAttemptsInput: PagedAndSortedInputDto, IGetLoginAttemptsInput, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public AbpLoginResultType? Result { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }

            Filter = Filter?.Trim();
        }
    }
}
