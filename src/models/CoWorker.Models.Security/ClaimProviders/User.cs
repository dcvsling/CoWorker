using System.Security.Claims;

namespace  CoWorker.Models.Identity
{
	using System;
	using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User
    {
        public Guid Id { get; set; }
        public string NameIdentifier { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageSource { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Sex { get; set; }
        public List<UserState> Status { get; set; }
    }

    public class UserState
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }

    public class Claim
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }

    public class RouteClaim
    {
        public Guid Id { get; set; }
        public string Path{ get; set; }
        public Claim Claims { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Creator { get; set; }
        public DateTimeOffset ModifyDate { get; set; }
        public string Modifier { get; set; }
    }
}
