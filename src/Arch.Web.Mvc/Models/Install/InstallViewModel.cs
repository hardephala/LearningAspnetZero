using System.Collections.Generic;
using Abp.Localization;
using Arch.Install.Dto;

namespace Arch.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
