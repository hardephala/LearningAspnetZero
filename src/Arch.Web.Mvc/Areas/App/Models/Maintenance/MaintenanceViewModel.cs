using System.Collections.Generic;
using Arch.Caching.Dto;

namespace Arch.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}