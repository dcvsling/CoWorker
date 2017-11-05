using CoWorker.Primitives;
using System.Collections.Generic;

namespace CoWorker.Models.Identity.Manages
{
    public class ConfigureTwoFactor
    {
        public string SelectedProvider { get; set; }

        public ICollection<ListItem> Providers { get; set; }
    }
}
