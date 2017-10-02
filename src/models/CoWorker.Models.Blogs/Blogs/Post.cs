
namespace CoWorker.Models.Blog
{
    using System;
    using System.Collections.Generic;

    public class Post : IEntity
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String Content { get; set; }
        public String Source { get; set; }
        public virtual Guid Id { get; set; }
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset ModifyDate { get; set; }
        public virtual string Creator { get; set; }
        public virtual string Modifier { get; set; }
    }
}
