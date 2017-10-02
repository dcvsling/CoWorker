using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CoWorker.Models.Identity.Manages
{
    public class ManageLogins
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
    }
}
