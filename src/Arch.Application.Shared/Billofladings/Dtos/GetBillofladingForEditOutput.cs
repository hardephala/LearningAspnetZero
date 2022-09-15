using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Arch.Billofladings.Dtos
{
    public class GetBillofladingForEditOutput
    {
        public CreateOrEditBillofladingDto Billoflading { get; set; }

    }
}