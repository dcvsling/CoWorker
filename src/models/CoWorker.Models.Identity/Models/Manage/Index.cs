using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CoWorker.Models.Identity.Manages
{
    public class Index
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public string AuthenticatorKey { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
    }
}
