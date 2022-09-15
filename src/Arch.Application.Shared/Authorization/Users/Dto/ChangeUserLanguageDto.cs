using System.ComponentModel.DataAnnotations;

namespace Arch.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
