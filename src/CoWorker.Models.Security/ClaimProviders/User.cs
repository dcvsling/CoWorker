using System.Security.Claims;

namespace  CoWorker.Models.Identity
{
	using System;
	using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User
    {
        public string NameIdentifier { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
    }
}
