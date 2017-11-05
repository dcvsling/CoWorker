using System.ComponentModel.DataAnnotations;

namespace CoWorker.Models.Identity.Accounts
{
    public class UseRecoveryCode
    {
        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}
