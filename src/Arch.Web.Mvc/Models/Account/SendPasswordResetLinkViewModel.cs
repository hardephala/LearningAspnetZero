using System.ComponentModel.DataAnnotations;

namespace Arch.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}