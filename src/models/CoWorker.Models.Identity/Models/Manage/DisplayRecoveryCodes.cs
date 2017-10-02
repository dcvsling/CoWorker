using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoWorker.Models.Identity.Manages
{
    public class DisplayRecoveryCodes
    {
        [Required]
        public IEnumerable<string> Codes { get; set; }

    }
}
