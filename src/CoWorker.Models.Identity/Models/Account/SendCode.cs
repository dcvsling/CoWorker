
using CoWorker.Primitives;
using System.Collections.Generic;

namespace CoWorker.Models.Identity.Accounts
{
    public class SendCode
    {
        public string SelectedProvider { get; set; }

        public ICollection<ListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
