using System.Collections;

namespace CoWorker.Models.Blog
{
    using System;
    using System.Collections.Generic;

    public class PostState : Post,IEntity
    {
        public Guid ParentId { get; set; }
        public virtual User Owner { get; set; }
        public Int32 Level { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}
