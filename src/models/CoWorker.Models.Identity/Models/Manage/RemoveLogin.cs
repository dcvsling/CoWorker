using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.Models.Identity.Manages
{
    public class RemoveLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
