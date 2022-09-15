using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Longstandings.Dtos
{
    public class GetLongstandingForEditOutput
    {
        public CreateOrEditLongstandingDto Longstanding { get; set; }

    }
}