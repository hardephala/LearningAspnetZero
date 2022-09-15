using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Donotreleases.Dtos
{
    public class GetDonotreleaseForEditOutput
    {
        public CreateOrEditDonotreleaseDto Donotrelease { get; set; }

    }
}