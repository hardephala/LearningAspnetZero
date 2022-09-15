using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Cargoreleases.Dtos
{
    public class GetCargoreleaseForEditOutput
    {
        public CreateOrEditCargoreleaseDto Cargorelease { get; set; }

    }
}